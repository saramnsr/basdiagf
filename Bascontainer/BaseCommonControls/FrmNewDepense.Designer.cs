namespace BaseCommonControls
{
    partial class FrmNewDepense
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
            this.txtbxDetails = new System.Windows.Forms.TextBox();
            this.cbxBanque = new System.Windows.Forms.ComboBox();
            this.cbxModeReglement = new System.Windows.Forms.ComboBox();
            this.txtbxCode = new System.Windows.Forms.TextBox();
            this.txtbxMontant = new System.Windows.Forms.TextBox();
            this.dtpvaleurbque = new System.Windows.Forms.DateTimePicker();
            this.dtpdepense = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMontant = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(513, 368);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 67);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(405, 368);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 67);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtbxDetails
            // 
            this.txtbxDetails.Location = new System.Drawing.Point(202, 52);
            this.txtbxDetails.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxDetails.Multiline = true;
            this.txtbxDetails.Name = "txtbxDetails";
            this.txtbxDetails.Size = new System.Drawing.Size(411, 53);
            this.txtbxDetails.TabIndex = 2;
            // 
            // cbxBanque
            // 
            this.cbxBanque.FormattingEnabled = true;
            this.cbxBanque.Location = new System.Drawing.Point(202, 287);
            this.cbxBanque.Margin = new System.Windows.Forms.Padding(4);
            this.cbxBanque.Name = "cbxBanque";
            this.cbxBanque.Size = new System.Drawing.Size(411, 25);
            this.cbxBanque.TabIndex = 8;
            // 
            // cbxModeReglement
            // 
            this.cbxModeReglement.FormattingEnabled = true;
            this.cbxModeReglement.Items.AddRange(new object[] {
            "Chèque",
            "Espèce",
            "CB",
            "Prelevement",
            "Virement",
            "AMEX"});
            this.cbxModeReglement.Location = new System.Drawing.Point(202, 254);
            this.cbxModeReglement.Margin = new System.Windows.Forms.Padding(4);
            this.cbxModeReglement.Name = "cbxModeReglement";
            this.cbxModeReglement.Size = new System.Drawing.Size(411, 25);
            this.cbxModeReglement.TabIndex = 6;
            // 
            // txtbxCode
            // 
            this.txtbxCode.Location = new System.Drawing.Point(202, 220);
            this.txtbxCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxCode.Name = "txtbxCode";
            this.txtbxCode.Size = new System.Drawing.Size(187, 24);
            this.txtbxCode.TabIndex = 5;
            // 
            // txtbxMontant
            // 
            this.txtbxMontant.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxMontant.Location = new System.Drawing.Point(202, 10);
            this.txtbxMontant.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxMontant.Name = "txtbxMontant";
            this.txtbxMontant.Size = new System.Drawing.Size(187, 34);
            this.txtbxMontant.TabIndex = 1;
            this.txtbxMontant.Text = "0,00";
            // 
            // dtpvaleurbque
            // 
            this.dtpvaleurbque.Location = new System.Drawing.Point(202, 188);
            this.dtpvaleurbque.Margin = new System.Windows.Forms.Padding(4);
            this.dtpvaleurbque.Name = "dtpvaleurbque";
            this.dtpvaleurbque.Size = new System.Drawing.Size(265, 24);
            this.dtpvaleurbque.TabIndex = 4;
            // 
            // dtpdepense
            // 
            this.dtpdepense.Location = new System.Drawing.Point(202, 154);
            this.dtpdepense.Margin = new System.Windows.Forms.Padding(4);
            this.dtpdepense.Name = "dtpdepense";
            this.dtpdepense.Size = new System.Drawing.Size(265, 24);
            this.dtpdepense.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "Date de la dépense : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 187);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 30);
            this.label2.TabIndex = 11;
            this.label2.Text = "Date valeur banque : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMontant
            // 
            this.lblMontant.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.Location = new System.Drawing.Point(13, 11);
            this.lblMontant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(181, 30);
            this.lblMontant.TabIndex = 12;
            this.lblMontant.Text = "Montant : ";
            this.lblMontant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 218);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 30);
            this.label3.TabIndex = 13;
            this.label3.Text = "Code : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 252);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 30);
            this.label4.TabIndex = 14;
            this.label4.Text = "Mode de reglement : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 284);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 30);
            this.label6.TabIndex = 16;
            this.label6.Text = "Compte bancaire de remise : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 30);
            this.label7.TabIndex = 17;
            this.label7.Text = "Détails : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmNewDepense
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 450);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpdepense);
            this.Controls.Add(this.dtpvaleurbque);
            this.Controls.Add(this.txtbxMontant);
            this.Controls.Add(this.txtbxCode);
            this.Controls.Add(this.cbxModeReglement);
            this.Controls.Add(this.cbxBanque);
            this.Controls.Add(this.txtbxDetails);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmNewDepense";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dépense";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmNewDepense_Load);
            this.Shown += new System.EventHandler(this.FrmNewDepense_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtbxDetails;
        private System.Windows.Forms.ComboBox cbxBanque;
        private System.Windows.Forms.ComboBox cbxModeReglement;
        private System.Windows.Forms.TextBox txtbxCode;
        private System.Windows.Forms.TextBox txtbxMontant;
        private System.Windows.Forms.DateTimePicker dtpvaleurbque;
        private System.Windows.Forms.DateTimePicker dtpdepense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}