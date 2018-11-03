namespace DetectCCD {
    partial class XFImageProcess {
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnDetectTab = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetectMark = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetectDefect = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetectLineLeakMetal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDetectLineLeakMetal);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDetectDefect);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDetectMark);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDetectTab);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(821, 545);
            this.splitContainerControl1.SplitterPosition = 217;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnDetectTab
            // 
            this.btnDetectTab.Location = new System.Drawing.Point(24, 19);
            this.btnDetectTab.Name = "btnDetectTab";
            this.btnDetectTab.Size = new System.Drawing.Size(128, 31);
            this.btnDetectTab.TabIndex = 2;
            this.btnDetectTab.Text = "检测极耳";
            this.btnDetectTab.Click += new System.EventHandler(this.btnDetectTab_Click);
            // 
            // btnDetectMark
            // 
            this.btnDetectMark.Location = new System.Drawing.Point(24, 56);
            this.btnDetectMark.Name = "btnDetectMark";
            this.btnDetectMark.Size = new System.Drawing.Size(128, 31);
            this.btnDetectMark.TabIndex = 3;
            this.btnDetectMark.Text = "检测Mark孔";
            this.btnDetectMark.Click += new System.EventHandler(this.btnDetectMark_Click);
            // 
            // btnDetectDefect
            // 
            this.btnDetectDefect.Location = new System.Drawing.Point(24, 93);
            this.btnDetectDefect.Name = "btnDetectDefect";
            this.btnDetectDefect.Size = new System.Drawing.Size(128, 31);
            this.btnDetectDefect.TabIndex = 4;
            this.btnDetectDefect.Text = "检测缺陷";
            this.btnDetectDefect.Click += new System.EventHandler(this.btnDetectDefect_Click);
            // 
            // btnDetectLineLeakMetal
            // 
            this.btnDetectLineLeakMetal.Location = new System.Drawing.Point(24, 144);
            this.btnDetectLineLeakMetal.Name = "btnDetectLineLeakMetal";
            this.btnDetectLineLeakMetal.Size = new System.Drawing.Size(128, 31);
            this.btnDetectLineLeakMetal.TabIndex = 5;
            this.btnDetectLineLeakMetal.Text = "检测线状漏金属";
            this.btnDetectLineLeakMetal.Click += new System.EventHandler(this.btnDetectLineLeakMetal_Click);
            // 
            // XFImageProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 545);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "XFImageProcess";
            this.Text = "图像处理测试";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnDetectDefect;
        private DevExpress.XtraEditors.SimpleButton btnDetectMark;
        private DevExpress.XtraEditors.SimpleButton btnDetectTab;
        private DevExpress.XtraEditors.SimpleButton btnDetectLineLeakMetal;
    }
}