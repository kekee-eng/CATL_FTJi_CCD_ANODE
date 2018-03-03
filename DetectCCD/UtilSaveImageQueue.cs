using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectCCD {
    public class UtilSaveImageQueue {

        class ImageObj {
            public ImageObj(HImage image, string path) {
                this.image = image;
                this.path = path;
            }

            public HImage image;
            public string path;
        }

        static ConcurrentQueue<ImageObj> m_queue = new ConcurrentQueue<ImageObj>();
        static void runThread(int i) {
            while (m_isRunning) {
                Thread.Sleep(10);

                //if (UtilPerformance.GetCpuLoad() > 60)
                //    continue;

                ImageObj dt;
                if (m_queue.TryDequeue(out dt)) {

                    //
                    string dir = Path.GetDirectoryName(dt.path);
                    if (!Directory.Exists(dir)) {
                        Directory.CreateDirectory(dir);
                    }

                    //
                    Log.Record(() => {
                        dt.image.WriteImage("bmp", 0, dt.path);
                    });
                    m_countOk++;

                    //
                    dt.image.Dispose();
                }
            }
        }

        public static void Put(HImage image, string path) {
            if (image == null || !image.IsInitialized())
                return;

            m_queue.Enqueue(new ImageObj(image, path));
        }
        public static bool Start(int threadNum = 1) {
            if (m_isRunning)
                return false;

            m_countThread = threadNum;
            m_isRunning = true;

            m_countOk = 0;
            for (int i = 0; i < threadNum; i++) {
                int p = i;
                new Thread(new ThreadStart(() => runThread(p))).Start();
            }

            return true;
        }
        public static void Stop() {
            m_isRunning = false;
        }

        public static bool m_isRunning = false;
        public static int m_countQueue {
            get {
                return m_queue.Count;
            }
        }
        public static int m_countOk;
        public static int m_countThread = 0;

        public static string GetInformaticon() {
            string info = string.Format(@"{1} [{0}]", m_countQueue, m_countOk);

            return info;
        }

    }
}