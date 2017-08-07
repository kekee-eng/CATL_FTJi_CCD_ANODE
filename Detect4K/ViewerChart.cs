using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Detect4K {
    class ViewerChart {

        public ViewerChart(EntryGrab grab, EntryDetect detect, ViewerImage viewer) {

            //
            this.Grab = grab;
            this.Detect = detect;
            this.ImageViewer = viewer;

        }

        //
        public ViewerImage ImageViewer;
        public EntryGrab Grab;
        public EntryDetect Detect;

        //
        public static Control parentInit(Control parent, Control obj) {
            if (parent.HasChildren) {
                for (int i = parent.Controls.Count - 1; i >= 0; i--) {
                    var ctrl = parent.Controls[i];
                    parent.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }
            parent.Controls.Add(obj);
            obj.Dock = DockStyle.Fill;
            obj.ContextMenu = parent.ContextMenu;
            return obj;
        }
        public static DataGridView gridParentInit(Control parent) {
            var obj= parentInit(parent, new DataGridView()) as DataGridView;
            gridInit(obj);
            return obj;
        }
        public static DataGridView girdParentGet(Control parent) {
            return parent.Controls[0] as DataGridView;
        }

        static void gridInit(DataGridView grid) {

            //
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;

            //
            grid.Rows.Clear();
            grid.Columns.Clear();

        }
        static DataGridViewRow gridAdd(DataGridView grid, params object[] objs) {

            grid.Rows.Insert(0, 1);
            grid.Rows[0].SetValues(objs);
            return grid.Rows[0];

        }
        static void gridStyle(DataGridViewRow row, params bool[] rowStyle) {

            //
            for (int i = 0; i < Math.Min(row.Cells.Count, rowStyle.Length); i++) {
                if (rowStyle[i])
                    row.Cells[i].Style.BackColor = Color.Pink;
            }
        }
        
        static void addTabGrid(DataGridView grid, DataTab dt) {

            //        
            var newRow = gridAdd(grid,
                dt.ID,
                dt.EA,
                dt.TAB,
                dt.TabY1.ToString("0.000"),
                dt.ValWidth.ToString("0.000"),
                dt.ValHeight.ToString("0.000"),
                dt.ValDist.ToString("0.000"),
                dt.ValDistDiff.ToString("0.000")
                );

            //
            gridStyle(newRow,
                dt.IsFail,
                false,
                false,
                false,
                dt.IsWidthFail,
                dt.IsHeightFail,
                dt.IsDistFail,
                dt.IsDistDiffFail
                );

        }
        public void InitTabGrid(Control parent) {

            //
            var grid = gridParentInit(parent);
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "序号" },
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "EA" },
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "TAB" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "位置" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极片宽度" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极耳长度" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极耳间距" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极耳间距差" }
                );

            //
            grid.CurrentCellChanged += (o, e) => {
                if (grid.CurrentCell != null) {
                    ImageViewer.MoveToTAB(grid.Rows.Count - grid.CurrentCell.RowIndex-1);
                }
            };

        }
        public void SyncTabGrid(Control parent) {

            var grid = girdParentGet(parent);

            if (Detect.Tabs.Count > grid.Rows.Count) {

                if (grid.Rows.Count != 0)
                    grid.Rows.RemoveAt(0);

                for (int i = grid.Rows.Count; i < Detect.Tabs.Count; i++)
                    addTabGrid(grid, Detect.Tabs[i]);
            }

            if (Detect.Tabs.Count < grid.Rows.Count) {
                grid.Rows.Clear();
            }

        }

        void addEAGrid(DataGridView grid, DataEA dt) {

            //
            var newRow = gridAdd(grid,
                dt.EA,
                dt.EAY.ToString("0.000"),
                dt.TabCount,
                dt.TabWidthFailCount,
                dt.TabHeightFailCount,
                dt.TabDistFailCount
                );

            //
            gridStyle(newRow,
                dt.IsFail,
                false,
                dt.IsTabCountFail,
                dt.IsTabWidthFailCountFail,
                dt.IsTabHeightFailCountFail,
                dt.IsTabDistFailCountFail
                );

        }
        public void InitEAGrid(Control parent) {

            //
            var grid = gridParentInit(parent);
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "EA" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "位置" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极耳数" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "宽度不良" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "模切不良" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "间距不良" }
                );

            //
            grid.CurrentCellChanged += (o, e) => {
                if (grid.CurrentCell != null) {
                    ImageViewer.MoveToTAB(grid.Rows.Count - grid.CurrentCell.RowIndex-1);
                }
            };

        }
        public void SyncEAGrid(Control parent) {

            var grid = girdParentGet(parent);
            int eaCount = Detect.EACount;

            if (eaCount > grid.Rows.Count) {
                if (grid.Rows.Count != 0)
                    grid.Rows.RemoveAt(0);

                var EAs = Detect.EAs;
                for (int i = grid.Rows.Count; i < eaCount; i++)
                    addEAGrid(grid, EAs[i]);

            }
            if (eaCount < grid.Rows.Count) {
                grid.Rows.Clear();
            }

        }

        void addDefectGrid(DataGridView grid, DataDefect dt) {

            //        
            gridAdd(grid,
                grid.Rows.Count,
                dt.X.ToString("0.000"),
                dt.Y.ToString("0.000"),
                dt.W.ToString("0.000"),
                dt.H.ToString("0.000"),
                dt.Width.ToString("0.000"),
                dt.Height.ToString("0.000")
                );

        }
        public void InitDefectGrid(Control parent) {

            //
            var grid = gridParentInit(parent);
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "序号" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "X" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "Y" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "W" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "H" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "宽度(mm)" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "高度(mm)" }
                );

            //
            grid.CurrentCellChanged += (o, e) => {
                if (grid.CurrentCell != null) {
                    ImageViewer.MoveToDefect(grid.Rows.Count - grid.CurrentCell.RowIndex -1);
                }
            };
        }
        public void SyncDefectGrid(Control parent) {

            var grid = girdParentGet(parent);

            if (Detect.Defects.Count > grid.Rows.Count) {
                for (int i = grid.Rows.Count; i < Detect.Defects.Count; i++)
                    addDefectGrid(grid, Detect.Defects[i]);
            }
            if (Detect.Defects.Count < grid.Rows.Count) {
                grid.Rows.Clear();
            }
        }

        //UnTest
        static void initMergeTabGrid(DataGridView grid) {

            //
            gridInit(grid);
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "序号" },
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "EA" },
                new DataGridViewTextBoxColumn() { Width = 60, HeaderText = "ER" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "内侧位置", Visible = false },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "外侧位置", Visible = false },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "内侧极宽" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "外侧极宽" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "总宽度" }
                );

        }
        static void addMergeTabGrid(DataGridView grid, DataTab dtInside, DataTab dtOutside) {

            //
            if (dtInside.ID != dtOutside.ID)
                throw new Exception("AddSyncER: ID sync error.");

            if (dtInside.EA != dtOutside.EA)
                throw new Exception("AddSyncER: EA sync error.");

            if (dtInside.TAB != dtOutside.TAB)
                throw new Exception("AddSyncER: ER sync error.");

            //        
            var newRow = gridAdd(grid,
                dtInside.ID,
                dtInside.EA,
                dtInside.TAB,
                dtInside.TabY1.ToString("0.000"),
                dtOutside.TabY1.ToString("0.000"),
                dtInside.ValWidth.ToString("0.000"),
                dtOutside.ValWidth.ToString("0.000"),
                (dtInside.ValWidth + dtOutside.ValWidth).ToString("0.000")
                );
            grid.Rows[0].Tag = new object[] { dtInside, dtOutside };

            //
            gridStyle(newRow,
                false,
                false,
                false,
                false,
                false,
                dtInside.IsWidthFail,
                dtOutside.IsWidthFail,
                false
                );

        }

        static void moveToGrid(DataGridView grid, int id) {

            //
            id = id - 1;
            id = Math.Min(grid.Rows.Count - 1, id);
            id = Math.Max(0, id);
            grid.FirstDisplayedScrollingRowIndex = id;
        }

        static void setGraphInit(ZedGraphControl chart) {

            //配置
            chart.ZoomButtons = MouseButtons.None;
            chart.IsEnableHPan = true;
            chart.IsEnableHZoom = true;
            chart.IsEnableVPan = false;
            chart.IsEnableVZoom = false;
            chart.IsShowContextMenu = false;
            chart.IsShowCopyMessage = false;
            chart.IsShowPointValues = true;
            chart.IsZoomOnMouseCenter = true;
            chart.PanButtons2 = MouseButtons.Left;

            //绘制对象
            var g = chart.GraphPane;

            //标题
            g.Title.Text = "";
            g.XAxis.Title.Text = "";
            g.YAxis.Title.Text = "";

            //图例
            g.Legend.IsVisible = true;
            g.Legend.Border.IsVisible = false;

            //坐标轴
            g.XAxis.MajorGrid.IsVisible = false;
            g.YAxis.MajorGrid.IsVisible = true;

            //上下限
            g.YAxis.Scale.Min = 75;
            g.YAxis.Scale.Max = 79;
            g.YAxis.Scale.MajorStep = 0.5;

            g.XAxis.Scale.Min = 0;
            g.XAxis.Scale.Max = 30;
            g.XAxis.Scale.MajorStep = 2;
            g.XAxis.Scale.MajorStepAuto = true;

            //数据
            g.CurveList.Clear();

        }
        static void setGraphAdd(ZedGraphControl chart, string title, Color color, bool visible) {

            var curve = new LineItem(title);
            curve.Color = color;
            curve.Symbol = new Symbol(SymbolType.Square, color);
            curve.Symbol.Fill = new Fill(color, Color.White, color);
            curve.Line.Width = 4;
            curve.IsVisible = visible;
            chart.GraphPane.CurveList.Add(curve);

        }

        static void initTabChart(ZedGraphControl chart) {

            //
            setGraphInit(chart);
            setGraphAdd(chart, "极片宽度", Color.Blue, false);
            setGraphAdd(chart, "极耳长度", Color.Green, false);
            setGraphAdd(chart, "极耳间距", Color.SandyBrown, false);
            setGraphAdd(chart, "极耳间距差", Color.Pink, false);

        }
        static void addTabChart(ZedGraphControl chart, DataTab dt) {

            //绘制对象
            var g = chart.GraphPane;

            //
            g.CurveList[0].AddPoint(dt.ID, dt.ValWidth);
            g.CurveList[1].AddPoint(dt.ID, dt.ValHeight);
            g.CurveList[2].AddPoint(dt.ID, dt.ValDist);
            g.CurveList[3].AddPoint(dt.ID, dt.ValDistDiff);

            chart.Refresh();
        }
        static void setTabChart(ZedGraphControl chart, int select, double min, double max, double step) {

            //绘制对象
            var g = chart.GraphPane;

            //显示对象
            for (int i = 0; i < g.CurveList.Count; i++) {
                g.CurveList[i].IsVisible = (i == select);
            }

            //上下限
            g.YAxis.Scale.Min = min;
            g.YAxis.Scale.Max = max;
            g.YAxis.Scale.MajorStep = step;

        }

        static void initMergeTabChart(ZedGraphControl chart) {

            //
            setGraphInit(chart);
            setGraphAdd(chart, "内侧极宽", Color.Blue, true);
            setGraphAdd(chart, "外侧极宽", Color.Green, true);

        }
        static void addMergeTabChart(ZedGraphControl chart, DataTab dtInside, DataTab dtOutside) {

            //绘制对象
            var g = chart.GraphPane;

            //
            g.CurveList[0].AddPoint(dtInside.ID, dtInside.ValWidth);
            g.CurveList[1].AddPoint(dtInside.ID, dtOutside.ValWidth);

        }
        static void setMergeTabChart(ZedGraphControl chart, double min, double max, double step) {

            //绘制对象
            var g = chart.GraphPane;

            //上下限
            g.YAxis.Scale.Min = min;
            g.YAxis.Scale.Max = max;
            g.YAxis.Scale.MajorStep = step;

        }

        static void applyChart(ZedGraphControl chart) {
            chart.GraphPane.AxisChange();
            chart.Refresh();
        }
        static void moveToChart(ZedGraphControl chart, int id) {

            double min = chart.GraphPane.XAxis.Scale.Min;
            double max = chart.GraphPane.XAxis.Scale.Max;
            double range = max - min;

            chart.GraphPane.XAxis.Scale.Max = id;
            chart.GraphPane.XAxis.Scale.Min = id - range;

        }

    }
}

