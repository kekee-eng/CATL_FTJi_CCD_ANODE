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
    partial class XMain : DevExpress.XtraEditors.XtraForm
    {
        public XMain()
        {
            InitializeComponent();

            //
            init_server();
            init_device();
            init_status();
            init_recipe();

            //
            UtilTool.AddBuildTag(this);
            this.Text += ((Static.App.Is4K) ? "~[4K]" : "~[8K]");
            UtilTool.XFWait.Close();
        }
        private void XFMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!isQuit && XtraMessageBox.Show("当前操作将退出视觉检测系统，请确认是否退出？", "退出确认", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }

            //取消全屏显示
            UtilTool.FullScreen.Set(this, false);

            //
            isQuit = true;
            process?.Dispose();
            device?.Dispose();

            closeLabelCSV();
            closeWidthCSV();
        }

        void runAction(string actName, Action act)
        {
            try
            {
                appendLog(String.Format("正在{0}...", actName));
                act();
                appendLog(String.Format("{0}成功", actName));
                Log.Operate(actName);
            }
            catch (Exception ex)
            {
                appendLog(String.Format("{0}失败: \r\n{1}", actName, ex.Message), -1, ex);
            }
        }
        void appendLog(string msg, int msgStatus = 0, Exception ex = null)
        {

            if (InvokeRequired)
            {
                this.Invoke(new Action(() => appendLog(msg, msgStatus, ex)));
                return;
            }

            //
            status_info.Caption = msg;
            status_info.ItemAppearance.Normal.ForeColor = msgStatus == 0 ? Color.Black : msgStatus > 0 ? Color.Green : Color.Red;

            //添加到日志文件中
            Log.AppLog.Info(msg);

            if (ex != null)
                Log.AppLog.Info(ex.StackTrace);

        }

        public bool isClear = false;
        bool isQuit = false;
        bool isRollOk = false;

        public ModProcess process = new ModProcess();
        public ModDevice device = new ModDevice();

        void init_server()
        {

            if (Static.App.Is8K)
            {

                RemoteDefect.InitServer();
                RemoteDefect._func_in_8k_getDefectCount += (isFront, isInner, start, end, id) =>
                {

                    start = Static.App.FrameInnerToFront(isFront, isInner, start);
                    end = Static.App.FrameInnerToFront(isFront, isInner, end);
                    lock (process)
                    {
                        var ret = (isFront ? process.InnerDetect : process.OuterDetect).AllocAndGetDefectCount(start, end, id);
                        return ret;
                    }
                };
                RemoteDefect._func_in_8k_getDefectList += (isFront, isInner, ea) =>
                {

                    var defs = (isFront ? process.InnerDetect : process.OuterDetect).Defects;
                    var outdefs = new List<DataDefect>();
                    for (int i = 0; i < defs.Count; i++)
                    {

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
                RemoteDefect._func_in_8k_viewer += (isFront, isInner, y, diffInnerOuter, diffFrontBack, diffInnerFront) =>
                {
                    Static.App.DiffFrameInnerOuter = diffInnerOuter;
                    Static.App.DiffFrameFrontBack = diffFrontBack;
                    Static.App.DiffFrameInnerFront = diffInnerFront;
                    (isFront ? process.InnerViewerImage : process.OuterViewerImage).MoveToFrame(
                        Static.App.FrameInnerToFront(isFront, isInner, y));
                };
                RemoteDefect._func_in_8k_init += () =>
                {

                    DeviceInit();
                    DeviceStartGrab();

                    this.Invoke(new Action(() =>
                    {
                        groupDevice.Enabled = false;
                        xtraTabControl1.SelectedTabPageIndex = 1;
                    }));
                };
                RemoteDefect._func_in_8k_startGrab += DeviceStartGrab;
                RemoteDefect._func_in_8k_stopGrab += DeviceStopGrab;
                RemoteDefect._func_in_8k_uninit += DeviceUninit;

                RemoteDefect._func_in_8k_setRoll += (recipe, roll) =>
                {
                    Static.Recipe.RecipeName = recipe;
                    Static.App.RollName = roll;

                    mainRollName.Text = roll;
                };

            }

        }
        void init_device()
        {

            //初始化
            process.Init();
            process.InnerViewerImage.Init(hwinInner);
            process.OuterViewerImage.Init(hwinOuter);

            //线程：采集图像
            device.EventInnerCameraGrab = obj => Log.Record(() =>
            {
                process.InnerGrab[obj.Frame] = obj;
                Log.RecordAsThread(obj.DetectTab);
            });
            device.EventOuterCameraGrab = obj => Log.Record(() =>
            {
                process.OuterGrab[obj.Frame] = obj;
                Log.RecordAsThread(obj.DetectTab);
            });

            //线程：手动拖动图像
            process.InnerViewerImage.OnViewUpdate += (y, x, s) => Log.Record(() =>
            {
                process.OuterViewerImage.MoveToView(y + Static.App.FixFrameOuterOrBackOffset, x, s);
                if (Static.App.Is4K)
                {
                    RemoteDefect.In4KCall8K_Viewer(true, true, y);
                    RemoteDefect.In4KCall8K_Viewer(false, true, y);
                }
            });
            process.OuterViewerImage.OnViewUpdate += (y, x, s) => Log.Record(() =>
            {
                process.InnerViewerImage.MoveToView(y - Static.App.FixFrameOuterOrBackOffset, x, s);
                if (Static.App.Is4K)
                {
                    RemoteDefect.In4KCall8K_Viewer(true, false, y);
                    RemoteDefect.In4KCall8K_Viewer(false, false, y);
                }
            });

            //线程：图像处理
            Log.RecordAsThread(() =>
            {
                var detect = process.InnerDetect;
                while (!isQuit)
                {
                    Thread.Sleep(1);

                    var frame = detect.m_frame;
                    var g = process.InnerGrab.Cache[frame];
                    if (g == null || !g.isDetectTab)
                        continue;

                    Log.Record(() =>
                    {
                        if (Static.App.Is4K)
                        {
                            if (g.hasTab && g.TabData != null)
                            {
                                detect.TryTransLabel(frame);
                                lock (process)
                                {
                                    detect.TryAddTab(g.TabData);
                                }
                            }
                        }
                        else
                        {
                            detect.TryAddDefect(g.hasDefect, frame);
                        }
                        detect.m_frame++;
                    });
                };
            });
            Log.RecordAsThread(() =>
            {

                var detect = process.OuterDetect;
                while (!isQuit)
                {
                    Thread.Sleep(1);
                    var frame = detect.m_frame;
                    var g = process.OuterGrab.Cache[frame];
                    if (g == null || !g.isDetectTab)
                        continue;

                    Log.Record(() =>
                    {
                        if (Static.App.Is4K)
                        {
                            if (g.hasTab && g.TabData != null)
                            {
                                detect.TryTransLabel(frame);
                                if (detect.TryAddTab(g.TabData))
                                {
                                    lock (process)
                                    {
                                        detect.TrySync(process.InnerDetect,process.OuterDetect);
                                    }
                                }
                            }
                        }
                        else
                        {
                            detect.TryAddDefect(g.hasDefect, frame);
                        }

                        process.OuterViewerImage.SetBottomTarget(frame);
                        process.InnerViewerImage.SetBottomTarget(frame - detect.DiffFrame);
                        detect.m_frame++;
                    });
                };
            });

            //线程：更新显示
            Log.RecordAsThread(() =>
            {
                while (!isQuit)
                {
                    Thread.Sleep(10);
                    process.InnerViewerImage.MoveTargetSync();
                };
            });
            Log.RecordAsThread(() =>
            {
                while (!isQuit)
                {
                    Thread.Sleep(10);
                    process.OuterViewerImage.MoveTargetSync();
                };
            });

            //PLC交互操作
            process.InnerDetect.OnNewLabel += obj => Log.RecordAsThread(() =>
            {
                if (obj.Encoder != 0)
                    RemotePLC.In4KCallPLC_SendLabel(true, obj.Encoder);

                saveLabelCSV(true, obj);
            });
            process.OuterDetect.OnNewLabel += obj => Log.RecordAsThread(() =>
            {
                if (obj.Encoder != 0)
                    RemotePLC.In4KCallPLC_SendLabel(false, obj.Encoder);

                saveLabelCSV(false, obj);
            });
            process.OuterDetect.OnSyncTab += (tabOuter, tabInner) => Log.RecordAsThread(() =>
            {
                RemotePLC.In4KCallPLC_ForWidth(
                    tabOuter.EA,
                    tabOuter.TAB,
                    !tabOuter.IsWidthFail && !tabInner.IsWidthFail,
                    tabInner.ValWidth,
                    tabOuter.ValWidth
                    );
              
                if (tabOuter.IsNewEA)
                {
                    //
                    var eaInner = process.InnerDetect.EAs[tabInner.EA-1];
                    var eaOuter = process.OuterDetect.EAs[tabOuter.EA-1];
                    if(eaInner.IsFail)
                    {
                        FilmData.InnerLableNum += 1;
                    }
                    if(eaOuter.IsFail)
                    {
                        FilmData.OuterLableNum += 1;
                    }
                    saveLablCountCsv(eaInner, eaOuter);
                }
                saveWidthCSV(tabInner, tabOuter);
            });

            //线程：离线循环测试
            Log.RecordAsThread(new Action(() =>
            {
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

                    if (Static.App.TestZipFileCircle)
                    {
                        Thread.Sleep(3000);
                        DeviceInit();

                        Thread.Sleep(3000);
                        DeviceStartGrab();
                    }
                }
            }));
        }
        void init_recipe()
        {

            //公用变量
            Static.App.BindTextBox(mainRollName, "RollName");
            Static.App.BindTextBox(mainMachineNum, "MachineNum");
            Static.App.BindTextBox(mainEmployeeNum, "EmployeeNum");
            Static.App.BindCheckBox(checkSaveNG, "RecordSaveImageNGBig");
            Static.App.BindCheckBox(checkSaveNGSmall, "RecordSaveImageNGSmall");
            Static.App.BindCheckBox(checkSaveMark, "RecordSaveImageMark");

            //设置参数
            tmpRecipe = new CfgRecipe(Static.PathCfgRecipe);
            tmpTiebiao = new CfgTiebiao(Static.PathCfgTiebiao);

            tmpTiebiao.BindCheckBox(checkEnableLabelEA, "EnableLabelEA");
            tmpTiebiao.BindCheckBox(checkEnableLabelEAForce, "EnableLabelEA_Force");
            tmpTiebiao.BindCheckBox(checkEnableLabelDefect, "EnableLabelDefect");

            tmpTiebiao.BindCheckBox(checkLabelContext_Join, "LabelContextJoin");
            tmpTiebiao.BindCheckBox(checkLabelContext_Tag, "LabelContextTag");
            tmpTiebiao.BindCheckBox(checkLabelContext_LeakMetal, "LabelContextLeakMetal");

            tmpTiebiao.BindCheckBox(checkEAContext_Join, "EAContextJoin");
            tmpTiebiao.BindCheckBox(checkEAContext_Tag, "EAContextTag");
            tmpTiebiao.BindCheckBox(checkEAContext_LeakMetal, "EAContextLeakMetal");
            tmpTiebiao.BindCheckBox(checkEAContext_Width, "EAContextWidth");

            tmpTiebiao.BindTextBox(textLabelEAOffset, "LabelY_EA");
            tmpTiebiao.BindTextBox(textLabelDefectOffset, "LabelY_Defect");

            tmpRecipe.BindTextBox(textRecipeName, "RecipeName");
            tmpRecipe.BindTextBox(textWidthMin, "TabWidthMin");
            tmpRecipe.BindTextBox(textWidthMax, "TabWidthMax");
            tmpRecipe.BindTextBox(textWidthStep, "TabWidthStep");
            tmpRecipe.BindTextBox(textTabCount, "CheckTabCount");
            tmpRecipe.BindTextBox(textEALength, "EALength");

            tmpRecipe.BindDataGridView(dataRecipe);

            //初始化
            updateRecipes();
        }
        void init_status()
        {

            //选定用户
            changeUser();

            //全屏显示
            //selectFullScreen_ItemClick(null, null);
            //status_plc_ItemClick(null, null);

            //
            ViewerChart.InitMergeTabChart(panelTabMergeChart, process.InnerViewerImage, process.OuterViewerImage);
            ViewerChart.InitMergeTabGrid(panelTabMergeGrid, process.InnerViewerImage, process.OuterViewerImage);
            process.InnerViewerChart.InitLabelGrid(panelLabel1);
            process.OuterViewerChart.InitLabelGrid(panelLabel2);
            process.InnerViewerChart.InitDefectGrid(panelDefect1);
            process.OuterViewerChart.InitDefectGrid(panelDefect2);

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
        void changeUser()
        {
            //隐藏按钮
            selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            selectFullScreen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //隐藏页面
            xtraTabControl1.TabPages.Remove(xtraTabPage3);
            xtraTabControl1.TabPages.Remove(xtraTabPage4);

            int userselect = Static.App.SelectUserId;
            if (userselect == 0)
            {
                //左下角图标
                status_user.ImageIndex = 0;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.OperatorViewstyle);
            }
            else if (userselect == 1)
            {
                //左下角图标
                status_user.ImageIndex = 1;

                //主题样式
                UtilTool.XFSkin.SetSkinStyle(Static.App.EngineerViewstyle);

                //附加页面
                xtraTabControl1.TabPages.Add(xtraTabPage3);
            }
            else
            {
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

        public void DeviceInit()
        {

            runAction("开启设备", () =>
            {

                if (Static.App.Is4K)
                {

                    closeLabelCSV();
                    closeWidthCSV();

                    Log.Record(RemoteDefect.InitClient);
                    Log.Record(RemotePLC.InitClient);
                    Log.Record(RemoteDefect.In4KCall8K_Init);
                    Log.Record(() => RemoteDefect.In4KCall8K_SetRoll(Static.Recipe.RecipeName, Static.App.RollName));

                }
                //else
                //{
                //    //8K
                //    runAction("内侧缺陷保存", () =>
                //    {
                //        foreach (var item in process.InnerDetect.Defects)
                //        {
                //            lock(process)
                //            {
                //                saveDetectCSV(item,"内侧");
                //            }
                            
                //        }
                //        closeDetectCSV();
                        
                //    });
                //    runAction("外侧缺陷保存", () =>
                //    {
                        
                //        foreach (var item in process.OuterDetect.Defects)
                //        {
                //            lock (process)
                //            {
                //                saveDetectCSV(item, "外侧");
                //            }

                //        }
                //        closeDetectCSV();
                //    });
                //}

                //
                ImageProcess.Init();

                //
                device.Dispose();

                //
                process.InnerViewerImage.SetBottomTarget(0);
                process.OuterViewerImage.SetBottomTarget(0);
                process.InnerViewerImage.MoveTargetDirect();
                process.OuterViewerImage.MoveTargetDirect();

                //
                device.Open();

                device.InnerCamera.Freeze();
                device.OuterCamera.Freeze();

                device.InnerCamera.Reset();
                device.OuterCamera.Reset();

                process.InnerViewerImage.SetUserEnable(true);
                process.OuterViewerImage.SetUserEnable(true);

                process.InnerGrab.Cache.Dispose();
                process.OuterGrab.Cache.Dispose();

                process.InnerDetect.Dispose();
                process.OuterDetect.Dispose();

                process.InnerViewerImage.SetBottomTarget(device.InnerCamera.m_frame);
                process.OuterViewerImage.SetBottomTarget(device.InnerCamera.m_frame);

                process.InnerViewerImage.MoveTargetDirect();
                process.OuterViewerImage.MoveTargetDirect();

            });

        }
        public void DeviceStartGrab()
        {

            runAction("开启采图", () =>
            {

                if (Static.App.Is4K)
                {
                    if (!isRollOk)
                    {
                        throw new Exception("请先设置膜卷！");
                    }
                }

                device.InnerCamera.Grab();
                device.OuterCamera.Grab();

                process.InnerViewerImage.SetUserEnable(false);
                process.OuterViewerImage.SetUserEnable(false);

                if (Static.App.Is4K)
                {
                    Log.Record(RemoteDefect.In4KCall8K_StartGrab);
                    Log.Record(() => RemoteDefect.In4KCall8K_SetRoll(Static.Recipe.RecipeName, Static.App.RollName));
                }
            });
        }
        public void DeviceStopGrab()
        {
            runAction("停止采图", () =>
            {
                device.InnerCamera.Freeze();
                device.OuterCamera.Freeze();

                process.InnerViewerImage.SetUserEnable(true);
                process.OuterViewerImage.SetUserEnable(true);

                if (Static.App.Is4K)
                {
                    Log.Record(RemoteDefect.In4KCall8K_StopGrab);
                }
                else
                {
                    //8K
                    runAction("内侧缺陷保存", () =>
                    {
                        foreach (var item in process.InnerDetect.Defects)
                        {
                            lock (process)
                            {
                                saveDetectCSV(item, "内侧");
                            }

                        }
                        closeDetectCSV();

                    });
                    runAction("外侧缺陷保存", () =>
                    {

                        foreach (var item in process.OuterDetect.Defects)
                        {
                            lock (process)
                            {
                                saveDetectCSV(item, "外侧");
                            }

                        }
                        closeDetectCSV();
                    });
                }

            });
        }
        public void DeviceUninit()
        {
            runAction("关闭设备", () =>
            {

                device?.Dispose();
                process?.Dispose();

                if (Static.App.Is4K)
                {
                    Log.Record(RemoteDefect.In4KCall8K_Uninit);
                    Log.Record(RemotePLC.In4KCallPLC_ClearEncoder);
                }
            });
        }

        StreamWriter csvWidthWriter = null;
        StreamWriter csvLabelWriter = null;
        StreamWriter csvDetectWriter = null;
        void closeWidthCSV()
        {
            if (csvWidthWriter != null)
            {
                csvWidthWriter.Flush();
                csvWidthWriter.Dispose();
                csvWidthWriter = null;
            }
        }
        void closeLabelCSV()
        {
            if (csvLabelWriter != null)
            {
                csvLabelWriter.Flush();
                csvLabelWriter.Dispose();
                csvLabelWriter = null;
            }
        }
        void closeDetectCSV()
        {
            if (csvDetectWriter != null)
            {
                csvDetectWriter.Flush();
                csvDetectWriter.Dispose();
                csvDetectWriter = null;
            }
        }
        void saveDetectCSV(DataDefect dt,string name)
        {
            Log.Record(() =>
            {
                if (csvDetectWriter == null)
                {
                    var folder = Static.FolderRecord;
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\{1}_缺陷-{2:D2}-{3:D2}_{4:D2}-{5:D2}-{6:D2}.csv",
                        folder,                        
                        name,                       
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );
                    csvDetectWriter = new StreamWriter(path);
                    csvDetectWriter.WriteLine("EA数序号,类型,X,Y,宽度,高度,面积,");

                }

                Action<double> appendItem = val => csvDetectWriter.Write(val.ToString("0.000") + ",");
                          
                appendItem(dt.EA);
                csvDetectWriter.Write(dt.GetTypeCaption()+",");
               // appendItem(dt.Type);
                appendItem(dt.X);
                appendItem(dt.Y);
                appendItem(dt.Width);
                appendItem(dt.Height);
                appendItem(dt.Area);
                csvDetectWriter.WriteLine();

            });
        }
        void saveWidthCSV(DataTab inner, DataTab outer)
        {
            Log.Record(() =>
            {
                if (csvWidthWriter == null)
                {
                    var folder = Static.FolderRecord;
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\{1}_{2}-{3:D2}-{4:D2}_{5:D2}-{6:D2}-{7:D2}.csv",
                        folder,
                        Static.App.MachineNum,
                        Static.App.RollName,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );

                    FilmData.FilePathWidth = path.Replace("..", System.Windows.Forms.Application.StartupPath).Replace("\\Bin", "");
                    csvWidthWriter = new StreamWriter(path);
                    csvWidthWriter.WriteLine("膜卷号," + FilmData.FilmNum);
                    csvWidthWriter.WriteLine("品种," + "NA");
                    csvWidthWriter.WriteLine("分条机号," + FilmData.MachineNum);
                    csvWidthWriter.WriteLine("开始时间," + FilmData.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    csvWidthWriter.WriteLine("结束时间," + "还未结束");
                    csvWidthWriter.WriteLine("分条膜宽目标," + Static.Recipe.TabWidthTarget);
                    csvWidthWriter.WriteLine("膜宽上限," + Static.Recipe.TabWidthMax);
                    csvWidthWriter.WriteLine("膜宽下限," + Static.Recipe.TabWidthMin);
                    csvWidthWriter.WriteLine("极宽上限," + "NA");
                    csvWidthWriter.WriteLine("极宽下限," + "NA");
                    csvWidthWriter.WriteLine("分条极宽目标," + "NA");
                    csvWidthWriter.WriteLine("优率," + "正在计算");
                    csvWidthWriter.WriteLine("EA总数," + "正在计算");
                    csvWidthWriter.WriteLine("内打标数," + "正在计算");
                    csvWidthWriter.WriteLine("外打标数," + "正在计算");
                    csvWidthWriter.WriteLine("");
                    csvWidthWriter.WriteLine("时间,EA数序号,极耳序号,内膜宽,外膜宽,内AT9,外AT9,内极宽,外极宽,总膜宽,总极宽,缺陷类型,打标数目,");

                }

                Action<double> appendItem = val => csvWidthWriter.Write(val.ToString("0.000") + ",");

                //csvWidthWriter.Write(DateTime.Now.ToString() + ",");
                csvWidthWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                appendItem(inner.EA);
                appendItem(inner.TAB);
                appendItem(inner.ValWidth);
                appendItem(outer.ValWidth);
                csvWidthWriter.Write("NA" + ",");
                csvWidthWriter.Write("NA" + ",");
                csvWidthWriter.Write("NA" + ",");
                csvWidthWriter.Write("NA" + ",");
                appendItem(inner.ValWidth + outer.ValWidth);
                csvWidthWriter.Write("NA" + ",");
                csvWidthWriter.WriteLine();

            });
        }
        void saveLablCountCsv(DataEA inner, DataEA outer)
        {
            Log.Record(() =>
            {
                if (csvWidthWriter == null)
                {
                    var folder = Static.FolderRecord;
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\{1}_{2}-{3:D2}-{4:D2}_{5:D2}-{6:D2}-{7:D2}.csv",
                        folder,
                        Static.App.MachineNum,
                        Static.App.RollName,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );
                  //  FilmData.FilePathWidth = path;
                    csvWidthWriter = new StreamWriter(path);
                    csvWidthWriter.WriteLine("膜卷号," + FilmData.FilmNum);
                    csvWidthWriter.WriteLine("品种," + "NA");
                    csvWidthWriter.WriteLine("分条机号," + FilmData.MachineNum);
                    csvWidthWriter.WriteLine("开始时间," + FilmData.StartTime);
                    csvWidthWriter.WriteLine("结束时间," + "还未结束");
                    csvWidthWriter.WriteLine("分条膜宽目标," + Static.Recipe.TabWidthTarget);
                    csvWidthWriter.WriteLine("膜宽上限," + Static.Recipe.TabWidthMax);
                    csvWidthWriter.WriteLine("膜宽下限," + Static.Recipe.TabWidthMin);
                    csvWidthWriter.WriteLine("极宽上限," + "NA");
                    csvWidthWriter.WriteLine("极宽下限," + "NA");
                    csvWidthWriter.WriteLine("分条极宽目标," + "NA");
                    csvWidthWriter.WriteLine("优率," + "正在计算");
                    csvWidthWriter.WriteLine("EA总数," + "正在计算");
                    csvWidthWriter.WriteLine("内打标数," + "正在计算");
                    csvWidthWriter.WriteLine("外打标数," + "正在计算");
                    csvWidthWriter.WriteLine("");
                    csvWidthWriter.WriteLine("时间,EA数序号,极耳序号,内膜宽,外膜宽,内AT9,外AT9,内极宽,外极宽,总膜宽,总极宽,缺陷类型,打标数目,");
                }

                csvWidthWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                csvWidthWriter.Write(inner.EA + ",");
                csvWidthWriter.Write("内侧" + ",");
                csvWidthWriter.Write("标签" + (inner.DefectCountBack_Tag + inner.DefectCountFront_Tag).ToString() + ",");
                csvWidthWriter.Write("漏金属" + (inner.DefectCountBack_LeakMetal + inner.DefectCountFront_LeakMetal).ToString() + ",");
                csvWidthWriter.Write("接带" + (inner.DefectCountBack_Join + inner.DefectCountFront_Join).ToString() + ",");
                csvWidthWriter.Write("宽度不良" + inner.TabWidthFailCount + ",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write((outer.IsFail ? "1" : "0") + ",");
                csvWidthWriter.WriteLine();
                csvWidthWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                csvWidthWriter.Write(inner.EA + ",");
                csvWidthWriter.Write("外侧" + ",");
                csvWidthWriter.Write("标签" + (outer.DefectCountBack_Tag + outer.DefectCountFront_Tag).ToString() + ",");
                csvWidthWriter.Write("漏金属" + (outer.DefectCountBack_LeakMetal + outer.DefectCountFront_LeakMetal).ToString() + ",");
                csvWidthWriter.Write("接带" + (outer.DefectCountBack_Join + outer.DefectCountFront_Join).ToString() + ",");
                csvWidthWriter.Write("宽度不良" + outer.TabWidthFailCount.ToString() + ",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write(",");
                csvWidthWriter.Write((outer.IsFail ? "1" : "0") + ",");
                csvWidthWriter.WriteLine();

            });
        }
        void saveFinalCsv(string filePathName, bool append)
        {
            Log.Record(() =>
            {
                List<String[]> ls = new List<String[]>();
                StreamReader fileReader = new StreamReader(filePathName);
                string strLine = "";
                while (strLine != null)
                {
                    strLine = fileReader.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        ls.Add(strLine.Split(','));

                    }
                }
                fileReader.Close();
                ls[4][1] = FilmData.StopTime.ToString("yyyy-MM-dd HH:mm:ss");
                ls[11][1] = ((double)FilmData.EaOkNum /(double) (FilmData.EaNgNum+ FilmData.EaOkNum)).ToString("00.00%");
                ls[12][1] = (FilmData.EaNgNum + FilmData.EaOkNum).ToString();
                ls[13][1] = FilmData.InnerLableNum.ToString();
                ls[14][1] = FilmData.OuterLableNum.ToString();
                StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
                foreach (String[] strArr in ls)
                {
                    fileWriter.WriteLine(String.Join(",", strArr));
                }
                fileWriter.Flush();
                fileWriter.Close();
            });
        }
        void saveLabelCSV(bool isInner, DataLabel label)
        {
            Log.Record(() =>
            {
                if (csvLabelWriter == null)
                {
                    var folder = Static.FolderRecord;
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    var path = string.Format("{0}\\{1}_不良数据-{2:D2}-{3:D2}_{4:D2}-{5:D2}-{6:D2}.csv",
                        folder,
                        Static.App.RollName,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        DateTime.Now.Second
                        );
                  //  FilmData.FilePathWidth = path;
                    csvLabelWriter = new StreamWriter(path);
                    csvLabelWriter.WriteLine("时间,EA数,位置,贴标原因");

                }

                csvLabelWriter.Write(DateTime.Now.ToString() + ",");
                csvLabelWriter.Write(label.EA + ",");
                csvLabelWriter.Write((isInner ? "内侧" : "外侧") + ",");
                csvLabelWriter.Write(label.Comment.Replace(",", "|") + ",");
                csvLabelWriter.WriteLine();
            });
        }
        /// <summary>
        /// 界面显示打标项
        /// </summary>
        /// <returns></returns>
        public string tiebiao()
        {
            string tt = "";
            string t1, t2, t3, t4;
            if (tmpTiebiao.EAContextJoin == true)
            {
                t1 = "接头，";
            }
            else
            {
                t1 = "";
            }
            if (tmpTiebiao.EAContextTag == true)
            {
                t2 = "标签，";
            }
            else
            {
                t2 = "";
            }
            if (tmpTiebiao.EAContextLeakMetal == true)
            {
                t3 = "漏金属，";
            }
            else
            {
                t3 = "";
            }
            if (tmpTiebiao.EAContextWidth == true)
            {
                t4 = "宽度不良，";
            }
            else
            {
                t4 = "";
            }
            return tt = string.Format("末尾贴标项启用:{0}{1}{2}{3}", t1, t2, t3, t4);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            Log.Record(() =>
            {
                
                groupRecipeManage.Enabled = Static.App.Is4K && !device.isGrabbing && !isRollOk;              
                groupLabel.Enabled = Static.App.Is4K;
                groupRemoteClient.Enabled = Static.App.Is4K;
                groupRoll.Enabled = Static.App.Is4K;

                if (Static.App.Is4K)
                {
                    _lc_remote_8k.Text = RemoteDefect.isConnect ? "On" : "Off";
                    _lc_remote_8k.ForeColor = RemoteDefect.isConnect ? Color.Green : Color.Red;

                    _lc_remote_plc.Text = RemotePLC.isConnect ? "On" : "Off";
                    _lc_remote_plc.ForeColor = RemotePLC.isConnect ? Color.Green : Color.Red;

                    if (device.isGrabbing && RemoteDefect.isConnect)
                    {
                        RemotePLC.In4KCallPLC_OnGrabbing();
                    }
                    btnDisconnect.Enabled = !isRollOk;

                }

                //
                textLabelDefectOffset.Enabled = checkEnableLabelDefect.Checked;
                textLabelEAOffset.Enabled = checkEnableLabelEA.Checked;
                checkEnableLabelEAForce.Enabled = checkEnableLabelEA.Checked;

                groupLabelContext.Enabled = checkEnableLabelDefect.Checked;
                groupEAContext.Enabled = checkEnableLabelEA.Checked;

                if (isRollOk)
                {
                    btnStartGrab.Enabled = device.isOpen && !device.isGrabbing&&!isRepeatRoll;
                }
                else
                {
                    btnStartGrab.Enabled = false;
                }
               
                btnStopGrab.Enabled = device.isOpen && device.isGrabbing;

                checkSaveNG.Enabled = Static.App.RecordSaveImageEnable;
                checkSaveNGSmall.Enabled = Static.App.RecordSaveImageEnable;
                checkSaveMark.Enabled= Static.App.RecordSaveImageEnable;

                if (device.isOpen)
                {
                    //
                    _lc_inner_camera.Text = device.InnerCamera.Name;
                    //_lc_inner_fps.Text = device.InnerCamera.m_fpsRealtime.ToString("0.000");
                    _lc_inner_frame.Text = device.InnerCamera.m_frame.ToString();
                    _lc_inner_isgrabbing.Text = device.InnerCamera.isGrabbing ? "On" : "Off";
                    _lc_inner_isopen.Text = device.InnerCamera.isOpen ? "On" : "Off";
                    _lc_inner_isgrabbing.ForeColor = device.InnerCamera.isGrabbing ? Color.Green : Color.Red;
                    _lc_inner_isopen.ForeColor = device.InnerCamera.isOpen ? Color.Green : Color.Red;

                    _lc_inner_caption.Text = device.InnerCamera.Caption;
                    _lc_inner_eaCount.Text = process.InnerDetect.ShowEACountView.ToString();
                    _lc_inner_widthCount.Text = process.InnerDetect.ShowEAWidthNGCount.ToString();
                    _lc_inner_defectCount.Text = process.InnerDetect.ShowEADefectNGCount.ToString();

                    //
                    _lc_outer_camera.Text = device.OuterCamera.Name;
                    _lc_outer_frame.Text = device.OuterCamera.m_frame.ToString();
                    _lc_outer_isgrabbing.Text = device.OuterCamera.isGrabbing ? "On" : "Off";
                    _lc_outer_isopen.Text = device.OuterCamera.isOpen ? "On" : "Off";
                    _lc_outer_isgrabbing.ForeColor = device.OuterCamera.isGrabbing ? Color.Green : Color.Red;
                    _lc_outer_isopen.ForeColor = device.OuterCamera.isOpen ? Color.Green : Color.Red;

                    _lc_outer_caption.Text = device.OuterCamera.Caption;
                    _lc_outer_eaCount.Text = process.OuterDetect.ShowEACountView.ToString();
                    _lc_outer_widthCount.Text = process.OuterDetect.ShowEAWidthNGCount.ToString();
                    _lc_outer_defectCount.Text = process.OuterDetect.ShowEADefectNGCount.ToString();
                }

                //状态栏
                status_time.Caption = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                status_device.ImageIndex = device.isOpen ? 5 : 4;
                status_memory.Caption = string.Format("内存已使用={0:0.0}M", UtilPerformance.GetMemoryLoad());
                status_diskspace.Caption = string.Format("硬盘剩余空间={0:0.0}G", UtilPerformance.GetDiskFree(Application.StartupPath[0].ToString()));
                if (tmpTiebiao!=null&& tmpTiebiao.EnableLabelEA)
                {
                    status_OpenTiebiao.Caption = tiebiao();
                }
                else
                {
                    status_OpenTiebiao.Caption = "末尾贴标未启用";
                }

                //更新表格
                ViewerChart.SyncMergeTabChart(panelTabMergeChart, process.InnerDetect, process.OuterDetect, 0);
                ViewerChart.SyncMergeTabGrid(panelTabMergeGrid, process.InnerDetect, process.OuterDetect);
                process.InnerViewerChart.SyncLabelGrid(panelLabel1);
                process.OuterViewerChart.SyncLabelGrid(panelLabel2);
                process.InnerViewerChart.SyncDefectGrid(panelDefect1);
                process.OuterViewerChart.SyncDefectGrid(panelDefect2);

            });
        }

        private void status_user_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            runAction("切换用户", () =>
            {
                UserSelect.GShow(this);
                changeUser();
            });
        }
        private void status_plc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!device.isGrabbing)
            {
                if (!device.isOpen)
                {
                    //DeviceOpen();
                }
            }
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
        FilmLevel FilmData;
        bool isRepeatRoll = false;     
        private void btnRollSet_Click(object sender, EventArgs e)
        {
            runAction((sender as SimpleButton).Text, () =>
            {

                if (device.isGrabbing)
                    throw new Exception("请先停止采集图像！");

                closeLabelCSV();
                closeWidthCSV();
                RemotePLC.In4KCallPLC_ClearEncoder();

                if (!isRollOk)
                {
                    FilmData = new FilmLevel();
                    if (mainRecipeName.Text == "")
                        throw new Exception("料号未设定!");

                    if (mainRollName.Text == "")
                        throw new Exception("膜卷号未设定!");
                    FilmData.FilmNum = mainRollName.Text;
                    FilmData.MachineNum = mainMachineNum.Text;
                    FilmData.EmployeeNum = mainEmployeeNum.Text;
                    FilmData.StartTime = DateTime.Now;
                    //
                    mainRollName.Enabled = false;
                    mainEmployeeNum.Enabled = false;
                    mainMachineNum.Enabled = false;
                    btnRollSet.Text = "结束膜卷";

                    //
                    RemoteDefect.In4KCall8K_SetRoll(Static.Recipe.RecipeName, Static.App.RollName);

                    //
                    isRollOk = true;
                }
                else
                {

                    //
                    try
                    {
                        if(Static.App.Is4K && !isRepeatRoll && process.InnerDetect.Tabs.Count>0&&process.OuterDetect.Tabs.Count>0)
                        {
                            FilmData.StopTime = DateTime.Now;
                            var FilmInnerWidthList = process.InnerDetect.Tabs.Select(x => x.ValWidth).Where(x => Math.Abs(x - Static.Recipe.TabWidthTarget) < 2);
                            var FilmInnerWidthMean = FilmInnerWidthList.Average();
                            var FilmInnerWidthSigma = Math.Sqrt(FilmInnerWidthList.Select(x => (x - FilmInnerWidthMean) * (x - FilmInnerWidthMean)).Average());

                            var FilmOuterWidthList = process.OuterDetect.Tabs.Select(x => x.ValWidth).Where(x => Math.Abs(x - Static.Recipe.TabWidthTarget) < 2);
                            var FilmOuterWidthMean = FilmOuterWidthList.Average();
                            var FilmOuterWidthSigma = Math.Sqrt(FilmOuterWidthList.Select(x => (x - FilmOuterWidthMean) * (x - FilmOuterWidthMean)).Average());

                            var FilmInOuterWidthSigma = Math.Sqrt((FilmOuterWidthList.Select(x => (x - FilmOuterWidthMean) * (x - FilmOuterWidthMean)).Sum()+ FilmInnerWidthList.Select(x => (x - FilmInnerWidthMean) * (x - FilmInnerWidthMean)).Sum())/ (FilmOuterWidthList.Count()+ FilmInnerWidthList.Count()));



                            var CoatedInnerWidthList = process.InnerDetect.Tabs.Select(x => x.ValWidth).Where(x => Math.Abs(x - Static.Recipe.TabWidthTarget) < 2);
                            var CoatedInnerWidthMean = CoatedInnerWidthList.Average();
                            var CoatedInnerWidthSigma = Math.Sqrt(CoatedInnerWidthList.Select(x => (x - CoatedInnerWidthMean) * (x - CoatedInnerWidthMean)).Average());

                            var CoatedOuterWidthList = process.OuterDetect.Tabs.Select(x => x.ValWidth).Where(x => Math.Abs(x - Static.Recipe.TabWidthTarget) < 2);
                            var CoatedOuterWidthMean = CoatedOuterWidthList.Average();
                            var CoatedOuterWidthSigma = Math.Sqrt(CoatedOuterWidthList.Select(x => (x - CoatedOuterWidthMean) * (x - CoatedOuterWidthMean)).Average());

                            var CoatedInOuterWidthSigma = Math.Sqrt((CoatedOuterWidthList.Select(x => (x - CoatedOuterWidthMean) * (x - CoatedOuterWidthMean)).Sum() + CoatedInnerWidthList.Select(x => (x - CoatedInnerWidthMean) * (x - CoatedInnerWidthMean)).Sum()) / (CoatedOuterWidthList.Count() + CoatedInnerWidthList.Count()));

                            var totalWidth =
                                from x in process.InnerDetect.Tabs
                                from y in process.OuterDetect.Tabs
                                where x.EA == y.EA && x.TAB == y.TAB
                                select x.ValWidth + y.ValWidth;

                            //  Enumerable.Range(0, 100).Select(x => process.InnerDetect.Tabs[x].ValWidth);


                            var FilmWidthList = totalWidth.Where(x => Math.Abs(x - 2 * Static.Recipe.TabWidthTarget) < 2);
                            var FilmWidthMean = FilmWidthList.Where(x => Math.Abs(x - 2 * Static.Recipe.TabWidthTarget) < 2).Average();
                            var FilmWidthSigma = Math.Sqrt(FilmWidthList.Select(x => (x - FilmWidthMean) * (x - FilmWidthMean)).Average());

                            var InnerEaOk = process.InnerDetect.EAs.Count(x => !x.IsFail);
                            var InnerEaNg = process.InnerDetect.EAs.Count(x => x.IsFail);
                            var InnerEAContextTagNum = process.InnerDetect.EAs.Count(x => x.DefectCountBack_Tag > 0 || x.DefectCountFront_Tag > 0);
                            var InnerEAContextJoinNum = process.InnerDetect.EAs.Count(x => x.DefectCountBack_Join > 0 || x.DefectCountFront_Join > 0);
                            var InnerEAContextLeakMetalNum = process.InnerDetect.EAs.Count(x => x.DefectCountBack_LeakMetal > 0 || x.DefectCountFront_LeakMetal > 0);
                            var InnerEAContextWidthNum = process.InnerDetect.EAs.Count(x => x.IsTabWidthFailCountFail);

                            var OuterEaOk = process.OuterDetect.EAs.Count(x => !x.IsFail);
                            var OuterEaNg = process.OuterDetect.EAs.Count(x => x.IsFail);
                            var OuterEAContextTagNum = process.OuterDetect.EAs.Count(x => x.DefectCountBack_Tag > 0 || x.DefectCountFront_Tag > 0);
                            var OuterEAContextJoinNum = process.OuterDetect.EAs.Count(x => x.DefectCountBack_Join > 0 || x.DefectCountFront_Join > 0);
                            var OuterEAContextLeakMetalNum = process.OuterDetect.EAs.Count(x => x.DefectCountBack_LeakMetal > 0 || x.DefectCountFront_LeakMetal > 0);
                            var OuterEAContextWidthNum = process.OuterDetect.EAs.Count(x => x.IsTabWidthFailCountFail);

                            FilmData.FILM_WIDTH_LEFT_M = FilmInnerWidthMean;
                            FilmData.FILM_WIDTH_LEFT_S = FilmInnerWidthSigma;
                            FilmData.FILM_WIDTH_RIGHT_M = FilmOuterWidthMean;
                            FilmData.FILM_WIDTH_RIGHT_S = FilmOuterWidthSigma;
                            FilmData.FILM_WIDTH_LRIGHT_S = FilmInOuterWidthSigma;
                            FilmData.COATED_WIDTH_LEFT_M = CoatedInnerWidthMean;
                            FilmData.COATED_WIDTH_LEFT_S = CoatedInnerWidthSigma;
                            FilmData.COATED_WIDTH_RIGHT_M = CoatedOuterWidthMean;
                            FilmData.COATED_WIDTH_RIGHT_S = CoatedOuterWidthSigma;
                            FilmData.COATED_WIDTH_LRIGHT_S = CoatedInOuterWidthSigma;
                            FilmData.FILM_SUMWIDTH_M = FilmWidthMean;
                            FilmData.FILM_SUMWIDTH_S = FilmWidthSigma;
                            FilmData.COATED_SUMWIDTH_M = FilmWidthMean;
                            FilmData.COATED_SUMWIDTH_S = FilmWidthSigma;
                            FilmData.EaOkNum = InnerEaOk + OuterEaOk;
                            FilmData.EaNgNum = InnerEaNg + OuterEaNg;
                            FilmData.EAContextJoin = InnerEAContextJoinNum + OuterEAContextJoinNum;
                            FilmData.EAContextTag = OuterEAContextTagNum + InnerEAContextTagNum;
                            FilmData.EAContextLeakMetal = InnerEAContextLeakMetalNum + OuterEAContextLeakMetalNum;
                            FilmData.EAContextWidth = InnerEAContextWidthNum + OuterEAContextWidthNum;
                            FilmData.FilmWidthMax = Static.Recipe.TabWidthMax;
                            FilmData.FilmWidthMin = Static.Recipe.TabWidthMin;
                            FilmData.FilmWidthTarget = Static.Recipe.TabWidthTarget;
                            saveFinalCsv(FilmData.FilePathWidth, false);
                            RemotePLC.In4KCallPLC_FilmLevelToMes(FilmData);
                        }
                        

                    }
                    catch (System.Exception ex)
                    {
                        Log.AppLog.Error(string.Format("->"), ex);
                    }
                   

                    mainRollName.Enabled = true;
                    mainEmployeeNum.Enabled = true;
                    mainMachineNum.Enabled = true;
                    btnRollSet.Text = "设置膜卷";
                    isRollOk = false;
                    isRepeatRoll = true;
                }
            });
        }
        private void btnOpenViewerChart_Click(object sender, EventArgs e)
        {
            new XFViewerChart(device, process).Show();
        }
        private void btnOfflineControl_Click(object sender, EventArgs e)
        {
            new XFCameraControl(this).Show();
        }
        private void btnStartGrab_Click(object sender, EventArgs e)
        {
            DeviceStartGrab();
        }
        private void btnStopGrab_Click(object sender, EventArgs e)
        {
            DeviceStopGrab();
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            UtilTool.XFWait.Open();
            await Task.Run(() =>
            {
                if (checkBackup() == 2)
                {
                    XtraMessageBox.Show(string.Format("参数{0}与备份不一致，请进入专家模式“备份所有参数”", checkBackupResult), "参数确认", MessageBoxButtons.OK);
                    UtilTool.XFWait.Close();
                }
                else if (checkBackup() == 1)
                {
                    XtraMessageBox.Show(string.Format("参数{0}与备份不一致，请进入工程师模式“备份参数”", checkBackupResult), "参数确认", MessageBoxButtons.OK);
                    UtilTool.XFWait.Close();
                }
                else
                {
                    DeviceInit();
                    DeviceStartGrab();
                    UtilTool.XFWait.Close();
                    isRepeatRoll = false;
                }
            });
        }
        string checkBackupResult = "";
        public int checkBackup()
        {
            string s1, s2, s3, s4, s5;
            int checkOK = 0;
            if (!Static.CompareFile(Static.PathCfgApp, Static.PathCfgAppBackup) && false)
            {
                s1 = "cfg_app,";
                checkOK = 1;
            }
            else
            {
                s1 = "";
            }
            if (!Static.CompareFile(Static.PathCfgTiebiao, Static.PathCfgTiebiaoBackup))
            {
                s2 = "cfg_tiebiao,";
                checkOK = 1;
            }
            else
            {
                s2 = "";
            }
            if (!Static.CompareFile(Static.PathCfgRecipe, Static.PathCfgRecipeBackup))
            {
                s3 = "cfg_param,";
                checkOK = 1;
            }
            else
            {
                s3 = "";
            }
            if (!Static.CompareFile(Static.PathImageProcess, Static.PathImageProcessBackup))
            {
                s4 = "image_process,";
                checkOK = 2;
            }
            else
            {
                s4 = "";
            }
            if (!Static.CompareFile(Static.PathCamera, Static.PathCameraBackup))
            {
                s5 = "card,";
                checkOK = 2;
            }
            else
            {
                s5 = "";
            }
            checkBackupResult = string.Format("{0}{1}{2}{3}{4}", s1, s2, s3, s4, s5);
            return checkOK;

        }
        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            UtilTool.XFWait.Open();
            DeviceUninit();
            closeLabelCSV();
            closeWidthCSV();

            await Task.Run(() =>
            {
                if (Static.App.Is4K)
                {
                    Log.Record(RemoteDefect.In4KCall8K_Uninit);
                }

                UtilTool.XFWait.Close();
            });
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupStatuInner_DoubleClick(object sender, EventArgs e)
        {
            splitContainerInner.Panel1Collapsed ^= true;
        }
        private void groupStatuOuter_DoubleClick(object sender, EventArgs e)
        {
            splitContainerOuter.Panel1Collapsed ^= true;
        }

        private async void btnConnectRemote8K_Click(object sender, EventArgs e)
        {
            if (!RemoteDefect.isConnect)
            {
                UtilTool.XFWait.Open();
                await Task.Run(() =>
                {
                    runAction((sender as SimpleButton).Text, () =>
                    {
                        RemoteDefect.InitClient();
                    });
                    UtilTool.XFWait.Close();
                });
            }
        }
        private async void btnConnectRemotePLC_Click(object sender, EventArgs e)
        {
            if (!RemotePLC.isConnect)
            {
                UtilTool.XFWait.Open();
                await Task.Run(() =>
                {
                    runAction((sender as SimpleButton).Text, () =>
                    {
                        RemotePLC.InitClient();
                    });
                    UtilTool.XFWait.Close();
                });
            }
        }

        CfgRecipe tmpRecipe;
        CfgTiebiao tmpTiebiao;
        List<CfgRecipe> getAllRecipe()
        {
            List<CfgRecipe> cfgs = new List<CfgRecipe>();
            var files = new System.IO.DirectoryInfo(Static.FolderCfg).GetFiles("recipe_*");
            foreach (var fi in files)
            {
                cfgs.Add(new CfgRecipe(fi.FullName));
            }
            return cfgs;
        }
        void updateRecipes()
        {
            listBoxRecipe.Items.Clear();
            listBoxRecipe.Items.AddRange(getAllRecipe().Select(x => x.RecipeName).ToArray());

            groupRecipeManage.Text = $"配方管理[当前：{Static.Recipe.RecipeName}]";
            mainRecipeName.Text = Static.Recipe.RecipeName;

        }

        private void btnApplyTiebiao_Click(object sender, EventArgs e)
        {

            //
            string msg = Static.Tiebiao.GetDiff(tmpTiebiao);
            if (msg == "")
                return;

            //
            string changeCheck = "修改贴标参数\r\n" + msg;
            if (DialogResult.Yes == XtraMessageBox.Show(changeCheck, "修改确认", MessageBoxButtons.YesNo))
            {
                Log.Operate(changeCheck);
                tmpTiebiao.Save();
                Static.Tiebiao.Load();
            }
        }
        private void btnAddRecipe_Click(object sender, EventArgs e)
        {
            runAction((sender as SimpleButton).Text, () =>
            {

                if (tmpRecipe.RecipeName == "")
                    throw new Exception("请输入配方名称");

                var recipes = getAllRecipe();
                if (recipes.Select(x => x.RecipeName).Contains(tmpRecipe.RecipeName))
                    throw new Exception("配方已存在");

                string newFilePath = "";
                int id = recipes.Count + 1;
                while (true)
                {
                    newFilePath = Static.FolderCfg + "recipe_" + id;
                    if (!System.IO.File.Exists(newFilePath))
                        break;
                    id++;
                }
                tmpRecipe.SaveAs(newFilePath);

                updateRecipes();
            });
        }
        private void btnRemoveRecipe_Click(object sender, EventArgs e)
        {

            runAction((sender as SimpleButton).Text, () =>
            {

                if (tmpRecipe.RecipeName == "")
                    throw new Exception("请输入配方名称");

                if (tmpRecipe.RecipeName == Static.Recipe.RecipeName)
                    throw new Exception("无法删除当前配方");

                var recipes = getAllRecipe();
                var removeRecipe = recipes.Find(x => x.RecipeName == tmpRecipe.RecipeName);
                if (removeRecipe == null)
                    throw new Exception("配方不存在，无法删除");

                removeRecipe.Delete();
                updateRecipes();
            });
        }
        private void btnSelectRecipe_Click(object sender, EventArgs e)
        {

            runAction((sender as SimpleButton).Text, () =>
            {

                if (listBoxRecipe.SelectedIndex == -1)
                    throw new Exception("请先选择配方");

                var recipes = getAllRecipe();
                var select = recipes.Find(x => x.RecipeName == tmpRecipe.RecipeName);
                if (select == null)
                    throw new Exception("配方不存在");

                select.SaveAs(Static.Recipe);
                Static.Recipe.Load();
                Static.Recipe.UpdateBind();

                updateRecipes();
            });
        }
        private void btnApplyRecipe_Click(object sender, EventArgs e)
        {

            runAction((sender as SimpleButton).Text, () =>
            {

                var recipes = getAllRecipe();
                var select = recipes.Find(x => x.RecipeName == tmpRecipe.RecipeName);
                if (select == null)
                    throw new Exception("配方不存在");

                Log.Operate("修改配方[" + tmpRecipe.RecipeName + "]参数\r\n" + select.GetDiff(tmpRecipe));
                tmpRecipe.SaveAs(select);

            });
        }
        private void listBoxRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {

            Log.Record(() =>
            {

                if (listBoxRecipe.SelectedIndex < 0)
                    return;

                var recipes = getAllRecipe();
                var select = recipes.Find(x => x.RecipeName == listBoxRecipe.SelectedItem.ToString());
                if (select == null)
                    return;

                tmpRecipe.LoadAs(select);
                tmpRecipe.UpdateBind();
                tmpRecipe.BindDataGridView(dataRecipe);

            });
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Log.Record(() =>
            {
                if (XtraMessageBox.Show("备份参数，请确认是否备份？", "备份确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Static.CopyDir(Static.FolderCfg, Static.FolderCfgBackup))
                    {
                        XtraMessageBox.Show("参数备份成功", "备份确认", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("参数备份失败", "备份确认", MessageBoxButtons.OK);
                    }
                }
            });

        }

        private void btnBackupALL_Click(object sender, EventArgs e)
        {
            Log.Record(() =>
            {
                if (XtraMessageBox.Show("备份所有参数，请确认是否备份？", "备份确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Static.CopyDirAll(Static.FolderCfg, Static.FolderCfgBackup))
                    {
                        XtraMessageBox.Show("参数备份成功", "备份确认", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("参数备份失败", "备份确认", MessageBoxButtons.OK);
                    }
                }
            });
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            Log.Record(() =>
            {
                if (XtraMessageBox.Show("还原所有参数，请确认是否还原？", "还原确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    if (Static.CopyDirAll(Static.FolderCfgBackup, Static.FolderCfg))
                    {
                        Static.Init();
                        XtraMessageBox.Show("还原参数成功", "还原确认", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("还原参数失败", "还原确认", MessageBoxButtons.OK);
                    }
                }

            });
        }
    }

}
