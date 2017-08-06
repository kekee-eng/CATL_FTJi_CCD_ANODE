using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.DataGrab;

namespace Detect4K {
    class ModRecord : TemplateDB {

        public void Init() {
            
            //
            InnerGrab = new GrabEntry(this, "InnerGrab", Config.App.RecordCacheSize);
            OuterGrab = new GrabEntry(this, "OuterGrab", Config.App.RecordCacheSize);

            //
            InnerDetect = new DetectEntry(this, "InnerDetect", Config.ParamShare, Config.ParamInner);
            OuterDetect = new DetectEntry(this, "OuterDetect", Config.ParamShare, Config.ParamOuter);

        }

        public GrabEntry InnerGrab;
        public GrabEntry OuterGrab;

        public DetectEntry InnerDetect;
        public DetectEntry OuterDetect;




    }
}
