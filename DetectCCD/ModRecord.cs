
using System;

namespace DetectCCD {
    public class ModRecord :IDisposable{
        
        public void Dispose() {
            InnerGrab.Cache.Dispose();
            OuterGrab.Cache.Dispose();
        }

        public void Uninit() {

            InnerGrab.Cache.Dispose();
            InnerViewerImage.Dispose();

            OuterGrab.Cache.Dispose();
            OuterViewerImage.Dispose();

        }

        public void Init() {

            //
            InnerGrab = new EntryGrab(Static.App.RecordCacheSize);
            InnerDetect = new EntryDetect(InnerGrab, true);
            InnerViewerImage = new ViewerImage(InnerGrab, InnerDetect);
            InnerViewerChart = new ViewerChart(InnerGrab, InnerDetect, InnerViewerImage);

            //
            OuterGrab = new EntryGrab(Static.App.RecordCacheSize);
            OuterDetect = new EntryDetect(OuterGrab, false);
            OuterViewerImage = new ViewerImage(OuterGrab, OuterDetect);
            OuterViewerChart = new ViewerChart(OuterGrab, OuterDetect, OuterViewerImage);
            
        }

        public EntryGrab InnerGrab;
        public EntryDetect InnerDetect;
        public ViewerImage InnerViewerImage;
        public ViewerChart InnerViewerChart;

        public EntryGrab OuterGrab;
        public EntryDetect OuterDetect;
        public ViewerImage OuterViewerImage;
        public ViewerChart OuterViewerChart;
        
    }
}
