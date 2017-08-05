
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
        public static string GenTimeStamp(System.DateTime time) {
            return time.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        #region 数据库接口

        object[] ToDB() {
            return new object[] { Camera, Frame, Encoder, Timestamp, UtilSerialization.obj2bytes(Image) };
        }
        static DataGrab FromDB(object[] objs) {
            System.Func<object, object, object> getDef = (obj, def) => obj is System.DBNull ? def : obj;

            var data = new DataGrab() {
                Camera = (string)getDef(objs[1], ""),
                Frame = (int)(long)getDef(objs[2], 0),
                Encoder = (int)(long)getDef(objs[3], 0),
                Timestamp = (string)getDef(objs[4], ""),
                
                IsCreated = true,
                IsCache = true,
                IsStore = true,
            };

            var a = getDef(objs[5], null);
            var b = (byte[])a;
            var c = UtilSerialization.bytes2obj(b);
            var d = (HImage)c;

            data.Image = (HImage)UtilSerialization.bytes2obj((byte[])getDef(objs[5], null));

            return data;

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

                //
                if (Count != 0) {
                    this[Min].Image.GetImageSize(out Width, out Height);
                }
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

            public int Width = 0;
            public int Height = 0;

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

                //
                int w, h;
                data.Image.GetImageSize(out w, out h);
                if (Count == 0) {
                    Width = w;
                    Height = h;
                }
                else {
                    if (Width != w || Height != h)
                        throw new Exception("DataGrab: DBTableGrab: Save: Image Size Error.");
                }

                db.Write(string.Format(@"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ", name), data.ToDB());
                data.IsStore = true;
            }

        }

        #endregion

        #region 缓存接口

        public class CacheGrab {

            public ConcurrentDictionary<int, DataGrab> store = new ConcurrentDictionary<int, DataGrab>();
            public int Min { get { return store.Count == 0 ? 0 : store.Keys.Min(); } }
            public int Max { get { return store.Count == 0 ? 0 : store.Keys.Max(); } }
            public int Count { get { return store.Count; } }

            public int Width = 0;
            public int Height = 0;

            public int LastKey = 0;
            public DataGrab this[int i] {
                get {
                    return store.Keys.Contains(i) ? store[i] : null;
                }
                set {

                    //
                    int w, h;
                    value.Image.GetImageSize(out w, out h);
                    if (store.Count == 0) {
                        Width = w;
                        Height = h;
                    }else {
                        if(Width!=w || Height!=h)
                            throw new Exception("DataGrab: CacheGrab: Set: Image Size Error.");
                    }

                    //
                    LastKey = i;
                    store[i] = value;
                    value.IsCache = true;

                }
            }

            public void RemoveAll() {

                foreach (var key in store.Keys.ToArray()) {
                    DataGrab dg;
                    if (store.TryRemove(key, out dg)) {
                        dg.Image.Dispose();
                    }
                }
            }
            public void RemoveOld(int max) {

                int del = this.store.Count - max;
                if (del > 0) {
                    foreach (var key in store.Keys.OrderByDescending(x => Math.Abs(x - LastKey)).Take(del).ToList()) {
                        DataGrab dg;
                        if (store.TryRemove(key, out dg)) {
                            dg.Image.Dispose();
                        }
                    }
                }
            }

            public void SaveToDB(DBTableGrab tg) {

                //
                foreach (var key in store.Keys.ToList()) {
                    tg.Save(this[key]);
                }
            }

        }

        #endregion

        #region 入口

        public class EntryGrab {

            public EntryGrab(TemplateDB parent, string tableName) {
                DB = new DBTableGrab(parent, tableName);
                Cache = new CacheGrab();
            }

            public int Min { get { return Math.Min(Cache.Min, DB.Min); } }
            public int Max { get { return Math.Max(Cache.Max, DB.Max); } }
            public DataGrab this[int i] {
                get {
                    var ret1 = Cache[i];
                    if (ret1 != null) {
                        return ret1;
                    }

                    var ret2 = DB[i];
                    if(ret2!=null) {
                        Cache[i] = ret2;
                        return ret2;
                    }

                    return Cache[i] ?? DB[i];
                }
            }

            public CacheGrab Cache;
            public DBTableGrab DB;
            public ViewerImageGrab Viewer;

            public void SaveToDB() {
                Cache.SaveToDB(DB);
            }
            
            public HImage GetImage(int i) {
                var dt = this[i];
                return dt == null ? null : dt.Image;
            }
            public HImage GetImage(int start, int end) {

                //变量定义
                int w = Math.Max( Cache.Width, DB.Width);
                int h = Math.Max(Cache.Height,DB.Height);
                if (w == 0 || h == 0)
                    return null;

                //分配内存
                int newLength = end - start + 1;
                var newImage = new HImage("byte", w, h * newLength);

                //填充数据
                for (int i = start; i <= end; i++) {
                    var srcImage = GetImage(i);
                    UtilTool.CopyImageOffset(newImage, srcImage, (i - start) * h, 0, h);
                }

                return newImage;
            }
            public HImage GetImage(double start, double end) {

                //变量定义
                int w = Math.Max(Cache.Width, DB.Width);
                int h = Math.Max(Cache.Height, DB.Height);
                if (w == 0 || h == 0)
                    return null;

                //计算偏移
                int p1 = (int)Math.Floor(start);
                int p2 = (int)Math.Floor(end) + 1;

                double p1f = start - p1;
                double p2f = p2 - end;

                int p1start = (int)(p1f * h);
                int p1len = h - p1start;

                int p2start = 0;
                int p2len = (int)((1 - p2f) * h);

                //分配内存
                int nh = p1len + p2len + (p2 - p1 - 2) * h;
                if (nh < 0)
                    return null;
                var newImage = new HImage("byte", w, nh);

                //填充数据
                for (int i = p1; i < p2; i++) {

                    int hsrc;
                    int hdst;
                    int hcopy;

                    if (i == p1 && i == p2 - 1) {
                        hcopy = p1len + p2len - h;
                        hsrc = p1start;
                        hdst = 0;
                    }
                    else if (i == p1) {
                        hcopy = p1len;
                        hsrc = p1start;
                        hdst = 0;
                    }
                    else if (i == p2 - 1) {
                        hcopy = p2len;
                        hsrc = p2start;
                        hdst = p1len + (i - p1 - 1) * h;
                    }
                    else {
                        hcopy = h;
                        hsrc = 0;
                        hdst = p1len + (i - p1 - 1) * h;
                    }

                    var srcImage = GetImage(i);
                    UtilTool.CopyImageOffset(newImage, srcImage, hdst, hsrc, hcopy);
                }

                return newImage;
            }

        }

        #endregion

        #region 可视化工具

        public class ViewerImageGrab {

            public ViewerImageGrab(HWindowControl hwin, EntryGrab grab) {

                //
                ImageBox = hwin;


            }

            public HWindowControl ImageBox;
            public EntryGrab ImageSource;

        }

        #endregion

    }
}
