
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DetectCCD {

    public class EntryGrab {

        //
        public EntryGrab(TemplateDB parent, string tableName, int cache) {
            DB = new DataGrab.GrabDB(parent, tableName);
            Cache = new DataGrab.GrabCache() { CountLimit = cache };
        }

        public void Reload() {
            DB.Count = DB.QueryCount;
            if (DB.Count > 0) {
                DB.Min = DB.QueryMin;
                DB.Max = DB.QueryMax;
                this[DB.Min] = DB[DB.Min];
            }
        }

        //
        public DataGrab.GrabCache Cache;
        public DataGrab.GrabDB DB;

        //
        public double Fx = -1;
        public double Fy = -1;

        public double ScaleX = -1;
        public double ScaleY = -1;

        public int Width = -1;
        public int Height = -1;

        public int Min { get { return Math.Min(Cache.Min, DB.Min); } }
        public int Max { get { return Math.Max(Cache.Max, DB.Max); } }

        public bool LastLoadCache = false;
        public bool LastLoadDB = false;

        //
        public DataGrab this[int i] {
            set {
                if (value == null)
                    return;

                if (Cache.Count == 0 || Fx == 0 || Fy == 0 || ScaleX == 0 || ScaleY == 0) {
                    if (value.Image != null) {
                        value.Image.GetImageSize(out value.Width, out value.Height);
                    }
                    Width = value.Width;
                    Height = value.Height;
                    ScaleX = value.ScaleX;
                    ScaleY = value.ScaleY;
                    Fx = value.Fx;
                    Fy = value.Fy;
                }

                Cache[i] = value;

            }
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
                    this[i] = ret2;
                    return ret2;
                }

                return null;
            }
        }
        public List<DataGrab> Save() {
            return Cache.SaveToDB(DB);
        }

        //
        public HImage GetImage(int i) {
            try { return this[i].Image; }
            catch { return null; }
        }
        public HImage GetImage(int start, int end) {

            //变量定义
            int w = Width;
            int h = Height;
            if (w <= 0 || h <= 0)
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
            if (w <= 0 || h <= 0)
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
            if (nh <= 0)
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

        //
        public int GetEncoder(double frame) {

            int encoder = 0;
            Static.SafeRun(() => {

                int p1 = (int)Math.Floor(frame) -1;
                int p2 = p1 + 1;

                var obj1 = this[p1];
                var obj2 = this[p2];

                if (obj1 == null || obj2 == null) {
                    obj1 = this[Min];
                    obj2 = this[Max];
                }

                if (obj1 != null && obj2 != null) {
                    encoder = (int)(obj1.Encoder + (frame - 1 - p1) * (obj2.Encoder - obj1.Encoder));
                }
            });
            return encoder;

        }
    }
}
