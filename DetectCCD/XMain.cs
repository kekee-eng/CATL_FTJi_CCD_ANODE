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

namespace DetectCCD {
    partial class XMain : DevExpress.XtraEditors.XtraForm {
        public XMain() {
            InitializeComponent();

            //
            init_status();
            init_device();

            //
            UtilTool.XFWait.Dispose();
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

            //
            isQuit = true;
            Record?.Dispose();
            Device?.Dispose();
        }



        //操作模板
        void runAction(string actName, Action act) {
            try {
                appendLog(String.Format("正在{0}...", actName));
                act();
                appendLog(String.Format("{0}成功", actName));
            }
            catch (Exception ex) {
                appendLog(String.Format("{0}失败: \r\n{1}", actName, ex.Message), -1, ex);
            }
        }
        void appendLog(string msg, int msgStatus = 0, Exception ex =null) {
            //
            status_info.Caption = msg;
            status_info.ItemAppearance.Normal.ForeColor = msgStatus == 0 ? Color.Black : msgStatus > 0 ? Color.Green : Color.Red;

            //添加到日志文件中
            Static.Log.Info(msg);

            if (ex != null)
                Static.Log.Info(ex.StackTrace);

        }

        bool isOnline { get { return textMode.SelectedIndex == 0; } }

        bool isQuit = false;
        bool isRunning = false;
        bool isReset = false;
        bool isRollOk = false;

        int rollType = -1;
        string rollName = "";
        int rollRepeat = 0;

        void init_status() {
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
        void changeUser() {
            //隐藏按钮
            selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //隐藏页面
            xtraTabControl1.TabPages.Remove(xtraTabPage3);
            xtraTabControl1.TabPages.Remove(xtraTabPage4);

            int userselect = Static.ParamApp.select_userid;
            if (userselect == 0) {
                //左下角图标
                status_user.ImageIndex = 0;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.ParamApp.operator_viewstyle);
            }
            else if (userselect == 1) {
                //左下角图标
                status_user.ImageIndex = 1;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.ParamApp.engineer_viewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
            }
            else {
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

                //
                _lc_inner_camera.Text = Device.InnerCamera.m_camera_name;
                _lc_inner_fps.Text = Device.InnerCamera.m_fpsRealtime.ToString("0.000");
                _lc_inner_frame.Text = Device.InnerCamera.m_frame.ToString();
                _lc_inner_isgrabbing.Text = Device.InnerCamera.isRun ? "On" : "Off";
                _lc_inner_isopen.Text = Device.InnerCamera.isReady ? "On" : "Off";
                _lc_inner_isgrabbing.ForeColor = Device.InnerCamera.isRun ? Color.Green : Color.Red;
                _lc_inner_isopen.ForeColor = Device.InnerCamera.isReady ? Color.Green : Color.Red;

                _lc_inner_camera2.Text = Device.InnerCamera.m_camera_name;
                _lc_inner_eaCount.Text = Record.InnerDetect.EACount.ToString();
                _lc_inner_widthCount.Text = Record.InnerDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                _lc_inner_defectCount.Text = Record.InnerDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();

                //
                _lc_outer_camera.Text = Device.OuterCamera.m_camera_name;
                _lc_outer_fps.Text = Device.OuterCamera.m_fpsRealtime.ToString("0.000");
                _lc_outer_frame.Text = Device.OuterCamera.m_frame.ToString();
                _lc_outer_isgrabbing.Text = Device.OuterCamera.isRun ? "On" : "Off";
                _lc_outer_isopen.Text = Device.OuterCamera.isReady ? "On" : "Off";
                _lc_outer_isgrabbing.ForeColor = Device.OuterCamera.isRun ? Color.Green : Color.Red;
                _lc_outer_isopen.ForeColor = Device.OuterCamera.isReady ? Color.Green : Color.Red;

                _lc_outer_camera2.Text = Device.OuterCamera.m_camera_name;
                _lc_outer_eaCount.Text = Record.OuterDetect.EACount.ToString();
                _lc_outer_widthCount.Text = Record.OuterDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                _lc_outer_defectCount.Text = Record.OuterDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                
                
                //状态栏
                status_time.Caption = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                status_device.ImageIndex = Device.isRun ? 5 : 4;
                status_memory.Caption = string.Format("内存={0:0.0}M", UtilPerformance.GetMemoryLoad());
                status_diskspace.Caption = string.Format("硬盘剩余空间={0:0.0}G", UtilPerformance.GetDiskFree(Application.StartupPath[0].ToString()));

            });
        }

