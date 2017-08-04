
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

            public int FrameStart {
                get {
                    if (Count == 0)
                        return 0;
                    return (int)(long)db.Read(string.Format(@"SELECT MIN(Frame) FROM {0}", name))[0][0];
                }
            }
            public int FrameEnd {
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
                if (CheckExist(data.Frame))
                    return;
                db.Write(string.Format(@"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ", name), data.ToDB());
            }
            
        }

        #endregion

        #region 缓存接口

        public class CacheGrab {

            public SortedDictionary<int, DataGrab> Cache = new SortedDictionary<int, DataGrab>();
            public int FrameStart { get { return Cache.Count == 0 ? 0 : Cache.Keys.Min(); } }
            public int FrameEnd { get { return Cache.Count == 0 ? 0 : Cache.Keys.Max(); } }
            public int Count { get { return Cache.Count; } }

            public DataGrab this [int i] {
                get {
                    return Cache.Keys.Contains(i) ? Cache[i] : null;
                }
                set {
                    Cache[i] = value;
                }
            }

            public void RemoveAll() {

                foreach (var key in Cache.Keys.ToArray()) {
                    Cache[key].Image.Dispose();
                    Cache.Remove(key);
                }
            }
            public void RemoveOld(int store) {
                
                int del = Cache.Count - store;
                if (del > 0) {
                    foreach (var key in Cache.Keys.Take(del).ToList()) {
                        Cache[key].Image.Dispose();
                        Cache.Remove(key);
                        
                    }
                }
            }
            
            public void SaveToDB(DBTableGrab tg) {
                
                //
                foreach(var key in Cache.Keys.ToList()) {
                    if (!tg.CheckExist(key))
                        tg.Save(Cache[key]);
                }
            }

        }

        #endregion

    }
}
