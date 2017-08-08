using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class CfgParamApp : DetectCCD.TemplateConfig {
        public CfgParamApp(string path) : base(path) { }

        //运行模式：相机仿真、记录回溯
        public bool ModeByCamera = true;
        public bool ModeByRecord = false;

        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = false;
        public bool CameraByFolder = false;
        public bool CameraByDB = false;

        //记录参数
        public int RecordCacheSize = 200;
        public bool RecordIsSaveImageOK = true;
        public bool RecordIsSaveImageNG = true;

        //显示参数
        public string CameraZipFile = "";
        public double CameraFpsControl = 10;
        public int CameraStartFrame = 1;

        //
        public bool ProcessReload = true;
    }

}
