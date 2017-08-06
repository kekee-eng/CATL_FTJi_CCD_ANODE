using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    [Serializable]
    class CfgParamSelf : Common.TemplateConfig {
        public CfgParamSelf(string path) : base(path) { }
        
        public double ScaleX = 0.06;
        public double ScaleY = 0.06;
        
    }
}
