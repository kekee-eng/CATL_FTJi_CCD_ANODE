﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {
    class ImageProcess {

        static HDevEngine m_engine;
        static HDevProgram m_program;

        public static bool Init(string program) {

            if (m_engine == null)
                m_engine = new HDevEngine();

            if (m_program == null)
                m_program = new HDevProgram(program);

            return true;
        }
        public static Dictionary<string, HTuple> TemplateProcess(string process, HImage image) {

            try {

                //
                var procedure = new HDevProcedure();
                procedure.LoadProcedure(m_program, process);

                //
                var call = procedure.CreateCall();
                call.SetInputIconicParamObject("Image", image);
                call.Execute();

                //
                if (false == call.GetOutputCtrlParamTuple("OutSuccess"))
                    return null;

                //
                Dictionary<string, HTuple> dict = new Dictionary<string, HTuple>();
                for (int i = 1; i <= procedure.GetOutputCtrlParamCount(); i++) {

                    string name = procedure.GetOutputCtrlParamName(i);
                    dict[name] = call.GetOutputCtrlParamTuple(i);
                }
                return dict;

            }
            catch (Exception ex) {

                //
                Config.Log.Error(string.Format("ImageProcess: {0}: {1}\n{2}", process, ex.Message, ex.StackTrace));
                return null;
            }

        }

        public static bool DetectTab(HImage image, out double x, out double[] y1, out double[] y2) {

            //
            x = 0;
            y1 = y2 = null;

            //
            var data = TemplateProcess("DetectTab", image);
            if (data == null) return false;

            //  
            x = data["OutX"];
            y1 = data["OutY1"].ToDArr();
            y2 = data["OutY2"].ToDArr();
            return true;

        }
        public static bool DetectWidth(HImage image, out double x1, out double x2) {

            //
            x1 = x2 = 0;

            //
            var data = TemplateProcess("DetectWidth", image);
            if (data == null) return false;

            //  
            x1 = data["OutX1"];
            x2 = data["OutX2"];
            return true;

        }
        public static bool DetectMark(HImage image, out double[] x, out double[] y) {

            //
            x = y = null;

            //
            var data = TemplateProcess("DetectMark", image);
            if (data == null) return false;

            //  
            x = data["OutX"].ToDArr();
            y = data["OutY"].ToDArr();
            return true;
        }
        public static bool DetectDefectFast(HImage image) {
            
            //
            var data = TemplateProcess("DetectDefectFast", image);
            if (data == null) return false;
            
            return true;
        }
        public static bool DetectDefectDeep(HImage image, out double[] x, out double[] y, out double[] w, out double[] h) {

            //
            x = y = w = h = null;

            //
            var data = TemplateProcess("DetectDefectDeep", image);
            if (data == null) return false;

            //  
            x = data["OutX"].ToDArr();
            y = data["OutY"].ToDArr();
            w = data["OutW"].ToDArr();
            h = data["OutH"].ToDArr();
            return true;
        }

    }
}
