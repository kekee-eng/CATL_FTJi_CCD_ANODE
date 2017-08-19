
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
        public int DefectCountFront_Join;
        public int DefectCountFront_Tag;
        public int DefectCountFront_LeakMetal;
        public int DefectCountBack_Join;
        public int DefectCountBack_Tag;
        public int DefectCountBack_LeakMetal;

        public bool IsTabCountFail { get { return TabCount != Static.Param.CheckTabCount; } }
        public bool IsTabWidthFailCountFail {
            get {
                if (Static.App.EAContextWidth) {
                    if (TabWidthFailCount > Static.Param.CheckTabWidthCount) {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsDefectCountFail {
            get {
                if (Static.App.EAContextJoin) {
                    if (DefectCountFront_Join > 0) return true;
                    if (DefectCountBack_Join > 0) return true;
                }
                if (Static.App.EAContextTag) {
                    if (DefectCountFront_Tag > 0) return true;
                    if (DefectCountBack_Tag > 0) return true;
                }
                if (Static.App.EAContextLeakMetal) {
                    if (DefectCountFront_LeakMetal > 0) return true;
                    if (DefectCountBack_LeakMetal > 0) return true;
                }
                return false;
            }
        }

        public bool IsFail { get { return IsDefectCountFail ||  IsTabWidthFailCountFail; } }
        public string GetFailReason() {
            string text = "";
            //if (IsTabCountFail)
                //text += string.Format("极耳计数[{0}], ", TabCount);

            if ( IsTabWidthFailCountFail)
                text += string.Format("宽度NG[{0}], ", TabWidthFailCount);

            if (IsDefectCountFail) {

                if (Static.App.EAContextJoin) {
                    if (DefectCountFront_Join > 0)
                        text += string.Format("正面接头[{0}], ", DefectCountFront_Join);
                    if (DefectCountBack_Join > 0)
                        text += string.Format("背面接头[{0}], ", DefectCountBack_Join);
                }
                if (Static.App.EAContextTag) {
                    if (DefectCountFront_Tag > 0)
                        text += string.Format("正面标签[{0}], ", DefectCountFront_Tag);

                    if (DefectCountBack_Tag > 0)
                        text += string.Format("背面标签[{0}], ", DefectCountBack_Tag);

                }
                if (Static.App.EAContextLeakMetal) {
                    if (DefectCountFront_LeakMetal > 0)
                        text += string.Format("正面漏金属[{0}], ", DefectCountFront_LeakMetal);

                    if (DefectCountBack_LeakMetal > 0)
                        text += string.Format("背面漏金属[{0}], ", DefectCountBack_LeakMetal);

                }
            }
            return text;
        }
    }
}
