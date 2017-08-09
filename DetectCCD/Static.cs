using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DetectCCD {

    class Static {

        public static string Root { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        public static string FolderCfg { get { return Root + "../Config/"; } }
        public static string FolderRecord { get { return Root + "../Record/"; } }
        public static string FolderTemp { get { return Root + "../Temp/"; } }

        public static string PathCfgApp { get { return FolderCfg + "cfg_app.xml"; } }
        public static string PathCfgParam { get { return FolderCfg + "cfg_param.xml"; } }
        public static string PathImageProcess { get { return FolderCfg + "image_process.hdev"; } }

        public static CfgApp App;
        public static CfgParam Param;

        public static ILog Log;

        public static void Init() {

            //
            if (true) {
                Action<string> DelFile = x => {
                    if (System.IO.File.Exists(x))
                        System.IO.File.Delete(x);
                };
                DelFile(PathCfgApp);
                DelFile(PathCfgParam);
            }

            //
            App = new CfgApp(PathCfgApp);
            Param = new CfgParam(PathCfgParam);
         
            //
            App.Save();
            Param.Save();

            //
            Log = LogManager.GetLogger(System.Windows.Forms.Application.ProductName);
            Log.Info("打开应用程序");

            //
            App.select_userid = 0;

        }
        public static void Uninit() {

            App.Save();
            Param.Save();

        }

        public static void SafeRun(Action act) {

            try {
                act();
            }
            catch (Exception ex) {
                Log.Error(string.Format("Run: {0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

    }
}
