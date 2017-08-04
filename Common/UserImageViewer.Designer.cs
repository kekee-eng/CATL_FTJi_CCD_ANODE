namespace Common {
    partial class UserImageViewer {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rtoolReset = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolInfo_store = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolInfo_image = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolInfo_viewx = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolInfo_viewy = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolInfo_views = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolInfo_fps_image = new System.Windows.Forms.ToolStripMenuItem();
            this.textFpsImageControl = new System.Windows.Forms.ToolStripTextBox();
            this.rtoolInfo_fps_view = new System.Windows.Forms.ToolStripMenuItem();
            this.textFpsViewControl = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolInfo_remain = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolInfo_step = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolLocationMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolLocationFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.textLocationFrame = new System.Windows.Forms.ToolStripTextBox();
            this.rtoolLocationEA = new System.Windows.Forms.ToolStripMenuItem();
            this.textLocationEA = new System.Windows.Forms.ToolStripTextBox();
            this.textLocationER = new System.Windows.Forms.ToolStripTextBox();
            this.rtoolShowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolShow_EA = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolShow_ER = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolShow_Width = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolShow_NG = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolShow_Mark = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDetectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDetect_Point2Point = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ContextMenuStrip = this.contextMenuStrip1;
            this.hWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(500, 500);
            this.hWindowControl.TabIndex = 7;
            this.hWindowControl.WindowSize = new System.Drawing.Size(500, 500);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolReset,
            this.rtoolInfoMenu,
            this.rtoolLocationMenu,
            this.rtoolShowMenu,
            this.rtoolDetectMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 114);
            // 
            // rtoolReset
            // 
            this.rtoolReset.Name = "rtoolReset";
            this.rtoolReset.Size = new System.Drawing.Size(124, 22);
            this.rtoolReset.Text = "复位视图";
            // 
            // rtoolInfoMenu
            // 
            this.rtoolInfoMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolTrack,
            this.toolStripSeparator4,
            this.rtoolInfo_store,
            this.rtoolInfo_image,
            this.toolStripSeparator1,
            this.rtoolInfo_viewx,
            this.rtoolInfo_viewy,
            this.rtoolInfo_views,
            this.toolStripSeparator2,
            this.rtoolInfo_fps_image,
            this.rtoolInfo_fps_view,
            this.toolStripSeparator3,
            this.rtoolInfo_remain,
            this.rtoolInfo_step});
            this.rtoolInfoMenu.Name = "rtoolInfoMenu";
            this.rtoolInfoMenu.Size = new System.Drawing.Size(124, 22);
            this.rtoolInfoMenu.Text = "查看信息";
            // 
            // rtoolTrack
            // 
            this.rtoolTrack.Name = "rtoolTrack";
            this.rtoolTrack.Size = new System.Drawing.Size(133, 22);
            this.rtoolTrack.Text = "Track";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(130, 6);
            // 
            // rtoolInfo_store
            // 
            this.rtoolInfo_store.Name = "rtoolInfo_store";
            this.rtoolInfo_store.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_store.Text = "Store";
            // 
            // rtoolInfo_image
            // 
            this.rtoolInfo_image.Name = "rtoolInfo_image";
            this.rtoolInfo_image.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_image.Text = "Image";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // rtoolInfo_viewx
            // 
            this.rtoolInfo_viewx.Name = "rtoolInfo_viewx";
            this.rtoolInfo_viewx.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_viewx.Text = "ViewX";
            // 
            // rtoolInfo_viewy
            // 
            this.rtoolInfo_viewy.Name = "rtoolInfo_viewy";
            this.rtoolInfo_viewy.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_viewy.Text = "ViewY";
            // 
            // rtoolInfo_views
            // 
            this.rtoolInfo_views.Name = "rtoolInfo_views";
            this.rtoolInfo_views.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_views.Text = "ViewS";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // rtoolInfo_fps_image
            // 
            this.rtoolInfo_fps_image.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textFpsImageControl});
            this.rtoolInfo_fps_image.Name = "rtoolInfo_fps_image";
            this.rtoolInfo_fps_image.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_fps_image.Text = "FpsImage";
            // 
            // textFpsImageControl
            // 
            this.textFpsImageControl.Name = "textFpsImageControl";
            this.textFpsImageControl.Size = new System.Drawing.Size(100, 23);
            // 
            // rtoolInfo_fps_view
            // 
            this.rtoolInfo_fps_view.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textFpsViewControl});
            this.rtoolInfo_fps_view.Name = "rtoolInfo_fps_view";
            this.rtoolInfo_fps_view.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_fps_view.Text = "FpsView";
            // 
            // textFpsViewControl
            // 
            this.textFpsViewControl.Name = "textFpsViewControl";
            this.textFpsViewControl.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // rtoolInfo_remain
            // 
            this.rtoolInfo_remain.Name = "rtoolInfo_remain";
            this.rtoolInfo_remain.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_remain.Text = "Remain";
            // 
            // rtoolInfo_step
            // 
            this.rtoolInfo_step.Name = "rtoolInfo_step";
            this.rtoolInfo_step.Size = new System.Drawing.Size(133, 22);
            this.rtoolInfo_step.Text = "Step";
            // 
            // rtoolLocationMenu
            // 
            this.rtoolLocationMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolLocationFrame,
            this.rtoolLocationEA});
            this.rtoolLocationMenu.Name = "rtoolLocationMenu";
            this.rtoolLocationMenu.Size = new System.Drawing.Size(124, 22);
            this.rtoolLocationMenu.Text = "定位到";
            // 
            // rtoolLocationFrame
            // 
            this.rtoolLocationFrame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textLocationFrame});
            this.rtoolLocationFrame.Name = "rtoolLocationFrame";
            this.rtoolLocationFrame.Size = new System.Drawing.Size(112, 22);
            this.rtoolLocationFrame.Text = "帧位置";
            // 
            // textLocationFrame
            // 
            this.textLocationFrame.Name = "textLocationFrame";
            this.textLocationFrame.Size = new System.Drawing.Size(100, 23);
            // 
            // rtoolLocationEA
            // 
            this.rtoolLocationEA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textLocationEA,
            this.textLocationER});
            this.rtoolLocationEA.Name = "rtoolLocationEA";
            this.rtoolLocationEA.Size = new System.Drawing.Size(112, 22);
            this.rtoolLocationEA.Text = "EA";
            // 
            // textLocationEA
            // 
            this.textLocationEA.Name = "textLocationEA";
            this.textLocationEA.Size = new System.Drawing.Size(100, 23);
            // 
            // textLocationER
            // 
            this.textLocationER.Name = "textLocationER";
            this.textLocationER.Size = new System.Drawing.Size(100, 23);
            // 
            // rtoolShowMenu
            // 
            this.rtoolShowMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolShow_EA,
            this.rtoolShow_ER,
            this.rtoolShow_Width,
            this.rtoolShow_NG,
            this.rtoolShow_Mark});
            this.rtoolShowMenu.Name = "rtoolShowMenu";
            this.rtoolShowMenu.Size = new System.Drawing.Size(124, 22);
            this.rtoolShowMenu.Text = "标记项";
            // 
            // rtoolShow_EA
            // 
            this.rtoolShow_EA.Checked = true;
            this.rtoolShow_EA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rtoolShow_EA.Name = "rtoolShow_EA";
            this.rtoolShow_EA.Size = new System.Drawing.Size(127, 22);
            this.rtoolShow_EA.Text = "EA分割线";
            // 
            // rtoolShow_ER
            // 
            this.rtoolShow_ER.Checked = true;
            this.rtoolShow_ER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rtoolShow_ER.Name = "rtoolShow_ER";
            this.rtoolShow_ER.Size = new System.Drawing.Size(127, 22);
            this.rtoolShow_ER.Text = "极耳";
            // 
            // rtoolShow_Width
            // 
            this.rtoolShow_Width.Checked = true;
            this.rtoolShow_Width.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rtoolShow_Width.Name = "rtoolShow_Width";
            this.rtoolShow_Width.Size = new System.Drawing.Size(127, 22);
            this.rtoolShow_Width.Text = "测宽";
            // 
            // rtoolShow_NG
            // 
            this.rtoolShow_NG.Checked = true;
            this.rtoolShow_NG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rtoolShow_NG.Name = "rtoolShow_NG";
            this.rtoolShow_NG.Size = new System.Drawing.Size(127, 22);
            this.rtoolShow_NG.Text = "瑕疵";
            // 
            // rtoolShow_Mark
            // 
            this.rtoolShow_Mark.Checked = true;
            this.rtoolShow_Mark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rtoolShow_Mark.Name = "rtoolShow_Mark";
            this.rtoolShow_Mark.Size = new System.Drawing.Size(127, 22);
            this.rtoolShow_Mark.Text = "打标位置";
            // 
            // rtoolDetectMenu
            // 
            this.rtoolDetectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolDetect_Point2Point});
            this.rtoolDetectMenu.Name = "rtoolDetectMenu";
            this.rtoolDetectMenu.Size = new System.Drawing.Size(124, 22);
            this.rtoolDetectMenu.Text = "量测工具";
            // 
            // rtoolDetect_Point2Point
            // 
            this.rtoolDetect_Point2Point.Name = "rtoolDetect_Point2Point";
            this.rtoolDetect_Point2Point.Size = new System.Drawing.Size(112, 22);
            this.rtoolDetect_Point2Point.Text = "点到点";
            // 
            // UserImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWindowControl);
            this.Name = "UserImageViewer";
            this.Size = new System.Drawing.Size(500, 500);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rtoolReset;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfoMenu;
        private System.Windows.Forms.ToolStripMenuItem rtoolTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_store;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_image;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_viewx;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_viewy;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_views;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_fps_image;
        private System.Windows.Forms.ToolStripTextBox textFpsImageControl;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_fps_view;
        private System.Windows.Forms.ToolStripTextBox textFpsViewControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_remain;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo_step;
        private System.Windows.Forms.ToolStripMenuItem rtoolLocationMenu;
        private System.Windows.Forms.ToolStripMenuItem rtoolLocationFrame;
        private System.Windows.Forms.ToolStripTextBox textLocationFrame;
        private System.Windows.Forms.ToolStripMenuItem rtoolLocationEA;
        private System.Windows.Forms.ToolStripTextBox textLocationEA;
        private System.Windows.Forms.ToolStripTextBox textLocationER;
        private System.Windows.Forms.ToolStripMenuItem rtoolShowMenu;
        private System.Windows.Forms.ToolStripMenuItem rtoolShow_EA;
        private System.Windows.Forms.ToolStripMenuItem rtoolShow_ER;
        private System.Windows.Forms.ToolStripMenuItem rtoolShow_Width;
        private System.Windows.Forms.ToolStripMenuItem rtoolShow_NG;
        private System.Windows.Forms.ToolStripMenuItem rtoolShow_Mark;
        private System.Windows.Forms.ToolStripMenuItem rtoolDetectMenu;
        private System.Windows.Forms.ToolStripMenuItem rtoolDetect_Point2Point;
    }
}
