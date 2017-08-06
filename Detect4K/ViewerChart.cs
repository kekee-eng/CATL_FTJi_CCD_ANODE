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
        
        static void setGridInit(DataGridView grid) {

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
        static void setGridRowStyle(DataGridView grid, int rowId, params bool[] rowStyle) {

            //
            for (int i = 0; i < Math.Min(grid.Rows[rowId].Cells.Count, rowStyle.Length); i++) {
                if (rowStyle[i])
                    grid.Rows[rowId].Cells[i].Style.BackColor = Color.Pink;
            }
        }

        static void addTabGrid(DataGridView grid, DataTab dt) {

            //        
            grid.Rows.Insert(0, 1);
            grid.Rows[0].SetValues(
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
            setGridRowStyle(grid, 0,
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
        public static void SyncTabGrid(DataGridView grid, EntryDetect detect) {

            for (int i = grid.Rows.Count; i < detect.Tabs.Count; i++)
                addTabGrid(grid, detect.Tabs[i]);

        }
        public static void InitTabGrid(DataGridView grid, Action<int> onMove) {

            //
            setGridInit(grid);
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
            grid.CellClick += (o, e) => onMove(grid.Rows.Count - e.RowIndex);

        }

        static void addEAGrid(DataGridView grid, DataEA dt) {

            //        
            grid.Rows.Insert(0, 1);
            grid.Rows[0].SetValues(
                dt.EA,
                dt.EAY.ToString("0.000"),
                dt.TabCount,
                dt.TabWidthFailCount,
                dt.TabHeightFailCount,
                dt.TabDistFailCount
                );

            //
            setGridRowStyle(grid, 0,
                dt.IsFail,
                false,
                dt.IsTabCountFail,
                dt.IsTabWidthFailCountFail,
                dt.IsTabHeightFailCountFail,
                dt.IsTabDistFailCountFail
                );

        }
        public static void SyncEAGrid(DataGridView grid, EntryDetect detect) {

            int eaCount = detect.EACount;
            int gridCount = grid.Rows.Count;
            if(gridCount < eaCount) {

                var EAs = detect.EAs;
                for (int i = gridCount; i < eaCount; i++)
                    addEAGrid(grid, EAs[i]);

            }

        }
        public static void InitEAGrid(DataGridView grid, Action<int> onMove) {

            //
            setGridInit(grid);
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "EA" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "位置" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "极耳数" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "宽度不良" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "模切不良" },
                new DataGridViewTextBoxColumn() { Width = 100, HeaderText = "间距不良" }
                );

            //
            grid.CellClick += (o, e) => {
                if (e.RowIndex >= 0)
                    onMove((int)grid.Rows[e.RowIndex].Cells[0].Value);
            };
        }

        static void addDefectGrid(DataGridView grid, DataDefect dt) {

            //        
            grid.Rows.Insert(0, 1);
            grid.Rows[0].SetValues(
                grid.Rows.Count,
                dt.X.ToString("0.000"),
                dt.Y.ToString("0.000"),
                dt.W.ToString("0.000"),
                dt.H.ToString("0.000"),
                dt.Width.ToString("0.000"),
                dt.Height.ToString("0.000")
                );

        }
        public static void SyncDefectGrid(DataGridView grid, EntryDetect detect) {

            for (int i = grid.Rows.Count; i < detect.Tabs.Count; i++)
                addDefectGrid(grid, detect.Defects[i]);

        }
        public static void InitDefectGrid(DataGridView grid, Action<int> onMove) {

            //
            setGridInit(grid);
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
            grid.CellClick += (o, e) => {
                if (e.RowIndex >= 0)
                    onMove((int)grid.Rows[e.RowIndex].Cells[0].Value-1);
            };
        }








        //UnTest
        static void initMergeTabGrid(DataGridView grid) {

            //
            setGridInit(grid);
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
            grid.Rows.Insert(0, 1);
            grid.Rows[0].SetValues(
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
            setGridRowStyle(grid, 0,
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

