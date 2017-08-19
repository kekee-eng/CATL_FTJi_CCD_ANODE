using HalconDotNet;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectCCD {

    public class CameraZipFile : TemplateCamera, IDisposable {

        public CameraZipFile(string filename) {
            m_filename = filename;
            prepareFile(filename);
            new Thread(new ThreadStart(threadProcess)).Start();

            m_frame = Static.App.CameraFrameStart;
            m_frameStart = Static.App.CameraFrameStart;
            m_fpsControl = Static.App.CameraFpsControl;
        }

        //
        string m_filename;
        ZipFile m_zipfile;
        Dictionary<int, ZipEntry> m_entrys;
        Dictionary<int, DataGrab> m_datas;

        //
        async void prepareFile(string zipfilename) {

            //从压缩包名称中取得相机名称
            Func<string, string> getCameraName = (filename) => {
                int p1 = filename.IndexOf('[');
                int p2 = filename.IndexOf(']');

                if (p1 < 0 || p2 < 0 || p1 > p2)
                    return "UNKNOW";
                else
                    return filename.Substring(p1 + 1, p2 - p1 - 1);
            };

            //从文件名称中取得图像信息
            Func<string, DataGrab> getImageData = (filename) => {

                // 2017_07_23_18_42_26_4144_F4.bmp
                // ['2017', '07', '23', '18', '42', '26', '4144', '4']
                //     0     1     2     3     4     5     6       7
                var tmp = filename.Replace("\\", "/").Split('/');
                var tmp2 = tmp[tmp.Length - 1];

                string[] sp = tmp2.ToUpper().Replace("F", "").Replace(".BMP", "").Split('_');
                if (sp.Length != 8)
                    return null;

                int[] v = new int[sp.Length];
                for (int i = 0; i < sp.Length; i++)
                    if (!int.TryParse(sp[i], out v[i]))
                        return null;

                //
                var data = new DataGrab() {
                    Camera = Name,
                    Frame = v[7],
                    Encoder = 0,
                    Timestamp = DataGrab.GenTimeStamp(new DateTime(v[0], v[1], v[2], v[3], v[4], v[5], v[6] / 10)),

                };

                return data;
            };

            isOpen = false;
            await Task.Run(() => {

                //
                Name = getCameraName(zipfilename);

                //
                m_zipfile = new ZipFile(zipfilename);
                m_entrys = new Dictionary<int, ZipEntry>();
                m_datas = new Dictionary<int, DataGrab>();

                //
                foreach (var entry in m_zipfile.Entries) {
                    if (entry.FileName.ToUpper().Contains(".BMP")) {
                        var data = getImageData(entry.FileName);

                        m_entrys[data.Frame] = entry;
                        m_datas[data.Frame] = data;
                    }
                }

                if (m_datas.Count == 0)
                    throw new Exception("当前离线压缩包中无数据！");

                isOpen = true;
            });
        }
        DataGrab prepareImage() {

            //
            if (!isOpen)
                return null;

            //
            m_frame++;
            if (m_frame > Max)
                m_frame = Max + 1;

            if (m_frame < Min)
                m_frame = Min;

            //
            if (!m_entrys.Keys.Contains(m_frame))
                return null;

            if (!m_datas.Keys.Contains(m_frame))
                return null;

            var entry = m_entrys[m_frame];
            var data = m_datas[m_frame];

            //
            var outdata = data;

            //内存复制
            using (var memory = new MemoryStream()) {
                entry.Extract(memory);

                //
                Bitmap bmp = new Bitmap(memory);
                var bmpdata = bmp.LockBits(new Rectangle(new Point(0, 0), bmp.Size), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                IntPtr src = bmpdata.Scan0;

                //
                string type;
                int w, h;
                outdata.Image = new HImage("byte", bmp.Width, bmp.Height);
                IntPtr dst = outdata.Image.GetImagePointer1(out type, out w, out h);

                //
                UtilTool.Image.CopyMemory(dst, src, w * h);

                //
                m_encoder += bmp.Height;
            }


            //
            outdata.Encoder = m_encoder;
            outdata.IsCreated = true;
            return outdata;
        }
        void threadProcess() {

            Stopwatch watch = new Stopwatch();
            while (!isQuit) {
                Thread.Sleep(1);
                if (!isGrabbing && watch.IsRunning) {
                    watch.Stop();
                    watch.Reset();
                }

                if (isGrabbing) {

                    //
                    isStopOk = false;

                    //
                    if (!watch.IsRunning) {
                        watch.Stop();
                        watch.Reset();
                        watch.Start();
                    }

                    //定义变量
                    double timeStart = 0;
                    double timeEnd = 0;
                    double timeElapsedRequire = 0;
                    double timeElapsedReal = 0;

                    //记录起始时间
                    timeStart = watch.ElapsedMilliseconds;

                    //准备图像
                    DataGrab dg = null;
                    while (isGrabbing && dg == null) {
                        Thread.Sleep(10);
                        dg = prepareImage();
                    }

                    //计算达到指定帧率，所需要的时间
                    if (m_fpsControl != 0)
                        timeElapsedRequire = 1000 / m_fpsControl;
                    timeElapsedRequire = Math.Max(timeElapsedRequire, 1);
                    timeElapsedRequire = Math.Min(timeElapsedRequire, 10000);

                    //等待时间到达
                    do {
                        Thread.Sleep(1);
                        timeEnd = watch.ElapsedMilliseconds;
                        timeElapsedReal = timeEnd - timeStart;
                    } while (isGrabbing && timeElapsedReal < timeElapsedRequire);

                    //计算实时帧率
                    m_fpsRealtime = 1000 / timeElapsedReal;

                    //回调
                    if (dg != null)
                        callImageReady(dg);

                    if (m_frame == Max)
                        callComplete();

                }
                else {
                    isStopOk = true;
                }
            }

            if (m_zipfile != null)
                m_zipfile.Dispose();
            
        }

        //
        public override void Reset() {
            if (!isGrabbing) {
                m_frame = m_frameStart;
                m_encoder = 0;
            }
        }
        public override void Dispose() {
            isOpen = false;
            isGrabbing = false;
            isQuit = true;
            Name = "";
        }
        public override void Grab() {
            if (!isGrabbing) {
                isGrabbing = true;
            }
        }
        public override void Freeze() {
            if (isGrabbing) {
                isGrabbing = false;
                while (!isQuit && !isStopOk && m_frame != Max) {
                    Thread.Sleep(10);
                }
            }
        }
        public override int getMin() { return m_datas.Keys.Min(); }
        public override int getMax() { return m_datas.Keys.Max(); }

        //变量
        public bool isQuit = false;
        public bool isStopOk = false;
        
    }
}
