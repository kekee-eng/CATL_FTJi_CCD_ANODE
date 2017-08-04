using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.UtilTool;

namespace Detect4K {
    public partial class FormDetect4K : Form {
        public FormDetect4K() {
            InitializeComponent();

            Config.Log.Error("App Start.");
            Config.Log.Fatal("Fai");
            Config.Log.Info("indok");
            Config.Log.Warn("www");
            Config.Log.Debug("dd");

        }
        private void FormDetect4K_FormClosing(object sender, FormClosingEventArgs e) {

            Config.Log.Debug("App Stop.");
        }

        private async void button1_Click(object sender, EventArgs e) {

            ConnectRecord db = new ConnectRecord();
            db.Open("../st.db");
            db.Init();

            var data = new DataGrab() {
                Camera = "test",
                Frame = 1,
                Encoder = 10,
                Timestamp = DateTime.Now.ToString(""),
                Image = new HalconDotNet.HImage("byte", 4096, 1000)
            };

            var tt = await Task.Run(() => {
                return TimeCounting(() => {

                    db.Transaction(() => {
                        for (int i = 0; i < 200; i++) {
                            data.Frame = i + 1;
                            data.Encoder = i * 10;
                            db.GrabInner.Save(data);
                        }
                    });
                });
            });

            db.Close();
            Console.WriteLine(tt);

            MessageBox.Show(tt.ToString());
        }
    }
}
