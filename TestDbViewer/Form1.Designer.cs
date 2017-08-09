namespace TestDbViewer {
    partial class Form1 {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hwinInner = new HalconDotNet.HWindowControl();
            this.hwinOuter = new HalconDotNet.HWindowControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.hwinOuter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hwinInner, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(698, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hwinInner
            // 
            this.hwinInner.BackColor = System.Drawing.Color.Black;
            this.hwinInner.BorderColor = System.Drawing.Color.Black;
            this.hwinInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinInner.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinInner.Location = new System.Drawing.Point(3, 3);
            this.hwinInner.Name = "hwinInner";
            this.hwinInner.Size = new System.Drawing.Size(343, 675);
            this.hwinInner.TabIndex = 30;
            this.hwinInner.WindowSize = new System.Drawing.Size(343, 675);
            // 
            // hwinOuter
            // 
            this.hwinOuter.BackColor = System.Drawing.Color.Black;
            this.hwinOuter.BorderColor = System.Drawing.Color.Black;
            this.hwinOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinOuter.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinOuter.Location = new System.Drawing.Point(352, 3);
            this.hwinOuter.Name = "hwinOuter";
            this.hwinOuter.Size = new System.Drawing.Size(343, 675);
            this.hwinOuter.TabIndex = 31;
            this.hwinOuter.WindowSize = new System.Drawing.Size(343, 675);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "TestDB";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HalconDotNet.HWindowControl hwinInner;
        private HalconDotNet.HWindowControl hwinOuter;
    }
}

