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
ID              INTEGER, 
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

        public CfgParamShare param;
        public CfgParamSelf param_self;

        public double Fx { get { return param_self.ScaleX * grab.Width; } }
        public double Fy { get { return param_self.ScaleX * grab.Width; } }

        public bool TryDetect(int frame) {

            //检测极耳
            var aimage = grab.GetImage(frame);
            double ax, ay1, ay2;
            if (aimage != null && ImageProcess.DetTab(aimage, out ax, out ay1, out ay2)) {

                int w = grab.Width;
                int h = grab.Height;

                //
                var data = new DataTab();
                data.TabX = ax / w;
                data.TabY1 = frame + ay1 / h;
                data.TabY2 = frame + ay2 / h;

                data.EAX = data.TabX;
                data.EAY = data.TabY1;

                //
                bool isNewData = true;
                if (Tabs.Count > 0) {
                    var lastData = Tabs.Last();
                    if (Math.Abs(data.TabY1 - lastData.TabY2) < param.TabMergeDistance) {

                        //更新极耳大小
                        lastData.TabY1 = Math.Min(lastData.TabY1, data.TabY1);
                        lastData.TabY2 = Math.Max(lastData.TabY2, data.TabY2);
                        lastData.ValHeight = (lastData.TabY2 - lastData.TabY1) * Fy;
                        lastData.IsSizeFail = lastData.ValHeight < param.TabHeightMin || lastData.ValHeight > param.TabHeightMax;

                        //
                        isNewData = false;
                    }
                }

                //
                if (isNewData) {

                    //检测宽度
                    double bx1, bx2;
                    double bfy1 = data.TabY1 + param.TabWidthStart;
                    double bfy2 = data.TabY1 + param.TabWidthEnd;

                    var bimage = grab.GetImage(bfy1, bfy2);
                    if (bimage != null && ImageProcess.DetWidth(bimage, out bx1, out bx2)) {
                        data.WidthY1 = bfy1;
                        data.WidthY2 = bfy2;
                        data.WidthX1 = bx1 / w;
                        data.WidthX2 = bx2 / w;
                    }

                    //检测是否EA头
                    double cx, cy;
                    double cfy1 = data.TabY1 + param.EAStart;
                    double cfy2 = data.TabY1 + param.EAEnd;
                    var cimage = grab.GetImage(cfy1, cfy2);
                    if (ImageProcess.DetEAMark(cimage, out cx, out cy)) {

                        //将最后一个极耳放到下个EA中
                        data.IsNewEA = true;
                        data.EAX = cx / w;
                        data.EAY = cfy1 + cy / h;

                    }

                    //
                    Tabs.Add(data);

                    //
                    return true;
                }
            }

            //
            return false;
        }
    }
}
