namespace BASEDiagAdulte
{
    partial class frmChoixASsPrat
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlAssPrat = new System.Windows.Forms.Panel();
            this.cbxAssResp = new BaseCommonControls.BAS.WizardPraticienAssistantes();
            this.cbxPratResp = new BaseCommonControls.BAS.WizardPraticienAssistantes();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpdebuttrmnt = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.pnlAssPrat.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlAssPrat);
            this.pnlContainer.Controls.Add(this.pnlDates);
            this.pnlContainer.Location = new System.Drawing.Point(13, 13);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(744, 100);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlAssPrat
            // 
            this.pnlAssPrat.Controls.Add(this.cbxAssResp);
            this.pnlAssPrat.Controls.Add(this.cbxPratResp);
            this.pnlAssPrat.Controls.Add(this.label10);
            this.pnlAssPrat.Controls.Add(this.label9);
            this.pnlAssPrat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAssPrat.Location = new System.Drawing.Point(0, 0);
            this.pnlAssPrat.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAssPrat.Name = "pnlAssPrat";
            this.pnlAssPrat.Size = new System.Drawing.Size(744, 100);
            this.pnlAssPrat.TabIndex = 0;
            // 
            // cbxAssResp
            // 
            this.cbxAssResp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAssResp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbxAssResp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxAssResp.Font = new System.Drawing.Font("Garamond", 11F);
            this.cbxAssResp.IncludeAssistantes = true;
            this.cbxAssResp.IncludePraticien = false;
            this.cbxAssResp.Location = new System.Drawing.Point(196, 51);
            this.cbxAssResp.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAssResp.Name = "cbxAssResp";
            this.cbxAssResp.SelectedNode = null;
            this.cbxAssResp.SelectedValue = null;
            this.cbxAssResp.Size = new System.Drawing.Size(351, 39);
            this.cbxAssResp.TabIndex = 73;
            this.cbxAssResp.Text = "<Aucune selection>";
            this.cbxAssResp.UseVisualStyleBackColor = false;
            this.cbxAssResp.WindowHeight = 200;
            this.cbxAssResp.WindowWidth = 400;
            this.cbxAssResp.WrapMode = true;
            // 
            // cbxPratResp
            // 
            this.cbxPratResp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPratResp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbxPratResp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxPratResp.Font = new System.Drawing.Font("Garamond", 11F);
            this.cbxPratResp.IncludeAssistantes = false;
            this.cbxPratResp.IncludePraticien = true;
            this.cbxPratResp.Location = new System.Drawing.Point(196, 4);
            this.cbxPratResp.Margin = new System.Windows.Forms.Padding(4);
            this.cbxPratResp.Name = "cbxPratResp";
            this.cbxPratResp.SelectedNode = null;
            this.cbxPratResp.SelectedValue = null;
            this.cbxPratResp.Size = new System.Drawing.Size(351, 39);
            this.cbxPratResp.TabIndex = 72;
            this.cbxPratResp.Text = "<Aucune selection>";
            this.cbxPratResp.UseVisualStyleBackColor = false;
            this.cbxPratResp.WindowHeight = 200;
            this.cbxPratResp.WindowWidth = 400;
            this.cbxPratResp.WrapMode = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 63);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 17);
            this.label10.TabIndex = 71;
            this.label10.Text = "Assistante responsable : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 17);
            this.label9.TabIndex = 70;
            this.label9.Text = "Praticien responsable : ";
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.label3);
            this.pnlDates.Controls.Add(this.dtpdebuttrmnt);
            this.pnlDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDates.Location = new System.Drawing.Point(0, 0);
            this.pnlDates.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(744, 100);
            this.pnlDates.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Début de traitement envisagé pour le ";
            // 
            // dtpdebuttrmnt
            // 
            this.dtpdebuttrmnt.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdebuttrmnt.Location = new System.Drawing.Point(297, 38);
            this.dtpdebuttrmnt.Name = "dtpdebuttrmnt";
            this.dtpdebuttrmnt.Size = new System.Drawing.Size(223, 25);
            this.dtpdebuttrmnt.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(615, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Suivant";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(466, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 41);
            this.button2.TabIndex = 2;
            this.button2.Text = "Précedent";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmWizardCreationDevis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(770, 176);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmWizardCreationDevis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Assistante et Praticien";
            this.Load += new System.EventHandler(this.frmWizardCreationDevis_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlAssPrat.ResumeLayout(false);
            this.pnlAssPrat.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.Panel pnlAssPrat;
        private BaseCommonControls.BAS.WizardPraticienAssistantes cbxAssResp;
        private BaseCommonControls.BAS.WizardPraticienAssistantes cbxPratResp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpdebuttrmnt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}