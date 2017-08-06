using Common;
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

namespace Detect4K {
    public partial class FormDetect4K : Form {
        public FormDetect4K() {
            InitializeComponent();

            //
            init_device();
            init_record();
            init_viewer();

            //
            init_form();

            //
            Task.Run(() => {

                Thread.Sleep(1000);

                viewer.InnerImage.SetBottomTarget(0);
                viewer.InnerImage.MoveTargetDirect();

            });
        }

        void init_form() {

            //
            UtilTool.Form.AddBuildTag(this);

            //管理线程
            Task.Run(() => {

                //线程：更新显示
                var tView1 = Task.Run(() => {

                    while (!isQuit) {
                        Thread.Sleep(10);

                        double refFps = device.InnerCamera.isRun ? device.InnerCamera.m_fpsRealtime : 10;
                        viewer.InnerImage.MoveTargetSync(device.InnerCamera.m_fpsRealtime);

                    };

                });

                //线程：写数据库
                var tWriteDB = Task.Run(() => {

                    do {

                        List<DataGrab> retInner = null;
                        if (record.Transaction(() => {
                            retInner = record.InnerGrab.Save();
                            record.InnerDetect.Save();
                        })) {
                            retInner.AsParallel().ForAll(x => x.IsStore = true);
                        }

                        Thread.Sleep(500);
                    } while (!isQuit);

                });

                //
                Task.WaitAll(tWriteDB, tView1);

                //
                record.Close();
            });

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
            device.InnerCamera = new ConnectCamera_ZipFile(Config.App.CameraZipFile);

            device.InnerCamera.OnImageReady += obj => {

                //线程1：内侧相机取图、处理
                //
                record.InnerGrab.Cache[obj.Frame] = obj;
                record.InnerDetect.TryDetect(obj.Frame);


                viewer.InnerImage.SetBottomTarget(obj.Frame);
            };
            device.InnerCamera.OnComplete += () => {

                //
                device.InnerCamera.Stop();
                device.InnerCamera.Reset();
                device.InnerCamera.Start();
            };
            
        }
        void init_record() {

            //
            System.IO.File.Delete(Config.FolderRecord + "01.db");
            record = new ModRecord();
            record.Open(Config.FolderRecord + "01.db");
            record.Init();

        }
        void init_viewer() {

            //
            viewer = new ModViewer();
            viewer.InnerImage = new ViewerImage(hwin, record.InnerGrab, record.InnerDetect);

        }

