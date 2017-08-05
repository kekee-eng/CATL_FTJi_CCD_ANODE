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
            InnerGrab = new GrabEntry(this, "InnerGrab");
            OuterGrab = new GrabEntry(this, "OuterGrab");

        }

        public GrabEntry InnerGrab;
        public GrabEntry OuterGrab;
        




    }
}
