using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DetectCCD
{
    class Log
    {
        public static ILog AppLog = LogManager.GetLogger("AppLog");
        public static ILog OperateLog = LogManager.GetLogger("OperateLog");
        
        public static void Record(Action act)
        {
            try
            {
                act();
            }
            catch (Exception ex)
            {
                Log.AppLog.Error(string.Format("->"), ex);
            }
        }
        public static void RecordAsThread(Action act)
        {
            Record(() => new Thread(new ThreadStart(() => Record(act))).Start());
        }
    }
}
