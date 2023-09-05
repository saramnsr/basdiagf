namespace BASEDiag
{
    partial class FirstScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstScreen));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.BtnPreAnalyse = new System.Windows.Forms.ToolStripButton();
            this.BtnMasqueFacial = new System.Windows.Forms.ToolStripButton();
            this.BtnSourireFace = new System.Windows.Forms.ToolStripButton();
            this.BtnOcclusal = new System.Windows.Forms.ToolStripButton();
            this.BtnFonctionnel = new System.Windows.Forms.ToolStripButton();
            this.BtnArcades = new System.Windows.Forms.ToolStripButton();
            this.BtnSourires = new System.Windows.Forms.ToolStripButton();
            this.BtnProfil = new System.Windows.Forms.ToolStripButton();
            this.BtnRadio = new System.Windows.Forms.ToolStripButton();
            this.BtnPano = new System.Windows.Forms.ToolStripButton();
            this.btnResultat = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFauteuil = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.imgCtrl = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.btnFauteuil);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.button1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.btnclose);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.imgCtrl);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.barrePatient1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1061, 282);
            this.toolStripContainer1.ContentPanel.Load += new System.EventHandler(this.toolStripContainer1_ContentPanel_Load);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1061, 394);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(80, 80);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.BtnPreAnalyse,
            this.BtnMasqueFacial,
            this.BtnSourireFace,
            this.BtnOcclusal,
            this.BtnFonctionnel,
            this.BtnArcades,
            this.BtnSourires,
            this.BtnProfil,
            this.BtnRadio,
            this.BtnPano,
            this.btnResultat});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1042, 87);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::BASEDiag.Properties.Resources.Patient;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(84, 84);
            this.toolStripButton1.Text = "Choix du patient";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // BtnPreAnalyse
            // 
            this.BtnPreAnalyse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPreAnalyse.Image = global::BASEDiag.Properties.Resources.PreAnalyse;
            this.BtnPreAnalyse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPreAnalyse.Name = "BtnPreAnalyse";
            this.BtnPreAnalyse.Size = new System.Drawing.Size(84, 84);
            this.BtnPreAnalyse.Text = "PreAnalyse";
            this.BtnPreAnalyse.Click += new System.EventHandler(this.BtnPreAnalyse_Click);
            // 
            // BtnMasqueFacial
            // 
            this.BtnMasqueFacial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnMasqueFacial.Image = global::BASEDiag.Properties.Resources.MasqueFacial;
            this.BtnMasqueFacial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnMasqueFacial.Name = "BtnMasqueFacial";
            this.BtnMasqueFacial.Size = new System.Drawing.Size(84, 84);
            this.BtnMasqueFacial.Text = "Masque Facial";
            this.BtnMasqueFacial.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // BtnSourireFace
            // 
            this.BtnSourireFace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSourireFace.Image = global::BASEDiag.Properties.Resources.AnalyseSourire;
            this.BtnSourireFace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSourireFace.Name = "BtnSourireFace";
            this.BtnSourireFace.Size = new System.Drawing.Size(84, 84);
            this.BtnSourireFace.Text = "Face Sourire";
            this.BtnSourireFace.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // BtnOcclusal
            // 
            this.BtnOcclusal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnOcclusal.Image = global::BASEDiag.Properties.Resources.Occlusal;
            this.BtnOcclusal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnOcclusal.Name = "BtnOcclusal";
            this.BtnOcclusal.Size = new System.Drawing.Size(84, 84);
            this.BtnOcclusal.Text = "Occlusal";
            this.BtnOcclusal.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // BtnFonctionnel
            // 
            this.BtnFonctionnel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnFonctionnel.Image = global::BASEDiag.Properties.Resources.Fonctionnel;
            this.BtnFonctionnel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnFonctionnel.Name = "BtnFonctionnel";
            this.BtnFonctionnel.Size = new System.Drawing.Size(84, 84);
            this.BtnFonctionnel.Text = "Fonctionnel";
            this.BtnFonctionnel.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // BtnArcades
            // 
            this.BtnArcades.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnArcades.Image = global::BASEDiag.Properties.Resources.Arcades;
            this.BtnArcades.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnArcades.Name = "BtnArcades";
            this.BtnArcades.Size = new System.Drawing.Size(84, 84);
            this.BtnArcades.Text = "Arcades";
            this.BtnArcades.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // BtnSourires
            // 
            this.BtnSourires.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSourires.Image = global::BASEDiag.Properties.Resources.Sourire;
            this.BtnSourires.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSourires.Name = "BtnSourires";
            this.BtnSourires.Size = new System.Drawing.Size(84, 84);
            this.BtnSourires.Text = "Sourire";
            this.BtnSourires.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // BtnProfil
            // 
            this.BtnProfil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnProfil.Image = global::BASEDiag.Properties.Resources.Profil1;
            this.BtnProfil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnProfil.Name = "BtnProfil";
            this.BtnProfil.Size = new System.Drawing.Size(84, 84);
            this.BtnProfil.Text = "Profil";
            this.BtnProfil.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // BtnRadio
            // 
            this.BtnRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnRadio.Image = global::BASEDiag.Properties.Resources.radio;
            this.BtnRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnRadio.Name = "BtnRadio";
            this.BtnRadio.Size = new System.Drawing.Size(84, 84);
            this.BtnRadio.Text = "Radio";
            this.BtnRadio.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // BtnPano
            // 
            this.BtnPano.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPano.Image = global::BASEDiag.Properties.Resources.Pano;
            this.BtnPano.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPano.Name = "BtnPano";
            this.BtnPano.Size = new System.Drawing.Size(84, 84);
            this.BtnPano.Text = "Panoramique";
            this.BtnPano.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // btnResultat
            // 
            this.btnResultat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResultat.Image = global::BASEDiag.Properties.Resources.Resultats;
            this.btnResultat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResultat.Name = "btnResultat";
            this.btnResultat.Size = new System.Drawing.Size(84, 84);
            this.btnResultat.Text = "Résultats";
            this.btnResultat.Click += new System.EventHandler(this.btnResultat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Enfant";
            // 
            // btnFauteuil
            // 
            this.btnFauteuil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFauteuil.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFauteuil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFauteuil.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFauteuil.Location = new System.Drawing.Point(1008, 59);
            this.btnFauteuil.Name = "btnFauteuil";
            this.btnFauteuil.Size = new System.Drawing.Size(50, 21);
            this.btnFauteuil.TabIndex = 10;
            this.btnFauteuil.Text = "F1";
            this.btnFauteuil.UseVisualStyleBackColor = true;
            this.btnFauteuil.Click += new System.EventHandler(this.btnFauteuil_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(952, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.ForeColor = System.Drawing.Color.Black;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(1008, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 6;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // imgCtrl
            // 
            this.imgCtrl.AllowDrop = true;
            this.imgCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgCtrl.AngleDeRotationPhoto = 0F;
            this.imgCtrl.AngleDeRotationRadio = 0F;
            this.imgCtrl.BackColor = System.Drawing.Color.White;
            this.imgCtrl.Brightness = 0F;
            this.imgCtrl.Contraste = 1F;
            this.imgCtrl.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.imgCtrl.DrawPointName = false;
            this.imgCtrl.file = null;
            this.imgCtrl.HelpFolder = ".\\";
            this.imgCtrl.HelpImage = null;
            this.imgCtrl.Location = new System.Drawing.Point(12, 59);
            this.imgCtrl.Name = "imgCtrl";
            this.imgCtrl.PhotoImage = null;
            this.imgCtrl.RadioImage = null;
            this.imgCtrl.ReadOnly = false;
            this.imgCtrl.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imgCtrl.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imgCtrl.Size = new System.Drawing.Size(1046, 220);
            this.imgCtrl.Synchronized = false;
            this.imgCtrl.TabIndex = 2;
            this.imgCtrl.TextIfNoImage = "Sourire Face";
            this.imgCtrl.Transparence = 0.99D;
            this.imgCtrl.zoomPhoto = 1F;
            this.imgCtrl.zoomRadio = 1F;
            this.imgCtrl.Load += new System.EventHandler(this.imgCtrl_Load);
            this.imgCtrl.DragDrop += new System.Windows.Forms.DragEventHandler(this.imgCtrl_DragDrop);
            this.imgCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imgCtrl_KeyDown);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(3, 3);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(943, 50);
            this.barrePatient1.TabIndex = 1;
            // 
            // FirstScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(1061, 394);
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FirstScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BAS Diag";
            this.Activated += new System.EventHandler(this.FirstScreen_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FirstScreen_FormClosing);
            this.Load += new System.EventHandler(this.FirstScreen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FirstScreen_KeyDown);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private BASEDiag.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.ToolStripButton BtnMasqueFacial;
        private System.Windows.Forms.ToolStripButton BtnSourireFace;
        private System.Windows.Forms.ToolStripButton BtnOcclusal;
        private System.Windows.Forms.ToolStripButton BtnArcades;
        private System.Windows.Forms.ToolStripButton BtnSourires;
        private System.Windows.Forms.ToolStripButton BtnProfil;
        private System.Windows.Forms.ToolStripButton BtnRadio;
        private System.Windows.Forms.ToolStripButton BtnPano;
        private BASEDiag.Ctrls.ImageCtrlAgg imgCtrl;
        private System.Windows.Forms.ToolStripButton BtnPreAnalyse;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton btnResultat;
        private System.Windows.Forms.Button btnFauteuil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton BtnFonctionnel;
    }
}

