namespace BASEDiag
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
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.rbLarge = new System.Windows.Forms.RadioButton();
            this.rbEtroit = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTriAucun = new System.Windows.Forms.RadioButton();
            this.rbTriBiLateral = new System.Windows.Forms.RadioButton();
            this.rbTriGauche = new System.Windows.Forms.RadioButton();
            this.rbTriDroit = new System.Windows.Forms.RadioButton();
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
            this.btnclose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnRisque = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.analyse21 = new BASEDiag.Ctrls.Analyse2();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.groupBox1.Controls.Add(this.rbNormal);
            this.groupBox1.Controls.Add(this.rbLarge);
            this.groupBox1.Controls.Add(this.rbEtroit);
            this.groupBox1.Location = new System.Drawing.Point(459, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 85);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sourire";
            // 
            // rbNormal
            // 
            this.rbNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNormal.AutoSize = true;
            this.rbNormal.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbNormal.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbNormal.Image = global::BASEDiag.Properties.Resources.SourireDentaire_Normal;
            this.rbNormal.Location = new System.Drawing.Point(129, 11);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(73, 73);
            this.rbNormal.TabIndex = 8;
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbNormal.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbLarge
            // 
            this.rbLarge.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLarge.AutoSize = true;
            this.rbLarge.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLarge.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbLarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLarge.Image = global::BASEDiag.Properties.Resources.SourireDentaire_Large;
            this.rbLarge.Location = new System.Drawing.Point(35, 11);
            this.rbLarge.Name = "rbLarge";
            this.rbLarge.Size = new System.Drawing.Size(73, 73);
            this.rbLarge.TabIndex = 7;
            this.rbLarge.UseVisualStyleBackColor = true;
            this.rbLarge.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbLarge.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbEtroit
            // 
            this.rbEtroit.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEtroit.AutoSize = true;
            this.rbEtroit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbEtroit.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbEtroit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbEtroit.Image = global::BASEDiag.Properties.Resources.SourireDentaire_Etroit;
            this.rbEtroit.Location = new System.Drawing.Point(223, 11);
            this.rbEtroit.Name = "rbEtroit";
            this.rbEtroit.Size = new System.Drawing.Size(73, 73);
            this.rbEtroit.TabIndex = 6;
            this.rbEtroit.UseVisualStyleBackColor = true;
            this.rbEtroit.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbEtroit.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbTriAucun);
            this.groupBox2.Controls.Add(this.rbTriBiLateral);
            this.groupBox2.Controls.Add(this.rbTriGauche);
            this.groupBox2.Controls.Add(this.rbTriDroit);
            this.groupBox2.Location = new System.Drawing.Point(459, 355);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 85);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Triangle";
            // 
            // rbTriAucun
            // 
            this.rbTriAucun.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTriAucun.AutoSize = true;
            this.rbTriAucun.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTriAucun.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTriAucun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTriAucun.Image = global::BASEDiag.Properties.Resources.TriangleAucun;
            this.rbTriAucun.Location = new System.Drawing.Point(246, 11);
            this.rbTriAucun.Name = "rbTriAucun";
            this.rbTriAucun.Size = new System.Drawing.Size(73, 73);
            this.rbTriAucun.TabIndex = 15;
            this.rbTriAucun.TabStop = true;
            this.rbTriAucun.UseVisualStyleBackColor = true;
            this.rbTriAucun.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTriAucun.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTriBiLateral
            // 
            this.rbTriBiLateral.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTriBiLateral.AutoSize = true;
            this.rbTriBiLateral.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTriBiLateral.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTriBiLateral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTriBiLateral.Image = global::BASEDiag.Properties.Resources.TriangleBiLa;
            this.rbTriBiLateral.Location = new System.Drawing.Point(166, 11);
            this.rbTriBiLateral.Name = "rbTriBiLateral";
            this.rbTriBiLateral.Size = new System.Drawing.Size(73, 73);
            this.rbTriBiLateral.TabIndex = 14;
            this.rbTriBiLateral.TabStop = true;
            this.rbTriBiLateral.UseVisualStyleBackColor = true;
            this.rbTriBiLateral.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTriBiLateral.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTriGauche
            // 
            this.rbTriGauche.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTriGauche.AutoSize = true;
            this.rbTriGauche.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTriGauche.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTriGauche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTriGauche.Image = global::BASEDiag.Properties.Resources.TriangleGauche;
            this.rbTriGauche.Location = new System.Drawing.Point(6, 11);
            this.rbTriGauche.Name = "rbTriGauche";
            this.rbTriGauche.Size = new System.Drawing.Size(73, 73);
            this.rbTriGauche.TabIndex = 13;
            this.rbTriGauche.TabStop = true;
            this.rbTriGauche.UseVisualStyleBackColor = true;
            this.rbTriGauche.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTriGauche.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
            // 
            // rbTriDroit
            // 
            this.rbTriDroit.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTriDroit.AutoSize = true;
            this.rbTriDroit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbTriDroit.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbTriDroit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTriDroit.Image = global::BASEDiag.Properties.Resources.TriangleDroit;
            this.rbTriDroit.Location = new System.Drawing.Point(86, 11);
            this.rbTriDroit.Name = "rbTriDroit";
            this.rbTriDroit.Size = new System.Drawing.Size(73, 73);
            this.rbTriDroit.TabIndex = 12;
            this.rbTriDroit.TabStop = true;
            this.rbTriDroit.UseVisualStyleBackColor = true;
            this.rbTriDroit.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbTriDroit.Paint += new System.Windows.Forms.PaintEventHandler(this.rbLarge_Paint);
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
            this.groupBox3.Size = new System.Drawing.Size(326, 131);
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
            this.tbDecInterIncisiveBas.Size = new System.Drawing.Size(233, 40);
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
            this.tbDecInterIncisiveHaut.Size = new System.Drawing.Size(233, 38);
            this.tbDecInterIncisiveHaut.TabIndex = 11;
            this.tbDecInterIncisiveHaut.VisibleItems = 11;
            this.tbDecInterIncisiveHaut.SelectedIndexChanged += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "BAS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
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
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiag.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(735, 0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 9;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 155);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(2, 341);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(104, 49);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiag.Properties.Resources.Risques;
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
            this.BtnPrint.Image = global::BASEDiag.Properties.Resources.Imprimer;
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
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
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
            this.BtnPrevious.Image = global::BASEDiag.Properties.Resources.Previous;
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
            this.BTnNext.Image = global::BASEDiag.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(403, 526);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 10;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
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
            // FrmAnalyse2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(797, 589);
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
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiag.Ctrls.Analyse2 analyse21;
        private BASEDiag.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.RadioButton rbLarge;
        private System.Windows.Forms.RadioButton rbEtroit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTriBiLateral;
        private System.Windows.Forms.RadioButton rbTriGauche;
        private System.Windows.Forms.RadioButton rbTriDroit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbTriAucun;
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Label label1;

    }
}