        void init_monitor() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["App"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["App_RunTime"] = () => UtilPerformance.GetAppRuntime();
            monitor["App_MemoryTotal"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryTotal());
            monitor["App_MemoryLoad"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryLoad());
            monitor["App_CpuCount"] = () => UtilPerformance.GetCpuCount();
            monitor["App_CpuLoad"] = () => string.Format("{0:0.00} %", UtilPerformance.GetCpuLoad());

            monitor["Inner_Grab"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Grab_Path"] = () => UtilTool.AutoInfo.GetPrivateValue(device.InnerCamera, "m_filename"); 
            monitor["Inner_Grab_Name"] = () => device.InnerCamera.m_camera_name;
            monitor["Inner_Grab_IsReady"] = () => device.InnerCamera.isReady;
            monitor["Inner_Grab_IsRun"] = () => device.InnerCamera.isRun;
            monitor["Inner_Grab_Frame"] = () => device.InnerCamera.m_frame;
            monitor["Inner_Grab_FrameReset"] = () => device.InnerCamera.m_frameReset;
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
            monitor["Inner_Record_TabsCount"] = () => record.InnerDetect.Tabs.Count;

            monitor["Inner_Viewer"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Viewer_showImageStatic"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showImageStatic");
            monitor["Inner_Viewer_showImageDynamic"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showImageDynamic");
            monitor["Inner_Viewer_showContextEA"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextEA");
            monitor["Inner_Viewer_showContextTab"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextTab");
            monitor["Inner_Viewer_showContextWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextWidth");
            monitor["Inner_Viewer_showContextNG"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextNG");
            monitor["Inner_Viewer_showContextLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextLabel");
            monitor["Inner_Viewer_showContextCross"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "showContextCross");

            monitor["Inner_Viewer_fpsControl"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "fpsControl");
            monitor["Inner_Viewer_fpsRealtime"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "fpsRealtime");

            monitor["Inner_Viewer_mouseAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "mouseAllow");
            monitor["Inner_Viewer_targetAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetAllow");
            monitor["Inner_Viewer_targetVs"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVs");
            monitor["Inner_Viewer_targetVx"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVx");
            monitor["Inner_Viewer_targetVy"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVy");
            monitor["Inner_Viewer_targetDist"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetDist");

            monitor["Inner_Viewer_frameVs"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameVs");
            monitor["Inner_Viewer_frameVx"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameVx");
            monitor["Inner_Viewer_frameVy"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameVy");
            monitor["Inner_Viewer_frameDx"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameDx");
            monitor["Inner_Viewer_frameDy"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameDy");
            monitor["Inner_Viewer_frameX1"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameX1");
            monitor["Inner_Viewer_frameX2"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameX2");
            monitor["Inner_Viewer_frameY1"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameY1");
            monitor["Inner_Viewer_frameY2"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameY2");

            monitor["Inner_Viewer_frameStart"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameStart");
            monitor["Inner_Viewer_frameEnd"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameEnd");
            monitor["Inner_Viewer_frameStartRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameStartRequire");
            monitor["Inner_Viewer_frameEndRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameEndRequire");
            monitor["Inner_Viewer_frameStartLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameStartLimit");
            monitor["Inner_Viewer_frameEndLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "frameEndLimit");

            monitor["Inner_Viewer_grabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "grabWidth");
            monitor["Inner_Viewer_grabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "grabHeight");
            monitor["Inner_Viewer_boxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "boxWidth");
            monitor["Inner_Viewer_boxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "boxHeight");
            monitor["Inner_Viewer_refGrabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "refGrabWidth");
            monitor["Inner_Viewer_refGrabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "refGrabHeight");
            monitor["Inner_Viewer_refBoxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "refBoxWidth");
            monitor["Inner_Viewer_refBoxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "refBoxHeight");


            //
            UtilTool.AutoInfo.InitGrid(dataGridView1, monitor);

        }

        bool isQuit = false;

        ModRecord record;
        ModDevice device;
        ModViewer viewer;

        private void btnLoadFile_Click(object sender, EventArgs e) {

            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK) {
                Config.App.CameraZipFile = ofd.FileName;

                btnGrabRestart_Click(null, null);
            }
        }
        private void btnGrabRestart_Click(object sender, EventArgs e) {
            init_device();
            //record.InnerDetect.Discard();
            device.InnerCamera.m_frame = Config.App.CameraFrameReset;
            device.InnerCamera.m_fpsControl = Config.App.CameraFpsControl;
            device.InnerCamera.Start();
        }
        private void btnGrabStart_Click(object sender, EventArgs e) {
            device.InnerCamera.m_fpsControl = Config.App.CameraFpsControl;
            device.InnerCamera.Start();
        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
        }
        
        private void timer1_Tick(object sender, EventArgs e) {

            if (IsHandleCreated && !IsDisposed) {

                switch (gridMode) {
                    default: break;
                    case 0: UtilTool.AutoInfo.Update(); break;
                    case 1: ViewerChart.SyncTabGrid(dataGridView1, record.InnerDetect); break;
                    case 2:ViewerChart.SyncEAGrid(dataGridView1, record.InnerDetect);break;
                }
            }
        }

        int gridMode = -1;
        void createNewGrid() {
            gridMode = -1;

            this.splitContainer1.Panel1.Controls.Remove(dataGridView1);
            dataGridView1.Dispose();
            dataGridView1 = null;

            dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.splitContainer1.Panel1.Controls.Add(dataGridView1);
        }
        private void rtoolInfo_Click(object sender, EventArgs e) {

            createNewGrid();
            init_monitor();
            gridMode = 0;

        }
        private void rtoolTab_Click(object sender, EventArgs e) {

            createNewGrid();
            ViewerChart.InitTabGrid(dataGridView1 , x=> {
                viewer.InnerImage.MoveToTAB(x);
            });
            gridMode = 1;
        }
        private void rtoolEA_Click(object sender, EventArgs e) {

            createNewGrid();
            ViewerChart.InitEAGrid(dataGridView1, x => {
                viewer.InnerImage.MoveToEA(x, 1);
            });
            gridMode = 2;
        }

        private void rtoolCfgApp_Click(object sender, EventArgs e) {

            createNewGrid();
            Config.App.BindDataGridView(dataGridView1);

        }
        private void rtoolCfgShare_Click(object sender, EventArgs e) {

            createNewGrid();
            Config.ParamShare.BindDataGridView(dataGridView1);

        }
        private void rtoolCfgInner_Click(object sender, EventArgs e) {

            createNewGrid();
            Config.ParamInner.BindDataGridView(dataGridView1);

        }

    }
}
