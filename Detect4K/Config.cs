using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Detect4K {

    class Config {

        public static string Root { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        public static string FolderCfg { get { return Root + "../Config/"; } }
        public static string FolderRecord { get { return Root + "../Record/"; } }
        public static string FolderTemp { get { return Root + "../Temp/"; } }

        public static string PathCfgApp { get { return FolderCfg + "cfg_app.xml"; } }
        public static string PathCfgParam { get { return FolderCfg + "cfg_param.xml"; } }
        public static string PathCfgInner { get { return FolderCfg + "cfg_inner.xml"; } }
        public static string PathCfgOuter { get { return FolderCfg + "cfg_outer.xml"; } }
        public static string PathImageProcess { get { return FolderCfg + "image_process.hdev"; } }

        public static CfgApp App;
        public static CfgParamShare ParamShare;
        public static CfgParamSelf ParamInner;
        public static CfgParamSelf ParamOuter;

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
                DelFile(PathCfgInner);
                DelFile(PathCfgOuter);
            }

            //
            App = new CfgApp(PathCfgApp);
            ParamShare = new CfgParamShare(PathCfgParam);
            ParamInner = new CfgParamSelf(PathCfgInner);
            ParamOuter = new CfgParamSelf(PathCfgOuter);

            //
            App.Save();
            ParamShare.Save();
            ParamInner.Save();
            ParamOuter.Save();

            //
            Log = LogManager.GetLogger(System.Windows.Forms.Application.ProductName);

        }

        public static void Uninit() {

        }


    }
}
