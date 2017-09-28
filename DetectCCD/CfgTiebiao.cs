using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    [Serializable]
    public class CfgTiebiao : TemplateConfig {
        public CfgTiebiao(string path) : base(path) { }

        //
        public bool EnableLabelEA = false;
        public bool EnableLabelEA_Force = false;
        public bool EnableLabelEA_EveryOne = false;
        public bool EnableLabelDefect = false;

        public bool LabelContextJoin = false;
        public bool LabelContextTag = false;
        public bool LabelContextLeakMetal = false;

        public bool EAContextJoin = false;
        public bool EAContextTag = false;
        public bool EAContextLeakMetal = false;
        public bool EAContextWidth = false;

        //
        public double LabelY_Defect = -400;
        public double LabelY_EA = 0;
        public double LabelShowX = 0.95;
        public double LabelShowW = 0.08;
        public double LabelShowH = 0.3;

    }
}
