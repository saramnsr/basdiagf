namespace BASEDiagAdulte
{
    partial class FrmAnalyse2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse2));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEtroit = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbTri2G = new System.Windows.Forms.RadioButton();
            this.rbTri1G = new System.Windows.Forms.RadioButton();
            this.rbTri3G = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.rbTri2D = new System.Windows.Forms.RadioButton();
            this.rbTri1D = new System.Windows.Forms.RadioButton();
            this.rbTri3D = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDecInterIncisiveBas = new ControlsLibrary.TreeViewIconCbx();
            this.tbDecInterIncisiveHaut = new ControlsLibrary.TreeViewIconCbx();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.lblTitre = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            this.analyse21 = new BASEDiagAdulte.Ctrls.Analyse2();
            this.btnclose = new System.Windows.Forms.Button();
            this.pictureBox1 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.pictureBox2 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.btnRisque = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.pictureBox3 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
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
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rbEtroit);
            this.groupBox1.Location = new System.Drawing.Point(668, 446);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 130);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sourire";
            // 
            // rbEtroit
            // 
            this.rbEtroit.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEtroit.AutoSize = true;
            this.rbEtroit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbEtroit.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbEtroit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbEtroit.Image = global::BASEDiagAdulte.Properties.Resources.SourireDentaire_Etroit;
            this.rbEtroit.Location = new System.Drawing.Point(27, 34);
            this.rbEtroit.Name = "rbEtroit";
            this.rbEtroit.Size = new System.Drawing.Size(71, 71);
            this.rbEtroit.TabIndex = 6;
            this.rbEtroit.UseVisualStyleBackColor = true;
            this.rbEtroit.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbEtroit.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(459, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 176);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Triangle";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.rbTri2G);
            this.panel2.Controls.Add(this.rbTri1G);
            this.panel2.Controls.Add(this.rbTri3G);
            this.panel2.Location = new System.Drawing.Point(9, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 73);
            this.panel2.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Gauche";
            // 
            // rbTri2G
            // 
            this.rbTri2G.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri2G.AutoSize = true;
            this.rbTri2G.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri2G.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri2G.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri2G.Image = global::BASEDiagAdulte.Properties.Resources.TNL2G;
            this.rbTri2G.Location = new System.Drawing.Point(156, 0);
            this.rbTri2G.Name = "rbTri2G";
            this.rbTri2G.Size = new System.Drawing.Size(73, 73);
            this.rbTri2G.TabIndex = 15;
            this.rbTri2G.TabStop = true;
            this.rbTri2G.UseVisualStyleBackColor = true;
            this.rbTri2G.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri2G.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTri1G
            // 
            this.rbTri1G.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri1G.AutoSize = true;
            this.rbTri1G.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri1G.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri1G.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri1G.Image = global::BASEDiagAdulte.Properties.Resources.TNL1G;
            this.rbTri1G.Location = new System.Drawing.Point(77, 0);
            this.rbTri1G.Name = "rbTri1G";
            this.rbTri1G.Size = new System.Drawing.Size(73, 73);
            this.rbTri1G.TabIndex = 16;
            this.rbTri1G.TabStop = true;
            this.rbTri1G.UseVisualStyleBackColor = true;
            this.rbTri1G.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri1G.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTri3G
            // 
            this.rbTri3G.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri3G.AutoSize = true;
            this.rbTri3G.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri3G.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri3G.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri3G.Image = global::BASEDiagAdulte.Properties.Resources.TNL3G;
            this.rbTri3G.Location = new System.Drawing.Point(237, 0);
            this.rbTri3G.Name = "rbTri3G";
            this.rbTri3G.Size = new System.Drawing.Size(73, 73);
            this.rbTri3G.TabIndex = 17;
            this.rbTri3G.TabStop = true;
            this.rbTri3G.UseVisualStyleBackColor = true;
            this.rbTri3G.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri3G.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rbTri2D);
            this.panel1.Controls.Add(this.rbTri1D);
            this.panel1.Controls.Add(this.rbTri3D);
            this.panel1.Location = new System.Drawing.Point(12, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 73);
            this.panel1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Droite";
            // 
            // rbTri2D
            // 
            this.rbTri2D.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri2D.AutoSize = true;
            this.rbTri2D.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri2D.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri2D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri2D.Image = global::BASEDiagAdulte.Properties.Resources.TNL2D;
            this.rbTri2D.Location = new System.Drawing.Point(153, 0);
            this.rbTri2D.Name = "rbTri2D";
            this.rbTri2D.Size = new System.Drawing.Size(73, 73);
            this.rbTri2D.TabIndex = 12;
            this.rbTri2D.TabStop = true;
            this.rbTri2D.UseVisualStyleBackColor = true;
            this.rbTri2D.CheckedChanged += new System.EventHandler(this.rbTri2D_CheckedChanged);
            this.rbTri2D.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri2D.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTri1D
            // 
            this.rbTri1D.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri1D.AutoSize = true;
            this.rbTri1D.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri1D.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri1D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri1D.Image = global::BASEDiagAdulte.Properties.Resources.TNL1D;
            this.rbTri1D.Location = new System.Drawing.Point(74, 0);
            this.rbTri1D.Name = "rbTri1D";
            this.rbTri1D.Size = new System.Drawing.Size(73, 73);
            this.rbTri1D.TabIndex = 13;
            this.rbTri1D.TabStop = true;
            this.rbTri1D.UseVisualStyleBackColor = true;
            this.rbTri1D.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri1D.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTri3D
            // 
            this.rbTri3D.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTri3D.AutoSize = true;
            this.rbTri3D.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTri3D.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTri3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTri3D.Image = global::BASEDiagAdulte.Properties.Resources.TNL3D;
            this.rbTri3D.Location = new System.Drawing.Point(234, 0);
            this.rbTri3D.Name = "rbTri3D";
            this.rbTri3D.Size = new System.Drawing.Size(73, 73);
            this.rbTri3D.TabIndex = 14;
            this.rbTri3D.TabStop = true;
            this.rbTri3D.UseVisualStyleBackColor = true;
            this.rbTri3D.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTri3D.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbDecInterIncisiveBas);
            this.groupBox3.Controls.Add(this.tbDecInterIncisiveHaut);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(459, 446);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 131);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Décalage Inter-Incisive";
            // 
            // tbDecInterIncisiveBas
            // 
            this.tbDecInterIncisiveBas.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbDecInterIncisiveBas.Location = new System.Drawing.Point(63, 75);
            this.tbDecInterIncisiveBas.Name = "tbDecInterIncisiveBas";
            this.tbDecInterIncisiveBas.SelectedIndex = -1;
            this.tbDecInterIncisiveBas.SelectedItem = null;
            this.tbDecInterIncisiveBas.Size = new System.Drawing.Size(134, 40);
            this.tbDecInterIncisiveBas.TabIndex = 12;
            this.tbDecInterIncisiveBas.VisibleItems = 11;
            this.tbDecInterIncisiveBas.SelectedIndexChanged += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            // 
            // tbDecInterIncisiveHaut
            // 
            this.tbDecInterIncisiveHaut.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbDecInterIncisiveHaut.Location = new System.Drawing.Point(63, 19);
            this.tbDecInterIncisiveHaut.Name = "tbDecInterIncisiveHaut";
            this.tbDecInterIncisiveHaut.SelectedIndex = -1;
            this.tbDecInterIncisiveHaut.SelectedItem = null;
            this.tbDecInterIncisiveHaut.Size = new System.Drawing.Size(134, 38);
            this.tbDecInterIncisiveHaut.TabIndex = 11;
            this.tbDecInterIncisiveHaut.VisibleItems = 11;
            this.tbDecInterIncisiveHaut.SelectedIndexChanged += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "BAS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "HAUT";
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(58, 526);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(227, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 12;
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
            this.lblTitre.Location = new System.Drawing.Point(9, 54);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(444, 23);
            this.lblTitre.TabIndex = 17;
            this.lblTitre.Text = "Sourire Face";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(459, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 178);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 83);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 92);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 83);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 511);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "Appuyer sur \'H\' pour voir l\'aide";
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(2, 1);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(671, 49);
            this.barrePatient1.TabIndex = 1;
            // 
            // analyse21
            // 
            this.analyse21.AllowDrop = true;
            this.analyse21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analyse21.AngleDeRotationPhoto = 0F;
            this.analyse21.AngleDeRotationRadio = 0F;
            this.analyse21.Brightness = 0F;
            this.analyse21.Contraste = 1F;
            this.analyse21.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.analyse21.DrawPointName = false;
            this.analyse21.file = null;
            this.analyse21.HelpFolder = ".\\";
            this.analyse21.HelpImage = null;
            this.analyse21.Location = new System.Drawing.Point(12, 80);
            this.analyse21.Name = "analyse21";
            this.analyse21.PhotoImage = null;
            this.analyse21.RadioImage = null;
            this.analyse21.ReadOnly = false;
            this.analyse21.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.analyse21.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.analyse21.Size = new System.Drawing.Size(441, 428);
            this.analyse21.Synchronized = false;
            this.analyse21.TabIndex = 0;
            this.analyse21.TextIfNoImage = "Pas d\'image";
            this.analyse21.Transparence = 0.99D;
            this.analyse21.zoomPhoto = 1F;
            this.analyse21.zoomRadio = 1F;
            this.analyse21.OnRadioChanged += new System.EventHandler(this.analyse21_OnRadioChanged);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(735, 0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 9;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 218);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 115);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(2, 339);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(104, 114);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiagAdulte.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(291, 526);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 18;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.BtnPrint.Location = new System.Drawing.Point(347, 526);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(50, 50);
            this.BtnPrint.TabIndex = 16;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(679, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 15;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(2, 526);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 11;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiagAdulte.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(403, 526);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 10;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(2, 459);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(104, 49);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 36;
            this.pictureBox3.TabStop = false;
            // 
            // FrmAnalyse2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(797, 589);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.analyse21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analyse Sourire Facial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse2_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse2_Load);
            this.Resize += new System.EventHandler(this.FrmAnalyse2_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiagAdulte.Ctrls.Analyse2 analyse21;
        private BASEDiagAdulte.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox rbEtroit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTri3D;
        private System.Windows.Forms.RadioButton rbTri1D;
        private System.Windows.Forms.RadioButton rbTri2D;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTitre;
        private ControlsLibrary.TreeViewIconCbx tbDecInterIncisiveHaut;
        private ControlsLibrary.TreeViewIconCbx tbDecInterIncisiveBas;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox1;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbTri2G;
        private System.Windows.Forms.RadioButton rbTri1G;
        private System.Windows.Forms.RadioButton rbTri3G;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox3;

    }
}