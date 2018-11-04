using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    [Serializable]
    public class CfgApp : TemplateConfig {
        public CfgApp(string path) : base(path) { }

        //
        public bool ImageProcessReload = false;

        //
        public double CheckDefectSimulate_XMax = 10;
        public double CheckDefectSimulate_WHMax = 1.2;
        public double CheckDefectSimulate_WHMin = 0.8;
        public double CheckDefectSimulate_AreaMax = 1.2;
        public double CheckDefectSimulate_AreaMin = 0.8;

        //
        public int LineLeakMetal_AlarmStop_MaxCount = 3;
        public int LineLeakMetal_SaveImage_MaxCount = 3;
        public bool LineLeakMetalEnable = true;
        public bool LineLeakMetalIsAlarmStop = true;
        public bool LineLeakMetalIsLabel = true;

        public bool LineLeakMetalParamEnable = true;
        public int LineLeakMetalParam_offset = 50;
        public int LineLeakMetalParam_upThreshold = 30;
        public int LineLeakMetalParam_downThreshold = 10;
        public int LineLeakMetalParam_deviation = 20;

        //检测露金属参数
        public bool ImageProcessParamEnable = false;
        public double ImageProcessParam_DetectDefect_MaxGray = 2.5;
        public double ImageProcessParam_DetectDefect_MinGray = 0.5;
        public double ImageProcessParam_DetectDefect_Deviation = 20;
        public double ImageProcessParam_DetectDefect_Area = 3500;
        public double ImageProcessParam_DetectDefect_RangeThreshold = 60;

        public double AppRestartTimeout = 12;

        public double CheckAdjustRange = 15;
        public double OnceAdjustValue = 1;

        public bool EnableSkipDetectWhenLabed = true;
        public int SkipDetectMaxNumber = 30;

        //检查硬盘剩余空间(G)
        public double CheckHardDiskLess = 100;

        //记录参数
        public int RecordCacheSize = 200;
        public bool RecordSaveImageEnable = false;
        public bool RecordSaveImageNGBig = false;
        public bool RecordSaveImageNGSmall = false;
        public int RecordSaveImageNGSmallMaxType = 5;
        public bool RecordSaveImageMark = false;
        public bool RecordSaveImageLineLeakMetal = false;

        public bool RecordSaveImageLimitEnable = true;
        public int RecordSaveImageLimitNumber = 20;
        
        //显示参数
        public double CameraFpsControl = 10;
        public int CameraFrameStart = 1;

        //
        public string RemoteHost = "localhost";
        public int RemotePort = 6500;
        public double DiffFrameInnerFront = -33.51;
        public double DiffFrameInnerOuter = 18.15;
        public double DiffFrameFrontBack = 2.225;

        public double DiffFrameInnerFrontFix = 0;

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
            var eFront = eFrontScale + (DiffFrameInnerFront+ DiffFrameInnerFrontFix);
            var eOutput = eFront + (isFront ? 0 : DiffFrameFrontBack);

            return (eOutput);
        }
        public double FrameFrontToInner(bool isFront, bool isInner, double i) {

            var eInput = i - (isFront ? 0 : DiffFrameFrontBack);
            var eInnerScale = eInput - (DiffFrameInnerFront+ DiffFrameInnerFrontFix);
            var eInner = eInnerScale / FixFrameInnerFrontScale;
            var eOutput = eInner + (isInner ? 0 : DiffFrameInnerOuter);

            return (eOutput);
        }
        
        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = true;
        public bool TestZipFileCircle = false;

        //
        public string RollName = "";
        public string MachineNum = "";
        public string EmployeeNum = "";

        //
        public bool Is4K = false;
        public bool Is8K = true;
        public string GetPrex() { return Is4K ? "4K" : (Is8K ? "8K" : "Unknow"); }

        public string CameraFileInner = "D:/#DAT/[1B][20170806][102417-104740][245][F1-F245].zip";
        public string CameraFileOuter = "D:/#DAT/[1A][20170806][102417-104740][245][F1-F245].zip";

        public int Camera4KCache = 2;
        public int Camera8KCache = 2;

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
        public string SelectUserName { get { return GetUsernameList()[SelectUserId]; } }

        public string[] GetUsernameList() { return new string[] { OperatorUsername, EngineerUsername, ExpertUsername }; }
        public string[] GetPasswordList() { return new string[] { OperatorPassword, EngineerPassword, ExpertPassword }; }
        public string[] GetViewstyleList() { return new string[] { OperatorViewstyle, EngineerViewstyle, ExpertViewstyle }; }
        
    }

}
