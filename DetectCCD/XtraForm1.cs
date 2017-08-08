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
     partial class XtraForm1 : DevExpress.XtraEditors.XtraForm {
        public XtraForm1(XMain parent) {
            InitializeComponent();

            Static.ParamApp.BindDataGridView(dataGridView1);
            Static.ParamShare.BindDataGridView(dataGridView2);
            Static.ParamInner.BindDataGridView(dataGridView3);
            Static.ParamOuter.BindDataGridView(dataGridView4);

            XM = parent;
        }

        public XMain XM;

        private void btnLoadFile1_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.ParamApp.CameraFileInner = ofd.FileName;
                Static.ParamApp.BindDataGridView(dataGridView1);
            }
        }

        private void btnLoadFile2_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                Static.ParamApp.CameraFileOuter = ofd.FileName;
                Static.ParamApp.BindDataGridView(dataGridView1);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void btnGrabRestart_Click(object sender, EventArgs e) {

        }

        private void trackSpeed_Scroll(object sender, EventArgs e) {

        }

        private void btnGrabStart_Click(object sender, EventArgs e) {
            XM.m_device.InnerCamera.Start();
            XM.m_device.OuterCamera.Start();
        }

        private void btnGrabStop_Click(object sender, EventArgs e) {
            XM.m_device.InnerCamera.Stop();
            XM.m_device.OuterCamera.Stop();
        }
    }
}