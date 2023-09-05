namespace BaseCommonControls
{
    partial class frmEditCorrespondant
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Telephones", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Fax", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("E-Mails", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Adresses", System.Windows.Forms.HorizontalAlignment.Left);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxNom = new System.Windows.Forms.TextBox();
            this.txtbxPrenom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvContact = new System.Windows.Forms.ListView();
            this.colLibe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxNumSecu = new System.Windows.Forms.MaskedTextBox();
            this.BtnAddCorrespondant = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbTu = new System.Windows.Forms.RadioButton();
            this.btnCopy = new System.Windows.Forms.Button();
            this.ucNotes1 = new BaseCommonControls.ucNotes();
            this.txtbxProfession = new System.Windows.Forms.TextBox();
            this.cbxSexe = new BaseCommonControls.SlindingBtn();
            this.cbxCivilite = new BaseCommonControls.SlindingBtn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.envoyerUnEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(765, 519);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(91, 71);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(864, 519);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 71);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 18);
            this.label8.TabIndex = 20;
            this.label8.Text = "Note : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "Sexe : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 18);
            this.label6.TabIndex = 18;
            this.label6.Text = "Prénom : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "Nom : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Civilité : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "Profession : ";
            // 
            // txtbxNom
            // 
            this.txtbxNom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbxNom.Location = new System.Drawing.Point(144, 58);
            this.txtbxNom.Name = "txtbxNom";
            this.txtbxNom.Size = new System.Drawing.Size(257, 25);
            this.txtbxNom.TabIndex = 2;
            this.txtbxNom.Leave += new System.EventHandler(this.txtbxNom_Leave);
            // 
            // txtbxPrenom
            // 
            this.txtbxPrenom.Location = new System.Drawing.Point(144, 89);
            this.txtbxPrenom.Name = "txtbxPrenom";
            this.txtbxPrenom.Size = new System.Drawing.Size(257, 25);
            this.txtbxPrenom.TabIndex = 3;
            this.txtbxPrenom.TextChanged += new System.EventHandler(this.txtbxPrenom_TextChanged);
            this.txtbxPrenom.Leave += new System.EventHandler(this.txtbxPrenom_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 57;
            this.label3.Text = "Contacts : ";
            // 
            // lvContact
            // 
            this.lvContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvContact.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLibe,
            this.colValue});
            this.lvContact.ContextMenuStrip = this.contextMenuStrip1;
            this.lvContact.FullRowSelect = true;
            listViewGroup1.Header = "Telephones";
            listViewGroup1.Name = "grpTel";
            listViewGroup2.Header = "Fax";
            listViewGroup2.Name = "grpFax";
            listViewGroup3.Header = "E-Mails";
            listViewGroup3.Name = "grpEMail";
            listViewGroup4.Header = "Adresses";
            listViewGroup4.Name = "grpAdd";
            this.lvContact.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.lvContact.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvContact.Location = new System.Drawing.Point(427, 37);
            this.lvContact.Margin = new System.Windows.Forms.Padding(4);
            this.lvContact.Name = "lvContact";
            this.lvContact.Size = new System.Drawing.Size(532, 463);
            this.lvContact.TabIndex = 56;
            this.lvContact.UseCompatibleStateImageBehavior = false;
            this.lvContact.View = System.Windows.Forms.View.Details;
            this.lvContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvContact_KeyDown);
            this.lvContact.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvContact_MouseDoubleClick);
            // 
            // colLibe
            // 
            this.colLibe.Text = "Libelle";
            this.colLibe.Width = 289;
            // 
            // colValue
            // 
            this.colValue.Text = "Valeur";
            this.colValue.Width = 374;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.TabIndex = 58;
            this.label1.Text = "Num Secu :";
            // 
            // txtbxNumSecu
            // 
            this.txtbxNumSecu.Location = new System.Drawing.Point(144, 151);
            this.txtbxNumSecu.Mask = "0 00 00 00 000 000 00";
            this.txtbxNumSecu.Name = "txtbxNumSecu";
            this.txtbxNumSecu.Size = new System.Drawing.Size(257, 25);
            this.txtbxNumSecu.TabIndex = 5;
            // 
            // BtnAddCorrespondant
            // 
            this.BtnAddCorrespondant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddCorrespondant.BackColor = System.Drawing.Color.Gainsboro;
            this.BtnAddCorrespondant.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnAddCorrespondant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddCorrespondant.Location = new System.Drawing.Point(886, 36);
            this.BtnAddCorrespondant.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddCorrespondant.Name = "BtnAddCorrespondant";
            this.BtnAddCorrespondant.Size = new System.Drawing.Size(73, 26);
            this.BtnAddCorrespondant.TabIndex = 10;
            this.BtnAddCorrespondant.Text = "Ajouter";
            this.BtnAddCorrespondant.UseVisualStyleBackColor = false;
            this.BtnAddCorrespondant.Click += new System.EventHandler(this.BtnAddCorrespondant_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-2, 254);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 18);
            this.label13.TabIndex = 66;
            this.label13.Text = "Cette personne est : ";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(229, 252);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(90, 22);
            this.radioButton1.TabIndex = 65;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Vouvoyée";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rbTu
            // 
            this.rbTu.AutoSize = true;
            this.rbTu.Location = new System.Drawing.Point(143, 252);
            this.rbTu.Margin = new System.Windows.Forms.Padding(4);
            this.rbTu.Name = "rbTu";
            this.rbTu.Size = new System.Drawing.Size(78, 22);
            this.rbTu.TabIndex = 8;
            this.rbTu.Text = "Tutoyée";
            this.rbTu.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(886, 69);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(73, 27);
            this.btnCopy.TabIndex = 68;
            this.btnCopy.Text = "Copier...";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucNotes1
            // 
            this.ucNotes1.ClipOnStars = false;
            this.ucNotes1.Location = new System.Drawing.Point(144, 289);
            this.ucNotes1.Margin = new System.Windows.Forms.Padding(6);
            this.ucNotes1.Max = 20;
            this.ucNotes1.Min = 0;
            this.ucNotes1.Name = "ucNotes1";
            this.ucNotes1.NbEtoiles = 5;
            this.ucNotes1.ReadOnly = false;
            this.ucNotes1.RealValue = 0;
            this.ucNotes1.Size = new System.Drawing.Size(146, 26);
            this.ucNotes1.TabIndex = 9;
            this.ucNotes1.Value = 0F;
            // 
            // txtbxProfession
            // 
            this.txtbxProfession.Location = new System.Drawing.Point(144, 120);
            this.txtbxProfession.Name = "txtbxProfession";
            this.txtbxProfession.Size = new System.Drawing.Size(257, 25);
            this.txtbxProfession.TabIndex = 4;
            // 
            // cbxSexe
            // 
            this.cbxSexe.BackColor = System.Drawing.Color.Gainsboro;
            this.cbxSexe.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cbxSexe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSexe.Location = new System.Drawing.Point(144, 213);
            this.cbxSexe.Name = "cbxSexe";
            this.cbxSexe.SelectedNode = null;
            this.cbxSexe.Size = new System.Drawing.Size(87, 32);
            this.cbxSexe.TabIndex = 7;
            this.cbxSexe.Text = "Masculin";
            this.cbxSexe.UseVisualStyleBackColor = false;
            this.cbxSexe.WindowHeight = -1;
            this.cbxSexe.WindowWidth = 400;
            this.cbxSexe.WrapMode = false;
            // 
            // cbxCivilite
            // 
            this.cbxCivilite.BackColor = System.Drawing.Color.Gainsboro;
            this.cbxCivilite.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cbxCivilite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxCivilite.Location = new System.Drawing.Point(144, 15);
            this.cbxCivilite.Name = "cbxCivilite";
            this.cbxCivilite.SelectedNode = null;
            this.cbxCivilite.Size = new System.Drawing.Size(87, 25);
            this.cbxCivilite.TabIndex = 1;
            this.cbxCivilite.UseVisualStyleBackColor = false;
            this.cbxCivilite.WindowHeight = -1;
            this.cbxCivilite.WindowWidth = 400;
            this.cbxCivilite.WrapMode = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.envoyerUnEmailToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 26);
            // 
            // envoyerUnEmailToolStripMenuItem
            // 
            this.envoyerUnEmailToolStripMenuItem.Name = "envoyerUnEmailToolStripMenuItem";
            this.envoyerUnEmailToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.envoyerUnEmailToolStripMenuItem.Text = "Envoyer un e-mail";
            this.envoyerUnEmailToolStripMenuItem.Click += new System.EventHandler(this.envoyerUnEmailToolStripMenuItem_Click);
            // 
            // frmEditCorrespondant
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(968, 603);
            this.Controls.Add(this.cbxCivilite);
            this.Controls.Add(this.cbxSexe);
            this.Controls.Add(this.txtbxProfession);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.rbTu);
            this.Controls.Add(this.BtnAddCorrespondant);
            this.Controls.Add(this.txtbxNumSecu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvContact);
            this.Controls.Add(this.txtbxPrenom);
            this.Controls.Add(this.txtbxNom);
            this.Controls.Add(this.ucNotes1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEditCorrespondant";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edition";
            this.Load += new System.EventHandler(this.frmEditCorrespondant_Load);
            this.Shown += new System.EventHandler(this.frmEditCorrespondant_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private BaseCommonControls.ucNotes ucNotes1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxNom;
        private System.Windows.Forms.TextBox txtbxPrenom;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ListView lvContact;
        private System.Windows.Forms.ColumnHeader colLibe;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtbxNumSecu;
        private System.Windows.Forms.Button BtnAddCorrespondant;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rbTu;
        private System.Windows.Forms.Button btnCopy;
        private SlindingBtn cbxSexe;
        private SlindingBtn cbxCivilite;
        public System.Windows.Forms.TextBox txtbxProfession;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem envoyerUnEmailToolStripMenuItem;
    }
}