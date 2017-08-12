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
        public XFCameraControl(XMain main) {
            InitializeComponent();

            this.main = main;

            Static.App.BindTextBox(textFrameStart, "CameraFrameStart");
            trackFps.Value = (int)(Static.App.CameraFpsControl * 10);
        }

        XMain main;
        
        private void trackFps_EditValueChanged(object sender, EventArgs e) {
            if (main.device.isOpen) {
                Static.App.CameraFpsControl = trackFps.Value / 10.0;
                main.device.InnerCamera.m_fpsControl = Static.App.CameraFpsControl;
                main.device.OuterCamera.m_fpsControl = Static.App.CameraFpsControl;
            }
        }
        private void textFrameStart_EditValueChanged(object sender, EventArgs e) {
            int i;
            if (int.TryParse(textFrameStart.Text, out i)) {
                Static.App.CameraFrameStart = i;
            }
        }
        private void btnLoadFileInner_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.App.CameraFileInner = ofd.FileName;
            }

            main.textMode.SelectedIndex = 0;
            Static.App.RunningMode = 0;
            Static.App.CameraByRealtime = false;
            Static.App.CameraByZipFile = true;
        }
        private void btnLoadFileOuter_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.App.CameraFileOuter = ofd.FileName;
            }

            main.textMode.SelectedIndex = 0;
            Static.App.RunningMode = 0;
            Static.App.CameraByRealtime = false;
            Static.App.CameraByZipFile = true;
        }
        private void btnInit_Click(object sender, EventArgs e) {
            main.DeviceInit(true);
        }
        private void btnStart_Click(object sender, EventArgs e) {
            main.DeviceStartGrab();
        }
        private void btnStop_Click(object sender, EventArgs e) {
            main.DeviceStopGrab();
        }
        private void btnLoadDb_Click(object sender, EventArgs e) {
            main.textMode.SelectedIndex = 1;
            Static.App.CameraByRealtime = false;
            Static.App.CameraByZipFile = true;
            main.DeviceLoad();
        }

        private void timer1_Tick(object sender, EventArgs e) {

            if (main.device.isOpen) {
                btnLoadFileInner.Enabled = !main.device.isGrabbing;
                btnLoadFileOuter.Enabled = !main.device.isGrabbing;
                btnLoadDb.Enabled = !main.isOnline && !main.device.isGrabbing;

                btnInit.Enabled = !main.device.isGrabbing;

                if (main.isOnline) {
                    btnStart.Enabled = !main.device.isGrabbing;
                    btnStop.Enabled = main.device.isGrabbing;
                }
                else {
                    btnStart.Enabled = main.isClear && !main.device.isGrabbing;
                    btnStop.Enabled = main.isClear && main.device.isGrabbing;
                }
                textFrameStart.Enabled = !main.device.isGrabbing;
            }

        }

    }
}