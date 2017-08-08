using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [System.Serializable]
    public class DataDefect {

        public int Type;

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
            double min = list.Min();
            double max = list.Max();

            return max >= start && min <= end;
        }

    }


}
