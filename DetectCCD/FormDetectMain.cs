
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectCCD {
    public partial class FormDetect4K : Form {
        public FormDetect4K() {
            InitializeComponent();

            //
            init_device();
            init_record();

            //
            init_form();
            
            //
            Static.ParamApp.BindTextBox(tbFrameStart, "CameraStartFrame");
            //rtoolDebug_Click(null, null);
        }

        void init_form() {

            //
            UtilTool.Form.AddBuildTag(this);

            //管理线程
            Task.Run((Action)(() => {

                //线程：更新显示
                var tView1 = Task.Run((Action)(() => {

                    while (!isQuit) {

                        Thread.Sleep(10);

                        Static.SafeRun(() => {
                            double refFps = device.InnerCamera.isRun ? device.InnerCamera.m_fpsRealtime : 10;
                            this.record.InnerViewerImage.MoveTargetSync(device.InnerCamera.m_fpsRealtime);
                        });
                    };

                }));

                //线程：写数据库
                var tWriteDB = Task.Run((Action)(() => {
                    
                    do {

                        Static.SafeRun(() => {
                            List<DataGrab> ret1 = null;
                            if (this.record.Transaction(() => {
                                ret1 = this.record.InnerGrab.Save();
                                this.record.InnerDetect.Save();
                            })) {
                                ret1.AsParallel().ForAll(x => x.IsStore = true);
                            }
                        });

                        Thread.Sleep(500);
                    } while (!isQuit);

                }));

                //
                Task.WaitAll(tWriteDB, tView1);

                //
                this.record.Close();
            }));

            //
            this.FormClosing += (o, e) => {
                device.Close();
                isQuit = true;
            };

        }
        void init_device() {

            //
            device?.Close();
            device = new ModDevice();

            //
            //string prefix = (HalconDotNet.HalconAPI.isWindows ? "D:/" : "/media/fra/DATA/");
            //device.InnerCamera = new ConnectCamera_ZipFile(prefix + "#DAT/[2B][20170728][125247-130642][1283][F1-F1283].zip");
            device.InnerCamera = new ConnectCamera_ZipFile(Static.ParamApp.CameraZipFile);

            device.InnerCamera.OnImageReady += obj => {

                Static.SafeRun(() => {
                    //线程1：内侧相机取图、处理
                    //
                    record.InnerGrab.Cache[obj.Frame] = obj;
                    record.InnerDetect.TryDetect(obj.Frame);

                    record.InnerViewerImage.SetBottomTarget(obj.Frame);
                });

            };
            device.InnerCamera.OnComplete += () => {

                Static.SafeRun(() => {
                    if (checkReplay.Checked) {
                        btnGrabRestart_Click(null, null);
                    }
                });
            };

        }
        void init_record() {

            string path = Static.FolderRecord + "01.db";

            //
            record?.Close();
            record?.Dispose();
            while (System.IO.File.Exists(path)) {
                Thread.Sleep(100);
                Static.SafeRun(() => System.IO.File.Delete(path));
            }

            //
            record = new ModRecord();
            record.Open(path);
            record.Init();

            record.InnerViewerImage.Init(hwin);
        }
        
        Dictionary<string, Func<object>> getMonitor() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["App"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["App_RunTime"] = () => UtilPerformance.GetAppRuntime();
            monitor["App_MemoryTotal"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryTotal());
            monitor["App_MemoryLoad"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryLoad());
            monitor["App_CpuCount"] = () => UtilPerformance.GetCpuCount();
            monitor["App_CpuLoad"] = () => string.Format("{0:0.00} %", UtilPerformance.GetCpuLoad());

            monitor["ImageProcess"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["ImageProcess_DetectTab"] = () => ImageProcess.TimeDetectTab;
            monitor["ImageProcess_DetectWidth"] = () => ImageProcess.TimeDetectWidth;
            monitor["ImageProcess_DetectMark"] = () => ImageProcess.TimeDetectMark;
            monitor["ImageProcess_DetectDefect"] = () => ImageProcess.TimeDetectDefect;


            monitor["Inner_Grab"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Grab_Path"] = () => UtilTool.AutoInfo.GetPrivateValue(device.InnerCamera, "m_filename");
            monitor["Inner_Grab_Name"] = () => device.InnerCamera.m_camera_name;
            monitor["Inner_Grab_IsReady"] = () => device.InnerCamera.isReady;
            monitor["Inner_Grab_IsRun"] = () => device.InnerCamera.isRun;
            monitor["Inner_Grab_Frame"] = () => device.InnerCamera.m_frame;
            monitor["Inner_Grab_FrameReset"] = () => device.InnerCamera.m_frameStart;
            monitor["Inner_Grab_FrameMax"] = () => device.InnerCamera.Max;
            monitor["Inner_Grab_FpsControl"] = () => device.InnerCamera.m_fpsControl;
            monitor["Inner_Grab_FpsRealtime"] = () => device.InnerCamera.m_fpsRealtime;

            monitor["Inner_Record"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Record_GrabCacheMin"] = () => record.InnerGrab.Cache.Min;
            monitor["Inner_Record_GrabCacheMax"] = () => record.InnerGrab.Cache.Max;
            monitor["Inner_Record_GrabCacheCount"] = () => record.InnerGrab.Cache.Count;

            monitor["Inner_Record_GrabDBMin"] = () => record.InnerGrab.DB.Min;
            monitor["Inner_Record_GrabDBMax"] = () => record.InnerGrab.DB.Max;
            monitor["Inner_Record_GrabDBCount"] = () => record.InnerGrab.DB.Count;

            monitor["Inner_Record_GrabCacheRemain"] = () => Math.Max(0, record.InnerGrab.Cache.Max - record.InnerGrab.DB.Max);

            monitor["Inner_Record_LastLoadCache"] = () => record.InnerGrab.LastLoadCache;
            monitor["Inner_Record_LastLoadDB"] = () => record.InnerGrab.LastLoadDB;

            monitor["Inner_Record_TabCount"] = () => record.InnerDetect.Tabs.Count;
            monitor["Inner_Record_EACount"] = () => record.InnerDetect.EACount;
            monitor["Inner_Record_DefectCount"] = () => record.InnerDetect.Defects.Count;
            monitor["Inner_Record_LabelCount"] = () => record.InnerDetect.Labels.Count;

            monitor["Inner_Viewer"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Viewer_showImageStatic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showImageStatic");
            monitor["Inner_Viewer_showImageDynamic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showImageDynamic");
            monitor["Inner_Viewer_showContextMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextMark");
            monitor["Inner_Viewer_showContextTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextTab");
            monitor["Inner_Viewer_showContextWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextWidth");
            monitor["Inner_Viewer_showContextDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextDefect");
            monitor["Inner_Viewer_showContextLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextLabel");
            monitor["Inner_Viewer_showContextCross"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextCross");

            monitor["Inner_Viewer_countShowTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowTab");
            monitor["Inner_Viewer_countShowMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowMark");
            monitor["Inner_Viewer_countShowDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowDefect");
            monitor["Inner_Viewer_countShowLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowLabel");

            monitor["Inner_Viewer_fpsControl"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "fpsControl");
            monitor["Inner_Viewer_fpsRealtime"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "fpsRealtime");

            monitor["Inner_Viewer_mouseAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "mouseAllow");
            monitor["Inner_Viewer_targetAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetAllow");
            monitor["Inner_Viewer_targetVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVs");
            monitor["Inner_Viewer_targetVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVx");
            monitor["Inner_Viewer_targetVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVy");
            monitor["Inner_Viewer_targetDist"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetDist");

            monitor["Inner_Viewer_frameVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVs");
            monitor["Inner_Viewer_frameVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVx");
            monitor["Inner_Viewer_frameVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVy");
            monitor["Inner_Viewer_frameDx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameDx");
            monitor["Inner_Viewer_frameDy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameDy");
            monitor["Inner_Viewer_frameX1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameX1");
            monitor["Inner_Viewer_frameX2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameX2");
            monitor["Inner_Viewer_frameY1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameY1");
            monitor["Inner_Viewer_frameY2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameY2");

            monitor["Inner_Viewer_frameStart"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStart");
            monitor["Inner_Viewer_frameEnd"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEnd");
            monitor["Inner_Viewer_frameStartRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStartRequire");
            monitor["Inner_Viewer_frameEndRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEndRequire");
            monitor["Inner_Viewer_frameStartLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStartLimit");
            monitor["Inner_Viewer_frameEndLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEndLimit");

            //monitor["Inner_Viewer_grabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "grabWidth");
            //monitor["Inner_Viewer_grabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "grabHeight");
            //monitor["Inner_Viewer_boxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "boxWidth");
            //monitor["Inner_Viewer_boxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "boxHeight");
            //monitor["Inner_Viewer_refGrabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refGrabWidth");
            //monitor["Inner_Viewer_refGrabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refGrabHeight");
            //monitor["Inner_Viewer_refBoxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refBoxWidth");
            //monitor["Inner_Viewer_refBoxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refBoxHeight");

            //
            return monitor;

        }

        bool isQuit = false;

        ModRecord record;
        ModDevice device;

        private void trackSpeed_Scroll(object sender, EventArgs e) {
            Static.ParamApp.CameraFpsControl = trackSpeed.Value / 10.0;
            device.InnerCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
        }

        private void btnLoadFile_Click(object sender, EventArgs e) {

            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK) {
                Static.ParamApp.CameraZipFile = ofd.FileName;
                tbFrameStart.Text = "1";
                btnGrabRestart_Click(null, null);
            }

        }
        private void btnGrabRestart_Click(object sender, EventArgs e) {
            init_device();
            init_record();
            device.InnerCamera.m_frameStart = Static.ParamApp.CameraStartFrame;
            device.InnerCamera.Reset();
            device.InnerCamera.Start();

            btnGrabRestart_Click(null, null);
        }
        private void btnGrabStart_Click(object sender, EventArgs e) {

            device.InnerCamera.m_fpsControl = Static.ParamApp.CameraFpsControl;
            device.InnerCamera.Start();
            record.InnerViewerImage.SetBottomTarget(device.InnerCamera.m_frameStart - 1);
            record.InnerViewerImage.MoveTargetDirect();
            record.InnerViewerImage.SetUserEnable(false);

        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
            record.InnerViewerImage.SetUserEnable(true);
        }

        private void timer1_Tick(object sender, EventArgs e) {

            if (IsHandleCreated && !IsDisposed) {

                tbFrameCurrent.Text = string.Format("{0} / {1}", device.InnerCamera.m_frame, device.InnerCamera.Max);

                switch (gridMode) {
                    default: break;
                    case 0: UtilTool.AutoInfo.Update(); break;
                    case 1: record.InnerViewerChart.SyncTabGrid(panelForViewer); break;
                    case 2: record.InnerViewerChart.SyncEAGrid(panelForViewer); break;
                    case 3: record.InnerViewerChart.SyncDefectGrid(panelForViewer); break;
                    case 4: ViewerChart.parentGetGrid(panelForViewer).Rows[0].Cells[0].Value = ImageProcess.ErrorMessage;break;
                    case 5: record.InnerViewerChart.SyncTabChart(panelForViewer); break;
                }
            }
        }
        
        int gridMode = -1;
        void viewerInit(int mode, Action<Control> act) {
            gridMode = -1;
            act(panelForViewer);
            gridMode = mode;
        }
       
        private void rtoolInfo_Click(object sender, EventArgs e) {
            viewerInit(0, x => UtilTool.AutoInfo.InitGrid(ViewerChart.parentInitGrid(x),getMonitor()));
        }
        private void rtoolTab_Click(object sender, EventArgs e) {
            viewerInit(1, record.InnerViewerChart.InitTabGrid);
        }
        private void rtoolEA_Click(object sender, EventArgs e) {
            viewerInit(2, record.InnerViewerChart.InitEAGrid);
        }
        private void rtoolDefect_Click(object sender, EventArgs e) {
            viewerInit(3, record.InnerViewerChart.InitDefectGrid);
        }

        private void rtoolCfgApp_Click(object sender, EventArgs e) {
            viewerInit(-1, x => Static.ParamApp.BindDataGridView(ViewerChart.parentInitGrid(x)));
        }
        private void rtoolCfgShare_Click(object sender, EventArgs e) {
            viewerInit(-1, x => Static.ParamShare.BindDataGridView(ViewerChart.parentInitGrid(x)));
        }
        private void rtoolCfgInner_Click(object sender, EventArgs e) {
            viewerInit(-1, x => Static.ParamInner.BindDataGridView(ViewerChart.parentInitGrid(x)));
        }

        private void rtoolDebug_Click(object sender, EventArgs e) {
            viewerInit(4, x => {

                var grid = ViewerChart.parentInitGrid(x);

                grid.Columns.Add(new DataGridViewTextBoxColumn() { Width = 500, HeaderText = "" });
                grid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                grid.Rows.Add("");
                grid.Rows[0].Height = 500;
                grid.Rows[0].Cells[0].Value = "";

                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                grid.RowsDefaultCellStyle.WrapMode = (DataGridViewTriState.True);

            });

        }
        private void rtoolDebugClear_Click(object sender, EventArgs e) {
            ImageProcess.ErrorMessage = "";
        }

        private void rtoolTABChart_Click(object sender, EventArgs e) {
            viewerInit(5, record.InnerViewerChart.InitTabChart);
            widthToolStripMenuItem_Click(widthToolStripMenuItem, null);
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e) {
            widthToolStripMenuItem.Checked = false;
            sizeToolStripMenuItem.Checked = false;
            distanceDiffToolStripMenuItem.Checked = false;
            distanceToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            record.InnerViewerChart.SelectTabChart(panelForViewer, 0);
        }
        private void sizeToolStripMenuItem_Click(object sender, EventArgs e) {
            widthToolStripMenuItem.Checked = false;
            sizeToolStripMenuItem.Checked = false;
            distanceDiffToolStripMenuItem.Checked = false;
            distanceToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            record.InnerViewerChart.SelectTabChart(panelForViewer, 1);
        }
        private void distanceToolStripMenuItem_Click(object sender, EventArgs e) {
            widthToolStripMenuItem.Checked = false;
            sizeToolStripMenuItem.Checked = false;
            distanceDiffToolStripMenuItem.Checked = false;
            distanceToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            record.InnerViewerChart.SelectTabChart(panelForViewer, 2);
        }
        private void distanceDiffToolStripMenuItem_Click(object sender, EventArgs e) {
            widthToolStripMenuItem.Checked = false;
            sizeToolStripMenuItem.Checked = false;
            distanceDiffToolStripMenuItem.Checked = false;
            distanceToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            record.InnerViewerChart.SelectTabChart(panelForViewer, 3);
        }
        

    }
}
