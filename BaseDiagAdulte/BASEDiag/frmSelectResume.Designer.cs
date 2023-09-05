namespace BASEDiagAdulte
{
    partial class frmSelectResume
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
            this.btncancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvDiags = new System.Windows.Forms.DataGridView();
            this.colDateDiag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiags)).BeginInit();
            this.SuspendLayout();
            // 
            // btncancel
            // 
            this.btncancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Location = new System.Drawing.Point(350, 283);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(112, 69);
            this.btncancel.TabIndex = 0;
            this.btncancel.Text = "Nouveau Diag";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(470, 283);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 69);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgvDiags
            // 
            this.dgvDiags.AllowUserToAddRows = false;
            this.dgvDiags.AllowUserToDeleteRows = false;
            this.dgvDiags.AllowUserToResizeRows = false;
            this.dgvDiags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDiags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiags.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiags.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDiags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateDiag});
            this.dgvDiags.Location = new System.Drawing.Point(12, 12);
            this.dgvDiags.Name = "dgvDiags";
            this.dgvDiags.ReadOnly = true;
            this.dgvDiags.RowHeadersVisible = false;
            this.dgvDiags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiags.Size = new System.Drawing.Size(570, 264);
            this.dgvDiags.TabIndex = 2;
            this.dgvDiags.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvDiags_MouseDoubleClick);
            // 
            // colDateDiag
            // 
            this.colDateDiag.HeaderText = "Date du diagnostique";
            this.colDateDiag.Name = "colDateDiag";
            this.colDateDiag.ReadOnly = true;
            // 
            // frmSelectResume
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btncancel;
            this.ClientSize = new System.Drawing.Size(595, 365);
            this.Controls.Add(this.dgvDiags);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btncancel);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmSelectResume";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historique des diagnostiques";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSelectResume_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvDiags;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateDiag;
    }
}