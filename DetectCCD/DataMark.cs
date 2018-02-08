using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD
{
   public class DataMark
    {
        public double MarkImageStart;
        public double MarkImageEnd;
        
        public bool HasMark = false;
        public bool HasTwoMark =false;

        public double MarkX;
        public double MarkY;

        public double MarkX_P;
        public double MarkY_P;

        public void CopyTo(DataMark other)
        {
            //
            other.MarkImageStart = MarkImageStart;
            other.MarkImageEnd = MarkImageEnd;
            other.MarkX = MarkX;
            other.MarkY = MarkY;
            other.MarkX_P = MarkX_P;
            other.MarkY_P = MarkY_P;
            other.HasMark = HasMark;
            other.HasTwoMark = HasTwoMark;
        }
    }
}
