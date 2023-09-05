namespace BASEDiag
{
    partial class FrmAnalyse6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse6));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLabial = new ControlsLibrary.TreeViewIconCbx();
            this.tbGingivInf = new ControlsLibrary.TreeViewIconCbx();
            this.tbGingivSup = new ControlsLibrary.TreeViewIconCbx();
            this.chkbxNormal = new System.Windows.Forms.CheckBox();
            this.chkbxLabial = new System.Windows.Forms.CheckBox();
            this.chkbxGingivalInf = new System.Windows.Forms.CheckBox();
            this.chkbxGingivalSup = new System.Windows.Forms.CheckBox();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.lblTitre = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnRisque = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.imageCtrl1 = new BASEDiag.Ctrls.ImageCtrlAgg();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.tbLabial);
            this.groupBox1.Controls.Add(this.tbGingivInf);
            this.groupBox1.Controls.Add(this.tbGingivSup);
            this.groupBox1.Controls.Add(this.chkbxNormal);
            this.groupBox1.Controls.Add(this.chkbxLabial);
            this.groupBox1.Controls.Add(this.chkbxGingivalInf);
            this.groupBox1.Controls.Add(this.chkbxGingivalSup);
            this.groupBox1.Location = new System.Drawing.Point(348, 336);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 227);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // tbLabial
            // 
            this.tbLabial.Enabled = false;
            this.tbLabial.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbLabial.Location = new System.Drawing.Point(77, 170);
            this.tbLabial.Name = "tbLabial";
            this.tbLabial.SelectedIndex = -1;
            this.tbLabial.SelectedItem = null;
            this.tbLabial.Size = new System.Drawing.Size(170, 37);
            this.tbLabial.TabIndex = 19;
            this.tbLabial.VisibleItems = 11;
            this.tbLabial.SelectedIndexChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged);
            // 
            // tbGingivInf
            // 
            this.tbGingivInf.Enabled = false;
            this.tbGingivInf.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbGingivInf.Location = new System.Drawing.Point(78, 100);
            this.tbGingivInf.Name = "tbGingivInf";
            this.tbGingivInf.SelectedIndex = -1;
            this.tbGingivInf.SelectedItem = null;
            this.tbGingivInf.Size = new System.Drawing.Size(170, 37);
            this.tbGingivInf.TabIndex = 18;
            this.tbGingivInf.VisibleItems = 11;
            this.tbGingivInf.SelectedIndexChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged);
            // 
            // tbGingivSup
            // 
            this.tbGingivSup.Enabled = false;
            this.tbGingivSup.ft = new System.Drawing.Font("Garamond", 12F);
            this.tbGingivSup.Location = new System.Drawing.Point(78, 29);
            this.tbGingivSup.Name = "tbGingivSup";
            this.tbGingivSup.SelectedIndex = -1;
            this.tbGingivSup.SelectedItem = null;
            this.tbGingivSup.Size = new System.Drawing.Size(170, 37);
            this.tbGingivSup.TabIndex = 17;
            this.tbGingivSup.VisibleItems = 11;
            this.tbGingivSup.SelectedIndexChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged);
            // 
            // chkbxNormal
            // 
            this.chkbxNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxNormal.AutoSize = true;
            this.chkbxNormal.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxNormal.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkbxNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxNormal.Image = global::BASEDiag.Properties.Resources.SourireNormal;
            this.chkbxNormal.Location = new System.Drawing.Point(254, 91);
            this.chkbxNormal.Name = "chkbxNormal";
            this.chkbxNormal.Size = new System.Drawing.Size(71, 71);
            this.chkbxNormal.TabIndex = 11;
            this.chkbxNormal.UseVisualStyleBackColor = true;
            this.chkbxNormal.CheckedChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged_1);
            this.chkbxNormal.Click += new System.EventHandler(this.chkbxNormal_Click);
            this.chkbxNormal.Paint += new System.Windows.Forms.PaintEventHandler(this.chkbxLabial_Paint);
            // 
            // chkbxLabial
            // 
            this.chkbxLabial.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxLabial.AutoSize = true;
            this.chkbxLabial.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxLabial.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkbxLabial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxLabial.Image = global::BASEDiag.Properties.Resources.SourireLabial;
            this.chkbxLabial.Location = new System.Drawing.Point(6, 154);
            this.chkbxLabial.Name = "chkbxLabial";
            this.chkbxLabial.Size = new System.Drawing.Size(71, 71);
            this.chkbxLabial.TabIndex = 8;
            this.chkbxLabial.UseVisualStyleBackColor = true;
            this.chkbxLabial.CheckedChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged_1);
            this.chkbxLabial.Click += new System.EventHandler(this.chkbxLabial_Click);
            this.chkbxLabial.Paint += new System.Windows.Forms.PaintEventHandler(this.chkbxLabial_Paint);
            // 
            // chkbxGingivalInf
            // 
            this.chkbxGingivalInf.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxGingivalInf.AutoSize = true;
            this.chkbxGingivalInf.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxGingivalInf.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkbxGingivalInf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxGingivalInf.Image = global::BASEDiag.Properties.Resources.SourireGingivalInf;
            this.chkbxGingivalInf.Location = new System.Drawing.Point(6, 82);
            this.chkbxGingivalInf.Name = "chkbxGingivalInf";
            this.chkbxGingivalInf.Size = new System.Drawing.Size(71, 71);
            this.chkbxGingivalInf.TabIndex = 7;
            this.chkbxGingivalInf.UseVisualStyleBackColor = true;
            this.chkbxGingivalInf.CheckedChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged_1);
            this.chkbxGingivalInf.Click += new System.EventHandler(this.chkbxGingivalInf_Click);
            this.chkbxGingivalInf.Paint += new System.Windows.Forms.PaintEventHandler(this.chkbxLabial_Paint);
            // 
            // chkbxGingivalSup
            // 
            this.chkbxGingivalSup.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxGingivalSup.AutoSize = true;
            this.chkbxGingivalSup.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkbxGingivalSup.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkbxGingivalSup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkbxGingivalSup.Image = global::BASEDiag.Properties.Resources.SourireGingivalSup;
            this.chkbxGingivalSup.Location = new System.Drawing.Point(6, 10);
            this.chkbxGingivalSup.Name = "chkbxGingivalSup";
            this.chkbxGingivalSup.Size = new System.Drawing.Size(71, 71);
            this.chkbxGingivalSup.TabIndex = 6;
            this.chkbxGingivalSup.UseVisualStyleBackColor = true;
            this.chkbxGingivalSup.CheckedChanged += new System.EventHandler(this.chkbxGingivalSup_CheckedChanged_1);
            this.chkbxGingivalSup.Click += new System.EventHandler(this.chkbxGingivalInf_Click);
            this.chkbxGingivalSup.Paint += new System.Windows.Forms.PaintEventHandler(this.chkbxLabial_Paint);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(68, 512);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(106, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 17;
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
            this.lblTitre.Location = new System.Drawing.Point(12, 57);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(330, 23);
            this.lblTitre.TabIndex = 20;
            this.lblTitre.Text = "Sourire";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(348, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 283);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 135);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 144);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 136);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            this.lstBxObjectifs.SelectedIndexChanged += new System.EventHandler(this.lstBxObjectifs_SelectedIndexChanged);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiag.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(624, 1);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 14;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiag.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(180, 514);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 22;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::BASEDiag.Properties.Resources.Imprimer;
            this.btnPrint.Location = new System.Drawing.Point(236, 514);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 50);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(568, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiag.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 514);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 16;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiag.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(292, 514);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 15;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 0);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(550, 51);
            this.barrePatient1.TabIndex = 12;
            // 
            // imageCtrl1
            // 
            this.imageCtrl1.AllowDrop = true;
            this.imageCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageCtrl1.AngleDeRotationPhoto = 0F;
            this.imageCtrl1.AngleDeRotationRadio = 0F;
            this.imageCtrl1.Brightness = 0F;
            this.imageCtrl1.Contraste = 1F;
            this.imageCtrl1.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.imageCtrl1.DrawPointName = false;
            this.imageCtrl1.file = null;
            this.imageCtrl1.HelpFolder = ".\\";
            this.imageCtrl1.HelpImage = null;
            this.imageCtrl1.Location = new System.Drawing.Point(12, 83);
            this.imageCtrl1.Name = "imageCtrl1";
            this.imageCtrl1.PhotoImage = null;
            this.imageCtrl1.RadioImage = null;
            this.imageCtrl1.ReadOnly = false;
            this.imageCtrl1.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imageCtrl1.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imageCtrl1.Size = new System.Drawing.Size(330, 423);
            this.imageCtrl1.Synchronized = false;
            this.imageCtrl1.TabIndex = 0;
            this.imageCtrl1.TextIfNoImage = "Pas d\'image";
            this.imageCtrl1.Transparence = 0.99D;
            this.imageCtrl1.zoomPhoto = 1F;
            this.imageCtrl1.zoomRadio = 1F;
            this.imageCtrl1.OnRadioChanged += new System.EventHandler(this.imageCtrl1_OnRadioChanged);
            this.imageCtrl1.Load += new System.EventHandler(this.imageCtrl1_load);
            // 
            // FrmAnalyse6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(686, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.imageCtrl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse6";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analyse du sourire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse5_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse5_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BASEDiag.Ctrls.ImageCtrlAgg imageCtrl1;
        private BASEDiag.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkbxLabial;
        private System.Windows.Forms.CheckBox chkbxGingivalInf;
        private System.Windows.Forms.CheckBox chkbxGingivalSup;
        private System.Windows.Forms.CheckBox chkbxNormal;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblTitre;
        private ControlsLibrary.TreeViewIconCbx tbGingivSup;
        private ControlsLibrary.TreeViewIconCbx tbLabial;
        private ControlsLibrary.TreeViewIconCbx tbGingivInf;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
    }
}