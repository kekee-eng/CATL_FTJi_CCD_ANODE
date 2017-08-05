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
            init_monitor();
            init_form();

            viewer.InnerImage.MoveDirect(1);
        }

        double fpsControl = 25;
        double fpsRealtime;
        void init_form() {

            //
            UtilTool.Form.AddBuildTag(this);

            //管理线程
            Task.Run(() => {

                //线程：更新显示
                var tView1 = Task.Run(() => {

                    Stopwatch watch = new Stopwatch();
                    while (!isQuit) {
                        Thread.Sleep(10);

                        if (!watch.IsRunning) {
                            watch.Start();
                        }

                        if (fpsControl < 1) fpsControl = 1;

                        //控制帧率
                        if (watch.ElapsedMilliseconds > 1000 / fpsControl) {

                            //实时显示帧率
                            fpsRealtime = 1000.0 / watch.ElapsedMilliseconds;

                            //重置定时器
                            watch.Stop();
                            watch.Reset();
                            watch.Start();

                            //设定目标
                            double dist = viewer.InnerImage.GetTargetDistance();

                            dist = Math.Max(dist, 1) * device.InnerCamera.m_fpsRealtime / fpsRealtime;
                            viewer.InnerImage.MoveToTarget(dist);
                        }
                    };

                });

                //线程：写数据库
                var tWriteDB = Task.Run(() => {

                    do {

                        record.Transaction(() => {
                            record.InnerGrab.Save();
                        });

                        Thread.Sleep(500);
                    } while (!isQuit);

                });

                //线程：更新界面
                var tUpdate = Task.Run(() => {

                    do {

                        if (IsHandleCreated) BeginInvoke(new Action(() => {

                            UtilTool.AutoInfo.Update();

                        }));

                        Thread.Sleep(1000);
                    } while (!isQuit);

                });

                //
                Task.WaitAll(tWriteDB, tView1);

                //
                record.Close();
                device.Close();
            });

            //
            this.FormClosing += (o, e) => {
                isQuit = true;
            };

        }
        void init_device() {

            //
            device = new ModDevice();

            //
            string prefix = (HalconDotNet.HalconAPI.isWindows ? "D:/" : "/media/fra/DATA/");
            device.InnerCamera = new ConnectCamera_ZipFile(prefix + "#DAT/[2B][20170728][125247-130642][1283][F1-F1283].zip");
            device.InnerCamera.OnImageReady += obj => {

                //线程1：内侧相机取图、处理
                //
                record.InnerGrab.Cache[obj.Frame] = obj;
                viewer.InnerImage.SetTargetBottom(obj.Frame);
            };
            device.InnerCamera.OnComplete += () => {

                //
                device.InnerCamera.Stop();
                device.InnerCamera.Reset();
                device.InnerCamera.Start();
            };

            //
            device.OuterCamera = new ConnectCamera_ZipFile(prefix + "#DAT/[2A][20170728][125247-130642][1283][F1-F1283].zip");
            device.OuterCamera.OnImageReady += obj => {

                //线程2：外侧相机取图、处理
            };
            device.OuterCamera.OnComplete += () => {

            };

        }
        void init_record() {

            //
            //System.IO.File.Delete(Config.FolderRecord + "01.db");
            record = new ModRecord();
            record.Open(Config.FolderRecord + "01.db");
            record.Init();
            
        }
        void init_viewer() {

            //
            viewer = new ModViewer();
            viewer.InnerImage = new DataGrab.ImageViewer(hwin, record.InnerGrab);
            
        }
        void init_monitor() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["App"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["App_RunTime"] = () => UtilPerformance.GetAppRuntime();

            monitor["Inner_Grab"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
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

            monitor["Inner_Viewer"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Viewer_FpsControl"] = () => fpsControl;
            monitor["Inner_Viewer_FpsRealtime"] = () => fpsRealtime;
            monitor["Inner_Viewer_targetVs"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVs");
            monitor["Inner_Viewer_targetVx"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVx");
            monitor["Inner_Viewer_targetVy"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetVy");
            monitor["Inner_Viewer_targetAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "targetAllow");
            monitor["Inner_Viewer_mouseAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(viewer.InnerImage, "mouseAllow");
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
        
        private void btnGrabStart_Click(object sender, EventArgs e) {
            device.InnerCamera.m_fpsControl = 0.3;
            device.InnerCamera.m_frameReset = 10;
            device.InnerCamera.m_frame = 10;
            device.InnerCamera.Start();
        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
        }

        private void button1_Click(object sender, EventArgs e) {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            viewer.InnerImage.SetTargetUsed(checkBox1.Checked);
        }
    }
}
