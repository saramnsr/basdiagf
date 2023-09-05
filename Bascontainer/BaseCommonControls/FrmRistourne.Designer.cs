namespace BaseCommonControls
{
    partial class FrmRistourne
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
            this.rbMontant = new System.Windows.Forms.RadioButton();
            this.rbpercent = new System.Windows.Forms.RadioButton();
            this.mTxtNouveauTarif = new System.Windows.Forms.MaskedTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mTxtMontantRemise = new System.Windows.Forms.MaskedTextBox();
            this.rbMontantRemise = new System.Windows.Forms.RadioButton();
            this.slidingList1 = new BaseCommonControls.SlidingList();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(433, 173);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 56);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbMontant
            // 
            this.rbMontant.AutoSize = true;
            this.rbMontant.Location = new System.Drawing.Point(23, 35);
            this.rbMontant.Name = "rbMontant";
            this.rbMontant.Size = new System.Drawing.Size(30, 22);
            this.rbMontant.TabIndex = 3;
            this.rbMontant.Text = " ";
            this.rbMontant.UseVisualStyleBackColor = true;
            this.rbMontant.CheckedChanged += new System.EventHandler(this.rbMontant_CheckedChanged);
            // 
            // rbpercent
            // 
            this.rbpercent.AutoSize = true;
            this.rbpercent.Checked = true;
            this.rbpercent.Location = new System.Drawing.Point(22, 113);
            this.rbpercent.Name = "rbpercent";
            this.rbpercent.Size = new System.Drawing.Size(30, 22);
            this.rbpercent.TabIndex = 4;
            this.rbpercent.TabStop = true;
            this.rbpercent.Text = " ";
            this.rbpercent.UseVisualStyleBackColor = true;
            // 
            // mTxtNouveauTarif
            // 
            this.mTxtNouveauTarif.Location = new System.Drawing.Point(59, 34);
            this.mTxtNouveauTarif.Name = "mTxtNouveauTarif";
            this.mTxtNouveauTarif.Size = new System.Drawing.Size(251, 25);
            this.mTxtNouveauTarif.TabIndex = 5;
            this.mTxtNouveauTarif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox1_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(306, 173);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(119, 56);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nouveau Tarif :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Remise en pourcentage :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Montant de la remise :";
            // 
            // mTxtMontantRemise
            // 
            this.mTxtMontantRemise.Location = new System.Drawing.Point(59, 194);
            this.mTxtMontantRemise.Name = "mTxtMontantRemise";
            this.mTxtMontantRemise.Size = new System.Drawing.Size(159, 25);
            this.mTxtMontantRemise.TabIndex = 10;
            this.mTxtMontantRemise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTxtMontantRemise_KeyDown);
            // 
            // rbMontantRemise
            // 
            this.rbMontantRemise.AutoSize = true;
            this.rbMontantRemise.Location = new System.Drawing.Point(23, 195);
            this.rbMontantRemise.Name = "rbMontantRemise";
            this.rbMontantRemise.Size = new System.Drawing.Size(30, 22);
            this.rbMontantRemise.TabIndex = 9;
            this.rbMontantRemise.Text = " ";
            this.rbMontantRemise.UseVisualStyleBackColor = true;
            // 
            // slidingList1
            // 
            this.slidingList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slidingList1.ButtonSize = new System.Drawing.SizeF(80F, 45F);
            this.slidingList1.CheckedItems = null;
            this.slidingList1.imagelist = null;
            this.slidingList1.Location = new System.Drawing.Point(59, 96);
            this.slidingList1.Margin = new System.Windows.Forms.Padding(4);
            this.slidingList1.MultiSelectMode = false;
            this.slidingList1.Name = "slidingList1";
            this.slidingList1.Size = new System.Drawing.Size(495, 57);
            this.slidingList1.TabIndex = 0;
            this.slidingList1.WrapMode = false;
            this.slidingList1.OnSelectionChange += new System.EventHandler(this.slidingList1_OnSelectionChange);
            // 
            // FrmRistourne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(565, 242);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mTxtMontantRemise);
            this.Controls.Add(this.rbMontantRemise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.mTxtNouveauTarif);
            this.Controls.Add(this.rbpercent);
            this.Controls.Add(this.rbMontant);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.slidingList1);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRistourne";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remise";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmRistourne_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BaseCommonControls.SlidingList slidingList1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbMontant;
        private System.Windows.Forms.RadioButton rbpercent;
        private System.Windows.Forms.MaskedTextBox mTxtNouveauTarif;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mTxtMontantRemise;
        private System.Windows.Forms.RadioButton rbMontantRemise;
    }
}