namespace BaseCommonControls
{
    partial class FrmSpecialite
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
            this.choixProfession = new BaseCommonControls.ChoiceMedical();
            this.SuspendLayout();
            // 
            // choixProfession
            // 
            this.choixProfession.BackColor = System.Drawing.SystemColors.Window;
            this.choixProfession.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choixProfession.Location = new System.Drawing.Point(13, 13);
            this.choixProfession.Margin = new System.Windows.Forms.Padding(4);
            this.choixProfession.Name = "choixProfession";
            this.choixProfession.Size = new System.Drawing.Size(744, 490);
            this.choixProfession.TabIndex = 0;
            this.choixProfession.Value = null;
            // 
            // FrmSpecialite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 516);
            this.Controls.Add(this.choixProfession);
            this.Name = "FrmSpecialite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix d\'une profession";
            this.Load += new System.EventHandler(this.FrmSpecialite_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChoiceMedical choixProfession;
    }
}