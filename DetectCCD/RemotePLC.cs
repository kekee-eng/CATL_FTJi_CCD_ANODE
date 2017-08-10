using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

public class RemotePLC : MarshalByRefObject {

    static RemotePLC client;
    public static void InitServer() {

        TcpServerChannel channel = new TcpServerChannel("RemotePLCServer", 777);
        ChannelServices.RegisterChannel(channel, false);
        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(RemotePLC),
            "RemotePLC",
            WellKnownObjectMode.SingleCall);

    }
    public static void InitClient() {

        client = (RemotePLC)Activator.GetObject(
            typeof(RemotePLC),
            string.Format("tcp://localhost:777/RemotePLC"));
    }

    public static void In4KCallPLC_SendLabel(bool isInner, int encoder) {
        try { client._IN_PLC_Call_reciveLabel(isInner, encoder); }
        catch { }
    }
    public static int In4KCallPLC_GetEncoder(bool isInner) {
        try { return client._IN_PLC_Call_encoderProvider(isInner); }
        catch { return 0; }
    }

    //注册此函数
    public static event Action<bool, int> ReciveLabelProcess;
    public void _IN_PLC_Call_reciveLabel(bool isInner, int encoder) {
        ReciveLabelProcess?.Invoke(isInner, encoder);
    }

    //注册此函数
    public static event Func<bool, int> EncoderProvider;
    public int _IN_PLC_Call_encoderProvider(bool isInner) {
        return EncoderProvider(isInner);
    }

}
