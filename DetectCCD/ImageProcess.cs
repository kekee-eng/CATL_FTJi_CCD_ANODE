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
        public static Dictionary<string, HTuple> TemplateProcess(string process, HImage image) {

            try {

                //
                if (Static.App.ImageProcessReload) {
                    Static.App.ImageProcessReload = false;
                    Init();
                }

                //
                if (image == null || !image.IsInitialized())
                    return null;

                //
                HDevProcedure procedure;
                HDevProcedureCall call;
                lock (m_engine) {
                    procedure = new HDevProcedure(m_program, process);
                    call = procedure.CreateCall();
                    call.SetInputIconicParamObject("Image", image);
                    if (process == "DetectDefect") {
                        HTuple param = new HTuple();
                        if (Static.App.ImageProcessParamEnable) {
                            param[0] = Static.App.ImageProcessParam_DetectDefect_MaxGray;
                            param[1] = Static.App.ImageProcessParam_DetectDefect_MinGray;
                            param[2] = Static.App.ImageProcessParam_DetectDefect_Deviation;
                            param[3] = Static.App.ImageProcessParam_DetectDefect_Area;
                        }
                        else {
                            param[0] = 2;
                            param[1] = 0.5;
                            param[2] = 15;
                            param[3] = 1200;
                        }
                        call.SetInputCtrlParamTuple("InParam", param);
                    }
                }

                //
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

                //
                call.Dispose();
                procedure.Dispose();

                return dict;
            }
            catch (Exception ex) {

                //
                ErrorMessage = string.Format("ImageProcess: {0}: {1}\n{2}", process, ex.Message, ex.StackTrace);

                //
                Log.AppLog.Error(ErrorMessage);
                return null;
            }

        }

        public static bool DetectTab(HImage image, out bool hasDefect, out bool hasDefect1, out bool hasDefect2, out bool hasTab, out double[] x, out double[] y1, out double[] y2) {

            //
            hasDefect = hasDefect1 = hasDefect2 = hasTab = false;
            x = y1 = y2 = null;

            //
            var data = TemplateProcess("DetectTab", image, out TimeDetectTab);
            if (data == null) return false;

            if (!data.Keys.Contains("OutHasDefect")) return false;
            if (!data.Keys.Contains("OutHasTab")) return false;

            hasDefect1 = data["OutHasDefect1"];
            hasDefect2 = data["OutHasDefect2"];
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
        public static bool DetectDefect(HImage image, out int[] type, out double[] x, out double[] y, out double[] w, out double[] h, out double[] area) {

            //
            type = null;
            x = y = w = h = area = null;

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

        public static bool DetectDarkLineLeakMetal(HImage image, out double px, out double pw) {
            pw = px = 0;
            try {

                //取得模区边界
                int xstart, xend;
                try {
                    var region = image.Threshold(Static.App.LineLeakMetalParam_bgGray, 255.0);
                    region = region.Connection();
                    region = region.SelectShapeStd("max_area", 5000);

                    HTuple row, x1list, x2list;
                    region.GetRegionRuns(out row, out x1list, out x2list);
                    
                    int x1 = x1list.TupleMedian();
                    int x2 = x2list.TupleMedian();
                    int offset = Static.App.LineLeakMetalParam_offset;

                    xstart = x1 + offset;
                    xend = x2 - offset;

                }catch
                {
                    throw new Exception("取得模区边界错误，请修改参数！");
                }

                //取得投影
                double[] VP;
                {
                    HTuple vertProjection;
                    image.GrayProjections(image, "simple", out vertProjection);

                    VP = vertProjection.DArr;
                }

                //判定
                {
                    //
                    var checkVP = VP.Skip(xstart).Take(xend - xstart);

                    double mean = checkVP.Average();
                    double max = checkVP.Max();
                    double min = checkVP.Min();

                    if (mean - min > Static.App.LineLeakMetalParam_downThreshold) {
                        //暗痕线性漏金属
                        int minid = VP.FindIndex(x => x == min);

                        //
                        px = minid;
                        pw = 40;
                        return true;
                    }

                    if (max - mean > Static.App.LineLeakMetalParam_upThreshold) {
                        //亮痕线性漏金属
                        int maxid = VP.FindIndex(x => x == max);
                        px = maxid;
                        pw = 40;
                        return true;
                    }
                }

            }
            catch (Exception ex) {
                Log.AppLog.Error("暗痕线性漏金属检测异常：", ex);
            }
            return false;
        }
    }
}