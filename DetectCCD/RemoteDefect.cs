using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    public class RemoteDefect : MarshalByRefObject {

        static RemoteDefect client;
        public static bool isConnect { get { return client != null; } }
        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel("RemoteDefect", Static.App.RemotePort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefect),
                "RemoteObject",
                WellKnownObjectMode.SingleCall);

        }
        public static void InitClient() {

            if (client == null) {
                client = (RemoteDefect)Activator.GetObject(
                    typeof(RemoteDefect),
                    string.Format("tcp://{0}:{1}/RemoteObject", Static.App.RemoteHost, Static.App.RemotePort));
            }

            //测试连接
            try { client._in_8k_init(); }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                client = null;
                throw;
            }
        }

        public static int In4KCall8K_GetDefectCount(bool isFront, bool isInner, double start, double end, int ea) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null) return -1;
                return client._in_8k_return_4k_getDefectCount(isFront, isInner, start, end, ea);
            }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                client = null;
            }
            return -2;
        }
        public static event Func<bool, bool, double, double, int, int> _func_in_8k_getDefectCount;
        public int _in_8k_return_4k_getDefectCount(bool isFront, bool isInner, double start, double end, int ea) {

            if (_func_in_8k_getDefectCount == null)
                return -3; //Master未关联

            try { return _func_in_8k_getDefectCount(isFront, isInner, start, end, ea); }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
            }
            return -4; //Master函数有问题
        }

        public static DataDefect[] In4KCall8K_GetDefectList(bool isFront, bool isInner) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null) return null;
                return client._in_8k_return_4k_getDefectList(isFront, isInner);
            }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                client = null;
            }
            return null;
        }
        public static event Func<bool, bool, DataDefect[]> _func_in_8k_getDefectList;
        public DataDefect[] _in_8k_return_4k_getDefectList(bool isFront, bool isInner) {
            try { return _func_in_8k_getDefectList(isFront, isInner); }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                return null;
            }
        }

        public static void In4KCall8K_Viewer(bool isFront, bool isInner, double y) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null) return;
                client._in_8k_viewer(isFront, isInner, y,
                    Static.App.DiffFrameInnerOuter,
                    Static.App.DiffFrameFrontBack,
                    Static.App.DiffFrameInnerFront);
            }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action<bool, bool, double, double, double, double> _func_in_8k_viewer;
        public void _in_8k_viewer(bool isFront, bool isInner, double y, double diffInnerOuter, double diffFrontBack, double diffInnerFront) {
            try { _func_in_8k_viewer(isFront, isInner, y, diffInnerOuter, diffFrontBack, diffInnerFront); }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_Init() {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null) return;
                client._in_8k_init();
            }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action _func_in_8k_init;
        public void _in_8k_init() {
            try { _func_in_8k_init(); }
            catch (Exception ex) {
                Static.Log.Error("RemoteDefect:", ex);
            }
        }

    }
}
