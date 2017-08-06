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

        public int EACount { get { return Tabs.Select(x => x.EA).Distinct().Count(); } }
        public List<DataEA> EAs {
            get {
                List<DataEA> objs = new List<DataEA>();

                var ids = Tabs.Select(x => x.EA).Distinct().OrderBy(x => x);

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

        int maybeCount = 0;
        public void TryDetectDefect(int frame) {

            //检测缺陷
            var dimage = grab.GetImage(frame);
            bool maybe = (dimage != null && ImageProcess.DetectDefectFast(dimage));

            if (maybe) maybeCount++;
            if (maybeCount >= 10 || (!maybe && maybeCount > 0)) {

                var eimage = grab.GetImage(frame - maybeCount, frame - 1);
                double[] ex, ey, ew, eh;
                if (eimage != null && ImageProcess.DetectDefectDeep(eimage, out ex, out ey, out ew, out eh)) {

                    int w = grab.Width;
                    int h = grab.Height;

                    int ecc = new int[] { ex.Length, ey.Length, ew.Length, eh.Length }.Min();
                    for (int i = 0; i < ecc; i++) {
                        DataDefect defect = new DataDefect();
                        defect.X = ex[i] / w;
                        defect.Y = frame - maybeCount + ey[i] / h;
                        defect.W = ew[i] / w;
                        defect.H = eh[i] / h;

                        defect.Width = defect.W * Fx;
                        defect.Height = defect.H * Fy;

                        Defects.Add(defect);
                    }
                }

                //清空缓存
                maybeCount = 0;
            }
        }
        public bool TryDetectTab(int frame) {

            //检测极耳
            var aimage = grab.GetImage(frame);
            double[] ax, ay1, ay2;
            if (aimage != null && ImageProcess.DetectTab(aimage, out ax, out ay1, out ay2)) {

                int w = grab.Width;
                int h = grab.Height;

                //
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

                data.MarkX = data.TabX;
                data.MarkY = data.TabY1;

                //
                bool isNewData = true;
                if (Tabs.Count > 0) {
                    var nearTab = Tabs.OrderBy(x => Math.Abs(x.TabY1 - data.TabY1)).First();
                    if (Math.Abs(data.TabY1 - nearTab.TabY2) * Fy < param.TabMergeDistance) {

                        //更新极耳大小
                        nearTab.TabY1 = Math.Min(nearTab.TabY1, data.TabY1);
                        nearTab.TabY2 = Math.Max(nearTab.TabY2, data.TabY2);
                        nearTab.ValHeight = (nearTab.TabY2 - nearTab.TabY1) * Fy;
                        nearTab.IsHeightFail = nearTab.ValHeight < param.TabHeightMin || nearTab.ValHeight > param.TabHeightMax;

                        //
                        isNewData = false;
                    }
                }

                //
                if (isNewData) {

                    //检测宽度
                    double bx1, bx2;
                    double bfy1 = data.TabY1 + param.TabWidthStart / Fy;
                    double bfy2 = data.TabY1 + param.TabWidthEnd / Fy;

                    var bimage = grab.GetImage(bfy1, bfy2);
                    if (bimage != null && ImageProcess.DetectWidth(bimage, out bx1, out bx2)) {
                        data.WidthY1 = bfy1;
                        data.WidthY2 = bfy2;
                        data.WidthX1 = bx1 / w;
                        data.WidthX2 = bx2 / w;
                    }

                    //检测是否EA头
                    double[] cx, cy;
                    double cfy1 = data.TabY1 + param.EAStart / Fy;
                    double cfy2 = data.TabY1 + param.EAEnd / Fy;
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
                    adjustER();

                    //
                    return true;
                }
            }

            //
            return false;
        }

        void adjustER() {

            //排序
            Tabs = Tabs.OrderBy(x => x.TabY1).ToList();

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
