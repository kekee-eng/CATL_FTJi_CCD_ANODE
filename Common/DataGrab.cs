
using HalconDotNet;
using System.Collections.Concurrent;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

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

        //标记
        public bool IsCreated = false;
        public bool IsCache = false;
        public bool IsStore = false;

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

        public class DBTableGrab {
            public DBTableGrab(TemplateDB parent, string tableName) {

                //
                db = parent;
                name = tableName;

                //
                db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER     PRIMARY KEY     AUTOINCREMENT,
Camera          TEXT,
Frame           INTEGER,
Encoder         INTEGER,
Timestamp       TEXT,
Image           BLOB
)", name));

            }

            TemplateDB db;
            string name;

            public int Min {
                get {
                    if (Count == 0)
                        return 0;
                    return (int)(long)db.Read(string.Format(@"SELECT MIN(Frame) FROM {0}", name))[0][0];
                }
            }
            public int Max {
                get {
                    if (Count == 0)
                        return 0;
                    return (int)(long)db.Read(string.Format(@"SELECT MAX(Frame) FROM {0}", name))[0][0];
                }
            }

            public int Count { get { return db.Count(name); } }
            public DataGrab this[int i] {
                get {
                    var ret = db.Read(string.Format(@"SELECT * FROM {0} WHERE Frame=""{1}""", name, i));
                    if (ret.Count == 0)
                        return null;
                    return FromDB(ret[0]);
                }
            }

            public bool CheckExist(int frame) {
                return db.Count(name + " WHERE Frame=" + frame) != 0;
            }
            public void Save(DataGrab data) {
                if (data == null)
                    return;

                if (data.IsStore)
                    return;

                if (CheckExist(data.Frame)) {
                    data.IsStore = true;
                    return;
                }

                db.Write(string.Format(@"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ", name), data.ToDB());
                data.IsStore = true;
            }

        }

        #endregion

        #region 缓存接口

        public class CacheGrab {

            public ConcurrentDictionary<int, DataGrab> Cache = new ConcurrentDictionary<int, DataGrab>();
            public int Min { get { return Cache.Count == 0 ? 0 : Cache.Keys.Min(); } }
            public int Max { get { return Cache.Count == 0 ? 0 : Cache.Keys.Max(); } }
            public int Count { get { return Cache.Count; } }

            public int LastKey = 0;
            public DataGrab this[int i] {
                get {
                    return Cache.Keys.Contains(i) ? Cache[i] : null;
                }
                set {
                    LastKey = i;
                    Cache[i] = value;
                    value.IsCache = true;
                }
            }

            public void RemoveAll() {

                foreach (var key in Cache.Keys.ToArray()) {
                    DataGrab dg;
                    if (Cache.TryRemove(key, out dg)) {
                        dg.Image.Dispose();
                    }
                }
            }
            public void RemoveOld(int store) {

                int del = Cache.Count - store;
                if (del > 0) {
                    foreach (var key in Cache.Keys.OrderByDescending(x => Math.Abs(x - LastKey)).Take(del).ToList()) {
                        DataGrab dg;
                        if (Cache.TryRemove(key, out dg)) {
                            dg.Image.Dispose();
                        }
                    }
                }
            }

            public void SaveToDB(DBTableGrab tg) {

                //
                foreach (var key in Cache.Keys.ToList()) {
                    tg.Save(this[key]);
                }
            }

        }

        #endregion

    }
}
