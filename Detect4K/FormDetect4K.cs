using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
