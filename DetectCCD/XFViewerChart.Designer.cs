namespace DetectCCD {
    partial class XFViewerChart {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInfoApp = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInfoInner = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInfoOuter = new System.Windows.Forms.ToolStripMenuItem();
            this.paramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtParamApp = new System.Windows.Forms.ToolStripMenuItem();
            this.rtParamShare = new System.Windows.Forms.ToolStripMenuItem();
            this.rtParamInner = new System.Windows.Forms.ToolStripMenuItem();
            this.rtParamOuter = new System.Windows.Forms.ToolStripMenuItem();
            this.innerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerTab = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerEa = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerDefect = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rtInnerTabChart = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerTabChartWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerTabChartHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerTabChartDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.rtInnerTabChartDistdiff = new System.Windows.Forms.ToolStripMenuItem();
            this.outerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterTab = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterEa = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterDefect = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rtOuterTabChart = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterTabChartWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterTabChartHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterTabChartDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.rtOuterTabChartDistdiff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rtReflush = new System.Windows.Forms.ToolStripMenuItem();
            this.rtReflushTime = new System.Windows.Forms.ToolStripTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.paramToolStripMenuItem,
            this.innerToolStripMenuItem1,
            this.outerToolStripMenuItem1,
            this.toolStripSeparator3,
            this.rtReflush});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 120);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtInfoApp,
            this.rtInfoInner,
            this.rtInfoOuter});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // rtInfoApp
            // 
            this.rtInfoApp.Name = "rtInfoApp";
            this.rtInfoApp.Size = new System.Drawing.Size(109, 22);
            this.rtInfoApp.Text = "App";
            this.rtInfoApp.Click += new System.EventHandler(this.rtInfoApp_Click);
            // 
            // rtInfoInner
            // 
            this.rtInfoInner.Name = "rtInfoInner";
            this.rtInfoInner.Size = new System.Drawing.Size(109, 22);
            this.rtInfoInner.Text = "Inner";
            this.rtInfoInner.Click += new System.EventHandler(this.rtInfoInner_Click);
            // 
            // rtInfoOuter
            // 
            this.rtInfoOuter.Name = "rtInfoOuter";
            this.rtInfoOuter.Size = new System.Drawing.Size(109, 22);
            this.rtInfoOuter.Text = "Outer";
            this.rtInfoOuter.Click += new System.EventHandler(this.rtInfoOuter_Click);
            // 
            // paramToolStripMenuItem
            // 
            this.paramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtParamApp,
            this.rtParamShare,
            this.rtParamInner,
            this.rtParamOuter});
            this.paramToolStripMenuItem.Name = "paramToolStripMenuItem";
            this.paramToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.paramToolStripMenuItem.Text = "Param";
            // 
            // rtParamApp
            // 
            this.rtParamApp.Name = "rtParamApp";
            this.rtParamApp.Size = new System.Drawing.Size(109, 22);
            this.rtParamApp.Text = "App";
            this.rtParamApp.Click += new System.EventHandler(this.rtParamApp_Click);
            // 
            // rtParamShare
            // 
            this.rtParamShare.Name = "rtParamShare";
            this.rtParamShare.Size = new System.Drawing.Size(109, 22);
            this.rtParamShare.Text = "Share";
            this.rtParamShare.Click += new System.EventHandler(this.rtParamShare_Click);
            // 
            // rtParamInner
            // 
            this.rtParamInner.Name = "rtParamInner";
            this.rtParamInner.Size = new System.Drawing.Size(109, 22);
            this.rtParamInner.Text = "Inner";
            this.rtParamInner.Click += new System.EventHandler(this.rtParamInner_Click);
            // 
            // rtParamOuter
            // 
            this.rtParamOuter.Name = "rtParamOuter";
            this.rtParamOuter.Size = new System.Drawing.Size(109, 22);
            this.rtParamOuter.Text = "Outer";
            this.rtParamOuter.Click += new System.EventHandler(this.rtParamOuter_Click);
            // 
            // innerToolStripMenuItem1
            // 
            this.innerToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtInnerTab,
            this.rtInnerEa,
            this.rtInnerDefect,
            this.rtInnerLabel,
            this.toolStripSeparator1,
            this.rtInnerTabChart});
            this.innerToolStripMenuItem1.Name = "innerToolStripMenuItem1";
            this.innerToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.innerToolStripMenuItem1.Text = "Inner";
            // 
            // rtInnerTab
            // 
            this.rtInnerTab.Name = "rtInnerTab";
            this.rtInnerTab.Size = new System.Drawing.Size(138, 22);
            this.rtInnerTab.Text = "TAB";
            this.rtInnerTab.Click += new System.EventHandler(this.rtInnerTab_Click);
            // 
            // rtInnerEa
            // 
            this.rtInnerEa.Name = "rtInnerEa";
            this.rtInnerEa.Size = new System.Drawing.Size(138, 22);
            this.rtInnerEa.Text = "EA";
            this.rtInnerEa.Click += new System.EventHandler(this.rtInnerEa_Click);
            // 
            // rtInnerDefect
            // 
            this.rtInnerDefect.Name = "rtInnerDefect";
            this.rtInnerDefect.Size = new System.Drawing.Size(138, 22);
            this.rtInnerDefect.Text = "Defect";
            this.rtInnerDefect.Click += new System.EventHandler(this.rtInnerDefect_Click);
            // 
            // rtInnerLabel
            // 
            this.rtInnerLabel.Name = "rtInnerLabel";
            this.rtInnerLabel.Size = new System.Drawing.Size(138, 22);
            this.rtInnerLabel.Text = "Label";
            this.rtInnerLabel.Click += new System.EventHandler(this.rtInnerLabel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // rtInnerTabChart
            // 
            this.rtInnerTabChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtInnerTabChartWidth,
            this.rtInnerTabChartHeight,
            this.rtInnerTabChartDistance,
            this.rtInnerTabChartDistdiff});
            this.rtInnerTabChart.Name = "rtInnerTabChart";
            this.rtInnerTabChart.Size = new System.Drawing.Size(138, 22);
            this.rtInnerTabChart.Text = "TAB(Chart)";
            // 
            // rtInnerTabChartWidth
            // 
            this.rtInnerTabChartWidth.Name = "rtInnerTabChartWidth";
            this.rtInnerTabChartWidth.Size = new System.Drawing.Size(125, 22);
            this.rtInnerTabChartWidth.Text = "Width";
            this.rtInnerTabChartWidth.Click += new System.EventHandler(this.rtInnerTabChartWidth_Click);
            // 
            // rtInnerTabChartHeight
            // 
            this.rtInnerTabChartHeight.Name = "rtInnerTabChartHeight";
            this.rtInnerTabChartHeight.Size = new System.Drawing.Size(125, 22);
            this.rtInnerTabChartHeight.Text = "Height";
            this.rtInnerTabChartHeight.Click += new System.EventHandler(this.rtInnerTabChartHeight_Click);
            // 
            // rtInnerTabChartDistance
            // 
            this.rtInnerTabChartDistance.Name = "rtInnerTabChartDistance";
            this.rtInnerTabChartDistance.Size = new System.Drawing.Size(125, 22);
            this.rtInnerTabChartDistance.Text = "Distance";
            this.rtInnerTabChartDistance.Click += new System.EventHandler(this.rtInnerTabChartDistance_Click);
            // 
            // rtInnerTabChartDistdiff
            // 
            this.rtInnerTabChartDistdiff.Name = "rtInnerTabChartDistdiff";
            this.rtInnerTabChartDistdiff.Size = new System.Drawing.Size(125, 22);
            this.rtInnerTabChartDistdiff.Text = "Distdiff";
            this.rtInnerTabChartDistdiff.Click += new System.EventHandler(this.rtInnerTabChartDistdiff_Click);
            // 
            // outerToolStripMenuItem1
            // 
            this.outerToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtOuterTab,
            this.rtOuterEa,
            this.rtOuterDefect,
            this.rtOuterLabel,
            this.toolStripSeparator2,
            this.rtOuterTabChart});
            this.outerToolStripMenuItem1.Name = "outerToolStripMenuItem1";
            this.outerToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.outerToolStripMenuItem1.Text = "Outer";
            // 
            // rtOuterTab
            // 
            this.rtOuterTab.Name = "rtOuterTab";
            this.rtOuterTab.Size = new System.Drawing.Size(138, 22);
            this.rtOuterTab.Text = "TAB";
            this.rtOuterTab.Click += new System.EventHandler(this.rtOuterTab_Click);
            // 
            // rtOuterEa
            // 
            this.rtOuterEa.Name = "rtOuterEa";
            this.rtOuterEa.Size = new System.Drawing.Size(138, 22);
            this.rtOuterEa.Text = "EA";
            this.rtOuterEa.Click += new System.EventHandler(this.rtOuterEa_Click);
            // 
            // rtOuterDefect
            // 
            this.rtOuterDefect.Name = "rtOuterDefect";
            this.rtOuterDefect.Size = new System.Drawing.Size(138, 22);
            this.rtOuterDefect.Text = "Defect";
            this.rtOuterDefect.Click += new System.EventHandler(this.rtOuterDefect_Click);
            // 
            // rtOuterLabel
            // 
            this.rtOuterLabel.Name = "rtOuterLabel";
            this.rtOuterLabel.Size = new System.Drawing.Size(138, 22);
            this.rtOuterLabel.Text = "Label";
            this.rtOuterLabel.Click += new System.EventHandler(this.rtOuterLabel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // rtOuterTabChart
            // 
            this.rtOuterTabChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtOuterTabChartWidth,
            this.rtOuterTabChartHeight,
            this.rtOuterTabChartDistance,
            this.rtOuterTabChartDistdiff});
            this.rtOuterTabChart.Name = "rtOuterTabChart";
            this.rtOuterTabChart.Size = new System.Drawing.Size(138, 22);
            this.rtOuterTabChart.Text = "TAB(Chart)";
            // 
            // rtOuterTabChartWidth
            // 
            this.rtOuterTabChartWidth.Name = "rtOuterTabChartWidth";
            this.rtOuterTabChartWidth.Size = new System.Drawing.Size(125, 22);
            this.rtOuterTabChartWidth.Text = "Width";
            this.rtOuterTabChartWidth.Click += new System.EventHandler(this.rtOuterTabChartWidth_Click);
            // 
            // rtOuterTabChartHeight
            // 
            this.rtOuterTabChartHeight.Name = "rtOuterTabChartHeight";
            this.rtOuterTabChartHeight.Size = new System.Drawing.Size(125, 22);
            this.rtOuterTabChartHeight.Text = "Height";
            this.rtOuterTabChartHeight.Click += new System.EventHandler(this.rtOuterTabChartHeight_Click);
            // 
            // rtOuterTabChartDistance
            // 
            this.rtOuterTabChartDistance.Name = "rtOuterTabChartDistance";
            this.rtOuterTabChartDistance.Size = new System.Drawing.Size(125, 22);
            this.rtOuterTabChartDistance.Text = "Distance";
            this.rtOuterTabChartDistance.Click += new System.EventHandler(this.rtOuterTabChartDistance_Click);
            // 
            // rtOuterTabChartDistdiff
            // 
            this.rtOuterTabChartDistdiff.Name = "rtOuterTabChartDistdiff";
            this.rtOuterTabChartDistdiff.Size = new System.Drawing.Size(125, 22);
            this.rtOuterTabChartDistdiff.Text = "Distdiff";
            this.rtOuterTabChartDistdiff.Click += new System.EventHandler(this.rtOuterTabChartDistdiff_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(115, 6);
            // 
            // rtReflush
            // 
            this.rtReflush.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtReflushTime});
            this.rtReflush.Name = "rtReflush";
            this.rtReflush.Size = new System.Drawing.Size(118, 22);
            this.rtReflush.Text = "Reflush";
            this.rtReflush.Click += new System.EventHandler(this.rtReflush_Click);
            // 
            // rtReflushTime
            // 
            this.rtReflushTime.Name = "rtReflushTime";
            this.rtReflushTime.Size = new System.Drawing.Size(100, 23);
            this.rtReflushTime.Text = "1000";
            this.rtReflushTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtReflushTime_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // XFViewerChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "XFViewerChart";
            this.Text = "XFViewerChart";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem paramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rtParamApp;
        private System.Windows.Forms.ToolStripMenuItem rtParamShare;
        private System.Windows.Forms.ToolStripMenuItem rtParamInner;
        private System.Windows.Forms.ToolStripMenuItem rtParamOuter;
        private System.Windows.Forms.ToolStripMenuItem innerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTab;
        private System.Windows.Forms.ToolStripMenuItem rtInnerEa;
        private System.Windows.Forms.ToolStripMenuItem outerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rtInfoApp;
        private System.Windows.Forms.ToolStripMenuItem rtInfoInner;
        private System.Windows.Forms.ToolStripMenuItem rtInfoOuter;
        private System.Windows.Forms.ToolStripMenuItem rtInnerDefect;
        private System.Windows.Forms.ToolStripMenuItem rtInnerLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTabChart;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTab;
        private System.Windows.Forms.ToolStripMenuItem rtOuterEa;
        private System.Windows.Forms.ToolStripMenuItem rtOuterDefect;
        private System.Windows.Forms.ToolStripMenuItem rtOuterLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTabChart;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTabChartWidth;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTabChartHeight;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTabChartDistance;
        private System.Windows.Forms.ToolStripMenuItem rtInnerTabChartDistdiff;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTabChartWidth;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTabChartHeight;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTabChartDistance;
        private System.Windows.Forms.ToolStripMenuItem rtOuterTabChartDistdiff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem rtReflush;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripTextBox rtReflushTime;
    }
}