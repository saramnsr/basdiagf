namespace BaseCommonControls
{
    partial class FrmCategories
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lstBxCategActuelles = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lvHistorique = new System.Windows.Forms.ListView();
            this.colLibelle = new System.Windows.Forms.ColumnHeader();
            this.colDebut = new System.Windows.Forms.ColumnHeader();
            this.colFin = new System.Windows.Forms.ColumnHeader();
            this.lblHisto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(993, 364);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 79);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(872, 364);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 79);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstBxCategActuelles
            // 
            this.lstBxCategActuelles.FormattingEnabled = true;
            this.lstBxCategActuelles.ItemHeight = 18;
            this.lstBxCategActuelles.Location = new System.Drawing.Point(18, 62);
            this.lstBxCategActuelles.Margin = new System.Windows.Forms.Padding(4);
            this.lstBxCategActuelles.Name = "lstBxCategActuelles";
            this.lstBxCategActuelles.Size = new System.Drawing.Size(366, 292);
            this.lstBxCategActuelles.TabIndex = 2;
            this.lstBxCategActuelles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBxCategActuelles_KeyDown);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(310, 364);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 58);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ajouter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvHistorique
            // 
            this.lvHistorique.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLibelle,
            this.colDebut,
            this.colFin});
            this.lvHistorique.Location = new System.Drawing.Point(456, 62);
            this.lvHistorique.Margin = new System.Windows.Forms.Padding(4);
            this.lvHistorique.Name = "lvHistorique";
            this.lvHistorique.Size = new System.Drawing.Size(648, 292);
            this.lvHistorique.TabIndex = 4;
            this.lvHistorique.UseCompatibleStateImageBehavior = false;
            this.lvHistorique.View = System.Windows.Forms.View.Details;
            // 
            // colLibelle
            // 
            this.colLibelle.Text = "Categorie";
            this.colLibelle.Width = 375;
            // 
            // colDebut
            // 
            this.colDebut.Text = "Date de début";
            this.colDebut.Width = 140;
            // 
            // colFin
            // 
            this.colFin.Text = "Date de fin";
            this.colFin.Width = 124;
            // 
            // lblHisto
            // 
            this.lblHisto.AutoSize = true;
            this.lblHisto.Location = new System.Drawing.Point(453, 40);
            this.lblHisto.Name = "lblHisto";
            this.lblHisto.Size = new System.Drawing.Size(87, 18);
            this.lblHisto.TabIndex = 5;
            this.lblHisto.Text = "Historique : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Catégories de la personne : ";
            // 
            // FrmCategories
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1124, 460);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHisto);
            this.Controls.Add(this.lvHistorique);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstBxCategActuelles);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCategories";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Affectation des categories";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmCategories_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstBxCategActuelles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lvHistorique;
        private System.Windows.Forms.ColumnHeader colLibelle;
        private System.Windows.Forms.ColumnHeader colDebut;
        private System.Windows.Forms.ColumnHeader colFin;
        private System.Windows.Forms.Label lblHisto;
        private System.Windows.Forms.Label label1;
    }
}