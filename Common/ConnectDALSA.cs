using DALSA.SaperaLT.SapClassBasic;
using System;

namespace Common {

    public class ConnectDALSA {

        /// <summary> 连接GIGE相机 </summary>
        public ConnectDALSA(int serverIndex2, int resourceIndex2, int bufferCount, string configFile2 = "") {
            this._server2 = serverIndex2;
            this._resource2 = resourceIndex2;
            this._buffer_count = bufferCount;
            this._config2 = configFile2;

            this.m_isCameraLink = false;
        }

        /// <summary> 连接CameraLink相机 </summary>
        public ConnectDALSA(int serverIndex1, int resourceIndex1, int serverIndex2, int resourceIndex2, int bufferCount, string configFile1 = "", string configFile2 = "") {
            this._server1 = serverIndex1;
            this._resource1 = resourceIndex1;
            this._server2 = serverIndex2;
            this._resource2 = resourceIndex2;
            this._buffer_count = bufferCount;
            this._config1 = configFile1;
            this._config2 = configFile2;

            this.m_isCameraLink = true;
        }

        int _server1;
        int _server2;
        int _resource1;
        int _resource2;
        string _config1;
        string _config2;
        int _buffer_count;

        public void SaveConfig() {
            if (_config1 != "" && m_isCameraLink)
                Acq.SaveParameters(_config1);
            if (_config2 != "")
                AcqDevice.SaveFeatures(_config2);
        }

        public SapAcquisition Acq = null;
        public SapAcqDevice AcqDevice = null;
        public SapBuffer Buffers = null;
        public SapTransfer Xfer = null;

        public event SapXferNotifyHandler CallBack = null;

        public bool m_isCameraLink = false;
        public bool m_isOpen = false;
        public bool m_isGrab = false;

        public int m_count = 0;
        public double m_fps {
            get {
                if (m_isGrab)
                    return Xfer.FrameRateStatistics.LiveFrameRate;
                return -2;
            }
        }

        public string m_camera_name = "";

        bool create_objects() {

            if (m_isCameraLink && Acq != null && !Acq.Initialized) {
                if (Acq.Create() == false) {
                    Close();
                    return false;
                }
            }
            if (AcqDevice != null && !AcqDevice.Initialized) {
                if (AcqDevice.Create() == false) {
                    Close();
                    return false;
                }
            }
            if (Buffers != null && !Buffers.Initialized) {
                if (Buffers.Create() == false) {
                    Close();
                    return false;
                }
                Buffers.Clear();
            }
            if (Xfer != null && Xfer.Pairs[0] != null) {
                Xfer.Pairs[0].Cycle = SapXferPair.CycleMode.NextWithTrash;
                if (Xfer.Pairs[0].Cycle != SapXferPair.CycleMode.NextWithTrash) {
                    Close();
                    return false;
                }
            }
            if (Xfer != null && !Xfer.Initialized) {
                if (Xfer.Create() == false) {
                    Close();
                    return false;
                }
            }
            return true;
        }

        public bool Open(bool isRebuild = false) {

            if (m_isOpen && !isRebuild)
                return false;

            if (isRebuild) {
                if (Xfer != null && Xfer.Initialized)
                    Xfer.Destroy();
                if (Buffers != null && Buffers.Initialized)
                    Buffers.Destroy();
            }

            if (m_isCameraLink) {

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
                    Close();
                    return false;
                }

            }

            //
            Xfer.Pairs[0].CounterStampTimeBase = SapXferPair.XferCounterStampTimeBase.ShaftEncoder;
            Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
            Xfer.XferNotify += (sender, args) => {

                //TODO:
                long encoder = args.HostTimeStamp;

                m_count++;
                CallBack?.Invoke(sender, args);
            };
            Xfer.XferNotifyContext = this;

            //创建对象
            if (!create_objects()) {
                Dispose();
                return false;
            }

            //
            AcqDevice.GetFeatureValue("DeviceUserID", out m_camera_name);

            //
            m_count = 0;
            m_isOpen = true;
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

            m_isGrab = false;
            m_isOpen = false;

            m_camera_name = "";
        }
        public void Dispose() {
            Close();
            if (Xfer != null) { Xfer.Dispose(); Xfer = null; }
            if (Buffers != null) { Buffers.Dispose(); Buffers = null; }
            if (AcqDevice != null) { AcqDevice.Dispose(); AcqDevice = null; }
            if (Acq != null) { Acq.Dispose(); Acq = null; }
        }


        public void Snap() {
            if (m_isOpen) {
                Xfer.Snap();
            }
        }
        public void Grab() {
            if (m_isOpen && !m_isGrab) {
                m_isGrab = Xfer.Grab();
            }
        }
        public void Freeze() {
            if (m_isOpen) {
                Xfer.Freeze();
                m_isGrab = false;
            }
        }

        public HalconDotNet.HImage GetImage() {
            int w = Buffers.Width;
            int h = Buffers.Height;
            IntPtr data;
            Buffers.GetAddress(out data);
            return new HalconDotNet.HImage("byte", w, h, data);
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
