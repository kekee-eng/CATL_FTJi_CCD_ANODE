using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.DataGrab;

namespace Detect4K {
    class ConnectRecord : TemplateDB {

        public void Init() {
            
            //
            InnerGrabCache = new CacheGrab();
            OuterGrabCache = new CacheGrab();

            //
            InnerGrabDB = new DBTableGrab(this, "InnerGrab");
            OuterGrabDB = new DBTableGrab(this, "OuterGrab");

        }

        public volatile CacheGrab InnerGrabCache;
        public volatile CacheGrab OuterGrabCache;

        public volatile DBTableGrab InnerGrabDB;
        public volatile DBTableGrab OuterGrabDB;


    }
}
