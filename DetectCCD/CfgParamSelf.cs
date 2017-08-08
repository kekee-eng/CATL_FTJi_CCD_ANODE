using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [Serializable]
    class CfgParamSelf : DetectCCD.TemplateConfig {
        public CfgParamSelf(string path) : base(path) { }

        public string Caption = "未定义";
        public double ScaleX = 0.06;
        public double ScaleY = 0.06;
        
    }
}
