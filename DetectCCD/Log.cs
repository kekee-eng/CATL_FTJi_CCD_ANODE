using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Threading;
using System.Windows.Forms;

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
            Record(() => new Thread(new ThreadStart(() => Record(act))) { IsBackground = true }.Start());
        }
        
        public static void Quiesce(Action act) {
            try {
                act?.Invoke();
            }
            catch {
            }
        }
        public static void QuiesceThread(Action act) {
            Quiesce(() => new Thread(new ThreadStart(() => Quiesce(act))) { IsBackground = true }.Start());
        }
        public static void Invoke(Control parent, Action act) {
            Quiesce(() => {

                if (!parent.IsHandleCreated || parent.IsDisposed)
                    return;

                if (parent.InvokeRequired) {
                    parent.Invoke(new Action(() => Invoke(parent, act)));
                    return;
                }

                Record(act);
            });
        }
        public static void BeginInvoke(Control parent, Action act) {
            Quiesce(() => {

                if (!parent.IsHandleCreated || parent.IsDisposed)
                    return;

                if (parent.InvokeRequired) {
                    parent.BeginInvoke(new Action(() => BeginInvoke(parent, act)));
                    return;
                }

                Record(act);
            });
        }
        
        public static void Operate(string context) {
            OperateLog.Info(string.Format(@"
操作人：{0}
执行操作：{1}",Static.App.SelectUserName, context));

        }
    }
}
