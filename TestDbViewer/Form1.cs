using DetectCCD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDbViewer {
    public partial class Form1 : Form {
        public Form1(string path) {
            InitializeComponent();

            Static.Init();
            Record.Init();
            Record.Open(path);

            Record.InnerViewerImage.Init(hwinInner);
            Record.OuterViewerImage.Init(hwinOuter);

            Record.InnerViewerImage.SetCenterTarget(Record.InnerGrab.Min);
            Record.OuterViewerImage.SetCenterTarget(Record.OuterGrab.Min);

            Record.InnerViewerImage.MoveTargetDirect();
            Record.OuterViewerImage.MoveTargetDirect();

        }

        ModRecord Record = new ModRecord();
        
    }
}
