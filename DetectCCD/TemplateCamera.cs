using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    public class TemplateCamera : IDisposable{
        
        public virtual void Reset() { }
        public virtual void Freeze() { }
        public virtual void Grab() { }
        public virtual void Dispose() { }

        public virtual int getMin() { return -1; }
        public virtual int getMax() { return -1; }

        public int Min {
            get {
                try { return getMin(); }
                catch { return -1; }
            }
        }
        public int Max {
            get {
                try { return getMax(); }
                catch { return -1; }
            }
        }

        protected void callImageReady(DataGrab obj) {

            //附加像素数据
            obj.ScaleX = ScaleX;
            obj.ScaleY = ScaleY;
            obj.Image.GetImageSize(out obj.Width, out obj.Height);

            //
            OnImageReady?.Invoke(obj);
        }
        public int callGetEncoder() {
            return GetEncoder == null ? 0 : GetEncoder();
        }
        public void callComplete() {
            OnComplete?.Invoke();
        }

        public event Action<DataGrab> OnImageReady = null;
        public event Func<int> GetEncoder = null;
        public event Action OnComplete = null;
        
        public int m_encoder = 0;
        public int m_frame = 0;
        public int m_frameStart = 0;
        public double m_fpsControl = 1.0;
        public double m_fpsRealtime = 0.0;

        public bool isOpen = false;
        public bool isGrabbing = false;

        public double ScaleX =0;
        public double ScaleY =0;

        public string Name = "";
        public string Caption = "";
        
    }
}
