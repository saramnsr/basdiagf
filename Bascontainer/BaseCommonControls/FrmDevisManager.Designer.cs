namespace BaseCommonControls
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblProp = new System.Windows.Forms.Label();
            this.dgvDevis = new System.Windows.Forms.DataGridView();
            this.colDateProp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateEcheance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNbPropositions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Traitements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMnuDevis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerLeDevisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revoirLeDevisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrintDevis = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevis)).BeginInit();
            this.ctxMnuDevis.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(873, 268);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 81);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Fermer";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(15, 268);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 81);
            this.button1.TabIndex = 3;
            this.button1.Text = "Accepter le devis";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.colDateEcheance,
            this.colNbPropositions,
            this.Traitements});
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
            this.dgvDevis.Size = new System.Drawing.Size(994, 218);
            this.dgvDevis.TabIndex = 8;
            // 
            // colDateProp
            // 
            dataGridViewCellStyle13.Format = "d";
            dataGridViewCellStyle13.NullValue = null;
            this.colDateProp.DefaultCellStyle = dataGridViewCellStyle13;
            this.colDateProp.FillWeight = 40F;
            this.colDateProp.HeaderText = "Date de proposition";
            this.colDateProp.Name = "colDateProp";
            this.colDateProp.ReadOnly = true;
            // 
            // colDateEcheance
            // 
            dataGridViewCellStyle14.Format = "d";
            this.colDateEcheance.DefaultCellStyle = dataGridViewCellStyle14;
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
            // ctxMnuDevis
            // 
            this.ctxMnuDevis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerLeDevisToolStripMenuItem,
            this.revoirLeDevisToolStripMenuItem});
            this.ctxMnuDevis.Name = "ctxMnuDevis";
            this.ctxMnuDevis.Size = new System.Drawing.Size(162, 48);
            // 
            // supprimerLeDevisToolStripMenuItem
            // 
            this.supprimerLeDevisToolStripMenuItem.Name = "supprimerLeDevisToolStripMenuItem";
            this.supprimerLeDevisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.supprimerLeDevisToolStripMenuItem.Text = "Supprimer le devis";
            this.supprimerLeDevisToolStripMenuItem.Click += new System.EventHandler(this.supprimerLeDevisToolStripMenuItem_Click);
            // 
            // revoirLeDevisToolStripMenuItem
            // 
            this.revoirLeDevisToolStripMenuItem.Name = "revoirLeDevisToolStripMenuItem";
            this.revoirLeDevisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.revoirLeDevisToolStripMenuItem.Text = "Revoir le devis";
            this.revoirLeDevisToolStripMenuItem.Click += new System.EventHandler(this.revoirLeDevisToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(299, 268);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 81);
            this.button2.TabIndex = 9;
            this.button2.Text = "Archiver le devis";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_3);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle15.Format = "d";
            dataGridViewCellStyle15.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Date de proposition";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 203;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle16.Format = "d";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn2.FillWeight = 40F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Date d\'acceptation";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 149;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle17.Format = "d";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle17;
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
            dataGridViewCellStyle18.Format = "C2";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn6.FillWeight = 30F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Montant";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 112;
            // 
            // btnPrintDevis
            // 
            this.btnPrintDevis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDevis.Location = new System.Drawing.Point(157, 268);
            this.btnPrintDevis.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintDevis.Name = "btnPrintDevis";
            this.btnPrintDevis.Size = new System.Drawing.Size(134, 81);
            this.btnPrintDevis.TabIndex = 10;
            this.btnPrintDevis.Text = "Imprimer le devis";
            this.btnPrintDevis.UseVisualStyleBackColor = true;
            this.btnPrintDevis.Click += new System.EventHandler(this.btnPrintDevis_Click_1);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(441, 268);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 81);
            this.button3.TabIndex = 11;
            this.button3.Text = "Modifier le devis";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
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
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPrintDevis);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblProp;
        private System.Windows.Forms.DataGridView dgvDevis;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ContextMenuStrip ctxMnuDevis;
        private System.Windows.Forms.ToolStripMenuItem supprimerLeDevisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revoirLeDevisToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateProp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateEcheance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNbPropositions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Traitements;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button btnPrintDevis;
        private System.Windows.Forms.Button button3;
    }
}