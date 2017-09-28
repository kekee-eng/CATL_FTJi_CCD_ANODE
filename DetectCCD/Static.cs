using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using System.IO;
using System.Threading;

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
        public static CfgRecipe Param;
        public static CfgTiebiao Tiebiao;
        
        public static void Init() {
            
            //
            App = new CfgApp(PathCfgApp);
            Param = new CfgRecipe(PathCfgParam);
         
            //
            App.Save();
            Param.Save();
            
            //
            App.SelectUserId = 0;
        }
        public static void Uninit() {

            App.Save();
            Param.Save();

        }
        
    }
}
