
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class ModDevice {
        
        public void Open() {

            //
            Close();

            //
            if (Static.ParamApp.CameraByRealtime) {
                //使用实时相机取图
            }
            else if (Static.ParamApp.CameraByZipFile) {
                //从Zip文件取图 
                InnerCamera = new ConnectCamera_ZipFile(Static.ParamApp.CameraFileInner);
                OuterCamera = new ConnectCamera_ZipFile(Static.ParamApp.CameraFileOuter);
            }
            else if (Static.ParamApp.CameraByFolder) {
                //从文件夹取图
            }
            else if (Static.ParamApp.CameraByDB) {
                //从数据库中取图
            }

        }
        public void Close() {

            InnerCamera?.Dispose();
            OuterCamera?.Dispose();
        }

        public bool isRun { get { return InnerCamera.isRun && OuterCamera.isRun; } }

        public ConnectCamera_ZipFile InnerCamera;
        public ConnectCamera_ZipFile OuterCamera;


    }
}
