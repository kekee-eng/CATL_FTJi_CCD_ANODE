﻿namespace DetectCCD
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
            this.status_device = new DevExpress.XtraBars.BarButtonItem();
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
            this.groupRemoteClient = new DevExpress.XtraEditors.GroupControl();
            this.btnConnectRemotePLC = new DevExpress.XtraEditors.SimpleButton();
            this.btnConnectRemote8K = new DevExpress.XtraEditors.SimpleButton();
            this._lc_remote_plc = new DevExpress.XtraEditors.LabelControl();
            this._lc_remote_8k = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnStopGrab = new DevExpress.XtraEditors.SimpleButton();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuit = new DevExpress.XtraEditors.SimpleButton();
            this.btnStartGrab = new DevExpress.XtraEditors.SimpleButton();
            this.textMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupRoll = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControlRoll = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageRollOnline = new DevExpress.XtraTab.XtraTabPage();
            this.btnRollSet = new DevExpress.XtraEditors.SimpleButton();
            this.textRollType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textRollName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textRollRepeat = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPageRollOffline = new DevExpress.XtraTab.XtraTabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.groupStatuOuter = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._lc_outer_frame = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_isgrabbing = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_isopen = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_camera = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._lc_outer_defectCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_widthCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_eaCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this._lc_outer_caption = new DevExpress.XtraEditors.LabelControl();
            this.hwinOuter = new HalconDotNet.HWindowControl();
            this.groupStatuInner = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lc_inner_frame = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_isgrabbing = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_isopen = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_camera = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this._lc_inner_defectCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_widthCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl37 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_eaCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl39 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this._lc_inner_caption = new DevExpress.XtraEditors.LabelControl();
            this.hwinInner = new HalconDotNet.HWindowControl();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelTabMergeChart = new System.Windows.Forms.Panel();
            this.panelTabMergeGrid = new System.Windows.Forms.Panel();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panelLabel2 = new System.Windows.Forms.Panel();
            this.panelLabel1 = new System.Windows.Forms.Panel();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDefect2 = new System.Windows.Forms.Panel();
            this.panelDefect1 = new System.Windows.Forms.Panel();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.groupLabel = new DevExpress.XtraEditors.GroupControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.textLabelEAOffset = new DevExpress.XtraEditors.TextEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.textLabelDefectOffset = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.checkEnableLabelEAEveryOne = new DevExpress.XtraEditors.CheckEdit();
            this.checkEnableLabelEA = new DevExpress.XtraEditors.CheckEdit();
            this.checkEnableLabelDefect = new DevExpress.XtraEditors.CheckEdit();
            this.groupCtrl = new DevExpress.XtraEditors.GroupControl();
            this.btnOpenViewerChart = new DevExpress.XtraEditors.SimpleButton();
            this.btnOfflineControl = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkDisableView = new DevExpress.XtraEditors.CheckEdit();
            this.ckViewOuterBack = new DevExpress.XtraEditors.CheckEdit();
            this.ckViewOuterFront = new DevExpress.XtraEditors.CheckEdit();
            this.ckViewInnerBack = new DevExpress.XtraEditors.CheckEdit();
            this.ckViewInnerFront = new DevExpress.XtraEditors.CheckEdit();
            this.ckViewLocal = new DevExpress.XtraEditors.CheckEdit();
            this.groupTest = new DevExpress.XtraEditors.GroupControl();
            this.checkDetectDefect = new DevExpress.XtraEditors.CheckEdit();
            this.checkDetectMark = new DevExpress.XtraEditors.CheckEdit();
            this.checkDetectWidth = new DevExpress.XtraEditors.CheckEdit();
            this.checkDetectTab = new DevExpress.XtraEditors.CheckEdit();
            this.checkSaveNG = new DevExpress.XtraEditors.CheckEdit();
            this.checkSaveOK = new DevExpress.XtraEditors.CheckEdit();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkEnableLabelEAForce = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.textLabelEAForce = new DevExpress.XtraEditors.TextEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTab)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupRemoteClient)).BeginInit();
            this.groupRemoteClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupRoll)).BeginInit();
            this.groupRoll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlRoll)).BeginInit();
            this.xtraTabControlRoll.SuspendLayout();
            this.xtraTabPageRollOnline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textRollType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRollName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRollRepeat.Properties)).BeginInit();
            this.xtraTabPageRollOffline.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupStatuOuter)).BeginInit();
            this.groupStatuOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupStatuInner)).BeginInit();
            this.groupStatuInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.xtraTabPage7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupLabel)).BeginInit();
            this.groupLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelEAOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelDefectOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEAEveryOne.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelDefect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrl)).BeginInit();
            this.groupCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkDisableView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewOuterBack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewOuterFront.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewInnerBack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewInnerFront.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewLocal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTest)).BeginInit();
            this.groupTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectDefect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectTab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSaveNG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSaveOK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEAForce.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelEAForce.Properties)).BeginInit();
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
            this.status_device,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.status_device),
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
            // status_device
            // 
            this.status_device.ActAsDropDown = true;
            this.status_device.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.status_device.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.status_device.Caption = "设备连接状态";
            this.status_device.Id = 12;
            this.status_device.ImageIndex = 4;
            this.status_device.Name = "status_device";
            this.status_device.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.status_plc_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1248, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 626);
            this.barDockControlBottom.Size = new System.Drawing.Size(1248, 36);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1248, 0);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(1248, 626);
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
            this.xtraTabPage1.Controls.Add(this.groupRemoteClient);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Controls.Add(this.groupRoll);
            this.xtraTabPage1.ImageIndex = 0;
            this.xtraTabPage1.MaxTabPageWidth = 100;
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1142, 620);
            this.xtraTabPage1.Text = "设备控制";
            // 
            // groupRemoteClient
            // 
            this.groupRemoteClient.Controls.Add(this.btnConnectRemotePLC);
            this.groupRemoteClient.Controls.Add(this.btnConnectRemote8K);
            this.groupRemoteClient.Controls.Add(this._lc_remote_plc);
            this.groupRemoteClient.Controls.Add(this._lc_remote_8k);
            this.groupRemoteClient.Controls.Add(this.labelControl6);
            this.groupRemoteClient.Controls.Add(this.labelControl4);
            this.groupRemoteClient.Location = new System.Drawing.Point(664, 26);
            this.groupRemoteClient.Name = "groupRemoteClient";
            this.groupRemoteClient.Size = new System.Drawing.Size(289, 199);
            this.groupRemoteClient.TabIndex = 1;
            this.groupRemoteClient.Text = "联机设备状态";
            // 
            // btnConnectRemotePLC
            // 
            this.btnConnectRemotePLC.Location = new System.Drawing.Point(168, 73);
            this.btnConnectRemotePLC.Name = "btnConnectRemotePLC";
            this.btnConnectRemotePLC.Size = new System.Drawing.Size(101, 30);
            this.btnConnectRemotePLC.TabIndex = 29;
            this.btnConnectRemotePLC.Text = "连接";
            this.btnConnectRemotePLC.Click += new System.EventHandler(this.btnConnectRemotePLC_Click);
            // 
            // btnConnectRemote8K
            // 
            this.btnConnectRemote8K.Location = new System.Drawing.Point(168, 37);
            this.btnConnectRemote8K.Name = "btnConnectRemote8K";
            this.btnConnectRemote8K.Size = new System.Drawing.Size(101, 30);
            this.btnConnectRemote8K.TabIndex = 28;
            this.btnConnectRemote8K.Text = "连接";
            this.btnConnectRemote8K.Click += new System.EventHandler(this.btnConnectRemote8K_Click);
            // 
            // _lc_remote_plc
            // 
            this._lc_remote_plc.Location = new System.Drawing.Point(91, 81);
            this._lc_remote_plc.Name = "_lc_remote_plc";
            this._lc_remote_plc.Size = new System.Drawing.Size(4, 14);
            this._lc_remote_plc.TabIndex = 3;
            this._lc_remote_plc.Text = "-";
            // 
            // _lc_remote_8k
            // 
            this._lc_remote_8k.Location = new System.Drawing.Point(91, 45);
            this._lc_remote_8k.Name = "_lc_remote_8k";
            this._lc_remote_8k.Size = new System.Drawing.Size(4, 14);
            this._lc_remote_8k.TabIndex = 2;
            this._lc_remote_8k.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(33, 81);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(20, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "PLC";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(33, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(15, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "8K";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnDisconnect);
            this.groupControl1.Controls.Add(this.btnStopGrab);
            this.groupControl1.Controls.Add(this.btnConnect);
            this.groupControl1.Controls.Add(this.btnQuit);
            this.groupControl1.Controls.Add(this.btnStartGrab);
            this.groupControl1.Controls.Add(this.textMode);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(39, 26);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(279, 296);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "初始化设置";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(31, 139);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(101, 30);
            this.btnDisconnect.TabIndex = 29;
            this.btnDisconnect.Text = "关闭设备";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnStopGrab
            // 
            this.btnStopGrab.Location = new System.Drawing.Point(154, 139);
            this.btnStopGrab.Name = "btnStopGrab";
            this.btnStopGrab.Size = new System.Drawing.Size(101, 30);
            this.btnStopGrab.TabIndex = 27;
            this.btnStopGrab.Text = "停止";
            this.btnStopGrab.Click += new System.EventHandler(this.btnStopGrab_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(31, 103);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 30);
            this.btnConnect.TabIndex = 28;
            this.btnConnect.Text = "开启设备";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(31, 225);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(101, 30);
            this.btnQuit.TabIndex = 27;
            this.btnQuit.Text = "退出系统";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnStartGrab
            // 
            this.btnStartGrab.Location = new System.Drawing.Point(154, 103);
            this.btnStartGrab.Name = "btnStartGrab";
            this.btnStartGrab.Size = new System.Drawing.Size(101, 30);
            this.btnStartGrab.TabIndex = 26;
            this.btnStartGrab.Text = "开始采图";
            this.btnStartGrab.Click += new System.EventHandler(this.btnStartGrab_Click);
            // 
            // textMode
            // 
            this.textMode.EditValue = "";
            this.textMode.Location = new System.Drawing.Point(85, 40);
            this.textMode.MenuManager = this.barManager1;
            this.textMode.Name = "textMode";
            this.textMode.Properties.AutoHeight = false;
            this.textMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.textMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textMode.Properties.Items.AddRange(new object[] {
            "在线（实时检测）",
            "离线（仿真）"});
            this.textMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.textMode.Size = new System.Drawing.Size(161, 25);
            this.textMode.TabIndex = 2;
            this.textMode.SelectedIndexChanged += new System.EventHandler(this.textMode_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(31, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "运行模式";
            // 
            // groupRoll
            // 
            this.groupRoll.Controls.Add(this.xtraTabControlRoll);
            this.groupRoll.Location = new System.Drawing.Point(351, 26);
            this.groupRoll.Name = "groupRoll";
            this.groupRoll.Size = new System.Drawing.Size(284, 199);
            this.groupRoll.TabIndex = 6;
            this.groupRoll.Text = "膜卷记录";
            // 
            // xtraTabControlRoll
            // 
            this.xtraTabControlRoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlRoll.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.xtraTabControlRoll.Location = new System.Drawing.Point(2, 21);
            this.xtraTabControlRoll.Name = "xtraTabControlRoll";
            this.xtraTabControlRoll.SelectedTabPage = this.xtraTabPageRollOnline;
            this.xtraTabControlRoll.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControlRoll.Size = new System.Drawing.Size(280, 176);
            this.xtraTabControlRoll.TabIndex = 0;
            this.xtraTabControlRoll.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageRollOnline,
            this.xtraTabPageRollOffline});
            // 
            // xtraTabPageRollOnline
            // 
            this.xtraTabPageRollOnline.Controls.Add(this.btnRollSet);
            this.xtraTabPageRollOnline.Controls.Add(this.textRollType);
            this.xtraTabPageRollOnline.Controls.Add(this.textRollName);
            this.xtraTabPageRollOnline.Controls.Add(this.labelControl2);
            this.xtraTabPageRollOnline.Controls.Add(this.textRollRepeat);
            this.xtraTabPageRollOnline.Controls.Add(this.labelControl1);
            this.xtraTabPageRollOnline.Name = "xtraTabPageRollOnline";
            this.xtraTabPageRollOnline.Size = new System.Drawing.Size(274, 147);
            this.xtraTabPageRollOnline.Text = "在线";
            // 
            // btnRollSet
            // 
            this.btnRollSet.Location = new System.Drawing.Point(136, 110);
            this.btnRollSet.Name = "btnRollSet";
            this.btnRollSet.Size = new System.Drawing.Size(101, 30);
            this.btnRollSet.TabIndex = 6;
            this.btnRollSet.Text = "设置膜卷";
            this.btnRollSet.Click += new System.EventHandler(this.btnRollSet_Click);
            // 
            // textRollType
            // 
            this.textRollType.Location = new System.Drawing.Point(76, 17);
            this.textRollType.MenuManager = this.barManager1;
            this.textRollType.Name = "textRollType";
            this.textRollType.Properties.AutoHeight = false;
            this.textRollType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textRollType.Properties.Items.AddRange(new object[] {
            "4EAL"});
            this.textRollType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.textRollType.Size = new System.Drawing.Size(161, 25);
            this.textRollType.TabIndex = 4;
            // 
            // textRollName
            // 
            this.textRollName.Location = new System.Drawing.Point(76, 48);
            this.textRollName.MenuManager = this.barManager1;
            this.textRollName.Name = "textRollName";
            this.textRollName.Properties.AutoHeight = false;
            this.textRollName.Size = new System.Drawing.Size(161, 25);
            this.textRollName.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(34, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "膜卷号";
            // 
            // textRollRepeat
            // 
            this.textRollRepeat.Enabled = false;
            this.textRollRepeat.Location = new System.Drawing.Point(76, 79);
            this.textRollRepeat.MenuManager = this.barManager1;
            this.textRollRepeat.Name = "textRollRepeat";
            this.textRollRepeat.Properties.AutoHeight = false;
            this.textRollRepeat.Size = new System.Drawing.Size(161, 25);
            this.textRollRepeat.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "应用料号";
            // 
            // xtraTabPageRollOffline
            // 
            this.xtraTabPageRollOffline.Controls.Add(this.richTextBox1);
            this.xtraTabPageRollOffline.Name = "xtraTabPageRollOffline";
            this.xtraTabPageRollOffline.Size = new System.Drawing.Size(274, 147);
            this.xtraTabPageRollOffline.Text = "离线";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(274, 147);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage2.ImageIndex = 1;
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1142, 620);
            this.xtraTabPage2.Text = "检测界面";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel1.Controls.Add(this.tableLayoutPanel8);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1142, 620);
            this.splitContainerControl1.SplitterPosition = 588;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.groupStatuOuter, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.groupStatuInner, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(544, 616);
            this.tableLayoutPanel8.TabIndex = 29;
            // 
            // groupStatuOuter
            // 
            this.groupStatuOuter.Controls.Add(this.splitContainerOuter);
            this.groupStatuOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupStatuOuter.Location = new System.Drawing.Point(275, 3);
            this.groupStatuOuter.Name = "groupStatuOuter";
            this.groupStatuOuter.Size = new System.Drawing.Size(266, 610);
            this.groupStatuOuter.TabIndex = 1;
            this.groupStatuOuter.Text = "Camera2";
            this.groupStatuOuter.DoubleClick += new System.EventHandler(this.groupStatuOuter_DoubleClick);
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOuter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerOuter.IsSplitterFixed = true;
            this.splitContainerOuter.Location = new System.Drawing.Point(2, 21);
            this.splitContainerOuter.Name = "splitContainerOuter";
            this.splitContainerOuter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.tableLayoutPanel7);
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.hwinOuter);
            this.splitContainerOuter.Size = new System.Drawing.Size(262, 587);
            this.splitContainerOuter.SplitterDistance = 120;
            this.splitContainerOuter.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(262, 120);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this._lc_outer_frame, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelControl18, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this._lc_outer_isgrabbing, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelControl20, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this._lc_outer_isopen, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl22, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl23, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._lc_outer_camera, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(125, 114);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // _lc_outer_frame
            // 
            this._lc_outer_frame.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_frame.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_frame.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_frame.Location = new System.Drawing.Point(66, 88);
            this._lc_outer_frame.Name = "_lc_outer_frame";
            this._lc_outer_frame.Size = new System.Drawing.Size(55, 22);
            this._lc_outer_frame.TabIndex = 34;
            this._lc_outer_frame.Text = "-";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl18.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl18.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl18.Location = new System.Drawing.Point(4, 88);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(55, 22);
            this.labelControl18.TabIndex = 33;
            this.labelControl18.Text = "帧数";
            // 
            // _lc_outer_isgrabbing
            // 
            this._lc_outer_isgrabbing.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_isgrabbing.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_isgrabbing.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_isgrabbing.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_isgrabbing.Location = new System.Drawing.Point(66, 60);
            this._lc_outer_isgrabbing.Name = "_lc_outer_isgrabbing";
            this._lc_outer_isgrabbing.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_isgrabbing.TabIndex = 32;
            this._lc_outer_isgrabbing.Text = "-";
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl20.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl20.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl20.Location = new System.Drawing.Point(4, 60);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(55, 21);
            this.labelControl20.TabIndex = 31;
            this.labelControl20.Text = "采图状态";
            // 
            // _lc_outer_isopen
            // 
            this._lc_outer_isopen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_isopen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_isopen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_isopen.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_isopen.Location = new System.Drawing.Point(66, 32);
            this._lc_outer_isopen.Name = "_lc_outer_isopen";
            this._lc_outer_isopen.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_isopen.TabIndex = 30;
            this._lc_outer_isopen.Text = "-";
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl22.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl22.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl22.Location = new System.Drawing.Point(4, 32);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(55, 21);
            this.labelControl22.TabIndex = 28;
            this.labelControl22.Text = "开启状态";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl23.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl23.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl23.Location = new System.Drawing.Point(4, 4);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(55, 21);
            this.labelControl23.TabIndex = 26;
            this.labelControl23.Text = "相机名称";
            // 
            // _lc_outer_camera
            // 
            this._lc_outer_camera.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_camera.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_camera.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_camera.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_camera.Location = new System.Drawing.Point(66, 4);
            this._lc_outer_camera.Name = "_lc_outer_camera";
            this._lc_outer_camera.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_camera.TabIndex = 29;
            this._lc_outer_camera.Text = "-";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this._lc_outer_defectCount, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.labelControl11, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this._lc_outer_widthCount, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.labelControl15, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this._lc_outer_eaCount, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl19, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl21, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._lc_outer_caption, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(134, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(125, 114);
            this.tableLayoutPanel4.TabIndex = 27;
            // 
            // _lc_outer_defectCount
            // 
            this._lc_outer_defectCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_defectCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_defectCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_defectCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_defectCount.Location = new System.Drawing.Point(66, 88);
            this._lc_outer_defectCount.Name = "_lc_outer_defectCount";
            this._lc_outer_defectCount.Size = new System.Drawing.Size(55, 22);
            this._lc_outer_defectCount.TabIndex = 34;
            this._lc_outer_defectCount.Text = "-";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl11.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl11.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl11.Location = new System.Drawing.Point(4, 88);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(55, 22);
            this.labelControl11.TabIndex = 33;
            this.labelControl11.Text = "瑕疵NG数";
            // 
            // _lc_outer_widthCount
            // 
            this._lc_outer_widthCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_widthCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_widthCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_widthCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_widthCount.Location = new System.Drawing.Point(66, 60);
            this._lc_outer_widthCount.Name = "_lc_outer_widthCount";
            this._lc_outer_widthCount.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_widthCount.TabIndex = 32;
            this._lc_outer_widthCount.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl15.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl15.Location = new System.Drawing.Point(4, 60);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(55, 21);
            this.labelControl15.TabIndex = 31;
            this.labelControl15.Text = "幅宽NG数";
            // 
            // _lc_outer_eaCount
            // 
            this._lc_outer_eaCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_eaCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_eaCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_eaCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_eaCount.Location = new System.Drawing.Point(66, 32);
            this._lc_outer_eaCount.Name = "_lc_outer_eaCount";
            this._lc_outer_eaCount.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_eaCount.TabIndex = 30;
            this._lc_outer_eaCount.Text = "-";
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl19.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl19.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl19.Location = new System.Drawing.Point(4, 32);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(55, 21);
            this.labelControl19.TabIndex = 28;
            this.labelControl19.Text = "EA数";
            // 
            // labelControl21
            // 
            this.labelControl21.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl21.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl21.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl21.Location = new System.Drawing.Point(4, 4);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(55, 21);
            this.labelControl21.TabIndex = 26;
            this.labelControl21.Text = "检测位置";
            // 
            // _lc_outer_caption
            // 
            this._lc_outer_caption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_outer_caption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_outer_caption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_outer_caption.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_outer_caption.Location = new System.Drawing.Point(66, 4);
            this._lc_outer_caption.Name = "_lc_outer_caption";
            this._lc_outer_caption.Size = new System.Drawing.Size(55, 21);
            this._lc_outer_caption.TabIndex = 29;
            this._lc_outer_caption.Text = "-";
            // 
            // hwinOuter
            // 
            this.hwinOuter.BackColor = System.Drawing.Color.Black;
            this.hwinOuter.BorderColor = System.Drawing.Color.Black;
            this.hwinOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinOuter.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinOuter.Location = new System.Drawing.Point(0, 0);
            this.hwinOuter.Name = "hwinOuter";
            this.hwinOuter.Size = new System.Drawing.Size(262, 463);
            this.hwinOuter.TabIndex = 30;
            this.hwinOuter.WindowSize = new System.Drawing.Size(262, 463);
            // 
            // groupStatuInner
            // 
            this.groupStatuInner.Controls.Add(this.splitContainerInner);
            this.groupStatuInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupStatuInner.Location = new System.Drawing.Point(3, 3);
            this.groupStatuInner.Name = "groupStatuInner";
            this.groupStatuInner.Size = new System.Drawing.Size(266, 610);
            this.groupStatuInner.TabIndex = 0;
            this.groupStatuInner.Text = "Camera1";
            this.groupStatuInner.DoubleClick += new System.EventHandler(this.groupStatuInner_DoubleClick);
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerInner.IsSplitterFixed = true;
            this.splitContainerInner.Location = new System.Drawing.Point(2, 21);
            this.splitContainerInner.Name = "splitContainerInner";
            this.splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.Controls.Add(this.tableLayoutPanel5);
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.hwinInner);
            this.splitContainerInner.Size = new System.Drawing.Size(262, 587);
            this.splitContainerInner.SplitterDistance = 120;
            this.splitContainerInner.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(262, 120);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this._lc_inner_frame, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl12, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lc_inner_isgrabbing, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lc_inner_isopen, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lc_inner_camera, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(125, 114);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _lc_inner_frame
            // 
            this._lc_inner_frame.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_frame.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_frame.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_frame.Location = new System.Drawing.Point(66, 88);
            this._lc_inner_frame.Name = "_lc_inner_frame";
            this._lc_inner_frame.Size = new System.Drawing.Size(55, 22);
            this._lc_inner_frame.TabIndex = 34;
            this._lc_inner_frame.Text = "-";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl12.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl12.Location = new System.Drawing.Point(4, 88);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(55, 22);
            this.labelControl12.TabIndex = 33;
            this.labelControl12.Text = "帧数";
            // 
            // _lc_inner_isgrabbing
            // 
            this._lc_inner_isgrabbing.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_isgrabbing.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_isgrabbing.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_isgrabbing.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_isgrabbing.Location = new System.Drawing.Point(66, 60);
            this._lc_inner_isgrabbing.Name = "_lc_inner_isgrabbing";
            this._lc_inner_isgrabbing.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_isgrabbing.TabIndex = 32;
            this._lc_inner_isgrabbing.Text = "-";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl10.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl10.Location = new System.Drawing.Point(4, 60);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(55, 21);
            this.labelControl10.TabIndex = 31;
            this.labelControl10.Text = "采图状态";
            // 
            // _lc_inner_isopen
            // 
            this._lc_inner_isopen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_isopen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_isopen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_isopen.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_isopen.Location = new System.Drawing.Point(66, 32);
            this._lc_inner_isopen.Name = "_lc_inner_isopen";
            this._lc_inner_isopen.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_isopen.TabIndex = 30;
            this._lc_inner_isopen.Text = "-";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl7.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl7.Location = new System.Drawing.Point(4, 32);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(55, 21);
            this.labelControl7.TabIndex = 28;
            this.labelControl7.Text = "开启状态";
            // 
            // _lc_inner_camera
            // 
            this._lc_inner_camera.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_camera.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_camera.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_camera.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_camera.Location = new System.Drawing.Point(66, 4);
            this._lc_inner_camera.Name = "_lc_inner_camera";
            this._lc_inner_camera.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_camera.TabIndex = 29;
            this._lc_inner_camera.Text = "-";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl5.Location = new System.Drawing.Point(4, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 21);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "相机名称";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this._lc_inner_defectCount, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.labelControl8, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this._lc_inner_widthCount, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.labelControl37, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this._lc_inner_eaCount, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelControl39, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelControl40, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this._lc_inner_caption, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(134, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(125, 114);
            this.tableLayoutPanel6.TabIndex = 28;
            // 
            // _lc_inner_defectCount
            // 
            this._lc_inner_defectCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_defectCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_defectCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_defectCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_defectCount.Location = new System.Drawing.Point(66, 88);
            this._lc_inner_defectCount.Name = "_lc_inner_defectCount";
            this._lc_inner_defectCount.Size = new System.Drawing.Size(55, 22);
            this._lc_inner_defectCount.TabIndex = 34;
            this._lc_inner_defectCount.Text = "-";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl8.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl8.Location = new System.Drawing.Point(4, 88);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(55, 22);
            this.labelControl8.TabIndex = 33;
            this.labelControl8.Text = "瑕疵NG数";
            // 
            // _lc_inner_widthCount
            // 
            this._lc_inner_widthCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_widthCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_widthCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_widthCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_widthCount.Location = new System.Drawing.Point(66, 60);
            this._lc_inner_widthCount.Name = "_lc_inner_widthCount";
            this._lc_inner_widthCount.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_widthCount.TabIndex = 32;
            this._lc_inner_widthCount.Text = "-";
            // 
            // labelControl37
            // 
            this.labelControl37.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl37.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl37.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl37.Location = new System.Drawing.Point(4, 60);
            this.labelControl37.Name = "labelControl37";
            this.labelControl37.Size = new System.Drawing.Size(55, 21);
            this.labelControl37.TabIndex = 31;
            this.labelControl37.Text = "幅宽NG数";
            // 
            // _lc_inner_eaCount
            // 
            this._lc_inner_eaCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_eaCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_eaCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_eaCount.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_eaCount.Location = new System.Drawing.Point(66, 32);
            this._lc_inner_eaCount.Name = "_lc_inner_eaCount";
            this._lc_inner_eaCount.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_eaCount.TabIndex = 30;
            this._lc_inner_eaCount.Text = "-";
            // 
            // labelControl39
            // 
            this.labelControl39.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl39.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl39.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl39.Location = new System.Drawing.Point(4, 32);
            this.labelControl39.Name = "labelControl39";
            this.labelControl39.Size = new System.Drawing.Size(55, 21);
            this.labelControl39.TabIndex = 28;
            this.labelControl39.Text = "EA数";
            // 
            // labelControl40
            // 
            this.labelControl40.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl40.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl40.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl40.Location = new System.Drawing.Point(4, 4);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(55, 21);
            this.labelControl40.TabIndex = 26;
            this.labelControl40.Text = "检测位置";
            // 
            // _lc_inner_caption
            // 
            this._lc_inner_caption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lc_inner_caption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lc_inner_caption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lc_inner_caption.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this._lc_inner_caption.Location = new System.Drawing.Point(66, 4);
            this._lc_inner_caption.Name = "_lc_inner_caption";
            this._lc_inner_caption.Size = new System.Drawing.Size(55, 21);
            this._lc_inner_caption.TabIndex = 29;
            this._lc_inner_caption.Text = "-";
            // 
            // hwinInner
            // 
            this.hwinInner.BackColor = System.Drawing.Color.Black;
            this.hwinInner.BorderColor = System.Drawing.Color.Black;
            this.hwinInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwinInner.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwinInner.Location = new System.Drawing.Point(0, 0);
            this.hwinInner.Name = "hwinInner";
            this.hwinInner.Size = new System.Drawing.Size(262, 463);
            this.hwinInner.TabIndex = 29;
            this.hwinInner.WindowSize = new System.Drawing.Size(262, 463);
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage5;
            this.xtraTabControl2.Size = new System.Drawing.Size(584, 616);
            this.xtraTabControl2.TabIndex = 0;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage5,
            this.xtraTabPage6,
            this.xtraTabPage7});
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(578, 587);
            this.xtraTabPage5.Text = "极耳同步数据";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl2.Panel1.Controls.Add(this.panelTabMergeChart);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl2.Panel2.Controls.Add(this.panelTabMergeGrid);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(578, 587);
            this.splitContainerControl2.SplitterPosition = 424;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // panelTabMergeChart
            // 
            this.panelTabMergeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabMergeChart.Location = new System.Drawing.Point(0, 0);
            this.panelTabMergeChart.Name = "panelTabMergeChart";
            this.panelTabMergeChart.Size = new System.Drawing.Size(574, 420);
            this.panelTabMergeChart.TabIndex = 1;
            // 
            // panelTabMergeGrid
            // 
            this.panelTabMergeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabMergeGrid.Location = new System.Drawing.Point(0, 0);
            this.panelTabMergeGrid.Name = "panelTabMergeGrid";
            this.panelTabMergeGrid.Size = new System.Drawing.Size(574, 153);
            this.panelTabMergeGrid.TabIndex = 1;
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.tableLayoutPanel3);
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(578, 587);
            this.xtraTabPage6.Text = "贴标数据";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panelLabel2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panelLabel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(578, 587);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panelLabel2
            // 
            this.panelLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLabel2.Location = new System.Drawing.Point(3, 296);
            this.panelLabel2.Name = "panelLabel2";
            this.panelLabel2.Size = new System.Drawing.Size(572, 288);
            this.panelLabel2.TabIndex = 1;
            // 
            // panelLabel1
            // 
            this.panelLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLabel1.Location = new System.Drawing.Point(3, 3);
            this.panelLabel1.Name = "panelLabel1";
            this.panelLabel1.Size = new System.Drawing.Size(572, 287);
            this.panelLabel1.TabIndex = 0;
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.tableLayoutPanel9);
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(578, 587);
            this.xtraTabPage7.Text = "瑕疵数据";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.panelDefect2, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.panelDefect1, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(578, 587);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // panelDefect2
            // 
            this.panelDefect2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDefect2.Location = new System.Drawing.Point(3, 296);
            this.panelDefect2.Name = "panelDefect2";
            this.panelDefect2.Size = new System.Drawing.Size(572, 288);
            this.panelDefect2.TabIndex = 1;
            // 
            // panelDefect1
            // 
            this.panelDefect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDefect1.Location = new System.Drawing.Point(3, 3);
            this.panelDefect1.Name = "panelDefect1";
            this.panelDefect1.Size = new System.Drawing.Size(572, 287);
            this.panelDefect1.TabIndex = 1;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.groupLabel);
            this.xtraTabPage3.Controls.Add(this.groupCtrl);
            this.xtraTabPage3.Controls.Add(this.groupControl2);
            this.xtraTabPage3.Controls.Add(this.groupTest);
            this.xtraTabPage3.ImageIndex = 2;
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1142, 620);
            this.xtraTabPage3.Text = "参数配置";
            // 
            // groupLabel
            // 
            this.groupLabel.Controls.Add(this.labelControl17);
            this.groupLabel.Controls.Add(this.textLabelEAForce);
            this.groupLabel.Controls.Add(this.labelControl24);
            this.groupLabel.Controls.Add(this.checkEnableLabelEAForce);
            this.groupLabel.Controls.Add(this.labelControl14);
            this.groupLabel.Controls.Add(this.textLabelEAOffset);
            this.groupLabel.Controls.Add(this.labelControl16);
            this.groupLabel.Controls.Add(this.labelControl13);
            this.groupLabel.Controls.Add(this.textLabelDefectOffset);
            this.groupLabel.Controls.Add(this.labelControl9);
            this.groupLabel.Controls.Add(this.checkEnableLabelEAEveryOne);
            this.groupLabel.Controls.Add(this.checkEnableLabelEA);
            this.groupLabel.Controls.Add(this.checkEnableLabelDefect);
            this.groupLabel.Location = new System.Drawing.Point(33, 233);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(446, 171);
            this.groupLabel.TabIndex = 29;
            this.groupLabel.Text = "贴标设置";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(374, 71);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(20, 14);
            this.labelControl14.TabIndex = 14;
            this.labelControl14.Text = "mm";
            // 
            // textLabelEAOffset
            // 
            this.textLabelEAOffset.Location = new System.Drawing.Point(268, 68);
            this.textLabelEAOffset.MenuManager = this.barManager1;
            this.textLabelEAOffset.Name = "textLabelEAOffset";
            this.textLabelEAOffset.Size = new System.Drawing.Size(100, 20);
            this.textLabelEAOffset.TabIndex = 13;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(238, 71);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(24, 14);
            this.labelControl16.TabIndex = 12;
            this.labelControl16.Text = "偏移";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(374, 45);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(20, 14);
            this.labelControl13.TabIndex = 11;
            this.labelControl13.Text = "mm";
            // 
            // textLabelDefectOffset
            // 
            this.textLabelDefectOffset.Location = new System.Drawing.Point(268, 42);
            this.textLabelDefectOffset.MenuManager = this.barManager1;
            this.textLabelDefectOffset.Name = "textLabelDefectOffset";
            this.textLabelDefectOffset.Size = new System.Drawing.Size(100, 20);
            this.textLabelDefectOffset.TabIndex = 10;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(238, 45);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 9;
            this.labelControl9.Text = "偏移";
            // 
            // checkEnableLabelEAEveryOne
            // 
            this.checkEnableLabelEAEveryOne.Location = new System.Drawing.Point(49, 95);
            this.checkEnableLabelEAEveryOne.MenuManager = this.barManager1;
            this.checkEnableLabelEAEveryOne.Name = "checkEnableLabelEAEveryOne";
            this.checkEnableLabelEAEveryOne.Properties.Caption = "[测试]每个EA都贴标";
            this.checkEnableLabelEAEveryOne.Size = new System.Drawing.Size(147, 19);
            this.checkEnableLabelEAEveryOne.TabIndex = 8;
            // 
            // checkEnableLabelEA
            // 
            this.checkEnableLabelEA.Location = new System.Drawing.Point(22, 70);
            this.checkEnableLabelEA.MenuManager = this.barManager1;
            this.checkEnableLabelEA.Name = "checkEnableLabelEA";
            this.checkEnableLabelEA.Properties.Caption = "启用“EA末端贴标”";
            this.checkEnableLabelEA.Size = new System.Drawing.Size(160, 19);
            this.checkEnableLabelEA.TabIndex = 7;
            // 
            // checkEnableLabelDefect
            // 
            this.checkEnableLabelDefect.Location = new System.Drawing.Point(22, 43);
            this.checkEnableLabelDefect.MenuManager = this.barManager1;
            this.checkEnableLabelDefect.Name = "checkEnableLabelDefect";
            this.checkEnableLabelDefect.Properties.Caption = "启用“接头标签转标”";
            this.checkEnableLabelDefect.Size = new System.Drawing.Size(160, 19);
            this.checkEnableLabelDefect.TabIndex = 6;
            // 
            // groupCtrl
            // 
            this.groupCtrl.Controls.Add(this.btnOpenViewerChart);
            this.groupCtrl.Controls.Add(this.btnOfflineControl);
            this.groupCtrl.Location = new System.Drawing.Point(655, 26);
            this.groupCtrl.Name = "groupCtrl";
            this.groupCtrl.Size = new System.Drawing.Size(281, 142);
            this.groupCtrl.TabIndex = 28;
            this.groupCtrl.Text = "控制台";
            // 
            // btnOpenViewerChart
            // 
            this.btnOpenViewerChart.Location = new System.Drawing.Point(22, 44);
            this.btnOpenViewerChart.Name = "btnOpenViewerChart";
            this.btnOpenViewerChart.Size = new System.Drawing.Size(101, 30);
            this.btnOpenViewerChart.TabIndex = 18;
            this.btnOpenViewerChart.Text = "打开图表";
            this.btnOpenViewerChart.Click += new System.EventHandler(this.btnOpenViewerChart_Click);
            // 
            // btnOfflineControl
            // 
            this.btnOfflineControl.Location = new System.Drawing.Point(22, 80);
            this.btnOfflineControl.Name = "btnOfflineControl";
            this.btnOfflineControl.Size = new System.Drawing.Size(101, 30);
            this.btnOfflineControl.TabIndex = 19;
            this.btnOfflineControl.Text = "离线控制台";
            this.btnOfflineControl.Click += new System.EventHandler(this.btnOfflineControl_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.checkDisableView);
            this.groupControl2.Controls.Add(this.ckViewOuterBack);
            this.groupControl2.Controls.Add(this.ckViewOuterFront);
            this.groupControl2.Controls.Add(this.ckViewInnerBack);
            this.groupControl2.Controls.Add(this.ckViewInnerFront);
            this.groupControl2.Controls.Add(this.ckViewLocal);
            this.groupControl2.Location = new System.Drawing.Point(337, 26);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(298, 183);
            this.groupControl2.TabIndex = 31;
            this.groupControl2.Text = "同步视图";
            // 
            // checkDisableView
            // 
            this.checkDisableView.Location = new System.Drawing.Point(186, 44);
            this.checkDisableView.MenuManager = this.barManager1;
            this.checkDisableView.Name = "checkDisableView";
            this.checkDisableView.Properties.Caption = "禁用显示";
            this.checkDisableView.Size = new System.Drawing.Size(87, 19);
            this.checkDisableView.TabIndex = 11;
            // 
            // ckViewOuterBack
            // 
            this.ckViewOuterBack.EditValue = true;
            this.ckViewOuterBack.Location = new System.Drawing.Point(15, 144);
            this.ckViewOuterBack.MenuManager = this.barManager1;
            this.ckViewOuterBack.Name = "ckViewOuterBack";
            this.ckViewOuterBack.Properties.Caption = "Outer->Back";
            this.ckViewOuterBack.Size = new System.Drawing.Size(113, 19);
            this.ckViewOuterBack.TabIndex = 10;
            // 
            // ckViewOuterFront
            // 
            this.ckViewOuterFront.EditValue = true;
            this.ckViewOuterFront.Location = new System.Drawing.Point(15, 119);
            this.ckViewOuterFront.MenuManager = this.barManager1;
            this.ckViewOuterFront.Name = "ckViewOuterFront";
            this.ckViewOuterFront.Properties.Caption = "Outer->Front";
            this.ckViewOuterFront.Size = new System.Drawing.Size(113, 19);
            this.ckViewOuterFront.TabIndex = 9;
            // 
            // ckViewInnerBack
            // 
            this.ckViewInnerBack.EditValue = true;
            this.ckViewInnerBack.Location = new System.Drawing.Point(15, 94);
            this.ckViewInnerBack.MenuManager = this.barManager1;
            this.ckViewInnerBack.Name = "ckViewInnerBack";
            this.ckViewInnerBack.Properties.Caption = "Inner->Back";
            this.ckViewInnerBack.Size = new System.Drawing.Size(113, 19);
            this.ckViewInnerBack.TabIndex = 8;
            // 
            // ckViewInnerFront
            // 
            this.ckViewInnerFront.EditValue = true;
            this.ckViewInnerFront.Location = new System.Drawing.Point(15, 69);
            this.ckViewInnerFront.MenuManager = this.barManager1;
            this.ckViewInnerFront.Name = "ckViewInnerFront";
            this.ckViewInnerFront.Properties.Caption = "Inner->Front";
            this.ckViewInnerFront.Size = new System.Drawing.Size(113, 19);
            this.ckViewInnerFront.TabIndex = 7;
            // 
            // ckViewLocal
            // 
            this.ckViewLocal.EditValue = true;
            this.ckViewLocal.Location = new System.Drawing.Point(15, 44);
            this.ckViewLocal.MenuManager = this.barManager1;
            this.ckViewLocal.Name = "ckViewLocal";
            this.ckViewLocal.Properties.Caption = "本地同步";
            this.ckViewLocal.Size = new System.Drawing.Size(87, 19);
            this.ckViewLocal.TabIndex = 6;
            // 
            // groupTest
            // 
            this.groupTest.Controls.Add(this.checkDetectDefect);
            this.groupTest.Controls.Add(this.checkDetectMark);
            this.groupTest.Controls.Add(this.checkDetectWidth);
            this.groupTest.Controls.Add(this.checkDetectTab);
            this.groupTest.Controls.Add(this.checkSaveNG);
            this.groupTest.Controls.Add(this.checkSaveOK);
            this.groupTest.Location = new System.Drawing.Point(33, 26);
            this.groupTest.Name = "groupTest";
            this.groupTest.Size = new System.Drawing.Size(281, 183);
            this.groupTest.TabIndex = 30;
            this.groupTest.Text = "检测开关";
            // 
            // checkDetectDefect
            // 
            this.checkDetectDefect.Location = new System.Drawing.Point(154, 119);
            this.checkDetectDefect.MenuManager = this.barManager1;
            this.checkDetectDefect.Name = "checkDetectDefect";
            this.checkDetectDefect.Properties.Caption = "检测瑕疵";
            this.checkDetectDefect.Size = new System.Drawing.Size(87, 19);
            this.checkDetectDefect.TabIndex = 8;
            // 
            // checkDetectMark
            // 
            this.checkDetectMark.Location = new System.Drawing.Point(154, 94);
            this.checkDetectMark.MenuManager = this.barManager1;
            this.checkDetectMark.Name = "checkDetectMark";
            this.checkDetectMark.Properties.Caption = "检测Mark孔";
            this.checkDetectMark.Size = new System.Drawing.Size(87, 19);
            this.checkDetectMark.TabIndex = 7;
            // 
            // checkDetectWidth
            // 
            this.checkDetectWidth.Location = new System.Drawing.Point(154, 69);
            this.checkDetectWidth.MenuManager = this.barManager1;
            this.checkDetectWidth.Name = "checkDetectWidth";
            this.checkDetectWidth.Properties.Caption = "检测宽度";
            this.checkDetectWidth.Size = new System.Drawing.Size(87, 19);
            this.checkDetectWidth.TabIndex = 6;
            // 
            // checkDetectTab
            // 
            this.checkDetectTab.Location = new System.Drawing.Point(154, 44);
            this.checkDetectTab.MenuManager = this.barManager1;
            this.checkDetectTab.Name = "checkDetectTab";
            this.checkDetectTab.Properties.Caption = "检测极耳";
            this.checkDetectTab.Size = new System.Drawing.Size(87, 19);
            this.checkDetectTab.TabIndex = 5;
            // 
            // checkSaveNG
            // 
            this.checkSaveNG.Location = new System.Drawing.Point(22, 69);
            this.checkSaveNG.MenuManager = this.barManager1;
            this.checkSaveNG.Name = "checkSaveNG";
            this.checkSaveNG.Properties.Caption = "保存NG图";
            this.checkSaveNG.Size = new System.Drawing.Size(83, 19);
            this.checkSaveNG.TabIndex = 4;
            // 
            // checkSaveOK
            // 
            this.checkSaveOK.Location = new System.Drawing.Point(22, 44);
            this.checkSaveOK.MenuManager = this.barManager1;
            this.checkSaveOK.Name = "checkSaveOK";
            this.checkSaveOK.Properties.Caption = "保存OK图";
            this.checkSaveOK.Size = new System.Drawing.Size(87, 19);
            this.checkSaveOK.TabIndex = 1;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.ImageIndex = 3;
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1142, 620);
            this.xtraTabPage4.Text = "测试工具";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "配置文件|*.cfg|所有文件|*.*";
            this.saveFileDialog1.InitialDirectory = "config_package";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "膜卷记录|*.db";
            this.openFileDialog1.InitialDirectory = "config_package";
            // 
            // checkEnableLabelEAForce
            // 
            this.checkEnableLabelEAForce.Location = new System.Drawing.Point(49, 120);
            this.checkEnableLabelEAForce.MenuManager = this.barManager1;
            this.checkEnableLabelEAForce.Name = "checkEnableLabelEAForce";
            this.checkEnableLabelEAForce.Properties.Caption = "EA超长强制打标";
            this.checkEnableLabelEAForce.Size = new System.Drawing.Size(147, 19);
            this.checkEnableLabelEAForce.TabIndex = 15;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(374, 123);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(20, 14);
            this.labelControl17.TabIndex = 18;
            this.labelControl17.Text = "mm";
            // 
            // textLabelEAForce
            // 
            this.textLabelEAForce.Location = new System.Drawing.Point(268, 120);
            this.textLabelEAForce.MenuManager = this.barManager1;
            this.textLabelEAForce.Name = "textLabelEAForce";
            this.textLabelEAForce.Size = new System.Drawing.Size(100, 20);
            this.textLabelEAForce.TabIndex = 17;
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(223, 123);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(39, 14);
            this.labelControl24.TabIndex = 16;
            this.labelControl24.Text = "EA长度";
            // 
            // XMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 662);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "XMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CATL 分条机视觉检测系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XFMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTab)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupRemoteClient)).EndInit();
            this.groupRemoteClient.ResumeLayout(false);
            this.groupRemoteClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupRoll)).EndInit();
            this.groupRoll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlRoll)).EndInit();
            this.xtraTabControlRoll.ResumeLayout(false);
            this.xtraTabPageRollOnline.ResumeLayout(false);
            this.xtraTabPageRollOnline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textRollType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRollName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRollRepeat.Properties)).EndInit();
            this.xtraTabPageRollOffline.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupStatuOuter)).EndInit();
            this.groupStatuOuter.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupStatuInner)).EndInit();
            this.groupStatuInner.ResumeLayout(false);
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupLabel)).EndInit();
            this.groupLabel.ResumeLayout(false);
            this.groupLabel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelEAOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelDefectOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEAEveryOne.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelDefect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrl)).EndInit();
            this.groupCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkDisableView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewOuterBack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewOuterFront.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewInnerBack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewInnerFront.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckViewLocal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTest)).EndInit();
            this.groupTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectDefect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDetectTab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSaveNG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSaveOK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEnableLabelEAForce.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textLabelEAForce.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem status_device;
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
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.SkinBarSubItem selectSkin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraBars.BarButtonItem selectFullScreen;
        private DevExpress.Utils.ImageCollection imageCollectionTab;
        private DevExpress.Utils.ImageCollection imageCollectionStatus;
        private DevExpress.XtraBars.BarStaticItem status_time;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem status_diskspace;
        private DevExpress.XtraBars.BarStaticItem status_memory;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit textRollName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit textRollType;
        private DevExpress.XtraEditors.TextEdit textRollRepeat;
        public DevExpress.XtraEditors.ComboBoxEdit textMode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupRoll;
        private DevExpress.XtraEditors.SimpleButton btnRollSet;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.LabelControl _lc_outer_defectCount;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl _lc_outer_widthCount;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl _lc_outer_eaCount;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl _lc_outer_caption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraEditors.LabelControl _lc_inner_defectCount;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl _lc_inner_widthCount;
        private DevExpress.XtraEditors.LabelControl labelControl37;
        private DevExpress.XtraEditors.LabelControl _lc_inner_eaCount;
        private DevExpress.XtraEditors.LabelControl labelControl39;
        private DevExpress.XtraEditors.LabelControl labelControl40;
        private DevExpress.XtraEditors.LabelControl _lc_inner_caption;
        private HalconDotNet.HWindowControl hwinOuter;
        private HalconDotNet.HWindowControl hwinInner;
        private DevExpress.XtraEditors.SimpleButton btnOpenViewerChart;
        private DevExpress.XtraEditors.SimpleButton btnOfflineControl;
        private DevExpress.XtraEditors.SimpleButton btnStopGrab;
        private DevExpress.XtraEditors.SimpleButton btnStartGrab;
        private DevExpress.XtraEditors.SimpleButton btnQuit;
        private DevExpress.XtraEditors.GroupControl groupStatuOuter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.LabelControl _lc_outer_frame;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl _lc_outer_isgrabbing;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl _lc_outer_isopen;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.GroupControl groupStatuInner;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl _lc_inner_isgrabbing;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl _lc_inner_isopen;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.LabelControl _lc_outer_camera;
        private DevExpress.XtraEditors.LabelControl _lc_inner_camera;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.SplitContainer splitContainerOuter;
        private System.Windows.Forms.SplitContainer splitContainerInner;
        private DevExpress.XtraEditors.SimpleButton btnDisconnect;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlRoll;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRollOnline;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRollOffline;
        private DevExpress.XtraEditors.GroupControl groupCtrl;
        private DevExpress.XtraEditors.GroupControl groupRemoteClient;
        private DevExpress.XtraEditors.LabelControl _lc_remote_plc;
        private DevExpress.XtraEditors.LabelControl _lc_remote_8k;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnConnectRemotePLC;
        private DevExpress.XtraEditors.SimpleButton btnConnectRemote8K;
        private DevExpress.XtraEditors.LabelControl _lc_inner_frame;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit checkDisableView;
        private DevExpress.XtraEditors.CheckEdit ckViewOuterBack;
        private DevExpress.XtraEditors.CheckEdit ckViewOuterFront;
        private DevExpress.XtraEditors.CheckEdit ckViewInnerBack;
        private DevExpress.XtraEditors.CheckEdit ckViewInnerFront;
        private DevExpress.XtraEditors.CheckEdit ckViewLocal;
        private DevExpress.XtraEditors.GroupControl groupTest;
        private DevExpress.XtraEditors.CheckEdit checkDetectDefect;
        private DevExpress.XtraEditors.CheckEdit checkDetectMark;
        private DevExpress.XtraEditors.CheckEdit checkDetectWidth;
        private DevExpress.XtraEditors.CheckEdit checkDetectTab;
        private DevExpress.XtraEditors.CheckEdit checkSaveNG;
        private DevExpress.XtraEditors.CheckEdit checkSaveOK;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Panel panelTabMergeChart;
        private System.Windows.Forms.Panel panelTabMergeGrid;
        private System.Windows.Forms.Panel panelLabel2;
        private System.Windows.Forms.Panel panelLabel1;
        private System.Windows.Forms.Panel panelDefect2;
        private System.Windows.Forms.Panel panelDefect1;
        private DevExpress.XtraEditors.GroupControl groupLabel;
        private DevExpress.XtraEditors.CheckEdit checkEnableLabelEA;
        private DevExpress.XtraEditors.CheckEdit checkEnableLabelDefect;
        private DevExpress.XtraEditors.CheckEdit checkEnableLabelEAEveryOne;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit textLabelEAOffset;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit textLabelDefectOffset;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit textLabelEAForce;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.CheckEdit checkEnableLabelEAForce;
    }
}