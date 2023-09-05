namespace BASEDiagAdulte
{
    partial class FrmWizardDevis
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChoixDevis = new System.Windows.Forms.TabPage();
            this.tvTemplate = new BASEDiagAdulte.Ctrls.TreeViewIcon();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstbxCorrespondant = new System.Windows.Forms.ListBox();
            this.lstbxDestinataires = new System.Windows.Forms.ListBox();
            this.txtbxSearchCorrespondant = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstBxPraticien = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabChoixDevis.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabChoixDevis);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(244, 485);
            this.tabControl1.TabIndex = 0;
            // 
            // tabChoixDevis
            // 
            this.tabChoixDevis.Controls.Add(this.tvTemplate);
            this.tabChoixDevis.Location = new System.Drawing.Point(4, 22);
            this.tabChoixDevis.Name = "tabChoixDevis";
            this.tabChoixDevis.Padding = new System.Windows.Forms.Padding(3);
            this.tabChoixDevis.Size = new System.Drawing.Size(236, 459);
            this.tabChoixDevis.TabIndex = 0;
            this.tabChoixDevis.Text = "Choix Devis";
            this.tabChoixDevis.UseVisualStyleBackColor = true;
            // 
            // tvTemplate
            // 
            this.tvTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTemplate.BackColor = System.Drawing.Color.White;
            this.tvTemplate.ButtonHeight = 20;
            this.tvTemplate.ButtonWidth = 200;
            this.tvTemplate.Location = new System.Drawing.Point(6, 6);
            this.tvTemplate.Name = "tvTemplate";
            this.tvTemplate.Size = new System.Drawing.Size(224, 447);
            this.tvTemplate.TabIndex = 0;
            this.tvTemplate.OnSelected += new System.EventHandler(this.tvTemplate_OnSelected);
            this.tvTemplate.OnChangeLevel += new System.EventHandler(this.tvTemplate_OnChangeLevel);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstbxCorrespondant);
            this.tabPage1.Controls.Add(this.lstbxDestinataires);
            this.tabPage1.Controls.Add(this.txtbxSearchCorrespondant);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(236, 459);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Choix Destinataire";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstbxCorrespondant
            // 
            this.lstbxCorrespondant.FormattingEnabled = true;
            this.lstbxCorrespondant.Location = new System.Drawing.Point(6, 32);
            this.lstbxCorrespondant.Name = "lstbxCorrespondant";
            this.lstbxCorrespondant.Size = new System.Drawing.Size(222, 225);
            this.lstbxCorrespondant.TabIndex = 3;
            this.lstbxCorrespondant.Click += new System.EventHandler(this.lstbxCorrespondant_Click);
            // 
            // lstbxDestinataires
            // 
            this.lstbxDestinataires.FormattingEnabled = true;
            this.lstbxDestinataires.Location = new System.Drawing.Point(6, 271);
            this.lstbxDestinataires.Name = "lstbxDestinataires";
            this.lstbxDestinataires.Size = new System.Drawing.Size(222, 173);
            this.lstbxDestinataires.TabIndex = 2;
            this.lstbxDestinataires.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstbxDestinataires_KeyDown);
            // 
            // txtbxSearchCorrespondant
            // 
            this.txtbxSearchCorrespondant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxSearchCorrespondant.Location = new System.Drawing.Point(6, 6);
            this.txtbxSearchCorrespondant.Name = "txtbxSearchCorrespondant";
            this.txtbxSearchCorrespondant.Size = new System.Drawing.Size(224, 20);
            this.txtbxSearchCorrespondant.TabIndex = 1;
            this.txtbxSearchCorrespondant.TextChanged += new System.EventHandler(this.txtbxSearchCorrespondant_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstBxPraticien);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(236, 459);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Praticien";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstBxPraticien
            // 
            this.lstBxPraticien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBxPraticien.FormattingEnabled = true;
            this.lstBxPraticien.Location = new System.Drawing.Point(6, 6);
            this.lstBxPraticien.Name = "lstBxPraticien";
            this.lstBxPraticien.Size = new System.Drawing.Size(224, 433);
            this.lstBxPraticien.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(100, 503);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(181, 503);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmWizardDevis
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(268, 540);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmWizardDevis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devis";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmWizardDevis_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabChoixDevis.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChoixDevis;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private BASEDiagAdulte.Ctrls.TreeViewIcon tvTemplate;
        private System.Windows.Forms.TextBox txtbxSearchCorrespondant;
        private System.Windows.Forms.ListBox lstBxPraticien;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstbxCorrespondant;
        private System.Windows.Forms.ListBox lstbxDestinataires;
    }
}