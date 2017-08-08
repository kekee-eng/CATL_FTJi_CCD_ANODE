namespace DetectCCD {
    partial class XtraForm1 {
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoadFile2 = new System.Windows.Forms.Button();
            this.btnLoadFile1 = new System.Windows.Forms.Button();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnGrabRestart = new System.Windows.Forms.Button();
            this.btnGrabStart = new System.Windows.Forms.Button();
            this.btnGrabStop = new System.Windows.Forms.Button();
            this.trackSpeed = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(509, 575);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnLoadFile2
            // 
            this.btnLoadFile2.Location = new System.Drawing.Point(542, 102);
            this.btnLoadFile2.Name = "btnLoadFile2";
            this.btnLoadFile2.Size = new System.Drawing.Size(71, 45);
            this.btnLoadFile2.TabIndex = 132;
            this.btnLoadFile2.Text = "L2";
            this.btnLoadFile2.UseVisualStyleBackColor = true;
            this.btnLoadFile2.Click += new System.EventHandler(this.btnLoadFile2_Click);
            // 
            // btnLoadFile1
            // 
            this.btnLoadFile1.Location = new System.Drawing.Point(542, 51);
            this.btnLoadFile1.Name = "btnLoadFile1";
            this.btnLoadFile1.Size = new System.Drawing.Size(71, 45);
            this.btnLoadFile1.TabIndex = 131;
            this.btnLoadFile1.Text = "L1";
            this.btnLoadFile1.UseVisualStyleBackColor = true;
            this.btnLoadFile1.Click += new System.EventHandler(this.btnLoadFile1_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(21, 27);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(515, 604);
            this.xtraTabControl1.TabIndex = 133;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.dataGridView1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(509, 575);
            this.xtraTabPage1.Text = "App";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.dataGridView2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(509, 575);
            this.xtraTabPage2.Text = "Share";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(509, 575);
            this.dataGridView2.TabIndex = 1;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.dataGridView3);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(509, 575);
            this.xtraTabPage3.Text = "Inner";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(509, 575);
            this.dataGridView3.TabIndex = 1;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.dataGridView4);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(509, 575);
            this.xtraTabPage4.Text = "Outer";
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.Size = new System.Drawing.Size(509, 575);
            this.dataGridView4.TabIndex = 1;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(542, 581);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(71, 45);
            this.btnQuit.TabIndex = 134;
            this.btnQuit.Text = "Q";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnGrabRestart
            // 
            this.btnGrabRestart.Location = new System.Drawing.Point(542, 182);
            this.btnGrabRestart.Name = "btnGrabRestart";
            this.btnGrabRestart.Size = new System.Drawing.Size(75, 23);
            this.btnGrabRestart.TabIndex = 6;
            this.btnGrabRestart.Text = "Restart";
            this.btnGrabRestart.UseVisualStyleBackColor = true;
            this.btnGrabRestart.Click += new System.EventHandler(this.btnGrabRestart_Click);
            // 
            // btnGrabStart
            // 
            this.btnGrabStart.Location = new System.Drawing.Point(542, 211);
            this.btnGrabStart.Name = "btnGrabStart";
            this.btnGrabStart.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStart.TabIndex = 3;
            this.btnGrabStart.Text = "Start";
            this.btnGrabStart.UseVisualStyleBackColor = true;
            this.btnGrabStart.Click += new System.EventHandler(this.btnGrabStart_Click);
            // 
            // btnGrabStop
            // 
            this.btnGrabStop.Location = new System.Drawing.Point(542, 240);
            this.btnGrabStop.Name = "btnGrabStop";
            this.btnGrabStop.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStop.TabIndex = 4;
            this.btnGrabStop.Text = "Stop";
            this.btnGrabStop.UseVisualStyleBackColor = true;
            this.btnGrabStop.Click += new System.EventHandler(this.btnGrabStop_Click);
            // 
            // trackSpeed
            // 
            this.trackSpeed.Location = new System.Drawing.Point(542, 269);
            this.trackSpeed.Maximum = 300;
            this.trackSpeed.Minimum = 3;
            this.trackSpeed.Name = "trackSpeed";
            this.trackSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackSpeed.Size = new System.Drawing.Size(45, 200);
            this.trackSpeed.TabIndex = 138;
            this.trackSpeed.TickFrequency = 10;
            this.trackSpeed.Value = 3;
            this.trackSpeed.Scroll += new System.EventHandler(this.trackSpeed_Scroll);
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 647);
            this.Controls.Add(this.btnGrabStop);
            this.Controls.Add(this.btnGrabStart);
            this.Controls.Add(this.btnGrabRestart);
            this.Controls.Add(this.trackSpeed);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnLoadFile2);
            this.Controls.Add(this.btnLoadFile1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoadFile2;
        private System.Windows.Forms.Button btnLoadFile1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnGrabRestart;
        private System.Windows.Forms.Button btnGrabStart;
        private System.Windows.Forms.Button btnGrabStop;
        private System.Windows.Forms.TrackBar trackSpeed;
    }
}