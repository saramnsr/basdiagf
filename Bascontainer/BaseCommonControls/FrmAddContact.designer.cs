namespace BaseCommonControls
{
    partial class frmAddContact
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlTelephone = new System.Windows.Forms.Panel();
            this.txtInd = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtValTelephone = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMail = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbxMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlAdresse = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxVille = new System.Windows.Forms.TextBox();
            this.txtbxCP = new System.Windows.Forms.TextBox();
            this.txtbxAdr2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtbxAdr1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAdresse = new System.Windows.Forms.RadioButton();
            this.rbMail = new System.Windows.Forms.RadioButton();
            this.rbTel = new System.Windows.Forms.RadioButton();
            this.cbxLibTel = new BaseCommonControls.SlindingBtn();
            this.cbxLibMail = new BaseCommonControls.SlindingBtn();
            this.cbxLibAddr = new BaseCommonControls.SlindingBtn();
            this.pnlContainer.SuspendLayout();
            this.pnlTelephone.SuspendLayout();
            this.pnlMail.SuspendLayout();
            this.pnlAdresse.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type de contact :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tél :";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(330, 300);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 73);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Location = new System.Drawing.Point(438, 300);
            this.btnAnnuler.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(100, 73);
            this.btnAnnuler.TabIndex = 6;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlTelephone);
            this.pnlContainer.Controls.Add(this.pnlMail);
            this.pnlContainer.Controls.Add(this.pnlAdresse);
            this.pnlContainer.Location = new System.Drawing.Point(50, 94);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(489, 192);
            this.pnlContainer.TabIndex = 7;
            // 
            // pnlTelephone
            // 
            this.pnlTelephone.Controls.Add(this.txtInd);
            this.pnlTelephone.Controls.Add(this.checkBox1);
            this.pnlTelephone.Controls.Add(this.cbxLibTel);
            this.pnlTelephone.Controls.Add(this.txtValTelephone);
            this.pnlTelephone.Controls.Add(this.label3);
            this.pnlTelephone.Controls.Add(this.label2);
            this.pnlTelephone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTelephone.Location = new System.Drawing.Point(0, 0);
            this.pnlTelephone.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTelephone.Name = "pnlTelephone";
            this.pnlTelephone.Size = new System.Drawing.Size(489, 192);
            this.pnlTelephone.TabIndex = 0;
            // 
            // txtInd
            // 
            this.txtInd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtInd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtInd.DropDownHeight = 150;
            this.txtInd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtInd.DropDownWidth = 80;
            this.txtInd.FormattingEnabled = true;
            this.txtInd.IntegralHeight = false;
            this.txtInd.Location = new System.Drawing.Point(81, 62);
            this.txtInd.Name = "txtInd";
            this.txtInd.Size = new System.Drawing.Size(86, 25);
            this.txtInd.TabIndex = 16;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(411, 64);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 21);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "SMS";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtValTelephone
            // 
            this.txtValTelephone.Location = new System.Drawing.Point(173, 62);
            this.txtValTelephone.Name = "txtValTelephone";
            this.txtValTelephone.Size = new System.Drawing.Size(232, 24);
            this.txtValTelephone.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Libelle : ";
            // 
            // pnlMail
            // 
            this.pnlMail.Controls.Add(this.cbxLibMail);
            this.pnlMail.Controls.Add(this.label6);
            this.pnlMail.Controls.Add(this.txtbxMail);
            this.pnlMail.Controls.Add(this.label7);
            this.pnlMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMail.Location = new System.Drawing.Point(0, 0);
            this.pnlMail.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMail.Name = "pnlMail";
            this.pnlMail.Size = new System.Drawing.Size(489, 192);
            this.pnlMail.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Libelle : ";
            // 
            // txtbxMail
            // 
            this.txtbxMail.Location = new System.Drawing.Point(109, 55);
            this.txtbxMail.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxMail.Name = "txtbxMail";
            this.txtbxMail.Size = new System.Drawing.Size(324, 24);
            this.txtbxMail.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Valeur :";
            // 
            // pnlAdresse
            // 
            this.pnlAdresse.Controls.Add(this.label5);
            this.pnlAdresse.Controls.Add(this.label4);
            this.pnlAdresse.Controls.Add(this.cbxLibAddr);
            this.pnlAdresse.Controls.Add(this.txtbxVille);
            this.pnlAdresse.Controls.Add(this.txtbxCP);
            this.pnlAdresse.Controls.Add(this.txtbxAdr2);
            this.pnlAdresse.Controls.Add(this.label8);
            this.pnlAdresse.Controls.Add(this.txtbxAdr1);
            this.pnlAdresse.Controls.Add(this.label9);
            this.pnlAdresse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAdresse.Location = new System.Drawing.Point(0, 0);
            this.pnlAdresse.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAdresse.Name = "pnlAdresse";
            this.pnlAdresse.Size = new System.Drawing.Size(489, 192);
            this.pnlAdresse.TabIndex = 7;
            this.pnlAdresse.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAdresse_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Code postal / Ville :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Adresse 2 :";
            // 
            // txtbxVille
            // 
            this.txtbxVille.Location = new System.Drawing.Point(262, 125);
            this.txtbxVille.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxVille.Name = "txtbxVille";
            this.txtbxVille.Size = new System.Drawing.Size(219, 24);
            this.txtbxVille.TabIndex = 11;
            // 
            // txtbxCP
            // 
            this.txtbxCP.Location = new System.Drawing.Point(157, 125);
            this.txtbxCP.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxCP.Name = "txtbxCP";
            this.txtbxCP.Size = new System.Drawing.Size(96, 24);
            this.txtbxCP.TabIndex = 10;
            this.txtbxCP.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtbxAdr2
            // 
            this.txtbxAdr2.Location = new System.Drawing.Point(157, 91);
            this.txtbxAdr2.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxAdr2.Name = "txtbxAdr2";
            this.txtbxAdr2.Size = new System.Drawing.Size(324, 24);
            this.txtbxAdr2.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Libelle : ";
            // 
            // txtbxAdr1
            // 
            this.txtbxAdr1.Location = new System.Drawing.Point(157, 57);
            this.txtbxAdr1.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxAdr1.Name = "txtbxAdr1";
            this.txtbxAdr1.Size = new System.Drawing.Size(324, 24);
            this.txtbxAdr1.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 59);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Adresse principal :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbAdresse);
            this.panel1.Controls.Add(this.rbMail);
            this.panel1.Controls.Add(this.rbTel);
            this.panel1.Location = new System.Drawing.Point(154, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 59);
            this.panel1.TabIndex = 8;
            // 
            // rbAdresse
            // 
            this.rbAdresse.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAdresse.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbAdresse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbAdresse.Location = new System.Drawing.Point(249, 3);
            this.rbAdresse.Name = "rbAdresse";
            this.rbAdresse.Size = new System.Drawing.Size(83, 53);
            this.rbAdresse.TabIndex = 3;
            this.rbAdresse.Text = "Adresse";
            this.rbAdresse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbAdresse.UseVisualStyleBackColor = true;
            this.rbAdresse.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // rbMail
            // 
            this.rbMail.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbMail.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbMail.Location = new System.Drawing.Point(159, 3);
            this.rbMail.Name = "rbMail";
            this.rbMail.Size = new System.Drawing.Size(83, 53);
            this.rbMail.TabIndex = 2;
            this.rbMail.Text = "E-Mail";
            this.rbMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbMail.UseVisualStyleBackColor = true;
            this.rbMail.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbTel
            // 
            this.rbTel.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTel.Checked = true;
            this.rbTel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbTel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTel.Location = new System.Drawing.Point(69, 3);
            this.rbTel.Name = "rbTel";
            this.rbTel.Size = new System.Drawing.Size(83, 53);
            this.rbTel.TabIndex = 0;
            this.rbTel.TabStop = true;
            this.rbTel.Text = "Téléphone";
            this.rbTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbTel.UseVisualStyleBackColor = true;
            this.rbTel.CheckedChanged += new System.EventHandler(this.rbTel_CheckedChanged);
            // 
            // cbxLibTel
            // 
            this.cbxLibTel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxLibTel.Location = new System.Drawing.Point(81, 24);
            this.cbxLibTel.Name = "cbxLibTel";
            this.cbxLibTel.SelectedNode = null;
            this.cbxLibTel.Size = new System.Drawing.Size(324, 30);
            this.cbxLibTel.TabIndex = 13;
            this.cbxLibTel.Text = " ";
            this.cbxLibTel.UseVisualStyleBackColor = true;
            this.cbxLibTel.WindowHeight = 200;
            this.cbxLibTel.WindowWidth = 400;
            this.cbxLibTel.WrapMode = true;
            this.cbxLibTel.Click += new System.EventHandler(this.cbxLibTel_Click);
            // 
            // cbxLibMail
            // 
            this.cbxLibMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxLibMail.Location = new System.Drawing.Point(109, 17);
            this.cbxLibMail.Name = "cbxLibMail";
            this.cbxLibMail.SelectedNode = null;
            this.cbxLibMail.Size = new System.Drawing.Size(324, 30);
            this.cbxLibMail.TabIndex = 13;
            this.cbxLibMail.Text = " ";
            this.cbxLibMail.UseVisualStyleBackColor = true;
            this.cbxLibMail.WindowHeight = 200;
            this.cbxLibMail.WindowWidth = 400;
            this.cbxLibMail.WrapMode = true;
            // 
            // cbxLibAddr
            // 
            this.cbxLibAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxLibAddr.Location = new System.Drawing.Point(157, 18);
            this.cbxLibAddr.Name = "cbxLibAddr";
            this.cbxLibAddr.SelectedNode = null;
            this.cbxLibAddr.Size = new System.Drawing.Size(324, 30);
            this.cbxLibAddr.TabIndex = 12;
            this.cbxLibAddr.Text = " ";
            this.cbxLibAddr.UseVisualStyleBackColor = true;
            this.cbxLibAddr.WindowHeight = 200;
            this.cbxLibAddr.WindowWidth = 400;
            this.cbxLibAddr.WrapMode = true;
            // 
            // frmAddContact
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(552, 382);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAddContact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajouter/Editer un contact";
            this.Load += new System.EventHandler(this.frmAddContact_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlTelephone.ResumeLayout(false);
            this.pnlTelephone.PerformLayout();
            this.pnlMail.ResumeLayout(false);
            this.pnlMail.PerformLayout();
            this.pnlAdresse.ResumeLayout(false);
            this.pnlAdresse.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlTelephone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlMail;
        private System.Windows.Forms.Panel pnlAdresse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbxMail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbAdresse;
        private System.Windows.Forms.RadioButton rbMail;
        private System.Windows.Forms.RadioButton rbTel;
        private System.Windows.Forms.MaskedTextBox txtValTelephone;
        private BaseCommonControls.SlindingBtn cbxLibTel;
        private BaseCommonControls.SlindingBtn cbxLibMail;
        private BaseCommonControls.SlindingBtn cbxLibAddr;
        public System.Windows.Forms.TextBox txtbxCP;
        public System.Windows.Forms.TextBox txtbxAdr2;
        public System.Windows.Forms.TextBox txtbxAdr1;
        public System.Windows.Forms.TextBox txtbxVille;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox txtInd;
    }
}