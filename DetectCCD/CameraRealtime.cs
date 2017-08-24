using DALSA.SaperaLT.SapClassBasic;
using System;
using System.Diagnostics;

namespace DetectCCD {

    public class CameraRealtime : TemplateCamera, IDisposable {

        /// <summary> 连接GIGE相机 </summary>
        public CameraRealtime(int serverIndex2, int resourceIndex2, string configFile2 = "", int bufferCount = 2) {
            this._server2 = serverIndex2;
            this._resource2 = resourceIndex2;
            this._buffer_count = bufferCount;
            this._config2 = configFile2;

            this.isCameraLink = false;
            
            //
            if (!Open()) {
                throw new Exception("打开GIGE相机失败");
            }
        }

        /// <summary> 连接CameraLink相机 </summary>
        public CameraRealtime(int serverIndex1, int resourceIndex1, int serverIndex2, int resourceIndex2, string configFile1, string configFile2 = "", int bufferCount = 2) {
            this._server1 = serverIndex1;
            this._resource1 = resourceIndex1;
            this._server2 = serverIndex2;
            this._resource2 = resourceIndex2;
            this._buffer_count = bufferCount;
            this._config1 = configFile1;
            this._config2 = configFile2;

            this.isCameraLink = true;

            //
            if (!Open()) {
                throw new Exception("打开CameraLink相机失败");
            }
        }

        int _server1;
        int _server2;
        int _resource1;
        int _resource2;
        string _config1;
        string _config2;
        int _buffer_count;

        public void SaveConfig() {
            if (_config1 != "" && isCameraLink)
                Acq.SaveParameters(_config1);
            if (_config2 != "")
                AcqDevice.SaveFeatures(_config2);
        }

        public SapAcquisition Acq = null;
        public SapAcqDevice AcqDevice = null;
        public SapBuffer Buffers = null;
        public SapTransfer Xfer = null;
        public bool isCameraLink = false;

        Stopwatch fps_watch = new Stopwatch();
        int encoder_current { get { return Static.App.Is4K ? callGetEncoder() : (int)Buffers.CounterStamp; } }

        bool create_objects() {

            if (isCameraLink && Acq != null && !Acq.Initialized) {
                if (Acq.Create() == false) {
                    Dispose();
                    return false;
                }
            }
            if (AcqDevice != null && !AcqDevice.Initialized) {
                if (AcqDevice.Create() == false) {
                    Dispose();
                    return false;
                }
            }
            if (Buffers != null && !Buffers.Initialized) {
                if (Buffers.Create() == false) {
                    Dispose();
                    return false;
                }
                Buffers.Clear();
            }
            if (Xfer != null && Xfer.Pairs[0] != null) {
                Xfer.Pairs[0].Cycle = SapXferPair.CycleMode.NextWithTrash;
                if (Xfer.Pairs[0].Cycle != SapXferPair.CycleMode.NextWithTrash) {
                    Dispose();
                    return false;
                }
            }
            if (Xfer != null && !Xfer.Initialized) {
                if (Xfer.Create() == false) {
                    Dispose();
                    return false;
                }
            }
            return true;
        }
        private void Xfer_XferNotify(object sender, SapXferNotifyEventArgs e) {

            //Fix：通过编码器来判断是否为超时触发
            if (Math.Abs(m_encoder - encoder_current) < Buffers.Height - 200) {
                m_trash++;
                return;
            }
            
            //
            if (m_frame == 0) {

                //
                fps_watch.Restart();
                m_fpsRealtime = 1.0;

            }
            else {
                
                //
                m_fpsRealtime = 1000.0 / fps_watch.ElapsedMilliseconds;
                fps_watch.Restart();
                
            }

            //
            m_frame++;
            m_encoder = encoder_current;

            //
            DataGrab dg = new DataGrab() {
                Camera = Name,
                Frame = m_frame,
                Encoder = m_encoder,
                Timestamp = DataGrab.GenTimeStamp(DateTime.Now),
            };

            //
            int w = Buffers.Width;
            int h = Buffers.Height;
            IntPtr data;
            Buffers.GetAddress(out data);
            dg.Image = new HalconDotNet.HImage("byte", w, h, data);
            dg.IsCreated = true;

            //
            callImageReady(dg);
        }

