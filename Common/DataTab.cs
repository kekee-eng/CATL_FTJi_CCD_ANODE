using System.Linq;

namespace Common {
#pragma warning disable 0649

    [System.Serializable]
    public class DataTab {

        //极耳位置
        public double TabX;
        public double TabY1;
        public double TabY2;
        public double TabX_P; //另一对极耳
        public double TabY1_P; 
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

        public double MarkY1;
        public double MarkY2;

        public double MarkX; //EA起始处
        public double MarkY; 
        public double MarkX_P; //另一对Mark孔
        public double MarkY_P;

        public int ID;
        public int EA;
        public int TAB;

        //
        public bool HasTwoTab = false; //是否双侧极耳
        public bool HasTwoMark = false; //是否有两个Mark孔
        
        //
        public bool IsNewEA = false; //EA起始
        public bool IsSync = false; //已同步
        public bool IsFix = false; //补测
        
        //
        public bool IsFail { get { return IsWidthFail || IsDistFail || IsDistDiffFail || IsHeightFail; } }
        
        //
        public bool InRange(double start, double end) {

            if (TAB == 0)
                return false;

            var list = new double[] { TabY1, TabY2, TabY1_P, TabY2_P, WidthY1, WidthY2, MarkY, MarkY_P }.TakeWhile(x => x > 0);
            double min = list.Min();
            double max = list.Max();

            return max >= start && min <= end;
        }
    }
}
