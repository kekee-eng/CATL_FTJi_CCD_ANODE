using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DetectCCD
{

    public class EntryDetect : IDisposable
    {

        public EntryDetect(EntryGrab grab, bool isInner)
        {

            //
            this.grab = grab;
            this.isinner = isInner;

        }
        public void Dispose() {

            _IN_8K_FROM_4K.Clear();

            EAs.Clear();
            Tabs.Clear();
            Defects.Clear();
            Labels.Clear();

            Marks.Clear();

            TabsCache.Clear();
            LabelsCache.Clear();

            ShowEACount = 0;
            ShowEADefectNGCount = 0;
            ShowEAWidthNGCount = 0;

            m_frame = 1;
            defectFrameCount = 0;
            posEAStart = -1;

            DiffFrame = Static.App.FixFrameOuterOrBackOffset;

            CountOfFrameMaybeDefect = 0;
            CountOfFrameRealDefect = 0;

            CountOfDetectDefectTotal = 0;
            CountOfDetectDefectReal = 0;
        }

        EntryGrab grab;
        bool isinner;

        public double Fx { get { return grab.Fx; } }
        public double Fy { get { return grab.Fy; } }

        public int m_frame = 1;
        public int CountOfFrameMaybeDefect = 0;
        public int CountOfFrameRealDefect = 0;

        public int CountOfDetectDefectTotal = 0;
        public int CountOfDetectDefectReal = 0;

        public List<DataEA> EAs = new List<DataEA>();
        public List<DataTab> Tabs = new List<DataTab>();
        public List<DataDefect> Defects = new List<DataDefect>();
        public List<DataLabel> Labels = new List<DataLabel>();
        public List<DataMark> Marks = new List<DataMark>();

        public List<DataTab> TabsCache = new List<DataTab>();
        public List<DataLabel> LabelsCache = new List<DataLabel>();

        public DataTab prevTab = null;

        void addLabel(DataLabel lab)
        {
            Log.Record(() =>
            {

                var repeat1 = LabelsCache.Find(x => Math.Abs(x.Y - lab.Y) < 0.5);
                var repeat2 = Labels.Find(x => Math.Abs(x.Y - lab.Y) < 0.5);
                if (repeat1 == null && repeat2 == null)
                {
                    lab.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);
                    LabelsCache.Add(lab);
                }
            });
        }
        void checkLabel(int frame)
        {
            Log.Record(() =>
            {
                if (LabelsCache.Count == 0)
                    return;

                var minLab = LabelsCache.OrderBy(x => x.Y).First();
                if (minLab != null)
                {
                    if (minLab.Y < frame)
                    {
                        LabelsCache.Remove(minLab);
                        minLab.Encoder = grab.GetEncoder(minLab.Y);

                        if (null == Labels.Find(x => Math.Abs(x.Y - minLab.Y) < 0.5))
                        {
                            Labels.Add(minLab);
                            OnNewLabel?.Invoke(minLab);
                        }
                    }
                }
            });

        }

        public int ShowEACount = 0;
        public int ShowEACountView
        {
            get
            {
                var count1 = Tabs.Count / Static.Recipe.CheckTabCount;
                if (Math.Abs(count1 - ShowEACount) < 3)
                    return ShowEACount;
                else
                    return count1;
            }
        }
        public int ShowEAWidthNGCount = 0;
        public int ShowEADefectNGCount = 0;

        public event Action<DataLabel> OnNewLabel;
        public event Action<DataTab, DataTab> OnSyncTab;

        int defectFrameCount = 0;
        public double DiffFrame;

        public void TryTransLabel(int frame)
        {

            //
            checkLabel(frame);

            //转标签
            if (Static.Tiebiao.EnableLabelDefect)
            {
                var remoteDefs = RemoteDefect.In4KCall8K_GetDefectList(true, isinner, -1);
                if (remoteDefs != null)
                {
                    foreach (var rl in remoteDefs)
                    {
                        if (rl.IsTransLabel())
                        {
                            addLabel(new DataLabel()
                            {
                                Y = rl.Y + Static.Tiebiao.LabelY_Defect / Fy,
                                Comment = string.Format("转标[正面][{0}]", rl.GetTypeCaption())
                            });
                        }
                    }
                }
            }
            if (Static.Tiebiao.EnableLabelDefect)
            {
                var remoteDefs = RemoteDefect.In4KCall8K_GetDefectList(false, isinner, -1);
                if (remoteDefs != null)
                {
                    foreach (var rl in remoteDefs)
                    {
                        if (rl.IsTransLabel())
                        {
                            addLabel(new DataLabel()
                            {
                                Y = rl.Y + Static.Tiebiao.LabelY_Defect / Fy,
                                Comment = string.Format("转标[背面][{0}]", rl.GetTypeCaption())
                            });
                        }
                    }
                }
            }

        }
        public void TrySync(EntryDetect partner) {

            // diff = this - partner
            // this = partner + diff
            // partner = this - diff

            //需要同步的对象
            var myER = TabsCache.Last();
            if (myER == null)
                return;

            //对齐Mark孔
            if (myER.IsNewEA) {

                double posMe = myER.MarkY;
                double posPartner = posMe - DiffFrame;
                double posPartnerReal = 0;
                for (int i = partner.Marks.Count - 1; i >= 0; i--) {

                    var dist = posPartner - partner.Marks[i].MarkY;

                    if (dist > -10 && dist < 10) {

                        if (dist > Static.App.OnceAdjustValue)
                            dist = Static.App.OnceAdjustValue;
                        if (dist < -Static.App.OnceAdjustValue)
                            dist = -Static.App.OnceAdjustValue;

                        posPartnerReal = partner.Marks[i].MarkY;
                        DiffFrame += dist;
                        break;
                    }
                }

                Log.AppLog.Info(string.Format("ME, P, PReal, FIX -> {0:0.0}, {1:0.0}, {2:0.0}, {3:0.0}",
                    posMe,
                    posPartner,
                    posPartnerReal,
                    DiffFrame - Static.App.FixFrameOuterOrBackOffset
                    ));

            }

            //尝试找到对应项
            var bindER = partner.findBind(myER.TabY1 - DiffFrame);
            if (bindER == null) {

                //未找到：对方补测一个宽度
                bindER = partner.fixER(myER.TabX, myER.TabY1 - DiffFrame, myER.TabY2 - DiffFrame);
            }

            //找到：标记已同步
            myER.IsSync = true;
            bindER.IsSync = true;

            //同步EA头
            if (myER.IsNewEA || bindER.IsNewEA) {
                myER.IsNewEA = true;
                bindER.IsNewEA = true;

                if (myER.MarkX == 0 && bindER.MarkX != 0) {
                    myER.MarkX = bindER.MarkX;
                    myER.MarkY = bindER.MarkY + DiffFrame;
                }

                if (myER.MarkX != 0 && bindER.MarkX == 0) {
                    bindER.MarkX = myER.MarkX;
                    bindER.MarkY = myER.MarkY - DiffFrame;
                }
            }

            //查看我方是否有漏测
            do {
                var missER = partner.TabsCache.Find(x => x.TabY1 < bindER.TabY1);
                if (missER == null)
                    break;

                //补测宽度
                var myMissER = fixER(missER.TabX, missER.TabY1 + DiffFrame, missER.TabY2 + DiffFrame);

                //同步EA头
                if (missER.IsNewEA || myMissER.IsNewEA) {
                    missER.IsNewEA = true;
                    myMissER.IsNewEA = true;

                    if (missER.MarkX == 0 && myMissER.MarkX != 0) {
                        missER.MarkX = myMissER.MarkX;
                        missER.MarkY = myMissER.MarkY - DiffFrame;
                    }

                    if (missER.MarkX != 0 && bindER.MarkX == 0) {
                        myMissER.MarkX = missER.MarkX;
                        myMissER.MarkY = missER.MarkY + DiffFrame;
                    }
                }

                //Fix: 宽度值为0
                if (myMissER.ValWidth == 0 && missER.ValWidth != 0) {
                    myMissER.ValWidth = missER.ValWidth;
                }
                if (myMissER.ValWidth != 0 && missER.ValWidth == 0) {
                    missER.ValWidth = myMissER.ValWidth;
                }

                myMissER.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);
                missER.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);

                //
                appendTab(myMissER);
                partner.appendTab(missER);

            } while (true);

            //Fix: 宽度值为0
            if (myER.ValWidth == 0 && bindER.ValWidth != 0) {
                myER.ValWidth = bindER.ValWidth;
            }
            if (myER.ValWidth != 0 && bindER.ValWidth == 0) {
                bindER.ValWidth = myER.ValWidth;
            }

            myER.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);
            bindER.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);

            //添加到列表
            appendTab(myER);
            partner.appendTab(bindER);

            //
            if (myER.ValWidth != 0 && bindER.ValWidth != 0)
                OnSyncTab?.Invoke(myER, bindER);

        }
        public bool TryAddTab(DataTab data)
        {
            //是否新极耳
            int w = grab.Width;
            int h = grab.Height;
            bool isNewData = true;
            if (Tabs.Count > 0 || TabsCache.Count > 0)
            {

                var nearTab = (TabsCache.Count > 0 ? TabsCache.Last() : Tabs.Last());
                if (nearTab != null && Math.Abs(data.TabY1 - nearTab.TabY2) * Fy < Static.Recipe.TabMergeDistance)
                {

                    //更新极耳大小
                    double dist = 30 / Fx;
                    if (data.HasTwoTab || nearTab.HasTwoTab || Math.Abs(data.TabX - nearTab.TabX) >= dist)
                    {

                        //
                        double leftx, rightx;
                        if (data.HasTwoTab)
                        {
                            leftx = data.TabX;
                            rightx = data.TabX_P;
                        }
                        else if (nearTab.HasTwoTab)
                        {
                            leftx = nearTab.TabX;
                            rightx = nearTab.TabX_P;
                        }
                        else
                        {
                            leftx = Math.Min(data.TabX, nearTab.TabX);
                            rightx = Math.Max(data.TabX, nearTab.TabX);
                        }

                        //
                        Action<double, DataTab, List<double>> addPoint = (x, dt, list) =>
                        {
                            if (Math.Abs(dt.TabX - x) < dist)
                            {
                                list.Add(dt.TabY1);
                                list.Add(dt.TabY2);
                            }

                            if (dt.HasTwoTab && Math.Abs(dt.TabX_P - x) < dist)
                            {
                                list.Add(dt.TabY1_P);
                                list.Add(dt.TabY2_P);
                            }
                        };

                        //
                        List<double> left = new List<double>();
                        List<double> right = new List<double>();
                        addPoint(leftx, data, left);
                        addPoint(leftx, nearTab, left);
                        addPoint(rightx, data, right);
                        addPoint(rightx, nearTab, right);

                        //
                        nearTab.TabX = leftx;
                        nearTab.TabX_P = rightx;
                        nearTab.TabY1 = left.Min();
                        nearTab.TabY2 = left.Max();
                        nearTab.TabY1_P = right.Min();
                        nearTab.TabY2_P = right.Max();

                        //
                        nearTab.HasTwoTab = true;
                    }
                    else
                    {
                        nearTab.TabY1 = Math.Min(nearTab.TabY1, data.TabY1);
                        nearTab.TabY2 = Math.Max(nearTab.TabY2, data.TabY2);
                    }

                    //
                    isNewData = false;
                }
            }

            if (!isNewData)
                return false;

            //宽度检测
            double[] bx1, bx2;
            double bfy1 = data.TabY1 + Static.Recipe.TabWidthStart / Fy;
            double bfy2 = data.TabY1 + Static.Recipe.TabWidthEnd / Fy;
            var bimage = grab.GetImage(bfy1, bfy2);
            if (bimage != null && ImageProcess.DetectWidth(bimage, out bx1, out bx2))
            {
                data.WidthY1 = bfy1;
                data.WidthY2 = bfy2;
                data.WidthX1 = bx1[0] / w;
                data.WidthX2 = bx2[0] / w;
            }
            bimage?.Dispose();

            //去除异常位置的测宽
            if (prevTab != null)
            {
                double py1 = prevTab.TabY1;
                double py2 = prevTab.TabY2;

                if (py2 >= bfy1 && py1 <= bfy2)
                {
                    data.WidthX1 = prevTab.WidthX1;
                    data.WidthX2 = prevTab.WidthX2;
                    data.WidthY1 = prevTab.WidthY1;
                    data.WidthY2 = prevTab.WidthY2;
                }
            }
            prevTab = data;

            //
            data.ValWidth = (data.WidthX2 - data.WidthX1) * Fx;
            data.ValHeight = (data.TabY2 - data.TabY1) * Fy;

            //判断有MARK孔
            if (Marks.Count > 0)
            {
                //最近一个Mark孔位置
                var dm = Marks.Last();
                //附近极耳区域
                var m1 = dm.MarkY;
                var m2 = dm.MarkY + 2;

                //待检测的极耳区域
                var y1 = data.TabY1;
                var y2 = data.TabY2;

                //区域存在交集
                var b = m2 >= y1 && m1 <= y2;

                if (b)
                {
                    data.IsNewEA = true;
                    data.HasTwoMark = dm.HasTwoMark;

                    data.MarkX = dm.MarkX;
                    data.MarkY = dm.MarkY;

                    data.MarkX_P = dm.MarkX_P;
                    data.MarkX_P = dm.MarkX_P;

                    data.MarkImageStart = dm.MarkImageStart;
                    data.MarkImageEnd = dm.MarkImageEnd;

                }
            }

            //
            TabsCache.Add(data);

            //
            return true;
        }
        public void In4kAlign8k(double dm)
        {
            //
            Static.App.DiffFrameInnerFrontFix = RemoteDefect.In4kAlign8k(true, true, dm);
        }
        public void AddLableToPlc(DataTab data, double dm)
        {

            //
            int ea = 0;
            if (Tabs.Count != 0)
            {
                ea = Tabs.Last().EA;
            }

            var objEA = getEA(ea);

            //FIX: 外侧相机前贴标机不打标
            if (objEA != null && !objEA.IsFail && objEA.TabCount < 5)
            {
                objEA = getEA(ea - 1);
            }

            if (objEA != null)
            {

                //添加标签
                if (Static.App.Is4K && Static.Tiebiao.EnableLabelEA)
                {
                    if (objEA.IsFail || Static.Tiebiao.EnableLabelEA_EveryOne)
                    {
                        var objLab = new DataLabel()
                        {
                            EA = ea,
                            Y = dm + Static.Tiebiao.LabelY_EA / Fy,
                            Comment = (Static.Tiebiao.EnableLabelEA_EveryOne ? "[测试]" : "") + "EA末端贴标: " + objEA.GetFailReason()
                        };
                        objLab.Encoder = grab.GetEncoder(objLab.Y);
                        addLabel(objLab);
                    }
                }

                //
                posEAStart = dm;
            }



            //强制打标
            if (Static.App.Is4K
                && Static.Tiebiao.EnableLabelEA
                && Static.Tiebiao.EnableLabelEA_Force
                && Static.Recipe.EALength > 100)
            {

                if (posEAStart >= 0)
                {
                    var y0 = posEAStart + Static.Recipe.EALength / Fy;
                    if (data.TabY1 > y0)
                    {
                        posEAStart = -1;

                        var objLab = new DataLabel()
                        {
                            EA = data.EA,
                            Y = y0,
                            Comment = "EA末端强制贴标"
                        };

                        objLab.Encoder = grab.GetEncoder(objLab.Y);
                        addLabel(objLab);
                    }
                }
            }
        }

        public void TryAddDefect(bool hasDefect, int frame) {
            //若有瑕疵，先缓存图片，直到瑕疵结束或图像过大
            if (hasDefect) {
                defectFrameCount++;
                CountOfFrameMaybeDefect++;
            }

            if (defectFrameCount >= 3 || (!hasDefect && defectFrameCount > 0)) {
                //拼成大图进行瑕疵检测
                int w = grab.Width;
                int h = grab.Height;
                int efx1 = frame - defectFrameCount;
                int efx2 = frame - 1;
                defectFrameCount = 0;

                //
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                var savefolder = Static.FolderRecord + "/瑕疵小图/";
                var savefolder1 = savefolder + "接头/";
                var savefolder2 = savefolder + "标签/";
                var savefolder3 = savefolder + "漏金属/";
                var savefolder4 = savefolder + "其它/";

                if (!System.IO.Directory.Exists(savefolder1))
                    System.IO.Directory.CreateDirectory(savefolder1);
                if (!System.IO.Directory.Exists(savefolder2))
                    System.IO.Directory.CreateDirectory(savefolder2);
                if (!System.IO.Directory.Exists(savefolder3))
                    System.IO.Directory.CreateDirectory(savefolder3);
                if (!System.IO.Directory.Exists(savefolder4))
                    System.IO.Directory.CreateDirectory(savefolder4);

                //
                var savefolderBig = Static.FolderRecord + "/瑕疵大图/";
                var saveBigFilename = string.Format("{0}{1}_{2}_F{3}", savefolderBig, timestamp, isinner ? "正面" : "背面", frame);

                if (!System.IO.Directory.Exists(savefolderBig))
                    System.IO.Directory.CreateDirectory(savefolderBig);

                //多线程运算
                Log.RecordAsThread(() => {
                    Log.Record(() => {

                        //统计检测次数
                        CountOfDetectDefectTotal++;

                        //
                        var eimage = grab.GetImage(efx1, efx2);
                        int[] etype;
                        double[] ex, ey, ew, eh, earea;
                        if (eimage != null && ImageProcess.DetectDefect(eimage, out etype, out ex, out ey, out ew, out eh, out earea)) {

                            int ecc = new int[] { etype.Length, ex.Length, ey.Length, ew.Length, eh.Length }.Min();
                            var myDefects = new List<DataDefect>();
                            for (int i = 0; i < ecc; i++) {
                                DataDefect defect = new DataDefect() {
                                    EA = -1,
                                    Type = etype[i],
                                    X = ex[i] / w,
                                    Y = efx1 + ey[i] / h,
                                    W = ew[i] / w,
                                    H = eh[i] / h
                                };

                                defect.Width = defect.W * Fx;
                                defect.Height = defect.H * Fy;
                                defect.Area = earea[i] * Fx * Fy / w / h;

                                //判断膜漏金属和AT9漏金属的规格
                                if (defect.Type == 2 && defect.Area <= Static.Recipe.FilmLeakMetalArea)
                                    continue;

                                defect.Timestamp = UtilTool.GenTimeStamp(DateTime.Now);
                                myDefects.Add(defect);
                            }

                            if (myDefects.Count > 0) {

                                //统计发现缺陷次数
                                CountOfDetectDefectReal++;
                                CountOfFrameRealDefect += defectFrameCount;
                            }

                            //添加到列表中
                            lock (Defects) {
                                Defects.AddRange(myDefects);
                            }

                            if (Static.App.RecordSaveImageEnable) {

                                //保存NG小图
                                bool hasSmallDefect = false;
                                for (int i = 0; i < myDefects.Count; i++) {
                                    var defect = myDefects[i];
                                    if (defect.Type < Static.App.RecordSaveImageNGSmallMaxType) {

                                        hasSmallDefect = true;
                                        if (Static.App.RecordSaveImageNGSmall) {

                                            string folder = "";
                                            if (defect.Type == 0)
                                                folder = savefolder1;
                                            else if (defect.Type == 1)
                                                folder = savefolder2;
                                            else if (defect.Type == 2)
                                                folder = savefolder3;
                                            else
                                                folder = savefolder4;

                                            var filename = string.Format("{0}{1}_{2}_{3}_F{4}_C{5}", folder, timestamp, defect.GetTypeCaption(), isinner ? "正面" : "背面", frame, i);
                                            defect.NGPartPath = filename;

                                            double h0 = Math.Max(eh[i], 450) + 50;
                                            double w0 = Math.Max(ew[i], 450) + 50;

                                            Log.Record(() => {
                                                double y1 = ey[i] - h0 / 2;
                                                double x1 = ex[i] - w0 / 2;
                                                double y2 = ey[i] + h0 / 2 - 1;
                                                double x2 = ex[i] + w0 / 2 - 1;

                                                int twidth, theight;
                                                eimage.GetImageSize(out twidth, out theight);
                                                y1 = Math.Max(0, y1);
                                                x1 = Math.Max(0, x1);
                                                y2 = Math.Min(theight - 1, y2);
                                                x2 = Math.Min(twidth - 1, x2);
                                                var saveimg = eimage.CropRectangle1(y1, x1, y2, x2);
                                                UtilSaveImageQueue.Put(saveimg, filename);
                                            });
                                        }
                                    }
                                }

                                //保存NG大图
                                if (hasSmallDefect && Static.App.RecordSaveImageNGBig) {
                                    var saveimg = eimage.CopyImage();
                                    myDefects[0].NGBigPath = saveBigFilename;
                                    UtilSaveImageQueue.Put(saveimg, saveBigFilename);
                                }
                            }

                        }

                        //
                        eimage?.Dispose();

                    });
                });

            }
        }

        DataTab findBind(double frame)
        {
            return TabsCache.Find(x => Math.Abs(x.TabY1 - frame) * Fy < Static.Recipe.TabMergeDistance);
        }
        DataTab fixER(double x, double y1, double y2)
        {

            int w = grab.Width;
            int h = grab.Height;

            DataTab data = new DataTab();
            data.TabX = x;
            data.TabY1 = y1;
            data.TabY2 = y1;
            data.HasTwoMark = false;

            //宽度检测
            if (Tabs.Count > 0)
            {
                //
                var dataCopy = Tabs.Last();
                data.WidthY1 = dataCopy.WidthY1;
                data.WidthY2 = dataCopy.WidthY2;
                data.WidthX1 = dataCopy.WidthX1;
                data.WidthX2 = dataCopy.WidthX2;

                //
                for (int i = Tabs.Count() - 1; i >= 0; i--)
                {
                    if (!Tabs[i].IsFix)
                    {
                        dataCopy = Tabs[i];
                        data.WidthY1 = dataCopy.WidthY1;
                        data.WidthY2 = dataCopy.WidthY2;
                        data.WidthX1 = dataCopy.WidthX1;
                        data.WidthX2 = dataCopy.WidthX2;
                        break;
                    }
                }
            }

            //
            data.ValWidth = (data.WidthX2 - data.WidthX1) * Fx;
            data.ValHeight = (data.TabY2 - data.TabY1) * Fy;

            ////EA头部Mark检测
            //double[] cx, cy;
            //double cfy1 = data.TabY1 + Static.Recipe.EAStart / Fy;
            //double cfy2 = data.TabY1 + Static.Recipe.EAEnd / Fy;

            ////
            //data.MarkImageStart = cfy1;
            //data.MarkImageEnd = cfy2;

            //var cimage = grab.GetImage(cfy1, cfy2);
            //if (ImageProcess.DetectMark(cimage, out cx, out cy))
            //{

            //    //将最后一个极耳放到下个EA中
            //    data.IsNewEA = true;
            //    data.MarkX = data.MarkX_P = cx[0] / w;
            //    data.MarkY = data.MarkY_P = cfy1 + cy[0] / h;

            //    if (cx.Length == 2 && cy.Length == 2)
            //    {
            //        data.HasTwoMark = true;
            //        data.MarkX_P = cx[1] / w;
            //        data.MarkY_P = cfy1 + cy[1] / h;
            //    }
            //}
            //cimage?.Dispose();

            data.IsFix = true;
            data.IsSync = true;

            TabsCache.Add(data);
            return data;
        }
        DataEA _get_ea(int id)
        {
            var curEaTab = Tabs.Find(x => x.EA == id && x.TAB == 1);
            if (curEaTab == null)
                return null;

            DataEA obj = new DataEA();
            obj.EA = id;
            obj.TabCount = Tabs.Count(x => x.EA == id);
            obj.TabWidthFailCount = Tabs.Count(x => x.EA == id && x.IsWidthFail);

            if (curEaTab.IsNewEA)
            {
                obj.EAX = curEaTab.MarkX;
                obj.EAY = curEaTab.MarkY;
            }
            else
            {
                obj.EAX = curEaTab.TabX;
                obj.EAY = curEaTab.TabY1;
            }

            double start = obj.EAY;
            double end = 0;
            var nextEaTab = Tabs.Find(x => x.EA == id + 1 && x.TAB == 1);
            if (nextEaTab != null)
            {
                end = nextEaTab.MarkY;
            }
            else
            {
                if (TabsCache.Count > 0)
                {
                    end = TabsCache.Select(x => x.TabY1).Max();
                }
                else
                {
                    end = Tabs.FindAll(x => x.EA == id).Select(x => x.TabY1).Max();
                }
            }

            if (Marks.Count > 0)
            {
                var markvalue = Marks.Select(x => x.MarkY).OrderBy(x => Math.Abs(x - end)).First();
                if (Math.Abs(markvalue - end) < 5)
                    end = markvalue;
            }
            AllocAndGetDefectCount(start, end, id);

            if (Static.App.Is4K)
            {
                //修复偶尔未从8K同步到标签的Bug
                for (int i = 0; i < 3; i++)
                {
                    var setFront = RemoteDefect.In4KCall8K_GetDefectCount(true, isinner, start, end, id);
                    var setBack = RemoteDefect.In4KCall8K_GetDefectCount(false, isinner, start, end, id);
                }

                var remoteDefsFront = RemoteDefect.In4KCall8K_GetDefectList(true, isinner, id);
                var remoteDefsBack = RemoteDefect.In4KCall8K_GetDefectList(false, isinner, id);

                if (remoteDefsFront != null)
                {
                    obj.DefectCountFront_Join = remoteDefsFront.Count(x => x.Type == 0);
                    obj.DefectCountFront_Tag = remoteDefsFront.Count(x => x.Type == 1);
                    obj.DefectCountFront_LeakMetal = remoteDefsFront.Count(x => x.Type == 2);
                }

                if (remoteDefsBack != null)
                {
                    obj.DefectCountBack_Join = remoteDefsBack.Count(x => x.Type == 0);
                    obj.DefectCountBack_Tag = remoteDefsBack.Count(x => x.Type == 1);
                    obj.DefectCountBack_LeakMetal = remoteDefsBack.Count(x => x.Type == 2);
                }

            }

            return obj;
        }
        DataEA getEA(int id)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    return _get_ea(id);
                }
                catch (Exception ex)
                {
                    Log.AppLog.Debug("[已处理]", ex);
                    Thread.Sleep(10);
                }
            }
            return null;
        }
        double posEAStart = -1;
        void appendTab(DataTab data)
        {
            int ea = 0;
            int tab = 0;
            if (Tabs.Count != 0)
            {
                var last = Tabs.Last();
                ea = last.EA;
                tab = last.TAB;
            }

            if (data.IsNewEA)
            {
                ea++;
                tab = 1;
            }
            else
            {
                tab++;
            }

            int i = Tabs.Count;
            data.ID = i + 1;
            data.EA = ea;
            data.TAB = tab;

            data.ValDist = (i == 0) ? 0 : (data.TabY1 - Tabs[i - 1].TabY1) * Fy;
            data.ValDistDiff = (i < 2) ? 0 : data.ValDist - Tabs[i - 1].ValDist;

            TabsCache.Remove(data);
            Tabs.Add(data);

            if (data.IsNewEA)
            {
                appendEA(data);
            }

        }
        void appendEA(DataTab data)
        {

            var objEA = getEA(data.EA - 1);
            if (objEA != null)
            {

                //
                ShowEACount++;
                if (objEA.IsTabWidthFailCountFail)
                {
                    ShowEAWidthNGCount++;
                }
                else if (objEA.IsDefectCountFail)
                {
                    ShowEADefectNGCount++;
                }

                //
                EAs.Add(objEA);
            }
        }

        public List<DataEA_SyncFrom4K> _IN_8K_FROM_4K = new List<DataEA_SyncFrom4K>();
        public int AllocAndGetDefectCount(double start, double end, int ea)
        {
            int count = 0;
            for (int i = 0; i < Defects.Count; i++)
            {
                if (Defects[i].InRange(start, end))
                {
                    Defects[i].EA = ea;
                    count++;

                    //Fix:保存图片分EA
                    string ngPartPath = Defects[i].NGPartPath;
                    string ngBigPath = Defects[i].NGBigPath;
                    Log.RecordAsThread(() =>
                    {

                        //
                        Action<string> moveFile = path =>
                        {
                            Log.Record(() =>
                            {
                                if (path == "")
                                    return;

                                string oldFile = path + ".png";
                                string newFile = path + $"_EA{ea}.png";

                                if (System.IO.File.Exists(oldFile))
                                    System.IO.File.Move(oldFile, newFile);
                            });
                        };

                        //
                        moveFile(ngPartPath);
                        moveFile(ngBigPath);

                    });
                }
            }

            var extEA = _IN_8K_FROM_4K.Find(x => x.EA == ea);
            if (extEA == null)
            {
                extEA = new DataEA_SyncFrom4K()
                {
                    Start = start,
                    End = end,
                    EA = ea,
                };
                _IN_8K_FROM_4K.Add(extEA);
            }
            else
            {
                extEA.Start = Math.Min(extEA.Start, start);
                extEA.End = Math.Max(extEA.End, end);
            }

            return count;
        }

    }
}
