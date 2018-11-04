
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

            var path = @"D:\Users\fra\Desktop\LNG2\2018_11_03_18_14_";
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            var fis = fi.Directory.GetFiles().Where(x=>x.FullName.StartsWith(path)).ToList();
            if(fis.Count > 0) {
                System.Diagnostics.Process.Start(fis[0].FullName);
            }

            //
            UtilTool.XFSkin.Init();
            UtilTool.XFWait.OpenStart();
            Static.Init();
            AuthManage.CheckKey();

            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var mm = new XMain();
            Application.Run(mm);

            Static.Uninit();
        }
    }
}
