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
        public static string PathCfgRecipe { get { return FolderCfg + "cfg_param.xml"; } }
        public static string PathCfgTiebiao { get { return FolderCfg + "cfg_tiebiao.xml"; } }
        public static string PathImageProcess { get { return FolderCfg + "image_process.hdev"; } }

        public static CfgApp App;
        public static CfgRecipe Recipe;
        public static CfgTiebiao Tiebiao;
        
        public static void Init() {
            
            //
            App = new CfgApp(PathCfgApp);
            Recipe = new CfgRecipe(PathCfgRecipe);
            Tiebiao = new CfgTiebiao(PathCfgTiebiao);

            //
            App.Save();
            Recipe.Save();
            Tiebiao.Save();

            //
            App.SelectUserId = 2;
        }
        public static void Uninit() {

            App.Save();
            Recipe.Save();

        }
        
    }
}
