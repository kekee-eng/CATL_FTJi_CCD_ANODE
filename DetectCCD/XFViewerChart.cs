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

        Dictionary<string, Func<object>> getMonitorApp() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["App"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["App_RunTime"] = () => UtilPerformance.GetAppRuntime();
            monitor["App_MemoryTotal"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryTotal());
            monitor["App_MemoryLoad"] = () => string.Format("{0:0.0} M", UtilPerformance.GetMemoryLoad());
            monitor["App_CpuCount"] = () => UtilPerformance.GetCpuCount();
            monitor["App_CpuLoad"] = () => string.Format("{0:0.00} %", UtilPerformance.GetCpuLoad());

            monitor["ImageProcess"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["ImageProcess_DetectTab"] = () => ImageProcess.TimeDetectTab;
            monitor["ImageProcess_DetectWidth"] = () => ImageProcess.TimeDetectWidth;
            monitor["ImageProcess_DetectMark"] = () => ImageProcess.TimeDetectMark;
            monitor["ImageProcess_DetectDefect"] = () => ImageProcess.TimeDetectDefect;

            //
            return monitor;

        }
        Dictionary<string, Func<object>> getMonitorInner() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["Inner_Grab"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Grab_Path"] = () => UtilTool.AutoInfo.GetPrivateValue(device.InnerCamera, "m_filename");
            monitor["Inner_Grab_Name"] = () => device.InnerCamera.CameraName;
            monitor["Inner_Grab_IsReady"] = () => device.InnerCamera.isOpen;
            monitor["Inner_Grab_IsRun"] = () => device.InnerCamera.isGrabbing;
            monitor["Inner_Grab_Frame"] = () => device.InnerCamera.m_frame;
            monitor["Inner_Grab_FrameReset"] = () => device.InnerCamera.m_frameStart;
            monitor["Inner_Grab_FrameMax"] = () => device.InnerCamera.Max;
            monitor["Inner_Grab_FpsControl"] = () => device.InnerCamera.m_fpsControl;
            monitor["Inner_Grab_FpsRealtime"] = () => device.InnerCamera.m_fpsRealtime;

            monitor["Inner_Record"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Record_GrabCacheMin"] = () => record.InnerGrab.Cache.Min;
            monitor["Inner_Record_GrabCacheMax"] = () => record.InnerGrab.Cache.Max;
            monitor["Inner_Record_GrabCacheCount"] = () => record.InnerGrab.Cache.Count;

            monitor["Inner_Record_GrabDBMin"] = () => record.InnerGrab.DB.Min;
            monitor["Inner_Record_GrabDBMax"] = () => record.InnerGrab.DB.Max;
            monitor["Inner_Record_GrabDBCount"] = () => record.InnerGrab.DB.Count;

            monitor["Inner_Record_GrabCacheRemain"] = () => Math.Max(0, record.InnerGrab.Cache.Max - record.InnerGrab.DB.Max);

            monitor["Inner_Record_LastLoadCache"] = () => record.InnerGrab.LastLoadCache;
            monitor["Inner_Record_LastLoadDB"] = () => record.InnerGrab.LastLoadDB;

            monitor["Inner_Record_TabCount"] = () => record.InnerDetect.Tabs.Count;
            monitor["Inner_Record_EACount"] = () => record.InnerDetect.EACount;
            monitor["Inner_Record_DefectCount"] = () => record.InnerDetect.Defects.Count;
            monitor["Inner_Record_LabelCount"] = () => record.InnerDetect.Labels.Count;

            monitor["Inner_Viewer"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Inner_Viewer_showImageStatic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showImageStatic");
            monitor["Inner_Viewer_showImageDynamic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showImageDynamic");
            monitor["Inner_Viewer_showContextMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextMark");
            monitor["Inner_Viewer_showContextTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextTab");
            monitor["Inner_Viewer_showContextWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextWidth");
            monitor["Inner_Viewer_showContextDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextDefect");
            monitor["Inner_Viewer_showContextLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextLabel");
            monitor["Inner_Viewer_showContextCross"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "showContextCross");

            monitor["Inner_Viewer_countShowTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowTab");
            monitor["Inner_Viewer_countShowMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowMark");
            monitor["Inner_Viewer_countShowDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowDefect");
            monitor["Inner_Viewer_countShowLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "countShowLabel");

            monitor["Inner_Viewer_fpsControl"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "fpsControl");
            monitor["Inner_Viewer_fpsRealtime"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "fpsRealtime");

            monitor["Inner_Viewer_mouseAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "mouseAllow");
            monitor["Inner_Viewer_targetAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetAllow");
            monitor["Inner_Viewer_targetVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVs");
            monitor["Inner_Viewer_targetVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVx");
            monitor["Inner_Viewer_targetVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetVy");
            monitor["Inner_Viewer_targetDist"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "targetDist");

            monitor["Inner_Viewer_frameVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVs");
            monitor["Inner_Viewer_frameVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVx");
            monitor["Inner_Viewer_frameVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameVy");
            monitor["Inner_Viewer_frameDx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameDx");
            monitor["Inner_Viewer_frameDy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameDy");
            monitor["Inner_Viewer_frameX1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameX1");
            monitor["Inner_Viewer_frameX2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameX2");
            monitor["Inner_Viewer_frameY1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameY1");
            monitor["Inner_Viewer_frameY2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameY2");

            monitor["Inner_Viewer_frameStart"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStart");
            monitor["Inner_Viewer_frameEnd"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEnd");
            monitor["Inner_Viewer_frameStartRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStartRequire");
            monitor["Inner_Viewer_frameEndRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEndRequire");
            monitor["Inner_Viewer_frameStartLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameStartLimit");
            monitor["Inner_Viewer_frameEndLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "frameEndLimit");

            //monitor["Inner_Viewer_grabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "grabWidth");
            //monitor["Inner_Viewer_grabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "grabHeight");
            //monitor["Inner_Viewer_boxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "boxWidth");
            //monitor["Inner_Viewer_boxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "boxHeight");
            //monitor["Inner_Viewer_refGrabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refGrabWidth");
            //monitor["Inner_Viewer_refGrabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refGrabHeight");
            //monitor["Inner_Viewer_refBoxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refBoxWidth");
            //monitor["Inner_Viewer_refBoxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.InnerViewerImage, "refBoxHeight");

            //
            return monitor;

        }
        Dictionary<string, Func<object>> getMonitorOuter() {

            //
            var monitor = new Dictionary<string, Func<object>>();

            monitor["Outer_Grab"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Outer_Grab_Path"] = () => UtilTool.AutoInfo.GetPrivateValue(device.OuterCamera, "m_filename");
            monitor["Outer_Grab_Name"] = () => device.OuterCamera.CameraName;
            monitor["Outer_Grab_IsReady"] = () => device.OuterCamera.isOpen;
            monitor["Outer_Grab_IsRun"] = () => device.OuterCamera.isGrabbing;
            monitor["Outer_Grab_Frame"] = () => device.OuterCamera.m_frame;
            monitor["Outer_Grab_FrameReset"] = () => device.OuterCamera.m_frameStart;
            monitor["Outer_Grab_FrameMax"] = () => device.OuterCamera.Max;
            monitor["Outer_Grab_FpsControl"] = () => device.OuterCamera.m_fpsControl;
            monitor["Outer_Grab_FpsRealtime"] = () => device.OuterCamera.m_fpsRealtime;

            monitor["Outer_Record"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Outer_Record_GrabCacheMin"] = () => record.OuterGrab.Cache.Min;
            monitor["Outer_Record_GrabCacheMax"] = () => record.OuterGrab.Cache.Max;
            monitor["Outer_Record_GrabCacheCount"] = () => record.OuterGrab.Cache.Count;

            monitor["Outer_Record_GrabDBMin"] = () => record.OuterGrab.DB.Min;
            monitor["Outer_Record_GrabDBMax"] = () => record.OuterGrab.DB.Max;
            monitor["Outer_Record_GrabDBCount"] = () => record.OuterGrab.DB.Count;

            monitor["Outer_Record_GrabCacheRemain"] = () => Math.Max(0, record.OuterGrab.Cache.Max - record.OuterGrab.DB.Max);

            monitor["Outer_Record_LastLoadCache"] = () => record.OuterGrab.LastLoadCache;
            monitor["Outer_Record_LastLoadDB"] = () => record.OuterGrab.LastLoadDB;

            monitor["Outer_Record_TabCount"] = () => record.OuterDetect.Tabs.Count;
            monitor["Outer_Record_EACount"] = () => record.OuterDetect.EACount;
            monitor["Outer_Record_DefectCount"] = () => record.OuterDetect.Defects.Count;
            monitor["Outer_Record_LabelCount"] = () => record.OuterDetect.Labels.Count;

            monitor["Outer_Viewer"] = () => UtilTool.AutoInfo.C_SPACE_TEXT;
            monitor["Outer_Viewer_showImageStatic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showImageStatic");
            monitor["Outer_Viewer_showImageDynamic"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showImageDynamic");
            monitor["Outer_Viewer_showContextMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextMark");
            monitor["Outer_Viewer_showContextTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextTab");
            monitor["Outer_Viewer_showContextWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextWidth");
            monitor["Outer_Viewer_showContextDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextDefect");
            monitor["Outer_Viewer_showContextLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextLabel");
            monitor["Outer_Viewer_showContextCross"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "showContextCross");

            monitor["Outer_Viewer_countShowTab"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "countShowTab");
            monitor["Outer_Viewer_countShowMark"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "countShowMark");
            monitor["Outer_Viewer_countShowDefect"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "countShowDefect");
            monitor["Outer_Viewer_countShowLabel"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "countShowLabel");

            monitor["Outer_Viewer_fpsControl"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "fpsControl");
            monitor["Outer_Viewer_fpsRealtime"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "fpsRealtime");

            monitor["Outer_Viewer_mouseAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "mouseAllow");
            monitor["Outer_Viewer_targetAllow"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "targetAllow");
            monitor["Outer_Viewer_targetVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "targetVs");
            monitor["Outer_Viewer_targetVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "targetVx");
            monitor["Outer_Viewer_targetVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "targetVy");
            monitor["Outer_Viewer_targetDist"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "targetDist");

            monitor["Outer_Viewer_frameVs"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameVs");
            monitor["Outer_Viewer_frameVx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameVx");
            monitor["Outer_Viewer_frameVy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameVy");
            monitor["Outer_Viewer_frameDx"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameDx");
            monitor["Outer_Viewer_frameDy"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameDy");
            monitor["Outer_Viewer_frameX1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameX1");
            monitor["Outer_Viewer_frameX2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameX2");
            monitor["Outer_Viewer_frameY1"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameY1");
            monitor["Outer_Viewer_frameY2"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameY2");

            monitor["Outer_Viewer_frameStart"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameStart");
            monitor["Outer_Viewer_frameEnd"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameEnd");
            monitor["Outer_Viewer_frameStartRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameStartRequire");
            monitor["Outer_Viewer_frameEndRequire"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameEndRequire");
            monitor["Outer_Viewer_frameStartLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameStartLimit");
            monitor["Outer_Viewer_frameEndLimit"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "frameEndLimit");

            //monitor["Outer_Viewer_grabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "grabWidth");
            //monitor["Outer_Viewer_grabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "grabHeight");
            //monitor["Outer_Viewer_boxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "boxWidth");
            //monitor["Outer_Viewer_boxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "boxHeight");
            //monitor["Outer_Viewer_refGrabWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "refGrabWidth");
            //monitor["Outer_Viewer_refGrabHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "refGrabHeight");
            //monitor["Outer_Viewer_refBoxWidth"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "refBoxWidth");
            //monitor["Outer_Viewer_refBoxHeight"] = () => UtilTool.AutoInfo.GetPrivateValue(record.OuterViewerImage, "refBoxHeight");

            //
            return monitor;

        }

        private void rtInfoApp_Click(object sender, EventArgs e) {
            viewerInit(
                x => UtilTool.AutoInfo.InitGrid(ViewerChart.parentInitGrid(x), getMonitorApp()),
                x => UtilTool.AutoInfo.Update());
        }
        private void rtInfoInner_Click(object sender, EventArgs e) {
            viewerInit(
                x => UtilTool.AutoInfo.InitGrid(ViewerChart.parentInitGrid(x), getMonitorInner()),
                x => UtilTool.AutoInfo.Update());
        }
        private void rtInfoOuter_Click(object sender, EventArgs e) {
            viewerInit(
                x => UtilTool.AutoInfo.InitGrid(ViewerChart.parentInitGrid(x), getMonitorOuter()),
                x => UtilTool.AutoInfo.Update());
        }

        private void rtParamApp_Click(object sender, EventArgs e) {
            viewerInit(x => Static.ParamApp.BindDataGridView(ViewerChart.parentInitGrid(x)), null);
        }
        private void rtParamShare_Click(object sender, EventArgs e) {
            viewerInit(x => Static.ParamShare.BindDataGridView(ViewerChart.parentInitGrid(x)), null);
        }
        private void rtParamInner_Click(object sender, EventArgs e) {
            viewerInit(x => Static.ParamInner.BindDataGridView(ViewerChart.parentInitGrid(x)), null);
        }
        private void rtParamOuter_Click(object sender, EventArgs e) {
            viewerInit(x => Static.ParamOuter.BindDataGridView(ViewerChart.parentInitGrid(x)), null);
        }

        private void rtInnerTab_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitTabGrid(x), x => record.InnerViewerChart.SyncTabGrid(x));
        }
        private void rtInnerEa_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitEAGrid(x), x => record.InnerViewerChart.SyncEAGrid(x));
        }
        private void rtInnerDefect_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitDefectGrid(x), x => record.InnerViewerChart.SyncDefectGrid(x));
        }
        private void rtInnerLabel_Click(object sender, EventArgs e) {

        }
        private void rtInnerTabChartWidth_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitTabChart(x), x => record.InnerViewerChart.SyncTabChart(x, 0));
        }
        private void rtInnerTabChartHeight_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitTabChart(x), x => record.InnerViewerChart.SyncTabChart(x, 1));
        }
        private void rtInnerTabChartDistance_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitTabChart(x), x => record.InnerViewerChart.SyncTabChart(x, 2));
        }
        private void rtInnerTabChartDistdiff_Click(object sender, EventArgs e) {
            viewerInit(x => record.InnerViewerChart.InitTabChart(x), x => record.InnerViewerChart.SyncTabChart(x, 3));
        }

        private void rtOuterTab_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitTabGrid(x), x => record.OuterViewerChart.SyncTabGrid(x));
        }
        private void rtOuterEa_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitEAGrid(x), x => record.OuterViewerChart.SyncEAGrid(x));
        }
        private void rtOuterDefect_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitDefectGrid(x), x => record.OuterViewerChart.SyncDefectGrid(x));
        }
        private void rtOuterLabel_Click(object sender, EventArgs e) {

        }
        private void rtOuterTabChartWidth_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitTabChart(x), x => record.OuterViewerChart.SyncTabChart(x, 0));
        }
        private void rtOuterTabChartHeight_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitTabChart(x), x => record.OuterViewerChart.SyncTabChart(x, 1));
        }
        private void rtOuterTabChartDistance_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitTabChart(x), x => record.OuterViewerChart.SyncTabChart(x, 2));
        }
        private void rtOuterTabChartDistdiff_Click(object sender, EventArgs e) {
            viewerInit(x => record.OuterViewerChart.InitTabChart(x), x => record.OuterViewerChart.SyncTabChart(x, 3));
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (rtReflush.Checked) {
                bindUpdate?.Invoke(this);
            }
        }
        private void rtReflush_Click(object sender, EventArgs e) {
            rtReflush.Checked ^= true;
            if (rtReflush.Checked) {
                bindInit?.Invoke(this);
            }
        }
        private void rtReflushTime_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {

                int i;
                if (int.TryParse(rtReflushTime.Text, out i)) {
                    i = Math.Min(i, 2000);
                    i = Math.Max(i, 10);
                    rtReflushTime.Text = i.ToString();
                    timer1.Interval = i;
                }

            }
        }




    }
}