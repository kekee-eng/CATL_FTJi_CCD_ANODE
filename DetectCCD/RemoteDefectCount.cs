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
        public static int GetExtDefectCountRemote(bool isFront, bool isInner, double start, double end, int ea) {

            int count = -1;
            Static.SafeRun(() => count= client._master_defect_count(isFront, isInner, start, end, ea));
            return count;

        }

        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel(Static.App.RemotePort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefectCount),
                "RemoteObject",
                WellKnownObjectMode.SingleCall);

        }
        public static void InitClient() {
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
        }
        public static void ConnectClient() {

            Static.SafeRun(() => {
                client = (RemoteDefectCount)Activator.GetObject(
                    typeof(RemoteDefectCount),
                    string.Format("tcp://{0}:{1}/RemoteObject", Static.App.RemoteHost, Static.App.RemotePort));
            });

        }

        public static event Func<bool, bool, double, double, int, int> DefectCountProvider;
        public int _master_defect_count(bool isfront, bool isinner, double start, double end, int ea) {

            if (DefectCountProvider == null)
                return -2; //Master未关联

            try { return DefectCountProvider(isfront, isinner, start, end, ea); }
            catch { }
            return -3; //Master函数有问题
        }

    }
}
