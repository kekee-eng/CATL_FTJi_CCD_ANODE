﻿using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

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

    public static void In4KCallPLC_ForWidth(int idEA, int idTab, bool isOK, double widthInner, double widthOuter) {
        try { client._IN_PLC_Call_widthProcess(idEA, idTab, isOK, widthInner, widthOuter); }
        catch { }
    }
    public static void In4KCallPLC_SendLabel(bool isInner, int encoder) {
        try { client._IN_PLC_Call_reciveLabelProcess(isInner, encoder); }
        catch { }
    }
    public static int In4KCallPLC_GetEncoder(bool isInner) {
        try { return client._IN_PLC_Call_encoderProvider(isInner); }
        catch { return 0; }
    }

    //注册事件
    public static event Action<int, int, bool, double, double> WidthProcess;
    public static event Action<bool, int> ReciveLabelProcess;
    public static event Func<bool, int> EncoderProvider;

    public void _IN_PLC_Call_widthProcess(int idEA, int idTab, bool isOK, double widthInner, double widthOuter) {
        WidthProcess?.Invoke(idEA, idTab, isOK, widthInner, widthOuter);
    }
    public void _IN_PLC_Call_reciveLabelProcess(bool isInner, int encoder) {
        ReciveLabelProcess?.Invoke(isInner, encoder);
    }
    public int _IN_PLC_Call_encoderProvider(bool isInner) {
        return EncoderProvider(isInner);
    }

}
