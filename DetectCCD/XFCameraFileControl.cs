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
        }

        ModRecord record;
        ModDevice device;

        private void trackFps_EditValueChanged(object sender, EventArgs e) {
            Static.ParamApp.CameraFpsControl = trackFps.Value / 10.0;
            device.InnerCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
            device.OuterCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
        }
        private void btnLoadInner_Click(object sender, EventArgs e) {
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

            device.InnerCamera.Stop();
            device.OuterCamera.Stop();

            device.InnerCamera.Reset();
            device.OuterCamera.Reset();

            record.InnerDetect.Discard();
            record.OuterDetect.Discard();

            record.InnerViewerImage.SetBottomTarget(0);
            record.InnerViewerImage.MoveTargetDirect();

            record.OuterViewerImage.SetBottomTarget(0);
            record.OuterViewerImage.MoveTargetDirect();

        }
        private void btnStart_Click(object sender, EventArgs e) {
            device.InnerCamera.Start();
            device.OuterCamera.Start();

            record.InnerViewerImage.SetUserEnable(false);
            record.OuterViewerImage.SetUserEnable(false);
        }
        private void btnStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
            device.OuterCamera.Stop();

            record.InnerViewerImage.SetUserEnable(true);
            record.OuterViewerImage.SetUserEnable(true);
        }

    }
}