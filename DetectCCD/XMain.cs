using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DetectCCD;
using System.IO;
using System.Diagnostics;

namespace DetectCCD
{
    public partial class XMain : DevExpress.XtraEditors.XtraForm
    {
        public XMain()
        {
            InitializeComponent();

            //
            init_status();

            //
            UtilTool.XFWait.Close();
        }
        private void XFMain_FormClosing(object sender, FormClosingEventArgs e) {

#if !DEBUG
            if(XtraMessageBox.Show("当前操作将退出视觉检测系统，请确认是否退出？", "退出确认", MessageBoxButtons.OKCancel) != DialogResult.OK) {
                e.Cancel = true;
                return;
            }

#endif

            //取消全屏显示
            UtilTool.FullScreen.Set(this, false);
        }
        
        void appendLog(string msg, int msgStatus = 0) {
            //
            status_info.Caption = msg;
            status_info.ItemAppearance.Normal.ForeColor = msgStatus == 0 ? Color.Black : msgStatus > 0 ? Color.Green : Color.Red;

            //添加到日志文件中
            Static.Log.Info(msg);
        }

        //操作模板
        void RunAction(string actName, Action act) {
            try {
                appendLog(String.Format("正在{0}...", actName));
                act();
                appendLog(String.Format("{0}成功", actName));
            }
            catch (Exception ex) {
                appendLog(String.Format("{0}失败: \r\n{1}", actName, ex.Message), -1);
            }
        }
        
        bool isOnline { get { return textMode.SelectedIndex == 0; } }
        bool isRunning = false;
        bool isReset = false;
        bool isRollOk = false;

        int rollType = -1;
        string rollName = "";
        int rollRepeat = 0;

        void init_status()
        {
            //选定用户
            changeUser();

            //
            textMode.SelectedIndex = Static.ParamApp.run_mode;

#if !DEBUG
            //全屏显示
            selectFullScreen_ItemClick(null, null);
#endif

            //连接设备
            status_plc_ItemClick(null, null);

            //定时器
            timer1.Tag = true;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1_Tick(null, null);

        }
        void changeUser()
        {
            //隐藏按钮
            selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //隐藏页面
            xtraTabControl1.TabPages.Remove(xtraTabPage3);
            xtraTabControl1.TabPages.Remove(xtraTabPage4);

            int userselect = Static.ParamApp.select_userid;
            if (userselect == 0)
            {
                //左下角图标
                status_user.ImageIndex = 0;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.ParamApp.operator_viewstyle);
            }
            else if (userselect == 1)
            {
                //左下角图标
                status_user.ImageIndex = 1;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.ParamApp.engineer_viewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
            }
            else
            {
                //左下角图标
                status_user.ImageIndex = 2;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.ParamApp.expert_viewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
                xtraTabControl1.TabPages.Add(xtraTabPage4);

                //右下角按钮
                selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }
        private void timer1_Tick(object sender, EventArgs e) {
            
            Static.SafeRun(() => {
                
                //
                textMode.BackColor = isOnline ? Color.LightGreen : Color.Pink;
                groupOnline.Enabled = isOnline;
                groupOffline.Enabled = !isOnline;

                //状态栏
                status_time.Caption = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                status_device.ImageIndex = true ? 5 : 4;
                status_memory.Caption = string.Format("内存={0:0.0}M", UtilPerformance.GetMemoryLoad());
                status_diskspace.Caption = string.Format("硬盘剩余空间={0:0.0}G", UtilPerformance.GetDiskFree(Application.StartupPath[0].ToString()));

            });
        }

        private void status_user_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RunAction("切换用户",() =>
            {
                UserSelect.GShow(this);
                changeUser();
            });
        }
        private void status_plc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            RunAction("连接设备", () => {

            });
        }
        private void selectFullScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectFullScreen.Tag == null)
                selectFullScreen.Tag = false;

            if ((bool)selectFullScreen.Tag)
            {
                //取消全屏
                UtilTool.FullScreen.Set(this, false);
                selectFullScreen.Tag = false;

                this.Size = new Size(1000, 800);

                //更改图标
                selectFullScreen.ImageIndex = 7;
            }
            else
            {
                //全屏
                UtilTool.FullScreen.Set(this, true);
                selectFullScreen.Tag = true;
                
                //更改图标
                selectFullScreen.ImageIndex = 6;
            }
        }
        
        private void btnRollSet_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

                if (!isRollOk) {
                    if (textRollType.SelectedIndex == -1)
                        throw new Exception("料号未设定！");

                    if (textRollName.Text == "")
                        throw new Exception("膜卷号未设定!");

                    rollRepeat++;
                    rollType = textRollType.SelectedIndex;
                    rollName = textRollName.Text;
                    textRollRepeat.Text = rollRepeat.ToString();

                    textRollType.Enabled = false;
                    textRollName.Enabled = false;
                    isRollOk = true;
                }
                else {
                    textRollType.Enabled = true;
                    textRollName.Enabled = true;
                    isRollOk = false;
                }
            });
        }
        private void btnDeivceQuit_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceStart_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

                if (!isRollOk)
                    throw new Exception("膜卷未设置！");

            });
        }
        private void btnDeviceStop_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceReset_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceEStop_Click(object sender, EventArgs e) {
            RunAction((sender as SimpleButton).Text, () => {

            });
        }
    }

}
