using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    class CfgParam : Common.TemplateConfig {
        public CfgParam(string path) : base(path) { }

        //是否保存图像
        public bool IsSaveImageOK = true;
        public bool IsSaveImageNG = true;

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

        public double TabSizeMin = 30;
        public double TabSizeMax = 40;
        public double TabSizeStep = 1;

        public double TabGapMin = 210;
        public double TabGapMax = 350;
        public double TabGapStep = 10;

        public double TabGapDiffMin = -100;
        public double TabGapDiffMax = 100;
        public double TabGapDiffStep = 10;

        //计数
        public int TabCount = 33;
        public int TabWidthCount = 10;
        public int TabGapCount = 1;
        public int TabSizeCount = 1;

    }
}
