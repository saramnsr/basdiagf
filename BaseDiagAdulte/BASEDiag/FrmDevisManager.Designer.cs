namespace BASEDiagAdulte
{
    partial class FrmDevisManager
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProp = new System.Windows.Forms.Label();
            this.btnPrintDevis = new System.Windows.Forms.Button();
            this.dgvDevis = new System.Windows.Forms.DataGridView();
            this.colDateProp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateEcheance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNbPropositions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Traitements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Montant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMnuDevis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerLeDevisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevis)).BeginInit();
            this.ctxMnuDevis.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(873, 232);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 117);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Fermer";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProp
            // 
            this.lblProp.AutoSize = true;
            this.lblProp.Location = new System.Drawing.Point(12, 9);
            this.lblProp.Name = "lblProp";
            this.lblProp.Size = new System.Drawing.Size(126, 18);
            this.lblProp.TabIndex = 6;
            this.lblProp.Text = "Devis en attentes :";
            // 
            // btnPrintDevis
            // 
            this.btnPrintDevis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDevis.Location = new System.Drawing.Point(15, 232);
            this.btnPrintDevis.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintDevis.Name = "btnPrintDevis";
            this.btnPrintDevis.Size = new System.Drawing.Size(129, 117);
            this.btnPrintDevis.TabIndex = 7;
            this.btnPrintDevis.Text = "Créer un devis";
            this.btnPrintDevis.UseVisualStyleBackColor = true;
            this.btnPrintDevis.Click += new System.EventHandler(this.btnPrintDevis_Click);
            // 
            // dgvDevis
            // 
            this.dgvDevis.AllowUserToAddRows = false;
            this.dgvDevis.AllowUserToDeleteRows = false;
            this.dgvDevis.AllowUserToResizeRows = false;
            this.dgvDevis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDevis.BackgroundColor = System.Drawing.Color.White;
            this.dgvDevis.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvDevis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateProp,
            this.dataGridViewTextBoxColumn1,
            this.colDateEcheance,
            this.colNbPropositions,
            this.Traitements,
            this.Montant});
            this.dgvDevis.ContextMenuStrip = this.ctxMnuDevis;
            this.dgvDevis.Location = new System.Drawing.Point(15, 42);
            this.dgvDevis.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgvDevis.MultiSelect = false;
            this.dgvDevis.Name = "dgvDevis";
            this.dgvDevis.ReadOnly = true;
            this.dgvDevis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDevis.RowHeadersVisible = false;
            this.dgvDevis.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDevis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDevis.ShowEditingIcon = false;
            this.dgvDevis.ShowRowErrors = false;
            this.dgvDevis.Size = new System.Drawing.Size(994, 182);
            this.dgvDevis.TabIndex = 8;
            // 
            // colDateProp
            // 
            dataGridViewCellStyle8.Format = "d";
            dataGridViewCellStyle8.NullValue = null;
            this.colDateProp.DefaultCellStyle = dataGridViewCellStyle8;
            this.colDateProp.FillWeight = 40F;
            this.colDateProp.HeaderText = "Date de proposition";
            this.colDateProp.Name = "colDateProp";
            this.colDateProp.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle9.Format = "d";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Date d\'acceptation";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // colDateEcheance
            // 
            dataGridViewCellStyle10.Format = "d";
            this.colDateEcheance.DefaultCellStyle = dataGridViewCellStyle10;
            this.colDateEcheance.FillWeight = 40F;
            this.colDateEcheance.HeaderText = "Date d\'echeance";
            this.colDateEcheance.Name = "colDateEcheance";
            this.colDateEcheance.ReadOnly = true;
            // 
            // colNbPropositions
            // 
            this.colNbPropositions.FillWeight = 15F;
            this.colNbPropositions.HeaderText = "Nb propositions";
            this.colNbPropositions.Name = "colNbPropositions";
            this.colNbPropositions.ReadOnly = true;
            // 
            // Traitements
            // 
            this.Traitements.HeaderText = "Traitement";
            this.Traitements.Name = "Traitements";
            this.Traitements.ReadOnly = true;
            // 
            // Montant
            // 
            dataGridViewCellStyle11.Format = "C2";
            this.Montant.DefaultCellStyle = dataGridViewCellStyle11;
            this.Montant.FillWeight = 30F;
            this.Montant.HeaderText = "Montant";
            this.Montant.Name = "Montant";
            this.Montant.ReadOnly = true;
            // 
            // ctxMnuDevis
            // 
            this.ctxMnuDevis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerLeDevisToolStripMenuItem});
            this.ctxMnuDevis.Name = "ctxMnuDevis";
            this.ctxMnuDevis.Size = new System.Drawing.Size(162, 26);
            // 
            // supprimerLeDevisToolStripMenuItem
            // 
            this.supprimerLeDevisToolStripMenuItem.Name = "supprimerLeDevisToolStripMenuItem";
            this.supprimerLeDevisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.supprimerLeDevisToolStripMenuItem.Text = "Supprimer le devis";
            this.supprimerLeDevisToolStripMenuItem.Click += new System.EventHandler(this.supprimerLeDevisToolStripMenuItem_Click);
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle12.Format = "d";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn2.FillWeight = 40F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Date d\'acceptation";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 149;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle13.Format = "d";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn3.FillWeight = 40F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Date d\'echeance";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 15F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Nb propositions";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 56;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Traitement";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 374;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle14.Format = "C2";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn6.FillWeight = 30F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Montant";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 112;
            // 
            // FrmDevisManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1025, 368);
            this.Controls.Add(this.lblProp);
            this.Controls.Add(this.dgvDevis);
            this.Controls.Add(this.btnPrintDevis);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDevisManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "En cours";
            this.Load += new System.EventHandler(this.FrmPropositionsManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevis)).EndInit();
            this.ctxMnuDevis.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProp;
        private System.Windows.Forms.Button btnPrintDevis;
        private System.Windows.Forms.DataGridView dgvDevis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateProp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateEcheance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNbPropositions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Traitements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Montant;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ContextMenuStrip ctxMnuDevis;
        private System.Windows.Forms.ToolStripMenuItem supprimerLeDevisToolStripMenuItem;
    }
}