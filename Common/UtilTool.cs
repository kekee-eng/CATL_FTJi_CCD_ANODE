
using HalconDotNet;
using System;
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

        public static long TimeCounting(Action act) {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            act();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
        
        public static void BindAutoInfo(string prefix, object obj, DataGridView grid) {

            //
            int q = grid.FirstDisplayedScrollingRowIndex;

            //初始化界面对象
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;

            grid.Columns.Clear();
            grid.Columns.Add("c1", "名称");
            grid.Columns.Add("c2", "值");

            grid.Columns[0].Width = 200;
            grid.Columns[1].Width = 120;

            grid.Columns[0].ReadOnly = true;
            grid.Columns[1].ReadOnly = true;

            //添加行
            grid.Rows.Clear();

            //
            var methods = obj.GetType().GetMethods().TakeWhile(x => x.Name.StartsWith(prefix));
            foreach (var x in methods) {

                //
                var name = x.Name.Replace(prefix, "");

                //
                string val;
                object ret = x.Invoke(obj, null);
                if (x.ReturnType == typeof(string))
                    val = ret.ToString();
                else if (x.ReturnType == typeof(int))
                    val = ret.ToString();
                else if (x.ReturnType == typeof(double))
                    val = ((double)ret).ToString("0.000");
                else if (x.ReturnType == typeof(bool))
                    val = ((bool)ret) ? "On" : "Off";
                else
                    throw new Exception("BindAppInfo: getValueFromMethod");

                //
                grid.Rows.Add(name, val);
                if (x.ReturnType == typeof(bool)) {
                    grid.Rows[grid.Rows.Count - 1].Cells[1].Style.BackColor = ((bool)ret) ? Color.LightGreen : Color.Pink;
                }
                else {
                    grid.Rows[grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightGray;
                }
            }

            //
            if (q != -1)
                grid.FirstDisplayedScrollingRowIndex = q;

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
}
