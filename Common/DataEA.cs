
using System.Collections.Generic;

namespace Common {
#pragma warning disable 0649

    [System.Serializable]
    public class DataEA {

        public double EAX; //EA起始处
        public double EAY; //EA起始处

        public int EA;
        public int TabCount;
        public int TabWidthFailCount;
        public int TabDistFailCount;
        public int TabHeightFailCount;

        public bool IsTabCountFail;
        public bool IsTabWidthFailCountFail;
        public bool IsTabDistFailCountFail;
        public bool IsTabHeightFailCountFail;

        public bool IsFail { get { return IsTabCountFail || IsTabWidthFailCountFail || IsTabDistFailCountFail || IsTabHeightFailCountFail; } }

    }
}
