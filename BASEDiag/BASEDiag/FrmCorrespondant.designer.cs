namespace BASEDiag
{
    partial class FrmCorrespondant
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
            this.txtbxPrenom = new System.Windows.Forms.TextBox();
            this.cbxCivilite = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxSexe = new System.Windows.Forms.ComboBox();
            this.txtbxNom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbxProfession = new System.Windows.Forms.TextBox();
            this.cbxTypeCorrespondant = new System.Windows.Forms.ComboBox();
            this.TypeCorrespondant = new System.Windows.Forms.Label();
            this.SuggestBxCorrespondant = new BASEDiag.Ctrls.SuggestBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFax = new System.Windows.Forms.RadioButton();
            this.rbEmail = new System.Windows.Forms.RadioButton();
            this.rbCourrier = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(265, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 59);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(184, 387);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 59);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtbxPrenom
            // 
            this.txtbxPrenom.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxPrenom.Location = new System.Drawing.Point(126, 200);
            this.txtbxPrenom.Name = "txtbxPrenom";
            this.txtbxPrenom.Size = new System.Drawing.Size(209, 25);
            this.txtbxPrenom.TabIndex = 4;
            // 
            // cbxCivilite
            // 
            this.cbxCivilite.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCivilite.FormattingEnabled = true;
            this.cbxCivilite.Items.AddRange(new object[] {
            "M",
            "Me",
            "Mlle",
            "Mme",
            "MM",
            "Dr",
            "Pr",
            "P",
            "Sr"});
            this.cbxCivilite.Location = new System.Drawing.Point(126, 128);
            this.cbxCivilite.Name = "cbxCivilite";
            this.cbxCivilite.Size = new System.Drawing.Size(121, 26);
            this.cbxCivilite.TabIndex = 1;
            this.cbxCivilite.SelectedIndexChanged += new System.EventHandler(this.cbxCivilite_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Prénom : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nom : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(54, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 19;
            this.label4.Text = "Civilité : ";
            // 
            // cbxSexe
            // 
            this.cbxSexe.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSexe.FormattingEnabled = true;
            this.cbxSexe.Items.AddRange(new object[] {
            "M",
            "F"});
            this.cbxSexe.Location = new System.Drawing.Point(253, 128);
            this.cbxSexe.Name = "cbxSexe";
            this.cbxSexe.Size = new System.Drawing.Size(82, 26);
            this.cbxSexe.TabIndex = 2;
            // 
            // txtbxNom
            // 
            this.txtbxNom.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxNom.Location = new System.Drawing.Point(126, 163);
            this.txtbxNom.Name = "txtbxNom";
            this.txtbxNom.Size = new System.Drawing.Size(209, 25);
            this.txtbxNom.TabIndex = 3;
            this.txtbxNom.TextChanged += new System.EventHandler(this.txtbxNom_TextChanged);
            this.txtbxNom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbxNom_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 18);
            this.label5.TabIndex = 50;
            this.label5.Text = "Profession : ";
            // 
            // txtbxProfession
            // 
            this.txtbxProfession.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxProfession.Location = new System.Drawing.Point(126, 231);
            this.txtbxProfession.Name = "txtbxProfession";
            this.txtbxProfession.Size = new System.Drawing.Size(209, 25);
            this.txtbxProfession.TabIndex = 8;
            // 
            // cbxTypeCorrespondant
            // 
            this.cbxTypeCorrespondant.FormattingEnabled = true;
            this.cbxTypeCorrespondant.Location = new System.Drawing.Point(126, 79);
            this.cbxTypeCorrespondant.Name = "cbxTypeCorrespondant";
            this.cbxTypeCorrespondant.Size = new System.Drawing.Size(209, 21);
            this.cbxTypeCorrespondant.TabIndex = 64;
            this.cbxTypeCorrespondant.SelectedIndexChanged += new System.EventHandler(this.cbxTypeCorrespondant_SelectedIndexChanged);
            // 
            // TypeCorrespondant
            // 
            this.TypeCorrespondant.AutoSize = true;
            this.TypeCorrespondant.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeCorrespondant.Location = new System.Drawing.Point(54, 82);
            this.TypeCorrespondant.Name = "TypeCorrespondant";
            this.TypeCorrespondant.Size = new System.Drawing.Size(66, 18);
            this.TypeCorrespondant.TabIndex = 65;
            this.TypeCorrespondant.Text = "Civilité : ";
            // 
            // SuggestBxCorrespondant
            // 
            this.SuggestBxCorrespondant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.SuggestBxCorrespondant.currentDistance = 0F;
            this.SuggestBxCorrespondant.Dock = System.Windows.Forms.DockStyle.Top;
            this.SuggestBxCorrespondant.FormattedText = "Souhaitiez-vous parler de %s ?";
            this.SuggestBxCorrespondant.LabelText = "";
            this.SuggestBxCorrespondant.Location = new System.Drawing.Point(0, 0);
            this.SuggestBxCorrespondant.MinDistanceForSuggest = 3.402823E+38F;
            this.SuggestBxCorrespondant.Name = "SuggestBxCorrespondant";
            this.SuggestBxCorrespondant.Size = new System.Drawing.Size(352, 31);
            this.SuggestBxCorrespondant.SuggestionList = null;
            this.SuggestBxCorrespondant.TabIndex = 47;
            this.SuggestBxCorrespondant.Visible = false;
            this.SuggestBxCorrespondant.OnYesClick += new System.EventHandler(this.SuggestBxCorrespondant_OnYesClick);
            this.SuggestBxCorrespondant.OnFound += new System.EventHandler(this.SuggestBxCorrespondant_OnFound);
            this.SuggestBxCorrespondant.Load += new System.EventHandler(this.SuggestBxCorrespondant_Load);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFax);
            this.groupBox1.Controls.Add(this.rbEmail);
            this.groupBox1.Controls.Add(this.rbCourrier);
            this.groupBox1.Location = new System.Drawing.Point(36, 307);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 62);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Préférence de communication";
            // 
            // rbFax
            // 
            this.rbFax.AutoSize = true;
            this.rbFax.Location = new System.Drawing.Point(193, 28);
            this.rbFax.Name = "rbFax";
            this.rbFax.Size = new System.Drawing.Size(42, 17);
            this.rbFax.TabIndex = 2;
            this.rbFax.Text = "Fax";
            this.rbFax.UseVisualStyleBackColor = true;
            // 
            // rbEmail
            // 
            this.rbEmail.AutoSize = true;
            this.rbEmail.Location = new System.Drawing.Point(105, 28);
            this.rbEmail.Name = "rbEmail";
            this.rbEmail.Size = new System.Drawing.Size(50, 17);
            this.rbEmail.TabIndex = 1;
            this.rbEmail.Text = "Email";
            this.rbEmail.UseVisualStyleBackColor = true;
            // 
            // rbCourrier
            // 
            this.rbCourrier.AutoSize = true;
            this.rbCourrier.Checked = true;
            this.rbCourrier.Location = new System.Drawing.Point(6, 28);
            this.rbCourrier.Name = "rbCourrier";
            this.rbCourrier.Size = new System.Drawing.Size(61, 17);
            this.rbCourrier.TabIndex = 0;
            this.rbCourrier.TabStop = true;
            this.rbCourrier.Text = "Courrier";
            this.rbCourrier.UseVisualStyleBackColor = true;
            // 
            // FrmCorrespondant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 458);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TypeCorrespondant);
            this.Controls.Add(this.cbxTypeCorrespondant);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtbxProfession);
            this.Controls.Add(this.SuggestBxCorrespondant);
            this.Controls.Add(this.txtbxNom);
            this.Controls.Add(this.cbxSexe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxCivilite);
            this.Controls.Add(this.txtbxPrenom);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCorrespondant";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Correspondant";
            this.Load += new System.EventHandler(this.FrmCorrespondant_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtbxPrenom;
        private System.Windows.Forms.ComboBox cbxCivilite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxSexe;
        private System.Windows.Forms.TextBox txtbxNom;
        private Ctrls.SuggestBox SuggestBxCorrespondant;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbxProfession;
        private System.Windows.Forms.ComboBox cbxTypeCorrespondant;
        private System.Windows.Forms.Label TypeCorrespondant;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFax;
        private System.Windows.Forms.RadioButton rbEmail;
        private System.Windows.Forms.RadioButton rbCourrier;
    }
}