namespace DetectCCD {
    partial class ViaImageDisplay {
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolViewTotal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(300, 300);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(300, 300);
            this.hWindowControl1.HInitWindow += new HalconDotNet.HInitWindowEventHandler(this.hWindowControl1_HInitWindow);
            this.hWindowControl1.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseWheel);
            this.hWindowControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hWindowControl1_MouseDown);
            this.hWindowControl1.MouseLeave += new System.EventHandler(this.hWindowControl1_MouseLeave);
            this.hWindowControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hWindowControl1_MouseMove);
            this.hWindowControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hWindowControl1_MouseUp);
            this.hWindowControl1.Resize += new System.EventHandler(this.hWindowControl1_Resize);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolViewTotal,
            this.toolSaveImage});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // toolViewTotal
            // 
            this.toolViewTotal.Name = "toolViewTotal";
            this.toolViewTotal.Size = new System.Drawing.Size(124, 22);
            this.toolViewTotal.Text = "显示全部";
            this.toolViewTotal.Click += new System.EventHandler(this.toolViewTotal_Click);
            // 
            // toolSaveImage
            // 
            this.toolSaveImage.Name = "toolSaveImage";
            this.toolSaveImage.Size = new System.Drawing.Size(124, 22);
            this.toolSaveImage.Text = "保存图片";
            this.toolSaveImage.Click += new System.EventHandler(this.toolSaveImage_Click);
            // 
            // ViaImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWindowControl1);
            this.Name = "ViaImageDisplay";
            this.Size = new System.Drawing.Size(300, 300);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolViewTotal;
        private System.Windows.Forms.ToolStripMenuItem toolSaveImage;
    }
}
