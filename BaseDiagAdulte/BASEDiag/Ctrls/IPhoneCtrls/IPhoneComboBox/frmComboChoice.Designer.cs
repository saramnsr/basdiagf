﻿namespace BASE_CONTACT.Ctrls.IPhoneControls.IPhoneComboBox
{
    partial class frmComboChoice
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
            this.SuspendLayout();
            // 
            // frmComboChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmComboChoice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmComboChoice";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.frmComboChoice_Deactivate);
            this.Load += new System.EventHandler(this.frmComboChoice_Load);
            this.VisibleChanged += new System.EventHandler(this.frmComboChoice_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmComboChoice_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

    }
}