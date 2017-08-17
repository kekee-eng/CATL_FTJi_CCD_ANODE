using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    public class CfgApp : TemplateConfig {
        public CfgApp(string path) : base(path) { }

        //
        public bool ImageProcessReload = false;

        //
        public bool EnableLabelEA = false;
        public bool EnableLabelEA_Force = false;
        public bool EnableLabelEA_EveryOne = false;
        public bool EnableLabelDefect = false;

        public bool LabelContextJoin = false;
        public bool LabelContextTag = false;
        public bool LabelContextLeakMetal = false;

        public bool EAContextJoin = false;
        public bool EAContextTag = false;
        public bool EAContextLeakMetal = false;
        
        //记录参数
        public int RecordCacheSize = 200;
        public bool RecordSaveImageEnable = false;
        public bool RecordSaveImageOK = false;
        public bool RecordSaveImageNG = false;
        public bool RecordSaveImageNGSmall = false;

        //检测内容
        public bool DetectEnable = true;
        public bool DetectTab = false;
        public bool DetectWidth = false;
        public bool DetectMark = false;
        public bool DetectDefect = false;

        //显示参数
        public double CameraFpsControl = 10;
        public int CameraFrameStart = 1;

        //
        public string RemoteHost = "localhost";
        public int RemotePort = 6500;
        public double DiffFrameInnerFront = -33.51;
        public double DiffFrameInnerOuter = 18.15;
        public double DiffFrameFrontBack = 2.225;

        public double FixFrameInnerFrontScale = 1.12816987473266;
        public double FixFrameOuterOrBackOffset {
            get {
                if (Is4K) return DiffFrameInnerOuter;
                return DiffFrameFrontBack;
            }
        }
        public double FrameInnerToFront(bool isFront, bool isInner, double i) {

            var eInner = i - (isInner ? 0 : DiffFrameInnerOuter);
            var eFrontScale = eInner * FixFrameInnerFrontScale;
            var eFront = eFrontScale + DiffFrameInnerFront;
            var eOutput = eFront + (isFront ? 0 : DiffFrameFrontBack);

            return (eOutput);
        }
        public double FrameFrontToInner(bool isFront, bool isInner, double i) {

            var eInput = i - (isFront ? 0 : DiffFrameFrontBack);
            var eInnerScale = eInput - DiffFrameInnerFront;
            var eInner = eInnerScale / FixFrameInnerFrontScale;
            var eOutput = eInner + (isInner ? 0 : DiffFrameInnerOuter);

            return (eOutput);
        }


        //运行模式：0=在线（实时检测）、1=离线（仿真）
        public int RunningMode = 0;
        public string RollName = "";

        public bool ModeByRecord { get { return RunningMode == 1; } }
        public bool ModeByCamera { get { return RunningMode != 1; } }

        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = true;

        //
        public bool Is4K = false;
        public bool Is8K = true;
        public string GetPrex() { return Is4K ? "4K" : (Is8K ? "8K" : "Unknow"); }

        public string CameraFileInner = "D:/#DAT/[1B][20170806][102417-104740][245][F1-F245].zip";
        public string CameraFileOuter = "D:/#DAT/[1A][20170806][102417-104740][245][F1-F245].zip";
        public string CameraFileDb = "";

        public int Camera4KInnerServer2 = 1;
        public int Camera4KInnerResource2 = 0;
        public string Camera4KInnerCCDFile = "";
        public string Camera4KInnerName = "2B";
        public string Camera4KInnerCaption = "内侧";
        public double Camera4KInnerScaleX = 0.0587852;
        public double Camera4KInnerScaleY = 0.0584285;

        public int Camera4KOuterServer2 = 2;
        public int Camera4KOuterResource2 = 0;
        public string Camera4KOuterCCDFile = "";
        public string Camera4KOuterName = "2A";
        public string Camera4KOuterCaption = "外侧";
        public double Camera4KOuterScaleX = 0.0584285;
        public double Camera4KOuterScaleY = 0.0584285;

        public int Camera8KInnerServer1 = 1;
        public int Camera8KInnerResource1 = 0;
        public int Camera8KInnerServer2 = 3;
        public int Camera8KInnerResource2 = 0;
        public string Camera8KInnerBoardFile = "../config/card.ccf";
        public string Camera8KInnerCCDFile = "";
        public string Camera8KInnerName = "1B";
        public string Camera8KInnerCaption = "正面";
        public double Camera8KInnerScaleX = 0.052071;
        public double Camera8KInnerScaleY = 0.0514019;

        public int Camera8KOuterServer1 = 2;
        public int Camera8KOuterResource1 = 0;
        public int Camera8KOuterServer2 = 4;
        public int Camera8KOuterResource2 = 0;
        public string Camera8KOuterBoardFile = "../config/card.ccf";
        public string Camera8KOuterCCDFile = "";
        public string Camera8KOuterName = "1A";
        public string Camera8KOuterCaption = "反面";
        public double Camera8KOuterScaleX = 0.0514019;
        public double Camera8KOuterScaleY = 0.0514019;

        //
        public string OperatorUsername = "operator";
        public string OperatorPassword = "";
        public string OperatorViewstyle = "DevExpress Style";

        public string EngineerUsername = "engineer";
        public string EngineerPassword = "catl";
        public string EngineerViewstyle = "McSkin";

        public string ExpertUsername = "expert";
        public string ExpertPassword = "via";
        public string ExpertViewstyle = "Money Twins";

        public int SelectUserId = 0;

        public string[] GetUsernameList() { return new string[] { OperatorUsername, EngineerUsername, ExpertUsername }; }
        public string[] GetPasswordList() { return new string[] { OperatorPassword, EngineerPassword, ExpertPassword }; }
        public string[] GetViewstyleList() { return new string[] { OperatorViewstyle, EngineerViewstyle, ExpertViewstyle }; }
        public string GetUserName() { return GetUsernameList()[SelectUserId]; }
        
    }

}
