namespace BaseCommonControls
{
    partial class ChoiceMedical
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lstProfessionAutre = new BaseCommonControls.SlidingList();
            this.lstProfessionPara = new BaseCommonControls.SlidingList();
            this.lstprofessionMed = new BaseCommonControls.SlidingList();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(390, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profession";
            // 
            // lstProfessionAutre
            // 
            this.lstProfessionAutre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProfessionAutre.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.lstProfessionAutre.imagelist = null;
            this.lstProfessionAutre.Location = new System.Drawing.Point(18, 335);
            this.lstProfessionAutre.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.lstProfessionAutre.MultiSelectMode = false;
            this.lstProfessionAutre.Name = "lstProfessionAutre";
            this.lstProfessionAutre.Size = new System.Drawing.Size(874, 104);
            this.lstProfessionAutre.TabIndex = 22;
            this.lstProfessionAutre.WrapMode = false;
            this.lstProfessionAutre.OnSelectionChange += new System.EventHandler(this.lstprofession_OnSelectionChange);
            // 
            // lstProfessionPara
            // 
            this.lstProfessionPara.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProfessionPara.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.lstProfessionPara.imagelist = null;
            this.lstProfessionPara.Location = new System.Drawing.Point(18, 202);
            this.lstProfessionPara.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.lstProfessionPara.MultiSelectMode = false;
            this.lstProfessionPara.Name = "lstProfessionPara";
            this.lstProfessionPara.Size = new System.Drawing.Size(874, 104);
            this.lstProfessionPara.TabIndex = 21;
            this.lstProfessionPara.WrapMode = false;
            this.lstProfessionPara.OnSelectionChange += new System.EventHandler(this.lstprofession_OnSelectionChange);
            // 
            // lstprofessionMed
            // 
            this.lstprofessionMed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstprofessionMed.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.lstprofessionMed.imagelist = null;
            this.lstprofessionMed.Location = new System.Drawing.Point(18, 68);
            this.lstprofessionMed.Margin = new System.Windows.Forms.Padding(7);
            this.lstprofessionMed.MultiSelectMode = false;
            this.lstprofessionMed.Name = "lstprofessionMed";
            this.lstprofessionMed.Size = new System.Drawing.Size(874, 104);
            this.lstprofessionMed.TabIndex = 20;
            this.lstprofessionMed.WrapMode = false;
            this.lstprofessionMed.OnSelectionChange += new System.EventHandler(this.lstprofession_OnSelectionChange);
            this.lstprofessionMed.Load += new System.EventHandler(this.lstprofessionMed_Load);
            // 
            // ChoiceMedical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lstProfessionAutre);
            this.Controls.Add(this.lstProfessionPara);
            this.Controls.Add(this.lstprofessionMed);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChoiceMedical";
            this.Size = new System.Drawing.Size(900, 490);
            this.Load += new System.EventHandler(this.ChoiceMedical_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private BaseCommonControls.SlidingList lstProfessionAutre;
        private BaseCommonControls.SlidingList lstProfessionPara;
        private BaseCommonControls.SlidingList lstprofessionMed;
    }
}
