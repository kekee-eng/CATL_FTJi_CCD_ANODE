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

            if (!isQuit && XtraMessageBox.Show("当前操作将退出视觉检测系统，请确认是否退出？", "退出确认", MessageBoxButtons.OKCancel) != DialogResult.OK) {
                e.Cancel = true;
                return;
            }

            //取消全屏显示
            UtilTool.FullScreen.Set(this, false);

            //
            isQuit = true;
            record?.Dispose();
            device?.Dispose();

            closeLabelCSV();
            closeWidthCSV();
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

            if (InvokeRequired) {
                this.Invoke(new Action(() => appendLog(msg, msgStatus, ex)));
                return;
            }

            //
            status_info.Caption = msg;
            status_info.ItemAppearance.Normal.ForeColor = msgStatus == 0 ? Color.Black : msgStatus > 0 ? Color.Green : Color.Red;

            //添加到日志文件中
            Static.Log.Info(msg);

            if (ex != null)
                Static.Log.Info(ex.StackTrace);

        }
        
        public bool isClear = false;
        bool isQuit = false;
        bool isRollOk = false;

        string rollType = "";
        string rollName = "";

        public ModRecord record = new ModRecord();
        public ModDevice device = new ModDevice();

        void init_server() {

            if (Static.App.Is8K) {

                RemoteDefect.InitServer();
                RemoteDefect._func_in_8k_getDefectCount += (isFront, isInner, start, end, id) => {
                    
                    start = Static.App.FrameInnerToFront(isFront, isInner, start);
                    end = Static.App.FrameInnerToFront(isFront, isInner, end);

                    var ret = (isFront ? record.InnerDetect : record.OuterDetect).AllocAndGetDefectCount(start, end, id);
                    return ret;

                };
                RemoteDefect._func_in_8k_getDefectList += (isFront, isInner, ea) => {
                    
                    var defs = (isFront ? record.InnerDetect : record.OuterDetect).Defects;
                    var outdefs = new List<DataDefect>();
                    for (int i = 0; i < defs.Count; i++) {

                        if (ea != -1 && defs[i].EA != ea)
                            continue;

                        if (!defs[i].InInner(isInner))
                            continue;

                        outdefs.Add(new DataDefect() { EA = defs[i].EA, Y = defs[i].Y, Type = defs[i].Type });
                    }

                    var arr = outdefs.ToArray();
                    for (int i = 0; i < arr.Length; i++)
                        arr[i].Y = Static.App.FrameFrontToInner(isFront, isInner, arr[i].Y);
                    arr = arr.OrderBy(x => x.Y).ToArray();
                    return arr;
                };
                RemoteDefect._func_in_8k_viewer += (isFront, isInner, y, diffInnerOuter, diffFrontBack, diffInnerFront) => {
                    Static.App.DiffFrameInnerOuter = diffInnerOuter;
                    Static.App.DiffFrameFrontBack = diffFrontBack;
                    Static.App.DiffFrameInnerFront = diffInnerFront;
                    (isFront ? record.InnerViewerImage : record.OuterViewerImage).MoveToFrame(
                        Static.App.FrameInnerToFront(isFront, isInner, y));
                };
                RemoteDefect._func_in_8k_init += () => {

                    DeviceInit();
                    DeviceStartGrab();

                    this.Invoke(new Action(() => {
                        groupDevice.Enabled = false;
                        xtraTabControl1.SelectedTabPageIndex = 1;
                    }));
                };
                RemoteDefect._func_in_8k_uninit += () => {
                    DeviceStopGrab();
                    DeviceClose();
                };

            }

        }
        void init_device() {
            
            //
            record.Init();

            //线程：采图
            record.InnerViewerImage.Init(hwinInner);
            device.EventInnerCameraGrab = obj => {
                Static.SafeRun(() => {
                    record.InnerGrab[obj.Frame] = obj;
                    Static.SafeRunThread(obj.DetectTab);
                });
            };
            record.OuterViewerImage.Init(hwinOuter);
            device.EventOuterCameraGrab = obj => {
                Static.SafeRun(() => {
                    record.OuterGrab[obj.Frame] = obj;
                    Static.SafeRunThread(obj.DetectTab);
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
            new Thread(new ThreadStart(() => {
                var detect = record.InnerDetect;
                while (!isQuit) {
                    Thread.Sleep(1);

                        var frame = detect.m_frame;
                        var g = record.InnerGrab.Cache[frame];
                        if (g == null || !g.isDetectTab)
                            continue;

                    Static.SafeRun(() => {
                        if (Static.App.Is4K) {
                            if (g.hasTab && g.TabData != null) {
                                detect.TryTransLabel(frame);
                                lock (record) {
                                    detect.TryAddTab(g.TabData);
                                }
                            }
                        }
                        else {
                            detect.TryAddDefect(g.hasDefect, frame);
                        }
                        detect.m_frame++;
                    });
                };
            })).Start();

            new Thread(new ThreadStart(() => {

                var detect = record.OuterDetect;
                while (!isQuit) {
                    Thread.Sleep(1);
                    var frame = detect.m_frame;
                    var g = record.OuterGrab.Cache[frame];
                    if (g == null || !g.isDetectTab)
                        continue;

                    Static.SafeRun(() => {
                        if (Static.App.Is4K) {
                            if (g.hasTab && g.TabData != null) {
                                detect.TryTransLabel(frame);
                                if (detect.TryAddTab(g.TabData)) {
                                    lock (record) {
                                        detect.TrySync(record.InnerDetect);
                                    }
                                }
                            }
                        }
                        else {
                            detect.TryAddDefect(g.hasDefect, frame);
                        }

                        record.OuterViewerImage.SetBottomTarget(frame);
                        record.InnerViewerImage.SetBottomTarget(frame - detect.DiffFrame);
                        detect.m_frame++;
                    });
                };
            })).Start();
            
            //线程：更新显示
            new Thread(new ThreadStart((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(1);
                    if (checkDisableView.Checked) continue;

                    Static.SafeRun(() => {
                        double refFps = 10.0;
                        if (device.isOpen && device.InnerCamera.isGrabbing)
                            refFps = device.InnerCamera.m_fpsRealtime;

                        record.InnerViewerImage.MoveTargetSync(refFps);
                    });
                };

            }))).Start();
            new Thread(new ThreadStart((Action)(() => {

                while (!isQuit) {
                    Thread.Sleep(1);
                    if (checkDisableView.Checked) continue;

                    Static.SafeRun(() => {
                        double refFps = 10.0;
                        if (device.isOpen && device.OuterCamera.isGrabbing)
                            refFps = device.OuterCamera.m_fpsRealtime;

                        record.OuterViewerImage.MoveTargetSync(refFps);
                    });
                };

            }))).Start();
            
            //PLC操作
            record.InnerDetect.OnNewLabel += obj => {
                new Thread(new ThreadStart(new Action(() => {
                    Static.SafeRun(() => {
                        if (obj.Encoder != 0)
                            RemotePLC.In4KCallPLC_SendLabel(true, obj.Encoder);
                        saveLabelCSV( true, obj);
                    });
                }))).Start();
            };
            record.OuterDetect.OnNewLabel += obj => {

                new Thread(new ThreadStart(new Action(() => {
                    Static.SafeRun(() => {
                        if (obj.Encoder != 0) {
                            RemotePLC.In4KCallPLC_SendLabel(false, obj.Encoder);
                        }
                        saveLabelCSV( false, obj);
                    });
                }))).Start();
                
            };
            record.OuterDetect.OnSyncTab += (tabOuter, tabInner) => {
                
                if (tabOuter.ValWidth == 0 || tabInner.ValWidth == 0)
                    return;

                new Thread(new ThreadStart(new Action(() => {
                    Static.SafeRun(() => {
                        RemotePLC.In4KCallPLC_ForWidth(
                            tabOuter.EA,
                            tabOuter.TAB,
                            !tabOuter.IsWidthFail && !tabInner.IsWidthFail,
                            tabInner.ValWidth,
                            tabOuter.ValWidth
                            );
                        saveWidthCSV( tabInner, tabOuter);
                    });
                }))).Start();

            };

            //

            new Thread(new ThreadStart(new Action(() => {

                bool b1 = false;
                bool b2 = false;

                device.EventInnerCameraComplete += () => b1 = true;
                device.EventOuterCameraComplete += () => b2 = true;
                while (!isQuit)
                {
                    b1 = false;
                    b2 = false;

                    while (!(b1 && b2))
                    {
                        if (isQuit) return;
                        Thread.Sleep(1000);
                    }

                    Thread.Sleep(3000);
                    DeviceInit();
                    
                    Thread.Sleep(3000);
                    DeviceStartGrab();
                }
            }))).Start();
        }
        void init_teston() {

            Static.App.BindTextBox(textRollName, "RollName");

            Static.App.BindCheckBox(checkSaveOK, "RecordSaveImageOK");
            Static.App.BindCheckBox(checkSaveNG, "RecordSaveImageNG");
            Static.App.BindCheckBox(checkSaveNGSmall, "RecordSaveImageNGSmall");
            
            Static.App.BindCheckBox(checkEnableLabelEA, "EnableLabelEA");
            Static.App.BindCheckBox(checkEnableLabelEAEveryOne, "EnableLabelEA_EveryOne");
            Static.App.BindCheckBox(checkEnableLabelEAForce, "EnableLabelEA_Force");
            Static.App.BindCheckBox(checkEnableLabelDefect, "EnableLabelDefect");

            Static.App.BindCheckBox(checkLabelContext_Join, "LabelContextJoin");
            Static.App.BindCheckBox(checkLabelContext_Tag, "LabelContextTag");
            Static.App.BindCheckBox(checkLabelContext_LeakMetal, "LabelContextLeakMetal");

            Static.App.BindCheckBox(checkEAContext_Join, "EAContextJoin");
            Static.App.BindCheckBox(checkEAContext_Tag, "EAContextTag");
            Static.App.BindCheckBox(checkEAContext_LeakMetal, "EAContextLeakMetal");
            Static.App.BindCheckBox(checkEAContext_Width, "EAContextWidth");

            Static.Param.BindTextBox(textLabelEAOffset, "LabelY_EA");
            Static.Param.BindTextBox(textLabelEAForce, "LabelY_EA_Force");
            Static.Param.BindTextBox(textLabelDefectOffset, "LabelY_Defect");

            Static.Param.BindTextBox(textWidthMin, "TabWidthMin");
            Static.Param.BindTextBox(textWidthMax, "TabWidthMax");
            Static.Param.BindTextBox(textWidthStep, "TabWidthStep");
            Static.Param.BindTextBox(textTabCount, "CheckTabCount");
            
        }
        void init_status() {

            //选定用户
            changeUser();
            
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

            runAction("打开离线数据包", () => {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                    record.Dispose();

                    //初始化
                    DeviceInit();

                    //打开设备
                    DeviceOpen();
                }
            });

        }
        public void DeviceInit() {

            runAction("初始化设备", () => {

                //
                ImageProcess.Init();

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

                record.InnerGrab.Cache.Dispose();
                record.OuterGrab.Cache.Dispose();

                record.InnerDetect.Dispose();
                record.OuterDetect.Dispose();

                record.InnerViewerImage.SetBottomTarget(device.InnerCamera.m_frame);
                record.OuterViewerImage.SetBottomTarget(device.InnerCamera.m_frame);

                record.InnerViewerImage.MoveTargetDirect();
                record.OuterViewerImage.MoveTargetDirect();

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
                device?.Dispose();
                record?.Dispose();
                if (Static.App.Is4K)
                    RemotePLC.In4KCallPLC_ClearEncoder();
            });
        }

        StreamWriter csvWidthWriter =null;
        StreamWriter csvLabelWriter =null;
        void closeWidthCSV() {
            if (csvWidthWriter != null) {
                csvWidthWriter.Flush();
                csvWidthWriter.Dispose();
                csvWidthWriter = null;
            }
        }
        void closeLabelCSV() {
            if (csvLabelWriter != null) {
                csvLabelWriter.Flush();
                csvLabelWriter.Dispose();
                csvLabelWriter = null;
            }
        }
        void saveWidthCSV(DataTab inner, DataTab outer) {
            Static.SafeRun(() => {
                if (csvWidthWriter == null) {
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
                    csvWidthWriter = new StreamWriter(path);

                    csvWidthWriter.WriteLine("时间,EA数,内膜宽,外膜宽,内外膜宽差,总膜宽,内膜宽-TARGET,外膜宽-TARGET,内极宽,外极宽,");
                }

                Action<double> appendItem = val => csvWidthWriter.Write(val.ToString("0.000") + ",");

                csvWidthWriter.Write(DateTime.Now.ToString() + ",");
                csvWidthWriter.Write(inner.EA + ",");
                appendItem(inner.ValWidth);
                appendItem(outer.ValWidth);
                appendItem(inner.ValWidth - outer.ValWidth);
                appendItem(inner.ValWidth + outer.ValWidth);
                appendItem(inner.ValWidth - Static.Param.TabWidthTarget);
                appendItem(outer.ValWidth - Static.Param.TabWidthTarget);
                appendItem(inner.ValWidth);
                appendItem(outer.ValWidth);
                csvWidthWriter.WriteLine();
            });
        }
        void saveLabelCSV(bool isInner, DataLabel label) {
            Static.SafeRun(() => {
                if (csvLabelWriter == null) {
                    var folder = Static.FolderTemp + "csv";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\贴标数据-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}.csv",
                        folder,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );
                    csvLabelWriter = new StreamWriter(path);
                    csvLabelWriter.WriteLine("时间,EA数,位置,贴标原因");
                }

                csvLabelWriter.Write(DateTime.Now.ToString() + ",");
                csvLabelWriter.Write(label.EA + ",");
                csvLabelWriter.Write((isInner ? "内侧" : "外侧") + ",");
                csvLabelWriter.Write(label.Comment.Replace(",","|") + ",");
                csvLabelWriter.WriteLine();
            });
        }

        private void timer1_Tick(object sender, EventArgs e) {

            Static.SafeRun(() => {

                groupWidth.Enabled = Static.App.Is4K;
                groupLabel.Enabled = Static.App.Is4K;
                groupRemoteClient.Enabled = Static.App.Is4K;

                if (Static.App.Is4K) {
                    _lc_remote_8k.Text = RemoteDefect.isConnect ? "On" : "Off";
                    _lc_remote_8k.ForeColor = RemoteDefect.isConnect ? Color.Green : Color.Red;

                    _lc_remote_plc.Text = RemotePLC.isConnect ? "On" : "Off";
                    _lc_remote_plc.ForeColor = RemotePLC.isConnect ? Color.Green : Color.Red;

                    if (device.isGrabbing && RemoteDefect.isConnect) {
                        RemotePLC.In4KCallPLC_OnGrabbing();
                    }
                }

                //
                textLabelDefectOffset.Enabled = checkEnableLabelDefect.Checked;
                textLabelEAOffset.Enabled = checkEnableLabelEA.Checked;
                checkEnableLabelEAEveryOne.Enabled = checkEnableLabelEA.Checked;
                checkEnableLabelEAForce.Enabled = checkEnableLabelEA.Checked;

                groupLabelContext.Enabled = checkEnableLabelDefect.Checked;
                groupEAContext.Enabled = checkEnableLabelEA.Checked;
                
                groupRoll.Enabled = Static.App.Is4K && device.isOpen;

                btnStartGrab.Enabled = device.isOpen && !device.isGrabbing;
                btnStopGrab.Enabled = device.isOpen && device.isGrabbing;

                checkSaveOK.Enabled = Static.App.RecordSaveImageEnable;
                checkSaveNG.Enabled = Static.App.RecordSaveImageEnable;
                checkSaveNGSmall.Enabled = Static.App.RecordSaveImageEnable;

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
                    _lc_inner_eaCount.Text = record.InnerDetect.ShowEACountView.ToString();
                    _lc_inner_widthCount.Text = record.InnerDetect.ShowEAWidthNGCount.ToString();
                    _lc_inner_defectCount.Text = record.InnerDetect.ShowEADefectNGCount.ToString();

                    //
                    _lc_outer_camera.Text = device.OuterCamera.Name;
                    //_lc_outer_fps.Text = device.OuterCamera.m_fpsRealtime.ToString("0.000");
                    _lc_outer_frame.Text = device.OuterCamera.m_frame.ToString();
                    _lc_outer_isgrabbing.Text = device.OuterCamera.isGrabbing ? "On" : "Off";
                    _lc_outer_isopen.Text = device.OuterCamera.isOpen ? "On" : "Off";
                    _lc_outer_isgrabbing.ForeColor = device.OuterCamera.isGrabbing ? Color.Green : Color.Red;
                    _lc_outer_isopen.ForeColor = device.OuterCamera.isOpen ? Color.Green : Color.Red;

                    _lc_outer_caption.Text = device.OuterCamera.Caption;
                    _lc_outer_eaCount.Text = record.OuterDetect.ShowEACountView.ToString();
                    _lc_outer_widthCount.Text = record.OuterDetect.ShowEAWidthNGCount.ToString();
                    _lc_outer_defectCount.Text = record.OuterDetect.ShowEADefectNGCount.ToString();
                }

                //状态栏
                status_time.Caption = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                status_device.ImageIndex = device.isOpen ? 5 : 4;
                status_memory.Caption = string.Format("内存已使用={0:0.0}M", UtilPerformance.GetMemoryLoad());
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

            if (!device.isGrabbing) {
                if (!device.isOpen) {
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

                closeLabelCSV();
                closeWidthCSV();
                RemotePLC.In4KCallPLC_ClearEncoder();

                if (!isRollOk) {

                    if (textRollType.SelectedIndex == -1)
                        throw new Exception("料号未设定！");

                    if (textRollName.Text == "")
                        throw new Exception("膜卷号未设定!");

                    //
                    rollType = textRollType.SelectedItem.ToString();
                    rollName = textRollName.Text;
                    
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
            new XFCameraControl(this).Show();
        }
        private void btnStartGrab_Click(object sender, EventArgs e) {
            DeviceStartGrab();
        }
        private void btnStopGrab_Click(object sender, EventArgs e) {
            DeviceStopGrab();
        }
        
        private async void btnConnect_Click(object sender, EventArgs e) {
            UtilTool.XFWait.Open();
            closeLabelCSV();
            closeWidthCSV();
            await Task.Run(() => {
                if (Static.App.Is4K) {
                    Static.SafeRun(RemoteDefect.InitClient);
                    Static.SafeRun(RemotePLC.InitClient);

                    Static.SafeRun(RemoteDefect.In4KCall8K_Init);
                }
                DeviceInit();
                DeviceStartGrab();
                UtilTool.XFWait.Close();
            });
        }
        private async void btnDisconnect_Click(object sender, EventArgs e) {
            UtilTool.XFWait.Open();
            DeviceClose();
            closeLabelCSV();
            closeWidthCSV();

            await Task.Run(() => {
                if (Static.App.Is4K) {
                    Static.SafeRun(RemoteDefect.In4KCall8K_Uninit);
                }

                UtilTool.XFWait.Close();
            });
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

        private async void btnConnectRemote8K_Click(object sender, EventArgs e) {
            if (!RemoteDefect.isConnect) {
                UtilTool.XFWait.Open();
                await Task.Run(() => {
                    runAction((sender as SimpleButton).Text, () => {
                        RemoteDefect.InitClient();
                    });
                    UtilTool.XFWait.Close();
                });
            }
        }
        private async void btnConnectRemotePLC_Click(object sender, EventArgs e) {
            if (!RemotePLC.isConnect) {
                UtilTool.XFWait.Open();
                await Task.Run(() => {
                    runAction((sender as SimpleButton).Text, () => {
                        RemotePLC.InitClient();
                    });
                    UtilTool.XFWait.Close();
                });
            }
        }
        
    }

}
