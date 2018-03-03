﻿using System;
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
        public static bool isConnect {
            get {
                return client != null;
            }
        }
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
            try {
                client._in_8k_viewer(true, true, 0,
                    Static.App.DiffFrameInnerOuter,
                    Static.App.DiffFrameFrontBack,
                    Static.App.DiffFrameInnerFront,
                    Static.App.DiffFrameInnerFrontFix
                    );
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
                throw;
            }
        }

        public static int In4KCall8K_GetDefectCount(bool isFront, bool isInner, double start, double end, int ea) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return -1;
                return client._in_8k_return_4k_getDefectCount(isFront, isInner, start, end, ea);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
            return -2;
        }

        public static double In4kAlign8k(bool isFront, bool isInner, double position) {
            try {
                if (client == null)
                    return 0;
                return client._In4kAlign8k(isFront, isInner, position);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
                return 0;
            }
        }


        public static event Func<bool, bool, double, double, int, int> _func_in_8k_getDefectCount;
        public int _in_8k_return_4k_getDefectCount(bool isFront, bool isInner, double start, double end, int ea) {

            if (_func_in_8k_getDefectCount == null)
                return -3; //Master未关联

            try {
                return _func_in_8k_getDefectCount(isFront, isInner, start, end, ea);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
            return -4; //Master函数有问题
        }

        public static DataDefect[] In4KCall8K_GetDefectList(bool isFront, bool isInner, int ea) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return null;
                return client._in_8k_return_4k_getDefectList(isFront, isInner, ea);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
            return null;
        }
        public static event Func<bool, bool, int, DataDefect[]> _func_in_8k_getDefectList;
        public DataDefect[] _in_8k_return_4k_getDefectList(bool isFront, bool isInner, int ea) {
            try {
                return _func_in_8k_getDefectList(isFront, isInner, ea);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                return null;
            }
        }

        public static void In4KCall8K_Viewer(bool isFront, bool isInner, double y) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_viewer(isFront, isInner, y,
                    Static.App.DiffFrameInnerOuter,
                    Static.App.DiffFrameFrontBack,
                    Static.App.DiffFrameInnerFront,
                    Static.App.DiffFrameInnerFrontFix
                    );
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Func<bool, bool, double, double> _func_In4kAlign8k;
        public double _In4kAlign8k(bool isFront, bool isInner, double position) {
            if (_func_In4kAlign8k == null)
                return Static.App.DiffFrameInnerFrontFix; //Master未关联
            try {
                return _func_In4kAlign8k(isFront, isInner, position);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                return Static.App.DiffFrameInnerFrontFix;
            }
        }

        public static event Action<bool, bool, double, double, double, double, double> _func_in_8k_viewer;
        public void _in_8k_viewer(bool isFront, bool isInner, double y, double diffInnerOuter, double diffFrontBack, double diffInnerFront, double diffInnerFrontFix) {
            try {
                _func_in_8k_viewer(isFront, isInner, y, diffInnerOuter, diffFrontBack, diffInnerFront, diffInnerFrontFix);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_Init() {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_init();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action _func_in_8k_init;
        public void _in_8k_init() {
            try {
                _func_in_8k_init();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_StartGrab() {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_startGrab();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action _func_in_8k_startGrab;
        public void _in_8k_startGrab() {
            try {
                _func_in_8k_startGrab();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_StopGrab() {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_stopGrab();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action _func_in_8k_stopGrab;
        public void _in_8k_stopGrab() {
            try {
                _func_in_8k_stopGrab();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_Uninit() {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_uninit();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action _func_in_8k_uninit;
        public void _in_8k_uninit() {
            try {
                _func_in_8k_uninit();
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KCall8K_SetRoll() {

            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            try {
                if (client == null)
                    return;
                client._in_8k_setRoll(Static.App, Static.Recipe, Static.Tiebiao);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }
        }
        public static event Action<CfgApp, CfgRecipe, CfgTiebiao> _func_in_8k_setRoll;
        public void _in_8k_setRoll(CfgApp cfgapp, CfgRecipe cfgrecipe, CfgTiebiao cfgtiebiao) {
            try {
                _func_in_8k_setRoll(cfgapp, cfgrecipe, cfgtiebiao);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
            }
        }

        public static void In4KGet8KFrame(out int front, out int back) {
            if (Static.App.Is8K)
                throw new Exception("In4KCall8K: don't in 8k use it.");

            front = 0;
            back = 0;

            try {
                if (client == null)
                    return;
                client._in_8k_getFrame(out front, out back);
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                client = null;
            }

        }
        public static event Func<int[]> _func_in_8k_getFrame;
        public void _in_8k_getFrame(out int front, out int back) {
            try {
                var values = _func_in_8k_getFrame();
                front = values[0];
                back = values[1];
            }
            catch (Exception ex) {
                Log.AppLog.Error("RemoteDefect:", ex);
                front = 0;
                back = 0;
            }
        }
    }
}
