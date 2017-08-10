
using System.Collections.Generic;

namespace DetectCCD {
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
        public int DefectCountLocal;
        public int DefectCountFront;
        public int DefectCountBack;

        public bool IsTabCountFail;
        public bool IsTabWidthFailCountFail;
        public bool IsTabDistFailCountFail;
        public bool IsTabHeightFailCountFail;
        public bool IsDefectCountFail;

        public bool IsFail { get { return IsDefectCountFail || IsTabCountFail || IsTabWidthFailCountFail; } }

    }
}
