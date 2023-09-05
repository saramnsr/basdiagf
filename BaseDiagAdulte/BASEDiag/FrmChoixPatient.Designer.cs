namespace BASEDiag
{
    partial class FrmChoixPatient
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
            this.txtbxSearch = new System.Windows.Forms.TextBox();
            this.LstBxChoixPatient = new System.Windows.Forms.ListBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbxSearch
            // 
            this.txtbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxSearch.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxSearch.Location = new System.Drawing.Point(12, 12);
            this.txtbxSearch.Name = "txtbxSearch";
            this.txtbxSearch.Size = new System.Drawing.Size(375, 29);
            this.txtbxSearch.TabIndex = 0;
            this.txtbxSearch.TextChanged += new System.EventHandler(this.txtbxSearch_TextChanged);
            // 
            // LstBxChoixPatient
            // 
            this.LstBxChoixPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LstBxChoixPatient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LstBxChoixPatient.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstBxChoixPatient.FormattingEnabled = true;
            this.LstBxChoixPatient.ItemHeight = 27;
            this.LstBxChoixPatient.Location = new System.Drawing.Point(12, 47);
            this.LstBxChoixPatient.Name = "LstBxChoixPatient";
            this.LstBxChoixPatient.Size = new System.Drawing.Size(375, 382);
            this.LstBxChoixPatient.TabIndex = 1;
            this.LstBxChoixPatient.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstBxChoixPatient_DrawItem);
            this.LstBxChoixPatient.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LstBxChoixPatient_MeasureItem);
            this.LstBxChoixPatient.SelectedIndexChanged += new System.EventHandler(this.LstBxChoixPatient_SelectedIndexChanged);
            this.LstBxChoixPatient.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstBxChoixPatient_MouseDown);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(335, 435);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(52, 44);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Annuler";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(276, 435);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(53, 44);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmChoixPatient
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(399, 491);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LstBxChoixPatient);
            this.Controls.Add(this.txtbxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmChoixPatient";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Patient";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmChoixPatient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxSearch;
        private System.Windows.Forms.ListBox LstBxChoixPatient;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}