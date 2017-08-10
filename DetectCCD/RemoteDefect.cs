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
        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel("RemoteDefect", Static.App.RemotePort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefect),
                "RemoteObject",
                WellKnownObjectMode.SingleCall);

        }
        public static void InitClient() {

            client = (RemoteDefect)Activator.GetObject(
                typeof(RemoteDefect),
                string.Format("tcp://{0}:{1}/RemoteObject", Static.App.RemoteHost, Static.App.RemotePort));

        }

        public static int In4KCall8K_GetDefectCount(bool isFront, bool isInner, double start, double end, int ea) {
            
            try {
                if (client == null)
                    return -1;

                return client._in_8k_return_4k_getDefectCount(isFront, isInner, start, end, ea);
            }
            catch {
                client = null;
            }
            return -2;
        }

        public static event Func<bool, bool, double, double, int, int> _func_in_8k_getDefectCount;
        public int _in_8k_return_4k_getDefectCount(bool isfront, bool isinner, double start, double end, int ea) {

            if (_func_in_8k_getDefectCount == null)
                return -3; //Master未关联

            try { return _func_in_8k_getDefectCount(isfront, isinner, start, end, ea); }
            catch { }
            return -4; //Master函数有问题
        }

    }
}
