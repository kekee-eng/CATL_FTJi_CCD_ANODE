﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {
    
    class TemplateCamera : IDisposable{
        
        public virtual void Reset() { }
        public virtual void Freeze() { }
        public virtual void Grab() { }
        public virtual void Dispose() { }

        public virtual int getMin() { return 0; }
        public virtual int getMax() { return 0; }

        public int Min {
            get {
                try { return getMin(); }
                catch { return 0; }
            }
        }
        public int Max {
            get {
                try { return getMax(); }
                catch { return 0; }
            }
        }

        protected void callImageReady(DataGrab obj) {
            OnImageReady?.Invoke(obj);
        }
        protected int callGetEncoder() {
            return GetEncoder == null ? 0 : GetEncoder();
        }
        protected void callComplete() {
            OnComplete?.Invoke();
        }

        public event Action<DataGrab> OnImageReady = null;
        public event Func<int> GetEncoder = null;
        public event Action OnComplete = null;

        public int m_frame = 0;
        public int m_frameStart = 0;
        public double m_fpsControl = 1.0;
        public double m_fpsRealtime = 0.0;

        public bool isOpen = false;
        public bool isGrabbing = false;

        public string m_camera_name = "";

    }
}
