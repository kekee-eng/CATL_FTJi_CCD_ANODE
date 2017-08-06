
namespace Common {
#pragma warning disable 0649

    class DataTab {

        //极耳位置
        public double TabX;
        public double TabY1;
        public double TabY2;

        //测宽位置
        public double WidthX1;
        public double WidthX2;
        public double WidthY1;
        public double WidthY2;

        //后期数据
        public double ValWidth;
        public double ValGap;
        public double ValGapDiff;
        public double ValSize;

        public bool IsWidthFail;
        public bool IsGapFail;
        public bool IsGapDiffFail;
        public bool IsSizeFail;

        public double EAX; //EA起始处
        public double EAY; //EA起始处

        public int ID;
        public int EA;
        public int TAB;

        //
        public bool IsNewEA = false; //
        public bool IsSync = false; //已同步
        public bool IsFix = false; //补测
        
        //
        public bool IsFail { get { return IsWidthFail || IsGapFail || IsGapDiffFail || IsSizeFail; } }

        //数据库接口

    }
}
