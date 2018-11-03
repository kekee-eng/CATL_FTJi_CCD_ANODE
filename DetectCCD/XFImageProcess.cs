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
using HalconDotNet;

namespace DetectCCD {
    public partial class XFImageProcess : DevExpress.XtraEditors.XtraForm {
        public XFImageProcess() {
            InitializeComponent();

            display = new ViaImageDisplay();
            display.Dock = DockStyle.Fill;
            splitContainerControl1.Panel2.Controls.Add(display);
        }
        public ViaImageDisplay display;

        void detect(Action<HImage> act) {

            Log.Record(() => {

                Static.App.ImageProcessReload = true;
                using (var ofd = new OpenFileDialog()) {
                    if (ofd.ShowDialog() == DialogResult.OK) {

                        var image = new HImage(ofd.FileName);
                        display.SetImage(image);

                        act(image);
                    }
                }
            });
        }

        private void btnDetect_Click(object sender, EventArgs e) {

            detect(img => {

                int ww, hh;
                img.GetImageSize(out ww, out hh);

                double x, w;
                if (ImageProcess.DetectLineLeakMetal(img, out x, out w)) {
                    display.SetImage(img, g => {
                        g.SetDraw("margin");
                        g.SetColor("red");
                        g.DispRectangle1(0, x - w / 2, hh, x + w / 2);
                    });

                }
            });
        }

        private void btnDetectTab_Click(object sender, EventArgs e) {

            detect(img => {

                bool hasDefect, hasDefect1, hasDefect2, hasTab;
                double[] ax, ay1, ay2;
                if (ImageProcess.DetectTab(img, out hasDefect, out hasDefect1, out hasDefect2, out hasTab, out ax, out ay1, out ay2)) {
                    display.SetImage(img, g => {

                        if (hasTab) {

                            g.SetDraw("margin");
                            g.SetColor("green");

                            int ecc = ax.Length;
                            for (int i = 0; i < ecc; i++) {
                                int d = 50;
                                g.DispRectangle1(ay1[i] - d, ax[i] - d, ay2[i] + d, ax[i] + d);
                            }
                        }
                    });
                }

            });
        }
        private void btnDetectMark_Click(object sender, EventArgs e) {

            detect(img => {

                double[] bx, by;
                if (ImageProcess.DetectMark(img, out bx, out by)) {
                    display.SetImage(img, g => {

                        g.SetDraw("margin");
                        g.SetColor("green");

                        int ecc = bx.Length;
                        for (int i = 0; i < ecc; i++) {
                            int dx = 200;
                            int dy = 50;
                            g.DispRectangle1(by[i] - dy, bx[i] - dx, by[i] + dy, bx[i] + dx);
                        }
                    });
                }

            });
        }
        private void btnDetectDefect_Click(object sender, EventArgs e) {

            detect(img => {

                int w, h;
                img.GetImageSize(out w, out h);

                int[] etype;
                double[] ex, ey, ew, eh, earea;
                if (ImageProcess.DetectDefect(img, out etype, out ex, out ey, out ew, out eh, out earea)) {


                    display.SetImage(img, g => {
                        int ecc = new int[] { etype.Length, ex.Length, ey.Length, ew.Length, eh.Length }.Min();
                        for (int i = 0; i < ecc; i++) {

                            DataDefect defect = new DataDefect() {
                                EA = -1,
                                Type = etype[i],
                            };

                            //
                            g.SetColor("red");
                            g.SetDraw("margin");
                            g.SetLineWidth(1);

                            //
                            g.DispRectangle2(ey[i], ex[i], 0, ew[i] / 2, eh[i] / 2);

                            //
                            g.SetTposition(Convert.ToInt32(ey[i]), Convert.ToInt32(ex[i]));
                            g.WriteString($"[{etype[i]}]" + defect.GetTypeCaption());
                        }
                    });
                }
            });
        }
        private void btnDetectLineLeakMetal_Click(object sender, EventArgs e) {

            detect(img => {

                int w, h;
                img.GetImageSize(out w, out h);

                double fx, fw;
                if (ImageProcess.DetectLineLeakMetal(img, out fx, out fw)) {
                    display.SetImage(img, g => {
                        g.SetDraw("margin");
                        g.SetColor("red");
                        g.DispRectangle1(0, fx - fw / 2, h, fx + fw / 2);
                    });
                }

            });
        }

    }
}