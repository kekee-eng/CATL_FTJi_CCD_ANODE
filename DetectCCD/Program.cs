
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

            //初始化主题界面
            UtilTool.XFSkin.Init();

            //显示载入界面
            UtilTool.XFWait.OpenStart();

            //
            Static.Init();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XMain());
            //Application.Run(new FormDetect4K());

            Static.Uninit();
        }
    }
}
