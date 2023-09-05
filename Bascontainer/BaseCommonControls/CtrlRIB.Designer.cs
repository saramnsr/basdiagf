namespace BaseCommonControls
{
    partial class CtrlRIB
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
            this.mTxtNumBque = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.mTxtCle = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mTxtGuichet = new System.Windows.Forms.MaskedTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.mTxtNumCpt = new System.Windows.Forms.MaskedTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mTxtNumBque
            // 
            this.mTxtNumBque.Location = new System.Drawing.Point(13, 22);
            this.mTxtNumBque.Mask = "&&&&&";
            this.mTxtNumBque.Name = "mTxtNumBque";
            this.mTxtNumBque.Size = new System.Drawing.Size(67, 20);
            this.mTxtNumBque.TabIndex = 61;
            this.mTxtNumBque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTxtNumBque_KeyDown);
            this.mTxtNumBque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mTxtNumBque_KeyUp);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Code banque";
            // 
            // mTxtCle
            // 
            this.mTxtCle.Location = new System.Drawing.Point(274, 22);
            this.mTxtCle.Mask = "&&";
            this.mTxtCle.Name = "mTxtCle";
            this.mTxtCle.Size = new System.Drawing.Size(36, 20);
            this.mTxtCle.TabIndex = 57;
            this.mTxtCle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTxtCle_KeyDown);
            this.mTxtCle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mTxtCle_KeyUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(271, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 60;
            this.label15.Text = "Clé RIB";
            // 
            // mTxtGuichet
            // 
            this.mTxtGuichet.Location = new System.Drawing.Point(86, 22);
            this.mTxtGuichet.Mask = "&&&&&";
            this.mTxtGuichet.Name = "mTxtGuichet";
            this.mTxtGuichet.Size = new System.Drawing.Size(68, 20);
            this.mTxtGuichet.TabIndex = 56;
            this.mTxtGuichet.ValidatingType = typeof(int);
            this.mTxtGuichet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTxtGuichet_KeyDown);
            this.mTxtGuichet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mTxtGuichet_KeyUp);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(87, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 59;
            this.label16.Text = "Code guichet";
            // 
            // mTxtNumCpt
            // 
            this.mTxtNumCpt.Location = new System.Drawing.Point(160, 22);
            this.mTxtNumCpt.Mask = "&&&&&&&&&&&";
            this.mTxtNumCpt.Name = "mTxtNumCpt";
            this.mTxtNumCpt.Size = new System.Drawing.Size(108, 20);
            this.mTxtNumCpt.TabIndex = 55;
            this.mTxtNumCpt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mTxtNumCpt_KeyDown);
            this.mTxtNumCpt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mTxtNumCpt_KeyUp);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(161, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 13);
            this.label17.TabIndex = 58;
            this.label17.Text = "Numéro de compte";
            // 
            // CtrlRIB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mTxtNumBque);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.mTxtCle);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.mTxtGuichet);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.mTxtNumCpt);
            this.Controls.Add(this.label17);
            this.Name = "CtrlRIB";
            this.Size = new System.Drawing.Size(319, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mTxtNumBque;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox mTxtCle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mTxtGuichet;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox mTxtNumCpt;
        private System.Windows.Forms.Label label17;
    }
}
