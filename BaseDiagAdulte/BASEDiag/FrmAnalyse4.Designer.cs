namespace BASEDiagAdulte
{
    partial class FrmAnalyse4
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ImageList Bigimgs;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse4));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.pnlFullScreen = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkbxLaterolDeviationFonctionnel = new System.Windows.Forms.RadioButton();
            this.chkbxLaterodeviationRC = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tendbucc = new System.Windows.Forms.RadioButton();
            this.buccexc = new System.Windows.Forms.RadioButton();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkbxInterposAnt = new System.Windows.Forms.RadioButton();
            this.chkbxInterposPos = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTriImg = new System.Windows.Forms.TabPage();
            this.TriImg1 = new BASEDiagAdulte.Ctrls.ImageCtrlAgg();
            this.TriImg2 = new BASEDiagAdulte.Ctrls.ImageCtrlAgg();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnRisque = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTriImg.SuspendLayout();
            this.SuspendLayout();
            // 
            // Bigimgs
            // 
            Bigimgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Bigimgs.ImageStream")));
            Bigimgs.TransparentColor = System.Drawing.Color.Transparent;
            Bigimgs.Images.SetKeyName(0, "mediumPPT.png");
            // 
            // SmallImg
            // 
            this.SmallImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImg.ImageStream")));
            this.SmallImg.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImg.Images.SetKeyName(0, "VerySmallPPT.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, -19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 18);
            this.label1.TabIndex = 43;
            this.label1.Text = "Décalage Inter-Incisive";
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 157);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 149);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            this.lstBxObjectifs.SelectedIndexChanged += new System.EventHandler(this.lstBxObjectifs_SelectedIndexChanged);
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 148);
            this.lstBxDiag.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(592, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 309);
            this.tableLayoutPanel1.TabIndex = 54;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(4, 36);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(578, 23);
            this.lblTitre.TabIndex = 52;
            this.lblTitre.Text = "Fonctionnel";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFullScreen
            // 
            this.pnlFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFullScreen.Location = new System.Drawing.Point(8, 36);
            this.pnlFullScreen.Name = "pnlFullScreen";
            this.pnlFullScreen.Size = new System.Drawing.Size(93, 38);
            this.pnlFullScreen.TabIndex = 44;
            this.pnlFullScreen.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkbxLaterolDeviationFonctionnel);
            this.groupBox2.Controls.Add(this.chkbxLaterodeviationRC);
            this.groupBox2.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(632, 441);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 122);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Diagnostique Fonctionnel";
            // 
            // chkbxLaterolDeviationFonctionnel
            // 
            this.chkbxLaterolDeviationFonctionnel.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxLaterolDeviationFonctionnel.AutoSize = true;
            this.chkbxLaterolDeviationFonctionnel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxLaterolDeviationFonctionnel.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxLaterolDeviationFonctionnel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxLaterolDeviationFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.latero;
            this.chkbxLaterolDeviationFonctionnel.Location = new System.Drawing.Point(6, 35);
            this.chkbxLaterolDeviationFonctionnel.Name = "chkbxLaterolDeviationFonctionnel";
            this.chkbxLaterolDeviationFonctionnel.Size = new System.Drawing.Size(73, 73);
            this.chkbxLaterolDeviationFonctionnel.TabIndex = 18;
            this.chkbxLaterolDeviationFonctionnel.UseVisualStyleBackColor = true;
            this.chkbxLaterolDeviationFonctionnel.CheckedChanged += new System.EventHandler(this.chkbxLaterolDeviationFonctionnel_CheckedChanged);
            this.chkbxLaterolDeviationFonctionnel.Click += new System.EventHandler(this.chkbxLaterolDeviationFonctionnel_Click);
            this.chkbxLaterolDeviationFonctionnel.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // chkbxLaterodeviationRC
            // 
            this.chkbxLaterodeviationRC.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxLaterodeviationRC.AutoSize = true;
            this.chkbxLaterodeviationRC.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxLaterodeviationRC.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxLaterodeviationRC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxLaterodeviationRC.Image = global::BASEDiagAdulte.Properties.Resources.NONcorres;
            this.chkbxLaterodeviationRC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkbxLaterodeviationRC.Location = new System.Drawing.Point(155, 35);
            this.chkbxLaterodeviationRC.Name = "chkbxLaterodeviationRC";
            this.chkbxLaterodeviationRC.Size = new System.Drawing.Size(73, 73);
            this.chkbxLaterodeviationRC.TabIndex = 19;
            this.chkbxLaterodeviationRC.UseVisualStyleBackColor = true;
            this.chkbxLaterodeviationRC.CheckedChanged += new System.EventHandler(this.chkbxLaterodeviationRC_CheckedChanged);
            this.chkbxLaterodeviationRC.Click += new System.EventHandler(this.chkbxLaterodeviationRC_Click);
            this.chkbxLaterodeviationRC.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.tendbucc);
            this.groupBox1.Controls.Add(this.buccexc);
            this.groupBox1.Location = new System.Drawing.Point(6, 380);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 95);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Respiration Buccale";
            // 
            // tendbucc
            // 
            this.tendbucc.Appearance = System.Windows.Forms.Appearance.Button;
            this.tendbucc.Checked = true;
            this.tendbucc.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.tendbucc.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.tendbucc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tendbucc.Image = global::BASEDiagAdulte.Properties.Resources.RespBucc1;
            this.tendbucc.Location = new System.Drawing.Point(6, 21);
            this.tendbucc.Name = "tendbucc";
            this.tendbucc.Size = new System.Drawing.Size(73, 73);
            this.tendbucc.TabIndex = 13;
            this.tendbucc.TabStop = true;
            this.tendbucc.UseVisualStyleBackColor = true;
            this.tendbucc.CheckedChanged += new System.EventHandler(this.tendbuccale__CheckedChanged);
            this.tendbucc.Click += new System.EventHandler(this.tendbuccale_Click);
            this.tendbucc.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // buccexc
            // 
            this.buccexc.Appearance = System.Windows.Forms.Appearance.Button;
            this.buccexc.AutoSize = true;
            this.buccexc.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buccexc.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.buccexc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buccexc.Image = global::BASEDiagAdulte.Properties.Resources.RespExc;
            this.buccexc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buccexc.Location = new System.Drawing.Point(159, 21);
            this.buccexc.Name = "buccexc";
            this.buccexc.Size = new System.Drawing.Size(73, 73);
            this.buccexc.TabIndex = 12;
            this.buccexc.UseVisualStyleBackColor = true;
            this.buccexc.CheckedChanged += new System.EventHandler(this.buccexc_CheckedChanged);
            this.buccexc.Click += new System.EventHandler(this.buccexc_Click);
            this.buccexc.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
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
            this.barrePatient1.Size = new System.Drawing.Size(782, 50);
            this.barrePatient1.TabIndex = 1;
            this.barrePatient1.Load += new System.EventHandler(this.barrePatient1_Load_1);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkbxInterposAnt);
            this.groupBox4.Controls.Add(this.chkbxInterposPos);
            this.groupBox4.Location = new System.Drawing.Point(314, 380);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 94);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Interposition Linguale";
            this.groupBox4.Move += new System.EventHandler(this.TriImg1_OnEndSaisie);
            // 
            // chkbxInterposAnt
            // 
            this.chkbxInterposAnt.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxInterposAnt.AutoSize = true;
            this.chkbxInterposAnt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxInterposAnt.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxInterposAnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxInterposAnt.Image = global::BASEDiagAdulte.Properties.Resources.InterposLingAnt;
            this.chkbxInterposAnt.Location = new System.Drawing.Point(22, 21);
            this.chkbxInterposAnt.Name = "chkbxInterposAnt";
            this.chkbxInterposAnt.Size = new System.Drawing.Size(73, 73);
            this.chkbxInterposAnt.TabIndex = 16;
            this.chkbxInterposAnt.UseVisualStyleBackColor = true;
            this.chkbxInterposAnt.CheckedChanged += new System.EventHandler(this.chkbxInterposAnt_CheckedChanged);
            this.chkbxInterposAnt.Click += new System.EventHandler(this.chkbxInterposAnt_Click);
            this.chkbxInterposAnt.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // chkbxInterposPos
            // 
            this.chkbxInterposPos.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxInterposPos.AutoSize = true;
            this.chkbxInterposPos.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxInterposPos.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.chkbxInterposPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxInterposPos.Image = global::BASEDiagAdulte.Properties.Resources.InterposLingPost;
            this.chkbxInterposPos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkbxInterposPos.Location = new System.Drawing.Point(170, 21);
            this.chkbxInterposPos.Name = "chkbxInterposPos";
            this.chkbxInterposPos.Size = new System.Drawing.Size(73, 73);
            this.chkbxInterposPos.TabIndex = 17;
            this.chkbxInterposPos.UseVisualStyleBackColor = true;
            this.chkbxInterposPos.CheckedChanged += new System.EventHandler(this.chkbxInterposPos_CheckedChanged);
            this.chkbxInterposPos.Click += new System.EventHandler(this.chkbxInterposPos_Click);
            this.chkbxInterposPos.Paint += new System.Windows.Forms.PaintEventHandler(this.rbFormeU_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabTriImg);
            this.tabControl1.Location = new System.Drawing.Point(12, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(577, 514);
            this.tabControl1.TabIndex = 56;
            // 
            // tabTriImg
            // 
            this.tabTriImg.BackColor = System.Drawing.Color.White;
            this.tabTriImg.Controls.Add(this.groupBox4);
            this.tabTriImg.Controls.Add(this.groupBox1);
            this.tabTriImg.Controls.Add(this.TriImg1);
            this.tabTriImg.Controls.Add(this.TriImg2);
            this.tabTriImg.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTriImg.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabTriImg.Location = new System.Drawing.Point(4, 22);
            this.tabTriImg.Name = "tabTriImg";
            this.tabTriImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriImg.Size = new System.Drawing.Size(569, 488);
            this.tabTriImg.TabIndex = 3;
            this.tabTriImg.Text = "Toutes";
            // 
            // TriImg1
            // 
            this.TriImg1.AllowDrop = true;
            this.TriImg1.AllowOffset = false;
            this.TriImg1.AngleDeRotationPhoto = 0F;
            this.TriImg1.AngleDeRotationRadio = 0F;
            this.TriImg1.Brightness = 0F;
            this.TriImg1.Contraste = 1F;
            this.TriImg1.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.TriImg1.DrawPointName = false;
            this.TriImg1.file = null;
            this.TriImg1.HelpFolder = ".\\";
            this.TriImg1.HelpImage = null;
            this.TriImg1.Location = new System.Drawing.Point(71, 190);
            this.TriImg1.Name = "TriImg1";
            this.TriImg1.NoImage = "Pas d\'image";
            this.TriImg1.PhotoImage = null;
            this.TriImg1.RadioImage = null;
            this.TriImg1.ReadOnly = false;
            this.TriImg1.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.TriImg1.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.TriImg1.Size = new System.Drawing.Size(126, 43);
            this.TriImg1.Synchronized = false;
            this.TriImg1.TabIndex = 13;
            this.TriImg1.TextIfNoImage = "Pas d\'image";
            this.TriImg1.Transparence = 0.99D;
            this.TriImg1.zoomPhoto = 1F;
            this.TriImg1.zoomRadio = 1F;
            this.TriImg1.Load += new System.EventHandler(this.TriImg1_Load);
            // 
            // TriImg2
            // 
            this.TriImg2.AllowDrop = true;
            this.TriImg2.AllowOffset = false;
            this.TriImg2.AngleDeRotationPhoto = 0F;
            this.TriImg2.AngleDeRotationRadio = 0F;
            this.TriImg2.Brightness = 0F;
            this.TriImg2.Contraste = 1F;
            this.TriImg2.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.TriImg2.DrawPointName = false;
            this.TriImg2.file = null;
            this.TriImg2.HelpFolder = ".\\";
            this.TriImg2.HelpImage = null;
            this.TriImg2.Location = new System.Drawing.Point(301, 190);
            this.TriImg2.Name = "TriImg2";
            this.TriImg2.NoImage = "Pas d\'image";
            this.TriImg2.PhotoImage = null;
            this.TriImg2.RadioImage = null;
            this.TriImg2.ReadOnly = false;
            this.TriImg2.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.TriImg2.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.TriImg2.Size = new System.Drawing.Size(140, 43);
            this.TriImg2.Synchronized = false;
            this.TriImg2.TabIndex = 12;
            this.TriImg2.TextIfNoImage = "Pas d\'image";
            this.TriImg2.Transparence = 0.99D;
            this.TriImg2.zoomPhoto = 1F;
            this.TriImg2.zoomRadio = 1F;
            this.TriImg2.Load += new System.EventHandler(this.TriImg2_Load);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(67, 604);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(352, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 57;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(856, 4);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 46;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click_1);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiagAdulte.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(424, 606);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 53;
            this.btnRisque.UseVisualStyleBackColor = true;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.BtnPrint.Location = new System.Drawing.Point(480, 606);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(50, 50);
            this.BtnPrint.TabIndex = 51;
            this.BtnPrint.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(800, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 36;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiagAdulte.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(536, 606);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 47;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click_2);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(8, 606);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 48;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            // 
            // FrmAnalyse4
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(918, 665);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlFullScreen);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse4";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyse Fonctionnel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFonct_FormClosing);
            this.Load += new System.EventHandler(this.FrmFonctionnel_Load);
            this.Resize += new System.EventHandler(this.FrmFonctionnel_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabTriImg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Panel pnlFullScreen;
        private System.Windows.Forms.RadioButton chkbxLaterodeviationRC;
        private System.Windows.Forms.RadioButton chkbxLaterolDeviationFonctionnel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton tendbucc;
        private System.Windows.Forms.RadioButton buccexc;
        private Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton chkbxInterposAnt;
        private System.Windows.Forms.RadioButton chkbxInterposPos;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTriImg;
        private System.Windows.Forms.ListView lvPPT;
        private Ctrls.ImageCtrlAgg TriImg2;
        private Ctrls.ImageCtrlAgg TriImg1;

    }
}
