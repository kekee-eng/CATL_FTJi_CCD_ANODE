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
            System.IO.File.Delete(Config.FolderRecord + "01.db");

            record = new ModRecord();
            record.Open(Config.FolderRecord + "01.db");
            record.Init();

            //
            device = new ModDevice();

            //
            string prefix = ( HalconDotNet.HalconAPI.isWindows? "D:/": "/media/fra/DATA/");
            device.InnerCamera = new ConnectCamera_ZipFile(prefix + "#DAT/[2B][20170728][125247-130642][1283][F1-F1283].zip");
            device.InnerCamera.OnImageReady += obj => {

                //线程1：内侧相机取图、处理
                //
                record.InnerGrab.Cache[obj.Frame] = obj;
                record.InnerGrab.Cache.RemoveOld(Config.App.RecordCacheSize);

                //
                UtilTool.ShowHImage(hwin, obj.Image);
            };
            device.InnerCamera.OnComplete += () => {

                //
                device.InnerCamera.Stop();
                device.InnerCamera.Reset();
                device.InnerCamera.Start();
            };

            //
            device.OuterCamera = new ConnectCamera_ZipFile(prefix +"#DAT/[2A][20170728][125247-130642][1283][F1-F1283].zip");
            device.OuterCamera.OnImageReady += obj => {

                //线程2：外侧相机取图、处理
            };
            device.OuterCamera.OnComplete += () => {
                
            };
            
            //
            new Thread(new ThreadStart(() => {

                //线程3：写数据库
                while (!isQuit) {
                    Thread.Sleep(500);

                    record.Transaction(() => {
                        record.InnerGrab.SaveToDB();
                    });
                }

                //
                record.Close();
                device.Close();
            })).Start();

            //
            this.FormClosing += (o, e) => {
                isQuit = true;
            };
            
        }
        
        bool isQuit = false;
        
        ModRecord record;
        ModDevice device;

        //公用信息，自动显示到控件上
        public string AutoInfo_Inner_Grab_Name() { return device.InnerCamera.m_camera_name; }
        public bool AutoInfo_Inner_Grab_IsReady() { return device.InnerCamera.isReady; }
        public bool AutoInfo_Inner_Grab_IsRun() { return device.InnerCamera.isRun; }
        public int AutoInfo_Inner_Grab_Frame() { return device.InnerCamera.m_frame; }
        public int AutoInfo_Inner_Grab_FrameStart() { return device.InnerCamera.m_frameStart; }
        public int AutoInfo_Inner_Grab_FrameEndMax() { return device.InnerCamera.Max; }
        public double AutoInfo_Inner_Grab_FpsControl() { return device.InnerCamera.m_fpsControl; }
        public double AutoInfo_Inner_Grab_FpsRealtime() { return device.InnerCamera.m_fpsRealtime; }

        public int AutoInfo_Inner_Record_GrabCacheMin() { return record.InnerGrab.Cache.Min; }
        public int AutoInfo_Inner_Record_GrabCacheMax() { return record.InnerGrab.Cache.Max; }
        public int AutoInfo_Inner_Record_GrabCacheCount() { return record.InnerGrab.Cache.Count; }
        public int AutoInfo_Inner_Record_GrabDBMin() { return record.InnerGrab.DB.Min; }
        public int AutoInfo_Inner_Record_GrabDBMax() { return record.InnerGrab.DB.Max; }
        public int AutoInfo_Inner_Record_GrabDBCount() { return record.InnerGrab.DB.Count; }
        public int AutoInfo_Inner_Record_GrabCacheRemain() { return Math.Max(0, record.InnerGrab.Cache.Max - record.InnerGrab.DB.Max); }
        
        //
        private void timer1_Tick(object sender, EventArgs e) {

            UtilTool.BindAutoInfo("AutoInfo_", this, dataGridView1);

        }
        private void btnGrabStart_Click(object sender, EventArgs e) {
            device.InnerCamera.m_fpsControl = 20;
            device.InnerCamera.Start();
        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            device.InnerCamera.Stop();
        }



    }
}
