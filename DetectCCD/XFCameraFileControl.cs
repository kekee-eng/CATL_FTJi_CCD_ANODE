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
    partial class XFCameraFileControl : DevExpress.XtraEditors.XtraForm {
        public XFCameraFileControl(ModDevice device, ModRecord record) {
            InitializeComponent();

            this.record = record;
            this.device = device;

            Static.ParamApp.BindTextBox(textFrameStart, "CameraFrameStart");
            trackFps.Value = (int)( Static.ParamApp.CameraFpsControl * 10);
        }

        ModRecord record;
        ModDevice device;

        private void trackFps_EditValueChanged(object sender, EventArgs e) {
            Static.ParamApp.CameraFpsControl = trackFps.Value / 10.0;
            device.InnerCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
            device.OuterCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
        }
        private void textFrameStart_EditValueChanged(object sender, EventArgs e) {
            int i;
            if(int.TryParse(textFrameStart.Text, out i)) {
                Static.ParamApp.CameraFrameStart = i;
            }
        }
        private void btnLoadFileInner_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.ParamApp.CameraFileInner = ofd.FileName;
            }
        }
        private void btnLoadFileOuter_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.ParamApp.CameraFileOuter = ofd.FileName;
            }
        }
        private void btnInit_Click(object sender, EventArgs e) {
            device.Dispose();
            device.Open();

            btnReset_Click(null, null);
        }

        private void btnReset_Click(object sender, EventArgs e) {

            device.InnerCamera.Freeze();
            device.OuterCamera.Freeze();

            device.InnerCamera.m_frameStart = Static.ParamApp.CameraFrameStart;
            device.OuterCamera.m_frameStart = Static.ParamApp.CameraFrameStart;

            device.InnerCamera.Reset();
            device.OuterCamera.Reset();

            record.InnerGrab.Cache.Dispose();
            record.OuterGrab.Cache.Dispose();

            record.InnerDetect.Dispose();
            record.OuterDetect.Dispose();

            record.InnerViewerImage.SetBottomTarget(0);
            record.InnerViewerImage.MoveTargetDirect();

            record.OuterViewerImage.SetBottomTarget(0);
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

            if (device.isReady) {
                btnLoadFileInner.Enabled = !device.isRun;
                btnLoadFileOuter.Enabled = !device.isRun;
                btnInit.Enabled = !device.isRun;

                btnReset.Enabled = !device.isRun;
                btnStart.Enabled = !device.isRun;
                btnStop.Enabled = device.isRun;

                textFrameStart.Enabled = !device.isRun;
            }

        }
    }
}