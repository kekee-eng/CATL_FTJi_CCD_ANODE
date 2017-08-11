using DetectCCD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDbViewer {
    public partial class Form1 : Form {
        public Form1(string path) {
            InitializeComponent();

            Static.Init();
            Record.Init();
            Record.Open(path);

            Record.InnerViewerImage.Init(hwinInner);
            Record.OuterViewerImage.Init(hwinOuter);

            Record.InnerViewerImage.SetCenterTarget(Record.InnerGrab.Min);
            Record.OuterViewerImage.SetCenterTarget(Record.OuterGrab.Min);

            Record.InnerViewerImage.MoveTargetDirect();
            Record.OuterViewerImage.MoveTargetDirect();

            splitContainer1.Panel1Collapsed = true;
        }

        ModRecord Record = new ModRecord();

        private void btnSaveInner_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.bmp|BMP文件";
            if (sfd.ShowDialog() == DialogResult.OK) {

                double i, j;
                if (double.TryParse(textInner1.Text, out i) &&
                    double.TryParse(textInner2.Text, out j)) {

                    Record.InnerGrab.GetImage(i, j).WriteImage("bmp", 0, sfd.FileName);
                }

            }
        }
        private void btnSaveOuter_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.bmp|BMP文件";
            if (sfd.ShowDialog() == DialogResult.OK) {

                double i, j;
                if (double.TryParse(textInner1.Text, out i) &&
                    double.TryParse(textInner2.Text, out j)) {

                    Record.OuterGrab.GetImage(i, j).WriteImage("bmp", 0, sfd.FileName);
                }

            }
        }
        private async void button2_Click(object sender, EventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) {

                this.Enabled = false;
                string path;

                path = fbd.SelectedPath + "/DB/Inner/";
                new System.IO.DirectoryInfo(path).Create();
                for (int i = Record.InnerGrab.Min; i <= Record.InnerGrab.Max; i++) {

                    await Task.Run(() => {
                        Record.InnerGrab.GetImage(i)?.WriteImage("bmp", 0, path + i);
                    });

                    this.Text = string.Format("Save Inner: {0}/{1}", i, Record.InnerGrab.Max);
                }

                path = fbd.SelectedPath + "/DB/Outer/";
                new System.IO.DirectoryInfo(path).Create();
                for (int i = Record.OuterGrab.Min; i <= Record.OuterGrab.Max; i++) {

                    await Task.Run(() => {
                        Record.OuterGrab.GetImage(i)?.WriteImage("bmp", 0, path + i);
                    });

                    this.Text = string.Format("Save Outer: {0}/{1}", i, Record.OuterGrab.Max);
                }


                this.Enabled = true;
            }
        }

    }
}
