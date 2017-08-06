using Common;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detect4K {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            
            Config.Init();

            List<object> myData = new List<object>();
            for(int i=0;i<10000;i++) {

                DataTab dt = new DataTab();

                myData.Add(dt);
            }

            var kk =UtilSerialization.obj2bytes(myData);
            int p = kk.Length;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDetect4K());
        }
    }
}
