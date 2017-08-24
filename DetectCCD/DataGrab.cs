
using HalconDotNet;
using System.Collections.Concurrent;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace DetectCCD {
#pragma warning disable 0649

    public class DataGrab {

        //相机
        public string Camera;

        //帧数据
        public int Frame;
        public int Encoder;
        public string Timestamp;

        //图像
        public HImage Image;

        //附像素数据
        public double ScaleX;
        public double ScaleY;

        public int Width;
        public int Height;

        public double Fx { get { return ScaleX * Width; } }
        public double Fy { get { return ScaleY * Height; } }

        //标记
        public bool IsCreated = false;
        public bool IsDetect = false;
        public bool IsCache = false;
        public bool IsStore = false;

        //
        public bool isDetectTab = false;
        public bool hasTab = false;
        public bool hasDefect = false;
        public DataTab TabData = null;

        public void DetectTab() {
            if (isDetectTab)
                return;

            //极耳检测、并判断是否可能有瑕疵
            var aimage = Image;
            double[] ax, ay1, ay2;
            if (aimage != null && ImageProcess.DetectTab(aimage, out hasDefect, out hasTab, out ax, out ay1, out ay2)) {

                if (hasTab) {
                    //极耳数据整理
                    TabData = new DataTab();
                    TabData.TabX = ax[0] / Width;
                    TabData.TabY1 = TabData.TabY1_P = Frame + ay1[0] / Height;
                    TabData.TabY2 = TabData.TabY2_P = Frame + ay2[0] / Height;
                    if (ax.Length == 2 && ay1.Length == 2 && ay2.Length == 2) {
                        TabData.HasTwoTab = true;
                        TabData.TabX_P = ax[1] / Width;
                        TabData.TabY1_P = Frame + ay1[1] / Height;
                        TabData.TabY2_P = Frame + ay2[1] / Height;
                    }
                }

                if (Static.App.RecordSaveImageEnable) {
                    bool isSaveImage = false;
                    if (hasDefect) {
                        isSaveImage = Static.App.RecordSaveImageNG;
                    }
                    else {
                        isSaveImage = Static.App.RecordSaveImageOK;
                    }

                    if (isSaveImage) {
                        var folder = Static.FolderTemp + "ImageBig/";
                        var filename = string.Format("{0}{1}_F{2}", folder, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"), Frame);

                        if (!System.IO.Directory.Exists(folder))
                            System.IO.Directory.CreateDirectory(folder);

                        new Thread(new ThreadStart((() => {
                            Static.SafeRun(() => {
                                aimage.WriteImage("bmp", 0, filename);
                            });
                        }))).Start();
                    }
                }
            }

            isDetectTab = true;
        }

        //是否存在
        public bool hasMark = false;

        //生成时间戳
        public static string GenTimeStamp(DateTime time) {
            return time.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        //数据库接口
        public class GrabDB {
            public GrabDB(TemplateDB parent, string tableName) {

                //
                db = parent;
                this.tname = tableName;

                //
                Count = QueryCount;

                //
                if (Count != 0) {
                    Min = QueryMin;
                    Max = QueryMax;

                    this[Min].Image.GetImageSize(out Width, out Height);
                }
            }

            TemplateDB db;
            string tname;

            public int QueryMin { get { return Count == 0 ? 1 : (int)(long)db.Read(string.Format(@"SELECT MIN(Frame) FROM {0}", tname))[0][0]; } }
            public int QueryMax { get { return Count == 0 ? 1 : (int)(long)db.Read(string.Format(@"SELECT MAX(Frame) FROM {0}", tname))[0][0]; } }
            public int QueryCount { get { return db.Count(tname); } }

            public int Min =1;
            public int Max =1;
            public int Count =0;

            public int Width = -1;
            public int Height = -1;

            public DataGrab this[int i] {
                get {
                    var ret = db.Read(string.Format(@"SELECT * FROM {0} WHERE Frame=""{1}""", tname, i));
                    if (ret == null || ret.Count == 0)
                        return null;
                    return FromDB(ret[0]);
                }
            }
            public bool Save(DataGrab data) {

                if (data == null)
                    return false;

                if (data.IsStore)
                    return false;

                if (db.Count(tname + " WHERE Frame=" + data.Frame) != 0) {
                    data.IsStore = true;
                    return false;
                }

                //
                bool isSaveImage = (data.hasDefect && Static.App.RecordSaveImageNG) || (!data.hasDefect && Static.App.RecordSaveImageOK);
                
                //
                db.Write(string.Format(@"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ", tname), ToDB(data, isSaveImage));

                //
                if (Count == 0) {

                    int w, h;
                    data.Image.GetImageSize(out w, out h);

                    Width = w;
                    Height = h;

                    Min = data.Frame;
                    Max = data.Frame;
                }

                //
                Min = Math.Min(Min, data.Frame);
                Max = Math.Max(Max, data.Frame);
                Count++;

                return true;
            }
            public void CreateTable() {

                //
                db.Write(string.Format(@"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER     PRIMARY KEY     AUTOINCREMENT,
Camera          TEXT,
Frame           INTEGER,
Encoder         INTEGER,
Timestamp       TEXT,
Image           BLOB
)", this.tname));

            }

            static object[] ToDB(DataGrab data, bool saveImage) {
                return new object[] { data.Camera, data.Frame, data.Encoder, data.Timestamp, saveImage ? UtilSerialization.obj2bytes(data.Image) : new byte[0] };
            }
            static DataGrab FromDB(object[] objs) {

                System.Func<object, object, object> getDef = (obj, def) => obj is System.DBNull ? def : obj;

                var data = new DataGrab() {
                    Camera = (string)getDef(objs[1], ""),
                    Frame = (int)(long)getDef(objs[2], 0),
                    Encoder = (int)(long)getDef(objs[3], 0),
                    Timestamp = (string)getDef(objs[4], ""),
                    Image = (HImage)UtilSerialization.bytes2obj((byte[])getDef(objs[5], null)),
                    IsCreated = true,
                    IsCache = true,
                    IsStore = true,
                };
                
                return data;

            }
        }

        //缓存接口
        public class GrabCache : IDisposable {

            public void Dispose() {

                foreach (var key in store.Keys.ToList()) {
                    DataGrab dg;
                    if (store.TryRemove(key, out dg)) {
                        dg.Image.Dispose();
                    }
                }
            }

            ConcurrentDictionary<int, DataGrab> store = new ConcurrentDictionary<int, DataGrab>();

            public int Min { get { return store.Count == 0 ? 1 : store.Keys.Min(); } }
            public int Max { get { return store.Count == 0 ? 1 : store.Keys.Max(); } }
            public int Count { get { return store.Count; } }
            public int CountLimit = 200;

            public int Width = -1;
            public int Height = -1;
            public int LastKey = 0;

            public DataGrab this[int i] {
                get {
                    return store.Keys.Contains(i) ? store[i] : null;
                }
                set {
                    if (value == null)
                        return;

                    //
                    int w, h;
                    value.Image.GetImageSize(out w, out h);
                    if (store.Count == 0) {
                        Width = w;
                        Height = h;
                    }
                    else {
                        if (Width != w || Height != h)
                            throw new Exception("DataGrab: CacheGrab: Set: Image Size Error.");
                    }

                    //
                    LastKey = i;
                    store[i] = value;
                    value.IsCache = true;

                    //RemoveOld
                    int del = Count - CountLimit;
                    if (del > 0) {
                        foreach (var key in store.Keys.OrderByDescending(x => Math.Abs(x - LastKey)).Take(del).ToList()) {
                            DataGrab dg;
                            if (store.TryRemove(key, out dg)) {
                                dg.Image.Dispose();
                            }
                        }
                    }

                }
            }
            public List<DataGrab> SaveToDB(GrabDB tg) {

                //
                List<DataGrab> saveDatas = new List<DataGrab>();
                foreach (var val in store.Values) {
                    if (tg.Save(val))
                        saveDatas.Add(val);
                }

                //
                return saveDatas;

            }

            public DataGrab LastDataGrab = null;
            public DataGrab GetFirstUnDetect() {

                int min = Min;
                int max = Max;
                for(int i = min; i <= max; i++) {
                    var obj = this[i];
                    if (obj !=null && !obj.IsDetect)
                        return obj;
                }
                return null;
            }

        }
        
    }
}
