﻿namespace BASEDiagAdulte
{
    partial class FrmAnalyse9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse9));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControle = new System.Windows.Forms.TabPage();
            this.cdentControle = new BaseCommonControls.ChoixDents();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cdentAgenesie = new BaseCommonControls.ChoixDents();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cdentincluses = new BaseCommonControls.ChoixDents();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cdentSurnum = new BaseCommonControls.ChoixDents();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rbDefinitive = new System.Windows.Forms.RadioButton();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitre = new System.Windows.Forms.Label();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            this.imageCtrl1 = new BASEDiagAdulte.Ctrls.ImageCtrl();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnRisque = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkbxEvolGermeDents = new System.Windows.Forms.CheckBox();
            this.BtnPrevious = new System.Windows.Forms.Button();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabControle.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Bigimgs
            // 
            Bigimgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Bigimgs.ImageStream")));
            Bigimgs.TransparentColor = System.Drawing.Color.Transparent;
            Bigimgs.Images.SetKeyName(0, "mediumPPT.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabControle);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(424, 136);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 439);
            this.tabControl1.TabIndex = 2;
            // 
            // tabControle
            // 
            this.tabControle.Controls.Add(this.cdentControle);
            this.tabControle.Location = new System.Drawing.Point(4, 22);
            this.tabControle.Name = "tabControle";
            this.tabControle.Padding = new System.Windows.Forms.Padding(3);
            this.tabControle.Size = new System.Drawing.Size(376, 413);
            this.tabControle.TabIndex = 5;
            this.tabControle.Text = "Controle";
            this.tabControle.UseVisualStyleBackColor = true;
            // 
            // cdentControle
            // 
            this.cdentControle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cdentControle.IsMandibulaireActif = true;
            this.cdentControle.IsMaxillaireActif = true;
            this.cdentControle.Location = new System.Drawing.Point(55, 16);
            this.cdentControle.Name = "cdentControle";
            this.cdentControle.SelectedDents = "";
            this.cdentControle.separator = ',';
            this.cdentControle.Size = new System.Drawing.Size(279, 396);
            this.cdentControle.TabIndex = 1;
            this.cdentControle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cdentControle_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cdentAgenesie);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(376, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Agénésie";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cdentAgenesie
            // 
            this.cdentAgenesie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cdentAgenesie.IsMandibulaireActif = true;
            this.cdentAgenesie.IsMaxillaireActif = true;
            this.cdentAgenesie.Location = new System.Drawing.Point(55, 16);
            this.cdentAgenesie.Name = "cdentAgenesie";
            this.cdentAgenesie.SelectedDents = "";
            this.cdentAgenesie.separator = ',';
            this.cdentAgenesie.Size = new System.Drawing.Size(279, 396);
            this.cdentAgenesie.TabIndex = 0;
            this.cdentAgenesie.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cdentAgenesie_MouseUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cdentincluses);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(376, 395);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dents incluses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cdentincluses
            // 
            this.cdentincluses.IsMandibulaireActif = true;
            this.cdentincluses.IsMaxillaireActif = true;
            this.cdentincluses.Location = new System.Drawing.Point(55, 16);
            this.cdentincluses.Name = "cdentincluses";
            this.cdentincluses.SelectedDents = "";
            this.cdentincluses.separator = ',';
            this.cdentincluses.Size = new System.Drawing.Size(251, 399);
            this.cdentincluses.TabIndex = 1;
            this.cdentincluses.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cdentincluses_MouseUp);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cdentSurnum);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(376, 395);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Dents surnum.";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cdentSurnum
            // 
            this.cdentSurnum.IsMandibulaireActif = true;
            this.cdentSurnum.IsMaxillaireActif = true;
            this.cdentSurnum.Location = new System.Drawing.Point(55, 16);
            this.cdentSurnum.Name = "cdentSurnum";
            this.cdentSurnum.SelectedDents = "";
            this.cdentSurnum.separator = ',';
            this.cdentSurnum.Size = new System.Drawing.Size(252, 402);
            this.cdentSurnum.TabIndex = 1;
            this.cdentSurnum.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cdentSurnum_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.rbDefinitive);
            this.panel1.Location = new System.Drawing.Point(87, 600);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 79);
            this.panel1.TabIndex = 4;
            this.panel1.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton3.AutoSize = true;
            this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton3.ForeColor = System.Drawing.Color.White;
            this.radioButton3.Image = global::BASEDiagAdulte.Properties.Resources.DentsAutres;
            this.radioButton3.Location = new System.Drawing.Point(151, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(73, 73);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.radioButton3.Click += new System.EventHandler(this.radioButton3_Click);
            this.radioButton3.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton3_Paint);
            // 
            // radioButton2
            // 
            this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton2.AutoSize = true;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Image = global::BASEDiagAdulte.Properties.Resources.DentsSagesses;
            this.radioButton2.Location = new System.Drawing.Point(77, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 73);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            this.radioButton2.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton3_Paint);
            // 
            // rbDefinitive
            // 
            this.rbDefinitive.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDefinitive.AutoSize = true;
            this.rbDefinitive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbDefinitive.ForeColor = System.Drawing.Color.White;
            this.rbDefinitive.Image = global::BASEDiagAdulte.Properties.Resources.DentsDefinitives;
            this.rbDefinitive.Location = new System.Drawing.Point(3, 3);
            this.rbDefinitive.Name = "rbDefinitive";
            this.rbDefinitive.Size = new System.Drawing.Size(73, 73);
            this.rbDefinitive.TabIndex = 0;
            this.rbDefinitive.UseVisualStyleBackColor = true;
            this.rbDefinitive.CheckedChanged += new System.EventHandler(this.rbDefinitive_CheckedChanged);
            this.rbDefinitive.Click += new System.EventHandler(this.rbDefinitive_Click);
            this.rbDefinitive.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton3_Paint);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(68, 685);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(568, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 33;
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 578);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Manque de place pour l\'evolution des germes des dents";
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(13, 63);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(683, 23);
            this.lblTitre.TabIndex = 39;
            this.lblTitre.Text = "Panoramique";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 10);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(684, 50);
            this.barrePatient1.TabIndex = 1;
            // 
            // imageCtrl1
            // 
            this.imageCtrl1.AllowDrop = true;
            this.imageCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageCtrl1.AngleDeRotation = 0F;
            this.imageCtrl1.Brightness = 0F;
            this.imageCtrl1.Contraste = 1F;
            this.imageCtrl1.file = null;
            this.imageCtrl1.Location = new System.Drawing.Point(12, 136);
            this.imageCtrl1.Name = "imageCtrl1";
            this.imageCtrl1.Offset = new System.Drawing.Point(0, 0);
            this.imageCtrl1.OriginalImage = null;
            this.imageCtrl1.Size = new System.Drawing.Size(429, 439);
            this.imageCtrl1.TabIndex = 0;
            this.imageCtrl1.zoom = 1F;
            this.imageCtrl1.OnImageChanged += new System.EventHandler(this.imageCtrl1_OnImageChanged);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(758, 10);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 6;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiagAdulte.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(642, 686);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 40;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Image = global::BASEDiagAdulte.Properties.Resources.Next;
            this.btnNext.Location = new System.Drawing.Point(754, 686);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 50);
            this.btnNext.TabIndex = 38;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.btnPrint.Location = new System.Drawing.Point(698, 686);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 50);
            this.btnPrint.TabIndex = 37;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(702, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 36;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkbxEvolGermeDents
            // 
            this.chkbxEvolGermeDents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkbxEvolGermeDents.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxEvolGermeDents.Checked = true;
            this.chkbxEvolGermeDents.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkbxEvolGermeDents.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxEvolGermeDents.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkbxEvolGermeDents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxEvolGermeDents.Image = ((System.Drawing.Image)(resources.GetObject("chkbxEvolGermeDents.Image")));
            this.chkbxEvolGermeDents.Location = new System.Drawing.Point(44, 567);
            this.chkbxEvolGermeDents.Name = "chkbxEvolGermeDents";
            this.chkbxEvolGermeDents.Size = new System.Drawing.Size(34, 35);
            this.chkbxEvolGermeDents.TabIndex = 34;
            this.chkbxEvolGermeDents.UseVisualStyleBackColor = true;
            this.chkbxEvolGermeDents.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.chkbxEvolGermeDents.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton3_Paint);
            this.chkbxEvolGermeDents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxEvolGermeDents_MouseDown);
            this.chkbxEvolGermeDents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkbxEvolGermeDents_MouseUp);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 685);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 15;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // FrmAnalyse8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(820, 748);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkbxEvolGermeDents);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.imageCtrl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse8";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAnalyse8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse8_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse8_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabControle.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiagAdulte.Ctrls.ImageCtrl imageCtrl1;
        private BASEDiagAdulte.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private BaseCommonControls.ChoixDents cdentAgenesie;
        private BaseCommonControls.ChoixDents cdentincluses;
        private System.Windows.Forms.TabPage tabPage3;
        private BaseCommonControls.ChoixDents cdentSurnum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rbDefinitive;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.CheckBox chkbxEvolGermeDents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.TabPage tabControle;
        private BaseCommonControls.ChoixDents cdentControle;
    }
}