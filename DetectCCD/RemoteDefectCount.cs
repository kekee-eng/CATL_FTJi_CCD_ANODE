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

        public RemoteDefectCount(bool isInner) {

            this.callFromInner = isInner;
        }

        RemoteDefectCount client;
        bool callFromInner;

        public int GetExtDefectCount(bool isFront, double start, double end, int ea) {

            try {
                if (client == null)
                    ChannelServices.RegisterChannel(new TcpClientChannel(), false);
                client = (RemoteDefectCount)Activator.GetObject(
                    typeof(RemoteDefectCount),
                    string.Format("tcp://{0}:{1}/RemoteObject", Static.App.RemoteHost, Static.App.RemotePort));

            }
            catch {
                client = null;
                return -1; //无连接
            }

            try { return client.GetMasterDefectCounr(isFront, callFromInner, start, end, ea); }
            catch {  }
            return -2; //掉线
        }

        public static void InitServer() {

            TcpServerChannel channel = new TcpServerChannel(Static.App.RemotePort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteDefectCount),
                "RemoteObject",
                WellKnownObjectMode.SingleCall);

        }
        public static event Func<bool, bool, double, double, int, int> DefectCountProvider;
        int GetMasterDefectCounr(bool isfront, bool isinner, double start, double end, int ea) {

            if (DefectCountProvider == null)
                return -3; //Master未关联

            try { return DefectCountProvider(isfront, isinner, start, end, ea); }
            catch {}
            return -4; //Master函数有问题
        }


    }
}
