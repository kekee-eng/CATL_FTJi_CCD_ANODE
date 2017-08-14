using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [Serializable]
    public class CfgParam : TemplateConfig {
        public CfgParam(string path) : base(path) { }

        //EA起始Mark孔判定位置
        public double EAStart = -40;
        public double EAEnd = -10;

        //宽度检测位置
        public double TabWidthStart = -165;
        public double TabWidthEnd = -135;

        //极耳距离小于此值的，合并成一个极耳
        public double TabMergeDistance = 60;

        //上下限及步距
        public double TabWidthMin = 75;
        public double TabWidthMax = 80;
        public double TabWidthStep = 0.5;
        public double TabWidthTarget { get { return (TabWidthMin + TabWidthMax) / 2; } }

        public double TabHeightMin = 30;
        public double TabHeightMax = 40;
        public double TabHeightStep = 1;

        public double TabDistMin = 210;
        public double TabDistMax = 350;
        public double TabDistStep = 10;

        public double TabDistDiffMin = -100;
        public double TabDistDiffMax = 100;
        public double TabDistDiffStep = 10;

        //计数
        public int CheckTabCount = 33;
        public int CheckTabWidthCount = 10;
        public int CheckTabHeightCount = 0;
        public int CheckTabDistCount = 0;
        public int CheckDefectCount = 0;

        //
        public double LabelY_Defect = 0;
        public double LabelY_EA = 0;
        public double LabelShowX = 0.5;
        public double LabelShowW = 0.05;
        public double LabelShowH = 0.05;

    }
}
