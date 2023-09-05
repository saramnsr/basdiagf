namespace BASEDiag
{
    partial class FrmPreAnalyse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreAnalyse));
            this.pnlAnalyse = new System.Windows.Forms.Panel();
            this.ImgRadioProfil = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.BTnNext = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ImgProfil = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.ImgFace = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.imgOccGauche = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.imgOccFace = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.imgOccDroit = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.imgPano = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.txtbxresumeQ1CS = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.motifDeConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFullScreen = new System.Windows.Forms.Panel();
            this.lnkCloseFullScreen = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.pnlAnalyse.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlFullScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAnalyse
            // 
            this.pnlAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAnalyse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAnalyse.Controls.Add(this.ImgRadioProfil);
            this.pnlAnalyse.Controls.Add(this.BTnNext);
            this.pnlAnalyse.Controls.Add(this.btnPrint);
            this.pnlAnalyse.Controls.Add(this.ImgProfil);
            this.pnlAnalyse.Controls.Add(this.ImgFace);
            this.pnlAnalyse.Controls.Add(this.imgOccGauche);
            this.pnlAnalyse.Controls.Add(this.imgOccFace);
            this.pnlAnalyse.Controls.Add(this.imgOccDroit);
            this.pnlAnalyse.Controls.Add(this.imgPano);
            this.pnlAnalyse.Location = new System.Drawing.Point(268, 85);
            this.pnlAnalyse.Name = "pnlAnalyse";
            this.pnlAnalyse.Size = new System.Drawing.Size(372, 411);
            this.pnlAnalyse.TabIndex = 8;
            this.pnlAnalyse.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAnalyse_Paint);
            // 
            // ImgRadioProfil
            // 
            this.ImgRadioProfil.AllowDrop = true;
            this.ImgRadioProfil.AngleDeRotationPhoto = 0F;
            this.ImgRadioProfil.AngleDeRotationRadio = 0F;
            this.ImgRadioProfil.Brightness = 0F;
            this.ImgRadioProfil.Contraste = 1F;
            this.ImgRadioProfil.DrawPointName = false;
            this.ImgRadioProfil.file = null;
            this.ImgRadioProfil.HelpFolder = ".\\";
            this.ImgRadioProfil.HelpImage = null;
            this.ImgRadioProfil.Location = new System.Drawing.Point(0, 0);
            this.ImgRadioProfil.Name = "ImgRadioProfil";
            this.ImgRadioProfil.PhotoImage = null;
            this.ImgRadioProfil.RadioImage = null;
            this.ImgRadioProfil.ReadOnly = false;
            this.ImgRadioProfil.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.ImgRadioProfil.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.ImgRadioProfil.Size = new System.Drawing.Size(312, 56);
            this.ImgRadioProfil.Synchronized = false;
            this.ImgRadioProfil.TabIndex = 14;
            this.ImgRadioProfil.TextIfNoImage = "Pas d\'image";
            this.ImgRadioProfil.Transparence = 0.99D;
            this.ImgRadioProfil.zoomPhoto = 1F;
            this.ImgRadioProfil.zoomRadio = 1F;
            this.ImgRadioProfil.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.ImgRadioProfil.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = ((System.Drawing.Image)(resources.GetObject("BTnNext.Image")));
            this.BTnNext.Location = new System.Drawing.Point(321, 360);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 14;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(104, 36);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Imprimer";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ImgProfil
            // 
            this.ImgProfil.AllowDrop = true;
            this.ImgProfil.AngleDeRotationPhoto = 0F;
            this.ImgProfil.AngleDeRotationRadio = 0F;
            this.ImgProfil.Brightness = 0F;
            this.ImgProfil.Contraste = 1F;
            this.ImgProfil.DrawPointName = false;
            this.ImgProfil.file = null;
            this.ImgProfil.HelpFolder = ".\\";
            this.ImgProfil.HelpImage = null;
            this.ImgProfil.Location = new System.Drawing.Point(0, 0);
            this.ImgProfil.Name = "ImgProfil";
            this.ImgProfil.PhotoImage = null;
            this.ImgProfil.RadioImage = null;
            this.ImgProfil.ReadOnly = false;
            this.ImgProfil.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.ImgProfil.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.ImgProfil.Size = new System.Drawing.Size(312, 36);
            this.ImgProfil.Synchronized = false;
            this.ImgProfil.TabIndex = 0;
            this.ImgProfil.TextIfNoImage = "Pas d\'image";
            this.ImgProfil.Transparence = 0.99D;
            this.ImgProfil.zoomPhoto = 1F;
            this.ImgProfil.zoomRadio = 1F;
            this.ImgProfil.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.ImgProfil.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // ImgFace
            // 
            this.ImgFace.AllowDrop = true;
            this.ImgFace.AngleDeRotationPhoto = 0F;
            this.ImgFace.AngleDeRotationRadio = 0F;
            this.ImgFace.Brightness = 0F;
            this.ImgFace.Contraste = 1F;
            this.ImgFace.DrawPointName = false;
            this.ImgFace.file = null;
            this.ImgFace.HelpFolder = ".\\";
            this.ImgFace.HelpImage = null;
            this.ImgFace.Location = new System.Drawing.Point(0, 0);
            this.ImgFace.Name = "ImgFace";
            this.ImgFace.PhotoImage = null;
            this.ImgFace.RadioImage = null;
            this.ImgFace.ReadOnly = false;
            this.ImgFace.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.ImgFace.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.ImgFace.Size = new System.Drawing.Size(312, 56);
            this.ImgFace.Synchronized = false;
            this.ImgFace.TabIndex = 1;
            this.ImgFace.TextIfNoImage = "Pas d\'image";
            this.ImgFace.Transparence = 0.99D;
            this.ImgFace.zoomPhoto = 1F;
            this.ImgFace.zoomRadio = 1F;
            this.ImgFace.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.ImgFace.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // imgOccGauche
            // 
            this.imgOccGauche.AllowDrop = true;
            this.imgOccGauche.AngleDeRotationPhoto = 0F;
            this.imgOccGauche.AngleDeRotationRadio = 0F;
            this.imgOccGauche.Brightness = 0F;
            this.imgOccGauche.Contraste = 1F;
            this.imgOccGauche.DrawPointName = false;
            this.imgOccGauche.file = null;
            this.imgOccGauche.HelpFolder = ".\\";
            this.imgOccGauche.HelpImage = null;
            this.imgOccGauche.Location = new System.Drawing.Point(3, 160);
            this.imgOccGauche.Name = "imgOccGauche";
            this.imgOccGauche.PhotoImage = null;
            this.imgOccGauche.RadioImage = null;
            this.imgOccGauche.ReadOnly = false;
            this.imgOccGauche.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imgOccGauche.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imgOccGauche.Size = new System.Drawing.Size(234, 195);
            this.imgOccGauche.Synchronized = false;
            this.imgOccGauche.TabIndex = 3;
            this.imgOccGauche.TextIfNoImage = "Pas d\'image";
            this.imgOccGauche.Transparence = 0.99D;
            this.imgOccGauche.zoomPhoto = 1F;
            this.imgOccGauche.zoomRadio = 1F;
            this.imgOccGauche.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.imgOccGauche.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // imgOccFace
            // 
            this.imgOccFace.AllowDrop = true;
            this.imgOccFace.AngleDeRotationPhoto = 0F;
            this.imgOccFace.AngleDeRotationRadio = 0F;
            this.imgOccFace.Brightness = 0F;
            this.imgOccFace.Contraste = 1F;
            this.imgOccFace.DrawPointName = false;
            this.imgOccFace.file = null;
            this.imgOccFace.HelpFolder = ".\\";
            this.imgOccFace.HelpImage = null;
            this.imgOccFace.Location = new System.Drawing.Point(0, 0);
            this.imgOccFace.Name = "imgOccFace";
            this.imgOccFace.PhotoImage = null;
            this.imgOccFace.RadioImage = null;
            this.imgOccFace.ReadOnly = false;
            this.imgOccFace.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imgOccFace.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imgOccFace.Size = new System.Drawing.Size(234, 91);
            this.imgOccFace.Synchronized = false;
            this.imgOccFace.TabIndex = 4;
            this.imgOccFace.TextIfNoImage = "Pas d\'image";
            this.imgOccFace.Transparence = 0.99D;
            this.imgOccFace.zoomPhoto = 1F;
            this.imgOccFace.zoomRadio = 1F;
            this.imgOccFace.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.imgOccFace.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // imgOccDroit
            // 
            this.imgOccDroit.AllowDrop = true;
            this.imgOccDroit.AngleDeRotationPhoto = 0F;
            this.imgOccDroit.AngleDeRotationRadio = 0F;
            this.imgOccDroit.Brightness = 0F;
            this.imgOccDroit.Contraste = 1F;
            this.imgOccDroit.DrawPointName = false;
            this.imgOccDroit.file = null;
            this.imgOccDroit.HelpFolder = ".\\";
            this.imgOccDroit.HelpImage = null;
            this.imgOccDroit.Location = new System.Drawing.Point(0, 0);
            this.imgOccDroit.Name = "imgOccDroit";
            this.imgOccDroit.PhotoImage = null;
            this.imgOccDroit.RadioImage = null;
            this.imgOccDroit.ReadOnly = false;
            this.imgOccDroit.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imgOccDroit.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imgOccDroit.Size = new System.Drawing.Size(234, 108);
            this.imgOccDroit.Synchronized = false;
            this.imgOccDroit.TabIndex = 5;
            this.imgOccDroit.TextIfNoImage = "Pas d\'image";
            this.imgOccDroit.Transparence = 0.99D;
            this.imgOccDroit.zoomPhoto = 1F;
            this.imgOccDroit.zoomRadio = 1F;
            this.imgOccDroit.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.imgOccDroit.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // imgPano
            // 
            this.imgPano.AllowDrop = true;
            this.imgPano.AngleDeRotationPhoto = 0F;
            this.imgPano.AngleDeRotationRadio = 0F;
            this.imgPano.BackColor = System.Drawing.Color.White;
            this.imgPano.Brightness = 0F;
            this.imgPano.Contraste = 1F;
            this.imgPano.DrawPointName = false;
            this.imgPano.file = null;
            this.imgPano.HelpFolder = ".\\";
            this.imgPano.HelpImage = null;
            this.imgPano.Location = new System.Drawing.Point(23, 0);
            this.imgPano.Name = "imgPano";
            this.imgPano.PhotoImage = null;
            this.imgPano.RadioImage = null;
            this.imgPano.ReadOnly = false;
            this.imgPano.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imgPano.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imgPano.Size = new System.Drawing.Size(636, 338);
            this.imgPano.Synchronized = false;
            this.imgPano.TabIndex = 6;
            this.imgPano.TextIfNoImage = "Pas d\'image";
            this.imgPano.Transparence = 0.99D;
            this.imgPano.zoomPhoto = 1F;
            this.imgPano.zoomRadio = 1F;
            this.imgPano.OnRadioChanged += new System.EventHandler(this.imgPano_OnRadioChanged);
            this.imgPano.DoubleClick += new System.EventHandler(this.ImgProfil_DoubleClick);
            // 
            // txtbxresumeQ1CS
            // 
            this.txtbxresumeQ1CS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtbxresumeQ1CS.BackColor = System.Drawing.Color.White;
            this.txtbxresumeQ1CS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbxresumeQ1CS.ContextMenuStrip = this.contextMenuStrip1;
            this.txtbxresumeQ1CS.Location = new System.Drawing.Point(12, 85);
            this.txtbxresumeQ1CS.Multiline = true;
            this.txtbxresumeQ1CS.Name = "txtbxresumeQ1CS";
            this.txtbxresumeQ1CS.ReadOnly = true;
            this.txtbxresumeQ1CS.Size = new System.Drawing.Size(250, 411);
            this.txtbxresumeQ1CS.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motifDeConsultationToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(335, 26);
            // 
            // motifDeConsultationToolStripMenuItem
            // 
            this.motifDeConsultationToolStripMenuItem.Name = "motifDeConsultationToolStripMenuItem";
            this.motifDeConsultationToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.motifDeConsultationToolStripMenuItem.Text = "Transformer la selection en motif de consultation";
            this.motifDeConsultationToolStripMenuItem.Click += new System.EventHandler(this.motifDeConsultationToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Résumé Q1CS";
            // 
            // pnlFullScreen
            // 
            this.pnlFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFullScreen.Controls.Add(this.lnkCloseFullScreen);
            this.pnlFullScreen.Location = new System.Drawing.Point(12, 276);
            this.pnlFullScreen.Name = "pnlFullScreen";
            this.pnlFullScreen.Size = new System.Drawing.Size(628, 220);
            this.pnlFullScreen.TabIndex = 12;
            this.pnlFullScreen.Visible = false;
            this.pnlFullScreen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlFullScreen_MouseDoubleClick);
            // 
            // lnkCloseFullScreen
            // 
            this.lnkCloseFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkCloseFullScreen.AutoSize = true;
            this.lnkCloseFullScreen.Location = new System.Drawing.Point(586, 3);
            this.lnkCloseFullScreen.Name = "lnkCloseFullScreen";
            this.lnkCloseFullScreen.Size = new System.Drawing.Size(39, 13);
            this.lnkCloseFullScreen.TabIndex = 0;
            this.lnkCloseFullScreen.TabStop = true;
            this.lnkCloseFullScreen.Text = "Fermer";
            this.lnkCloseFullScreen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCloseFullScreen_LinkClicked);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(534, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiag.Properties.Resources.close;
            this.btnclose.Location = new System.Drawing.Point(590, 12);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 11;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 12);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(516, 50);
            this.barrePatient1.TabIndex = 7;
            // 
            // FrmPreAnalyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(652, 508);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlFullScreen);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbxresumeQ1CS);
            this.Controls.Add(this.pnlAnalyse);
            this.Controls.Add(this.barrePatient1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPreAnalyse";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmPreAnalyse";
            this.Load += new System.EventHandler(this.FrmPreAnalyse_Load);
            this.Resize += new System.EventHandler(this.FrmPreAnalyse_Resize);
            this.pnlAnalyse.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlFullScreen.ResumeLayout(false);
            this.pnlFullScreen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiag.Ctrls.ImageCtrlAgg ImgProfil;
        private BASEDiag.Ctrls.ImageCtrlAgg ImgFace;
        private BASEDiag.Ctrls.ImageCtrlAgg imgOccGauche;
        private BASEDiag.Ctrls.ImageCtrlAgg imgOccFace;
        private BASEDiag.Ctrls.ImageCtrlAgg imgOccDroit;
        private BASEDiag.Ctrls.ImageCtrlAgg imgPano;
        private BASEDiag.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.Panel pnlAnalyse;
        private System.Windows.Forms.TextBox txtbxresumeQ1CS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Panel pnlFullScreen;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.LinkLabel lnkCloseFullScreen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem motifDeConsultationToolStripMenuItem;
        private Ctrls.ImageCtrlAgg ImgRadioProfil;
    }
}