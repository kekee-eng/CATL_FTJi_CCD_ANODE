using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DetectCCD {
    partial class XFCameraControl : DevExpress.XtraEditors.XtraForm {
        public XFCameraControl(ModDevice device, ModRecord record) {
            InitializeComponent();

            this.record = record;
            this.device = device;

            Static.App.BindTextBox(textFrameStart, "CameraFrameStart");
            trackFps.Value = (int)( Static.App.CameraFpsControl * 10);
        }

        ModRecord record;
        ModDevice device;

        private void trackFps_EditValueChanged(object sender, EventArgs e) {
            if (device.isOpen) {
                Static.App.CameraFpsControl = trackFps.Value / 10.0;
                device.InnerCamera.m_fpsControl = Static.App.CameraFpsControl;
                device.OuterCamera.m_fpsControl = Static.App.CameraFpsControl;
            }
        }
        private void textFrameStart_EditValueChanged(object sender, EventArgs e) {
            int i;
            if(int.TryParse(textFrameStart.Text, out i)) {
                Static.App.CameraFrameStart = i;
            }
        }
        private void btnLoadFileInner_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.App.CameraFileInner = ofd.FileName;
            }
        }
        private void btnLoadFileOuter_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.App.CameraFileOuter = ofd.FileName;
            }
        }
        private void btnInit_Click(object sender, EventArgs e) {
            device.Dispose();
            device.Open();
            
            device.InnerCamera.Freeze();
            device.OuterCamera.Freeze();

            device.InnerCamera.Reset();
            device.OuterCamera.Reset();

            record.InnerGrab.Cache.Dispose();
            record.OuterGrab.Cache.Dispose();

            record.InnerDetect.Dispose();
            record.OuterDetect.Dispose();

            record.InnerViewerImage.SetBottomTarget(device.InnerCamera.m_frame);
            record.InnerViewerImage.MoveTargetDirect();

            record.OuterViewerImage.SetBottomTarget(device.InnerCamera.m_frame);
            record.OuterViewerImage.MoveTargetDirect();

        }
        private void btnStart_Click(object sender, EventArgs e) {
            device.InnerCamera.Grab();
            device.OuterCamera.Grab();

            record.InnerViewerImage.SetUserEnable(false);
            record.OuterViewerImage.SetUserEnable(false);
        }
        private void btnStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Freeze();
            device.OuterCamera.Freeze();

            record.InnerViewerImage.SetUserEnable(true);
            record.OuterViewerImage.SetUserEnable(true);
        }

        private void timer1_Tick(object sender, EventArgs e) {

            if (device.isOpen) {
                btnLoadFileInner.Enabled = !device.isGrabbing;
                btnLoadFileOuter.Enabled = !device.isGrabbing;
                btnInit.Enabled = !device.isGrabbing;
                
                btnStart.Enabled = !device.isGrabbing;
                btnStop.Enabled = device.isGrabbing;

                textFrameStart.Enabled = !device.isGrabbing;
            }

        }
    }
}