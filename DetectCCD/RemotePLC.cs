using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    public class RemotePLC : MarshalByRefObject {

        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel(7777);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefectCount),
                "RemotePLC",
                WellKnownObjectMode.SingleCall);

        }
        public static void InitClient() {
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
        }
        public static void ConnectClient() {

            Static.SafeRun(() => {
                client = (RemoteDefectCount)Activator.GetObject(
                    typeof(RemoteDefectCount),
                    "tcp://localhost:7777/RemotePLC");
            });

        }
        static RemoteDefectCount client;


        public static int GetExtDefectCountRemote(bool isFront, bool isInner, double start, double end, int ea) {

            try {
                if (client == null)
                    return -1;

                return client._master_defect_count(isFront, isInner, start, end, ea);
            }
            catch {
                client = null;
            }
            return -2;
        }

        public static event Func<bool, bool, double, double, int, int> DefectCountProvider;
        public int _master_defect_count(bool isfront, bool isinner, double start, double end, int ea) {

            if (DefectCountProvider == null)
                return -3; //Master未关联

            try { return DefectCountProvider(isfront, isinner, start, end, ea); }
            catch { }
            return -4; //Master函数有问题
        }


    }
}
