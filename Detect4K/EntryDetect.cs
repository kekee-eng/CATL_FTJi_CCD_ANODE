using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Detect4K {

    class EntryDetect {

        public EntryDetect(TemplateDB parent, string tableName, CfgParamShare pshare, CfgParamSelf pself, EntryGrab grab) {

            //
            this.db = parent;
            this.tname = tableName;
            this.param = pshare;
            this.param_self = pself;
            this.grab = grab;

            //
            db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER   PRIMARY KEY, 
Tabs            BLOB,
Defects         BLOB,
Labels          BLOB,
CfgParamShare   BLOB,
CfgParamSelf    BLOB
)", this.tname));

            //
            if (db.Count(tname) > 0) {
                Load();
            }
        }

        public void Save() {

            if (!needSave)
                return;


            needSave = false;
            db.Write(string.Format(@"REPLACE INTO {0} ( ID, Tabs, Defects, Labels, CfgParamShare, CfgParamSelf ) VALUES (?,?,?,?,?,?) ", tname),
                0,
                UtilSerialization.obj2bytes(Tabs),
                UtilSerialization.obj2bytes(Defects),
                UtilSerialization.obj2bytes(Labels),
                UtilSerialization.obj2bytes(param),
                UtilSerialization.obj2bytes(param_self)
                );

        }
        public void Load(bool useParam = false) {

            var ret = db.Read(string.Format("SELECT * FROM {0} WHERE ID=0", tname));
            if (ret.Count == 0)
                return;

            Tabs = UtilSerialization.bytes2obj((byte[])ret[0][1]) as List<DataTab>;
            Defects = UtilSerialization.bytes2obj((byte[])ret[0][2]) as List<DataDefect>;
            Labels = UtilSerialization.bytes2obj((byte[])ret[0][3]) as List<DataLabel>;

            if (useParam) {
                (UtilSerialization.bytes2obj((byte[])ret[0][4]) as CfgParamShare).CopyTo(param);
                (UtilSerialization.bytes2obj((byte[])ret[0][5]) as CfgParamSelf).CopyTo(param_self);
            }
        }

        bool needSave = false;

        EntryGrab grab;
        TemplateDB db;
        string tname;

        public List<DataTab> Tabs = new List<DataTab>();
        public List<DataDefect> Defects = new List<DataDefect>();
        public List<DataLabel> Labels = new List<DataLabel>();

        public int EACount {
            get {
                int ret = 0;
                Static.SafeRun(() => ret = Tabs.Select(x => x.EA).Distinct().Count());
                return ret;
            }
        }
        public List<DataEA> EAs {
            get {
                List<DataEA> objs = new List<DataEA>();

                Static.SafeRun(() => {
                    var ids = Tabs.TakeWhile(x => x.TAB != 0).Select(x => x.EA).Distinct().OrderBy(x => x);
                    foreach (var id in ids) {

                        DataEA obj = new DataEA();
                        obj.EA = id;
                        obj.TabCount = Tabs.Count(x => x.EA == id);
                        obj.TabWidthFailCount = Tabs.Count(x => x.EA == id && x.IsWidthFail);
                        obj.TabHeightFailCount = Tabs.Count(x => x.EA == id && x.IsHeightFail);
                        obj.TabDistFailCount = Tabs.Count(x => x.EA == id && x.IsDistFail);
                        obj.IsTabCountFail = obj.TabCount != param.TabCount;
                        obj.IsTabWidthFailCountFail = obj.TabWidthFailCount > param.TabWidthCount;
                        obj.IsTabHeightFailCountFail = obj.TabHeightFailCount > param.TabHeightCount;
                        obj.IsTabDistFailCountFail = obj.TabDistFailCount > param.TabDistCount;

                        var firstER = Tabs.Find(x => x.EA == id && x.TAB == 1);
                        if (firstER == null)
                            throw new Exception("DetectResult: GetEA");

                        obj.EAX = firstER.MarkX;
                        obj.EAY = firstER.MarkY;

                        objs.Add(obj);
                    }
                });

                return objs;
            }
        }

        public CfgParamShare param;
        public CfgParamSelf param_self;

        public double Fx { get { return param_self.ScaleX * grab.Width; } }
        public double Fy { get { return param_self.ScaleY * grab.Height; } }

        public void Discard() {
            Tabs.Clear();
            Defects.Clear();
            Labels.Clear();
        }

        int defectCount = 0;
        bool statuPrev = false;

        public bool TryDetect(int frame) {

            bool statuCurr = tryDetect(frame);
            if(!statuCurr && statuPrev ) {
                adjustER();
            }
            statuPrev = statuCurr;
            return statuCurr;
        }
        bool tryDetect(int frame) {

            //
            int w = grab.Width;
            int h = grab.Height;

            //极耳检测、并判断是否可能有瑕疵
            var aimage = grab.GetImage(frame);
            bool hasTab, hasDefect;
            double[] ax, ay1, ay2;
            if (aimage == null || !ImageProcess.DetectTab(aimage, out hasDefect, out hasTab, out ax, out ay1, out ay2))
                return false;

            //若有瑕疵，先缓存图片，直到瑕疵结束或图像过大
            if (hasDefect) defectCount++;
            if (defectCount >= 10 || (!hasDefect && defectCount > 0)) {

                //拼成大图进行瑕疵检测
                int efx1 = frame - defectCount;
                int efx2 = frame - 1;
                defectCount = 0;
                
                Task.Run(() => {
                    var eimage = grab.GetImage(efx1, efx2);
                    int[] etype;
                    double[] ex, ey, ew, eh;
                    if (eimage != null && ImageProcess.DetectDefect(eimage, out etype, out ex, out ey, out ew, out eh)) {

                        int ecc = new int[] { etype.Length, ex.Length, ey.Length, ew.Length, eh.Length }.Min();
                        for (int i = 0; i < ecc; i++) {
                            DataDefect defect = new DataDefect() {
                                Type = etype[i],
                                X = ex[i] / w,
                                Y = efx1 + ey[i] / h,
                                W = ew[i] / w,
                                H = eh[i] / h,
                            };

                            defect.Width = defect.W * Fx;
                            defect.Height = defect.H * Fy;

                            Defects.Add(defect);
                        }
                    }
                });

            }

            //是否有极耳
            if (!hasTab)
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
                if (Math.Abs(data.TabY1 - nearTab.TabY2) * Fy < param.TabMergeDistance) {
                    
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

            //宽度检测
            double[] bx1, bx2;
            double bfy1 = data.TabY1 + param.TabWidthStart / Fy;
            double bfy2 = data.TabY1 + param.TabWidthEnd / Fy;

            var bimage = grab.GetImage(bfy1, bfy2);
            if (bimage != null && ImageProcess.DetectWidth(bimage, out bx1, out bx2)) {
                data.WidthY1 = bfy1;
                data.WidthY2 = bfy2;
                data.WidthX1 = bx1[0] / w;
                data.WidthX2 = bx2[0] / w;
            }

            //EA头部Mark检测
            double[] cx, cy;
            double cfy1 = data.TabY1 + param.EAStart / Fy;
            double cfy2 = data.TabY1 + param.EAEnd / Fy;

            //
            data.MarkY1 = cfy1;
            data.MarkY2 = cfy2;

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

            //
            Tabs.Add(data);
            return true;
        }

        void adjustDefect() {

            //去除与Mark点重合的瑕疵
            double ck = 0.5 / Fx;
            var marks = Tabs.TakeWhile(x => x.IsNewEA).ToArray();
            foreach (var mark in marks) {
                Defects.RemoveAll(m =>
                    (Math.Abs(m.X - mark.MarkX) < ck && Math.Abs(m.Y - mark.MarkY) < ck) ||
                    (mark.HasTwoMark && Math.Abs(m.X - mark.MarkX_P) < ck && Math.Abs(m.Y - mark.MarkY_P) < ck)
                );
            }
        }
        void adjustER() {

            //排序
            Tabs.Sort((a, b) => (int)((a.TabY1 - b.TabY1) * 1000));

            //重新生成
            int ea = 0;
            int er = 0;
            for (int i = 0; i < Tabs.Count; i++) {

                if (Tabs[i].IsNewEA) {
                    ea++;
                    er = 1;
                }
                else {
                    er++;
                }

                //EA值
                Tabs[i].ID = i + 1;
                Tabs[i].EA = ea;
                Tabs[i].TAB = er;

                //测量值
                Tabs[i].ValWidth = (Tabs[i].WidthX2 - Tabs[i].WidthX1) * Fx;
                Tabs[i].ValHeight = (Tabs[i].TabY2 - Tabs[i].TabY1) * Fy;
                Tabs[i].ValDist = (i == 0) ? 0 : (Tabs[i].TabY1 - Tabs[i - 1].TabY1) * Fy;
                Tabs[i].ValDistDiff = (i < 2) ? 0 : Tabs[i].ValDist - Tabs[i - 1].ValDist;
                Tabs[i].IsWidthFail = Tabs[i].ValWidth < param.TabWidthMin || Tabs[i].ValWidth > param.TabWidthMax;
                Tabs[i].IsHeightFail = Tabs[i].ValHeight < param.TabHeightMin || Tabs[i].ValHeight > param.TabHeightMax;
                Tabs[i].IsDistFail = Tabs[i].ValDist < param.TabDistMin || Tabs[i].ValDist > param.TabDistMax;
                Tabs[i].IsDistDiffFail = Tabs[i].ValDistDiff < param.TabDistDiffMin || Tabs[i].ValDistDiff > param.TabDistDiffMax;

            }

            //
            needSave = true;
        }

    }
}
