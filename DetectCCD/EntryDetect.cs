﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DetectCCD {

    public class EntryDetect : IDisposable {

        public EntryDetect(TemplateDB parent, string tableName, EntryGrab grab, bool isInner) {

            //
            this.db = parent;
            this.tname = tableName;
            this.grab = grab;
            this.isinner = isInner;

        }

        public void Dispose() {
            EAs.Clear();
            Tabs.Clear();
            Defects.Clear();
            LabelsCache.Clear();
            Labels.Clear();
        }

        public void CreateTable() {

            //
            db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER   PRIMARY KEY, 
Tabs            BLOB,
Defects         BLOB,
Labels          BLOB,
CfgApp          BLOB,
CfgParam        BLOB
)", this.tname));

        }
        public void Save() {

            if (!needSave)
                return;

            needSave = false;
            db.Write(string.Format(@"REPLACE INTO {0} ( ID, Tabs, Defects, Labels, CfgApp, CfgParam ) VALUES (?,?,?,?,?,?,?) ", tname),
                0,
                UtilSerialization.obj2bytes(Tabs),
                UtilSerialization.obj2bytes(Defects),
                UtilSerialization.obj2bytes(Labels),
                UtilSerialization.obj2bytes(Static.App),
                UtilSerialization.obj2bytes(Static.Param)
                );

        }
        public void Reload(bool useParam = false) {
            Dispose();
            var ret = db.Read(string.Format("SELECT * FROM {0} WHERE ID=\"0\"", tname));
            if (ret.Count == 0)
                return;

            Tabs = UtilSerialization.bytes2obj((byte[])ret[0][1]) as List<DataTab>;
            Defects = UtilSerialization.bytes2obj((byte[])ret[0][2]) as List<DataDefect>;
            Labels = UtilSerialization.bytes2obj((byte[])ret[0][3]) as List<DataLabel>;

            if (useParam) {
                (UtilSerialization.bytes2obj((byte[])ret[0][4]) as CfgParam).CopyTo(Static.App);
                (UtilSerialization.bytes2obj((byte[])ret[0][5]) as CfgParam).CopyTo(Static.Param);
            }
        }

        bool needSave = false;

        EntryGrab grab;
        TemplateDB db;
        string tname;
        bool isinner;

        public double Fx { get { return grab.Fx; } }
        public double Fy { get { return grab.Fy; } }

        public int m_frame = -1;

        public List<DataEA> EAs = new List<DataEA>();
        public List<DataTab> Tabs = new List<DataTab>();
        public List<DataDefect> Defects = new List<DataDefect>();
        public List<DataLabel> Labels = new List<DataLabel>();

        public List<DataLabel> LabelsCache = new List<DataLabel>();
        void addLabel(DataLabel lab) {
            Static.SafeRun(() => {

                var repeat1 = LabelsCache.Find(x => Math.Abs(x.Y - lab.Y) < 0.5);
                var repeat2 = Labels.Find(x => Math.Abs(x.Y - lab.Y) < 0.5);
                if (repeat1 == null && repeat2 == null) {
                    LabelsCache.Add(lab);
                }
            });
        }
        void checkLabel(int frame) {
            Static.SafeRun(() => {
                if (LabelsCache.Count == 0)
                    return;

                var minLab = LabelsCache.OrderBy(x => x.Y).First();
                if (minLab != null) {
                    if (minLab.Y < frame) {
                        LabelsCache.Remove(minLab);
                        minLab.Encoder = grab.GetEncoder(minLab.Y);

                        if (null == Labels.Find(x => Math.Abs(x.Y - minLab.Y) < 0.5)) {
                            Labels.Add(minLab);
                            OnNewLabel?.Invoke(minLab.Encoder);
                        }
                    }
                } 
            });
            
        }

        public int ShowEACount =0;
        public int ShowEAWidthNGCount = 0;
        public int ShowEADefectNGCount = 0;
        
        public event Action<int> OnNewLabel;
        public event Action<DataTab, DataTab> OnSyncTab;
        
        int defectFrameCount = 0;

        public int TimeTotal =0;
        public bool TryDetect(DataGrab obj) {

            //
            checkLabel(obj.Frame);

            //转标签
            if (Static.App.Is4K && Static.App.EnableLabelDefect) {
                var remoteDefs = RemoteDefect.In4KCall8K_GetDefectList(true, isinner);
                if (remoteDefs != null) {
                    foreach (var rl in remoteDefs) {
                        if (rl.IsTransLabel()) {
                            addLabel(new DataLabel() {
                                Y = rl.Y + Static.Param.LabelY_Defect / Fy,
                                Comment = string.Format("转标[正面][{0}]", rl.GetTypeCaption())
                            });
                        }
                    }
                }
            }

            if (Static.App.Is4K && Static.App.EnableLabelDefect) {
                var remoteDefs = RemoteDefect.In4KCall8K_GetDefectList(false, isinner);
                if (remoteDefs != null) {
                    foreach (var rl in remoteDefs) {
                        if (rl.IsTransLabel()) {
                            addLabel(new DataLabel() {
                                Y = rl.Y + Static.Param.LabelY_Defect / Fy,
                                Comment = string.Format("转标[背面][{0}]", rl.GetTypeCaption())
                            });
                        }
                    }
                }
            }
            
            var ret = tryDetect(obj);
            return ret;
        }
        public void Sync(EntryDetect partner) {

            // diff = this - partner
            // this = partner + diff
            // partner = this - diff
            var diffFrame = Static.App.FixFrameOuterOrBackOffset;

            //需要同步的对象
            var myER = getFirstUnBind();
            if (myER == null)
                return;

            lock (Tabs) {
                lock (partner.Tabs) {
                    
                    //尝试找到对应项
                    var bindER = partner.findBind(myER.TabY1 - diffFrame);
                    if (bindER == null) {

                        //未找到：对方补测一个宽度
                        bindER = partner.fixER(myER.TabX, myER.TabY1 - diffFrame, myER.TabY2 - diffFrame);
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
                            myER.MarkY = bindER.MarkY + diffFrame;
                        }

                        if (myER.MarkX != 0 && bindER.MarkX == 0) {
                            bindER.MarkX = myER.MarkX;
                            bindER.MarkY = myER.MarkY - diffFrame;
                        }
                    }

                    //查看我方是否有漏测
                    do {
                        var missER = partner.Tabs.Find(x => !x.IsSync && x.TabY1 < bindER.TabY1);
                        if (missER == null)
                            break;

                        //补测宽度
                        var myMissER = fixER(missER.TabX, missER.TabY1 + diffFrame, missER.TabY2 + diffFrame);

                        //标记同步
                        missER.IsSync = true;

                        //同步EA头
                        if (missER.IsNewEA || myMissER.IsNewEA) {
                            missER.IsNewEA = true;
                            myMissER.IsNewEA = true;

                            if (missER.MarkX == 0 && myMissER.MarkX != 0) {
                                missER.MarkX = myMissER.MarkX;
                                missER.MarkY = myMissER.MarkY - diffFrame;
                            }

                            if (missER.MarkX != 0 && bindER.MarkX == 0) {
                                myMissER.MarkX = missER.MarkX;
                                myMissER.MarkY = missER.MarkY + diffFrame;
                            }
                        }

                    } while (true);

                    //重置序号
                    adjustER();
                    //adjustDefect();
                    partner.adjustER();
                    //partner.adjustDefect();
                    OnSyncTab?.Invoke(myER, bindER);
                }
            }
        }

        bool tryDetect(DataGrab obj) {

            //
            m_frame = obj.Frame;
            int frame = obj.Frame;
            obj.IsDetect = true;

            //
            int w = obj.Width;
            int h = obj.Height;

            //
            if (!Static.App.DetectEnable)
                return false;

            //极耳检测、并判断是否可能有瑕疵
            var aimage = grab.GetImage(frame);
            double[] ax, ay1, ay2;
            if (aimage == null || !ImageProcess.DetectTab(aimage, out obj.hasDefect, out obj.hasTab, out ax, out ay1, out ay2))
                return false;

            if (Static.App.DetectDefect) {

                //若有瑕疵，先缓存图片，直到瑕疵结束或图像过大
                if (obj.hasDefect) {
                    defectFrameCount++;
                }

                if (defectFrameCount >= 10 || (!obj.hasDefect && defectFrameCount > 0)) {

                    //拼成大图进行瑕疵检测
                    int efx1 = frame - 1 - defectFrameCount;
                    int efx2 = frame - 1;
                    defectFrameCount = 0;

                    Task.Run(() => {
                        var eimage = grab.GetImage(efx1, efx2);
                        int[] etype;
                        double[] ex, ey, ew, eh, earea;
                        if (eimage != null && ImageProcess.DetectDefect(eimage, out etype, out ex, out ey, out ew, out eh, out earea)) {

                            int ecc = new int[] { etype.Length, ex.Length, ey.Length, ew.Length, eh.Length }.Min();
                            for (int i = 0; i < ecc; i++) {
                                DataDefect defect = new DataDefect() {
                                    EA= -1,
                                    Type = etype[i],
                                    X = ex[i] / w,
                                    Y = efx1 + ey[i] / h,
                                    W = ew[i] / w,
                                    H = eh[i] / h
                                };

                                defect.Width = defect.W * Fx;
                                defect.Height = defect.H * Fy;
                                defect.Area = earea[i] * Fx * Fy / w / h;

                                Defects.Add(defect);
                            }
                        }
                    });

                }
            }

            //是否有极耳
            if (!obj.hasTab)
                return false;

            if (!Static.App.DetectTab)
                return false;

            //极耳数据整理
            var data = new DataTab();
            data.TabX = ax[0] / w;
            data.TabY1 = data.TabY1_P = frame + ay1[0] / h;
            data.TabY2 = data.TabY2_P = frame + ay2[0] / h;
            if (ax.Length == 2 && ay1.Length == 2 && ay2.Length == 2) {
                data.HasTwoTab = true;
                data.TabX_P = ax[1] / w;
                data.TabY1_P = frame + ay1[1] / h;
                data.TabY2_P = frame + ay2[1] / h;
            }

            //是否新极耳
            bool isNewData = true;
            if (Tabs.Count > 0) {
                var nearTab = Tabs.OrderBy(x => Math.Abs(x.TabY1 - data.TabY1)).First();
                if (Math.Abs(data.TabY1 - nearTab.TabY2) * Fy < Static.Param.TabMergeDistance) {

                    //更新极耳大小
                    double dist = 10 / Fx; //10mm
                    if (data.HasTwoTab || nearTab.HasTwoTab || Math.Abs(data.TabX - nearTab.TabX) >= dist) {

                        //
                        double leftx, rightx;
                        if (data.HasTwoTab) {
                            leftx = data.TabX;
                            rightx = data.TabX_P;
                        }
                        else if (nearTab.HasTwoTab) {
                            leftx = nearTab.TabX;
                            rightx = nearTab.TabX_P;
                        }
                        else {
                            leftx = Math.Min(data.TabX, nearTab.TabX);
                            rightx = Math.Max(data.TabX, nearTab.TabX);
                        }

                        //
                        Action<double, DataTab, List<double>> addPoint = (x, dt, list) => {
                            if (Math.Abs(dt.TabX - x) < dist) {
                                list.Add(dt.TabY1);
                                list.Add(dt.TabY2);
                            }

                            if (dt.HasTwoTab && Math.Abs(dt.TabX_P - x) < dist) {
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
                    else {
                        nearTab.TabY1 = Math.Min(nearTab.TabY1, data.TabY1);
                        nearTab.TabY2 = Math.Max(nearTab.TabY2, data.TabY2);
                    }

                    //
                    isNewData = false;
                }
            }

            if (!isNewData)
                return false;

            if (Static.App.DetectWidth) {

                //宽度检测
                double[] bx1, bx2;
                double bfy1 = data.TabY1 + Static.Param.TabWidthStart / Fy;
                double bfy2 = data.TabY1 + Static.Param.TabWidthEnd / Fy;

                var bimage = grab.GetImage(bfy1, bfy2);
                if (bimage != null && ImageProcess.DetectWidth(bimage, out bx1, out bx2)) {
                    data.WidthY1 = bfy1;
                    data.WidthY2 = bfy2;
                    data.WidthX1 = bx1[0] / w;
                    data.WidthX2 = bx2[0] / w;
                }
            }

            if (Static.App.DetectMark) {

                //EA头部Mark检测
                double[] cx, cy;
                double cfy1 = data.TabY1 + Static.Param.EAStart / Fy;
                double cfy2 = data.TabY1 + Static.Param.EAEnd / Fy;

                //
                data.MarkImageStart = cfy1;
                data.MarkImageEnd = cfy2;

                var cimage = grab.GetImage(cfy1, cfy2);
                if (ImageProcess.DetectMark(cimage, out cx, out cy)) {

                    //将最后一个极耳放到下个EA中
                    data.IsNewEA = true;
                    data.MarkX = data.MarkX_P = cx[0] / w;
                    data.MarkY = data.MarkY_P = cfy1 + cy[0] / h;

                    if (cx.Length == 2 && cy.Length == 2) {
                        data.HasTwoMark = true;
                        data.MarkX_P = cx[1] / w;
                        data.MarkY_P = cfy1 + cy[1] / h;
                    }
                    
                }
            }

            lock (Tabs) {
                Tabs.Add(data);
            }
            return true;

        }
        DataTab findBind(double frame) {
            return Tabs.Find(x => Math.Abs(x.TabY1 - frame) * Fy < Static.Param.TabMergeDistance);
        }
        DataTab fixER(double x, double y1, double y2) {

            int w = grab.Width;
            int h = grab.Height;

            DataTab data = new DataTab();
            data.TabX = x;
            data.TabY1 = y1;
            data.TabY2 = y1;
            data.HasTwoMark = false;

            if (Static.App.DetectWidth) {
                //宽度检测
                double[] bx1, bx2;
                double bfy1 = data.TabY1 + Static.Param.TabWidthStart / Fy;
                double bfy2 = data.TabY1 + Static.Param.TabWidthEnd / Fy;

                var bimage = grab.GetImage(bfy1, bfy2);
                if (bimage != null && ImageProcess.DetectWidth(bimage, out bx1, out bx2)) {
                    data.WidthY1 = bfy1;
                    data.WidthY2 = bfy2;
                    data.WidthX1 = bx1[0] / w;
                    data.WidthX2 = bx2[0] / w;
                }
            }

            if (Static.App.DetectMark) {

                //EA头部Mark检测
                double[] cx, cy;
                double cfy1 = data.TabY1 + Static.Param.EAStart / Fy;
                double cfy2 = data.TabY1 + Static.Param.EAEnd / Fy;

                //
                data.MarkImageStart = cfy1;
                data.MarkImageEnd = cfy2;

                var cimage = grab.GetImage(cfy1, cfy2);
                if (ImageProcess.DetectMark(cimage, out cx, out cy)) {

                    //将最后一个极耳放到下个EA中
                    data.IsNewEA = true;
                    data.MarkX = data.MarkX_P = cx[0] / w;
                    data.MarkY = data.MarkY_P = cfy1 + cy[0] / h;

                    if (cx.Length == 2 && cy.Length == 2) {
                        data.HasTwoMark = true;
                        data.MarkX_P = cx[1] / w;
                        data.MarkY_P = cfy1 + cy[1] / h;
                    }

                }
            }
            
            data.IsFix = true;
            data.IsSync = true;

            Tabs.Add(data);
            return data;
        }
        DataEA getEA(int id) {

            var curEaTab = Tabs.Find(x => x.EA == id && x.TAB == 1);
            if (curEaTab == null)
                return null;

            DataEA obj = new DataEA();
            obj.EA = id;
            obj.TabCount = Tabs.Count(x => x.EA == id);
            obj.TabWidthFailCount = Tabs.Count(x => x.EA == id && x.IsWidthFail);

            if (curEaTab.IsNewEA) {
                obj.EAX = curEaTab.MarkX;
                obj.EAY = curEaTab.MarkY;
            }
            else {
                obj.EAX = curEaTab.TabX;
                obj.EAY = curEaTab.TabY1;
            }

            double start = curEaTab.MarkY;
            double end = 0;
            var nextEaTab = Tabs.Find(x => x.EA == id + 1 && x.TAB == 1);
            if (nextEaTab != null) {
                end = nextEaTab.MarkY;
            }
            else {
                end = Tabs.FindAll(x => x.EA == id).Select(x => x.TabY1).Max();
            }

            AllocAndGetDefectCount(start, end, id);

            if (Static.App.Is4K) {
                var setFront = RemoteDefect.In4KCall8K_GetDefectCount(true, isinner, start, end, id);
                var setBack = RemoteDefect.In4KCall8K_GetDefectCount(false, isinner, start, end, id);

                var remoteDefsFront = RemoteDefect.In4KCall8K_GetDefectList(true, isinner);
                var remoteDefsBack = RemoteDefect.In4KCall8K_GetDefectList(false, isinner);

                if (remoteDefsFront != null) {
                    obj.DefectCountFront_Join = remoteDefsFront.Count(x => x.EA == id && x.Type == 0);
                    obj.DefectCountFront_Tag = remoteDefsFront.Count(x => x.EA == id && x.Type == 1);
                    obj.DefectCountFront_LeakMetal = remoteDefsFront.Count(x => x.EA == id && x.Type == 40);
                }

                if (remoteDefsBack != null) {
                    obj.DefectCountBack_Join = remoteDefsBack.Count(x => x.EA == id && x.Type == 0);
                    obj.DefectCountBack_Tag = remoteDefsBack.Count(x => x.EA == id && x.Type == 1);
                    obj.DefectCountBack_LeakMetal = remoteDefsBack.Count(x => x.EA == id && x.Type == 40);
                }

            }

            return obj;
        }
        void adjustDefect() {

            //去除与Mark点重合的瑕疵
            double ck = 1;
            for (int i = 0; i < Tabs.Count; i++) {
                if (Tabs[i].IsNewEA) {
                    var mark = Tabs[i];
                    Defects.RemoveAll(m =>
                        (Math.Abs(m.X - mark.MarkX) * Fx < ck && Math.Abs(m.Y - mark.MarkY) * Fy < ck) ||
                        (mark.HasTwoMark && Math.Abs(m.X - mark.MarkX_P) * Fx < ck && Math.Abs(m.Y - mark.MarkY_P) * Fy < ck)
                    );
                }
            }

        }
        void adjustER() {

            //排序
            //Tabs.Sort((a, b) => (int)((a.TabY1 - b.TabY1) * 1000));
            for (int i = 0; i < Tabs.Count - 1; i++) {
                for (int j = i + 1; j < Tabs.Count; j++) {
                    var obj1 = Tabs[i];
                    var obj2 = Tabs[j];
                    if (obj1.TabY1 > obj2.TabY1) {
                        Tabs.RemoveAt(j);
                        Tabs.RemoveAt(i);
                        Tabs.Insert(i, obj2);
                        Tabs.Insert(j, obj1);
                    }
                }
            }
            
            //重新生成
            int ea = 0;
            int er = 0;
            double posEAStart = -1;

            int eaCount = 0;
            int eaWidthNGCount = 0;
            int eaDefectNGCount = 0;
            for (int i = 0; i < Tabs.Count; i++) {

                if (Tabs[i].IsNewEA) {
                    ea++;
                    er = 1;

                    var objEA = getEA(ea - 1);
                    if (objEA != null) {

                        //
                        eaCount++;
                        if (objEA.IsTabWidthFailCountFail)
                            eaWidthNGCount++;

                        if (objEA.IsDefectCountFail)
                            eaDefectNGCount++;

                        //
                        if (!Tabs[i].IsNewEACallBack) {

                            //
                            Tabs[i].IsNewEACallBack = true;
                            EAs.Add(objEA);

                            //添加标签
                            if (Static.App.Is4K && Static.App.EnableLabelEA) {
                                if (objEA.IsFail || Static.App.EnableLabelEA_EveryOne) {
                                    var objLab = new DataLabel() {
                                        EA = ea - 1,
                                        Y = Tabs[i].MarkY + Static.Param.LabelY_EA / Fy,
                                        Comment = (Static.App.EnableLabelEA_EveryOne ? "[测试]" : "") + "EA末端贴标: " + objEA.GetFailReason()
                                    };
                                    objLab.Encoder = grab.GetEncoder(objLab.Y);
                                    addLabel(objLab);
                                }
                            }

                            //
                            posEAStart = Tabs[i].MarkY;
                        }
                    }
                }
                else {
                    er++;
                }

                //EA值
                Tabs[i].IsSort = true;
                Tabs[i].ID = i + 1;
                Tabs[i].EA = ea;
                Tabs[i].TAB = er;

                //测量值
                Tabs[i].ValWidth = (Tabs[i].WidthX2 - Tabs[i].WidthX1) * Fx;
                Tabs[i].ValHeight = (Tabs[i].TabY2 - Tabs[i].TabY1) * Fy;
                Tabs[i].ValDist = (i == 0) ? 0 : (Tabs[i].TabY1 - Tabs[i - 1].TabY1) * Fy;
                Tabs[i].ValDistDiff = (i < 2) ? 0 : Tabs[i].ValDist - Tabs[i - 1].ValDist;

                //强制打标
                if (Static.App.Is4K && Static.App.EnableLabelEA && Static.App.EnableLabelEA_Force) {
                    if (posEAStart >= 0) {
                        var y0 = posEAStart + Static.Param.LabelY_EA_Force / Fy;
                        if (Tabs[i].TabY1 > y0) {
                            posEAStart = -1;

                            var objLab = new DataLabel() {
                                EA = ea,
                                Y = y0,
                                Comment = "EA末端强制贴标"
                            };

                            objLab.Encoder = grab.GetEncoder(objLab.Y);
                            addLabel(objLab);
                        }
                    }
                }

            }
            ShowEACount = eaCount;
            ShowEADefectNGCount = eaDefectNGCount;
            ShowEAWidthNGCount = eaWidthNGCount;

            //
            needSave = true;
        }
        DataTab getFirstUnBind() {

            double unBindPos = double.MaxValue;
            DataTab unBindObj = null;
            for (int i = Tabs.Count - 1; i >= 0; i--) {
                if (!Tabs[i].IsSync && Tabs[i].TabY1 < unBindPos) {
                    unBindPos = Tabs[i].TabY1;
                    unBindObj = Tabs[i];
                }
            }
            return unBindObj;
        }

        public List<DataEA_SyncFrom4K> _IN_8K_FROM_4K = new List<DataEA_SyncFrom4K>();
        public int AllocAndGetDefectCount(double start, double end, int ea) {
            int count = 0;
            for (int i = 0; i < Defects.Count; i++) {
                if (Defects[i].InRange(start, end)) {
                    Defects[i].EA = ea;
                    count++;
                }
            }

            var extEA = _IN_8K_FROM_4K.Find(x => x.EA == ea);
            if (extEA == null) {
                extEA = new DataEA_SyncFrom4K() {
                    Start = start,
                    End = end,
                    EA = ea,
                };
                _IN_8K_FROM_4K.Add(extEA);
            }
            else {
                extEA.Start = Math.Min(extEA.Start, start);
                extEA.End = Math.Max(extEA.End, end);
            }

            return count;
        }

    }
}
