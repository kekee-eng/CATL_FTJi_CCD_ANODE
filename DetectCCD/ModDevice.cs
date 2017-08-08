
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class ModDevice:IDisposable {
        
        public void Open() {

            //
            Dispose();

            //
            if (Static.ParamApp.CameraByRealtime) {
                //使用实时相机取图
            }
            else if (Static.ParamApp.CameraByZipFile) {
                //从Zip文件取图 
                InnerCamera = new CameraZipFile(Static.ParamApp.CameraFileInner);
                OuterCamera = new CameraZipFile(Static.ParamApp.CameraFileOuter);
            }
            else if (Static.ParamApp.CameraByFolder) {
                //从文件夹取图
            }
            else if (Static.ParamApp.CameraByDB) {
                //从数据库中取图
            }

            InnerCamera.OnImageReady += EventInnerCamera;
            OuterCamera.OnImageReady += EventOuterCamera;
        }
        public void Dispose() {

            InnerCamera?.Dispose();
            OuterCamera?.Dispose();

        }

        public bool isReady { get { return InnerCamera.isOpen && OuterCamera.isOpen; } }
        public bool isRun { get { return InnerCamera.isGrabbing && OuterCamera.isGrabbing; } }

        public TemplateCamera InnerCamera;
        public TemplateCamera OuterCamera;

        public Action<DataGrab> EventInnerCamera;
        public Action<DataGrab> EventOuterCamera;
    }
}
