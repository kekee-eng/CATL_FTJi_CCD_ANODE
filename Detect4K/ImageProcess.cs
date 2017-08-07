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
        public static string ErrorMessage;
        
        public static Dictionary<string, HTuple> TemplateProcess(string process, HImage image) {

            try {

                //
                if (m_engine == null)
                    m_engine = new HDevEngine();

                if (m_program == null)
                    m_program = new HDevProgram(Config.PathImageProcess);

                if(Config.App.ProcessReload) {
                    m_program.Dispose();
                    m_program = new HDevProgram(Config.PathImageProcess);
                }
                
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
                ErrorMessage = string.Format("ImageProcess: {0}: {1}\n{2}", process, ex.Message, ex.StackTrace);

                //
                Config.Log.Error(ErrorMessage);
                return null;
            }

        }
        public static bool DetectTab(HImage image, out double[] x, out double[] y1, out double[] y2) {

            //
            x = y1 = y2 = null;

            //
            var data = TemplateProcess("DetectTab", image);
            if (data == null) return false;

            //
            if (!data.Keys.Contains("OutX")) return false;
            if (!data.Keys.Contains("OutY1")) return false;
            if (!data.Keys.Contains("OutY2")) return false;

            //  
            x = data["OutX"].ToDArr();
            y1 = data["OutY1"].ToDArr();
            y2 = data["OutY2"].ToDArr();

            //
            if (x.Length == 0) return false;
            if (y1.Length == 0) return false;
            if (y2.Length == 0) return false;

            return true;

        }
        public static bool DetectWidth(HImage image, out double x1, out double x2) {

            //
            x1 = x2 = 0;

            //
            var data = TemplateProcess("DetectWidth", image);
            if (data == null) return false;

            //
            if (!data.Keys.Contains("OutX1")) return false;
            if (!data.Keys.Contains("OutX2")) return false;

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
            if (!data.Keys.Contains("OutX")) return false;
            if (!data.Keys.Contains("OutY")) return false;

            //
            x = data["OutX"].ToDArr();
            y = data["OutY"].ToDArr();

            //
            if (x.Length == 0) return false;
            if (y.Length == 0) return false;
            
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
            if (!data.Keys.Contains("OutX")) return false;
            if (!data.Keys.Contains("OutY")) return false;
            if (!data.Keys.Contains("OutW")) return false;
            if (!data.Keys.Contains("OutH")) return false;

            //  
            x = data["OutX"].ToDArr();
            y = data["OutY"].ToDArr();
            w = data["OutW"].ToDArr();
            h = data["OutH"].ToDArr();

            //
            if (x.Length == 0) return false;
            if (y.Length == 0) return false;
            if (w.Length == 0) return false;
            if (h.Length == 0) return false;

            return true;
        }

    }
}
