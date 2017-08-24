
using System;

namespace DetectCCD {

    public class ModDevice :IDisposable {

        public void Open() {
            if (isOpen)
                return;

            //
            if (Static.App.ModeByCamera) {
                if (Static.App.CameraByRealtime) {
                    //使用实时相机取图

                    if (Static.App.Is4K) {
                        InnerCamera = new CameraRealtime(
                            Static.App.Camera4KInnerServer2,
                            Static.App.Camera4KInnerResource2,
                            Static.App.Camera4KInnerCCDFile);
                        OuterCamera = new CameraRealtime(
                            Static.App.Camera4KOuterServer2,
                            Static.App.Camera4KOuterResource2,
                            Static.App.Camera4KOuterCCDFile);

                        //判断反向
                        if ((InnerCamera.Name == Static.App.Camera4KOuterName) &&
                            (OuterCamera.Name == Static.App.Camera4KInnerName)) {

                            if (InnerCamera.Name == OuterCamera.Name)
                                throw new Exception("两只相机请勿使用相同的名称");

                            //交换参数
                            var tmpServer2 = Static.App.Camera4KOuterServer2;
                            Static.App.Camera4KOuterServer2 = Static.App.Camera4KInnerServer2;
                            Static.App.Camera4KInnerServer2 = tmpServer2;

                            //重新检测
                            Open();
                            return;
                        }

                        //确认
                        if ((InnerCamera.Name != Static.App.Camera4KInnerName)
                            || (OuterCamera.Name != Static.App.Camera4KOuterName))
                            throw new Exception("相机名称与预设名称不一致");

                    }
                    else if (Static.App.Is8K) {

                        //使用实时相机取图
                        InnerCamera = new CameraRealtime(
                            Static.App.Camera8KInnerServer1,
                            Static.App.Camera8KInnerResource1,
                            Static.App.Camera8KInnerServer2,
                            Static.App.Camera8KInnerResource2,
                            Static.App.Camera8KInnerBoardFile,
                            Static.App.Camera8KInnerCCDFile);
                        OuterCamera = new CameraRealtime(
                            Static.App.Camera8KOuterServer1,
                            Static.App.Camera8KOuterResource1,
                            Static.App.Camera8KOuterServer2,
                            Static.App.Camera8KOuterResource2,
                            Static.App.Camera8KOuterBoardFile,
                            Static.App.Camera8KOuterCCDFile);

                        //判断反向
                        if ((InnerCamera.Name == Static.App.Camera8KOuterName) &&
                            (OuterCamera.Name == Static.App.Camera8KInnerName)) {

                            if (InnerCamera.Name == OuterCamera.Name)
                                throw new Exception("两只相机请勿使用相同的名称");

                            //交换参数
                            var tmpServer1 = Static.App.Camera8KOuterServer1;
                            var tmpServer2 = Static.App.Camera8KOuterServer2;
                            Static.App.Camera8KOuterServer1 = Static.App.Camera8KInnerServer1;
                            Static.App.Camera8KOuterServer2 = Static.App.Camera8KInnerServer2;
                            Static.App.Camera8KInnerServer1 = tmpServer1;
                            Static.App.Camera8KInnerServer2 = tmpServer2;

                            //重新检测
                            Open();
                            return;
                        }

                        //确认
                        if ((InnerCamera.Name != Static.App.Camera8KInnerName)
                            || (OuterCamera.Name != Static.App.Camera8KOuterName))
                            throw new Exception("相机名称与预设名称不一致");

                    }
                    else
                        throw new Exception("请在配置文件中选择：4K/8K");

                }
                else if (Static.App.CameraByZipFile) {
                    //从Zip文件取图 
                    InnerCamera = new CameraZipFile(Static.App.CameraFileInner);
                    OuterCamera = new CameraZipFile(Static.App.CameraFileOuter);

                }
            }
            else  {
                //从数据库中取图
                InnerCamera = new CameraDB(GetInnerDb());
                OuterCamera = new CameraDB(GetOuterDb());
            }

            //设置信息
            if(Static.App.Is4K) {

                InnerCamera.Name = Static.App.Camera4KInnerName;
                OuterCamera.Name = Static.App.Camera4KOuterName;

                InnerCamera.Caption = Static.App.Camera4KInnerCaption;
                OuterCamera.Caption = Static.App.Camera4KOuterCaption;

                InnerCamera.ScaleX = Static.App.Camera4KInnerScaleX;
                InnerCamera.ScaleY = Static.App.Camera4KInnerScaleY;
                OuterCamera.ScaleX = Static.App.Camera4KOuterScaleX;
                OuterCamera.ScaleY = Static.App.Camera4KOuterScaleY;

            }
            else if(Static.App.Is8K) {
                
                InnerCamera.Name = Static.App.Camera8KInnerName;
                OuterCamera.Name = Static.App.Camera8KOuterName;

                InnerCamera.Caption = Static.App.Camera8KInnerCaption;
                OuterCamera.Caption = Static.App.Camera8KOuterCaption;

                InnerCamera.ScaleX = Static.App.Camera8KInnerScaleX;
                InnerCamera.ScaleY = Static.App.Camera8KInnerScaleY;
                OuterCamera.ScaleX = Static.App.Camera8KOuterScaleX;
                OuterCamera.ScaleY = Static.App.Camera8KOuterScaleY;

            }

            //
            InnerCamera.OnImageReady += EventInnerCamera;
            OuterCamera.OnImageReady += EventOuterCamera;


            //
            if (Static.App.Is4K) {
                InnerCamera.GetEncoder += () => RemotePLC.In4KCallPLC_GetEncoder(true);
                OuterCamera.GetEncoder += () => RemotePLC.In4KCallPLC_GetEncoder(false);

                InnerCamera.GetEncoderCheck += () => RemotePLC.In4KCallPLC_GetEncoder(false);
                OuterCamera.GetEncoderCheck += () => RemotePLC.In4KCallPLC_GetEncoder(false);
            }

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

        public event Func<DataGrab.GrabDB> GetInnerDb;
        public event Func<DataGrab.GrabDB> GetOuterDb;


    }
}
