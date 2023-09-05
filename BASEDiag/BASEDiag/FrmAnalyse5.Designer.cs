namespace BASEDiag
{
    partial class FrmAnalyse5
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
            System.Windows.Forms.ImageList Bigimgs;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse5));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFormeU = new System.Windows.Forms.RadioButton();
            this.rbFormeV = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkbxFreinLingual = new System.Windows.Forms.CheckBox();
            this.chkBxDDD = new System.Windows.Forms.CheckBox();
            this.chkbxDDM = new System.Windows.Forms.CheckBox();
            this.chkbxFreinLabial = new System.Windows.Forms.CheckBox();
            this.chkbxLangueBas = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSurplomb = new ControlsLibrary.TreeViewIconCbx();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkbxInterposPos = new System.Windows.Forms.CheckBox();
            this.chkbxInterposAnt = new System.Windows.Forms.CheckBox();
            this.tabcontrol1 = new System.Windows.Forms.TabControl();
            this.tabTriImg = new System.Windows.Forms.TabPage();
            this.TriImg3 = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.TriImg2 = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.TriImg1 = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.lblTitre = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRisque = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.pnlFullScreen = new System.Windows.Forms.Panel();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabcontrol1.SuspendLayout();
            this.tabTriImg.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Bigimgs
            // 
            Bigimgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Bigimgs.ImageStream")));
            Bigimgs.TransparentColor = System.Drawing.Color.Transparent;
            Bigimgs.Images.SetKeyName(0, "mediumPPT.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbFormeU);
            this.groupBox1.Controls.Add(this.rbFormeV);
            this.groupBox1.Location = new System.Drawing.Point(487, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forme de l\'arcade";
            // 
            // rbFormeU
            // 
            this.rbFormeU.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFormeU.AutoSize = true;
            this.rbFormeU.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbFormeU.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbFormeU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbFormeU.Image = global::BASEDiag.Properties.Resources.FormeEnU;
            this.rbFormeU.Location = new System.Drawing.Point(6, 11);
            this.rbFormeU.Name = "rbFormeU";
            this.rbFormeU.Size = new System.Drawing.Size(73, 73);
            this.rbFormeU.TabIndex = 15;
            this.rbFormeU.TabStop = true;
            this.rbFormeU.UseVisualStyleBackColor = true;
            this.rbFormeU.Click += new System.EventHandler(this.rbPosterieur_CheckedChanged);
            this.rbFormeU.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // rbFormeV
            // 
            this.rbFormeV.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFormeV.AutoSize = true;
            this.rbFormeV.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbFormeV.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbFormeV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbFormeV.Image = global::BASEDiag.Properties.Resources.FormeEnV;
            this.rbFormeV.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbFormeV.Location = new System.Drawing.Point(80, 11);
            this.rbFormeV.Name = "rbFormeV";
            this.rbFormeV.Size = new System.Drawing.Size(73, 73);
            this.rbFormeV.TabIndex = 14;
            this.rbFormeV.TabStop = true;
            this.rbFormeV.UseVisualStyleBackColor = true;
            this.rbFormeV.Click += new System.EventHandler(this.rbPosterieur_CheckedChanged);
            this.rbFormeV.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkbxFreinLingual);
            this.groupBox2.Controls.Add(this.chkBxDDD);
            this.groupBox2.Controls.Add(this.chkbxDDM);
            this.groupBox2.Controls.Add(this.chkbxFreinLabial);
            this.groupBox2.Controls.Add(this.chkbxLangueBas);
            this.groupBox2.Location = new System.Drawing.Point(487, 377);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 176);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Divers";
            // 
            // chkbxFreinLingual
            // 
            this.chkbxFreinLingual.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxFreinLingual.AutoSize = true;
            this.chkbxFreinLingual.Checked = true;
            this.chkbxFreinLingual.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxFreinLingual.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxFreinLingual.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxFreinLingual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxFreinLingual.Image = global::BASEDiag.Properties.Resources.FreinLingual;
            this.chkbxFreinLingual.Location = new System.Drawing.Point(94, 13);
            this.chkbxFreinLingual.Name = "chkbxFreinLingual";
            this.chkbxFreinLingual.Size = new System.Drawing.Size(71, 71);
            this.chkbxFreinLingual.TabIndex = 7;
            this.chkbxFreinLingual.UseVisualStyleBackColor = true;
            this.chkbxFreinLingual.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            this.chkbxFreinLingual.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxDDM_MouseDown);
            this.chkbxFreinLingual.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxLangueBas_MouseUp);
            // 
            // chkBxDDD
            // 
            this.chkBxDDD.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBxDDD.AutoSize = true;
            this.chkBxDDD.Checked = true;
            this.chkBxDDD.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkBxDDD.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkBxDDD.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkBxDDD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBxDDD.Image = global::BASEDiag.Properties.Resources.DDD;
            this.chkBxDDD.Location = new System.Drawing.Point(94, 85);
            this.chkBxDDD.Name = "chkBxDDD";
            this.chkBxDDD.Size = new System.Drawing.Size(71, 71);
            this.chkBxDDD.TabIndex = 6;
            this.chkBxDDD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkBxDDD.UseVisualStyleBackColor = true;
            this.chkBxDDD.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            this.chkBxDDD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxDDM_MouseDown);
            this.chkBxDDD.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxLangueBas_MouseUp);
            // 
            // chkbxDDM
            // 
            this.chkbxDDM.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxDDM.AutoSize = true;
            this.chkbxDDM.Checked = true;
            this.chkbxDDM.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxDDM.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxDDM.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxDDM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxDDM.Image = global::BASEDiag.Properties.Resources.DDM;
            this.chkbxDDM.Location = new System.Drawing.Point(22, 85);
            this.chkbxDDM.Name = "chkbxDDM";
            this.chkbxDDM.Size = new System.Drawing.Size(71, 71);
            this.chkbxDDM.TabIndex = 5;
            this.chkbxDDM.UseVisualStyleBackColor = true;
            this.chkbxDDM.CheckedChanged += new System.EventHandler(this.chkbxDDM_CheckedChanged);
            this.chkbxDDM.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            this.chkbxDDM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxDDM_MouseDown);
            this.chkbxDDM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxLangueBas_MouseUp);
            // 
            // chkbxFreinLabial
            // 
            this.chkbxFreinLabial.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxFreinLabial.AutoSize = true;
            this.chkbxFreinLabial.Checked = true;
            this.chkbxFreinLabial.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxFreinLabial.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxFreinLabial.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxFreinLabial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxFreinLabial.Image = global::BASEDiag.Properties.Resources.FreinLabial;
            this.chkbxFreinLabial.Location = new System.Drawing.Point(22, 13);
            this.chkbxFreinLabial.Name = "chkbxFreinLabial";
            this.chkbxFreinLabial.Size = new System.Drawing.Size(71, 71);
            this.chkbxFreinLabial.TabIndex = 4;
            this.chkbxFreinLabial.UseVisualStyleBackColor = true;
            this.chkbxFreinLabial.CheckedChanged += new System.EventHandler(this.chkbxFreinLabial_CheckedChanged);
            this.chkbxFreinLabial.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            this.chkbxFreinLabial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxDDM_MouseDown);
            this.chkbxFreinLabial.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxLangueBas_MouseUp);
            // 
            // chkbxLangueBas
            // 
            this.chkbxLangueBas.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxLangueBas.AutoSize = true;
            this.chkbxLangueBas.Checked = true;
            this.chkbxLangueBas.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxLangueBas.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxLangueBas.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxLangueBas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxLangueBas.Image = global::BASEDiag.Properties.Resources.LangueBas;
            this.chkbxLangueBas.Location = new System.Drawing.Point(168, 13);
            this.chkbxLangueBas.Name = "chkbxLangueBas";
            this.chkbxLangueBas.Size = new System.Drawing.Size(71, 71);
            this.chkbxLangueBas.TabIndex = 3;
            this.chkbxLangueBas.UseVisualStyleBackColor = true;
            this.chkbxLangueBas.CheckedChanged += new System.EventHandler(this.chkbxLangueBas_CheckedChanged);
            this.chkbxLangueBas.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            this.chkbxLangueBas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxDDM_MouseDown);
            this.chkbxLangueBas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxLangueBas_MouseUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbSurplomb);
            this.groupBox3.Location = new System.Drawing.Point(487, 559);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 85);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Valeur du Surplomb";
            // 
            // tbSurplomb
            // 
            this.tbSurplomb.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbSurplomb.Location = new System.Drawing.Point(22, 19);
            this.tbSurplomb.Name = "tbSurplomb";
            this.tbSurplomb.SelectedIndex = -1;
            this.tbSurplomb.SelectedItem = null;
            this.tbSurplomb.Size = new System.Drawing.Size(277, 52);
            this.tbSurplomb.TabIndex = 8;
            this.tbSurplomb.VisibleItems = 12;
            this.tbSurplomb.SelectedIndexChanged += new System.EventHandler(this.tbSurplomb_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkbxInterposPos);
            this.groupBox4.Controls.Add(this.chkbxInterposAnt);
            this.groupBox4.Location = new System.Drawing.Point(655, 286);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(158, 85);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Interposition Lingual";
            // 
            // chkbxInterposPos
            // 
            this.chkbxInterposPos.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxInterposPos.AutoSize = true;
            this.chkbxInterposPos.Checked = true;
            this.chkbxInterposPos.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxInterposPos.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxInterposPos.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxInterposPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxInterposPos.Image = global::BASEDiag.Properties.Resources.InterposLingPost;
            this.chkbxInterposPos.Location = new System.Drawing.Point(78, 13);
            this.chkbxInterposPos.Name = "chkbxInterposPos";
            this.chkbxInterposPos.Size = new System.Drawing.Size(71, 71);
            this.chkbxInterposPos.TabIndex = 17;
            this.chkbxInterposPos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkbxInterposPos.UseVisualStyleBackColor = true;
            this.chkbxInterposPos.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.chkbxInterposPos.Click += new System.EventHandler(this.chkbxInterposPos_Click);
            this.chkbxInterposPos.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // chkbxInterposAnt
            // 
            this.chkbxInterposAnt.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxInterposAnt.AutoSize = true;
            this.chkbxInterposAnt.Checked = true;
            this.chkbxInterposAnt.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxInterposAnt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxInterposAnt.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxInterposAnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxInterposAnt.Image = global::BASEDiag.Properties.Resources.InterposLingAnt;
            this.chkbxInterposAnt.Location = new System.Drawing.Point(6, 13);
            this.chkbxInterposAnt.Name = "chkbxInterposAnt";
            this.chkbxInterposAnt.Size = new System.Drawing.Size(71, 71);
            this.chkbxInterposAnt.TabIndex = 16;
            this.chkbxInterposAnt.UseVisualStyleBackColor = true;
            this.chkbxInterposAnt.CheckedChanged += new System.EventHandler(this.chkbxInterposAnt_CheckedChanged);
            this.chkbxInterposAnt.Click += new System.EventHandler(this.chkbxInterposAnt_Click);
            this.chkbxInterposAnt.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // tabcontrol1
            // 
            this.tabcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcontrol1.Controls.Add(this.tabTriImg);
            this.tabcontrol1.ItemSize = new System.Drawing.Size(53, 32);
            this.tabcontrol1.Location = new System.Drawing.Point(12, 100);
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new System.Drawing.Size(469, 487);
            this.tabcontrol1.TabIndex = 8;
            // 
            // tabTriImg
            // 
            this.tabTriImg.Controls.Add(this.TriImg3);
            this.tabTriImg.Controls.Add(this.TriImg2);
            this.tabTriImg.Controls.Add(this.TriImg1);
            this.tabTriImg.Location = new System.Drawing.Point(4, 36);
            this.tabTriImg.Name = "tabTriImg";
            this.tabTriImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriImg.Size = new System.Drawing.Size(461, 447);
            this.tabTriImg.TabIndex = 3;
            this.tabTriImg.Text = "Toutes";
            this.tabTriImg.UseVisualStyleBackColor = true;
            this.tabTriImg.Click += new System.EventHandler(this.tabTriImg_Click);
            // 
            // TriImg3
            // 
            this.TriImg3.AllowDrop = true;
            this.TriImg3.AngleDeRotationPhoto = 0F;
            this.TriImg3.AngleDeRotationRadio = 0F;
            this.TriImg3.Brightness = 0F;
            this.TriImg3.Contraste = 1F;
            this.TriImg3.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.TriImg3.DrawPointName = false;
            this.TriImg3.file = null;
            this.TriImg3.HelpFolder = ".\\";
            this.TriImg3.HelpImage = null;
            this.TriImg3.Location = new System.Drawing.Point(341, 6);
            this.TriImg3.Name = "TriImg3";
            this.TriImg3.PhotoImage = null;
            this.TriImg3.RadioImage = null;
            this.TriImg3.ReadOnly = false;
            this.TriImg3.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.TriImg3.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.TriImg3.Size = new System.Drawing.Size(99, 222);
            this.TriImg3.Synchronized = false;
            this.TriImg3.TabIndex = 2;
            this.TriImg3.TextIfNoImage = "Pas d\'image";
            this.TriImg3.Transparence = 0.99D;
            this.TriImg3.zoomPhoto = 1F;
            this.TriImg3.zoomRadio = 1F;
            this.TriImg3.OnRadioChanged += new System.EventHandler(this.TriImg3_OnRadioChanged);
            this.TriImg3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TriImg1_MouseDoubleClick);
            // 
            // TriImg2
            // 
            this.TriImg2.AllowDrop = true;
            this.TriImg2.AngleDeRotationPhoto = 0F;
            this.TriImg2.AngleDeRotationRadio = 0F;
            this.TriImg2.Brightness = 0F;
            this.TriImg2.Contraste = 1F;
            this.TriImg2.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.TriImg2.DrawPointName = false;
            this.TriImg2.file = null;
            this.TriImg2.HelpFolder = ".\\";
            this.TriImg2.HelpImage = null;
            this.TriImg2.Location = new System.Drawing.Point(160, 6);
            this.TriImg2.Name = "TriImg2";
            this.TriImg2.PhotoImage = null;
            this.TriImg2.RadioImage = null;
            this.TriImg2.ReadOnly = false;
            this.TriImg2.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.TriImg2.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.TriImg2.Size = new System.Drawing.Size(99, 222);
            this.TriImg2.Synchronized = false;
            this.TriImg2.TabIndex = 1;
            this.TriImg2.TextIfNoImage = "Pas d\'image";
            this.TriImg2.Transparence = 0.99D;
            this.TriImg2.zoomPhoto = 1F;
            this.TriImg2.zoomRadio = 1F;
            this.TriImg2.OnRadioChanged += new System.EventHandler(this.TriImg2_OnRadioChanged);
            this.TriImg2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TriImg1_MouseDoubleClick);
            // 
            // TriImg1
            // 
            this.TriImg1.AllowDrop = true;
            this.TriImg1.AngleDeRotationPhoto = 0F;
            this.TriImg1.AngleDeRotationRadio = 0F;
            this.TriImg1.Brightness = 0F;
            this.TriImg1.Contraste = 1F;
            this.TriImg1.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.TriImg1.DrawPointName = false;
            this.TriImg1.file = null;
            this.TriImg1.HelpFolder = ".\\";
            this.TriImg1.HelpImage = null;
            this.TriImg1.Location = new System.Drawing.Point(6, 6);
            this.TriImg1.Name = "TriImg1";
            this.TriImg1.PhotoImage = null;
            this.TriImg1.RadioImage = null;
            this.TriImg1.ReadOnly = false;
            this.TriImg1.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.TriImg1.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.TriImg1.Size = new System.Drawing.Size(99, 222);
            this.TriImg1.Synchronized = false;
            this.TriImg1.TabIndex = 0;
            this.TriImg1.TextIfNoImage = "Pas d\'image";
            this.TriImg1.Transparence = 0.99D;
            this.TriImg1.zoomPhoto = 1F;
            this.TriImg1.zoomRadio = 1F;
            this.TriImg1.OnRadioChanged += new System.EventHandler(this.TriImg1_OnRadioChanged);
            this.TriImg1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TriImg1_MouseDoubleClick);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(68, 593);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(245, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 16;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // SmallImg
            // 
            this.SmallImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImg.ImageStream")));
            this.SmallImg.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImg.Images.SetKeyName(0, "VerySmallPPT.png");
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(9, 61);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(472, 23);
            this.lblTitre.TabIndex = 20;
            this.lblTitre.Text = "Analyse des arcades";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(487, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 180);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 84);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 93);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 84);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiag.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(763, 4);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 9;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 136);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiag.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(319, 594);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 21;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Image = global::BASEDiag.Properties.Resources.Imprimer;
            this.BtnPrint.Location = new System.Drawing.Point(375, 593);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(50, 50);
            this.BtnPrint.TabIndex = 18;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(707, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 17;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiag.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 593);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 15;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiag.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(431, 593);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 14;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // pnlFullScreen
            // 
            this.pnlFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFullScreen.Location = new System.Drawing.Point(12, 87);
            this.pnlFullScreen.Name = "pnlFullScreen";
            this.pnlFullScreen.Size = new System.Drawing.Size(475, 501);
            this.pnlFullScreen.TabIndex = 24;
            this.pnlFullScreen.Visible = false;
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 4);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(689, 50);
            this.barrePatient1.TabIndex = 1;
            // 
            // FrmAnalyse5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(825, 656);
            this.Controls.Add(this.pnlFullScreen);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.tabcontrol1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barrePatient1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse5";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analyse des arcades";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse4_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse4_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabcontrol1.ResumeLayout(false);
            this.tabTriImg.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BASEDiag.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFormeU;
        private System.Windows.Forms.RadioButton rbFormeV;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBxDDD;
        private System.Windows.Forms.CheckBox chkbxDDM;
        private System.Windows.Forms.CheckBox chkbxFreinLabial;
        private System.Windows.Forms.CheckBox chkbxLangueBas;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabcontrol1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TabPage tabTriImg;
        private BASEDiag.Ctrls.ImageCtrlAgg TriImg3;
        private BASEDiag.Ctrls.ImageCtrlAgg TriImg2;
        private BASEDiag.Ctrls.ImageCtrlAgg TriImg1;
        private System.Windows.Forms.CheckBox chkbxInterposPos;
        private System.Windows.Forms.CheckBox chkbxInterposAnt;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.CheckBox chkbxFreinLingual;
        private System.Windows.Forms.Label lblTitre;
        private ControlsLibrary.TreeViewIconCbx tbSurplomb;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Panel pnlFullScreen;
    }
}