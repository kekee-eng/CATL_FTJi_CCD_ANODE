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
            GrabDBInner = new DBTableGrab(this, "GrabInner");
            GrabDBOuter = new DBTableGrab(this, "GrabOuter");

            //
            GrabCacheInner = new CacheGrab();
            GrabCacheOuter = new CacheGrab();

        }

        public volatile DBTableGrab GrabDBInner;
        public volatile DBTableGrab GrabDBOuter;

        public volatile CacheGrab GrabCacheInner;
        public volatile CacheGrab GrabCacheOuter;

    }
}
