﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using System.IO;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DetectCCD {

   public class Static {

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
            App = new CfgApp(PathCfgApp);
            Param = new CfgParam(PathCfgParam);
         
            //
            App.Save();
            Param.Save();

            //
            Log = LogManager.GetLogger(System.Windows.Forms.Application.ProductName);
            Log.Info("打开应用程序");

            //
            App.SelectUserId = 0;
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
                Log.Error(string.Format("->"),ex);
            }
        }
        public static void SafeRunThread(Action act) {
            new Thread(new ThreadStart(() => SafeRun(act))).Start();
        }

    }
}
