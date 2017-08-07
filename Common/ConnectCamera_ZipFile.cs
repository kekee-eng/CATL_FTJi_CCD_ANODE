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

namespace Common {

    public class ConnectCamera_ZipFile : IDisposable {

        public ConnectCamera_ZipFile(string filename) {
            m_filename = filename;
            prepareFile(filename);
            new Thread(new ThreadStart(threadProcess)).Start();
        }
        ~ConnectCamera_ZipFile() {
            if (m_zipfile != null) {
                m_zipfile.Dispose();
            }
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
                    Camera = m_camera_name,
                    Frame = v[7],
                    Encoder = 0,
                    Timestamp = DataGrab.GenTimeStamp(new DateTime(v[0], v[1], v[2], v[3], v[4], v[5], v[6] / 10)),

                };

                return data;
            };

            isReady = false;
            await Task.Run(() => {

                //
                m_camera_name = getCameraName(zipfilename);

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
                
                //
                isReady = true;

            });
        }
        DataGrab prepareImage() {

            //
            if (!isReady)
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
            }

            //
            outdata.IsCreated = true;
            return outdata;
        }
        void threadProcess() {

            Stopwatch watch = new Stopwatch();
            while (!isQuit) {
                Thread.Sleep(1);
                if (!isRun && watch.IsRunning) {
                    watch.Stop();
                    watch.Reset();
                }

                if (isRun) {

                    //
                    isStopOk = false;

                    //
                    if (!watch.IsRunning) {
                        watch.Stop();
                        watch.Reset();
                        watch.Start();
                        //callStart();
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
                    while (isRun && dg == null) {
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
                    } while (isRun && timeElapsedReal < timeElapsedRequire);

                    //计算实时帧率
                    m_fpsRealtime = 1000 / timeElapsedReal;

                    //回调
                    if (dg != null)
                        OnImageReady?.Invoke(dg);

                    if (m_frame == Max)
                        OnComplete?.Invoke();

                }
                else {
                    isStopOk = true;
                }
            }
        }

        //
        public void Reset() {
            if (!isRun) {
                m_frame = m_frameStart;
            }
        }
        public void Stop() {
            if (isRun) {
                isRun = false;
                while (!isQuit && !isStopOk && m_frame != Max) {
                    Thread.Sleep(10);
                }
            }
        }
        public void Start() {
            if (!isRun) {
                isRun = true;
            }
        }
        public string GetTitle() {
            return string.Format("{0} [离线][{1}-{2}] [{3}]", m_camera_name, Min, Max, m_frame);
        }

        public virtual void Dispose() {
            isRun = false;
            isQuit = true;
            m_camera_name = "";
        }

        //事件
        public event Action<DataGrab> OnImageReady = null;
        public event Action OnComplete = null;

        //变量
        public bool isQuit = false;
        public bool isReady = false;
        public bool isRun = false;
        public bool isStopOk = false;

        public int Min { get { return m_datas?.Count > 0 ? m_datas.Keys.Min() : 0; } }
        public int Max { get { return m_datas?.Count > 0 ? m_datas.Keys.Max() : 0; } }

        public int m_frame = 0;
        public int m_frameStart = 0;
        public double m_fpsControl = 1.0;
        public double m_fpsRealtime = 0.0;

        //
        public string m_camera_name = "";

    }
}
