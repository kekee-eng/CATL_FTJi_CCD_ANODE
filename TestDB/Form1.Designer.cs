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
            this.hwinOuter = new HalconDotNet.HWindowControl();
            this.hwinInner = new HalconDotNet.HWindowControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveInner = new System.Windows.Forms.Button();
            this.textInner1 = new System.Windows.Forms.TextBox();
            this.textInner2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textOuter2 = new System.Windows.Forms.TextBox();
            this.textOuter1 = new System.Windows.Forms.TextBox();
            this.btnSaveOuter = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(735, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hwinOuter
            // 
            this.hwinOuter.BackColor = System.Drawing.Color.Black;
            this.hwinOuter.BorderColor = System.Drawing.Color.Black;
            this.hwinOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinOuter.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinOuter.Location = new System.Drawing.Point(370, 3);
            this.hwinOuter.Name = "hwinOuter";
            this.hwinOuter.Size = new System.Drawing.Size(362, 675);
            this.hwinOuter.TabIndex = 31;
            this.hwinOuter.WindowSize = new System.Drawing.Size(362, 675);
            // 
            // hwinInner
            // 
            this.hwinInner.BackColor = System.Drawing.Color.Black;
            this.hwinInner.BorderColor = System.Drawing.Color.Black;
            this.hwinInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinInner.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinInner.Location = new System.Drawing.Point(3, 3);
            this.hwinInner.Name = "hwinInner";
            this.hwinInner.Size = new System.Drawing.Size(361, 675);
            this.hwinInner.TabIndex = 30;
            this.hwinInner.WindowSize = new System.Drawing.Size(361, 675);
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.textOuter2);
            this.splitContainer1.Panel1.Controls.Add(this.textOuter1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveOuter);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textInner2);
            this.splitContainer1.Panel1.Controls.Add(this.textInner1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveInner);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 681);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnSaveInner
            // 
            this.btnSaveInner.Location = new System.Drawing.Point(78, 186);
            this.btnSaveInner.Name = "btnSaveInner";
            this.btnSaveInner.Size = new System.Drawing.Size(162, 40);
            this.btnSaveInner.TabIndex = 0;
            this.btnSaveInner.Text = "保存小图(左)";
            this.btnSaveInner.UseVisualStyleBackColor = true;
            this.btnSaveInner.Click += new System.EventHandler(this.btnSaveInner_Click);
            // 
            // textInner1
            // 
            this.textInner1.Location = new System.Drawing.Point(61, 159);
            this.textInner1.Name = "textInner1";
            this.textInner1.Size = new System.Drawing.Size(84, 21);
            this.textInner1.TabIndex = 1;
            this.textInner1.Text = "1.5";
            // 
            // textInner2
            // 
            this.textInner2.Location = new System.Drawing.Point(156, 159);
            this.textInner2.Name = "textInner2";
            this.textInner2.Size = new System.Drawing.Size(84, 21);
            this.textInner2.TabIndex = 2;
            this.textInner2.Text = "2.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Inner";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(78, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "解压所有图片";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Outer";
            // 
            // textOuter2
            // 
            this.textOuter2.Location = new System.Drawing.Point(156, 321);
            this.textOuter2.Name = "textOuter2";
            this.textOuter2.Size = new System.Drawing.Size(84, 21);
            this.textOuter2.TabIndex = 9;
            this.textOuter2.Text = "5";
            // 
            // textOuter1
            // 
            this.textOuter1.Location = new System.Drawing.Point(61, 321);
            this.textOuter1.Name = "textOuter1";
            this.textOuter1.Size = new System.Drawing.Size(84, 21);
            this.textOuter1.TabIndex = 8;
            this.textOuter1.Text = "1";
            // 
            // btnSaveOuter
            // 
            this.btnSaveOuter.Location = new System.Drawing.Point(78, 348);
            this.btnSaveOuter.Name = "btnSaveOuter";
            this.btnSaveOuter.Size = new System.Drawing.Size(162, 40);
            this.btnSaveOuter.TabIndex = 7;
            this.btnSaveOuter.Text = "保存小图(右)";
            this.btnSaveOuter.UseVisualStyleBackColor = true;
            this.btnSaveOuter.Click += new System.EventHandler(this.btnSaveOuter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 681);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "TestDB";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HalconDotNet.HWindowControl hwinInner;
        private HalconDotNet.HWindowControl hwinOuter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSaveInner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textInner2;
        private System.Windows.Forms.TextBox textInner1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textOuter2;
        private System.Windows.Forms.TextBox textOuter1;
        private System.Windows.Forms.Button btnSaveOuter;
    }
}

