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
            init_server();
            init_device();
            init_status();
            init_teston();

            //
            UtilTool.AddBuildTag(this);
            this.Text += ((Static.App.Is4K) ? "~[4K]" : "~[8K]");
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
            csvWriter?.Dispose();
        }

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

        public bool isOnline { get { return textMode.SelectedIndex == 0; } }
        public bool isClear = false;
        bool isQuit = false;
        bool isRollOk = false;

        string rollType = "";
        string rollName = "";
        int rollRepeat = 0;

        public ModRecord record = new ModRecord();
        public ModDevice device = new ModDevice();

        void init_server() {

            if (Static.App.Is8K) {

                RemoteDefect.InitServer();
                RemoteDefect._func_in_8k_getDefectCount += (isFront, isInner, start, end, id) => {

                    start = Static.App.FrameInnerToFront(isFront, isInner, start);
                    end = Static.App.FrameInnerToFront(isFront, isInner, end);
                    (isFront ? record.InnerDetect : record.OuterDetect).AllocAndGetDefectCount(start, end, id);

                    //Fix
                    var defs = (isFront ? record.InnerDetect : record.OuterDetect).Defects;
                    int count=0;
                    foreach (var def in defs) {
                        if (def.Type < 2 && def.InInner(isInner)) {
                            count++;
                        }
                    }
                    return count;
                };
                RemoteDefect._func_in_8k_getDefectList += (isFront, isInner) => {

                    var defs = (isFront ? record.InnerDetect : record.OuterDetect).Defects;
                    var outdefs = new List<DataDefect>();
                    foreach (var def in defs) {
                        if (def.Type < 2 && def.InInner(isInner)) {
                            outdefs.Add(new DataDefect() { Y = def.Y });
                        }
                    }

                    var arr = outdefs.ToArray();
                    for (int i = 0; i < arr.Length; i++)
                        arr[i].Y = Static.App.FrameFrontToInner(isFront, isInner, arr[i].Y);
                    arr = arr.OrderBy(x => x.Y).ToArray();
                    return arr;
                };
                RemoteDefect._func_in_8k_viewer += (isFront, isInner, y) => {
                    (isFront ? record.InnerViewerImage : record.OuterViewerImage).MoveToFrame(
                        Static.App.FrameInnerToFront(isFront, isInner, y));
                };

            }
            else {

                //4K
                Static.SafeRun(RemotePLC.InitClient);
                //Static.SafeRun(RemoteDefect.InitClient);

            }
        }
        void init_device() {

            //
            device.GetInnerDb += () => record.InnerGrab.DB;
            device.GetOuterDb += () => record.OuterGrab.DB;

            //
            record.Init();

            //线程：采图
            record.InnerViewerImage.Init(hwinInner);
            device.EventInnerCamera = obj => {
                Static.SafeRun(() => {
                    record.InnerGrab[obj.Frame] = obj;
                });
            };
            record.OuterViewerImage.Init(hwinOuter);
            device.EventOuterCamera = obj => {
                Static.SafeRun(() => {
                    record.OuterGrab[obj.Frame] = obj;
                });
            };

            record.InnerViewerImage.OnViewUpdate += (y, x, s) => {

                Static.SafeRun(() => {
                    if (ckViewLocal.Checked)
                        record.OuterViewerImage.MoveToView(y + Static.App.FixFrameOuterOrBackOffset, x, s);

                    if (Static.App.Is4K) {
                        if (ckViewInnerFront.Checked) RemoteDefect.In4KCall8K_Viewer(true, true, y);
                        if (ckViewInnerBack.Checked) RemoteDefect.In4KCall8K_Viewer(false, true, y);
                    }
                });
            };
            record.OuterViewerImage.OnViewUpdate += (y, x, s) => {

                Static.SafeRun(() => {
                    if (ckViewLocal.Checked)
                        record.InnerViewerImage.MoveToView(y - Static.App.FixFrameOuterOrBackOffset, x, s);

                    if (Static.App.Is4K) {
                        if (ckViewOuterFront.Checked) RemoteDefect.In4KCall8K_Viewer(true, false, y);
                        if (ckViewOuterBack.Checked) RemoteDefect.In4KCall8K_Viewer(false, false, y);
                    }
                });
            };

            //线程：图像处理
            var tProcess1 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);

                    Static.SafeRun(() => {

                        var obj = record.InnerGrab.Cache.GetFirstUnDetect();
                        if (obj != null) {
                            record.InnerDetect.TryDetect(obj);
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
                            if (record.OuterDetect.TryDetect(obj)) {

                                //外侧同步到内侧
                                record.OuterDetect.Sync(record.InnerDetect);

                            }
                            record.OuterViewerImage.SetBottomTarget(obj.Frame);
                            record.InnerViewerImage.SetBottomTarget(obj.Frame - Static.App.FixFrameOuterOrBackOffset);
                        }
                    });
                };

            }));

            //线程：更新显示
            var tView1 = Task.Run((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(10);
                    if (checkDisableView.Checked) continue;

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
                    if (checkDisableView.Checked) continue;

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

            //PLC操作
            record.InnerDetect.OnNewLabel += obj => {
                if (obj != 0)
                    RemotePLC.In4KCallPLC_SendLabel(true, obj);
            };
            record.OuterDetect.OnNewLabel += obj => {
                if (obj != 0)
                    RemotePLC.In4KCallPLC_SendLabel(false, obj);
            };
            record.OuterDetect.OnSyncTab += (tabOuter, tabInner) => {

                if (tabOuter.ValWidth == 0 || tabInner.ValWidth == 0)
                    return;

                saveWidthCSV(tabInner, tabOuter);
                RemotePLC.In4KCallPLC_ForWidth(
                    tabOuter.EA,
                    tabOuter.TAB,
                    !tabOuter.IsWidthFail && !tabInner.IsWidthFail,
                    tabInner.ValWidth,
                    tabOuter.ValWidth
                    );

            };
        }
        void init_teston() {

            checkSaveOK.Checked = Static.App.RecordSaveImageOK;
            checkSaveNG.Checked = Static.App.RecordSaveImageNG;

            checkSaveOK.CheckedChanged += (o, e) => Static.App.RecordSaveImageOK = (o as CheckEdit).Checked;
            checkSaveNG.CheckedChanged += (o, e) => Static.App.RecordSaveImageNG = (o as CheckEdit).Checked;

            checkDetectDefect.Checked = Static.App.DetectDefect;
            checkDetectMark.Checked = Static.App.DetectMark;
            checkDetectTab.Checked = Static.App.DetectTab;
            checkDetectWidth.Checked = Static.App.DetectWidth;

            checkDetectDefect.CheckedChanged += (o, e) => Static.App.DetectDefect = (o as CheckEdit).Checked;
            checkDetectMark.CheckedChanged += (o, e) => Static.App.DetectMark = (o as CheckEdit).Checked;
            checkDetectTab.CheckedChanged += (o, e) => Static.App.DetectTab = (o as CheckEdit).Checked;
            checkDetectWidth.CheckedChanged += (o, e) => Static.App.DetectWidth = (o as CheckEdit).Checked;

            checkEnableLabelEA.Checked = Static.App.EnableLabelEA;
            checkEnableLabelEAEveryOne.Checked = Static.App.EnableLabelEA_EveryOne;
            checkEnableLabelEAForce.Checked = Static.App.EnableLabelEA_Force;
            checkEnableLabelDefect.Checked = Static.App.EnableLabelDefect;

            checkEnableLabelEA.CheckedChanged += (o, e) => Static.App.EnableLabelEA = (o as CheckEdit).Checked;
            checkEnableLabelEAEveryOne.CheckedChanged += (o, e) => Static.App.EnableLabelEA_EveryOne = (o as CheckEdit).Checked;
            checkEnableLabelEAForce.CheckedChanged += (o, e) => Static.App.EnableLabelEA_Force = (o as CheckEdit).Checked;
            checkEnableLabelDefect.CheckedChanged += (o, e) => Static.App.EnableLabelDefect = (o as CheckEdit).Checked;

            Static.Param.BindTextBox(textLabelEAOffset, "LabelY_EA");
            Static.Param.BindTextBox(textLabelEAForce, "LabelY_EA_Force");
            Static.Param.BindTextBox(textLabelDefectOffset, "LabelY_Defect");

            Static.Param.BindTextBox(textWidthMin, "TabWidthMin");
            Static.Param.BindTextBox(textWidthMax, "TabWidthMax");
            Static.Param.BindTextBox(textWidthStep, "TabWidthStep");
        }
        void init_status() {

            //选定用户
            changeUser();

            //
            textMode.SelectedIndex = Static.App.RunningMode;

            //全屏显示
            //selectFullScreen_ItemClick(null, null);
            //status_plc_ItemClick(null, null);

            //
            ViewerChart.InitMergeTabChart(panelTabMergeChart, record.InnerViewerImage, record.OuterViewerImage);
            ViewerChart.InitMergeTabGrid(panelTabMergeGrid, record.InnerViewerImage, record.OuterViewerImage);
            record.InnerViewerChart.InitLabelGrid(panelLabel1);
            record.OuterViewerChart.InitLabelGrid(panelLabel2);
            record.InnerViewerChart.InitDefectGrid(panelDefect1);
            record.OuterViewerChart.InitDefectGrid(panelDefect2);

            //定时器
            timer1.Tag = true;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1_Tick(null, null);

            //
            xtraTabPage7.PageVisible = Static.App.Is8K;
            xtraTabPage6.PageVisible = Static.App.Is4K;
            xtraTabPage5.PageVisible = Static.App.Is4K;
        }
        void changeUser() {
            //隐藏按钮
            selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //隐藏页面
            xtraTabControl1.TabPages.Remove(xtraTabPage3);
            xtraTabControl1.TabPages.Remove(xtraTabPage4);

            int userselect = Static.App.SelectUserId;
            if (userselect == 0) {
                //左下角图标
                status_user.ImageIndex = 0;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.OperatorViewstyle);
            }
            else if (userselect == 1) {
                //左下角图标
                status_user.ImageIndex = 1;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.EngineerViewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
            }
            else {
                //左下角图标
                status_user.ImageIndex = 2;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.ExpertViewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
                xtraTabControl1.TabPages.Add(xtraTabPage4);

                //右下角按钮
                selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        public void DeviceLoad() {

            runAction("打开离线数据包", async () => {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                    UtilTool.XFWait.Open();
                    await Task.Run(() => {

                        record.Dispose();
                        record.Open(openFileDialog1.FileName);

                    });
                    UtilTool.XFWait.Close();
                    richTextBox1.Text = openFileDialog1.FileName;

                    //初始化
                    DeviceInit();

                    //打开设备
                    DeviceOpen();
                }
            });

        }
        public void DeviceInit(bool isClear = false) {

            runAction("初始化设备", () => {

                //
                device.Dispose();

                //
                record.InnerViewerImage.SetBottomTarget(0);
                record.OuterViewerImage.SetBottomTarget(0);
                record.InnerViewerImage.MoveTargetDirect();
                record.OuterViewerImage.MoveTargetDirect();
                
                //
                device.Open();

                device.InnerCamera.Freeze();
                device.OuterCamera.Freeze();

                device.InnerCamera.Reset();
                device.OuterCamera.Reset();

                record.InnerViewerImage.SetUserEnable(true);
                record.OuterViewerImage.SetUserEnable(true);

                this.isClear = isClear;
                if (isClear) {
                    record.InnerGrab.Cache.Dispose();
                    record.OuterGrab.Cache.Dispose();

                    record.InnerDetect.Dispose();
                    record.OuterDetect.Dispose();

                    record.InnerViewerImage.SetBottomTarget(device.InnerCamera.m_frame);
                    record.OuterViewerImage.SetBottomTarget(device.InnerCamera.m_frame);

                    record.InnerViewerImage.MoveTargetDirect();
                    record.OuterViewerImage.MoveTargetDirect();
                }

            });

        }
        public void DeviceStartGrab() {

            runAction("开启采图", () => {
                device.InnerCamera.Grab();
                device.OuterCamera.Grab();

                record.InnerViewerImage.SetUserEnable(false);
                record.OuterViewerImage.SetUserEnable(false);
            });
        }
        public void DeviceStopGrab() {
            runAction("停止采图", () => {
                device.InnerCamera.Freeze();
                device.OuterCamera.Freeze();

                record.InnerViewerImage.SetUserEnable(true);
                record.OuterViewerImage.SetUserEnable(true);
            });
        }
        public void DeviceOpen() {

            runAction("开启设备", async () => {

                UtilTool.XFWait.Open();
                await Task.Run(() => {

                    device.Open();
                    UtilTool.XFWait.Close();

                });
            });
        }
        public void DeviceClose() {
            runAction("停止设备", () => {
                device.Dispose();
            });
        }

        StreamWriter csvWriter;
        void closeWidthCSV() {
            if(csvWriter!=null) {
                csvWriter.Dispose();
                csvWriter = null;
            }
        }
        void saveWidthCSV(DataTab inner, DataTab outer) {
            Static.SafeRun(() => {
                if (csvWriter == null) {
                    var folder = Static.FolderTemp + "csv";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\极耳对应后数据-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}.csv",
                        folder,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );
                    csvWriter = new StreamWriter(path);

                    csvWriter.WriteLine("时间,EA数,内膜宽,外膜宽,内外膜宽差,总膜宽,内膜宽-TARGET,外膜宽-TARGET,内极宽,外极宽,");
                }

                Action<double> appendItem = val => csvWriter.Write(val.ToString("0.000") + ",");

                csvWriter.Write(DateTime.Now.ToString() + ",");
                csvWriter.Write(inner.EA + ",");
                appendItem(inner.ValWidth);
                appendItem(outer.ValWidth);
                appendItem(inner.ValWidth - outer.ValWidth);
                appendItem(inner.ValWidth + outer.ValWidth);
                appendItem(inner.ValWidth - Static.Param.TabWidthTarget);
                appendItem(outer.ValWidth - Static.Param.TabWidthTarget);
                appendItem(inner.ValWidth);
                appendItem(outer.ValWidth);
                csvWriter.WriteLine();
                csvWriter.Flush();
            });
        }

        private void timer1_Tick(object sender, EventArgs e) {

            Static.SafeRun(() => {

                groupWidth.Visible = Static.App.Is4K;
                groupLabel.Visible = Static.App.Is4K;
                groupRemoteClient.Visible = Static.App.Is4K;
                if (Static.App.Is4K) {
                    _lc_remote_8k.Text = RemoteDefect.isConnect ? "On" : "Off";
                    _lc_remote_8k.ForeColor = RemoteDefect.isConnect ? Color.Green : Color.Red;

                    _lc_remote_plc.Text = RemotePLC.isConnect ? "On" : "Off";
                    _lc_remote_plc.ForeColor = RemotePLC.isConnect ? Color.Green : Color.Red;
                }

                //
                textMode.BackColor = isOnline ? Color.LightGreen : Color.Pink;

                textLabelDefectOffset.Enabled = checkEnableLabelDefect.Checked;
                textLabelEAOffset.Enabled = checkEnableLabelEA.Checked;
                textLabelEAForce.Enabled = checkEnableLabelEA.Checked && checkEnableLabelEAForce.Checked;
                checkEnableLabelEAEveryOne.Enabled = checkEnableLabelEA.Checked;
                checkEnableLabelEAForce.Enabled = checkEnableLabelEA.Checked;

                xtraTabControlRoll.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                xtraTabControlRoll.SelectedTabPage = isOnline ? xtraTabPageRollOnline : xtraTabPageRollOffline;

                groupRoll.Enabled = device.isOpen;
                btnConnect.Enabled = isOnline;
                btnDisconnect.Enabled = isOnline;

                if (isOnline) {
                    btnStartGrab.Enabled = device.isOpen && !device.isGrabbing;
                    btnStopGrab.Enabled = device.isOpen && device.isGrabbing;
                }
                else {
                    btnStartGrab.Enabled = isClear && device.isOpen && !device.isGrabbing;
                    btnStopGrab.Enabled = isClear && device.isOpen && device.isGrabbing;
                }

                checkSaveOK.Enabled = Static.App.RecordSaveImageEnable;
                checkSaveNG.Enabled = Static.App.RecordSaveImageEnable;

                checkDetectDefect.Enabled = Static.App.DetectEnable;
                checkDetectMark.Enabled = Static.App.DetectEnable;
                checkDetectTab.Enabled = Static.App.DetectEnable;
                checkDetectWidth.Enabled = Static.App.DetectEnable;

                if (device.isOpen) {
                    //
                    _lc_inner_camera.Text = device.InnerCamera.Name;
                    //_lc_inner_fps.Text = device.InnerCamera.m_fpsRealtime.ToString("0.000");
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
                    //_lc_outer_fps.Text = device.OuterCamera.m_fpsRealtime.ToString("0.000");
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

                //更新表格
                ViewerChart.SyncMergeTabChart(panelTabMergeChart, record.InnerDetect, record.OuterDetect, 0);
                ViewerChart.SyncMergeTabGrid(panelTabMergeGrid, record.InnerDetect, record.OuterDetect);
                record.InnerViewerChart.SyncLabelGrid(panelLabel1);
                record.OuterViewerChart.SyncLabelGrid(panelLabel2);
                record.InnerViewerChart.SyncDefectGrid(panelDefect1);
                record.OuterViewerChart.SyncDefectGrid(panelDefect2);

            });
        }

        private void status_user_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            runAction("切换用户", () => {
                UserSelect.GShow(this);
                changeUser();
            });
        }
        private void status_plc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            if(!device.isGrabbing) {
                if(!device.isOpen) {
                    //DeviceOpen();
                }
            }
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

                closeWidthCSV();
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
                    record.Dispose();

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
            new XFCameraControl(this).Show();
        }
        private void btnStartGrab_Click(object sender, EventArgs e) {
            DeviceStartGrab();
        }
        private void btnStopGrab_Click(object sender, EventArgs e) {
            DeviceStopGrab();
        }

        private void textMode_SelectedIndexChanged(object sender, EventArgs e) {

            device.Dispose();
            if (isOnline) {
                Static.App.RunningMode = 0;
            }
            else {
                Static.App.RunningMode = 1;
            }

        }
        private void btnConnect_Click(object sender, EventArgs e) {
            DeviceOpen();
        }
        private void btnDisconnect_Click(object sender, EventArgs e) {
            DeviceClose();
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

        private void btnConnectRemote8K_Click(object sender, EventArgs e) {

            runAction((sender as SimpleButton).Text, () => {
                RemoteDefect.InitClient();
            });
        }
        private void btnConnectRemotePLC_Click(object sender, EventArgs e) {

            runAction((sender as SimpleButton).Text, () => {
                RemotePLC.InitClient();
            });
        }

    }

}
