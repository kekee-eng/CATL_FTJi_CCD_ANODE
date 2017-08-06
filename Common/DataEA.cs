
using System.Collections.Generic;

namespace Common {
#pragma warning disable 0649

    [System.Serializable]
    class DataEA {

        public double EAX; //EA起始处
        public double EAY; //EA起始处

        public int EA;
        public int ERCount;
        public int ERWidthFailCount;
        public int ERGapFailCount;
        public int ERSizeFailCount;

        public bool IsERCountFail;
        public bool IsERWidthFailCountFail;
        public bool IsERGapFailCountFail;
        public bool IsERSizeFailCountFail;

        public bool IsFail { get { return IsERCountFail || IsERWidthFailCountFail || IsERGapFailCountFail || IsERSizeFailCountFail; } }
        
        public class EntryEA {

            public EntryEA(TemplateDB parent, string tableName) {

                //
                db = parent;
                this.tname = tableName;

                //
                db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER     PRIMARY KEY     AUTOINCREMENT,
EA                  INTEGER,
ERCount             INTEGER,
ERWidthFailCount    INTEGER,
ERGapFailCount      INTEGER,
ERSizeFailCount     INTEGER,
IsERCountFail       BOOLEAN,
IsERWidthFailCountFail  BOOLEAN,
IsERGapFailCountFail    BOOLEAN,
IsERSizeFailCountFail   BOOLEAN
)", this.tname));

                //
                Count = QueryCount;
                
            }

            TemplateDB db;
            string tname;

            public int QueryCount { get { return db.Count(tname); } }
            public int Count;

        }
    }
}
