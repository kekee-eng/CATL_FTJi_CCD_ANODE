namespace DetectCCD
{
    partial class XMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.status_user = new DevExpress.XtraBars.BarButtonItem();
            this.status_plc = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.status_info = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.selectSkin = new DevExpress.XtraBars.SkinBarSubItem();
            this.selectFullScreen = new DevExpress.XtraBars.BarButtonItem();
            this.status_diskspace = new DevExpress.XtraBars.BarStaticItem();
            this.status_memory = new DevExpress.XtraBars.BarStaticItem();
            this.status_time = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollectionStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.imageCollectionTab = new DevExpress.Utils.ImageCollection(this.components);
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.imageCollectionButton = new DevExpress.Utils.ImageCollection(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTab)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionButton)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollectionStatus;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.status_plc,
            this.status_info,
            this.status_user,
            this.barStaticItem1,
            this.selectSkin,
            this.selectFullScreen,
            this.status_time,
            this.barStaticItem2,
            this.status_diskspace,
            this.status_memory});
            this.barManager1.MaxItemId = 27;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.status_user),
            new DevExpress.XtraBars.LinkPersistInfo(this.status_plc),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.status_info),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.selectSkin),
            new DevExpress.XtraBars.LinkPersistInfo(this.selectFullScreen),
            new DevExpress.XtraBars.LinkPersistInfo(this.status_diskspace),
            new DevExpress.XtraBars.LinkPersistInfo(this.status_memory),
            new DevExpress.XtraBars.LinkPersistInfo(this.status_time)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // status_user
            // 
            this.status_user.Caption = "更换用户";
            this.status_user.Id = 14;
            this.status_user.ImageIndex = 0;
            this.status_user.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
            this.status_user.Name = "status_user";
            this.status_user.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.status_user_ItemClick);
            // 
            // status_plc
            // 
            this.status_plc.ActAsDropDown = true;
            this.status_plc.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.status_plc.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.status_plc.Caption = "设备连接状态";
            this.status_plc.Id = 12;
            this.status_plc.ImageIndex = 4;
            this.status_plc.Name = "status_plc";
            this.status_plc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.status_plc_ItemClick);
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Id = 24;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // status_info
            // 
            this.status_info.Caption = "-";
            this.status_info.Id = 13;
            this.status_info.Name = "status_info";
            this.status_info.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.barStaticItem1.Id = 15;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.Size = new System.Drawing.Size(32, 0);
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // selectSkin
            // 
            this.selectSkin.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.selectSkin.Caption = "修改皮肤";
            this.selectSkin.Id = 17;
            this.selectSkin.ImageIndex = 3;
            this.selectSkin.Name = "selectSkin";
            this.selectSkin.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            this.selectSkin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // selectFullScreen
            // 
            this.selectFullScreen.Caption = "全屏显示";
            this.selectFullScreen.Id = 18;
            this.selectFullScreen.ImageIndex = 7;
            this.selectFullScreen.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.selectFullScreen.Name = "selectFullScreen";
            this.selectFullScreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.selectFullScreen_ItemClick);
            // 
            // status_diskspace
            // 
            this.status_diskspace.Caption = "Disk";
            this.status_diskspace.Id = 25;
            this.status_diskspace.Name = "status_diskspace";
            this.status_diskspace.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // status_memory
            // 
            this.status_memory.Caption = "Memory";
            this.status_memory.Id = 26;
            this.status_memory.Name = "status_memory";
            this.status_memory.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // status_time
            // 
            this.status_time.Caption = "DateTime";
            this.status_time.Id = 23;
            this.status_time.Name = "status_time";
            this.status_time.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1184, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 626);
            this.barDockControlBottom.Size = new System.Drawing.Size(1184, 36);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 626);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1184, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 626);
            // 
            // imageCollectionStatus
            // 
            this.imageCollectionStatus.ImageSize = new System.Drawing.Size(25, 25);
            this.imageCollectionStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionStatus.ImageStream")));
            this.imageCollectionStatus.Images.SetKeyName(0, "operator_128px_30130_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(1, "engineer_nuclear_radiation_worker_128px_2057_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(2, "civil_engineer_128px_11886_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(3, "Color_Pallette_749px_1191204_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(4, "network_disconnect_256px_571982_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(5, "network_connect_256px_571981_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(6, "screen_scale_200px_1189737_easyicon.net.png");
            this.imageCollectionStatus.Images.SetKeyName(7, "screen_ful_201px_1189736_easyicon.net.png");
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Images = this.imageCollectionTab;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(1184, 626);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // imageCollectionTab
            // 
            this.imageCollectionTab.ImageSize = new System.Drawing.Size(35, 35);
            this.imageCollectionTab.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionTab.ImageStream")));
            this.imageCollectionTab.Images.SetKeyName(0, "run_128px_27882_easyicon.net.png");
            this.imageCollectionTab.Images.SetKeyName(1, "RUN_128px_1068484_easyicon.net.png");
            this.imageCollectionTab.Images.SetKeyName(2, "gear_run_settings_64px_4550_easyicon.net.png");
            this.imageCollectionTab.Images.SetKeyName(3, "System_preferences_tool_tools_512px_581754_easyicon.net.png");
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.ImageIndex = 0;
            this.xtraTabPage1.MaxTabPageWidth = 100;
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1078, 620);
            this.xtraTabPage1.Text = "设备控制";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.ImageIndex = 1;
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1078, 620);
            this.xtraTabPage2.Text = "检测界面";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage3.ImageIndex = 2;
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1078, 620);
            this.xtraTabPage3.Text = "参数配置";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1078, 620);
            this.splitContainerControl2.SplitterPosition = 255;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.ImageIndex = 3;
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1078, 620);
            this.xtraTabPage4.Text = "测试工具";
            // 
            // imageCollectionButton
            // 
            this.imageCollectionButton.ImageSize = new System.Drawing.Size(80, 80);
            this.imageCollectionButton.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionButton.ImageStream")));
            this.imageCollectionButton.Images.SetKeyName(0, "Circle_Grey_256px_566285_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(1, "Circle_Green_256px_566284_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(2, "Circle_Orange_256px_566286_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(3, "Circle_Blue_256px_566283_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(4, "Circle_Red_256px_566287_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(5, "Circle_Yellow_256px_566288_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(6, "power_128px_1180583_easyicon.net.png");
            this.imageCollectionButton.Images.SetKeyName(7, "power_128px_1180391_easyicon.net.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "配置文件|*.cfg|所有文件|*.*";
            this.saveFileDialog1.InitialDirectory = "config_package";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "配置文件|*.cfg|所有文件|*.*";
            this.openFileDialog1.InitialDirectory = "config_package";
            // 
            // XMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CATL 分条机视觉检测系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XFMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTab)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem status_plc;
        private DevExpress.XtraBars.BarStaticItem status_info;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraBars.BarButtonItem status_user;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.SkinBarSubItem selectSkin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraBars.BarButtonItem selectFullScreen;
        private DevExpress.Utils.ImageCollection imageCollectionTab;
        private DevExpress.Utils.ImageCollection imageCollectionStatus;
        private DevExpress.Utils.ImageCollection imageCollectionButton;
        private DevExpress.XtraBars.BarStaticItem status_time;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem status_diskspace;
        private DevExpress.XtraBars.BarStaticItem status_memory;
    }
}