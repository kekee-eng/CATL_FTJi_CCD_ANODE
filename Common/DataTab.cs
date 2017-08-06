
namespace Common {
#pragma warning disable 0649

    [System.Serializable]
    public class DataTab {

        //极耳位置
        public double TabX;
        public double TabY1;
        public double TabY2;
        public double TabY1_P; //另一对极耳
        public double TabY2_P;

        //测宽位置
        public double WidthX1;
        public double WidthX2;
        public double WidthY1;
        public double WidthY2;

        //后期数据
        public double ValWidth;
        public double ValDist;
        public double ValDistDiff;
        public double ValHeight;

        public bool IsWidthFail;
        public bool IsDistFail;
        public bool IsDistDiffFail;
        public bool IsHeightFail;

        public double EAX; //EA起始处
        public double EAY; //EA起始处
        public double EAX_P; //另一对Mark孔
        public double EAY_P;

        public int ID;
        public int EA;
        public int TAB;

        //
        public bool IsNewEA = false; //EA起始
        public bool IsSync = false; //已同步
        public bool IsFix = false; //补测
        
        //
        public bool IsFail { get { return IsWidthFail || IsDistFail || IsDistDiffFail || IsHeightFail; } }
        
    }
}
