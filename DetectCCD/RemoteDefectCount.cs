using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    public class RemoteDefectCount : MarshalByRefObject {

        static RemoteDefectCount client;
        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel(Static.App.RemotePort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefectCount),
                "RemoteObject",
                WellKnownObjectMode.SingleCall);

        }
        public static void InitClient() {

            Static.SafeRun(() => {
                client = (RemoteDefectCount)Activator.GetObject(
                    typeof(RemoteDefectCount),
                    string.Format("tcp://{0}:{1}/RemoteObject", Static.App.RemoteHost, Static.App.RemotePort));
            });

        }

        public static int GetIn4KCall8K(bool isFront, bool isInner, double start, double end, int ea) {
            
            try {
                if (client == null)
                    return -1;

                return client._in_8k_return_4k(isFront, isInner, start, end, ea);
            }
            catch {
                client = null;
            }
            return -2;
        }

        public static event Func<bool, bool, double, double, int, int> _func_in_8k;
        public int _in_8k_return_4k(bool isfront, bool isinner, double start, double end, int ea) {

            if (_func_in_8k == null)
                return -3; //Master未关联

            try { return _func_in_8k(isfront, isinner, start, end, ea); }
            catch { }
            return -4; //Master函数有问题
        }

    }
}
