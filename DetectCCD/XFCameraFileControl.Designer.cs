namespace DetectCCD {
    partial class XFCameraFileControl {
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
            this.btnLoadInner = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFileOuter = new DevExpress.XtraEditors.SimpleButton();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.trackFps = new DevExpress.XtraEditors.TrackBarControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textFrameStart = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnInit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFrameStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadInner
            // 
            this.btnLoadInner.Location = new System.Drawing.Point(367, 12);
            this.btnLoadInner.Name = "btnLoadInner";
            this.btnLoadInner.Size = new System.Drawing.Size(101, 30);
            this.btnLoadInner.TabIndex = 7;
            this.btnLoadInner.Text = "设置内侧相机";
            this.btnLoadInner.Click += new System.EventHandler(this.btnLoadInner_Click);
            // 
            // btnLoadFileOuter
            // 
            this.btnLoadFileOuter.Location = new System.Drawing.Point(367, 48);
            this.btnLoadFileOuter.Name = "btnLoadFileOuter";
            this.btnLoadFileOuter.Size = new System.Drawing.Size(101, 30);
            this.btnLoadFileOuter.TabIndex = 8;
            this.btnLoadFileOuter.Text = "设置外侧相机";
            this.btnLoadFileOuter.Click += new System.EventHandler(this.btnLoadFileOuter_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(367, 166);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(101, 30);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "复位";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(367, 202);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 30);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "开始";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(367, 238);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 30);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // trackFps
            // 
            this.trackFps.EditValue = 30;
            this.trackFps.Location = new System.Drawing.Point(114, 187);
            this.trackFps.Name = "trackFps";
            this.trackFps.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackFps.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackFps.Properties.Maximum = 300;
            this.trackFps.Properties.Minimum = 30;
            this.trackFps.Properties.TickFrequency = 10;
            this.trackFps.Size = new System.Drawing.Size(186, 45);
            this.trackFps.TabIndex = 12;
            this.trackFps.Value = 30;
            this.trackFps.EditValueChanged += new System.EventHandler(this.trackFps_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(60, 192);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "播放帧率";
            // 
            // textFrameStart
            // 
            this.textFrameStart.Location = new System.Drawing.Point(114, 219);
            this.textFrameStart.Name = "textFrameStart";
            this.textFrameStart.Properties.AutoHeight = false;
            this.textFrameStart.Size = new System.Drawing.Size(186, 25);
            this.textFrameStart.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(60, 224);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "起始位置";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(367, 84);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(101, 30);
            this.btnInit.TabIndex = 17;
            this.btnInit.Text = "初始化";
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // XFCameraFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 310);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textFrameStart);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.trackFps);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnLoadFileOuter);
            this.Controls.Add(this.btnLoadInner);
            this.Name = "XFCameraFileControl";
            this.Text = "XFCameraFileControl";
            ((System.ComponentModel.ISupportInitialize)(this.trackFps.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFrameStart.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLoadInner;
        private DevExpress.XtraEditors.SimpleButton btnLoadFileOuter;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.TrackBarControl trackFps;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textFrameStart;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnInit;
    }
}