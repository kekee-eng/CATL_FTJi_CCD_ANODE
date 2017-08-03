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
        public static string PathApp { get { return Root + "../config/cfg_app.xml"; } }
        public static string PathParam { get { return Root + "../config/cfg_param.xml"; } }
        public static string PathInner { get { return Root + "../config/cfg_inner.xml"; } }
        public static string PathOuter { get { return Root + "../config/cfg_outer.xml"; } }

        public static CfgApp App;
        public static CfgParam Param;
        public static CfgWork Inner;
        public static CfgWork Outer;

        public static ILog Log;

        public static void Init() {

            //
            Action<string> DelFile = x => {
                if (System.IO.File.Exists(x))
                    System.IO.File.Delete(x);
            };
            DelFile(PathApp);
            DelFile(PathParam);
            DelFile(PathInner);
            DelFile(PathOuter);

            //
            App = new CfgApp(PathApp);
            Param = new CfgParam(PathParam);
            Inner = new CfgWork(PathInner);
            Outer = new CfgWork(PathOuter);

            //
            App.Save();
            Param.Save();
            Inner.Save();
            Outer.Save();

            //
            Log = LogManager.GetLogger(System.Windows.Forms.Application.ProductName);

        }

        public static void Uninit() {

        }


    }
}
