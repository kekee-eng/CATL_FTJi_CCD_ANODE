
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DetectCCD.DataGrab;

namespace DetectCCD {
    class ModRecord : TemplateDB, IDisposable {

        public void Dispose() {
            
            InnerGrab.Cache.Dispose();
            InnerViewerImage.Dispose();
        }

        public void Init() {

            //
            InnerGrab = new EntryGrab(this, "InnerGrab", Static.ParamApp.RecordCacheSize);
            InnerDetect = new EntryDetect(this, "InnerDetect", Static.ParamShare, Static.ParamInner, InnerGrab);
            InnerViewerImage = new ViewerImage(InnerGrab, InnerDetect);
            InnerViewerChart = new ViewerChart(InnerGrab, InnerDetect, InnerViewerImage);

        }

        public EntryGrab InnerGrab;
        public EntryDetect InnerDetect;

        public ViewerImage InnerViewerImage;
        public ViewerChart InnerViewerChart;

        //public EntryGrab OuterGrab;
        //public EntryDetect OuterDetect;




    }
}
