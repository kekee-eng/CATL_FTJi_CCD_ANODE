using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [Serializable]
    class CfgParamShare : DetectCCD.TemplateConfig {
        public CfgParamShare(string path) : base(path) { }

        //EA起始Mark孔判定位置
        public double EAStart = -40;
        public double EAEnd = -10;

        //宽度检测位置
        public double TabWidthStart = -165;
        public double TabWidthEnd = -135;

        //极耳距离小于此值的，合并成一个极耳
        public double TabMergeDistance = 50;

        //上下限及步距
        public double TabWidthMin = 75;
        public double TabWidthMax = 80;
        public double TabWidthStep = 0.5;

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
        public int TabCount = 33;
        public int TabWidthCount = 10;
        public int TabHeightCount = 0;
        public int TabDistCount = 0;

    }
}
