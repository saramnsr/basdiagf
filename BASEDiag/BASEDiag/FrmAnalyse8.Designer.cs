namespace BASEDiag
{
    partial class FrmAnalyse8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse8));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.btnclose = new System.Windows.Forms.Button();
            this.btnEnvoie = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.btnRisque = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnmoins = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbIncSupInfPro = new System.Windows.Forms.RadioButton();
            this.rbIncSupInfRetro = new System.Windows.Forms.RadioButton();
            this.rbIncSupInfNormo = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbSensSagittalCIII = new System.Windows.Forms.RadioButton();
            this.rbSensSagittalCI = new System.Windows.Forms.RadioButton();
            this.rbSensSagittalCII = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbSensVertHyper = new System.Windows.Forms.RadioButton();
            this.rbSensVertHypo = new System.Windows.Forms.RadioButton();
            this.rbSensVertNormo = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbIncSupPro = new System.Windows.Forms.RadioButton();
            this.rbIncSupRetro = new System.Windows.Forms.RadioButton();
            this.rbIncSupNormo = new System.Windows.Forms.RadioButton();
            this.analyse71 = new BASEDiag.Ctrls.Analyse7();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.button2 = new System.Windows.Forms.Button();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiag.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(856, 12);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 28;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnEnvoie
            // 
            this.btnEnvoie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnvoie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnvoie.Image = global::BASEDiag.Properties.Resources.EnvoyeVersBASPhoto;
            this.btnEnvoie.Location = new System.Drawing.Point(411, 595);
            this.btnEnvoie.Name = "btnEnvoie";
            this.btnEnvoie.Size = new System.Drawing.Size(50, 50);
            this.btnEnvoie.TabIndex = 40;
            this.btnEnvoie.UseVisualStyleBackColor = true;
            this.btnEnvoie.Click += new System.EventHandler(this.btnEnvoie_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(580, 75);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 198);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 93);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 102);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 93);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiag.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(352, 594);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 38;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(12, 75);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(562, 23);
            this.lblTitre.TabIndex = 37;
            this.lblTitre.Text = "Radio";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPlus
            // 
            this.btnPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPlus.Location = new System.Drawing.Point(539, 510);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(38, 36);
            this.btnPlus.TabIndex = 36;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Visible = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnmoins
            // 
            this.btnmoins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnmoins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmoins.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmoins.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnmoins.Location = new System.Drawing.Point(539, 552);
            this.btnmoins.Name = "btnmoins";
            this.btnmoins.Size = new System.Drawing.Size(38, 36);
            this.btnmoins.TabIndex = 35;
            this.btnmoins.Text = "-";
            this.btnmoins.UseVisualStyleBackColor = true;
            this.btnmoins.Visible = false;
            this.btnmoins.Click += new System.EventHandler(this.btnmoins_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 576);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "Appuyer sur \'H\' pour voir l\'aide";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(800, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 33;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(68, 594);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(278, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 32;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiag.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 594);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 31;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiag.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(524, 594);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 30;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::BASEDiag.Properties.Resources.Imprimer;
            this.btnPrint.Location = new System.Drawing.Point(468, 594);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 50);
            this.btnPrint.TabIndex = 29;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rbIncSupInfPro);
            this.groupBox4.Controls.Add(this.rbIncSupInfRetro);
            this.groupBox4.Controls.Add(this.rbIncSupInfNormo);
            this.groupBox4.Location = new System.Drawing.Point(580, 459);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(326, 85);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Incisive Inférieure";
            // 
            // rbIncSupInfPro
            // 
            this.rbIncSupInfPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupInfPro.AutoSize = true;
            this.rbIncSupInfPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupInfPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupInfPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupInfPro.Image = global::BASEDiag.Properties.Resources.Pro;
            this.rbIncSupInfPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncSupInfPro.Location = new System.Drawing.Point(236, 11);
            this.rbIncSupInfPro.Name = "rbIncSupInfPro";
            this.rbIncSupInfPro.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupInfPro.TabIndex = 19;
            this.rbIncSupInfPro.TabStop = true;
            this.rbIncSupInfPro.UseVisualStyleBackColor = true;
            this.rbIncSupInfPro.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupInfPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbIncSupInfRetro
            // 
            this.rbIncSupInfRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupInfRetro.AutoSize = true;
            this.rbIncSupInfRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupInfRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupInfRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupInfRetro.Image = global::BASEDiag.Properties.Resources.Retro;
            this.rbIncSupInfRetro.Location = new System.Drawing.Point(23, 11);
            this.rbIncSupInfRetro.Name = "rbIncSupInfRetro";
            this.rbIncSupInfRetro.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupInfRetro.TabIndex = 18;
            this.rbIncSupInfRetro.TabStop = true;
            this.rbIncSupInfRetro.UseVisualStyleBackColor = true;
            this.rbIncSupInfRetro.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupInfRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbIncSupInfNormo
            // 
            this.rbIncSupInfNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupInfNormo.AutoSize = true;
            this.rbIncSupInfNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupInfNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupInfNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupInfNormo.Image = global::BASEDiag.Properties.Resources.Normo;
            this.rbIncSupInfNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncSupInfNormo.Location = new System.Drawing.Point(133, 11);
            this.rbIncSupInfNormo.Name = "rbIncSupInfNormo";
            this.rbIncSupInfNormo.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupInfNormo.TabIndex = 17;
            this.rbIncSupInfNormo.TabStop = true;
            this.rbIncSupInfNormo.UseVisualStyleBackColor = true;
            this.rbIncSupInfNormo.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupInfNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rbSensSagittalCIII);
            this.groupBox3.Controls.Add(this.rbSensSagittalCI);
            this.groupBox3.Controls.Add(this.rbSensSagittalCII);
            this.groupBox3.Location = new System.Drawing.Point(580, 369);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 85);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sens Sagittal";
            // 
            // rbSensSagittalCIII
            // 
            this.rbSensSagittalCIII.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensSagittalCIII.AutoSize = true;
            this.rbSensSagittalCIII.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensSagittalCIII.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensSagittalCIII.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensSagittalCIII.Image = global::BASEDiag.Properties.Resources.Classe_III;
            this.rbSensSagittalCIII.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbSensSagittalCIII.Location = new System.Drawing.Point(236, 11);
            this.rbSensSagittalCIII.Name = "rbSensSagittalCIII";
            this.rbSensSagittalCIII.Size = new System.Drawing.Size(73, 73);
            this.rbSensSagittalCIII.TabIndex = 19;
            this.rbSensSagittalCIII.TabStop = true;
            this.rbSensSagittalCIII.UseVisualStyleBackColor = true;
            this.rbSensSagittalCIII.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensSagittalCIII.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbSensSagittalCI
            // 
            this.rbSensSagittalCI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensSagittalCI.AutoSize = true;
            this.rbSensSagittalCI.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensSagittalCI.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensSagittalCI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensSagittalCI.Image = global::BASEDiag.Properties.Resources.Classe_I;
            this.rbSensSagittalCI.Location = new System.Drawing.Point(23, 11);
            this.rbSensSagittalCI.Name = "rbSensSagittalCI";
            this.rbSensSagittalCI.Size = new System.Drawing.Size(73, 73);
            this.rbSensSagittalCI.TabIndex = 18;
            this.rbSensSagittalCI.TabStop = true;
            this.rbSensSagittalCI.UseVisualStyleBackColor = true;
            this.rbSensSagittalCI.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensSagittalCI.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbSensSagittalCII
            // 
            this.rbSensSagittalCII.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensSagittalCII.AutoSize = true;
            this.rbSensSagittalCII.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensSagittalCII.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensSagittalCII.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensSagittalCII.Image = global::BASEDiag.Properties.Resources.Classe_II;
            this.rbSensSagittalCII.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbSensSagittalCII.Location = new System.Drawing.Point(133, 11);
            this.rbSensSagittalCII.Name = "rbSensSagittalCII";
            this.rbSensSagittalCII.Size = new System.Drawing.Size(73, 73);
            this.rbSensSagittalCII.TabIndex = 17;
            this.rbSensSagittalCII.TabStop = true;
            this.rbSensSagittalCII.UseVisualStyleBackColor = true;
            this.rbSensSagittalCII.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensSagittalCII.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbSensVertHyper);
            this.groupBox2.Controls.Add(this.rbSensVertHypo);
            this.groupBox2.Controls.Add(this.rbSensVertNormo);
            this.groupBox2.Location = new System.Drawing.Point(580, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 85);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sens Vertical";
            // 
            // rbSensVertHyper
            // 
            this.rbSensVertHyper.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensVertHyper.AutoSize = true;
            this.rbSensVertHyper.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensVertHyper.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensVertHyper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensVertHyper.Image = global::BASEDiag.Properties.Resources.HyperDiv;
            this.rbSensVertHyper.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbSensVertHyper.Location = new System.Drawing.Point(236, 11);
            this.rbSensVertHyper.Name = "rbSensVertHyper";
            this.rbSensVertHyper.Size = new System.Drawing.Size(73, 73);
            this.rbSensVertHyper.TabIndex = 19;
            this.rbSensVertHyper.TabStop = true;
            this.rbSensVertHyper.UseVisualStyleBackColor = true;
            this.rbSensVertHyper.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensVertHyper.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbSensVertHypo
            // 
            this.rbSensVertHypo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensVertHypo.AutoSize = true;
            this.rbSensVertHypo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensVertHypo.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensVertHypo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensVertHypo.Image = global::BASEDiag.Properties.Resources.HypoDiv;
            this.rbSensVertHypo.Location = new System.Drawing.Point(23, 11);
            this.rbSensVertHypo.Name = "rbSensVertHypo";
            this.rbSensVertHypo.Size = new System.Drawing.Size(73, 73);
            this.rbSensVertHypo.TabIndex = 18;
            this.rbSensVertHypo.TabStop = true;
            this.rbSensVertHypo.UseVisualStyleBackColor = true;
            this.rbSensVertHypo.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensVertHypo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbSensVertNormo
            // 
            this.rbSensVertNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSensVertNormo.AutoSize = true;
            this.rbSensVertNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbSensVertNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbSensVertNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSensVertNormo.Image = global::BASEDiag.Properties.Resources.NormoDiv;
            this.rbSensVertNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbSensVertNormo.Location = new System.Drawing.Point(133, 11);
            this.rbSensVertNormo.Name = "rbSensVertNormo";
            this.rbSensVertNormo.Size = new System.Drawing.Size(73, 73);
            this.rbSensVertNormo.TabIndex = 17;
            this.rbSensVertNormo.TabStop = true;
            this.rbSensVertNormo.UseVisualStyleBackColor = true;
            this.rbSensVertNormo.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbSensVertNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbIncSupPro);
            this.groupBox1.Controls.Add(this.rbIncSupRetro);
            this.groupBox1.Controls.Add(this.rbIncSupNormo);
            this.groupBox1.Location = new System.Drawing.Point(580, 549);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 85);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Incisive Supérieure";
            // 
            // rbIncSupPro
            // 
            this.rbIncSupPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupPro.AutoSize = true;
            this.rbIncSupPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupPro.Image = global::BASEDiag.Properties.Resources.Pro;
            this.rbIncSupPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncSupPro.Location = new System.Drawing.Point(236, 11);
            this.rbIncSupPro.Name = "rbIncSupPro";
            this.rbIncSupPro.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupPro.TabIndex = 19;
            this.rbIncSupPro.TabStop = true;
            this.rbIncSupPro.UseVisualStyleBackColor = true;
            this.rbIncSupPro.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbIncSupRetro
            // 
            this.rbIncSupRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupRetro.AutoSize = true;
            this.rbIncSupRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupRetro.Image = global::BASEDiag.Properties.Resources.Retro;
            this.rbIncSupRetro.Location = new System.Drawing.Point(23, 11);
            this.rbIncSupRetro.Name = "rbIncSupRetro";
            this.rbIncSupRetro.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupRetro.TabIndex = 18;
            this.rbIncSupRetro.TabStop = true;
            this.rbIncSupRetro.UseVisualStyleBackColor = true;
            this.rbIncSupRetro.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // rbIncSupNormo
            // 
            this.rbIncSupNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncSupNormo.AutoSize = true;
            this.rbIncSupNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncSupNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbIncSupNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncSupNormo.Image = global::BASEDiag.Properties.Resources.Normo;
            this.rbIncSupNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncSupNormo.Location = new System.Drawing.Point(133, 11);
            this.rbIncSupNormo.Name = "rbIncSupNormo";
            this.rbIncSupNormo.Size = new System.Drawing.Size(73, 73);
            this.rbIncSupNormo.TabIndex = 17;
            this.rbIncSupNormo.TabStop = true;
            this.rbIncSupNormo.UseVisualStyleBackColor = true;
            this.rbIncSupNormo.Click += new System.EventHandler(this.rbSensVertHypo_Click);
            this.rbIncSupNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbSensVertHypo_Paint);
            // 
            // analyse71
            // 
            this.analyse71.AllowDrop = true;
            this.analyse71.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analyse71.AngleDeRotationPhoto = 0F;
            this.analyse71.AngleDeRotationRadio = 0F;
            this.analyse71.Brightness = 0F;
            this.analyse71.Contraste = 1F;
            this.analyse71.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.analyse71.DrawPointName = false;
            this.analyse71.file = null;
            this.analyse71.HelpFolder = ".\\";
            this.analyse71.HelpImage = null;
            this.analyse71.Location = new System.Drawing.Point(15, 99);
            this.analyse71.Name = "analyse71";
            this.analyse71.PhotoImage = null;
            this.analyse71.RadioImage = null;
            this.analyse71.ReadOnly = false;
            this.analyse71.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.analyse71.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.analyse71.Size = new System.Drawing.Size(562, 489);
            this.analyse71.Synchronized = false;
            this.analyse71.TabIndex = 0;
            this.analyse71.TextIfNoImage = "Pas d\'image";
            this.analyse71.Transparence = 0.99D;
            this.analyse71.zoomPhoto = 1F;
            this.analyse71.zoomRadio = 1F;
            this.analyse71.OnEndSynchro += new System.EventHandler(this.analyse71_OnEndSynchro);
            this.analyse71.OnSaisie += new System.EventHandler(this.analyse71_OnSaisie);
            this.analyse71.OnRadioChanged += new System.EventHandler(this.analyse71_OnRadioChanged);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(15, 12);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(782, 50);
            this.barrePatient1.TabIndex = 14;
            this.barrePatient1.Load += new System.EventHandler(this.barrePatient1_Load);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Garamond", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(15, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 23);
            this.button2.TabIndex = 41;
            this.button2.Text = "Synchro Photo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmAnalyse8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(918, 665);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnEnvoie);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnmoins);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.analyse71);
            this.Controls.Add(this.barrePatient1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse8";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAnalyse8";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse7_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse7_Load);
            this.Shown += new System.EventHandler(this.FrmAnalyse7_Shown);
            this.Resize += new System.EventHandler(this.FrmAnalyse7_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private BASEDiag.Ctrls.Analyse7 analyse71;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbIncSupInfPro;
        private System.Windows.Forms.RadioButton rbIncSupInfRetro;
        private System.Windows.Forms.RadioButton rbIncSupInfNormo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbSensSagittalCIII;
        private System.Windows.Forms.RadioButton rbSensSagittalCI;
        private System.Windows.Forms.RadioButton rbSensSagittalCII;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbSensVertHyper;
        private System.Windows.Forms.RadioButton rbSensVertHypo;
        private System.Windows.Forms.RadioButton rbSensVertNormo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbIncSupPro;
        private System.Windows.Forms.RadioButton rbIncSupRetro;
        private System.Windows.Forms.RadioButton rbIncSupNormo;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnmoins;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Button btnEnvoie;
        private Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.Button button2;
    }
}