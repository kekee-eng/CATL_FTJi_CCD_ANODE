﻿namespace DetectCCD {
    partial class XFCameraControl {
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
            this.btnLoadFileInner = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFileOuter = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.trackFps = new DevExpress.XtraEditors.TrackBarControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textFrameStart = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnInit = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnLoadDb = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFrameStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadFileInner
            // 
            this.btnLoadFileInner.Location = new System.Drawing.Point(305, 80);
            this.btnLoadFileInner.Name = "btnLoadFileInner";
            this.btnLoadFileInner.Size = new System.Drawing.Size(101, 30);
            this.btnLoadFileInner.TabIndex = 7;
            this.btnLoadFileInner.Text = "设置内侧相机";
            this.btnLoadFileInner.Click += new System.EventHandler(this.btnLoadFileInner_Click);
            // 
            // btnLoadFileOuter
            // 
            this.btnLoadFileOuter.Location = new System.Drawing.Point(305, 116);
            this.btnLoadFileOuter.Name = "btnLoadFileOuter";
            this.btnLoadFileOuter.Size = new System.Drawing.Size(101, 30);
            this.btnLoadFileOuter.TabIndex = 8;
            this.btnLoadFileOuter.Text = "设置外侧相机";
            this.btnLoadFileOuter.Click += new System.EventHandler(this.btnLoadFileOuter_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(44, 171);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 30);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "开始采图";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(44, 207);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 30);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // trackFps
            // 
            this.trackFps.EditValue = 10;
            this.trackFps.Location = new System.Drawing.Point(85, 279);
            this.trackFps.Name = "trackFps";
            this.trackFps.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackFps.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackFps.Properties.Maximum = 300;
            this.trackFps.Properties.Minimum = 3;
            this.trackFps.Properties.TickFrequency = 10;
            this.trackFps.Size = new System.Drawing.Size(186, 45);
            this.trackFps.TabIndex = 12;
            this.trackFps.Value = 10;
            this.trackFps.EditValueChanged += new System.EventHandler(this.trackFps_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 284);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "播放帧率";
            // 
            // textFrameStart
            // 
            this.textFrameStart.Location = new System.Drawing.Point(85, 311);
            this.textFrameStart.Name = "textFrameStart";
            this.textFrameStart.Properties.AutoHeight = false;
            this.textFrameStart.Size = new System.Drawing.Size(186, 25);
            this.textFrameStart.TabIndex = 15;
            this.textFrameStart.EditValueChanged += new System.EventHandler(this.textFrameStart_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 316);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "起始位置";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(44, 63);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(101, 30);
            this.btnInit.TabIndex = 17;
            this.btnInit.Text = "初始化";
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnLoadDb
            // 
            this.btnLoadDb.Location = new System.Drawing.Point(44, 28);
            this.btnLoadDb.Name = "btnLoadDb";
            this.btnLoadDb.Size = new System.Drawing.Size(101, 30);
            this.btnLoadDb.TabIndex = 18;
            this.btnLoadDb.Text = "设置膜卷数据";
            this.btnLoadDb.Click += new System.EventHandler(this.btnLoadDb_Click);
            // 
            // XFCameraControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 361);
            this.Controls.Add(this.btnLoadDb);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textFrameStart);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.trackFps);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnLoadFileOuter);
            this.Controls.Add(this.btnLoadFileInner);
            this.Name = "XFCameraControl";
            this.Text = "XFCameraControl";
            ((System.ComponentModel.ISupportInitialize)(this.trackFps.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFrameStart.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLoadFileInner;
        private DevExpress.XtraEditors.SimpleButton btnLoadFileOuter;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.TrackBarControl trackFps;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textFrameStart;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnInit;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SimpleButton btnLoadDb;
    }
}