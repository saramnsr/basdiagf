namespace BASEDiagAdulte
{
    partial class FrmAnalyse1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse1));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.btnclose = new System.Windows.Forms.Button();
            this.pictureBox4 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.pictureBox3 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.pictureBox2 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.pictureBox1 = new BASEDiagAdulte.Ctrls.ExtandablePictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.btnRisque = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.BTnNext = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.rbEffondrement = new System.Windows.Forms.RadioButton();
            this.rbDiminution = new System.Windows.Forms.RadioButton();
            this.rbAugmentation = new System.Windows.Forms.RadioButton();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            this.analyse11 = new BASEDiagAdulte.Ctrls.Analyse1();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(735, 1);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 5;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(3, 354);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(73, 76);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 39;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 436);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(73, 60);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 259);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 502);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 594);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "Appuyer sur \'H\' pour voir l\'aide";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(463, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(322, 510);
            this.tableLayoutPanel1.TabIndex = 21;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(316, 249);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 258);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(316, 249);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiagAdulte.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(294, 610);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 17;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(9, 59);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(444, 23);
            this.lblTitre.TabIndex = 16;
            this.lblTitre.Text = "Masque Facial";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 609);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 15;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(679, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 14;
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
            this.lvPPT.Location = new System.Drawing.Point(68, 609);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(220, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 8;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = ((System.Drawing.Image)(resources.GetObject("BTnNext.Image")));
            this.BTnNext.Location = new System.Drawing.Point(403, 609);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 7;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.BtnPrint.Location = new System.Drawing.Point(350, 609);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(50, 50);
            this.BtnPrint.TabIndex = 6;
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rbNormal);
            this.groupBox1.Controls.Add(this.rbEffondrement);
            this.groupBox1.Controls.Add(this.rbDiminution);
            this.groupBox1.Controls.Add(this.rbAugmentation);
            this.groupBox1.Location = new System.Drawing.Point(459, 575);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Etage Inférieur";
            // 
            // rbNormal
            // 
            this.rbNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNormal.AutoSize = true;
            this.rbNormal.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbNormal.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbNormal.Image = global::BASEDiagAdulte.Properties.Resources.EtageInf_Normal;
            this.rbNormal.Location = new System.Drawing.Point(162, 11);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(73, 73);
            this.rbNormal.TabIndex = 8;
            this.rbNormal.TabStop = true;
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbNormal.Paint += new System.Windows.Forms.PaintEventHandler(this.rbEffondrement_Paint);
            // 
            // rbEffondrement
            // 
            this.rbEffondrement.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEffondrement.AutoSize = true;
            this.rbEffondrement.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbEffondrement.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbEffondrement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbEffondrement.Image = global::BASEDiagAdulte.Properties.Resources.EtageInf_Effondrement;
            this.rbEffondrement.Location = new System.Drawing.Point(4, 11);
            this.rbEffondrement.Name = "rbEffondrement";
            this.rbEffondrement.Size = new System.Drawing.Size(73, 73);
            this.rbEffondrement.TabIndex = 7;
            this.rbEffondrement.TabStop = true;
            this.rbEffondrement.UseVisualStyleBackColor = true;
            this.rbEffondrement.CheckedChanged += new System.EventHandler(this.rbEffondrement_CheckedChanged_1);
            this.rbEffondrement.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbEffondrement.Paint += new System.Windows.Forms.PaintEventHandler(this.rbEffondrement_Paint);
            // 
            // rbDiminution
            // 
            this.rbDiminution.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDiminution.AutoSize = true;
            this.rbDiminution.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbDiminution.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbDiminution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbDiminution.Image = global::BASEDiagAdulte.Properties.Resources.EtageInf_Diminution;
            this.rbDiminution.Location = new System.Drawing.Point(83, 11);
            this.rbDiminution.Name = "rbDiminution";
            this.rbDiminution.Size = new System.Drawing.Size(73, 73);
            this.rbDiminution.TabIndex = 6;
            this.rbDiminution.TabStop = true;
            this.rbDiminution.UseVisualStyleBackColor = true;
            this.rbDiminution.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbDiminution.Paint += new System.Windows.Forms.PaintEventHandler(this.rbEffondrement_Paint);
            // 
            // rbAugmentation
            // 
            this.rbAugmentation.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAugmentation.AutoSize = true;
            this.rbAugmentation.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbAugmentation.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbAugmentation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbAugmentation.Image = global::BASEDiagAdulte.Properties.Resources.EtageInf_Augmentation;
            this.rbAugmentation.Location = new System.Drawing.Point(241, 11);
            this.rbAugmentation.Name = "rbAugmentation";
            this.rbAugmentation.Size = new System.Drawing.Size(73, 73);
            this.rbAugmentation.TabIndex = 5;
            this.rbAugmentation.TabStop = true;
            this.rbAugmentation.UseVisualStyleBackColor = true;
            this.rbAugmentation.Click += new System.EventHandler(this.rbEffondrement_CheckedChanged);
            this.rbAugmentation.Paint += new System.Windows.Forms.PaintEventHandler(this.rbEffondrement_Paint);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 1);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(661, 49);
            this.barrePatient1.TabIndex = 1;
            // 
            // analyse11
            // 
            this.analyse11.AllowDrop = true;
            this.analyse11.AllowOffset = false;
            this.analyse11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analyse11.AngleDeRotationPhoto = 0F;
            this.analyse11.AngleDeRotationRadio = 0F;
            this.analyse11.Brightness = 0F;
            this.analyse11.Contraste = 1F;
            this.analyse11.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.analyse11.DrawPointName = false;
            this.analyse11.file = null;
            this.analyse11.HelpFolder = ".\\";
            this.analyse11.HelpImage = null;
            this.analyse11.Location = new System.Drawing.Point(12, 79);
            this.analyse11.Name = "analyse11";
            this.analyse11.PhotoImage = null;
            this.analyse11.RadioImage = null;
            this.analyse11.ReadOnly = false;
            this.analyse11.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.analyse11.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.analyse11.Size = new System.Drawing.Size(441, 524);
            this.analyse11.Synchronized = false;
            this.analyse11.TabIndex = 0;
            this.analyse11.TextIfNoImage = "Pas d\'image";
            this.analyse11.Transparence = 0.99D;
            this.analyse11.zoomPhoto = 1F;
            this.analyse11.zoomRadio = 1F;
            this.analyse11.OnSaisie += new System.EventHandler(this.analyse11_OnSaisie);
            this.analyse11.OnRadioChanged += new System.EventHandler(this.analyse11_OnRadioChanged);
            this.analyse11.Load += new System.EventHandler(this.analyse11_Load);
            // 
            // FrmAnalyse1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(797, 672);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.analyse11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Masque Facial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse1_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse1_Load);
            this.Resize += new System.EventHandler(this.FrmAnalyse1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiagAdulte.Ctrls.Analyse1 analyse11;
        private BASEDiagAdulte.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEffondrement;
        private System.Windows.Forms.RadioButton rbDiminution;
        private System.Windows.Forms.RadioButton rbAugmentation;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox1;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox2;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox3;
        private BASEDiagAdulte.Ctrls.ExtandablePictureBox pictureBox4;
    }
}