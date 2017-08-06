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
            InnerGrab = new EntryGrab(this, "InnerGrab", Config.App.RecordCacheSize);
            //OuterGrab = new EntryGrab(this, "OuterGrab", Config.App.RecordCacheSize);

            //
            InnerDetect = new EntryDetect(this, "InnerDetect", Config.ParamShare, Config.ParamInner, InnerGrab);
            //OuterDetect = new EntryDetect(this, "OuterDetect", Config.ParamShare, Config.ParamOuter, OuterGrab);

        }

        public EntryGrab InnerGrab;
        public EntryDetect InnerDetect;

        //public EntryGrab OuterGrab;
        //public EntryDetect OuterDetect;




    }
}
