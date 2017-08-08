namespace DetectCCD
{
    partial class UserSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSelect));
            this.picEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.defaultToolTipController1 = new DevExpress.Utils.DefaultToolTipController(this.components);
            this.picEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.picEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picEdit1
            // 
            this.picEdit1.EditValue = ((object)(resources.GetObject("picEdit1.EditValue")));
            this.picEdit1.Location = new System.Drawing.Point(83, 62);
            this.picEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.picEdit1.Name = "picEdit1";
            this.picEdit1.Properties.ShowMenu = false;
            this.picEdit1.Size = new System.Drawing.Size(151, 165);
            this.picEdit1.TabIndex = 0;
            this.picEdit1.ToolTip = "操作员可以进行以下操作：\r\n1. 设备开启，停止，复位\r\n2. 设备状态监控\r\n3. 可选择“人工复判”模式";
            this.picEdit1.ToolTipController = this.defaultToolTipController1.DefaultController;
            this.picEdit1.ToolTipTitle = "操作员";
            // 
            // defaultToolTipController1
            // 
            // 
            // 
            // 
            this.defaultToolTipController1.DefaultController.Rounded = true;
            // 
            // picEdit2
            // 
            this.picEdit2.EditValue = ((object)(resources.GetObject("picEdit2.EditValue")));
            this.picEdit2.Location = new System.Drawing.Point(347, 62);
            this.picEdit2.Margin = new System.Windows.Forms.Padding(2);
            this.picEdit2.Name = "picEdit2";
            this.picEdit2.Properties.ShowMenu = false;
            this.picEdit2.Size = new System.Drawing.Size(151, 165);
            this.picEdit2.TabIndex = 1;
            this.picEdit2.ToolTip = "工程师可以进行以下操作：\r\n1. 配方载入、保存及修改（含检测规格）";
            this.picEdit2.ToolTipTitle = "工程师";
            // 
            // picEdit3
            // 
            this.picEdit3.EditValue = ((object)(resources.GetObject("picEdit3.EditValue")));
            this.picEdit3.Location = new System.Drawing.Point(612, 62);
            this.picEdit3.Margin = new System.Windows.Forms.Padding(2);
            this.picEdit3.Name = "picEdit3";
            this.picEdit3.Properties.ShowMenu = false;
            this.picEdit3.Size = new System.Drawing.Size(151, 165);
            this.picEdit3.TabIndex = 2;
            this.picEdit3.ToolTip = "专家可以进行以下操作：\r\n1. 图像采集测试\r\n2. PLC交互测试\r\n3. 数据库测试";
            this.picEdit3.ToolTipTitle = "专家";
            // 
            // label1
            // 
            this.label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.label1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.label1.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.label1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.label1.LineVisible = true;
            this.label1.Location = new System.Drawing.Point(83, 232);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "操作员";
            // 
            // label2
            // 
            this.label2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.label2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.label2.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.label2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.label2.LineVisible = true;
            this.label2.Location = new System.Drawing.Point(347, 232);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "工程师";
            // 
            // label3
            // 
            this.label3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.label3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label3.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.label3.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.label3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.label3.LineVisible = true;
            this.label3.Location = new System.Drawing.Point(612, 232);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "专家";
            // 
            // UserSelect
            // 
            this.defaultToolTipController1.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 320);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picEdit3);
            this.Controls.Add(this.picEdit2);
            this.Controls.Add(this.picEdit1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "更换用户";
            ((System.ComponentModel.ISupportInitialize)(this.picEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picEdit1;
        private DevExpress.XtraEditors.PictureEdit picEdit2;
        private DevExpress.XtraEditors.PictureEdit picEdit3;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.Utils.DefaultToolTipController defaultToolTipController1;
    }
}