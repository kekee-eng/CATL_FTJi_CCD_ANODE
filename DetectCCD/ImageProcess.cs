﻿
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    class ImageProcess {

        static HDevEngine m_engine = new HDevEngine();
        static HDevProgram m_program = null;
        public static string ErrorMessage;
        
        public static void Init() {
            
            //
            m_program?.Dispose();
            m_program = new HDevProgram(Static.PathImageProcess);
            
        }

        public static Dictionary<string, HTuple> TemplateProcess(string process, HImage image, out int time) {

            Dictionary<string, HTuple> call = null;
            time = UtilTool.TimeCounting(() => call = TemplateProcess(process, image));
            return call;

        }
        public static Dictionary<string, HTuple> TemplateProcess(string process, HImage image)
        {

            try
            {

                //
                if (Static.App.ImageProcessReload)
                {
                    Static.App.ImageProcessReload = false;
                    Init();
                }

                //
                HDevProcedure procedure;
                HDevProcedureCall call;
                lock (m_engine)
                {
                    procedure = new HDevProcedure(m_program, process);
                    call = procedure.CreateCall();
                    call.SetInputIconicParamObject("Image", image);
                }

                //
                call.Execute();

                //
                if (false == call.GetOutputCtrlParamTuple("OutSuccess"))
                    return null;

                //
                Dictionary<string, HTuple> dict = new Dictionary<string, HTuple>();
                for (int i = 1; i <= procedure.GetOutputCtrlParamCount(); i++)
                {

                    string name = procedure.GetOutputCtrlParamName(i);
                    dict[name] = call.GetOutputCtrlParamTuple(i);
                }

                //
                call.Dispose();
                procedure.Dispose();

                return dict;
            }
            catch (Exception ex)
            {

                //
                ErrorMessage = string.Format("ImageProcess: {0}: {1}\n{2}", process, ex.Message, ex.StackTrace);

                //
                Log.AppLog.Error(ErrorMessage);
                return null;
            }

        }

        public static bool DetectTab(HImage image, out bool hasDefect, out bool hasTab, out double[] x, out double[] y1, out double[] y2) {

            //
            hasDefect = hasTab = false;
            x = y1 = y2 = null;

            //
            var data = TemplateProcess("DetectTab", image, out TimeDetectTab);
            if (data == null) return false;
            
            if (!data.Keys.Contains("OutHasDefect")) return false;
            if (!data.Keys.Contains("OutHasTab")) return false;

            hasDefect = data["OutHasDefect"];
            hasTab = data["OutHasTab"];

            if (hasTab) {
                x = data["OutX"].ToDArr();
                y1 = data["OutY1"].ToDArr();
                y2 = data["OutY2"].ToDArr();

                //
                hasTab = (x.Length > 0) && (y1.Length > 0) && (y2.Length > 0);
            }
            return true;

        }
        public static bool DetectWidth(HImage image, out double[] x1, out double[] x2) {

            //
            x1 = x2 = null;

            //
            var data = TemplateProcess("DetectWidth", image, out TimeDetectWidth);
            if (data == null) return false;
            
            //  
            x1 = data["OutX1"];
            x2 = data["OutX2"];

            //
            if (x1.Length == 0) return false;
            if (x2.Length == 0) return false;

            return true;

        }
        public static bool DetectMark(HImage image, out double[] x, out double[] y) {

            //
            x = y = null;

            //
            var data = TemplateProcess("DetectMark", image, out TimeDetectMark);
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
        public static bool DetectDefect(HImage image, out int[] type, out double[] x, out double[] y, out double[] w, out double[] h, out double [] area) {

            //
            type = null;
            x = y = w = h =area = null;

            //
            image.GetImageSize(out ImageDefectWidth, out ImageDefectHeight);

            //
            var data = TemplateProcess("DetectDefect", image, out TimeDetectDefect);
            if (data == null) return false;
            
            //
            type = data["OutType"].ToIArr();
            x = data["OutX"].ToDArr();
            y = data["OutY"].ToDArr();
            w = data["OutW"].ToDArr();
            h = data["OutH"].ToDArr();
            area = data["OutArea"].ToDArr();

            //
            if (type.Length == 0) return false;
            if (x.Length == 0) return false;
            if (y.Length == 0) return false;
            if (w.Length == 0) return false;
            if (h.Length == 0) return false;
            if (area.Length == 0) return false;

            return true;
        }

        public static int TimeDetectTab = 0;
        public static int TimeDetectWidth = 0;
        public static int TimeDetectMark = 0;
        public static int TimeDetectDefect = 0;
        public static int ImageDefectWidth = 0;
        public static int ImageDefectHeight = 0;

    }
}
