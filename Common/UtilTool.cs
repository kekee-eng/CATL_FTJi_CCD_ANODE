
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common {

    public class UtilTool {

        public class Debug {

            public static long TimeCounting(Action act) {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                act();
                watch.Stop();
                return watch.ElapsedMilliseconds;
            }

        }

        public class Image {

            [DllImport("kernel32.dll")]
            public static extern void CopyMemory(IntPtr dst, IntPtr src, int len);

            [DllImport("kernel32.dll")]
            public static extern void ZeroMemory(IntPtr dst, int len);

            public static void CopyImageOffset(HImage imgDst, HImage imgSrc, int hdst, int hsrc, int hcopy) {

                if (imgDst == null)
                    return;

                //
                string type;
                int w, h;
                IntPtr pdst = imgDst.GetImagePointer1(out type, out w, out h);
                if (imgSrc == null) {

                    //
                    ZeroMemory(pdst + hdst * w, hcopy * w);
                }
                else {

                    //
                    IntPtr psrc = imgSrc.GetImagePointer1(out type, out w, out h);
                    CopyMemory(pdst + hdst * w, psrc + hsrc * w, hcopy * w);
                }
            }

        }

        public class Form {

            public static void AddCaptionTag(Control form) {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var tbuild = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);

                string tag = string.Format("~[Version:{6}]~[Build:{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}]",
                    tbuild.Year, tbuild.Month, tbuild.Day, tbuild.Hour, tbuild.Minute, tbuild.Second, version.ToString());

#if DEBUG
                tag += "~[DEBUG]";
#endif
                form.Text += tag;
            }

            public static void ShowHImage(HWindowControl hwindow, HImage himg) {

                if (himg == null)
                    return;

                try {
                    int w, h;
                    himg.GetImageSize(out w, out h);

                    int w0 = hwindow.Width;
                    int h0 = hwindow.Height;

                    double f0 = 1.0 * w0 / h0;
                    double f1 = 1.0 * w / h;

                    int w1, h1;
                    if (f0 > f1) {
                        h1 = h;
                        w1 = (int)(h * f0);
                    }
                    else {
                        w1 = w;
                        h1 = (int)(w / f0);
                    }

                    hwindow.HalconWindow.SetPart(0, 0, h1, w1);
                    hwindow.HalconWindow.DispImage(himg);
                }
                catch {

                }
            }

        }

        public class AutoInfo {

            public static string ValueToString(object val) {

                if (val.GetType() == typeof(string) ||
                    val.GetType() == typeof(int)) {

                    return val.ToString();
                }
                else if (val.GetType() == typeof(double)) {

                    return ((double)val).ToString("0.000");
                }
                else if (val.GetType() == typeof(bool)) {

                    return ((bool)val) ? "On" : "Off";
                }

                throw new Exception("UtilTool: AutoInfo: ValueToString");
            }
            public static string GetPrivateValue(object obj, string name) {

                var flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

                var d1 = obj.GetType().GetField(name, flag);
                if (d1 != null) {

                    return ValueToString(d1.GetValue(obj));
                }

                var d2 = obj.GetType().GetMethod("get_" + name, flag);
                if (d2 != null) {

                    return ValueToString(d2.Invoke(obj, null));
                }

                throw new Exception("UtilTool: AutoInfo: GetPrivateValue");
            }

            static DataGridView _grid;
            static Dictionary<string, Func<object>> _func;
            public static string C_SPACE_TEXT = ">=====<";
            public static void InitGrid(DataGridView grid, Dictionary<string, Func<object>> func) {

                _grid = grid;
                _func = func;

                //初始化界面对象
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.AllowUserToOrderColumns = false;
                grid.AllowUserToResizeColumns = false;
                grid.AllowUserToResizeRows = false;

                grid.Columns.Clear();
                grid.Columns.Add("c0", "ID");
                grid.Columns.Add("c1", "名称");
                grid.Columns.Add("c2", "值");

                grid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                grid.Columns[0].Width = 40;
                grid.Columns[1].Width = 200;
                grid.Columns[2].Width = 120;

                grid.Columns[0].ReadOnly = true;
                grid.Columns[1].ReadOnly = true;
                grid.Columns[2].ReadOnly = true;

                //添加行
                grid.Rows.Clear();

                int i = 0;
                DataGridViewRow lastTitle = null;
                foreach (KeyValuePair<string, Func<object>> kv in _func) {

                    //
                    string name = kv.Key;
                    string value = ValueToString(kv.Value());

                    //
                    if (value == C_SPACE_TEXT) {

                        if (lastTitle != null) {
                            lastTitle.Cells[0].Value = string.Format("[{0}]", i);
                        }

                        _grid.Rows.Add("", name, value);
                        lastTitle = _grid.Rows[_grid.Rows.Count - 1];
                        lastTitle.Cells[0].Style.BackColor = Color.LightCyan;
                        lastTitle.Cells[1].Style.BackColor = Color.LightCyan;
                        lastTitle.Cells[2].Style.BackColor = Color.LightCyan;

                        i = 0;
                    }
                    else {
                        _grid.Rows.Add(++i, name, "");
                        _grid.Rows[_grid.Rows.Count - 1].Cells[2].Style.BackColor = Color.LightGray;
                    }
                }

                if (lastTitle != null) {
                    lastTitle.Cells[0].Value = string.Format("[{0}]", i);
                }


            }
            public static void Update() {

                bool isChanged = false;
                foreach (KeyValuePair<string, Func<object>> kv in _func) {

                    //
                    string name = kv.Key;
                    string value = ValueToString(kv.Value());

                    //
                    for (int i = 0; i < _grid.Rows.Count; i++) {
                        string name0 = _grid.Rows[i].Cells[1].Value.ToString();
                        string value0 = _grid.Rows[i].Cells[2].Value.ToString();

                        if (name0 == name && value0 != value) {

                            _grid.Rows[i].Cells[2].Value = value;

                            Func<Color> getColor = () => {
                                if (value == "On") return Color.LightGreen;
                                if (value == "Off") return Color.Pink;
                                return Color.LightGray;
                            };
                            
                            _grid.Rows[i].Cells[2].Style.BackColor = getColor();

                            isChanged = true;
                        }
                    }
                }

                if (isChanged)
                    _grid.Invalidate();
            }
        }

    }
}
