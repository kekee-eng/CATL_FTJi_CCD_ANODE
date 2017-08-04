
namespace Common {
#pragma warning disable 0649

    public class DataGrab {

        //取相相机
        public string Camera;

        //帧数据
        public int Frame;
        public int Encoder;
        public string Timestamp;

        //图像
        public byte[] Image;

        #region 数据库接口

        object[] ToDB() {
            return new object[] { Camera, Frame, Encoder, Timestamp, Image };
        }
        static DataGrab FromDB(object[] objs) {
            System.Func<object, object, object> getDef = (obj, def) => obj is System.DBNull ? def : obj;

            return new DataGrab() {
                Camera = (string)getDef(objs[1], ""),
                Frame = (int)getDef(objs[2], 0),
                Encoder = (int)getDef(objs[3], 0),
                Timestamp = (string)getDef(objs[4], ""),
                Image = (byte[])getDef(objs[5], null),
            };
        }

        static readonly string C_CREATE = @"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER     PRIMARY KEY     AUTOINCREMENT,
Camera          TEXT,
Frame           INTEGER,
Encoder         INTEGER,
Timestamp       TEXT,
Image           BLOB
)";
        static readonly string C_INSERT = @"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ";
        static readonly string C_SELECT = @"SELECT * FROM {0} WHERE Frame=""{1}""";

        public class DBTableGrab {
            public DBTableGrab(TemplateDB parent, string tableName) {

                //
                db = parent;
                name = tableName;

                //
                db.Write(string.Format(C_CREATE, name));
            }

            TemplateDB db;
            string name;

            public int Count {
                get { return db.Count(name); }
            }
            public bool CheckFrameExist(int frame) {
                return db.Count(name + " WHERE Frame=" + frame) != 0;
            }
            public bool Save(DataGrab data) {
                if (CheckFrameExist(data.Frame))
                    return false;

                db.Write(string.Format(C_INSERT, name), data.ToDB());
                return true;
            }
            public DataGrab Get(int frame) {
                var ret = db.Read(string.Format(C_SELECT, name, frame));
                if (ret.Count == 0)
                    return null;
                return FromDB(ret[0]);
            }
            
        }

        #endregion

    }
}
