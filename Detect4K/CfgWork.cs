using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {

    class CfgWork : Common.TemplateConfig {
        public CfgWork(string path) : base(path) { }

        //
        public double ImageWidth = 4096;
        public double ImageHeight = 1000;

        public double ScaleX = 0.06;
        public double ScaleY = 0.06;

        public double Fx { get { return ImageWidth * ScaleX; } }
        public double Fy { get { return ImageHeight * ScaleY; } }
        
        //
        //public double GetPixelX(double framex) { return framex * ImageWidth; }
        //public double GetPixelY(double framey) { return framey * ImageHeight; }
        public double GetFrameX(double posx) { return posx / ScaleX / ImageWidth; }
        public double GetFrameY(double posy) { return posy / ScaleY / ImageHeight; }
        public double GetPosX(double framex) { return framex * ScaleX * ImageWidth; }
        public double GetPosY(double framey) { return framey * ScaleY * ImageHeight; }


    }
}
