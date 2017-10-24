using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

public class RemotePLC : MarshalByRefObject {

#if true

    //On 4K
    static void RunAction(Action act) {

        try {
            if (client != null)
                act();
        }
        catch (Exception ex) {
            DetectCCD.Log.AppLog.Error(string.Format("PLC: {0}\n{1}", ex.Message, ex.StackTrace));
            client = null;
        }

    }
    static RemotePLC client;
    public static bool isConnect { get { return client != null; } }
    public static void InitClient() {

        client = (RemotePLC)Activator.GetObject(
            typeof(RemotePLC),
            string.Format("tcp://localhost:777/RemotePLC"));

        //测试连接
        try { client._IN_PLC_Call_clearEncoder(); }
        catch (Exception ex) {
            DetectCCD.Log.AppLog.Error(string.Format("PLC: {0}\n{1}", ex.Message, ex.StackTrace));
            client = null;
            throw;
        }
    }

    public static void In4KCallPLC_ForWidth(int idEA, int idTab, bool isOK, double widthInner, double widthOuter) {
        RunAction(() => client._IN_PLC_Call_widthProcess(idEA, idTab, isOK, widthInner, widthOuter));
    }
    public static void In4KCallPLC_SendLabel(bool isInner, int encoder) {
        RunAction(() => client._IN_PLC_Call_reciveLabelProcess(isInner, encoder));
    }
    public static int In4KCallPLC_GetEncoder(bool isInner) {
        int ret = 0;
        RunAction(() =>ret = client._IN_PLC_Call_encoderProvider(isInner));
        return ret;
    }
    public static void In4KCallPLC_ClearEncoder() {
        RunAction(() => client._IN_PLC_Call_clearEncoder());
    }
    public static void In4KCallPLC_OnGrabbing() {
        RunAction(() => client._IN_PLC_Call_onGrabbing());
    }

#endif

    //On PLC
    public static void InitServer() {

        TcpServerChannel channel = new TcpServerChannel("RemotePLCServer", 777);
        ChannelServices.RegisterChannel(channel, false);
        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(RemotePLC),
            "RemotePLC",
            WellKnownObjectMode.SingleCall);

    }

    public static event Action<string,int, int, bool, double, double> WidthProcess;
    public static event Action<bool, int> ReciveLabelProcess;
    public static event Func<bool, int> EncoderProvider;
    public static event Action ClearEncoder;

    static int grabbingWatchDog = 0;
    static int grabbingWatchDogPrev = 0;
    static int grabbingWatchDogCount = 0;

    public static bool Is4KGrabbing { get { return grabbingWatchDogCount < 10 / 0.2; } }
    public static void Check4KGrabbing() {
        if(grabbingWatchDogPrev!= grabbingWatchDog) {
            grabbingWatchDogCount = 0;
        }
        else {
            grabbingWatchDogCount++;
        }
        grabbingWatchDogPrev = grabbingWatchDog;
    }

    public void _IN_PLC_Call_widthProcess(int idEA, int idTab, bool isOK, double widthInner, double widthOuter) {
        WidthProcess?.Invoke(DetectCCD.Static.App.RollName, idEA, idTab, isOK, widthInner, widthOuter);
    }
    public void _IN_PLC_Call_reciveLabelProcess(bool isInner, int encoder) {
        ReciveLabelProcess?.Invoke(isInner, encoder);
    }
    public int _IN_PLC_Call_encoderProvider(bool isInner) {
        return EncoderProvider(isInner);
    }
    public void _IN_PLC_Call_clearEncoder() {
        ClearEncoder?.Invoke();
    }
    public void _IN_PLC_Call_onGrabbing() {
        grabbingWatchDog++;
    }

}
