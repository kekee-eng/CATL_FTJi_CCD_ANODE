using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                m_record.GrabCacheInner[obj.Frame] = obj;
                m_record.GrabCacheInner.RemoveOld(30);
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
                        m_record.GrabCacheInner.SaveToDB(m_record.GrabDBInner);
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
        
        private void timer1_Tick(object sender, EventArgs e) {

            lb_grab_isready.Text = string.Format("{0,7} = {1}", "IsReady", m_camera.isReady ? "On" : "Off");
            lb_grab_frame.Text = string.Format("{0,7} = {1}", "Frame", m_camera.m_frame);
            lb_grab_fps.Text = string.Format("{0,7} = {1:0.00}", "Fps", m_camera.m_fpsRealtime);

            lb_rec_cache.Text = string.Format("{0,7} = {1} [{2}->{3}]", "Cache",
                m_record.GrabCacheInner.Count,
                m_record.GrabCacheInner.FrameStart,
                m_record.GrabCacheInner.FrameEnd
                );

            lb_rec_db.Text = string.Format("{0,7} = {1} [{2}->{3}]", "DB",
                m_record.GrabDBInner.Count,
                m_record.GrabDBInner.FrameStart,
                m_record.GrabDBInner.FrameEnd
                );

            lb_rec_remain.Text = string.Format("{0,7} = {1}", "Remain", m_record.GrabCacheInner.FrameEnd - m_record.GrabDBInner.FrameEnd);
            
        }

        private void btnGrabStart_Click(object sender, EventArgs e) {
            m_camera.m_fpsControl = 10;
            m_camera.Start();
        }

        private void btnGrabStop_Click(object sender, EventArgs e) {
            m_camera.Stop();
        }
    }
}