        private void status_user_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            runAction("切换用户", () => {
                UserSelect.GShow(this);
                changeUser();
            });
        }
        private void status_plc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            runAction("连接设备", () => {
                Device.Open();
            });
        }
        private void selectFullScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (selectFullScreen.Tag == null)
                selectFullScreen.Tag = false;

            if ((bool)selectFullScreen.Tag) {
                //取消全屏
                UtilTool.FullScreen.Set(this, false);
                selectFullScreen.Tag = false;

                this.Size = new Size(1000, 800);

                //更改图标
                selectFullScreen.ImageIndex = 7;
            }
            else {
                //全屏
                UtilTool.FullScreen.Set(this, true);
                selectFullScreen.Tag = true;

                //更改图标
                selectFullScreen.ImageIndex = 6;
            }
        }

        private void btnRollSet_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

                if (!isRollOk) {
                    if (textRollType.SelectedIndex == -1)
                        throw new Exception("料号未设定！");

                    if (textRollName.Text == "")
                        throw new Exception("膜卷号未设定!");

                    rollRepeat++;
                    rollType = textRollType.SelectedIndex;
                    rollName = textRollName.Text;
                    textRollRepeat.Text = rollRepeat.ToString();

                    _init_record();

                    textRollType.Enabled = false;
                    textRollName.Enabled = false;
                    btnRollSet.Text = "结束膜卷";
                    isRollOk = true;
                }
                else {
                    textRollType.Enabled = true;
                    textRollName.Enabled = true;
                    btnRollSet.Text = "设置膜卷";
                    isRollOk = false;
                }
            });
        }

        private void btnDeivceQuit_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceStart_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

                if (!isRollOk)
                    throw new Exception("膜卷未设置！");

            });
        }
        private void btnDeviceStop_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceReset_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

            });
        }
        private void btnDeviceEStop_Click(object sender, EventArgs e) {
            runAction((sender as SimpleButton).Text, () => {

            });
        }


        public ModRecord Record = new ModRecord();
        public ModDevice Device = new ModDevice();

        void init_device() {

            Record.Init();

            Record.InnerViewerImage.Init(hwinInner);
            Device.InnerCamera.OnImageReady += obj => {

                Record.InnerGrab.Cache[obj.Frame] = obj;
                Record.InnerDetect.TryDetect(obj.Frame);
                Record.InnerViewerImage.SetBottomTarget(obj.Frame);
            };

            Record.OuterViewerImage.Init(hwinOuter);
            Device.OuterCamera.OnImageReady += obj => {

                Record.OuterGrab.Cache[obj.Frame] = obj;
                Record.OuterDetect.TryDetect(obj.Frame);
                Record.OuterViewerImage.SetBottomTarget(obj.Frame);
            };


            //管理线程
            Task.Run((Action)(() => {

                //线程：更新显示
                var tView1 = Task.Run((Action)(() => {

                    while (!isQuit) {

                        Thread.Sleep(10);

                        Static.SafeRun(() => {
                            double refFps = Device.InnerCamera.isRun ? Device.InnerCamera.m_fpsRealtime : 10;
                            Record.InnerViewerImage.MoveTargetSync(Device.InnerCamera.m_fpsRealtime);
                        });
                    };

                }));

                //线程：更新显示
                var tView2 = Task.Run((Action)(() => {

                    while (!isQuit) {

                        Thread.Sleep(10);

                        Static.SafeRun(() => {
                            double refFps = Device.OuterCamera.isRun ? Device.OuterCamera.m_fpsRealtime : 10;
                            Record.OuterViewerImage.MoveTargetSync(Device.OuterCamera.m_fpsRealtime);
                        });
                    };

                }));

                //线程：写数据库
                var tWriteDB = Task.Run((Action)(() => {
                    return;
                    do {

                        Static.SafeRun(() => {
                            List<DataGrab> ret1 = null;
                            if (Record.Transaction(() => {
                                ret1 = Record.InnerGrab.Save();
                                Record.InnerDetect.Save();
                            })) {
                                ret1.AsParallel().ForAll(x => x.IsStore = true);
                            }
                        });

                        Thread.Sleep(500);
                    } while (!isQuit);

                }));

                //
                Task.WaitAll(tWriteDB, tView1);
                
            }));
            
        }
        void _init_record() {

            string rPath = Static.FolderRecord + string.Format("[{0}][{1}][{2}].db", rollType, rollName, rollRepeat);

            StringBuilder rBuilder = new StringBuilder(rPath);
            foreach (char rInvalidChar in Path.GetInvalidPathChars())
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);

            Record.Open(rPath);

        }


    }

}
