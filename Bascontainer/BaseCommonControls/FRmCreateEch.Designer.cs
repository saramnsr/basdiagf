namespace BaseCommonControls
{
    partial class FRmCreateEch
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkbxPrelevement = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInvisalign = new System.Windows.Forms.RadioButton();
            this.txtbxNbEcheances = new System.Windows.Forms.NumericUpDown();
            this.rbNbEcheances = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxMntParEch = new System.Windows.Forms.TextBox();
            this.rbMntEch = new System.Windows.Forms.RadioButton();
            this.rbJusquau = new System.Windows.Forms.RadioButton();
            this.dtpLastEch = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFirstEch = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtNbPeriodicite = new System.Windows.Forms.TextBox();
            this.lblErreur = new System.Windows.Forms.Label();
            this.chkbxVirement = new System.Windows.Forms.CheckBox();
            this.cbxPrelevement = new System.Windows.Forms.ComboBox();
            this.lstPre = new BaseCommonControls.SlidingList();
            this.cbxPeriodicite = new BaseCommonControls.TreeViewIconCbx();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbxNbEcheances)).BeginInit();
            this.SuspendLayout();
            // 
            // chkbxPrelevement
            // 
            this.chkbxPrelevement.AutoSize = true;
            this.chkbxPrelevement.Location = new System.Drawing.Point(33, 10);
            this.chkbxPrelevement.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxPrelevement.Name = "chkbxPrelevement";
            this.chkbxPrelevement.Size = new System.Drawing.Size(131, 22);
            this.chkbxPrelevement.TabIndex = 36;
            this.chkbxPrelevement.Text = "Par Prelevement";
            this.chkbxPrelevement.UseVisualStyleBackColor = true;
            this.chkbxPrelevement.CheckedChanged += new System.EventHandler(this.chkbxPrelevement_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 296);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.TabIndex = 33;
            this.label2.Text = "Définir une echéance tous les : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbInvisalign);
            this.groupBox1.Controls.Add(this.txtbxNbEcheances);
            this.groupBox1.Controls.Add(this.rbNbEcheances);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtbxMntParEch);
            this.groupBox1.Controls.Add(this.rbMntEch);
            this.groupBox1.Controls.Add(this.rbJusquau);
            this.groupBox1.Controls.Add(this.dtpLastEch);
            this.groupBox1.Location = new System.Drawing.Point(33, 75);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(688, 201);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbInvisalign
            // 
            this.rbInvisalign.AutoSize = true;
            this.rbInvisalign.Location = new System.Drawing.Point(16, 166);
            this.rbInvisalign.Name = "rbInvisalign";
            this.rbInvisalign.Size = new System.Drawing.Size(171, 17);
            this.rbInvisalign.TabIndex = 24;
            this.rbInvisalign.TabStop = true;
            this.rbInvisalign.Text = "Echeances 50% 20% 20% 10%";
            this.rbInvisalign.UseVisualStyleBackColor = true;
            this.rbInvisalign.CheckedChanged += new System.EventHandler(this.rbNbEcheances_CheckedChanged);
            // 
            // txtbxNbEcheances
            // 
            this.txtbxNbEcheances.Location = new System.Drawing.Point(300, 26);
            this.txtbxNbEcheances.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxNbEcheances.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtbxNbEcheances.Name = "txtbxNbEcheances";
            this.txtbxNbEcheances.Size = new System.Drawing.Size(99, 25);
            this.txtbxNbEcheances.TabIndex = 23;
            this.txtbxNbEcheances.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // rbNbEcheances
            // 
            this.rbNbEcheances.AutoSize = true;
            this.rbNbEcheances.Checked = true;
            this.rbNbEcheances.Location = new System.Drawing.Point(16, 26);
            this.rbNbEcheances.Margin = new System.Windows.Forms.Padding(4);
            this.rbNbEcheances.Name = "rbNbEcheances";
            this.rbNbEcheances.Size = new System.Drawing.Size(104, 17);
            this.rbNbEcheances.TabIndex = 22;
            this.rbNbEcheances.TabStop = true;
            this.rbNbEcheances.Text = "Nb echeances : ";
            this.rbNbEcheances.UseVisualStyleBackColor = true;
            this.rbNbEcheances.CheckedChanged += new System.EventHandler(this.rbNbEcheances_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "€";
            // 
            // txtbxMntParEch
            // 
            this.txtbxMntParEch.Enabled = false;
            this.txtbxMntParEch.Location = new System.Drawing.Point(300, 122);
            this.txtbxMntParEch.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxMntParEch.Name = "txtbxMntParEch";
            this.txtbxMntParEch.Size = new System.Drawing.Size(99, 25);
            this.txtbxMntParEch.TabIndex = 19;
            // 
            // rbMntEch
            // 
            this.rbMntEch.AutoSize = true;
            this.rbMntEch.Location = new System.Drawing.Point(16, 122);
            this.rbMntEch.Margin = new System.Windows.Forms.Padding(4);
            this.rbMntEch.Name = "rbMntEch";
            this.rbMntEch.Size = new System.Drawing.Size(142, 17);
            this.rbMntEch.TabIndex = 18;
            this.rbMntEch.Text = "Montant par echéance : ";
            this.rbMntEch.UseVisualStyleBackColor = true;
            this.rbMntEch.CheckedChanged += new System.EventHandler(this.rbNbEcheances_CheckedChanged);
            // 
            // rbJusquau
            // 
            this.rbJusquau.AutoSize = true;
            this.rbJusquau.Location = new System.Drawing.Point(16, 77);
            this.rbJusquau.Margin = new System.Windows.Forms.Padding(4);
            this.rbJusquau.Name = "rbJusquau";
            this.rbJusquau.Size = new System.Drawing.Size(73, 17);
            this.rbJusquau.TabIndex = 16;
            this.rbJusquau.Text = "jusqu\'au : ";
            this.rbJusquau.UseVisualStyleBackColor = true;
            this.rbJusquau.CheckedChanged += new System.EventHandler(this.rbNbEcheances_CheckedChanged);
            // 
            // dtpLastEch
            // 
            this.dtpLastEch.Location = new System.Drawing.Point(300, 76);
            this.dtpLastEch.Margin = new System.Windows.Forms.Padding(4);
            this.dtpLastEch.Name = "dtpLastEch";
            this.dtpLastEch.Size = new System.Drawing.Size(217, 25);
            this.dtpLastEch.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(485, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 18);
            this.label1.TabIndex = 31;
            this.label1.Text = "Premiere échéance : ";
            // 
            // dtpFirstEch
            // 
            this.dtpFirstEch.Location = new System.Drawing.Point(488, 42);
            this.dtpFirstEch.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFirstEch.Name = "dtpFirstEch";
            this.dtpFirstEch.Size = new System.Drawing.Size(233, 25);
            this.dtpFirstEch.TabIndex = 30;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(488, 344);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 64);
            this.btnOk.TabIndex = 37;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(609, 344);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 64);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtNbPeriodicite
            // 
            this.txtNbPeriodicite.Location = new System.Drawing.Point(243, 293);
            this.txtNbPeriodicite.Name = "txtNbPeriodicite";
            this.txtNbPeriodicite.Size = new System.Drawing.Size(72, 25);
            this.txtNbPeriodicite.TabIndex = 39;
            this.txtNbPeriodicite.Text = "1";
            // 
            // lblErreur
            // 
            this.lblErreur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblErreur.Location = new System.Drawing.Point(33, 357);
            this.lblErreur.Name = "lblErreur";
            this.lblErreur.Size = new System.Drawing.Size(448, 29);
            this.lblErreur.TabIndex = 40;
            // 
            // chkbxVirement
            // 
            this.chkbxVirement.AutoSize = true;
            this.chkbxVirement.Location = new System.Drawing.Point(184, 10);
            this.chkbxVirement.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxVirement.Name = "chkbxVirement";
            this.chkbxVirement.Size = new System.Drawing.Size(111, 22);
            this.chkbxVirement.TabIndex = 41;
            this.chkbxVirement.Text = "Par Virement";
            this.chkbxVirement.UseVisualStyleBackColor = true;
            this.chkbxVirement.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbxPrelevement
            // 
            this.cbxPrelevement.FormattingEnabled = true;
            this.cbxPrelevement.Location = new System.Drawing.Point(488, 42);
            this.cbxPrelevement.Name = "cbxPrelevement";
            this.cbxPrelevement.Size = new System.Drawing.Size(233, 26);
            this.cbxPrelevement.TabIndex = 42;
            this.cbxPrelevement.Visible = false;
            // 
            // lstPre
            // 
            this.lstPre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPre.ButtonSize = new System.Drawing.SizeF(70F, 25F);
            this.lstPre.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lstPre.imagelist = null;
            this.lstPre.Location = new System.Drawing.Point(33, 35);
            this.lstPre.Margin = new System.Windows.Forms.Padding(4);
            this.lstPre.MultiSelectMode = false;
            this.lstPre.Name = "lstPre";
            this.lstPre.Size = new System.Drawing.Size(432, 40);
            this.lstPre.TabIndex = 43;
            this.lstPre.Visible = false;
            this.lstPre.WrapMode = false;
            this.lstPre.OnSelectionChange += new System.EventHandler(this.lstPre_OnSelectionChange);
            // 
            // cbxPeriodicite
            // 
            this.cbxPeriodicite.Fnt = null;
            this.cbxPeriodicite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbxPeriodicite.Location = new System.Drawing.Point(324, 293);
            this.cbxPeriodicite.Margin = new System.Windows.Forms.Padding(6);
            this.cbxPeriodicite.Name = "cbxPeriodicite";
            this.cbxPeriodicite.SelectedIndex = -1;
            this.cbxPeriodicite.SelectedItem = null;
            this.cbxPeriodicite.Size = new System.Drawing.Size(185, 25);
            this.cbxPeriodicite.Sorted = false;
            this.cbxPeriodicite.TabIndex = 35;
            this.cbxPeriodicite.VisibleItems = 5;
            // 
            // FRmCreateEch
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(736, 421);
            this.Controls.Add(this.lstPre);
            this.Controls.Add(this.cbxPrelevement);
            this.Controls.Add(this.chkbxVirement);
            this.Controls.Add(this.lblErreur);
            this.Controls.Add(this.txtNbPeriodicite);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkbxPrelevement);
            this.Controls.Add(this.cbxPeriodicite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFirstEch);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FRmCreateEch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Détailler l\'échéance";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FRmCreateEch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbxNbEcheances)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbxPrelevement;
        private BaseCommonControls.TreeViewIconCbx cbxPeriodicite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtbxNbEcheances;
        private System.Windows.Forms.RadioButton rbNbEcheances;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbxMntParEch;
        private System.Windows.Forms.RadioButton rbMntEch;
        private System.Windows.Forms.RadioButton rbJusquau;
        private System.Windows.Forms.DateTimePicker dtpLastEch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFirstEch;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtNbPeriodicite;
        private System.Windows.Forms.Label lblErreur;
        private System.Windows.Forms.RadioButton rbInvisalign;
        private System.Windows.Forms.CheckBox chkbxVirement;
        private System.Windows.Forms.ComboBox cbxPrelevement;
        private BaseCommonControls.SlidingList lstPre;
    }
}