        public bool Open(bool isRebuild = false) {

            if (isOpen && !isRebuild)
                return false;

            if (isRebuild) {
                if (Xfer != null && Xfer.Initialized)
                    Xfer.Destroy();
                if (Buffers != null && Buffers.Initialized)
                    Buffers.Destroy();
            }

            if (isCameraLink) {

                //CameraLink
                if (!isRebuild) {
                    Acq = new SapAcquisition(new SapLocation(_server1, _resource1), _config1);
                    AcqDevice = new SapAcqDevice(new SapLocation(_server2, _resource2), _config2);
                }
                Buffers = new SapBufferWithTrash(_buffer_count, Acq, SapBuffer.MemoryType.ScatterGather);
                Xfer = new SapAcqToBuf(Acq, Buffers);

            }
            else {

                //Gige
                if (!isRebuild) {
                    AcqDevice = new SapAcqDevice(new SapLocation(_server2, _resource2), _config2);
                }
                Buffers = new SapBufferWithTrash(_buffer_count, AcqDevice, SapBuffer.MemoryType.ScatterGather);
                Xfer = new SapAcqDeviceToBuf(AcqDevice, Buffers);

                //Fix: Gige Camera Timeout set.
                if (AcqDevice.Create()) {
                    AcqDevice.SetFeatureValue("ImageTimeout", 60.0);
                }
                else {
                    Dispose();
                    return false;
                }

            }

            //
            Xfer.Pairs[0].CounterStampTimeBase = SapXferPair.XferCounterStampTimeBase.ShaftEncoder;
            Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
            Xfer.XferNotify += Xfer_XferNotify;
            Xfer.XferNotifyContext = this;

            //创建对象
            if (!create_objects()) {
                Dispose();
                return false;
            }

            //
            AcqDevice.GetFeatureValue("DeviceUserID", out Name);

            //
            m_frame = m_frameStart;
            m_trash = 0;
            isOpen = true;
            return true;
        }

        public void Close() {

            try {
                if (Xfer != null && Xfer.Grabbing)
                    Xfer.Abort();

                if (Xfer != null && Xfer.Initialized)
                    Xfer.Destroy();
                if (Buffers != null && Buffers.Initialized)
                    Buffers.Destroy();
                if (AcqDevice != null && AcqDevice.Initialized)
                    AcqDevice.Destroy();
                if (Acq != null && Acq.Initialized)
                    Acq.Destroy();
            }
            catch { }

            isGrabbing = false;
            isOpen = false;

            Name = "";
        }
        public void Snap() {
            if (isOpen) {
                Xfer.Snap();
            }
        }

        public override void Dispose() {
            Close();
            if (Xfer != null) { Xfer.Dispose(); Xfer = null; }
            if (Buffers != null) { Buffers.Dispose(); Buffers = null; }
            if (AcqDevice != null) { AcqDevice.Dispose(); AcqDevice = null; }
            if (Acq != null) { Acq.Dispose(); Acq = null; }
        }
        public override void Reset() {
            if (!isGrabbing) {
                m_frame = m_frameStart;
            }
        }
        public override void Grab() {
            if (isOpen && !isGrabbing) {
                isGrabbing = Xfer.Grab();
            }
        }
        public override void Freeze() {
            if (isOpen) {
                isGrabbing = !Xfer.Freeze();
            }
        }

        //Param
        public bool ParamTriggerMode {
            get {
                int k;
                AcqDevice.GetFeatureValue("TriggerMode", out k);
                return k == 1;
            }
            set {
                int k = value ? 1 : 0;
                AcqDevice.SetFeatureValue("TriggerMode", k);
            }
        }

    }

}
