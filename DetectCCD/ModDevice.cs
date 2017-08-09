
using System;

namespace DetectCCD {

    class ModDevice :IDisposable {

        public void Open() {
            if (isOpen)
                return;

            //
            Dispose();

            //
            if (Static.ParamApp.CameraByRealtime4K) {

                //使用实时相机取图
                InnerCamera = new CameraRealtime(
                    Static.ParamApp.Camera4KInnerServer2,
                    Static.ParamApp.Camera4KInnerResource2,
                    Static.ParamApp.Camera4KInnerCCDFile);
                OuterCamera = new CameraRealtime(
                    Static.ParamApp.Camera4KOuterServer2,
                    Static.ParamApp.Camera4KOuterResource2,
                    Static.ParamApp.Camera4KOuterCCDFile);

                //判断反向
                if ((InnerCamera.Name == Static.ParamApp.Camera4KOuterName) &&
                    (OuterCamera.Name == Static.ParamApp.Camera4KInnerName)) {

                    if (InnerCamera.Name == OuterCamera.Name)
                        throw new Exception("两只相机请勿使用相同的名称");

                    //交换参数
                    var tmpServer2 = Static.ParamApp.Camera4KOuterServer2;
                    Static.ParamApp.Camera4KOuterServer2 = Static.ParamApp.Camera4KInnerServer2;
                    Static.ParamApp.Camera4KInnerServer2 = tmpServer2;

                    //重新检测
                    Open();
                }

                //确认
                if ((InnerCamera.Name != Static.ParamApp.Camera4KInnerName)
                    || (OuterCamera.Name != Static.ParamApp.Camera4KOuterName))
                    throw new Exception("相机名称与预设名称不一致");

                //载入标题
                InnerCamera.Caption = Static.ParamApp.Camera4KInnerCaption;
                OuterCamera.Caption = Static.ParamApp.Camera4KOuterCaption;

            }
            if (Static.ParamApp.CameraByRealtime8K) {

                //使用实时相机取图
                InnerCamera = new CameraRealtime(
                    Static.ParamApp.Camera8KInnerServer1,
                    Static.ParamApp.Camera8KInnerResource1,
                    Static.ParamApp.Camera8KInnerServer2,
                    Static.ParamApp.Camera8KInnerResource2,
                    Static.ParamApp.Camera8KInnerBoardFile,
                    Static.ParamApp.Camera8KInnerCCDFile);
                OuterCamera = new CameraRealtime(
                    Static.ParamApp.Camera8KOuterServer1,
                    Static.ParamApp.Camera8KOuterResource1,
                    Static.ParamApp.Camera8KOuterServer2,
                    Static.ParamApp.Camera8KOuterResource2,
                    Static.ParamApp.Camera8KOuterBoardFile,
                    Static.ParamApp.Camera8KOuterCCDFile);

                //判断反向
                if ((InnerCamera.Name == Static.ParamApp.Camera8KOuterName) &&
                    (OuterCamera.Name == Static.ParamApp.Camera8KInnerName)) {

                    if (InnerCamera.Name == OuterCamera.Name)
                        throw new Exception("两只相机请勿使用相同的名称");

                    //交换参数
                    var tmpServer1 = Static.ParamApp.Camera8KOuterServer1;
                    var tmpServer2 = Static.ParamApp.Camera8KOuterServer2;
                    Static.ParamApp.Camera8KOuterServer1 = Static.ParamApp.Camera8KInnerServer1;
                    Static.ParamApp.Camera8KOuterServer2 = Static.ParamApp.Camera8KInnerServer2;
                    Static.ParamApp.Camera8KInnerServer1 = tmpServer1;
                    Static.ParamApp.Camera8KInnerServer2 = tmpServer2;

                    //重新检测
                    Open();
                }

                //确认
                if ((InnerCamera.Name != Static.ParamApp.Camera8KInnerName)
                    || (OuterCamera.Name != Static.ParamApp.Camera8KOuterName))
                    throw new Exception("相机名称与预设名称不一致");

                //载入标题
                InnerCamera.Caption = Static.ParamApp.Camera4KInnerCaption;
                OuterCamera.Caption = Static.ParamApp.Camera4KOuterCaption;
                
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

        public bool isOpen { get { return InnerCamera!=null && OuterCamera!=null && InnerCamera.isOpen && OuterCamera.isOpen; } }
        public bool isGrabbing { get { return isOpen && InnerCamera.isGrabbing && OuterCamera.isGrabbing; } }

        public TemplateCamera InnerCamera;
        public TemplateCamera OuterCamera;

        public Action<DataGrab> EventInnerCamera;
        public Action<DataGrab> EventOuterCamera;
    }
}
