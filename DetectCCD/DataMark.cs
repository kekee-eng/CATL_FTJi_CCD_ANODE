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

        public void CopyFrom(DataMark other)
        {
            //
            MarkImageStart= other.MarkImageStart;
            MarkImageEnd = other.MarkImageEnd;
            MarkX = other.MarkX;
            MarkY = other.MarkY;
            MarkX_P = other.MarkX_P;
            MarkY_P = other.MarkY_P;
            HasMark = other.HasMark;
            HasTwoMark = other.HasTwoMark;
        }
    }
}
