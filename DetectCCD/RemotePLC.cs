using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using static DetectCCD.UtilSerialization;

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
        RunAction(() => client._IN_PLC_Call_widthProcess(DetectCCD.Static.App.RollName, idEA, idTab, isOK, widthInner, widthOuter));
    }
    public static void In4KCallPLC_SendLabel(bool isInner, int encoder) {
        RunAction(() => client._IN_PLC_Call_reciveLabelProcess(isInner, encoder));
    }
    public static int In4KCallPLC_GetEncoder(bool isInner) {
        int ret = 0;
        RunAction(() => ret = client._IN_PLC_Call_encoderProvider(isInner));
        return ret;
    }
    public static void In4KCallPLC_ClearEncoder() {
        RunAction(() => client._IN_PLC_Call_clearEncoder());
    }
    public static void In4KCallPLC_OnGrabbing() {
        RunAction(() => client._IN_PLC_Call_onGrabbing());
    }
    public static void In4KCallPLC_FilmLevelToMes(FilmLevel f) {
        RunAction(() => client._IN_PLC_Call_filmLevelToMes(obj2bytes(f)));
    }

    public static void In4KCallPLC_AlarmStop(bool isAlarm, bool isStop, string message) {
        RunAction(() => client._IN_PLC_Call_alarmStop(isAlarm, isStop, message));
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

    public static event Action<string, int, int, bool, double, double> WidthProcess;
    public static event Action<bool, int> ReciveLabelProcess;
    public static event Func<bool, int> EncoderProvider;
    public static event Action ClearEncoder;
    public static event Action<FilmLevel> FilmLevelToMes;

    static int grabbingWatchDog = 0;
    static int grabbingWatchDogPrev = 0;
    static int grabbingWatchDogCount = 0;

    public static bool Is4KGrabbing { get { return grabbingWatchDogCount < 10 / 0.2; } }
    public static void Check4KGrabbing() {
        if (grabbingWatchDogPrev != grabbingWatchDog) {
            grabbingWatchDogCount = 0;
        }
        else {
            grabbingWatchDogCount++;
        }
        grabbingWatchDogPrev = grabbingWatchDog;
    }

    public void _IN_PLC_Call_widthProcess(string rollname, int idEA, int idTab, bool isOK, double widthInner, double widthOuter) {
        WidthProcess?.Invoke(rollname, idEA, idTab, isOK, widthInner, widthOuter);
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
    public void _IN_PLC_Call_filmLevelToMes(byte[] filmData) {
        FilmLevelToMes?.Invoke((FilmLevel)bytes2obj(filmData));
    }

    public static event Action<bool, bool, string> AlarmStop;
    public void _IN_PLC_Call_alarmStop(bool isAlarm, bool isStop, string message) {
        AlarmStop?.Invoke(isAlarm, isStop, message);
    }
}


[Serializable]
public class FilmLevel {
    public string FilmNum = "";
    public string MachineNum;
    public DateTime StartTime;
    public DateTime StopTime;
    public string EmployeeNum;

    public double FILM_WIDTH_LEFT_M = 0.0;
    public double FILM_WIDTH_LEFT_S;
    public double FILM_WIDTH_RIGHT_M;
    public double FILM_WIDTH_RIGHT_S;
    public double FILM_WIDTH_LRIGHT_S;

    public double COATED_WIDTH_LEFT_M;
    public double COATED_WIDTH_LEFT_S;
    public double COATED_WIDTH_RIGHT_M;
    public double COATED_WIDTH_RIGHT_S;
    public double COATED_WIDTH_LRIGHT_S;

    public double FILM_SUMWIDTH_M;
    public double FILM_SUMWIDTH_S;
    public double COATED_SUMWIDTH_M;
    public double COATED_SUMWIDTH_S;

    public string FilePathWidth;
    public string FilePathSendToMes;

    public double FilmWidthMin;
    public double FilmWidthMax;
    public double FilmWidthTarget;
    public double CoatedWidthTarget;

    public int EaOkNum;
    public int EaNgNum;
    public int InnerLableNum = 0;
    public int OuterLableNum = 0;
    public int EAContextJoin;
    public int EAContextTag;
    public int EAContextLeakMetal;
    public int EAContextWidth;
}
