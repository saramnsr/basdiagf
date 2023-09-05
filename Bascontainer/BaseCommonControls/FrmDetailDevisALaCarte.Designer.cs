namespace BaseCommonControls
{
    partial class FrmDetailDevisALaCarte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetailDevisALaCarte));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgvactepropose = new System.Windows.Forms.DataGridView();
            this.colNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColDel = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlDevis = new System.Windows.Forms.Panel();
            this.btnRistourneGlobal = new System.Windows.Forms.Button();
            this.lblTotalAvantRemise = new System.Windows.Forms.Label();
            this.lblTotalAvantRemiseSurActes = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlEcheance = new System.Windows.Forms.Panel();
            this.lblEcheances = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvactepropose)).BeginInit();
            this.pnlDevis.SuspendLayout();
            this.pnlEcheance.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(696, 439);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 65);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(14, 439);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 65);
            this.button5.TabIndex = 8;
            this.button5.Text = "Imprimer le devis";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "trashbin.jpg");
            this.imageList1.Images.SetKeyName(1, "Compta.png");
            this.imageList1.Images.SetKeyName(2, "101184-bouton-remise-pdn.jpg");
            // 
            // dgvactepropose
            // 
            this.dgvactepropose.AllowUserToAddRows = false;
            this.dgvactepropose.AllowUserToDeleteRows = false;
            this.dgvactepropose.AllowUserToOrderColumns = true;
            this.dgvactepropose.AllowUserToResizeRows = false;
            this.dgvactepropose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvactepropose.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvactepropose.BackgroundColor = System.Drawing.Color.White;
            this.dgvactepropose.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvactepropose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvactepropose.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNom,
            this.colQte,
            this.colTarif,
            this.colBtn,
            this.ColDel});
            this.dgvactepropose.GridColor = System.Drawing.Color.White;
            this.dgvactepropose.Location = new System.Drawing.Point(14, 12);
            this.dgvactepropose.MultiSelect = false;
            this.dgvactepropose.Name = "dgvactepropose";
            this.dgvactepropose.RowHeadersVisible = false;
            this.dgvactepropose.RowTemplate.Height = 32;
            this.dgvactepropose.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvactepropose.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvactepropose.Size = new System.Drawing.Size(802, 289);
            this.dgvactepropose.TabIndex = 15;
            this.dgvactepropose.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvactepropose_CellClick);
            this.dgvactepropose.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvactepropose.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvactepropose_CellEndEdit);
            this.dgvactepropose.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvactepropose_CellPainting);
            this.dgvactepropose.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvactepropose_CellValidated);
            this.dgvactepropose.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvactepropose_RowValidated);
            // 
            // colNom
            // 
            this.colNom.HeaderText = "Nom";
            this.colNom.Name = "colNom";
            this.colNom.ReadOnly = true;
            // 
            // colQte
            // 
            this.colQte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colQte.HeaderText = "Qte";
            this.colQte.Name = "colQte";
            this.colQte.Width = 90;
            // 
            // colTarif
            // 
            this.colTarif.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Format = "C2";
            this.colTarif.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTarif.HeaderText = "Tarif";
            this.colTarif.Name = "colTarif";
            this.colTarif.ReadOnly = true;
            this.colTarif.Width = 150;
            // 
            // colBtn
            // 
            this.colBtn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "%";
            dataGridViewCellStyle2.NullValue = "%";
            this.colBtn.DefaultCellStyle = dataGridViewCellStyle2;
            this.colBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colBtn.HeaderText = "";
            this.colBtn.Name = "colBtn";
            this.colBtn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colBtn.Text = "%";
            this.colBtn.ToolTipText = "Remise";
            this.colBtn.Width = 32;
            // 
            // ColDel
            // 
            this.ColDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColDel.HeaderText = "";
            this.ColDel.Image = global::BaseCommonControls.Properties.Resources.trashbin;
            this.ColDel.Name = "ColDel";
            this.ColDel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColDel.Width = 32;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(568, 439);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 65);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(696, 439);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 65);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Enregistrer";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlDevis
            // 
            this.pnlDevis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDevis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDevis.Controls.Add(this.btnRistourneGlobal);
            this.pnlDevis.Controls.Add(this.lblTotalAvantRemise);
            this.pnlDevis.Controls.Add(this.lblTotalAvantRemiseSurActes);
            this.pnlDevis.Controls.Add(this.lblTotal);
            this.pnlDevis.Location = new System.Drawing.Point(14, 307);
            this.pnlDevis.Name = "pnlDevis";
            this.pnlDevis.Size = new System.Drawing.Size(410, 125);
            this.pnlDevis.TabIndex = 20;
            // 
            // btnRistourneGlobal
            // 
            this.btnRistourneGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRistourneGlobal.Location = new System.Drawing.Point(373, 89);
            this.btnRistourneGlobal.Name = "btnRistourneGlobal";
            this.btnRistourneGlobal.Size = new System.Drawing.Size(32, 29);
            this.btnRistourneGlobal.TabIndex = 3;
            this.btnRistourneGlobal.Text = "%";
            this.btnRistourneGlobal.UseVisualStyleBackColor = true;
            this.btnRistourneGlobal.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblTotalAvantRemise
            // 
            this.lblTotalAvantRemise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAvantRemise.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAvantRemise.Location = new System.Drawing.Point(203, 47);
            this.lblTotalAvantRemise.Name = "lblTotalAvantRemise";
            this.lblTotalAvantRemise.Size = new System.Drawing.Size(162, 27);
            this.lblTotalAvantRemise.TabIndex = 2;
            this.lblTotalAvantRemise.Text = "label1";
            this.lblTotalAvantRemise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalAvantRemise.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblTotalAvantRemiseSurActes
            // 
            this.lblTotalAvantRemiseSurActes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAvantRemiseSurActes.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAvantRemiseSurActes.Location = new System.Drawing.Point(203, 5);
            this.lblTotalAvantRemiseSurActes.Name = "lblTotalAvantRemiseSurActes";
            this.lblTotalAvantRemiseSurActes.Size = new System.Drawing.Size(162, 27);
            this.lblTotalAvantRemiseSurActes.TabIndex = 1;
            this.lblTotalAvantRemiseSurActes.Text = "label1";
            this.lblTotalAvantRemiseSurActes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(203, 91);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(164, 27);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "label1";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlEcheance
            // 
            this.pnlEcheance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEcheance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEcheance.Controls.Add(this.lblEcheances);
            this.pnlEcheance.Location = new System.Drawing.Point(430, 307);
            this.pnlEcheance.Name = "pnlEcheance";
            this.pnlEcheance.Size = new System.Drawing.Size(386, 125);
            this.pnlEcheance.TabIndex = 22;
            // 
            // lblEcheances
            // 
            this.lblEcheances.Location = new System.Drawing.Point(3, 5);
            this.lblEcheances.Name = "lblEcheances";
            this.lblEcheances.Size = new System.Drawing.Size(373, 113);
            this.lblEcheances.TabIndex = 22;
            this.lblEcheances.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEcheances.Click += new System.EventHandler(this.lblEcheances_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "Editer";
            this.dataGridViewImageColumn1.Image = global::BaseCommonControls.Properties.Resources._101184_bouton_remise_pdn1;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Nom";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 313;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Format = "C2";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tarif";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 314;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Format = "C2";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn3.HeaderText = "Tarif appliqué";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 313;
            // 
            // FrmDetailDevisALaCarte
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(830, 518);
            this.Controls.Add(this.pnlEcheance);
            this.Controls.Add(this.pnlDevis);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvactepropose);
            this.Controls.Add(this.button5);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Garamond", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmDetailDevisALaCarte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modification des tarifs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmUpdateTarifProposition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvactepropose)).EndInit();
            this.pnlDevis.ResumeLayout(false);
            this.pnlEcheance.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dgvactepropose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlDevis;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalAvantRemiseSurActes;
        private System.Windows.Forms.Label lblTotalAvantRemise;
        private System.Windows.Forms.Button btnRistourneGlobal;
        private System.Windows.Forms.Panel pnlEcheance;
        private System.Windows.Forms.Label lblEcheances;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQte;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarif;
        private System.Windows.Forms.DataGridViewButtonColumn colBtn;
        private System.Windows.Forms.DataGridViewImageColumn ColDel;
    }
}