using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    class CfgApp : Common.TemplateConfig {
        public CfgApp(string path) : base(path) { }

        //运行模式：相机仿真、记录回溯
        public bool ModeByCamera = true;
        public bool ModeByRecord = false;

        //相机源：真实相机、Zip文件、文件夹、数据库
        public bool CameraByRealtime = false;
        public bool CameraByZipFile = false;
        public bool CameraByFolder = false;
        public bool CameraByDB = false;

        //


    }

}
