
using System.Collections.Generic;

namespace Common {
#pragma warning disable 0649

    [System.Serializable]
    public class DataEA {

        public double EAX; //EA起始处
        public double EAY; //EA起始处

        public int EA;
        public int ERCount;
        public int ERWidthFailCount;
        public int ERGapFailCount;
        public int ERSizeFailCount;

        public bool IsERCountFail;
        public bool IsERWidthFailCountFail;
        public bool IsERGapFailCountFail;
        public bool IsERSizeFailCountFail;

        public bool IsFail { get { return IsERCountFail || IsERWidthFailCountFail || IsERGapFailCountFail || IsERSizeFailCountFail; } }

    }
}
