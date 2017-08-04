
using HalconDotNet;
using System.Collections.Concurrent;
using System.Linq;

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
        public HImage Image;

        //生成时间戳
        public void GenTimeStamp(System.DateTime time) {
            Timestamp = time.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        #region 数据库接口

        object[] ToDB() {
            return new object[] { Camera, Frame, Encoder, Timestamp, UtilSerialization.obj2bytes(Image) };
        }
        static DataGrab FromDB(object[] objs) {
            System.Func<object, object, object> getDef = (obj, def) => obj is System.DBNull ? def : obj;

            return new DataGrab() {
                Camera = (string)getDef(objs[1], ""),
                Frame = (int)getDef(objs[2], 0),
                Encoder = (int)getDef(objs[3], 0),
                Timestamp = (string)getDef(objs[4], ""),
                Image = (HImage)UtilSerialization.bytes2obj((byte[])getDef(objs[5], null)),
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

            public int Count { get { return db.Count(name); } }
            public DataGrab this[int i] {
                get {
                    var ret = db.Read(string.Format(C_SELECT, name, i));
                    if (ret.Count == 0)
                        return null;
                    return FromDB(ret[0]);
                }
            }

            public bool CheckExist(int frame) {
                return db.Count(name + " WHERE Frame=" + frame) != 0;
            }
            public void Save(DataGrab data) {
                if (CheckExist(data.Frame))
                    return;
                db.Write(string.Format(C_INSERT, name), data.ToDB());
            }

        }

        #endregion

        #region 缓存接口

        public class CacheGrab {

            ConcurrentDictionary<int, DataGrab> cache;

            public int Count { get { return cache.Count; } }
            public DataGrab this [int i] {
                get {
                    return cache.Keys.Contains(i) ? cache[i] : null;
                }
                set {
                    cache[i] = value;
                }
            }

            public void RemoveAll() {

                foreach (var key in cache.Keys.ToArray()) {
                    DataGrab data;
                    if (cache.TryRemove(key, out data)) {
                        data.Image.Dispose();
                    }
                }
            }
            public void RemoveOld(int store) {

                int del = cache.Count - store;
                if (del > 0) {
                    foreach (var key in cache.Keys.Take(del).ToList()) {
                        DataGrab data;
                        if (cache.TryRemove(key, out data)) {
                            data.Image.Dispose();
                        }
                    }
                }
            }

        }

        #endregion

    }
}
