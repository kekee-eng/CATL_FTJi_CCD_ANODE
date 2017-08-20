using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectCCD {
    public class ViewerImage : IDisposable {

        public ViewerImage(EntryGrab grab, EntryDetect detect) {

            //
            Grab = grab;
            Detect = detect;
        }
        public void Dispose() {
            mouseAllow = false;
            Image?.Dispose();
        }

        public void Init(HWindowControl hwindow) {

            //
            Box = hwindow;
            g = hwindow.HalconWindow;

            //
            initEvent(hwindow);
            initRightMenu(hwindow);

        }
        
        public EntryGrab Grab;
        public EntryDetect Detect;
        
        public HImage Image;
        public HWindowControl Box;
        public HWindow g;

        public event Action<double, double, double> OnViewUpdate;

        public void SetUserEnable(bool allow) {
            rtEnableUser.Checked = allow;
        }

        public void MoveTargetSync(double refFps) {

            double fpsNow = fpsControl;
            if (!fpsWatch.IsRunning || fpsWatch.ElapsedMilliseconds > 1000 / fpsNow) {

                //实时显示帧率
                fpsRealtime = 1000.0 / fpsWatch.ElapsedMilliseconds;

                //重置定时器
                fpsWatch.Restart();

                //
                if (!targetAllow)
                    return;

                //
                double distTotal = targetDist;
                if (distTotal == 0)
                    return;

                //
                frameVx = targetVx;
                frameVy = targetVy;
                frameVs = targetVs;

                Static.SafeRun(() => updateView(false));
            }
        }
        public void MoveTargetDirect() {

            //
            frameVx = targetVx;
            frameVy = targetVy;
            frameVs = targetVs;

            //
            updateView();
        }

        public void SetTopTarget(double y) {
            SetCenterTarget(y + frameDy / 2);
        }
        public void SetBottomTarget(double y) {
            SetCenterTarget(y + 1 - frameDy / 2);
        }
        public void SetCenterTarget(double y, double x = 0.5, double s = 1) {
            targetVx = x;
            targetVy = y;
            targetVs = s;
        }
        
        public void MoveToView(double y, double x, double s) {

            if (!mouseAllow) return;

            //
            frameVx = x;
            frameVy = y;
            frameVs = s;

            //
            updateView(false);
        }
        public void MoveToTarget(double y, double x, double w, double h) {
            if (!mouseAllow) return;

            double sx = w;
            double sy = h * grabHeight / refGrabHeight;

            double s = Math.Max(sx, sy) * 1.3;
            s = Math.Min(s, 2);
            s = Math.Max(s, 0.04);

            SetCenterTarget(y, x, s);
            MoveTargetDirect();

        }
        public void MoveToFrame(double frame) {
            if (!mouseAllow) return;

            SetCenterTarget(frame);
            MoveTargetDirect();
        }
        public void MoveToEA(int idEa) {
            MoveToTAB(idEa, 1);
        }
        public void MoveToTAB(int idEa, int idTab) {
            if (!mouseAllow) return;

            Static.SafeRun(() => {
                var obj = Detect.Tabs.Find(x => x.EA == idEa && x.TAB == idTab);
                if (obj != null) MoveToFrame(obj.TabY1);
            });
        }
        public void MoveToTAB(int id) {
            if (!mouseAllow) return;

            Static.SafeRun(() => {
                var obj = Detect.Tabs.Find(x => x.ID == id);
                if (obj != null) MoveToFrame(obj.TabY1);
            });

        }
        public void MoveToDefect(int id) {
            if (!mouseAllow) return;

            Static.SafeRun(() => {
                id = id - 1;
                if (id >= 0 && id <= Detect.Defects.Count - 1) {
                    var obj = Detect.Defects[id];

                    MoveToTarget(obj.Y, obj.X, obj.W, obj.H);
                }
            });
        }
        public void MoveToLabel(int id) {
            if (!mouseAllow) return;

            Static.SafeRun(() => {
                id = id - 1;
                if (id >= 0 && id <= Detect.Labels.Count - 1) {
                    var obj = Detect.Labels[id];

                    MoveToFrame(obj.Y);
                }
            });
        }
        
        void initRightMenu(HWindowControl hwindow) {

            //
            Func<object, string, ToolStripMenuItem> addItem = (parent, name) => {

                if (parent.GetType() == typeof(ContextMenuStrip))
                    return (parent as ContextMenuStrip).Items.Add(name) as ToolStripMenuItem;

                if (parent.GetType() == typeof(ToolStripMenuItem)) {
                    return (parent as ToolStripMenuItem).DropDownItems.Add(name) as ToolStripMenuItem;
                }

                throw new Exception("DataGrab: ImageViewer: initMenu: addItem: unknow type.");
            };
            Action<object> addSp = (parent) => {
                if (parent.GetType() == typeof(ContextMenuStrip)) {
                    (parent as ContextMenuStrip).Items.Add(new ToolStripSeparator());
                    return;
                }
                if (parent.GetType() == typeof(ToolStripMenuItem)) {
                    (parent as ToolStripMenuItem).DropDownItems.Add(new ToolStripSeparator());
                    return;
                }

                throw new Exception("DataGrab: ImageViewer: initMenu: addSeq: unknow type.");
            };
            Action<ToolStripMenuItem, Action<bool>> bindItem = (item, func) => {
                item.Click += (o, e) => item.Checked ^= true;
                if (func != null) item.CheckedChanged += (o, e) => func(item.Checked);
            };

            //
            var rtMenu = new ContextMenuStrip();
            
            //
            var rtContext = addItem(rtMenu, "显示内容");
            var rtContextDef1 = addItem(rtContext, "[预设1] 开启检测");
            var rtContextDef2 = addItem(rtContext, "[预设2] 开启定位");
            addSp(rtContext);
            var rtContextEA = addItem(rtContext, "EA分割线");
            var rtContextTab = addItem(rtContext, "极耳位置");
            var rtContextWidth = addItem(rtContext, "宽度检测结果");
            var rtContextDefect = addItem(rtContext, "瑕疵检测结果");
            var rtContextLabel = addItem(rtContext, "打标位置");
            addSp(rtContext);
            var rtContextCross = addItem(rtContext, "显示定位准星");
            var rtContextPosFrame = addItem(rtContext, "显示帧位置");
            var rtContextPosWidth = addItem(rtContext, "显示宽度检测范围");
            var rtContextPosMark = addItem(rtContext, "显示Mask检测范围");

            //
            var rtLocFrameText = new ToolStripTextBox();
            var rtLocEAText1 = new ToolStripTextBox();
            var rtLocEAText2 = new ToolStripTextBox();

            //
            addSp(rtMenu);
            rtEnableUser = addItem(rtMenu, "启用交互");
            addSp(rtMenu);
            var rtReset = addItem(rtMenu, "复位视图");
            var rtLoc = addItem(rtMenu, "定位到");
            var rtLocFrame = addItem(rtLoc, "Frame");
            rtLocFrame.DropDownItems.Add(rtLocFrameText);
            var rtLocEA = addItem(rtLoc, "EA");
            rtLocEA.DropDownItems.Add(rtLocEAText1);
            rtLocEA.DropDownItems.Add(rtLocEAText2);
            var rtMeasure = addItem(rtMenu, "测量工具");
            var rtMeasureLocPoint = addItem(rtMeasure, "定位点坐标");
            var rtMeasurePPDist = addItem(rtMeasure, "点到点测距");
            var rtMeasureRect = addItem(rtMeasure, "方框测范围");
            var rtMeasurePolygon = addItem(rtMeasure, "多边形测面积");

            //
            bindItem(rtContextDef1, null);
            bindItem(rtContextDef2, null);

            bindItem(rtContextEA, b => showContextMark = b);
            bindItem(rtContextTab, b => showContextTab = b);
            bindItem(rtContextWidth, b => showContextWidth = b);
            bindItem(rtContextDefect, b => showContextDefect = b);
            bindItem(rtContextLabel, b => showContextLabel = b);
            bindItem(rtContextCross, b => showContextCross = b);
            bindItem(rtContextPosFrame, b => showContextPosFrame = b);
            bindItem(rtContextPosWidth, b => showContextPosWidth = b);
            bindItem(rtContextPosMark, b => showContextPosMark = b);

            bindItem(rtEnableUser, b => mouseAllow = !(targetAllow = !b));

            //
            rtContextDef1.CheckedChanged += (o, e) => {
                bool b = (o as ToolStripMenuItem).Checked;
                rtContextEA.Checked = b;
                rtContextTab.Checked = b;
                rtContextWidth.Checked = b;
                rtContextDefect.Checked = b;
                rtContextLabel.Checked = b;
            };
            rtContextDef2.CheckedChanged += (o, e) => {
                bool b = (o as ToolStripMenuItem).Checked;
                rtContextCross.Checked = b;
                rtContextPosFrame.Checked = b;
                rtContextPosWidth.Checked = b;
                rtContextPosMark.Checked = b;
            };
            rtEnableUser.CheckedChanged += (o, e) => {
                bool b = (o as ToolStripMenuItem).Checked;
                rtReset.Visible = b;
                rtLoc.Visible = b;
                rtMeasure.Visible = b;
            };

            rtReset.Click += (o, e) => {
                frameVs = 1.0;
                frameVx = !showContextCross ? 0.5 : Math.Min(Math.Max(frameVx, 0), 1);
                frameVy = Math.Min(Math.Max(frameVy, frameStartLimit), frameEndLimit);
                updateView();
            };

            rtLocFrameText.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Enter) {
                    double f;
                    if (double.TryParse(rtLocFrameText.Text, out f)) {
                        MoveToFrame(f);
                        rtLocFrameText.Text = f.ToString("0.000");
                    }
                }
            };
            rtLocEAText1.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Enter) {
                    int t1, t2 = 1;
                    if (int.TryParse(rtLocEAText1.Text, out t1)) {
                        MoveToTAB(t1, t2);
                        rtLocEAText1.Text = t1.ToString();
                        rtLocEAText2.Text = t2.ToString();
                    }
                }
            };
            rtLocEAText2.KeyDown += (o, e) => {
                if (e.KeyCode == Keys.Enter) {
                    int t1, t2;
                    if (int.TryParse(rtLocEAText1.Text, out t1) && int.TryParse(rtLocEAText2.Text, out t2)) {
                        MoveToTAB(t1, t2);
                        rtLocEAText1.Text = t1.ToString();
                        rtLocEAText2.Text = t2.ToString();
                    }
                }
            };

            rtMeasureLocPoint.Click += (o, e) => {

                mouseAllow = false;

                g.SetDraw("margin");
                g.SetColor("red");
                g.SetLineWidth(2);

                double y, x;
                g.DrawPointMod(pixRow0, pixCol0, out y, out x);

                g.SetColor("green");
                g.DispCross(y, x, 100, Math.PI / 4);

                g.SetColor("yellow");
                g.SetTposition((int)y, (int)x);
                g.WriteString(string.Format("{0:0.000}, {1:0.000}", getFrameX(x), getFrameY(y)));

                mouseAllow = true;
            };
            rtMeasurePPDist.Click += (o, e) => {

                mouseAllow = false;

                double x1 = pixCol1 + (pixCol2 - pixCol1) / 4;
                double x2 = pixCol2 - (pixCol2 - pixCol1) / 4;
                double y1 = (pixRow1 + pixRow2) / 2;
                double y2 = (pixRow1 + pixRow2) / 2;

                g.SetDraw("margin");
                g.SetColor("red");
                g.SetLineWidth(2);
                g.DrawLineMod(y1, x1, y2, x2, out y1, out x1, out y2, out x2);

                double dx = (x2 - x1) * Detect.Fx / grabWidth;
                double dy = (y2 - y1) * Detect.Fy / grabHeight;
                double dist = Math.Sqrt(dy * dy + dx * dx);

                g.SetDraw("margin");
                g.SetColor("green");
                g.SetLineWidth(2);
                g.DispArrow(y1, x1, y2, x2, dist / 20);
                g.DispArrow(y2, x2, y1, x1, dist / 20);

                g.SetColor("yellow");
                g.SetTposition((int)((y1 + y2) / 2), (int)((x1 + x2) / 2));
                g.WriteString(dist.ToString("0.000") + "mm");

                mouseAllow = true;

            };
            rtMeasureRect.Click += (o, e) => {

                mouseAllow = false;

                double x1 = pixCol1 + (pixCol2 - pixCol1) / 4;
                double x2 = pixCol2 - (pixCol2 - pixCol1) / 4;
                double y1 = pixRow1 + (pixRow2 - pixRow1) / 4;
                double y2 = pixRow2 - (pixRow2 - pixRow1) / 4;

                g.SetDraw("margin");
                g.SetColor("red");
                g.SetLineWidth(2);
                g.DrawRectangle1Mod(y1, x1, y2, x2, out y1, out x1, out y2, out x2);

                double dx = (x2 - x1) * Detect.Fx / grabWidth;
                double dy = (y2 - y1) * Detect.Fy / grabHeight;

                g.SetDraw("margin");
                g.SetColor("green");
                g.SetLineWidth(2);
                g.DispRectangle1(y1, x1, y2, x2);

                g.SetColor("yellow");
                g.SetTposition((int)y1, (int)x1);
                g.WriteString(string.Format("{0:0.000}*{1:0.000}mm", dx, dy));

                mouseAllow = true;
            };
            rtMeasurePolygon.Click += (o, e) => {

                mouseAllow = false;

                g.SetDraw("margin");
                g.SetColor("red");
                g.SetLineWidth(2);
                var hg = g.DrawRegion();

                double area = hg.Area * Detect.Fx / grabWidth * Detect.Fy / grabHeight;

                g.SetDraw("margin");
                g.SetColor("green");
                g.SetLineWidth(2);
                g.DispRegion(hg);

                g.SetColor("yellow");
                g.SetTposition((int)hg.Row.D, (int)hg.Column.D);
                g.WriteString(string.Format("{0:0.000}mm2", area));

                mouseAllow = true;
            };

            //
            rtContextDef1.Checked = true;

            rtEnableUser.Checked = true;
            rtEnableUser.Checked = false;

            //
            hwindow.ContextMenuStrip = rtMenu;

        }
        void initEvent(HWindowControl hwindow) {

            //
            g.SetWindowParam("background_color", "cyan");
            g.ClearWindow();

            //
            hwindow.SizeChanged += (o, e) => Static.SafeRun(()=>updateView());

            //
            hwindow.PreviewKeyDown += (o, e) => {
                if (!mouseAllow) return;

                //
                switch (e.KeyCode) {
                    case Keys.Left: frameVx -= frameDx / 10; break;
                    case Keys.Right: frameVx += frameDx / 10; break;

                    case Keys.Up: frameVy -= frameDy / 10; break;
                    case Keys.Down: frameVy += frameDy / 10; break;
                    case Keys.PageUp: frameVy -= frameDy; break;
                    case Keys.PageDown: frameVy += frameDy; break;
                    case Keys.Home: frameVy = frameStartLimit; break;
                    case Keys.End: frameVy = frameEndLimit; break;

                    default: return;
                }

                updateView();
            };

            //
            hwindow.MouseLeave += (o, e) => mouseIsMove = false;
            hwindow.MouseUp += (o, e) => mouseIsMove = false;
            hwindow.MouseDown += (o, e) => {
                if (!mouseAllow) return;

                if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                    mouseFrameX = frameVx;
                    mouseFrameY = frameVy;
                    mouseBoxX = e.X;
                    mouseBoxY = e.Y;
                    mouseIsMove = true;
                }
            };
            hwindow.MouseMove += (o, e) => {
                if (mouseAllow && mouseIsMove) {
                    double dy = mouseBoxY - e.Y;
                    double dx = mouseBoxX - e.X;

                    if (Math.Abs(dx) > 3 || Math.Abs(dy) > 3) {
                        frameVx = mouseFrameX + dx / refBoxWidth * frameVs;
                        frameVy = mouseFrameY + dy / refBoxHeight * frameVs;

                        updateView();
                    }
                }
            };
            hwindow.HMouseWheel += (o, e) => {
                if (!mouseAllow) return;

                double s1 = frameVs;
                frameVs *= (e.Delta < 0 ? 1.1 : 1 / 1.1);
                frameVs = Math.Min(frameVs, 2);
                frameVs = Math.Max(frameVs, 0.005);
                double s2 = frameVs;

                if (!showContextCross) {
                    frameVx += (e.X / 640.0 - 0.5) * (s1 - s2);
                    frameVy += (e.Y / 480.0 - 0.5) * (s1 - s2) * refGrabHeight / grabHeight;
                }

                updateView();
            };


        }
        void updateView(bool forceEvent =true) {

            if (forceEvent)
                OnViewUpdate?.Invoke(frameVy, frameVx, frameVs);

            if (!Box.IsHandleCreated)
                return;

            if (Box.IsDisposed)
                return;

            if (frameEndRequire < frameStartLimit || frameStartRequire > frameEndLimit) {

                //超过显示范围
                g.ClearWindow();
                return;
            }

            //准备图像
            if ((frameStart != frameStartLimit && frameStart > frameStartRequire) ||
            (frameEnd != frameEndLimit && frameEnd < frameEndRequire)) {

                //
                frameStart = frameStartRequire;
                frameEnd = frameEndRequire;

                //
                Grab.Check(ref frameStart, ref frameEnd);
                Image = Grab.GetImage(frameStart, frameEnd);
            }

            if (Image == null)
                return;

            if (!Box.IsHandleCreated)
                return;

            //显示图像
            //
            g.SetPart(pixRow1, pixCol1, pixRow2, pixCol2);
            g.DispImage(Image);

            //
            double offs = 50;
            double crossSize = 100;
            double crossAngle = Math.PI / 4;
            double arrowSize = 10;

            //
            int countTab = 0;
            int countMark = 0;
            int countDefect = 0;
            int countLabel = 0;

            //极耳
            for (int i = 0; i < Detect.Tabs.Count; i++) {

                var tab = Detect.Tabs[i];
                if (tab.InRange(frameStart, frameEnd)) {
                    countTab++;

                    if (showContextTab) {

                        //极耳外框
                        g.SetDraw("margin");
                        g.SetColor(tab.IsFix ? "red" : "green");
                        g.SetLineWidth(1);
                        g.DispRectangle1(getPixRow(tab.TabY1) - offs, getPixCol(tab.TabX) - offs, getPixRow(tab.TabY2) + offs, getPixCol(tab.TabX) + offs);
                        if (tab.HasTwoTab)
                            g.DispRectangle1(getPixRow(tab.TabY1_P) - offs, getPixCol(tab.TabX_P) - offs, getPixRow(tab.TabY2_P) + offs, getPixCol(tab.TabX_P) + offs);

                        //极耳控制点
                        g.SetDraw("margin");
                        g.SetColor("blue");
                        g.SetLineWidth(3);
                        g.DispCross(getPixRow(tab.TabY1), getPixCol(tab.TabX), crossSize, crossAngle);
                        g.DispCross(getPixRow(tab.TabY2), getPixCol(tab.TabX), crossSize, crossAngle);
                        if (tab.HasTwoTab) {
                            g.DispCross(getPixRow(tab.TabY1_P), getPixCol(tab.TabX_P), crossSize, crossAngle);
                            g.DispCross(getPixRow(tab.TabY2_P), getPixCol(tab.TabX_P), crossSize, crossAngle);
                        }

                        //极耳标识
                        g.SetDraw("margin");
                        g.SetColor("yellow");
                        g.SetLineWidth(1);
                        double pos = tab.WidthX1 != 0 ? tab.WidthX1 : tab.TabX;
                        g.SetTposition((int)getPixRow(tab.TabY1), (int)(getPixCol(pos) + offs));
                        g.WriteString(string.Format("#{0}.#{1}", tab.EA, tab.TAB));

                    }
                    if (showContextWidth) {

                        //测宽
                        g.SetDraw("margin");
                        g.SetColor(tab.IsWidthFail ? "red" : "green");
                        g.SetLineWidth(1);
                        g.DispLine(getPixRow(tab.WidthY1), getPixCol(tab.WidthX1), getPixRow(tab.WidthY2), getPixCol(tab.WidthX1));
                        g.DispLine(getPixRow(tab.WidthY1), getPixCol(tab.WidthX2), getPixRow(tab.WidthY2), getPixCol(tab.WidthX2));
                        g.DispArrow(getPixRow((tab.WidthY1 + tab.WidthY2) / 2), getPixCol((tab.WidthX1 + tab.WidthX2) / 2), getPixRow((tab.WidthY1 + tab.WidthY2) / 2), getPixCol(tab.WidthX1), arrowSize);
                        g.DispArrow(getPixRow((tab.WidthY1 + tab.WidthY2) / 2), getPixCol((tab.WidthX1 + tab.WidthX2) / 2), getPixRow((tab.WidthY1 + tab.WidthY2) / 2), getPixCol(tab.WidthX2), arrowSize);

                        //侧宽标识
                        g.SetDraw("margin");
                        g.SetColor("yellow");
                        g.SetLineWidth(1);
                        g.SetTposition((int)getPixRow(tab.WidthY1), (int)(getPixCol(tab.WidthX1) + offs));
                        g.WriteString(tab.ValWidth.ToString("0.000"));

                    }
                    if (showContextMark && tab.IsNewEA) {
                        countMark++;

                        //EA-Mark点
                        g.SetDraw("margin");
                        g.SetColor("blue");
                        g.SetLineWidth(3);
                        g.DispCross(getPixRow(tab.MarkY), getPixCol(tab.MarkX), crossSize, crossAngle);
                        if (tab.HasTwoMark)
                            g.DispCross(getPixRow(tab.MarkY_P), getPixCol(tab.MarkX_P), crossSize, crossAngle);

                        //EA头部显示
                        g.SetDraw("margin");
                        g.SetColor("yellow");
                        g.SetLineWidth(1);
                        g.SetLineStyle(new HTuple(new int[] { 20, 7 }));
                        g.DispLine(getPixRow(tab.MarkY), getPixCol(0), getPixRow(tab.MarkY), getPixCol(1));
                        g.SetLineStyle(new HTuple());

                        g.SetTposition((int)getPixRow(tab.MarkY), (int)getPixCol(0));
                        g.WriteString(string.Format("EA=#{0}", tab.EA));

                    }

                    if (showContextPosWidth) {

                        g.SetDraw("margin");
                        g.SetColor("pink");
                        g.SetLineWidth(1);
                        g.DispLine(getPixRow(tab.WidthY1), getPixCol(0), getPixRow(tab.WidthY1), getPixCol(1));
                        g.DispLine(getPixRow(tab.WidthY2), getPixCol(0), getPixRow(tab.WidthY2), getPixCol(1));

                        double dx = (tab.WidthY2 - tab.WidthY1) * grabHeight / grabWidth;
                        for (double xp = 0; xp < 1 + dx; xp += 0.05) {
                            g.DispLine(getPixRow(tab.WidthY1), getPixCol(xp), getPixRow(tab.WidthY2), getPixCol(xp - dx));
                        }
                    }

                    if (showContextPosMark) {

                        g.SetDraw("margin");
                        g.SetColor("light blue");
                        g.SetLineWidth(1);
                        g.DispLine(getPixRow(tab.MarkImageStart), getPixCol(0), getPixRow(tab.MarkImageStart), getPixCol(1));
                        g.DispLine(getPixRow(tab.MarkImageEnd), getPixCol(0), getPixRow(tab.MarkImageEnd), getPixCol(1));

                        double dx = (tab.MarkImageEnd - tab.MarkImageStart) * grabHeight / grabWidth;
                        for (double xp = -dx; xp < 1; xp += 0.05) {
                            g.DispLine(getPixRow(tab.MarkImageStart), getPixCol(xp), getPixRow(tab.MarkImageEnd), getPixCol(xp + dx));
                        }
                    }

                }
            }

            //瑕疵
            if (showContextDefect) {
                for (int i = 0; i < Detect.Defects.Count; i++) {

                    var def = Detect.Defects[i];
                    if (def.InRange(frameStart, frameEnd)) {
                        countDefect++;

                        //瑕疵
                        g.SetDraw("margin");
                        g.SetColor("red");
                        g.SetLineWidth(1);
                        g.DispRectangle1(
                            getPixRow(def.Y - def.H / 2 * 1.1),
                            getPixCol(def.X - def.W / 2 * 1.1),
                            getPixRow(def.Y + def.H / 2 * 1.1),
                            getPixCol(def.X + def.W / 2 * 1.1));

                    }
                }
            }

            if (showContextMark && Static.App.Is8K) {

                for (int i = 0; i < Detect._IN_8K_FROM_4K.Count; i++) {
                    var extEa = Detect._IN_8K_FROM_4K[i];
                    if (extEa.InRange(frameStart, frameEnd)) {

                        g.SetDraw("margin");
                        g.SetColor("yellow");
                        g.SetLineWidth(1);
                        g.SetLineStyle(new HTuple(new int[] { 20, 7 }));
                        g.DispLine(getPixRow(extEa.Start), getPixCol(0), getPixRow(extEa.Start), getPixCol(1));
                        g.DispLine(getPixRow(extEa.End), getPixCol(0), getPixRow(extEa.End), getPixCol(1));
                        g.SetLineStyle(new HTuple());

                        g.SetTposition((int)getPixRow(extEa.Start), (int)getPixCol(0));
                        g.SetTposition((int)getPixRow(extEa.End), (int)getPixCol(0));
                        g.WriteString(string.Format("EAStart=#{0}", extEa.EA));
                    }
                }
            }

            //打标
            if (showContextLabel) {
                for (int i = 0; i < Detect.Labels.Count; i++) {

                    var lab = Detect.Labels[i];
                    if (lab.InRange(frameStart, frameEnd)) {
                        countLabel++;
                        
                        g.SetDraw("margin");
                        g.SetColor("red");
                        g.SetLineWidth(3);
                        g.SetLineStyle(new HTuple(new int[] { 3, 1 }));
                        
                        g.DispRectangle1(
                            getPixRow(lab.Y - lab.H / 2),
                            getPixCol(lab.X - lab.W / 2),
                            getPixRow(lab.Y + lab.H / 2),
                            getPixCol(lab.X + lab.W / 2));
                        
                        g.SetLineStyle(new HTuple());

                        g.SetColor("yellow");
                        g.SetTposition((int)getPixRow(lab.Y - lab.H / 2), (int)getPixCol(lab.X - lab.W / 2));
                        g.WriteString(string.Format("{0}", i + 1));

                    }
                }
            }

            //定位准星
            if (showContextCross) {

                g.SetDraw("margin");
                g.SetColor("red");
                g.SetLineWidth(1);
                g.DispLine(pixRow1, pixCol0, pixRow2, pixCol0);
                g.DispLine(pixRow0, pixCol1, pixRow0, pixCol2);

                g.SetDraw("margin");
                g.SetColor("yellow");
                g.SetLineWidth(1);
                g.SetTposition((int)pixRow0, (int)pixCol0);
                g.WriteString(string.Format("{0:0.000}, {1:0.000}, {2:0.000}", frameVy, frameVx, frameVs));

            }

            //帧分割
            if (showContextPosFrame) {
                for (int i = frameStart; i <= frameEnd; i++) {

                    g.SetDraw("margin");
                    g.SetColor("violet");
                    g.SetLineWidth(1);
                    g.DispLine(getPixRow(i), pixCol1, getPixRow(i), pixCol2);

                    g.SetLineWidth(1);
                    g.SetTposition((int)getPixRow(i), 0);
                    g.WriteString(string.Format("[{0}]", i));

                }
            }

            //
            countShowDefect = countDefect;
            countShowLabel = countLabel;
            countShowMark = countMark;
            countShowTab = countTab;

            //清空不显示区域
            int bw = boxWidth;
            int bh = boxHeight;
            int gw = grabWidth;
            int gh = grabHeight;

            double x1 = 1.0 * bw * (0 - pixCol1) / (pixCol2 - pixCol1);
            double x2 = 1.0 * bw * (gw - pixCol1) / (pixCol2 - pixCol1);
            double y1 = 1.0 * bh * (0 - pixRow1) / (pixRow2 - pixRow1);
            double y2 = 1.0 * bh * (gh * (frameEnd - frameStart + 1) - pixRow1) / (pixRow2 - pixRow1);

            if (x1 > 0) g.ClearRectangle(0, 0, bh, x1);
            if (x2 < bw) g.ClearRectangle(0, x2, bh, bw);
            if (y1 > 0) g.ClearRectangle(0, 0, y1, bw);
            if (y2 < bh) g.ClearRectangle(y2, 0, bh, bw);

        }
        
        ToolStripMenuItem rtEnableUser;
        
        bool showContextMark = false;
        bool showContextTab = false;
        bool showContextWidth = false;
        bool showContextDefect = false;
        bool showContextLabel = false;
        bool showContextCross = false;
        bool showContextPosFrame = false;
        bool showContextPosWidth = false;
        bool showContextPosMark = false;

        int countShowLabel = 0;
        int countShowDefect = 0;
        int countShowTab = 0;
        int countShowMark = 0;

        //
        bool mouseAllow = true;
        bool mouseIsMove = false;
        int mouseBoxX = 0;
        int mouseBoxY = 0;
        double mouseFrameX = 0;
        double mouseFrameY = 0;

        //
        double fpsControl = 1;
        double fpsRealtime = 1;
        Stopwatch fpsWatch = new Stopwatch();

        //
        bool targetAllow = false;
        double targetVx = 0.5;
        double targetVy = 0;
        double targetVs = 1;

        double targetDx { get { return targetVx - frameVx; } }
        double targetDy { get { return targetVy - frameVy; } }
        double targetDs { get { return targetVs - frameVs; } }
        double targetDist { get { return Math.Sqrt(targetDx * targetDx + targetDy * targetDy); } }

        //
        double frameVx = 0.5;
        double frameVy = 0;
        double frameVs = 1;

        double frameDx { get { return frameVs * refGrabWidth / grabWidth; } }
        double frameDy { get { return frameVs * refGrabHeight / grabHeight; } }

        double frameX1 { get { return frameVx - frameDx / 2; } }
        double frameY1 { get { return frameVy - frameDy / 2; } }

        double frameX2 { get { return frameVx + frameDx / 2; } }
        double frameY2 { get { return frameVy + frameDy / 2; } }

        //
        int frameStart = 0;
        int frameEnd = 0;

        int frameStartRequire { get { return (int)Math.Floor(frameY1); } }
        int frameEndRequire { get { return (int)Math.Ceiling(frameY2); } }

        int frameStartLimit { get { return Grab.Min; } }
        int frameEndLimit { get { return Grab.Max + 1; } }

        //
        double getFrameX(double px) { return px / grabWidth; }
        double getFrameY(double py) { return py / grabHeight + frameStart; }
        double getPixCol(double framex) { return framex * grabWidth; }
        double getPixRow(double framey) { return (framey - frameStart) * grabHeight; }

        //
        double pixCol0 { get { return (int)getPixCol(frameVx); } }
        double pixRow0 { get { return (int)getPixRow(frameVy); } }

        int pixCol1 { get { return (int)getPixCol(frameX1); } }
        int pixCol2 { get { return (int)getPixCol(frameX2); } }
        int pixRow1 { get { return (int)getPixRow(frameY1); } }
        int pixRow2 { get { return (int)getPixRow(frameY2); } }

        //
        int grabWidth { get { return Grab.Width; } }
        int grabHeight { get { return Grab.Height; } }
        int boxWidth { get { return Box.Width; } }
        int boxHeight { get { return Box.Height; } }

        int refGrabWidth { get { return grabWidth; } }
        int refGrabHeight { get { return grabWidth * boxHeight / boxWidth; } }
        int refBoxWidth { get { return boxWidth; } }
        int refBoxHeight { get { return boxWidth * grabHeight / grabWidth; } }


    }
}