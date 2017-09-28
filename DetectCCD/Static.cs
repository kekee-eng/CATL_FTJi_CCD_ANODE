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
        public static string FolderRecord {
            get {
                //保存路径结构修改为：日期 ->品种 ->膜卷号 ->分类 ->大图 /小图
                Func<string, string> fixPath = (s) => s == "" ? "[UNKNOW]" : s;
                return string.Format("../Record/{0}/{1}/{2}",
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    fixPath(Static.Recipe.RecipeName),
                    fixPath(Static.App.RollName));
            }
        }
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
