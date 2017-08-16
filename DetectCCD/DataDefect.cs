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
        public bool InInner(bool isinner) {
            if(isinner)
                return (X - W / 2 < 0.5);
            else
                return (X + W / 2 > 0.5);
        }

        //
        public string GetTypeCaption() {
            switch (Type) {
                default: return "UNKNOW";
                case 0: return "[A]接头";
                case 1: return "[B]标签";
                case 10: return "[F]黑斑";
                case 11: return "[G]白斑";
                case 20: return "[E]脱碳";
                case 30: return "[D]划痕";
                case 40: return "[C]漏金属";
            }

        }

    }


}
