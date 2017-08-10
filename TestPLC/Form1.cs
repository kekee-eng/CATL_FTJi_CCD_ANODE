using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPLC {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            RemotePLC.InitServer();

            RemotePLC.EncoderProvider += (b) => {
                int i = 0;
                int.TryParse(b?textBox1.Text:textBox2.Text, out i);
                return i;
            };
            RemotePLC.ReciveLabelProcess += (b, en) => {
                richTextBox1.Text += string.Format("Camera {0} SendPos {1}\n", b ? "Inner" : "Outer", en);
            };
        }
    }
}
