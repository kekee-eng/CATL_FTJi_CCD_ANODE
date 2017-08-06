using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    class ModDevice {

        public void Init() {

            //
            if(Config.App.ModeByRecord) {
                //记录回溯模式
            }
            else {
                //仿真模式
                if(Config.App.CameraByRealtime) {
                    //使用实时相机取图
                }
                else if(Config.App.CameraByZipFile) {
                    //从Zip文件取图
                }
                else if(Config.App.CameraByFolder) {
                    //从文件夹取图
                }
                else if(Config.App.CameraByDB) {
                    //从数据库中取图
                }
            }

        }
        public void Open() {

        }
        public void Close() {

            InnerCamera.Dispose();
            //OuterCamera.Dispose();
        }

        public ConnectCamera_ZipFile InnerCamera;
        //public ConnectCamera_ZipFile OuterCamera;



    }
}
