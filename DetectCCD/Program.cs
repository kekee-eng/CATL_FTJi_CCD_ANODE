
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectCCD {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {

            //
            UtilTool.XFSkin.Init();
            UtilTool.XFWait.OpenStart();
            Static.Init();

            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mm = new XMain();
            Application.Run(mm);

            Static.Uninit();
        }
    }
}
