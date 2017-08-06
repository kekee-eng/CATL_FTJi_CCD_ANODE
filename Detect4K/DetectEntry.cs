using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Detect4K {

    class DetectEntry {

        public DetectEntry(TemplateDB parent, string tableName, CfgParamShare pshare, CfgParamSelf pself) {

            //
            this.db = parent;
            this.tname = tableName;
            this.param_share = pshare;
            this.param_self = pself;

            //
            db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER，
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

            db.Write(string.Format(@"REPLACE INTO {0} ( ID, Tabs, Defects, Labels, CfgParamShare, CfgParamSelf ) VALUES (?,?,?,?,?,?) ", tname),
                0,
                UtilSerialization.obj2bytes(Tabs),
                UtilSerialization.obj2bytes(Defects),
                UtilSerialization.obj2bytes(Labels),
                UtilSerialization.obj2bytes(param_share),
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
                (UtilSerialization.bytes2obj((byte[])ret[0][4]) as CfgParamShare).CopyTo(param_share);
                (UtilSerialization.bytes2obj((byte[])ret[0][5]) as CfgParamSelf).CopyTo(param_self);
            }
        }

        TemplateDB db;
        string tname;

        public List<DataTab> Tabs = new List<DataTab>();
        public List<DataDefect> Defects = new List<DataDefect>();
        public List<DataLabel> Labels = new List<DataLabel>();

        public CfgParamShare param_share;
        public CfgParamSelf param_self;
        

    }
}
