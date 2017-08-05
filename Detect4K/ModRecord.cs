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

        }

        public GrabEntry InnerGrab;
        public GrabEntry OuterGrab;
        




    }
}
