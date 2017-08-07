using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {

    [System.Serializable]
    public class DataLabel {

        public double X;
        public double Y;

        //
        public bool InRange(double start, double end) {
            var list = new double[] { Y - 0.5, Y + 0.5 }.TakeWhile(x => x > 0);
            double min = list.Min();
            double max = list.Max();

            return max >= start && min <= end;
        }


    }

}

