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
            init_device();
            init_status();
            init_save();

            //
            UtilTool.XFWait.Close();
        }
        private void XFMain_FormClosing(object sender, FormClosingEventArgs e) {

            if (XtraMessageBox.Show("当前操作将退出视觉检测系统，请确认是否退出？", "退出确认", MessageBoxButtons.OKCancel) != DialogResult.OK) {
                e.Cancel = true;
                return;
            }


            //取消全屏显示
            UtilTool.FullScreen.Set(this, false);

            //
            isQuit = true;
            record?.Dispose();
            device?.Dispose();
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
        void appendLog(string msg, int msgStatus = 0, Exception ex = null) {
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
        bool isRollOk = false;

        string rollType = "";
        string rollName = "";
        int rollRepeat = 0;

        public ModRecord record = new ModRecord();
        public ModDevice device = new ModDevice();

        void init_device() {

            record.Init();

            //线程：采图
            record.InnerViewerImage.Init(hwinInner);
            device.EventInnerCamera = obj => {
                record.InnerGrab[obj.Frame] = obj;
            };
            record.OuterViewerImage.Init(hwinOuter);
            device.EventOuterCamera = obj => {
                record.OuterGrab[obj.Frame] = obj;
            };

            //线程：图像处理
            var tProcess1 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);

                    Static.SafeRun(() => {

                        var obj = record.InnerGrab.Cache.GetFirstUnDetect();
                        if (obj != null) {
                            record.InnerDetect.TryDetect(obj);
                            record.InnerViewerImage.SetBottomTarget(obj.Frame);
                        }
                    });
                };

            }));
            var tProcess2 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);

                    Static.SafeRun(() => {

                        var obj = record.OuterGrab.Cache.GetFirstUnDetect();
                        if (obj != null) {
                            record.OuterDetect.TryDetect(obj);
                            record.OuterViewerImage.SetBottomTarget(obj.Frame);
                        }
                    });
                };

            }));

            //线程：更新显示
            var tView1 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);

                    Static.SafeRun(() => {
                        double refFps = 10.0;
                        if (device.isOpen && device.InnerCamera.isGrabbing)
                            refFps = device.InnerCamera.m_fpsRealtime;

                        record.InnerViewerImage.MoveTargetSync(refFps);
                    });
                };

            }));
            var tView2 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);

                    Static.SafeRun(() => {
                        double refFps = 10.0;
                        if (device.isOpen && device.OuterCamera.isGrabbing)
                            refFps = device.OuterCamera.m_fpsRealtime;

                        record.OuterViewerImage.MoveTargetSync(refFps);
                    });
                };

            }));

            //线程：写数据库
            var tWriteDB = Task.Run((Action)(() => {

                do {
                    Thread.Sleep(100);
                    if (!isRollOk)
                        continue;

                    if (!Static.App.RecordSaveImageEnable)
                        continue;

                    Static.SafeRun(() => {

                        //
                        List<DataGrab> ret1 = null;
                        List<DataGrab> ret2 = null;

                        //
                        if (record.Transaction(() => {

                            ret1 = record.InnerGrab.Save();
                            ret2 = record.OuterGrab.Save();

                            record.InnerDetect.Save();
                            record.OuterDetect.Save();
                        })) {

                            ret1.AsParallel().ForAll(x => x.IsStore = true);
                            ret2.AsParallel().ForAll(x => x.IsStore = true);
                        }

                    });

                } while (!isQuit);

            }));

        }
        void init_save() {

            checkSaveAll.Checked = Static.App.RecordSaveImageAll;
            checkSaveEnable.Checked = Static.App.RecordSaveImageEnable;
            checkSaveDefect.Checked = Static.App.RecordSaveImageDefect;
            checkSaveMark.Checked = Static.App.RecordSaveImageMark;
            checkSaveTab.Checked = Static.App.RecordSaveImageTab;

            checkSaveAll.CheckedChanged += (o, e) => Static.App.RecordSaveImageAll = (o as CheckEdit).Checked;
            checkSaveEnable.CheckedChanged += (o, e) => Static.App.RecordSaveImageEnable = (o as CheckEdit).Checked;
            checkSaveDefect.CheckedChanged += (o, e) => Static.App.RecordSaveImageDefect = (o as CheckEdit).Checked;
            checkSaveMark.CheckedChanged += (o, e) => Static.App.RecordSaveImageMark = (o as CheckEdit).Checked;
            checkSaveTab.CheckedChanged += (o, e) => Static.App.RecordSaveImageTab = (o as CheckEdit).Checked;

        }
        void init_status() {
            //选定用户
            changeUser();

            //
            textMode.SelectedIndex = Static.App.run_mode;

            //全屏显示
            //selectFullScreen_ItemClick(null, null);

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

            int userselect = Static.App.select_userid;
            if (userselect == 0) {
                //左下角图标
                status_user.ImageIndex = 0;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.operator_viewstyle);
            }
            else if (userselect == 1) {
                //左下角图标
                status_user.ImageIndex = 1;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.engineer_viewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
            }
            else {
                //左下角图标
                status_user.ImageIndex = 2;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.expert_viewstyle);

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

                xtraTabControlRoll.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                xtraTabControlRoll.SelectedTabPage = isOnline ? xtraTabPageRollOnline : xtraTabPageRollOffline;

                if (device.isOpen) {
                    //
                    _lc_inner_camera.Text = device.InnerCamera.Name;
                    _lc_inner_fps.Text = device.InnerCamera.m_fpsRealtime.ToString("0.000");
                    _lc_inner_frame.Text = device.InnerCamera.m_frame.ToString();
                    _lc_inner_isgrabbing.Text = device.InnerCamera.isGrabbing ? "On" : "Off";
                    _lc_inner_isopen.Text = device.InnerCamera.isOpen ? "On" : "Off";
                    _lc_inner_isgrabbing.ForeColor = device.InnerCamera.isGrabbing ? Color.Green : Color.Red;
                    _lc_inner_isopen.ForeColor = device.InnerCamera.isOpen ? Color.Green : Color.Red;

                    _lc_inner_caption.Text = device.InnerCamera.Caption;
                    _lc_inner_eaCount.Text = record.InnerDetect.EACount.ToString();
                    _lc_inner_widthCount.Text = record.InnerDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                    _lc_inner_defectCount.Text = record.InnerDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();

                    //
                    _lc_outer_camera.Text = device.OuterCamera.Name;
                    _lc_outer_fps.Text = device.OuterCamera.m_fpsRealtime.ToString("0.000");
                    _lc_outer_frame.Text = device.OuterCamera.m_frame.ToString();
                    _lc_outer_isgrabbing.Text = device.OuterCamera.isGrabbing ? "On" : "Off";
                    _lc_outer_isopen.Text = device.OuterCamera.isOpen ? "On" : "Off";
                    _lc_outer_isgrabbing.ForeColor = device.OuterCamera.isGrabbing ? Color.Green : Color.Red;
                    _lc_outer_isopen.ForeColor = device.OuterCamera.isOpen ? Color.Green : Color.Red;

                    _lc_outer_caption.Text = device.OuterCamera.Caption;
                    _lc_outer_eaCount.Text = record.OuterDetect.EACount.ToString();
                    _lc_outer_widthCount.Text = record.OuterDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                    _lc_outer_defectCount.Text = record.OuterDetect.EAs.Count(x => x.IsTabWidthFailCountFail).ToString();
                }

                //状态栏
                status_time.Caption = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                status_device.ImageIndex = device.isOpen ? 5 : 4;
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
            //runAction("连接设备", () => {
            //    device.Open();
            //});
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

                if (device.isGrabbing)
                    throw new Exception("请先停止采集图像！");

                if (!isRollOk) {

                    if (textRollType.SelectedIndex == -1)
                        throw new Exception("料号未设定！");

                    if (textRollName.Text == "")
                        throw new Exception("膜卷号未设定!");

                    //
                    rollRepeat++;
                    rollType = textRollType.SelectedItem.ToString();
                    rollName = textRollName.Text;
                    textRollRepeat.Text = rollRepeat.ToString();

                    //
                    string rPath = Static.FolderRecord + string.Format("[{0}][{1}][{2}][{3}].db", Static.App.GetPrex(), rollType, rollName, rollRepeat);
                    StringBuilder rBuilder = new StringBuilder(rPath);
                    foreach (char rInvalidChar in Path.GetInvalidPathChars())
                        rBuilder.Replace(rInvalidChar.ToString(), string.Empty);

                    //
                    record.Dispose();
                    record.Open(rPath);

                    //
                    textRollType.Enabled = false;
                    textRollName.Enabled = false;
                    btnRollSet.Text = "结束膜卷";
                    isRollOk = true;
                }
                else {

                    //
                    textRollType.Enabled = true;
                    textRollName.Enabled = true;
                    btnRollSet.Text = "设置膜卷";
                    isRollOk = false;
                }
            });
        }
        private void btnOpenViewerChart_Click(object sender, EventArgs e) {
            new XFViewerChart(device, record).Show();
        }
        private void btnOfflineControl_Click(object sender, EventArgs e) {
            new XFCameraControl(device, record).Show();
        }
        private void btnStartGrab_Click(object sender, EventArgs e) {
            device.InnerCamera.Grab();
            device.OuterCamera.Grab();

            record.InnerViewerImage.SetUserEnable(false);
            record.OuterViewerImage.SetUserEnable(false);
        }
        private void btnStopGrab_Click(object sender, EventArgs e) {
            device.InnerCamera.Freeze();
            device.OuterCamera.Freeze();

            record.InnerViewerImage.SetUserEnable(true);
            record.OuterViewerImage.SetUserEnable(true);
        }

        private void textMode_SelectedIndexChanged(object sender, EventArgs e) {

        }
        private void btnConnect_Click(object sender, EventArgs e) {

            runAction((sender as SimpleButton).Text, async () => {

                UtilTool.XFWait.Open();
                await Task.Run(() => device.Open());
                UtilTool.XFWait.Close();

            });

        }
        private void btnDisconnect_Click(object sender, EventArgs e) {
            device.Dispose();
        }
        private void btnQuit_Click(object sender, EventArgs e) {
            this.Close();
        }
        
        private void groupStatuInner_DoubleClick(object sender, EventArgs e) {
            splitContainerInner.Panel1Collapsed ^= true;
        }
        private void groupStatuOuter_DoubleClick(object sender, EventArgs e) {
            splitContainerOuter.Panel1Collapsed ^= true;
        }

    }

}
