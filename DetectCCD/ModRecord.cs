
using System;

namespace DetectCCD {
    public class ModRecord : TemplateDB {

        public override bool Open(string path) {
            
            if (base.Open(path)) {

                InnerDetect.CreateTable();
                InnerGrab.DB.CreateTable();
                InnerGrab.Cache.Dispose();

                OuterDetect.CreateTable();
                OuterGrab.DB.CreateTable();
                OuterGrab.Cache.Dispose();

                InnerGrab.Reload();
                OuterGrab.Reload();

                InnerDetect.Reload();
                OuterDetect.Reload();
                return true;
            }
            return false;
        }

        public void Uninit() {

            InnerGrab.Cache.Dispose();
            InnerViewerImage.Dispose();

            OuterGrab.Cache.Dispose();
            OuterViewerImage.Dispose();

        }

        public void Init() {

            //
            InnerGrab = new EntryGrab(this, "InnerGrab", Static.App.RecordCacheSize);
            InnerDetect = new EntryDetect(this, "InnerDetect", InnerGrab, true);
            InnerViewerImage = new ViewerImage(InnerGrab, InnerDetect);
            InnerViewerChart = new ViewerChart(InnerGrab, InnerDetect, InnerViewerImage);

            //
            OuterGrab = new EntryGrab(this, "OuterGrab", Static.App.RecordCacheSize);
            OuterDetect = new EntryDetect(this, "OuterDetect", OuterGrab, false);
            OuterViewerImage = new ViewerImage(OuterGrab, OuterDetect);
            OuterViewerChart = new ViewerChart(OuterGrab, OuterDetect, OuterViewerImage);

            //
            if (Static.App.IsRemoteServer) {

            }

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
