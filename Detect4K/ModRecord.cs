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
            InnerGrab = new EntryGrab(this, "InnerGrab");
            OuterGrab = new EntryGrab(this, "OuterGrab");

        }

        public EntryGrab InnerGrab;
        public EntryGrab OuterGrab;
        

    }
}
