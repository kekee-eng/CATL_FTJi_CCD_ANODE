namespace Detect4K {
    partial class FormDetect4K {
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.btnGrabStop = new System.Windows.Forms.Button();
            this.btnGrabStart = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rtoolInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolTab = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolEA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolCfgApp = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgShare = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgInner = new System.Windows.Forms.ToolStripMenuItem();
            this.hwin = new HalconDotNet.HWindowControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnGrabRestart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rtoolDefect = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGrabStop
            // 
            this.btnGrabStop.Location = new System.Drawing.Point(246, 3);
            this.btnGrabStop.Name = "btnGrabStop";
            this.btnGrabStop.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStop.TabIndex = 4;
            this.btnGrabStop.Text = "Stop";
            this.btnGrabStop.UseVisualStyleBackColor = true;
            this.btnGrabStop.Click += new System.EventHandler(this.btnGrabStop_Click);
            // 
            // btnGrabStart
            // 
            this.btnGrabStart.Location = new System.Drawing.Point(165, 3);
            this.btnGrabStart.Name = "btnGrabStart";
            this.btnGrabStart.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStart.TabIndex = 3;
            this.btnGrabStart.Text = "Start";
            this.btnGrabStart.UseVisualStyleBackColor = true;
            this.btnGrabStart.Click += new System.EventHandler(this.btnGrabStart_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(483, 783);
            this.dataGridView1.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolInfo,
            this.toolStripSeparator1,
            this.rtoolTab,
            this.rtoolEA,
            this.rtoolDefect,
            this.toolStripSeparator2,
            this.rtoolCfgApp,
            this.rtoolCfgShare,
            this.rtoolCfgInner});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 192);
            // 
            // rtoolInfo
            // 
            this.rtoolInfo.Name = "rtoolInfo";
            this.rtoolInfo.Size = new System.Drawing.Size(152, 22);
            this.rtoolInfo.Text = "Info";
            this.rtoolInfo.Click += new System.EventHandler(this.rtoolInfo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // rtoolTab
            // 
            this.rtoolTab.Name = "rtoolTab";
            this.rtoolTab.Size = new System.Drawing.Size(152, 22);
            this.rtoolTab.Text = "TAB";
            this.rtoolTab.Click += new System.EventHandler(this.rtoolTab_Click);
            // 
            // rtoolEA
            // 
            this.rtoolEA.Name = "rtoolEA";
            this.rtoolEA.Size = new System.Drawing.Size(152, 22);
            this.rtoolEA.Text = "EA";
            this.rtoolEA.Click += new System.EventHandler(this.rtoolEA_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // rtoolCfgApp
            // 
            this.rtoolCfgApp.Name = "rtoolCfgApp";
            this.rtoolCfgApp.Size = new System.Drawing.Size(152, 22);
            this.rtoolCfgApp.Text = "CfgApp";
            this.rtoolCfgApp.Click += new System.EventHandler(this.rtoolCfgApp_Click);
            // 
            // rtoolCfgShare
            // 
            this.rtoolCfgShare.Name = "rtoolCfgShare";
            this.rtoolCfgShare.Size = new System.Drawing.Size(152, 22);
            this.rtoolCfgShare.Text = "CfgShare";
            this.rtoolCfgShare.Click += new System.EventHandler(this.rtoolCfgShare_Click);
            // 
            // rtoolCfgInner
            // 
            this.rtoolCfgInner.Name = "rtoolCfgInner";
            this.rtoolCfgInner.Size = new System.Drawing.Size(152, 22);
            this.rtoolCfgInner.Text = "CfgInner";
            this.rtoolCfgInner.Click += new System.EventHandler(this.rtoolCfgInner_Click);
            // 
            // hwin
            // 
            this.hwin.BackColor = System.Drawing.Color.Black;
            this.hwin.BorderColor = System.Drawing.Color.Black;
            this.hwin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwin.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwin.Location = new System.Drawing.Point(0, 0);
            this.hwin.Name = "hwin";
            this.hwin.Size = new System.Drawing.Size(563, 723);
            this.hwin.TabIndex = 7;
            this.hwin.WindowSize = new System.Drawing.Size(563, 723);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 783);
            this.splitContainer1.SplitterDistance = 483;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.hwin);
            this.splitContainer2.Size = new System.Drawing.Size(563, 783);
            this.splitContainer2.SplitterDistance = 56;
            this.splitContainer2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnLoadFile);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabRestart);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabStart);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabStop);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(563, 56);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(3, 3);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 7;
            this.btnLoadFile.Text = "LoadFile";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnGrabRestart
            // 
            this.btnGrabRestart.Location = new System.Drawing.Point(84, 3);
            this.btnGrabRestart.Name = "btnGrabRestart";
            this.btnGrabRestart.Size = new System.Drawing.Size(75, 23);
            this.btnGrabRestart.TabIndex = 6;
            this.btnGrabRestart.Text = "Restart";
            this.btnGrabRestart.UseVisualStyleBackColor = true;
            this.btnGrabRestart.Click += new System.EventHandler(this.btnGrabRestart_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rtoolDefect
            // 
            this.rtoolDefect.Name = "rtoolDefect";
            this.rtoolDefect.Size = new System.Drawing.Size(152, 22);
            this.rtoolDefect.Text = "Defect";
            this.rtoolDefect.Click += new System.EventHandler(this.rtoolDefect_Click);
            // 
            // FormDetect4K
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 783);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormDetect4K";
            this.Text = "Detect4K";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGrabStop;
        private System.Windows.Forms.Button btnGrabStart;
        private System.Windows.Forms.DataGridView dataGridView1;
        private HalconDotNet.HWindowControl hwin;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnGrabRestart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rtoolInfo;
        private System.Windows.Forms.ToolStripMenuItem rtoolTab;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem rtoolEA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem rtoolCfgApp;
        private System.Windows.Forms.ToolStripMenuItem rtoolCfgShare;
        private System.Windows.Forms.ToolStripMenuItem rtoolCfgInner;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.ToolStripMenuItem rtoolDefect;
    }
}

