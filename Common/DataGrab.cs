
namespace Common {
#pragma warning disable 0649

    public class DataGrab {

        //取相相机
        public string Camera;

        //帧数据
        public int Frame;
        public int Encoder;
        public System.DateTime Timestamp;

        //图像
        public HalconDotNet.HImage Image;
        
    }
}
