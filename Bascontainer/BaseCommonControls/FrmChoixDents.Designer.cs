namespace BaseCommonControls
{
    partial class FrmChoixDents
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.choixDents1 = new BaseCommonControls.ChoixDents();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(55, 517);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(251, 57);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(336, 94);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = " ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // choixDents1
            // 
            this.choixDents1.IsMandibulaireActif = true;
            this.choixDents1.IsMaxillaireActif = true;
            this.choixDents1.Location = new System.Drawing.Point(55, 109);
            this.choixDents1.Margin = new System.Windows.Forms.Padding(6);
            this.choixDents1.Name = "choixDents1";
            this.choixDents1.SelectedDents = "";
            this.choixDents1.separator = ',';
            this.choixDents1.Size = new System.Drawing.Size(251, 398);
            this.choixDents1.TabIndex = 0;
            // 
            // FrmChoixDents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(361, 587);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.choixDents1);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmChoixDents";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Choix des dents";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmChoixDents_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChoixDents choixDents1;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Button btnOk;
    }
}