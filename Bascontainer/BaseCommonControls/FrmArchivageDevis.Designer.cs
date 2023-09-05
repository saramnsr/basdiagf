namespace BaseCommonControls
{
    partial class FrmArchivageDevis
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtbxArchivageWhy = new System.Windows.Forms.TextBox();
            this.txtbxEmplacement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxEcrivain = new BaseCommonControls.BAS.WizardPraticienAssistantes();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(425, 258);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 64);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(329, 258);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 64);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtbxArchivageWhy
            // 
            this.txtbxArchivageWhy.Location = new System.Drawing.Point(205, 48);
            this.txtbxArchivageWhy.Multiline = true;
            this.txtbxArchivageWhy.Name = "txtbxArchivageWhy";
            this.txtbxArchivageWhy.Size = new System.Drawing.Size(308, 155);
            this.txtbxArchivageWhy.TabIndex = 4;
            // 
            // txtbxEmplacement
            // 
            this.txtbxEmplacement.Location = new System.Drawing.Point(329, 209);
            this.txtbxEmplacement.Name = "txtbxEmplacement";
            this.txtbxEmplacement.Size = new System.Drawing.Size(184, 25);
            this.txtbxEmplacement.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Emplacement de l\'archive : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Qui archive : ";
            // 
            // cbxEcrivain
            // 
            this.cbxEcrivain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbxEcrivain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxEcrivain.Font = new System.Drawing.Font("Garamond", 11F);
            this.cbxEcrivain.IncludeAssistantes = true;
            this.cbxEcrivain.IncludePraticien = true;
            this.cbxEcrivain.Location = new System.Drawing.Point(205, 12);
            this.cbxEcrivain.Name = "cbxEcrivain";
            this.cbxEcrivain.NbLine = 2;
            this.cbxEcrivain.SelectedNode = null;
            this.cbxEcrivain.SelectedValue = null;
            this.cbxEcrivain.Size = new System.Drawing.Size(308, 30);
            this.cbxEcrivain.TabIndex = 5;
            this.cbxEcrivain.Text = "<Qui archive ?>";
            this.cbxEcrivain.UseVisualStyleBackColor = false;
            this.cbxEcrivain.WindowHeight = 200;
            this.cbxEcrivain.WindowWidth = 400;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Raison de l\'archivage : ";
            // 
            // FrmArchivageDevis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 335);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbxEmplacement);
            this.Controls.Add(this.cbxEcrivain);
            this.Controls.Add(this.txtbxArchivageWhy);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmArchivageDevis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archivage du devis";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmArchivageDevis_Load);
            this.Shown += new System.EventHandler(this.FrmArchivageDevis_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtbxArchivageWhy;
        private BaseCommonControls.BAS.WizardPraticienAssistantes cbxEcrivain;
        private System.Windows.Forms.TextBox txtbxEmplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}