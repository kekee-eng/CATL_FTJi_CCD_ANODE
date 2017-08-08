namespace DetectCCD {
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rtoolInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDebugClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolCfgApp = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgShare = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolCfgInner = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolTab = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolEA = new System.Windows.Forms.ToolStripMenuItem();
            this.rtoolDefect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rtoolTABChart = new System.Windows.Forms.ToolStripMenuItem();
            this.widthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distanceDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForViewer = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtoolInfo,
            this.toolStripSeparator3,
            this.rtoolDebug,
            this.toolStripSeparator1,
            this.rtoolCfgApp,
            this.rtoolCfgShare,
            this.rtoolCfgInner,
            this.toolStripSeparator2,
            this.rtoolTab,
            this.rtoolEA,
            this.rtoolDefect,
            this.toolStripSeparator4,
            this.rtoolTABChart});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 226);
            // 
            // rtoolInfo
            // 
            this.rtoolInfo.Name = "rtoolInfo";
            this.rtoolInfo.Size = new System.Drawing.Size(129, 22);
            this.rtoolInfo.Text = "Info";
            this.rtoolInfo.Click += new System.EventHandler(this.rtoolInfo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(126, 6);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(126, 6);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
            // 
            // rtoolTABChart
            // 
            this.rtoolTABChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.widthToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.distanceToolStripMenuItem,
            this.distanceDiffToolStripMenuItem});
            this.rtoolTABChart.Name = "rtoolTABChart";
            this.rtoolTABChart.Size = new System.Drawing.Size(129, 22);
            this.rtoolTABChart.Text = "TAB";
            this.rtoolTABChart.Click += new System.EventHandler(this.rtoolTABChart_Click);
            // 
            // widthToolStripMenuItem
            // 
            this.widthToolStripMenuItem.Name = "widthToolStripMenuItem";
            this.widthToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.widthToolStripMenuItem.Text = "Width";
            this.widthToolStripMenuItem.Click += new System.EventHandler(this.widthToolStripMenuItem_Click);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.sizeToolStripMenuItem.Text = "Height";
            this.sizeToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
            // 
            // distanceToolStripMenuItem
            // 
            this.distanceToolStripMenuItem.Name = "distanceToolStripMenuItem";
            this.distanceToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.distanceToolStripMenuItem.Text = "Distance";
            this.distanceToolStripMenuItem.Click += new System.EventHandler(this.distanceToolStripMenuItem_Click);
            // 
            // distanceDiffToolStripMenuItem
            // 
            this.distanceDiffToolStripMenuItem.Name = "distanceDiffToolStripMenuItem";
            this.distanceDiffToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.distanceDiffToolStripMenuItem.Text = "DistanceDiff";
            this.distanceDiffToolStripMenuItem.Click += new System.EventHandler(this.distanceDiffToolStripMenuItem_Click);
            // 
            // panelForViewer
            // 
            this.panelForViewer.ContextMenuStrip = this.contextMenuStrip1;
            this.panelForViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForViewer.Location = new System.Drawing.Point(0, 0);
            this.panelForViewer.Name = "panelForViewer";
            this.panelForViewer.Size = new System.Drawing.Size(591, 561);
            this.panelForViewer.TabIndex = 0;
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
            this.ClientSize = new System.Drawing.Size(591, 561);
            this.Controls.Add(this.panelForViewer);
            this.Name = "FormDetect4K";
            this.Text = "Bind";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.ToolStripMenuItem rtoolDefect;
        private System.Windows.Forms.ToolStripMenuItem rtoolDebug;
        private System.Windows.Forms.ToolStripMenuItem rtoolDebugClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panelForViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem rtoolTABChart;
        private System.Windows.Forms.ToolStripMenuItem widthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distanceDiffToolStripMenuItem;
    }
}

