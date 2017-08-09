using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class CfgApp : TemplateConfig {
        public CfgApp(string path) : base(path) { }

        //运行模式：0=在线（实时检测）、1=离线（仿真）
        public int run_mode = 0;

        public bool ModeByRecord { get { return run_mode == 1; } }
        public bool ModeByCamera { get { return run_mode != 1; } }

        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = true;
        public bool CameraByFolder = false;
        public bool CameraByDB = false;

        //
        public bool Is4K = false;
        public bool Is8K = true;
        public string GetPrex() { return Is4K ? "4K" : (Is8K ? "8K" : "Uknow"); } 
        
        public string CameraFileInner = "D:/#DAT/[1B][20170806][102417-104740][245][F1-F245].zip";
        public string CameraFileOuter = "D:/#DAT/[1A][20170806][102417-104740][245][F1-F245].zip";

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

        //记录参数
        public int RecordCacheSize = 200;
        public bool RecordSaveImageEnable = false;
        public bool RecordSaveImageAll = false;
        public bool RecordSaveImageTab = false;
        public bool RecordSaveImageMark = false;
        public bool RecordSaveImageDefect = false;

        //显示参数
        public double CameraFpsControl = 10;
        public int CameraFrameStart = 1;

        //
        public bool ImageProcessReload = false;

        //
        public string operator_username = "operator";
        public string operator_password = "";
        public string operator_viewstyle = "DevExpress Style";

        public string engineer_username = "engineer";
        public string engineer_password = "catl";
        public string engineer_viewstyle = "McSkin";

        public string expert_username = "expert";
        public string expert_password = "via";
        public string expert_viewstyle = "Money Twins";

        public int select_userid = 0;

        public string[] GetUsernameList() { return new string[] { operator_username, engineer_username, expert_username }; }
        public string[] GetPasswordList() { return new string[] { operator_password, engineer_password, expert_password }; }
        public string[] GetViewstyleList() { return new string[] { operator_viewstyle, engineer_viewstyle, expert_viewstyle }; }

        public string GetUserName() { return GetUsernameList()[select_userid]; }

    }

}
