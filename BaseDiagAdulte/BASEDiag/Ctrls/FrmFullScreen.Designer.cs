﻿namespace BASEDiagAdulte.Ctrls
{
    partial class FrmFullScreen
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
            this.pbxFullScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFullScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxFullScreen
            // 
            this.pbxFullScreen.BackColor = System.Drawing.Color.White;
            this.pbxFullScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxFullScreen.Location = new System.Drawing.Point(0, 0);
            this.pbxFullScreen.Name = "pbxFullScreen";
            this.pbxFullScreen.Size = new System.Drawing.Size(292, 266);
            this.pbxFullScreen.TabIndex = 0;
            this.pbxFullScreen.TabStop = false;
            this.pbxFullScreen.Click += new System.EventHandler(this.pbxFullScreen_Click);
            // 
            // FrmFullScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.pbxFullScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFullScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmFullScreen";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbxFullScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxFullScreen;
    }
}