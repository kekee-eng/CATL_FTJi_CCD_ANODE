
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

        public class GrabDB {
            public GrabDB(TemplateDB parent, string tableName) {

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

        public class GrabCache {

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
                    }
                    else {
                        if (Width != w || Height != h)
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

            public void SaveToDB(GrabDB tg) {

                //
                foreach (var val in store.Values) {
                    tg.Save(val);
                }

            }

        }

        #endregion

        #region 入口

        public class GrabEntry {

            //
            public GrabEntry(TemplateDB parent, string tableName) {
                DB = new GrabDB(parent, tableName);
                Cache = new GrabCache();
            }

            //
            public void Save() {
                Cache.SaveToDB(DB);
            }

            //
            public int Width { get { return Math.Max(Cache.Width, DB.Width); } }
            public int Height { get { return Math.Max(Cache.Height, DB.Height); } }

            //
            public int Min { get { return Math.Min(Cache.Min, DB.Min); } }
            public int Max { get { return Math.Max(Cache.Max, DB.Max); } }
            public DataGrab this[int i] {
                get {
                    var ret1 = Cache[i];
                    if (ret1 != null) {
                        return ret1;
                    }

                    var ret2 = DB[i];
                    if (ret2 != null) {
                        Cache[i] = ret2;
                        return ret2;
                    }

                    return Cache[i] ?? DB[i];
                }
            }

            //
            public GrabCache Cache;
            public GrabDB DB;

            //
            public HImage GetImage(int i) {
                try {
                    return this[i].Image;
                }
                catch {
                    return null;
                }
            }
            public HImage GetImage(int start, int end) {

                //变量定义
                int w = Width;
                int h = Height;
                if (w == 0 || h == 0)
                    return null;

                //分配内存
                int newLength = end - start + 1;
                var newImage = new HImage("byte", w, h * newLength);

                //填充数据
                for (int i = start; i <= end; i++) {
                    var srcImage = GetImage(i);
                    UtilTool.Image.CopyImageOffset(newImage, srcImage, (i - start) * h, 0, h);
                }

                return newImage;
            }
            public HImage GetImage(double start, double end) {

                //变量定义
                int w = Width;
                int h = Height;
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
                    UtilTool.Image.CopyImageOffset(newImage, srcImage, hdst, hsrc, hcopy);
                }

                return newImage;
            }
            public void Check(ref int start, ref int end) {

                if (start > end) {
                    int tmp = start;
                    start = end;
                    end = tmp;
                }
                start = Math.Min(Math.Max(start, Min), Max);
                end = Math.Min(Math.Max(end, start), Max);
            }
        }

        #endregion

        #region 可视化工具

        public class ImageViewer {

            public ImageViewer(HWindowControl hwindow, GrabEntry grab) {

                //
                Box = hwindow;
                Grab = grab;

                //绘图句柄
                g = hwindow.HalconWindow;
                g.SetWindowParam("background_color", "cyan");
                g.ClearWindow();

                //
                initMouse(hwindow);
            }

            public HImage Image;
            public GrabEntry Grab;
            public HWindowControl Box;
            public HWindow g;

            public void View(double x, double y, double s) {

                //
                frameX = x;
                frameY = y;
                frameS = s;

                //
                updateView();
            }

            #region 鼠标事件
            
            //
            bool mouseAllow = true;
            bool mouseIsMove = false;
            int mouseBoxX = 0;
            int mouseBoxY = 0;
            double mouseFrameX = 0;
            double mouseFrameY = 0;

            //
            void initMouse(HWindowControl hwindow) {

                hwindow.SizeChanged += (o, e) => updateView();
                hwindow.MouseLeave += (o, e) => mouseIsMove = false;
                hwindow.MouseUp += (o, e) => mouseIsMove = false;
                hwindow.MouseDown += (o, e) => {
                    if (!mouseAllow) return;

                    if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                        mouseFrameX = frameX;
                        mouseFrameY = frameY;
                        mouseBoxX = e.X;
                        mouseBoxY = e.Y;
                        mouseIsMove = true;
                    }
                };
                hwindow.MouseMove += (o, e) => {
                    if (mouseAllow && mouseIsMove) {
                        double dy = mouseBoxY - e.Y;
                        double dx = mouseBoxX - e.X;

                        if (Math.Abs(dx) > 3 || Math.Abs(dy) > 3) {
                            frameX = mouseFrameX + dx / refBoxWidth * frameS;
                            frameY = mouseFrameY + dy / refBoxHeight * frameS;

                            updateView();
                        }
                    }
                };
                hwindow.HMouseWheel += (o, e) => {
                    if (!mouseAllow) return;

                    double s1 = frameS;
                    frameS *= (e.Delta < 0 ? 1.1 : 1 / 1.1);
                    frameS = Math.Min(frameS, 2);
                    frameS = Math.Max(frameS, 0.005);
                    double s2 = frameS;

                    frameX += (e.X / 640.0 - 1) * (s1 - s2);
                    frameY += (e.Y / 480.0 - 1) * (s1 - s2) * refGrabHeight / grabHeight;

                    updateView();
                };
            }

            #endregion

            #region 显示

            //
            int grabWidth { get { return Grab.Width; } }
            int grabHeight { get { return Grab.Height; } }
            int boxWidth { get { return Box.Width; } }
            int boxHeight { get { return Box.Height; } }

            int refGrabWidth { get { return grabWidth; } }
            int refGrabHeight { get { return grabWidth * boxHeight / boxWidth; } }
            int refBoxWidth { get { return boxWidth; } }
            int refBoxHeight { get { return boxWidth * grabHeight / grabWidth; } }

            //
            double frameX = 1;
            double frameY = 1;
            double frameS = 1;

            double frameX0 { get { return frameX - frameS * refGrabWidth / grabWidth; } }
            double frameY0 { get { return frameY - frameS * refGrabHeight / grabHeight; } }

            int frameStart = 0;
            int frameEnd = 0;

            int frameStartLimit { get { return Grab.Min; } }
            int frameEndLimit { get { return Grab.Max; } }

            int frameStartRequire { get { return (int)Math.Floor(frameY0); } }
            int frameEndRequire { get { return (int)Math.Ceiling(frameY); } }

            //
            int pixelPartRow1 { get { return (int)getPixelY(frameY0); } }
            int pixelPartRow2 { get { return (int)getPixelY(frameY); } }
            int pixelPartCol1 { get { return (int)getPixelX(frameX0); } }
            int pixelPartCol2 { get { return (int)getPixelX(frameX); } }

            //
            double getPixelX(double framex) { return framex * grabWidth; }
            double getPixelY(double framey) { return (framey - frameStart) * grabHeight; }

            void updateView() {

                if (frameEndRequire < frameStartLimit || frameStartRequire > frameEndLimit) {

                    //超过显示范围
                    g.ClearWindow();
                }
                else {

                    //准备图像
                    if ((frameStart != frameStartLimit && frameStart > frameStartRequire) ||
                    (frameEnd != frameEndLimit && frameEnd < frameEndRequire)) {

                        //
                        frameStart = frameStartRequire;
                        frameEnd = frameEndRequire;

                        //
                        Grab.Check(ref frameStart, ref frameEnd);
                        Image = Grab.GetImage(frameStart, frameEnd);
                    }

                    //显示图像
                    if (Image == null)
                        return;

                    g.SetPart(pixelPartRow1, pixelPartCol1, pixelPartRow2, pixelPartCol2);
                    g.DispImage(Image);

                    //显示极耳


                    //清空不显示区域
                    double x1 = 1.0 * boxWidth * (0 - pixelPartCol1) / (pixelPartCol2 - pixelPartCol1);
                    double x2 = 1.0 * boxWidth * (grabWidth - pixelPartCol1) / (pixelPartCol2 - pixelPartCol1);
                    double y1 = 1.0 * boxHeight * (0 - pixelPartRow1) / (pixelPartRow2 - pixelPartRow1);
                    double y2 = 1.0 * boxHeight * (grabHeight * (frameEnd - frameStart + 1) - pixelPartRow1) / (pixelPartRow2 - pixelPartRow1);
                    int w = boxWidth;
                    int h = boxHeight;

                    if (x1 > 0) g.ClearRectangle(0, 0, h, x1);
                    if (x2 < w) g.ClearRectangle(0, x2, h, w);
                    if (y1 > 0) g.ClearRectangle(0, 0, y1, w);
                    if (y2 < h) g.ClearRectangle(y2, 0, h, w);

                }

            }

            #endregion
            
        }

        #endregion

    }
}
