namespace BASEDiagAdulte.Ctrls
{
    partial class frmPlanTraitementDEP
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtbxPlanTraitement = new System.Windows.Forms.TextBox();
            this.lstbxPlantraitmntDef = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(744, 436);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 75);
            this.btnOk.TabIndex = 27;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(866, 436);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 75);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtbxPlanTraitement
            // 
            this.txtbxPlanTraitement.AcceptsReturn = true;
            this.txtbxPlanTraitement.AcceptsTab = true;
            this.txtbxPlanTraitement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxPlanTraitement.Location = new System.Drawing.Point(18, 284);
            this.txtbxPlanTraitement.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxPlanTraitement.Multiline = true;
            this.txtbxPlanTraitement.Name = "txtbxPlanTraitement";
            this.txtbxPlanTraitement.Size = new System.Drawing.Size(958, 142);
            this.txtbxPlanTraitement.TabIndex = 28;
            // 
            // lstbxPlantraitmntDef
            // 
            this.lstbxPlantraitmntDef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbxPlantraitmntDef.FormattingEnabled = true;
            this.lstbxPlantraitmntDef.ItemHeight = 18;
            this.lstbxPlantraitmntDef.Location = new System.Drawing.Point(18, 17);
            this.lstbxPlantraitmntDef.Margin = new System.Windows.Forms.Padding(4);
            this.lstbxPlantraitmntDef.Name = "lstbxPlantraitmntDef";
            this.lstbxPlantraitmntDef.Size = new System.Drawing.Size(958, 256);
            this.lstbxPlantraitmntDef.TabIndex = 29;
            this.lstbxPlantraitmntDef.Click += new System.EventHandler(this.lstbxPlantraitmntDef_Click);
            this.lstbxPlantraitmntDef.SelectedIndexChanged += new System.EventHandler(this.lstbxPlantraitmntDef_SelectedIndexChanged);
            // 
            // frmPlanTraitementDEP
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(996, 528);
            this.Controls.Add(this.lstbxPlantraitmntDef);
            this.Controls.Add(this.txtbxPlanTraitement);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPlanTraitementDEP";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan de traitement";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPlanTraitementDEP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtbxPlanTraitement;
        private System.Windows.Forms.ListBox lstbxPlantraitmntDef;
    }
}