using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DetectCCD
{
    public partial class UserSelect : DevExpress.XtraEditors.XtraForm
    {
        public UserSelect()
        {
            InitializeComponent();

            var listLabel = new List<LabelControl>() { label1, label2, label3 };
            var listPicEdit = new List<PictureEdit>() { picEdit1, picEdit2, picEdit3 };
            int listCount = listLabel.Count;

            for (int i = 0; i < listCount; i++)
            {
                //初始化显示
                listLabel[i].ForeColor = (i == Static.App.select_userid ? Color.Red : Color.Silver);
                
                //初始化控件参数与事件
                listPicEdit[i].Tag = i;
                listPicEdit[i].Click += picEdit_Click;
                listPicEdit[i].KeyDown += picEdit_KeyDown;
            }

            this.KeyDown += (o1, e1) =>
            {
                if (e1.KeyCode == Keys.Escape)
                    this.Dispose();
            };
        }

        void picEdit_KeyDown(object o1, KeyEventArgs e1)
        {
            if (e1.KeyCode == Keys.Enter)
                picEdit_Click(o1, null);

            if (e1.KeyCode == Keys.Escape)
                this.Dispose();
        }

        void picEdit_Click(object o1, EventArgs e1)
        {
            int i = (int)((PictureEdit)o1).Tag;
            if (i != Static.App.select_userid)
            {
                string username = Static.App.GetUsernameList()[i];
                string password = Static.App.GetPasswordList()[i];
                using (UserLogin login = new UserLogin(username, password))
                {
                    login.ShowDialog(this);
                    if (login.Pass)
                    {
                        Static.App.select_userid = i;
                        this.Dispose();
                    }
                }
            }
        }

        public static void GShow(IWin32Window parent) {
            UserSelect us = new UserSelect();
            us.ShowDialog(parent);
        }

    }
}