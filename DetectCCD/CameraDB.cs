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

    public class CameraDB : TemplateCamera, IDisposable {

        public CameraDB(DataGrab.GrabDB db) {
            this.db = db;
            prepareFile();
            new Thread(new ThreadStart(threadProcess)).Start();

            m_frame = Static.App.CameraFrameStart;
            m_frameStart = Static.App.CameraFrameStart;
            m_fpsControl = Static.App.CameraFpsControl;
        }
        
        //
        DataGrab.GrabDB db;

        //
        async void prepareFile() {
            
            isOpen = false;
            await Task.Run(() => {
                
                if (db.QueryCount == 0)
                    throw new Exception("当前离线包中无数据！");

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
            var outdata = db[m_frame];
            outdata.IsCache = false;
            outdata.IsDetect = false;

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
        public override int getMin() { return db.Min; }
        public override int getMax() { return db.Max; }

        //变量
        public bool isQuit = false;
        public bool isStopOk = false;
        
    }
}
