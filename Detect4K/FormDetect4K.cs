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
            //System.IO.File.Delete(Config.FolderRecord + "01.db");
            m_record = new ConnectRecord();
            m_record.Open(Config.FolderRecord + "01.db");
            m_record.Init();

            //
            m_camera = new ConnectCamZip(@"D:\#DAT\[2A][20170728][125247-130642][1283][F1-F1283].zip");
            m_camera.OnImageReady += obj => {

                //
                m_record.InnerGrabCache[obj.Frame] = obj;
                m_record.InnerGrabCache.RemoveOld(100);
            };
            m_camera.OnComplete += () => {

                //
                m_camera.Stop();
                m_camera.Reset();
                m_camera.Start();
            };

            //
            Task.Run(() => {

                //
                while (!isQuit) {
                    Thread.Sleep(200);

                    m_record.Transaction(() => {
                        m_record.InnerGrabCache.SaveToDB(m_record.InnerGrabDB);
                    });
                }

                //
                m_record.Close();
                m_camera.Dispose();
            });

            //
            this.FormClosing += (o, e) => {
                isQuit = true;
            };
            
        }
        
        bool isQuit = false;

        ConnectCamZip m_camera;
        ConnectRecord m_record;

        //公用信息，自动显示到控件上
        public string AutoInfo_Inner_Grab_Name() { return m_camera.m_camera_name; }
        public bool AutoInfo_Inner_Grab_IsReady() { return m_camera.isReady; }
        public bool AutoInfo_Inner_Grab_IsRun() { return m_camera.isRun; }
        public int AutoInfo_Inner_Grab_Frame() { return m_camera.m_frame; }
        public int AutoInfo_Inner_Grab_FrameStart() { return m_camera.m_frameStart; }
        public int AutoInfo_Inner_Grab_FrameEndMax() { return m_camera.m_frameEndMax; }
        public double AutoInfo_Inner_Grab_FpsControl() { return m_camera.m_fpsControl; }
        public double AutoInfo_Inner_Grab_FpsRealtime() { return m_camera.m_fpsRealtime; }

        public int AutoInfo_Inner_Record_GrabCacheMin() { return m_record.InnerGrabCache.Min; }
        public int AutoInfo_Inner_Record_GrabCacheMax() { return m_record.InnerGrabCache.Max; }
        public int AutoInfo_Inner_Record_GrabCacheCount() { return m_record.InnerGrabCache.Count; }
        public int AutoInfo_Inner_Record_GrabDBMin() { return m_record.InnerGrabDB.Min; }
        public int AutoInfo_Inner_Record_GrabDBMax() { return m_record.InnerGrabDB.Max; }
        public int AutoInfo_Inner_Record_GrabDBCount() { return m_record.InnerGrabDB.Count; }
        public int AutoInfo_Inner_Record_GrabCacheRemain() { return Math.Max(0, m_record.InnerGrabCache.Max - m_record.InnerGrabDB.Max); }
        
        //
        private void timer1_Tick(object sender, EventArgs e) {

            UtilTool.BindAutoInfo("AutoInfo_", this, dataGridView1);
            
        }
        private void btnGrabStart_Click(object sender, EventArgs e) {
            m_camera.m_fpsControl = 20;
            m_camera.Start();
        }
        private void btnGrabStop_Click(object sender, EventArgs e) {
            m_camera.Stop();
        }



    }
}
