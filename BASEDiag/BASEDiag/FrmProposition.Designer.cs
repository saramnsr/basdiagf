namespace BASEDiag
{
    partial class FrmProposition
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtbxLibelle = new System.Windows.Forms.TextBox();
            this.pnlDuree = new System.Windows.Forms.Panel();
            this.pnlPlanTraitement = new System.Windows.Forms.Panel();
            this.pnlPlanTraitementSecu = new System.Windows.Forms.Panel();
            this.pnlRisques = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvTarifs = new System.Windows.Forms.DataGridView();
            this.colNomActe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifs)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbxLibelle
            // 
            this.txtbxLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxLibelle.Location = new System.Drawing.Point(12, 12);
            this.txtbxLibelle.Name = "txtbxLibelle";
            this.txtbxLibelle.Size = new System.Drawing.Size(671, 20);
            this.txtbxLibelle.TabIndex = 0;
            // 
            // pnlDuree
            // 
            this.pnlDuree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDuree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDuree.ForeColor = System.Drawing.Color.Black;
            this.pnlDuree.Location = new System.Drawing.Point(12, 43);
            this.pnlDuree.Name = "pnlDuree";
            this.pnlDuree.Size = new System.Drawing.Size(195, 57);
            this.pnlDuree.TabIndex = 18;
            this.pnlDuree.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDuree_Paint);
            this.pnlDuree.Click += new System.EventHandler(this.pnlDuree_Click);
            // 
            // pnlPlanTraitement
            // 
            this.pnlPlanTraitement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlanTraitement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlanTraitement.ForeColor = System.Drawing.Color.Black;
            this.pnlPlanTraitement.Location = new System.Drawing.Point(12, 106);
            this.pnlPlanTraitement.Name = "pnlPlanTraitement";
            this.pnlPlanTraitement.Size = new System.Drawing.Size(195, 57);
            this.pnlPlanTraitement.TabIndex = 19;
            this.pnlPlanTraitement.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPlanTraitement_Paint);
            this.pnlPlanTraitement.Click += new System.EventHandler(this.pnlPlanTraitement_Click);
            // 
            // pnlPlanTraitementSecu
            // 
            this.pnlPlanTraitementSecu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlanTraitementSecu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlanTraitementSecu.ForeColor = System.Drawing.Color.Black;
            this.pnlPlanTraitementSecu.Location = new System.Drawing.Point(213, 43);
            this.pnlPlanTraitementSecu.Name = "pnlPlanTraitementSecu";
            this.pnlPlanTraitementSecu.Size = new System.Drawing.Size(183, 120);
            this.pnlPlanTraitementSecu.TabIndex = 20;
            this.pnlPlanTraitementSecu.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPlanTraitementSecu_Paint);
            // 
            // pnlRisques
            // 
            this.pnlRisques.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRisques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRisques.ForeColor = System.Drawing.Color.Black;
            this.pnlRisques.Location = new System.Drawing.Point(402, 43);
            this.pnlRisques.Name = "pnlRisques";
            this.pnlRisques.Size = new System.Drawing.Size(281, 120);
            this.pnlRisques.TabIndex = 21;
            this.pnlRisques.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRisques_Paint);
            this.pnlRisques.Click += new System.EventHandler(this.pnlRisques_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(608, 285);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 54);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(527, 285);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 54);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgvTarifs
            // 
            this.dgvTarifs.AllowUserToAddRows = false;
            this.dgvTarifs.AllowUserToDeleteRows = false;
            this.dgvTarifs.AllowUserToResizeRows = false;
            this.dgvTarifs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTarifs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarifs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNomActe,
            this.colMontant});
            this.dgvTarifs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTarifs.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTarifs.Location = new System.Drawing.Point(12, 169);
            this.dgvTarifs.Name = "dgvTarifs";
            this.dgvTarifs.RowHeadersVisible = false;
            this.dgvTarifs.Size = new System.Drawing.Size(671, 98);
            this.dgvTarifs.TabIndex = 26;
            this.dgvTarifs.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTarifs_CellEndEdit);
            // 
            // colNomActe
            // 
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkGray;
            this.colNomActe.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNomActe.HeaderText = "Acte";
            this.colNomActe.MinimumWidth = 150;
            this.colNomActe.Name = "colNomActe";
            this.colNomActe.ReadOnly = true;
            this.colNomActe.Width = 500;
            // 
            // colMontant
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.colMontant.DefaultCellStyle = dataGridViewCellStyle2;
            this.colMontant.HeaderText = "Montant / sem";
            this.colMontant.Name = "colMontant";
            // 
            // FrmProposition
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(695, 351);
            this.Controls.Add(this.dgvTarifs);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlRisques);
            this.Controls.Add(this.pnlPlanTraitementSecu);
            this.Controls.Add(this.pnlPlanTraitement);
            this.Controls.Add(this.pnlDuree);
            this.Controls.Add(this.txtbxLibelle);
            this.Name = "FrmProposition";
            this.Text = "Proposition";
            this.Load += new System.EventHandler(this.FrmProposition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDuree;
        private System.Windows.Forms.Panel pnlPlanTraitement;
        private System.Windows.Forms.Panel pnlPlanTraitementSecu;
        private System.Windows.Forms.Panel pnlRisques;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvTarifs;
        public System.Windows.Forms.TextBox txtbxLibelle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomActe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMontant;
    }
}