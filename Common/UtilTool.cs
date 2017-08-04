
using System;
using System.Diagnostics;
using System.Reflection;
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

        public static long TimeCounting(Action act) {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            act();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

    }
}
