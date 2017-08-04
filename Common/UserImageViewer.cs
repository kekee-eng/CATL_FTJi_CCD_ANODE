using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common {
    public partial class UserImageViewer : UserControl {
        public UserImageViewer() {
            InitializeComponent();

        }

        //初始化控件
        void init() {

            hWindowControl.HInitWindow += (o,e) => {

            };
            hWindowControl.SizeChanged += (o, e) => {

            };
            hWindowControl.HMouseWheel += (o, e) => {

            };
            hWindowControl.MouseDown += (o, e) => {

            };
            hWindowControl.MouseLeave += (o, e) => {

            };
            hWindowControl.MouseUp += (o, e) => {

            };
            hWindowControl.MouseMove += (o, e) => {

            };
        }


    }
}
