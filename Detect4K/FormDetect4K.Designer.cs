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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGrabStop = new System.Windows.Forms.Button();
            this.btnGrabStart = new System.Windows.Forms.Button();
            this.lb_grab_isready = new System.Windows.Forms.Label();
            this.lb_grab_fps = new System.Windows.Forms.Label();
            this.lb_grab_frame = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_rec_db = new System.Windows.Forms.Label();
            this.lb_rec_cache = new System.Windows.Forms.Label();
            this.lb_rec_remain = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGrabStop);
            this.groupBox1.Controls.Add(this.btnGrabStart);
            this.groupBox1.Controls.Add(this.lb_grab_isready);
            this.groupBox1.Controls.Add(this.lb_grab_fps);
            this.groupBox1.Controls.Add(this.lb_grab_frame);
            this.groupBox1.Location = new System.Drawing.Point(23, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "取相";
            // 
            // btnGrabStop
            // 
            this.btnGrabStop.Location = new System.Drawing.Point(33, 136);
            this.btnGrabStop.Name = "btnGrabStop";
            this.btnGrabStop.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStop.TabIndex = 4;
            this.btnGrabStop.Text = "Stop";
            this.btnGrabStop.UseVisualStyleBackColor = true;
            this.btnGrabStop.Click += new System.EventHandler(this.btnGrabStop_Click);
            // 
            // btnGrabStart
            // 
            this.btnGrabStart.Location = new System.Drawing.Point(33, 107);
            this.btnGrabStart.Name = "btnGrabStart";
            this.btnGrabStart.Size = new System.Drawing.Size(75, 23);
            this.btnGrabStart.TabIndex = 3;
            this.btnGrabStart.Text = "Start";
            this.btnGrabStart.UseVisualStyleBackColor = true;
            this.btnGrabStart.Click += new System.EventHandler(this.btnGrabStart_Click);
            // 
            // lb_grab_isready
            // 
            this.lb_grab_isready.AutoSize = true;
            this.lb_grab_isready.Location = new System.Drawing.Point(31, 29);
            this.lb_grab_isready.Name = "lb_grab_isready";
            this.lb_grab_isready.Size = new System.Drawing.Size(47, 12);
            this.lb_grab_isready.TabIndex = 2;
            this.lb_grab_isready.Text = "IsReady";
            // 
            // lb_grab_fps
            // 
            this.lb_grab_fps.AutoSize = true;
            this.lb_grab_fps.Location = new System.Drawing.Point(31, 75);
            this.lb_grab_fps.Name = "lb_grab_fps";
            this.lb_grab_fps.Size = new System.Drawing.Size(23, 12);
            this.lb_grab_fps.TabIndex = 1;
            this.lb_grab_fps.Text = "Fps";
            // 
            // lb_grab_frame
            // 
            this.lb_grab_frame.AutoSize = true;
            this.lb_grab_frame.Location = new System.Drawing.Point(31, 52);
            this.lb_grab_frame.Name = "lb_grab_frame";
            this.lb_grab_frame.Size = new System.Drawing.Size(35, 12);
            this.lb_grab_frame.TabIndex = 0;
            this.lb_grab_frame.Text = "Frame";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_rec_remain);
            this.groupBox2.Controls.Add(this.lb_rec_db);
            this.groupBox2.Controls.Add(this.lb_rec_cache);
            this.groupBox2.Location = new System.Drawing.Point(189, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 187);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record";
            // 
            // lb_rec_db
            // 
            this.lb_rec_db.AutoSize = true;
            this.lb_rec_db.Location = new System.Drawing.Point(18, 52);
            this.lb_rec_db.Name = "lb_rec_db";
            this.lb_rec_db.Size = new System.Drawing.Size(17, 12);
            this.lb_rec_db.TabIndex = 4;
            this.lb_rec_db.Text = "DB";
            // 
            // lb_rec_cache
            // 
            this.lb_rec_cache.AutoSize = true;
            this.lb_rec_cache.Location = new System.Drawing.Point(18, 29);
            this.lb_rec_cache.Name = "lb_rec_cache";
            this.lb_rec_cache.Size = new System.Drawing.Size(35, 12);
            this.lb_rec_cache.TabIndex = 3;
            this.lb_rec_cache.Text = "Cache";
            // 
            // lb_rec_remain
            // 
            this.lb_rec_remain.AutoSize = true;
            this.lb_rec_remain.Location = new System.Drawing.Point(18, 75);
            this.lb_rec_remain.Name = "lb_rec_remain";
            this.lb_rec_remain.Size = new System.Drawing.Size(41, 12);
            this.lb_rec_remain.TabIndex = 5;
            this.lb_rec_remain.Text = "Remain";
            // 
            // FormDetect4K
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 694);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormDetect4K";
            this.Text = "Detect4K";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_grab_frame;
        private System.Windows.Forms.Label lb_grab_fps;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_grab_isready;
        private System.Windows.Forms.Button btnGrabStop;
        private System.Windows.Forms.Button btnGrabStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_rec_db;
        private System.Windows.Forms.Label lb_rec_cache;
        private System.Windows.Forms.Label lb_rec_remain;
    }
}

