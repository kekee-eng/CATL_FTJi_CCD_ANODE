using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [System.Serializable]
    public class DataDefect {

        public int Type;
        public int EA;

        public double X;
        public double Y;
        public double W;
        public double H;

        public double Width;
        public double Height;
        public double Area;
        
        public string NGPartPath = "";
        public string NGBigPath = "";

        //判断缺陷相似
        public bool IsSimulate(DataDefect other) {

            //
            if (Math.Abs(X - other.X) * 8192 > Static.App.CheckDefectSimulate_XMax)
                return false;

            double fw = Width == 0 ? 0 : other.Width / Width;
            double fh = Height == 0 ? 0 : other.Height / Height;
            double fa = Area == 0 ? 0 : other.Area / Area;

            if (fw < Static.App.CheckDefectSimulate_WHMin || fw > Static.App.CheckDefectSimulate_WHMax)
                return false;

            if (fh < Static.App.CheckDefectSimulate_WHMin || fh > Static.App.CheckDefectSimulate_WHMax)
                return false;

            if (fa < Static.App.CheckDefectSimulate_AreaMin || fa > Static.App.CheckDefectSimulate_AreaMax)
                return false;

            return true;
        }
        public int SimulateCount = 0;

        //处理缺陷
        public enum DefectProcess {
            ForceLeakMetal, ForceOther, None
        }
        public DefectProcess Proc = DefectProcess.None;
        public string GetProcCaption() {
            if (Proc == DefectProcess.ForceLeakMetal) return "判成漏金属";
            if (Proc == DefectProcess.ForceOther) return "判成其它";
            return "未处理";
        }

        //
        public void Edit(DataDefect other) {
            Type = other.Type;
            EA = other.EA;
            X = other.X;
            Y = other.Y;
            W = other.W;
            H = other.H;
            Width = other.Width;
            Height = other.Height;
            Area = other.Area;
        }

        public string Timestamp;
        //
        public bool InRange(double start, double end) {
            var list = new double[] { Y - H / 2, Y + H / 2 }.TakeWhile(x => x > 0);
            if (list.Count() == 0)
                return false;

            double min = list.Min();
            double max = list.Max();

            return max >= start && min <= end;
        }

        //
        public string IsInnerText() {
            //
            var isinner = InInner(true);
            var isouter = InInner(false);

            if (isinner && isouter)
                return "全部";

            if (isinner)
                return "内侧";

            if (isouter)
                return "外侧";

            return "无";
        }

        //
        public bool InInner(bool isinner) {
            if (isinner)
                return (X + W / 2 > 0.5);
            else
                return (X - W / 2 < 0.5);
        }

        //
        public string GetTypeCaption() {
            switch (Type) {
                default: return "[N]未分类";

                case 0: return "[A]接头";
                case 1: return "[B]标签";
                case 2:
                    return "[C]漏金属";
                case 3:
                    return "[D]边缘披风";

                case 4: return "[U]待定";

                case 10: return "[R]黑斑";
                case 11: return "[S]白斑";
                case 20: return "[T]脱碳";
                case 30: return "[V]划痕";

                case 40: return "[L]线性漏金属";
            }

        }

        public bool IsTransLabel() {
            if (Static.Tiebiao.LabelContextJoin && Type == 0) return true;
            if (Static.Tiebiao.LabelContextTag && Type == 1) return true;
            if (Static.Tiebiao.LabelContextLeakMetal && Type == 2)
                return true;
            if (Static.Tiebiao.LabelContextPifeng && Type == 3)
                return true;
            return false;
        }
        
    }


}
