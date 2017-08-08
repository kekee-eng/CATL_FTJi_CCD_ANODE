using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DetectCCD {
    partial class XFViewerChart : DevExpress.XtraEditors.XtraForm {
        public XFViewerChart(ModDevice device, ModRecord record) {
            InitializeComponent();

            this.record = record;
            this.device = device;
        }

        ModRecord record;
        ModDevice device;

        Action<Control> bindInit;
        Action<Control> bindUpdate;
        void viewerInit(Action<Control> init, Action<Control> update) {

            //
            bindInit = init;
            bindUpdate = null;
            bindInit(this);
            bindUpdate = update;

        }

        private void rtInfoApp_Click(object sender, EventArgs e) {

        }
        private void rtInfoInner_Click(object sender, EventArgs e) {

        }
        private void rtInfoOuter_Click(object sender, EventArgs e) {

        }
        private void rtParamApp_Click(object sender, EventArgs e) {

        }
        private void rtParamShare_Click(object sender, EventArgs e) {

        }
        private void rtParamInner_Click(object sender, EventArgs e) {

        }
        private void rtParamOuter_Click(object sender, EventArgs e) {

        }
        private void rtInnerTab_Click(object sender, EventArgs e) {

        }
        private void rtInnerEa_Click(object sender, EventArgs e) {

        }
        private void rtInnerDefect_Click(object sender, EventArgs e) {

        }
        private void rtInnerLabel_Click(object sender, EventArgs e) {

        }
        private void rtInnerTabChartWidth_Click(object sender, EventArgs e) {

        }
        private void rtInnerTabChartHeight_Click(object sender, EventArgs e) {

        }
        private void rtInnerTabChartDistance_Click(object sender, EventArgs e) {

        }
        private void rtInnerTabChartDistdiff_Click(object sender, EventArgs e) {

        }
        private void rtOuterTab_Click(object sender, EventArgs e) {

        }
        private void rtOuterEa_Click(object sender, EventArgs e) {

        }
        private void rtOuterDefect_Click(object sender, EventArgs e) {

        }
        private void rtOuterLabel_Click(object sender, EventArgs e) {

        }
        private void rtOuterTabChartWidth_Click(object sender, EventArgs e) {

        }
        private void rtOuterTabChartHeight_Click(object sender, EventArgs e) {

        }
        private void rtOuterTabChartDistance_Click(object sender, EventArgs e) {

        }
        private void rtOuterTabChartDistdiff_Click(object sender, EventArgs e) {

        }
        private void rtReflush_Click(object sender, EventArgs e) {
            bindInit?.Invoke(this);
        }
    }
}