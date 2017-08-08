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
        public static string PathCfgInner { get { return FolderCfg + "cfg_inner.xml"; } }
        public static string PathCfgOuter { get { return FolderCfg + "cfg_outer.xml"; } }
        public static string PathImageProcess { get { return FolderCfg + "image_process.hdev"; } }

        public static CfgParamApp ParamApp;
        public static CfgParamShare ParamShare;
        public static CfgParamSelf ParamInner;
        public static CfgParamSelf ParamOuter;

        public static ILog Log;

        public static void Init() {

            //
            if (false) {
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
            ParamApp = new CfgParamApp(PathCfgApp);
            ParamShare = new CfgParamShare(PathCfgParam);
            ParamInner = new CfgParamSelf(PathCfgInner);
            ParamOuter = new CfgParamSelf(PathCfgOuter);

            //
            ParamApp.Save();
            ParamShare.Save();
            ParamInner.Save();
            ParamOuter.Save();

            //
            Log = LogManager.GetLogger(System.Windows.Forms.Application.ProductName);
            
        }
        public static void Uninit() {

            ParamApp.Save();
            ParamShare.Save();
            ParamInner.Save();
            ParamOuter.Save();

        }
        
        public static void SafeRun(Action act) {

            try {
                act();
            }
            catch (Exception ex) {
                Log.Error(string.Format("{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

    }
}
