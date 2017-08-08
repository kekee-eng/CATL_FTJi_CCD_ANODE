using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class CfgParamApp : DetectCCD.TemplateConfig {
        public CfgParamApp(string path) : base(path) { }

        //运行模式：0=在线（实时检测）、1=离线（仿真）
        public int run_mode = 0;
        
        public bool ModeByRecord { get { return run_mode == 1; } }
        public bool ModeByCamera { get { return run_mode != 1; } }

        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = false;
        public bool CameraByFolder = false;
        public bool CameraByDB = false;

        public string CameraFileInner = "";
        public string CameraFileOuter = "";

        //记录参数
        public int RecordCacheSize = 200;
        public bool RecordIsSaveImageOK = true;
        public bool RecordIsSaveImageNG = true;

        //显示参数
        public double CameraFpsControl = 10;
        public int CameraStartFrame = 1;

        //
        public bool ImageProcessReload = true;
        


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
