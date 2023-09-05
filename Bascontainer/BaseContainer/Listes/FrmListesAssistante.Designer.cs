namespace WindowsFormsApplication1
{
    partial class FrmListesAssistante
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvRelance = new System.Windows.Forms.DataGridView();
            this.colPatient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateDernierRDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMotf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepuis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnumtent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvSoldeNeg = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateEch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lstbxStatus = new BaseCommonControls.SlidingList();
            this.dgvpatientstatut = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxAssistante = new System.Windows.Forms.ComboBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDernierRDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMotif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArecDepuis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDerniereTentative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelance)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSoldeNeg)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpatientstatut)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(783, 684);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(146, 60);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Fermer";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 78);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(921, 598);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvRelance);
            this.tabPage4.Location = new System.Drawing.Point(4, 27);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(913, 567);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Recontacts";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvRelance
            // 
            this.dgvRelance.AllowUserToAddRows = false;
            this.dgvRelance.AllowUserToDeleteRows = false;
            this.dgvRelance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRelance.BackgroundColor = System.Drawing.Color.White;
            this.dgvRelance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPatient,
            this.colDateDernierRDV,
            this.colMotf,
            this.colDepuis,
            this.colnumtent});
            this.dgvRelance.Location = new System.Drawing.Point(6, 6);
            this.dgvRelance.Name = "dgvRelance";
            this.dgvRelance.ReadOnly = true;
            this.dgvRelance.RowHeadersVisible = false;
            this.dgvRelance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelance.Size = new System.Drawing.Size(895, 555);
            this.dgvRelance.TabIndex = 4;
            this.dgvRelance.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRelance_CellMouseDoubleClick);
            this.dgvRelance.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvRelance_MouseDoubleClick);
            // 
            // colPatient
            // 
            this.colPatient.HeaderText = "Patient";
            this.colPatient.Name = "colPatient";
            this.colPatient.ReadOnly = true;
            // 
            // colDateDernierRDV
            // 
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.colDateDernierRDV.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDateDernierRDV.HeaderText = "Dernier RDV";
            this.colDateDernierRDV.Name = "colDateDernierRDV";
            this.colDateDernierRDV.ReadOnly = true;
            // 
            // colMotf
            // 
            this.colMotf.HeaderText = "Motif";
            this.colMotf.Name = "colMotf";
            this.colMotf.ReadOnly = true;
            // 
            // colDepuis
            // 
            dataGridViewCellStyle2.Format = "d";
            this.colDepuis.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDepuis.HeaderText = "Depuisle";
            this.colDepuis.Name = "colDepuis";
            this.colDepuis.ReadOnly = true;
            // 
            // colnumtent
            // 
            this.colnumtent.HeaderText = "Num tentative";
            this.colnumtent.Name = "colnumtent";
            this.colnumtent.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvSoldeNeg);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(913, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Patient avec Solde negatif en RDV ce jour";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvSoldeNeg
            // 
            this.dgvSoldeNeg.AllowUserToAddRows = false;
            this.dgvSoldeNeg.AllowUserToDeleteRows = false;
            this.dgvSoldeNeg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSoldeNeg.BackgroundColor = System.Drawing.Color.White;
            this.dgvSoldeNeg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSoldeNeg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.colDateEch,
            this.colMontant});
            this.dgvSoldeNeg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSoldeNeg.Location = new System.Drawing.Point(4, 4);
            this.dgvSoldeNeg.Name = "dgvSoldeNeg";
            this.dgvSoldeNeg.ReadOnly = true;
            this.dgvSoldeNeg.RowHeadersVisible = false;
            this.dgvSoldeNeg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSoldeNeg.Size = new System.Drawing.Size(905, 559);
            this.dgvSoldeNeg.TabIndex = 3;
            this.dgvSoldeNeg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSoldeNeg_MouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle3.Format = "C2";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "Patient";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // colDateEch
            // 
            dataGridViewCellStyle4.Format = "d";
            this.colDateEch.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDateEch.HeaderText = "Date d\'échéance";
            this.colDateEch.Name = "colDateEch";
            this.colDateEch.ReadOnly = true;
            // 
            // colMontant
            // 
            dataGridViewCellStyle5.Format = "C2";
            this.colMontant.DefaultCellStyle = dataGridViewCellStyle5;
            this.colMontant.HeaderText = "Montant";
            this.colMontant.Name = "colMontant";
            this.colMontant.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lstbxStatus);
            this.tabPage3.Controls.Add(this.dgvpatientstatut);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(913, 567);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Personne avec un certain statut";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // lstbxStatus
            // 
            this.lstbxStatus.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.lstbxStatus.imagelist = null;
            this.lstbxStatus.Location = new System.Drawing.Point(9, 9);
            this.lstbxStatus.Margin = new System.Windows.Forms.Padding(6);
            this.lstbxStatus.Name = "lstbxStatus";
            this.lstbxStatus.Size = new System.Drawing.Size(895, 126);
            this.lstbxStatus.TabIndex = 7;
            this.lstbxStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstbxStatus_MouseClick);
            // 
            // dgvpatientstatut
            // 
            this.dgvpatientstatut.AllowUserToAddRows = false;
            this.dgvpatientstatut.AllowUserToDeleteRows = false;
            this.dgvpatientstatut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvpatientstatut.BackgroundColor = System.Drawing.Color.White;
            this.dgvpatientstatut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpatientstatut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6});
            this.dgvpatientstatut.Location = new System.Drawing.Point(9, 144);
            this.dgvpatientstatut.Name = "dgvpatientstatut";
            this.dgvpatientstatut.ReadOnly = true;
            this.dgvpatientstatut.RowHeadersVisible = false;
            this.dgvpatientstatut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpatientstatut.Size = new System.Drawing.Size(895, 482);
            this.dgvpatientstatut.TabIndex = 3;
            this.dgvpatientstatut.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvpatientstatut_MouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle6.Format = "C2";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn6.HeaderText = "Patient";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxAssistante);
            this.groupBox1.Location = new System.Drawing.Point(13, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(921, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrer les listes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Par responsable : ";
            // 
            // cbxAssistante
            // 
            this.cbxAssistante.FormattingEnabled = true;
            this.cbxAssistante.Location = new System.Drawing.Point(132, 24);
            this.cbxAssistante.Name = "cbxAssistante";
            this.cbxAssistante.Size = new System.Drawing.Size(313, 26);
            this.cbxAssistante.TabIndex = 0;
            this.cbxAssistante.SelectedIndexChanged += new System.EventHandler(this.cbxAssistante_SelectedIndexChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Patient";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 902;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.Format = "dd/MM/yyyy HH:mm";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Dernier RDV";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 451;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle8.Format = "d";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.HeaderText = "Date d\'échéance";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle9.Format = "C2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn5.HeaderText = "Montant";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 301;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle10.Format = "d";
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn7.HeaderText = "Patient";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 300;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle11.Format = "dd/MM/yyyy HH:mm";
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn8.HeaderText = "DernierRDV";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 179;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle12.Format = "C2";
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn9.HeaderText = "Motif";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 178;
            // 
            // colDernierRDV
            // 
            dataGridViewCellStyle13.Format = "dd/MM/yyyy HH:mm";
            this.colDernierRDV.DefaultCellStyle = dataGridViewCellStyle13;
            this.colDernierRDV.HeaderText = "DernierRDV";
            this.colDernierRDV.Name = "colDernierRDV";
            this.colDernierRDV.ReadOnly = true;
            // 
            // colMotif
            // 
            this.colMotif.HeaderText = "Motif";
            this.colMotif.Name = "colMotif";
            this.colMotif.ReadOnly = true;
            // 
            // colArecDepuis
            // 
            dataGridViewCellStyle14.Format = "dd/MM/yyyy";
            this.colArecDepuis.DefaultCellStyle = dataGridViewCellStyle14;
            this.colArecDepuis.HeaderText = "A recontacter depuis";
            this.colArecDepuis.Name = "colArecDepuis";
            this.colArecDepuis.ReadOnly = true;
            // 
            // colDerniereTentative
            // 
            this.colDerniereTentative.HeaderText = "Derniere tentative";
            this.colDerniereTentative.Name = "colDerniereTentative";
            this.colDerniereTentative.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle15.Format = "dd/MM/yyyy";
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn10.HeaderText = "A recontacter depuis";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 179;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Derniere tentative";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 178;
            // 
            // FrmListesAssistante
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(947, 760);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmListesAssistante";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listes Assistante";
            this.Load += new System.EventHandler(this.FrmListesAssistante_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelance)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSoldeNeg)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvpatientstatut)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dgvSoldeNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateEch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMontant;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvpatientstatut;
        private BaseCommonControls.SlidingList lstbxStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvRelance;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDernierRDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMotif;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArecDepuis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDerniereTentative;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxAssistante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateDernierRDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMotf;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepuis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnumtent;
    }
}