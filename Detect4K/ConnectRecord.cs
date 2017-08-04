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
            GrabInner = new DBTableGrab(this, "GrabInner");
            GrabOuter = new DBTableGrab(this, "GrabOuter");

            //
            GrabCacheInner = new CacheGrab();
            GrabCacheOuter = new CacheGrab();

        }

        public DBTableGrab GrabInner;
        public DBTableGrab GrabOuter;

        public CacheGrab GrabCacheInner;
        public CacheGrab GrabCacheOuter;

    }
}
