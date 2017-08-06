using Common;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.DataGrab;

namespace Detect4K {

    class EntryGrab {

        //
        public EntryGrab(TemplateDB parent, string tableName, int cache) {
            DB = new GrabDB(parent, tableName);
            Cache = new GrabCache() { CountLimit = cache };
        }

        //
        public GrabCache Cache;
        public GrabDB DB;

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
        public void Save() {
            Cache.SaveToDB(DB);
        }

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
}
