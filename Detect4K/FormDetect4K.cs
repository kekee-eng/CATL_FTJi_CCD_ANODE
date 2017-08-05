using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            
            //
            viewer.InnerImage.ViewFrame(1);
        }

        void init_form() {

            //
            Task.Run(() => {

                //线程：更新显示
                while (!isQuit) {
                    Thread.Sleep(10);
                    //viewer.InnerImage.View(1, device.InnerCamera.m_frame, 1);
                }
            });

            //
            Task.Run(() => {

                //线程3：写数据库
                while (!isQuit) {
                    Thread.Sleep(500);

                    record.Transaction(() => {
                        record.InnerGrab.Save();
                    });
                }

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
                record.InnerGrab.Cache.RemoveOld(Config.App.RecordCacheSize);

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

            monitor["Inner_Grab"] = () => ">=====<";
            monitor["Inner_Grab_Name"] = () => device.InnerCamera.m_camera_name;
            monitor["Inner_Grab_IsReady"] = () => device.InnerCamera.isReady;
            monitor["Inner_Grab_IsRun"] = () => device.InnerCamera.isRun;
            monitor["Inner_Grab_Frame"] = () => device.InnerCamera.m_frame;
            monitor["Inner_Grab_FrameReset"] = () => device.InnerCamera.m_frameReset;
            monitor["Inner_Grab_FrameMax"] = () => device.InnerCamera.Max;
            monitor["Inner_Grab_FpsControl"] = () => device.InnerCamera.m_fpsControl;
            monitor["Inner_Grab_FpsRealtime"] = () => device.InnerCamera.m_fpsRealtime;

            monitor["Inner_Record"] = () => ">=====<";
            monitor["Inner_Record_GrabCacheMin"] = () => record.InnerGrab.Cache.Min;
            monitor["Inner_Record_GrabCacheMax"] = () => record.InnerGrab.Cache.Max;
            monitor["Inner_Record_GrabCacheCount"] = () => record.InnerGrab.Cache.Count;
            monitor["Inner_Record_GrabDBMin"] = () => record.InnerGrab.DB.Min;
            monitor["Inner_Record_GrabDBMax"] = () => record.InnerGrab.DB.Max;
            monitor["Inner_Record_GrabDBCount"] = () => record.InnerGrab.DB.Count;
            monitor["Inner_Record_GrabCacheRemain"] = () => Math.Max(0, record.InnerGrab.Cache.Max - record.InnerGrab.DB.Max);

            monitor["Inner_Viewer"] = () => ">=====<";
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

        //
        private void timer1_Tick(object sender, EventArgs e) {

            UtilTool.AutoInfo.Update();

        }
        private void btnGrabStart_Click(object sender, EventArgs e) {
            device.InnerCamera.m_fpsControl = 20;
            device.InnerCamera.Start();
        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
        }

        private void button1_Click(object sender, EventArgs e) {
            
        }
    }
}
