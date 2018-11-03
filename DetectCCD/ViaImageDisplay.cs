using HalconDotNet;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DetectCCD {

    /// <summary> 图像显示控件 </summary>
    public partial class ViaImageDisplay : UserControl {
        public ViaImageDisplay() {
            InitializeComponent();
        }

        /// <summary> 加载图像副本 </summary>
        public void SetImageClone(HImage image = null, Action<HWindow> fun = null) {

            //
            if (image == null || !image.IsInitialized())
                return;

            //
            bool reviewImage = _Image == null || !_Image.IsInitialized();
            if (!reviewImage) {
                int w1, h1;
                int w2, h2;
                image.GetImageSize(out w1, out h1);
                _Image.GetImageSize(out w2, out h2);
                if (w1 != w2 ||
                    w2 != h2) {
                    reviewImage = true;
                }
            }

            //
            _Image?.Dispose();
            _Image = image.Clone();
            _DrawFunc = fun;

            //
            if (reviewImage)
                ViewImage();
            else
                ViewUpdate();
        }

        /// <summary> 加载图像 </summary>
        public void SetImage(HImage image = null, Action<HWindow> fun = null) {

            //
            if (image == null || !image.IsInitialized())
                return;

            //
            bool reviewImage = _Image == null || !_Image.IsInitialized();
            if (!reviewImage) {
                int w1, h1;
                int w2, h2;
                image.GetImageSize(out w1, out h1);
                _Image.GetImageSize(out w2, out h2);
                if (w1 != w2 ||
                    w2 != h2) {
                    reviewImage = true;
                }
            }

            //
            _Image = image;
            _DrawFunc = fun;

            //
            if (reviewImage)
                ViewImage();
            else
                ViewUpdate();
        }

        /// <summary> 释放图像内存 </summary>
        public void ReleaseImage() {

            //
            _Image?.Dispose();

            //
            Disp(g => g.ClearWindow());

        }

        /// <summary> 显示图像 </summary>
        public void ViewImage() {
            if (isImageReady) {
                ViewTarget(imageWidth / 2, imageHeight / 2, imageWidth, imageHeight);
            }
        }

        /// <summary> 显示目标 </summary>
        public void ViewTarget(double x, double y, double w, double h) {

            //
            double sx = w / refImageWidth;
            double sy = h / refImageHeight;

            //
            double s = Math.Max(sx, sy) * 1.01;
            s = Math.Min(s, 10);
            s = Math.Max(s, 0.1);

            //
            ViewPos(x, y, s);

        }

        /// <summary> 显示目标 </summary>
        public void ViewPos(double x, double y, double s) {

            //
            viewx = x;
            viewy = y;
            views = s;

            //
            ViewUpdate();

        }

        /// <summary> 更新显示 </summary>
        public void ViewUpdate() {

            DetectCCD.Log.Quiesce(() => {

                if (!isDisplayReady)
                    return;

                if (!isImageReady) {
                    Disp(g => g.ClearWindow());
                    return;
                }

                Disp(g => {

                    //设置位置
                    double vw = views * refImageWidth;
                    double vh = views * refImageHeight;
                    double vx = viewx - vw / 2;
                    double vy = viewy - vh / 2;
                    hWindowControl1.ImagePart = new Rectangle(
                        Convert.ToInt32(vx),
                        Convert.ToInt32(vy),
                        Convert.ToInt32(vw),
                        Convert.ToInt32(vh));

                    //清空不显示区域
                    double bw = boxWidth;
                    double bh = boxHeight;
                    double gw = imageWidth;
                    double gh = imageHeight;

                    double x1 = bw * (0 - vx) / vw;
                    double x2 = bw * (gw - vx) / vw;
                    double y1 = bh * (0 - vy) / vh;
                    double y2 = bh * (gh - vy) / vh;

                    if (x1 > 0)
                        g.ClearRectangle(0, 0, bh, x1);
                    if (x2 < bw)
                        g.ClearRectangle(0, x2, bh, bw);
                    if (y1 > 0)
                        g.ClearRectangle(0, 0, y1, bw);
                    if (y2 < bh)
                        g.ClearRectangle(y2, 0, bh, bw);

                    //显示图像
                    g.DispImage(_Image);

                    //显示绘制结果
                    _DrawFunc?.Invoke(g);

                });
            });

        }

        /// <summary> 开启鼠标移动事件 </summary>
        public void SetMouseMove(bool isAllow) {
            mouseAllow = isAllow;
        }

        /// <summary> 开启右键事件 </summary>
        public void SetContextMenuStrip(bool isAllow) {
            this.hWindowControl1.ContextMenuStrip = isAllow ? contextMenuStrip1 : null;
        }

        /// <summary> 是否启用保存图像功能 </summary>
        public void SetSaveImageAllow(bool isAllow) {
            this.toolSaveImage.Enabled = isAllow;
        }

        //显示图像
        HImage _Image;
        Action<HWindow> _DrawFunc;

        //视点位置
        double viewx = 0;
        double viewy = 0;
        double views = 1;

        //图像与控件准备状态
        bool isDisplayReady { get { return hWindowControl1.IsHandleCreated && !hWindowControl1.IsDisposed; } }
        bool isImageReady { get { return _Image != null && _Image.IsInitialized(); } }

        //图像与控件大小
        double imageWidth {
            get {
                if (!isImageReady)
                    return 1;

                int w, h;
                _Image.GetImageSize(out w, out h);
                return w;
            }
        }
        double imageHeight {
            get {
                if (!isImageReady)
                    return 1;

                int w, h;
                _Image.GetImageSize(out w, out h);
                return h;
            }
        }

        double boxWidth { get { return hWindowControl1.Width; } }
        double boxHeight { get { return hWindowControl1.Height; } }

        double refImageWidth { get { return imageWidth; } }
        double refImageHeight { get { return imageWidth * boxHeight / boxWidth; } }

        double refBoxWidth { get { return boxWidth; } }
        double refBoxHeight { get { return boxWidth * imageHeight / imageWidth; } }

        //鼠标事件对象
        bool mouseAllow = true;
        bool mouseIsMove = false;
        int mouseBoxX = 0;
        int mouseBoxY = 0;
        double mouseViewX = 0;
        double mouseViewY = 0;

        //显示函数
        public void Disp(Action<HWindow> f) {
            if (isDisplayReady) {
                f(hWindowControl1.HalconWindow);
            }
        }

        //界面交互
        private void hWindowControl1_HInitWindow(object sender, EventArgs e) {
            DetectCCD.Log.Quiesce(() => {
                Disp(g => {
                    g.SetWindowParam("background_color", "black");
                    g.ClearWindow();
                });
                ViewUpdate();
            });
        }
        private void hWindowControl1_Resize(object sender, EventArgs e) {
            DetectCCD.Log.Quiesce(() => {
                ViewUpdate();
            });
        }
        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e) {
            DetectCCD.Log.Quiesce(() => {

                if (!isDisplayReady)
                    return;

                if (!isImageReady)
                    return;

                //
                double s1 = views;
                views *= (e.Delta < 0 ? 1.1 : 1 / 1.1);
                views = Math.Min(views, 10);
                views = Math.Max(views, 0.005);
                double s2 = views;

                //
                double px = hWindowControl1.ImagePart.X;
                double py = hWindowControl1.ImagePart.Y;
                double pw = hWindowControl1.ImagePart.Width;
                double ph = hWindowControl1.ImagePart.Height;

                viewx += ((e.X - px) / pw - 0.5) * (s1 - s2) * refImageWidth;
                viewy += ((e.Y - py) / ph - 0.5) * (s1 - s2) * refImageHeight;

                //
                ViewUpdate();
            });
        }
        private void hWindowControl1_MouseUp(object sender, MouseEventArgs e) {
            mouseIsMove = false;
        }
        private void hWindowControl1_MouseLeave(object sender, EventArgs e) {
            mouseIsMove = false;
        }
        private void hWindowControl1_MouseDown(object sender, MouseEventArgs e) {
            if (!mouseAllow) return;

            if (e.Button == MouseButtons.Left) {
                mouseViewX = viewx;
                mouseViewY = viewy;
                mouseBoxX = e.X;
                mouseBoxY = e.Y;
                mouseIsMove = true;
            }
        }
        private void hWindowControl1_MouseMove(object sender, MouseEventArgs e) {
            DetectCCD.Log.Quiesce(() => {
                if (!mouseAllow)
                    return;

                if (mouseIsMove) {
                    double dy = mouseBoxY - e.Y;
                    double dx = mouseBoxX - e.X;

                    if (Math.Abs(dx) > 3 || Math.Abs(dy) > 3) {
                        viewx = mouseViewX + dx / refBoxWidth * views * imageWidth;
                        viewy = mouseViewY + dy / refBoxHeight * views * imageHeight;

                        ViewUpdate();
                    }
                }
            });
        }

        private void toolViewTotal_Click(object sender, EventArgs e) {
            DetectCCD.Log.Quiesce(ViewImage);
        }
        private void toolSaveImage_Click(object sender, EventArgs e) {

            DetectCCD.Log.Record(() => {

                if (!isImageReady) {
                    MessageBox.Show("图片不存在");
                    return;
                }

                var img = _Image.Clone();
                var sfd = new SaveFileDialog();
                sfd.Filter = "BMP文件|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK) {
                    img.WriteImage("bmp", 0, sfd.FileName);
                    img.Dispose();
                }

            });
        }

    }
}
