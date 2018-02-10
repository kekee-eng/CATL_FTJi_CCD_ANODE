﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [System.Serializable]
    public class DataLabel {

        public string Timestamp;

        public int EA = -100;
        public int Encoder;
        public double X { get { return Static.Tiebiao.LabelShowX; } }
        public double Y;
        public double W { get { return Static.Tiebiao.LabelShowW; } }
        public double H { get { return Static.Tiebiao.LabelShowH;}}
        public string Comment;

        public bool IsSend = false;

        //
        public bool InRange(double start, double end) {
            var list = new double[] { Y - 0.5, Y + 0.5 }.TakeWhile(x => x > 0);
            if (list.Count() == 0)
                return false;

            double min = list.Min();
            double max = list.Max();

            return max >= start && min <= end;
        }


    }

}

