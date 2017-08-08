namespace DetectCCD
{
    partial class UserLogin
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
            this.c_textPwd = new DevExpress.XtraEditors.TextEdit();
            this.c_textUser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.c_textPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_textUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // c_textPwd
            // 
            this.c_textPwd.EditValue = "";
            this.c_textPwd.Location = new System.Drawing.Point(116, 71);
            this.c_textPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.c_textPwd.Name = "c_textPwd";
            this.c_textPwd.Properties.PasswordChar = '*';
            this.c_textPwd.Size = new System.Drawing.Size(134, 20);
            this.c_textPwd.TabIndex = 20;
            // 
            // c_textUser
            // 
            this.c_textUser.Enabled = false;
            this.c_textUser.Location = new System.Drawing.Point(116, 50);
            this.c_textUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.c_textUser.Name = "c_textUser";
            this.c_textUser.Size = new System.Drawing.Size(134, 20);
            this.c_textUser.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(87, 52);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "用户";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(87, 73);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "密码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(105, 111);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 29);
            this.btnLogin.TabIndex = 30;
            this.btnLogin.Text = "登陆";
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 186);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.c_textUser);
            this.Controls.Add(this.c_textPwd);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UserLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登陆";
            ((System.ComponentModel.ISupportInitialize)(this.c_textPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_textUser.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit c_textPwd;
        private DevExpress.XtraEditors.TextEdit c_textUser;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
    }
}