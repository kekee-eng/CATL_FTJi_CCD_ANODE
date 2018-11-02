﻿
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
        public int DefectCountFront_Pifeng;
        public int DefectCountBack_Join;
        public int DefectCountBack_Tag;
        public int DefectCountBack_LeakMetal;
        public int DefectCountBack_Pifeng;

        public int DefectCountFront_LineLeakMetal;
        public int DefectCountBack_LineLeakMetal;


        public bool IsTabCountFail { get { return TabCount != Static.Recipe.CheckTabCount; } }
        public bool IsTabWidthFailCountFail {
            get {
                if (Static.Tiebiao.EAContextWidth) {
                    if (TabWidthFailCount > Static.Recipe.CheckTabWidthCount) {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsDefectCountFail {
            get {
                if (Static.Tiebiao.EAContextJoin) {
                    if (DefectCountFront_Join > 0) return true;
                    if (DefectCountBack_Join > 0) return true;
                }
                if (Static.Tiebiao.EAContextTag) {
                    if (DefectCountFront_Tag > 0) return true;
                    if (DefectCountBack_Tag > 0) return true;
                }
                if (Static.Tiebiao.EAContextLeakMetal) {
                    if (DefectCountFront_LeakMetal > 0)
                        return true;
                    if (DefectCountBack_LeakMetal > 0)
                        return true;
                }
                if (Static.Tiebiao.EAContextPifeng) {
                    if (DefectCountFront_Pifeng> 0)
                        return true;
                    if (DefectCountBack_Pifeng > 0)
                        return true;
                }
                if (Static.App.LineLeakMetalIsLabel)
                {
                    if (DefectCountBack_LineLeakMetal > 0) return true;
                    if (DefectCountFront_LineLeakMetal > 0) return true;
                }
                return false;
            }
        }

        public bool IsFail { get { return IsDefectCountFail ||  IsTabWidthFailCountFail; } }
        public string GetFailReason() {
            string text = "";

            if ( IsTabWidthFailCountFail)
                text += string.Format("宽度NG[{0}], ", TabWidthFailCount);

            if (IsDefectCountFail) {

                if (Static.Tiebiao.EAContextJoin) {
                    if (DefectCountFront_Join > 0)
                        text += string.Format("正面接头[{0}], ", DefectCountFront_Join);
                    if (DefectCountBack_Join > 0)
                        text += string.Format("背面接头[{0}], ", DefectCountBack_Join);
                }
                if (Static.Tiebiao.EAContextTag) {
                    if (DefectCountFront_Tag > 0)
                        text += string.Format("正面标签[{0}], ", DefectCountFront_Tag);

                    if (DefectCountBack_Tag > 0)
                        text += string.Format("背面标签[{0}], ", DefectCountBack_Tag);

                }
                if (Static.Tiebiao.EAContextLeakMetal) {
                    if (DefectCountFront_LeakMetal > 0)
                        text += string.Format("正面漏金属[{0}], ", DefectCountFront_LeakMetal);

                    if (DefectCountBack_LeakMetal > 0)
                        text += string.Format("背面漏金属[{0}], ", DefectCountBack_LeakMetal);
                }
                if (Static.Tiebiao.EAContextPifeng)
                {
                    if (DefectCountFront_Pifeng > 0)
                        text += string.Format("正面披风[{0}], ", DefectCountFront_Pifeng);

                    if (DefectCountBack_Pifeng > 0)
                        text += string.Format("背面披风[{0}], ", DefectCountBack_Pifeng);
                }
                if (Static.App.LineLeakMetalIsLabel)
                {
                    if (DefectCountFront_LineLeakMetal > 0)
                        text += string.Format("正面线性漏金属[{0}], ", DefectCountFront_LineLeakMetal);

                    if (DefectCountBack_LineLeakMetal > 0)
                        text += string.Format("背面线性漏金属[{0}], ", DefectCountBack_LineLeakMetal);
                }
            }
            return text;
        }
    }
}
