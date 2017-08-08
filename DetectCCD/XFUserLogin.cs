using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace DetectCCD
{
    public partial class UserLogin : DevExpress.XtraEditors.XtraForm
    {
        public UserLogin(string username, string password)
        {
            InitializeComponent();

            //初始化用户名、密码
            c_textUser.Text = username;
            c_textPwd.Tag = password;

            //登陆事件
            btnLogin.Click += (o1, e1) =>
            {
                if (c_textPwd.Text != c_textPwd.Tag.ToString())
                {
                    XtraMessageBox.Show("密码错误");
                    c_textPwd.Text = "";
                    c_textPwd.Focus();
                }
                else
                {
                    this.Pass = true;
                    this.Dispose();
                }
            };

            //键盘事件
            c_textPwd.KeyDown += (o1, e1) =>
            {
                if (e1.KeyCode == Keys.Enter)
                    btnLogin.PerformClick();
                if (e1.KeyCode == Keys.Escape)
                    this.Dispose();
            };

            //若密码为空，则不必弹出界面
            this.Shown += (o1, e1) =>
            {
                if (password == "")
                {
                    this.Pass = true;
                    this.Dispose();
                }
            };
        }

        /// <summary> 密码通过 </summary>
        public bool Pass = false;
    }
}
