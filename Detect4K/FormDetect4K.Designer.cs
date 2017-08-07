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
            this.rtoolDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDebugClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolTab = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolEA = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDefect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolCfgApp = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgShare = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgInner = new System.Windows.Forms.ToolStripMenuItem();
            this.hwin = new HalconDotNet.HWindowControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkReplay = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFrameCurrent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnGrabRestart = new System.Windows.Forms.Button();
            this.trackSpeed = new System.Windows.Forms.TrackBar();
            this.tbFrameStart = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).BeginInit();
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
            this.dataGridView1.Size = new System.Drawing.Size(483, 561);
            this.dataGridView1.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolInfo,
            this.rtoolDebug,
            this.toolStripSeparator1,
            this.rtoolTab,
            this.rtoolEA,
            this.rtoolDefect,
            this.toolStripSeparator2,
            this.rtoolCfgApp,
            this.rtoolCfgShare,
            this.rtoolCfgInner});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 192);
            // 
            // rtoolInfo
            // 
            this.rtoolInfo.Name = "rtoolInfo";
            this.rtoolInfo.Size = new System.Drawing.Size(129, 22);
            this.rtoolInfo.Text = "Info";
            this.rtoolInfo.Click += new System.EventHandler(this.rtoolInfo_Click);
            // 
            // rtoolDebug
            // 
            this.rtoolDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolDebugClear});
            this.rtoolDebug.Name = "rtoolDebug";
            this.rtoolDebug.Size = new System.Drawing.Size(129, 22);
            this.rtoolDebug.Text = "Debug";
            this.rtoolDebug.Click += new System.EventHandler(this.rtoolDebug_Click);
            // 
            // rtoolDebugClear
            // 
            this.rtoolDebugClear.Name = "rtoolDebugClear";
            this.rtoolDebugClear.Size = new System.Drawing.Size(106, 22);
            this.rtoolDebugClear.Text = "Clear";
            this.rtoolDebugClear.Click += new System.EventHandler(this.rtoolDebugClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // rtoolTab
            // 
            this.rtoolTab.Name = "rtoolTab";
            this.rtoolTab.Size = new System.Drawing.Size(129, 22);
            this.rtoolTab.Text = "TAB";
            this.rtoolTab.Click += new System.EventHandler(this.rtoolTab_Click);
            // 
            // rtoolEA
            // 
            this.rtoolEA.Name = "rtoolEA";
            this.rtoolEA.Size = new System.Drawing.Size(129, 22);
            this.rtoolEA.Text = "EA";
            this.rtoolEA.Click += new System.EventHandler(this.rtoolEA_Click);
            // 
            // rtoolDefect
            // 
            this.rtoolDefect.Name = "rtoolDefect";
            this.rtoolDefect.Size = new System.Drawing.Size(129, 22);
            this.rtoolDefect.Text = "Defect";
            this.rtoolDefect.Click += new System.EventHandler(this.rtoolDefect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(126, 6);
            // 
            // rtoolCfgApp
            // 
            this.rtoolCfgApp.Name = "rtoolCfgApp";
            this.rtoolCfgApp.Size = new System.Drawing.Size(129, 22);
            this.rtoolCfgApp.Text = "CfgApp";
            this.rtoolCfgApp.Click += new System.EventHandler(this.rtoolCfgApp_Click);
            // 
            // rtoolCfgShare
            // 
            this.rtoolCfgShare.Name = "rtoolCfgShare";
            this.rtoolCfgShare.Size = new System.Drawing.Size(129, 22);
            this.rtoolCfgShare.Text = "CfgShare";
            this.rtoolCfgShare.Click += new System.EventHandler(this.rtoolCfgShare_Click);
            // 
            // rtoolCfgInner
            // 
            this.rtoolCfgInner.Name = "rtoolCfgInner";
            this.rtoolCfgInner.Size = new System.Drawing.Size(129, 22);
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
            this.hwin.Size = new System.Drawing.Size(563, 452);
            this.hwin.TabIndex = 7;
            this.hwin.WindowSize = new System.Drawing.Size(563, 452);
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
            this.splitContainer1.Size = new System.Drawing.Size(1050, 561);
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
            this.splitContainer2.Panel1.Controls.Add(this.checkReplay);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.tbFrameCurrent);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer2.Panel1.Controls.Add(this.trackSpeed);
            this.splitContainer2.Panel1.Controls.Add(this.tbFrameStart);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.hwin);
            this.splitContainer2.Size = new System.Drawing.Size(563, 561);
            this.splitContainer2.SplitterDistance = 105;
            this.splitContainer2.TabIndex = 0;
            // 
            // checkReplay
            // 
            this.checkReplay.AutoSize = true;
            this.checkReplay.Location = new System.Drawing.Point(411, 71);
            this.checkReplay.Name = "checkReplay";
            this.checkReplay.Size = new System.Drawing.Size(120, 16);
            this.checkReplay.TabIndex = 15;
            this.checkReplay.Text = "完成后，重头播放";
            this.checkReplay.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(364, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "当前帧";
            // 
            // tbFrameCurrent
            // 
            this.tbFrameCurrent.Enabled = false;
            this.tbFrameCurrent.Location = new System.Drawing.Point(411, 44);
            this.tbFrameCurrent.Name = "tbFrameCurrent";
            this.tbFrameCurrent.Size = new System.Drawing.Size(140, 21);
            this.tbFrameCurrent.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "起始帧";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "速度";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnLoadFile);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabRestart);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabStart);
            this.flowLayoutPanel1.Controls.Add(this.btnGrabStop);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 53);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(339, 34);
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
            // trackSpeed
            // 
            this.trackSpeed.Location = new System.Drawing.Point(60, 12);
            this.trackSpeed.Maximum = 300;
            this.trackSpeed.Minimum = 3;
            this.trackSpeed.Name = "trackSpeed";
            this.trackSpeed.Size = new System.Drawing.Size(236, 45);
            this.trackSpeed.TabIndex = 10;
            this.trackSpeed.TickFrequency = 10;
            this.trackSpeed.Value = 3;
            this.trackSpeed.Scroll += new System.EventHandler(this.trackSpeed_Scroll);
            // 
            // tbFrameStart
            // 
            this.tbFrameStart.Location = new System.Drawing.Point(411, 17);
            this.tbFrameStart.Name = "tbFrameStart";
            this.tbFrameStart.Size = new System.Drawing.Size(140, 21);
            this.tbFrameStart.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormDetect4K
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 561);
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
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).EndInit();
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
        private System.Windows.Forms.TextBox tbFrameStart;
        private System.Windows.Forms.TrackBar trackSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFrameCurrent;
        private System.Windows.Forms.CheckBox checkReplay;
        private System.Windows.Forms.ToolStripMenuItem rtoolDebug;
        private System.Windows.Forms.ToolStripMenuItem rtoolDebugClear;
    }
}

