namespace BASEDiagAdulte.Ctrls
{
    partial class SuggestBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnChooseSuggestedCorres = new System.Windows.Forms.Button();
            this.lblSuggest = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnNextSuggestedCorres = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnChooseSuggestedCorres
            // 
            this.BtnChooseSuggestedCorres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChooseSuggestedCorres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChooseSuggestedCorres.Location = new System.Drawing.Point(613, 3);
            this.BtnChooseSuggestedCorres.Name = "BtnChooseSuggestedCorres";
            this.BtnChooseSuggestedCorres.Size = new System.Drawing.Size(75, 23);
            this.BtnChooseSuggestedCorres.TabIndex = 2;
            this.BtnChooseSuggestedCorres.Text = "Oui";
            this.BtnChooseSuggestedCorres.UseVisualStyleBackColor = true;
            this.BtnChooseSuggestedCorres.Click += new System.EventHandler(this.BtnChooseSuggestedCorres_Click);
            // 
            // lblSuggest
            // 
            this.lblSuggest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSuggest.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuggest.Location = new System.Drawing.Point(37, 3);
            this.lblSuggest.Name = "lblSuggest";
            this.lblSuggest.Size = new System.Drawing.Size(489, 23);
            this.lblSuggest.TabIndex = 0;
            this.lblSuggest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 23);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // BtnNextSuggestedCorres
            // 
            this.BtnNextSuggestedCorres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNextSuggestedCorres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNextSuggestedCorres.Location = new System.Drawing.Point(532, 3);
            this.BtnNextSuggestedCorres.Name = "BtnNextSuggestedCorres";
            this.BtnNextSuggestedCorres.Size = new System.Drawing.Size(75, 23);
            this.BtnNextSuggestedCorres.TabIndex = 3;
            this.BtnNextSuggestedCorres.Text = "Non";
            this.BtnNextSuggestedCorres.UseVisualStyleBackColor = true;
            this.BtnNextSuggestedCorres.Click += new System.EventHandler(this.BtnNextSuggestedCorres_Click);
            // 
            // SuggestBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.BtnNextSuggestedCorres);
            this.Controls.Add(this.BtnChooseSuggestedCorres);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblSuggest);
            this.Name = "SuggestBox";
            this.Size = new System.Drawing.Size(688, 31);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnChooseSuggestedCorres;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSuggest;
        private System.Windows.Forms.Button BtnNextSuggestedCorres;
    }
}
