﻿
using HalconDotNet;
using System.Collections.Concurrent;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

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
        public static string GenTimeStamp(DateTime time) {
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
                Count = QueryCount;

                //
                if (Count != 0) {
                    Min = QueryMin;
                    Max = QueryMax;

                    this[Min].Image.GetImageSize(out Width, out Height);
                }
            }

            TemplateDB db;
            string name;

            public int QueryMin { get { return Count == 0 ? 1 : (int)(long)db.Read(string.Format(@"SELECT MIN(Frame) FROM {0}", name))[0][0]; } }
            public int QueryMax { get { return Count == 0 ? 1 : (int)(long)db.Read(string.Format(@"SELECT MAX(Frame) FROM {0}", name))[0][0]; } }
            public int QueryCount { get { return db.Count(name); } }

            public int Min;
            public int Max;
            public int Count;

            public int Width = -1;
            public int Height = -1;

            public DataGrab this[int i] {
                get {
                    var ret = db.Read(string.Format(@"SELECT * FROM {0} WHERE Frame=""{1}""", name, i));
                    if (ret.Count == 0)
                        return null;
                    return FromDB(ret[0]);
                }
            }

            public void Save(DataGrab data) {

                if (data == null)
                    return;

                if (data.IsStore)
                    return;

                if (db.Count(name + " WHERE Frame=" + data.Frame) != 0) {
                    data.IsStore = true;
                    return;
                }

                //
                db.Write(string.Format(@"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ", name), data.ToDB());
                data.IsStore = true;

                //
                int w, h;
                data.Image.GetImageSize(out w, out h);
                if (Count == 0) {
                    Width = w;
                    Height = h;

                    Min = data.Frame;
                    Max = data.Frame;
                }
                else {
                    if (Width != w || Height != h)
                        throw new Exception("DataGrab: DBTableGrab: Save: Image Size Error.");
                }

                //
                Min = Math.Min(Min, data.Frame);
                Max = Math.Max(Max, data.Frame);
                Count++;
            }

        }

        #endregion

        #region 缓存接口

        public class GrabCache {

            public ConcurrentDictionary<int, DataGrab> store = new ConcurrentDictionary<int, DataGrab>();
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
            
            //public void RemoveAll() {
            //    foreach (var key in store.Keys.ToArray()) {
            //        DataGrab dg;
            //        if (store.TryRemove(key, out dg)) {
            //            dg.Image.Dispose();
            //        }
            //    }
            //}

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
            public GrabEntry(TemplateDB parent, string tableName, int cache) {
                DB = new GrabDB(parent, tableName);
                Cache = new GrabCache() { CountLimit = cache };
            }

            //
            public void Save() {
                Cache.SaveToDB(DB);
            }

            //
            public int Width { get { return Math.Max(Cache.Width, DB.Width); } }
            public int Height { get { return Math.Max(Cache.Height, DB.Height); } }

            public int Min { get { return Math.Min(Cache.Min, DB.Min); } }
            public int Max { get { return Math.Max(Cache.Max, DB.Max); } }

            public bool LastLoadCache = false;
            public bool LastLoadDB = false;

            //
            public DataGrab this[int i] {
                get {
                    var ret1 = Cache[i];
                    if (ret1 != null) {
                        LastLoadCache = true;
                        LastLoadDB = false;
                        return ret1;
                    }

                    var ret2 = DB[i];
                    if (ret2 != null) {
                        LastLoadCache = false;
                        LastLoadDB = true;
                        Cache[i] = ret2;
                        return ret2;
                    }

                    return null;
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
                int len = end - start + 1;
                var newImage = new HImage("byte", w, h * len);

                //填充数据
                Enumerable.Range(start, len).AsParallel().ForAll(i => {
                    var srcImage = GetImage(i);
                    UtilTool.Image.CopyImageOffset(newImage, srcImage, (i - start) * h, 0, h);
                });

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
                Enumerable.Range(p1, p2 - p1).AsParallel().ForAll(i => {

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

                });

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
                g = hwindow.HalconWindow;

                //
                initEvent(hwindow);
                initMenu(hwindow);
            }

            //
            public HImage Image;
            public GrabEntry Grab;
            public HWindowControl Box;
            public HWindow g;

            public void MoveTargetSync(double refFps) {

                double fpsNow = Math.Max(1, showImageDynamic ? fpsControl : 1);
                if (!fpsWatch.IsRunning || fpsWatch.ElapsedMilliseconds > 1000 / fpsNow) {

                    //实时显示帧率
                    fpsRealtime = 1000.0 / fpsWatch.ElapsedMilliseconds;

                    //重置定时器
                    fpsWatch.Restart();

                    //
                    if (!targetAllow)
                        return;

                    //
                    double distTotal = targetDist;
                    if (distTotal == 0)
                        return;

                    //
                    double distMove = Math.Max(distTotal, 1) * refFps / fpsRealtime;

                    //
                    if (showImageStatic || distMove <= 0 || distMove >= distTotal) {
                        frameVx = targetVx;
                        frameVy = targetVy;
                        frameVs = targetVs;
                    }
                    else {
                        double precent = distMove / distTotal;
                        frameVx += targetDx * precent;
                        frameVy += targetDy * precent;
                        frameVs += targetDs * precent;
                    }

                    //
                    updateView();
                }
            }
            public void MoveTargetDirect() {

                //
                frameVx = targetVx;
                frameVy = targetVy;
                frameVs = targetVs;

                //
                updateView();
            }

            public void SetTopTarget(double y, double x = 0.5, double s = 1) {
                SetCenterTarget(y + frameDy / 2, x, s);
            }
            public void SetBottomTarget(double y, double x = 0.5, double s = 1) {
                SetCenterTarget(y - frameDy / 2, x, s);
            }
            public void SetCenterTarget(double y, double x = 0.5, double s = 1) {
                targetVx = x;
                targetVy = y;
                targetVs = s;
            }

            public void MoveFrame(double frame) {

                SetCenterTarget(frame);
                MoveTargetSync(fpsMoveRef);

            }
            public void MoveEA(int ea, int tab) {

            }

            //
            void initMenu(HWindowControl hwindow) {

                //
                Func<object, string, ToolStripMenuItem> addItem = (parent, name) => {

                    if (parent.GetType() == typeof(ContextMenuStrip))
                        return (parent as ContextMenuStrip).Items.Add(name) as ToolStripMenuItem;

                    if (parent.GetType() == typeof(ToolStripMenuItem)) {
                        return (parent as ToolStripMenuItem).DropDownItems.Add(name) as ToolStripMenuItem;
                    }

                    throw new Exception("DataGrab: ImageViewer: initMenu: addItem: unknow type.");
                };
                Action<object> addSp = (parent) => {
                    if (parent.GetType() == typeof(ContextMenuStrip)) {
                        (parent as ContextMenuStrip).Items.Add(new ToolStripSeparator());
                        return;
                    }
                    if (parent.GetType() == typeof(ToolStripMenuItem)) {
                        (parent as ToolStripMenuItem).DropDownItems.Add(new ToolStripSeparator());
                        return;
                    }

                    throw new Exception("DataGrab: ImageViewer: initMenu: addSeq: unknow type.");
                };
                Action<ToolStripMenuItem, Action<bool>> bindItem = (item, func) => {
                    item.Click += (o, e) => item.Checked ^= true;
                    if (func != null) item.CheckedChanged += (o, e) => func(item.Checked);
                };

                //
                var rtMenu = new ContextMenuStrip();

                //
                var rtImage = addItem(rtMenu, "图像模式");
                var rtImageStatic = addItem(rtImage, "静态图像");
                var rtImageDynamic = addItem(rtImage, "滚动图像");

                //
                var rtContext = addItem(rtMenu, "显示内容");
                var rtContextDef1 = addItem(rtContext, "[预设1] 开启检测");
                var rtContextDef2 = addItem(rtContext, "[预设2] 开启定位");
                addSp(rtContext);
                var rtContextEA = addItem(rtContext, "EA分割线");
                var rtContextTab = addItem(rtContext, "极耳位置");
                var rtContextWidth = addItem(rtContext, "测宽检测结果");
                var rtContextNG = addItem(rtContext, "瑕疵检测结果");
                var rtContextLabel = addItem(rtContext, "打标位置");
                addSp(rtContext);
                var rtContextCross = addItem(rtContext, "定位准星");

                //
                var rtLocFrameText = new ToolStripTextBox();
                var rtLocEAText1 = new ToolStripTextBox();
                var rtLocEAText2 = new ToolStripTextBox();

                //
                addSp(rtMenu);
                var rtEnableUser = addItem(rtMenu, "启用交互");
                addSp(rtMenu);
                var rtReset = addItem(rtMenu, "复位视图");
                var rtLoc = addItem(rtMenu, "定位到");
                var rtLocFrame = addItem(rtLoc, "Frame");
                rtLocFrame.DropDownItems.Add(rtLocFrameText);
                var rtLocEA = addItem(rtLoc, "EA");
                rtLocEA.DropDownItems.Add(rtLocEAText1);
                rtLocEA.DropDownItems.Add(rtLocEAText2);
                var rtMeasure = addItem(rtMenu, "测量工具");
                var rtMeasureLocPoint = addItem(rtMeasure, "定位点坐标");
                var rtMeasurePPDist = addItem(rtMeasure, "点到点测距");

                //
                bindItem(rtImageStatic, b => rtImageDynamic.Checked = !(showImageStatic = b));
                bindItem(rtImageDynamic, b => rtImageStatic.Checked = !(showImageDynamic = b));

                bindItem(rtContextDef1, null);
                bindItem(rtContextDef2, null);

                bindItem(rtContextEA, b => showContextEA = b);
                bindItem(rtContextTab, b => showContextTab = b);
                bindItem(rtContextWidth, b => showContextWidth = b);
                bindItem(rtContextNG, b => showContextNG = b);
                bindItem(rtContextLabel, b => showContextLabel = b);
                bindItem(rtContextCross, b => showContextCross = b);

                bindItem(rtEnableUser, b => mouseAllow = !(targetAllow = !b));

                //
                rtContextDef1.CheckedChanged += (o, e) => {
                    bool b = (o as ToolStripMenuItem).Checked;
                    rtContextEA.Checked = b;
                    rtContextTab.Checked = b;
                    rtContextWidth.Checked = b;
                    rtContextNG.Checked = b;
                    rtContextLabel.Checked = b;
                };
                rtContextDef2.CheckedChanged += (o, e) => {
                    bool b = (o as ToolStripMenuItem).Checked;
                    rtContextCross.Checked = b;
                };
                rtEnableUser.CheckedChanged += (o, e) => {
                    bool b = (o as ToolStripMenuItem).Checked;
                    rtReset.Visible = b;
                    rtLoc.Visible = b;
                    rtMeasure.Visible = b;
                };

                rtReset.Click += (o, e) => {
                    frameVs = 1.0;
                    frameVx = Math.Min(Math.Max(frameVx, 0), 1);
                    frameVy = Math.Min(Math.Max(frameVy, frameStartLimit), frameEndLimit);
                    updateView();
                };

                rtLocFrameText.KeyDown += (o, e) => {

                };

                rtMeasureLocPoint.Click += (o, e) => {

                };
                rtMeasurePPDist.Click += (o, e) => {

                };

                //
                rtImageDynamic.Checked = true;
                rtContextDef1.Checked = true;

                rtEnableUser.Checked = true;
                rtEnableUser.Checked = false;

                //
                hwindow.ContextMenuStrip = rtMenu;

            }
            void initEvent(HWindowControl hwindow) {

                //
                hwindow.SizeChanged += (o, e) => {

                    try { updateView(); }
                    catch { }

                };
                hwindow.HInitWindow += (o, e) => {
                    g.SetWindowParam("background_color", "cyan");
                    g.ClearWindow();
                };

                //
                hwindow.PreviewKeyDown += (o, e) => {
                    if (!mouseAllow) return;

                    //
                    switch (e.KeyCode) {
                        case Keys.Left: frameVx -= frameDx / 10; break;
                        case Keys.Right: frameVx += frameDx / 10; break;

                        case Keys.Up: frameVy -= frameDy / 10; break;
                        case Keys.Down: frameVy += frameDy / 10; break;
                        case Keys.PageUp: frameVy -= frameDy; break;
                        case Keys.PageDown: frameVy += frameDy; break;
                        case Keys.Home: frameVy = frameStartLimit; break;
                        case Keys.End: frameVy = frameEndLimit; break;

                        default: return;
                    }

                    updateView();
                };

                //
                hwindow.MouseLeave += (o, e) => mouseIsMove = false;
                hwindow.MouseUp += (o, e) => mouseIsMove = false;
                hwindow.MouseDown += (o, e) => {
                    if (!mouseAllow) return;

                    if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                        mouseFrameX = frameVx;
                        mouseFrameY = frameVy;
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
                            frameVx = mouseFrameX + dx / refBoxWidth * frameVs;
                            frameVy = mouseFrameY + dy / refBoxHeight * frameVs;

                            updateView();
                        }
                    }
                };
                hwindow.HMouseWheel += (o, e) => {
                    if (!mouseAllow) return;

                    double s1 = frameVs;
                    frameVs *= (e.Delta < 0 ? 1.1 : 1 / 1.1);
                    frameVs = Math.Min(frameVs, 2);
                    frameVs = Math.Max(frameVs, 0.005);
                    double s2 = frameVs;

                    frameVx += (e.X / 640.0 - 0.5) * (s1 - s2);
                    frameVy += (e.Y / 480.0 - 0.5) * (s1 - s2) * refGrabHeight / grabHeight;

                    updateView();
                };


            }
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

                    if (Image == null)
                        return;

                    if (!Box.IsHandleCreated)
                        return;

                    //显示图像
                    {
                        //
                        Func<double, double> getPixelX = framex => framex * grabWidth;
                        Func<double, double> getPixelY = framey => (framey - frameStart) * grabHeight;

                        //
                        int row1 = (int)getPixelY(frameY1);
                        int row2 = (int)getPixelY(frameY2);
                        int col1 = (int)getPixelX(frameX1);
                        int col2 = (int)getPixelX(frameX2);

                        //
                        g.SetPart(row1, col1, row2, col2);
                        g.DispImage(Image);

                        //显示极耳


                        if (showContextCross) {

                            g.SetDraw("margin");
                            g.SetColor("red");
                            g.SetLineWidth(1);

                            g.DispLine(getPixelY(frameVy), getPixelX(frameX1), getPixelY(frameVy), getPixelX(frameX2));
                            g.DispLine(getPixelY(frameY1), getPixelX(frameVx), getPixelY(frameY2), getPixelX(frameVx));

                        }

                        //清空不显示区域
                        int bw = boxWidth;
                        int bh = boxHeight;
                        int gw = grabWidth;
                        int gh = grabHeight;

                        double x1 = 1.0 * bw * (0 - col1) / (col2 - col1);
                        double x2 = 1.0 * bw * (gw - col1) / (col2 - col1);
                        double y1 = 1.0 * bh * (0 - row1) / (row2 - row1);
                        double y2 = 1.0 * bh * (gh * (frameEnd - frameStart + 1) - row1) / (row2 - row1);

                        if (x1 > 0) g.ClearRectangle(0, 0, bh, x1);
                        if (x2 < bw) g.ClearRectangle(0, x2, bh, bw);
                        if (y1 > 0) g.ClearRectangle(0, 0, y1, bw);
                        if (y2 < bh) g.ClearRectangle(y2, 0, bh, bw);
                    }
                }

            }
            
            //
            bool showImageStatic = false;
            bool showImageDynamic = false;

            bool showContextEA = false;
            bool showContextTab = false;
            bool showContextWidth = false;
            bool showContextNG = false;
            bool showContextLabel = false;
            bool showContextCross = false;

            //
            double fpsMoveRef = 10;
            double fpsControl = 25;
            double fpsRealtime = 1;
            Stopwatch fpsWatch = new Stopwatch();

            //
            bool targetAllow = false;
            double targetVx =0.5;
            double targetVy =1;
            double targetVs =1;

            double targetDx { get { return targetVx - frameVx; } }
            double targetDy { get { return targetVy - frameVy; } }
            double targetDs { get { return targetVs - frameVs; } }
            double targetDist {  get { return Math.Sqrt(targetDx * targetDx + targetDy * targetDy); } }
            
            //
            bool mouseAllow = true;
            bool mouseIsMove = false;
            int mouseBoxX = 0;
            int mouseBoxY = 0;
            double mouseFrameX = 0;
            double mouseFrameY = 0;
            
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
            double frameVx = 0.5;
            double frameVy = 1;
            double frameVs = 1;
            
            double frameDx { get { return  frameVs * refGrabWidth / grabWidth; } }
            double frameDy { get { return  frameVs * refGrabHeight / grabHeight; } }

            double frameX1 { get { return frameVx - frameDx/2; } }
            double frameY1 { get { return frameVy - frameDy/2; } }

            double frameX2 { get { return frameVx + frameDx/2; } }
            double frameY2 { get { return frameVy + frameDy/2; } }

            //
            int frameStart = 0;
            int frameEnd = 0;

            int frameStartRequire { get { return (int)Math.Floor(frameY1); } }
            int frameEndRequire { get { return (int)Math.Ceiling(frameY2); } }

            int frameStartLimit { get { return Grab.Min; } }
            int frameEndLimit { get { return Grab.Max+1; } }
            
        }

        #endregion

    }
}
