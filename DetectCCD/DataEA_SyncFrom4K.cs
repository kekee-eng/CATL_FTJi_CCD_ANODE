using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    public class DataEA_SyncFrom4K {
        public double Start;
        public double End;
        public double EA;

        public bool InRange(double start, double end) {
            return (Start >= start && Start <= end) || (End >= start && End <= end);
        }

    }
}
