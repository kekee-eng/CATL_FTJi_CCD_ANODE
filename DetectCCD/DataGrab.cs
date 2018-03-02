
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

        //Mark相关数据
        public HImage ImageMark;
        public DataMark DM = new DataMark();


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
            double[] ax, ay1, ay2;
            if (Image != null && ImageProcess.DetectTab(Image, out hasDefect, out hasTab, out ax, out ay1, out ay2)) {

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

            }

            //检测Mark孔
            double[] cx, cy;
            double w = Width;
            double h = Height;
            if (ImageMark!=null && ImageProcess.DetectMark(ImageMark, out cx, out cy))
            {
                DM.HasMark = true;
                DM.MarkX = DM.MarkX_P = cx[0] / w;
                DM.MarkY = DM.MarkY_P = DM.MarkImageStart + cy[0] / h;

                if (cx.Length == 2 && cy.Length == 2)
                {
                    DM.HasTwoMark = true;
                    DM.MarkX_P = cx[1] / w;
                    DM.MarkY_P = DM.MarkImageStart + cy[1] / h;
                }

                //保存Mark孔图
                if (Static.App.RecordSaveImageEnable && Static.App.RecordSaveImageMark) {
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                    var savefolder = Static.FolderRecord + "/Mark孔/";

                    if (!System.IO.Directory.Exists(savefolder))
                        System.IO.Directory.CreateDirectory(savefolder);

                    string filename = $"{savefolder}{timestamp}_{Camera}_F{Convert.ToInt32(DM.MarkY)}";
                    var img = ImageMark.CopyImage();
                    Log.RecordAsThread(() => {
                        img?.WriteImage("png", 0, filename);
                        img?.Dispose();
                    });
                }
            }
            ImageMark.Dispose();


            isDetectTab = true;
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

        }
        
    }
}
