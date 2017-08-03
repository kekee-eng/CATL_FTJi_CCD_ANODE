using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    class CfgParam : Common.TemplateConfig {
        public CfgParam(string path) : base(path) { }
        
        //极耳距离小于此值的，合并成一个极耳
        public double ERMergeDistance = 50;

        //宽度检测位置
        public double ERWidthStart = -165;
        public double ERWidthEnd = -135;

        //EA起始判定位置
        public double EAStart = -40;
        public double EAEnd = -10;
       
        //上下限及步距
        public double ERWidthMin = 75;
        public double ERWidthMax = 80;
        public double ERWidthStep = 0.5;

        public double ERSizeMin = 30;
        public double ERSizeMax = 40;
        public double ERSizeStep = 1;

        public double ERGapMin = 210;
        public double ERGapMax = 350;
        public double ERGapStep = 10;

        public double ERGapDiffMin = -100;
        public double ERGapDiffMax = 100;
        public double ERGapDiffStep = 10;

        //计数
        public int ERCount = 33;
        public int ERWidthCount = 10;
        public int ERGapCount = 1;
        public int ERSizeCount = 1;
        
    }
}
