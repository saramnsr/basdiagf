namespace WindowsFormsApplication1
{
    partial class FrmRecontactMgmt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecontactMgmt));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Telephones", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Fax", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("E-Mails", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Adresses", System.Windows.Forms.HorizontalAlignment.Left);
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.cbxutilisateur = new BaseCommonControls.SlidingList();
            this.pnlPriseContact = new WizardBase.IntermediateStep();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lvContact = new System.Windows.Forms.ListView();
            this.colLibe = new System.Windows.Forms.ColumnHeader();
            this.colValue = new System.Windows.Forms.ColumnHeader();
            this.pnlIsJoignable = new WizardBase.IntermediateStep();
            this.label5 = new System.Windows.Forms.Label();
            this.rbOui = new System.Windows.Forms.RadioButton();
            this.rbnon = new System.Windows.Forms.RadioButton();
            this.pnlPoseRDV = new WizardBase.IntermediateStep();
            this.btnOpenRHBase = new System.Windows.Forms.Button();
            this.pnlAutreContact = new WizardBase.IntermediateStep();
            this.rbDate = new System.Windows.Forms.RadioButton();
            this.rb30 = new System.Windows.Forms.RadioButton();
            this.rb15 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateRecontact = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMotifRecontact = new System.Windows.Forms.ComboBox();
            this.finishStep1 = new WizardBase.FinishStep();
            this.startStep1.SuspendLayout();
            this.pnlPriseContact.SuspendLayout();
            this.pnlIsJoignable.SuspendLayout();
            this.pnlPoseRDV.SuspendLayout();
            this.pnlAutreContact.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = true;
            this.wizardControl1.BackButtonText = "< Precedent";
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonText = "Annuler";
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.EulaButtonEnabled = false;
            this.wizardControl1.EulaButtonText = "eula";
            this.wizardControl1.EulaButtonVisible = false;
            this.wizardControl1.HelpButtonEnabled = false;
            this.wizardControl1.HelpButtonVisible = false;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonText = "Suivant >";
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(557, 405);
            this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.pnlPriseContact,
            this.pnlIsJoignable,
            this.pnlPoseRDV,
            this.pnlAutreContact,
            this.finishStep1});
            this.wizardControl1.Click += new System.EventHandler(this.wizardControl1_Click);
            this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
            this.wizardControl1.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_NextButtonClick);
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.cbxutilisateur);
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "Patient à recontacter";
            this.startStep1.Title = "Recontact du patient\r\n";
            // 
            // cbxutilisateur
            // 
            this.cbxutilisateur.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.cbxutilisateur.imagelist = null;
            this.cbxutilisateur.Location = new System.Drawing.Point(173, 45);
            this.cbxutilisateur.Name = "cbxutilisateur";
            this.cbxutilisateur.NbLigne = 2;
            this.cbxutilisateur.Size = new System.Drawing.Size(372, 217);
            this.cbxutilisateur.TabIndex = 0;
            this.cbxutilisateur.ClickNode += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.cbxutilisateur_ClickNode);
            // 
            // pnlPriseContact
            // 
            this.pnlPriseContact.BindingImage = ((System.Drawing.Image)(resources.GetObject("pnlPriseContact.BindingImage")));
            this.pnlPriseContact.Controls.Add(this.btnAdd);
            this.pnlPriseContact.Controls.Add(this.lvContact);
            this.pnlPriseContact.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlPriseContact.Name = "pnlPriseContact";
            this.pnlPriseContact.Subtitle = "Informations necessaire à la prise de contact avec le patient";
            this.pnlPriseContact.Title = "Prise de contact";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(7205, 67);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(32, 32);
            this.btnAdd.TabIndex = 56;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvContact
            // 
            this.lvContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvContact.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLibe,
            this.colValue});
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
            this.lvContact.Location = new System.Drawing.Point(4, 67);
            this.lvContact.Margin = new System.Windows.Forms.Padding(4);
            this.lvContact.Name = "lvContact";
            this.lvContact.Size = new System.Drawing.Size(7233, 4674);
            this.lvContact.TabIndex = 55;
            this.lvContact.UseCompatibleStateImageBehavior = false;
            this.lvContact.View = System.Windows.Forms.View.Details;
            this.lvContact.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvContact_MouseDoubleClick);
            this.lvContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvContact_KeyDown);
            // 
            // colLibe
            // 
            this.colLibe.Text = "Libelle";
            this.colLibe.Width = 144;
            // 
            // colValue
            // 
            this.colValue.Text = "Valeur";
            this.colValue.Width = 374;
            // 
            // pnlIsJoignable
            // 
            this.pnlIsJoignable.BindingImage = ((System.Drawing.Image)(resources.GetObject("pnlIsJoignable.BindingImage")));
            this.pnlIsJoignable.Controls.Add(this.label5);
            this.pnlIsJoignable.Controls.Add(this.rbOui);
            this.pnlIsJoignable.Controls.Add(this.rbnon);
            this.pnlIsJoignable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlIsJoignable.Name = "pnlIsJoignable";
            this.pnlIsJoignable.Subtitle = "Le patient a-t-il répondu ?";
            this.pnlIsJoignable.Title = "Patient joignable ?";
            this.pnlIsJoignable.OnNext += new System.ComponentModel.CancelEventHandler(this.pnlIsJoignable_OnNext);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(111, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(279, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "La personne etait-elle joignable ?";
            // 
            // rbOui
            // 
            this.rbOui.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbOui.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbOui.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.rbOui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbOui.Location = new System.Drawing.Point(163, 164);
            this.rbOui.Name = "rbOui";
            this.rbOui.Size = new System.Drawing.Size(93, 58);
            this.rbOui.TabIndex = 1;
            this.rbOui.Text = "OUI";
            this.rbOui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbOui.UseVisualStyleBackColor = true;
            this.rbOui.CheckedChanged += new System.EventHandler(this.rbOui_CheckedChanged);
            // 
            // rbnon
            // 
            this.rbnon.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbnon.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbnon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.rbnon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnon.Location = new System.Drawing.Point(312, 164);
            this.rbnon.Name = "rbnon";
            this.rbnon.Size = new System.Drawing.Size(93, 58);
            this.rbnon.TabIndex = 0;
            this.rbnon.Text = "NON";
            this.rbnon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbnon.UseVisualStyleBackColor = true;
            this.rbnon.CheckedChanged += new System.EventHandler(this.rbOui_CheckedChanged);
            // 
            // pnlPoseRDV
            // 
            this.pnlPoseRDV.BindingImage = ((System.Drawing.Image)(resources.GetObject("pnlPoseRDV.BindingImage")));
            this.pnlPoseRDV.Controls.Add(this.btnOpenRHBase);
            this.pnlPoseRDV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlPoseRDV.Name = "pnlPoseRDV";
            this.pnlPoseRDV.Subtitle = "Pose d\'un prochain RDV";
            this.pnlPoseRDV.Title = "Pose d\'un Rendez-Vous";
            this.pnlPoseRDV.OnShow += new System.EventHandler(this.pnlPoseRDV_OnShow);
            // 
            // btnOpenRHBase
            // 
            this.btnOpenRHBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenRHBase.Location = new System.Drawing.Point(201, 178);
            this.btnOpenRHBase.Name = "btnOpenRHBase";
            this.btnOpenRHBase.Size = new System.Drawing.Size(148, 39);
            this.btnOpenRHBase.TabIndex = 0;
            this.btnOpenRHBase.Text = "Ouvrir RHBase";
            this.btnOpenRHBase.UseVisualStyleBackColor = true;
            this.btnOpenRHBase.Click += new System.EventHandler(this.btnOpenRHBase_Click);
            // 
            // pnlAutreContact
            // 
            this.pnlAutreContact.BindingImage = ((System.Drawing.Image)(resources.GetObject("pnlAutreContact.BindingImage")));
            this.pnlAutreContact.Controls.Add(this.rbDate);
            this.pnlAutreContact.Controls.Add(this.rb30);
            this.pnlAutreContact.Controls.Add(this.rb15);
            this.pnlAutreContact.Controls.Add(this.label4);
            this.pnlAutreContact.Controls.Add(this.dtpDateRecontact);
            this.pnlAutreContact.Controls.Add(this.label1);
            this.pnlAutreContact.Controls.Add(this.cbxMotifRecontact);
            this.pnlAutreContact.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlAutreContact.Name = "pnlAutreContact";
            this.pnlAutreContact.Subtitle = "Planification d\'une nouvelle prise de contact ";
            this.pnlAutreContact.Title = "";
            this.pnlAutreContact.OnNext += new System.ComponentModel.CancelEventHandler(this.pnlAutreContact_OnNext);
            this.pnlAutreContact.Click += new System.EventHandler(this.pnlAutreContact_Click);
            // 
            // rbDate
            // 
            this.rbDate.AutoSize = true;
            this.rbDate.Location = new System.Drawing.Point(162, 289);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(14, 13);
            this.rbDate.TabIndex = 63;
            this.rbDate.UseVisualStyleBackColor = true;
            // 
            // rb30
            // 
            this.rb30.AutoSize = true;
            this.rb30.Checked = true;
            this.rb30.Location = new System.Drawing.Point(162, 256);
            this.rb30.Name = "rb30";
            this.rb30.Size = new System.Drawing.Size(90, 17);
            this.rb30.TabIndex = 62;
            this.rb30.TabStop = true;
            this.rb30.Text = "Dans 30 jours";
            this.rb30.UseVisualStyleBackColor = true;
            // 
            // rb15
            // 
            this.rb15.AutoSize = true;
            this.rb15.Location = new System.Drawing.Point(162, 223);
            this.rb15.Name = "rb15";
            this.rb15.Size = new System.Drawing.Size(90, 17);
            this.rb15.TabIndex = 61;
            this.rb15.Text = "Dans 15 jours";
            this.rb15.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "A recontacter :";
            // 
            // dtpDateRecontact
            // 
            this.dtpDateRecontact.Location = new System.Drawing.Point(182, 285);
            this.dtpDateRecontact.Name = "dtpDateRecontact";
            this.dtpDateRecontact.Size = new System.Drawing.Size(236, 20);
            this.dtpDateRecontact.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Motif : ";
            // 
            // cbxMotifRecontact
            // 
            this.cbxMotifRecontact.FormattingEnabled = true;
            this.cbxMotifRecontact.Location = new System.Drawing.Point(182, 133);
            this.cbxMotifRecontact.Name = "cbxMotifRecontact";
            this.cbxMotifRecontact.Size = new System.Drawing.Size(236, 21);
            this.cbxMotifRecontact.TabIndex = 59;
            // 
            // finishStep1
            // 
            this.finishStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BindingImage")));
            this.finishStep1.Name = "finishStep1";
            // 
            // FrmRecontactMgmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(557, 405);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmRecontactMgmt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recontact";
            this.Load += new System.EventHandler(this.FrmRecontactMgmt_Load);
            this.startStep1.ResumeLayout(false);
            this.pnlPriseContact.ResumeLayout(false);
            this.pnlIsJoignable.ResumeLayout(false);
            this.pnlIsJoignable.PerformLayout();
            this.pnlPoseRDV.ResumeLayout(false);
            this.pnlAutreContact.ResumeLayout(false);
            this.pnlAutreContact.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvContact;
        private System.Windows.Forms.ColumnHeader colLibe;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbxMotifRecontact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateRecontact;
        private WizardBase.WizardControl wizardControl1;
        private WizardBase.IntermediateStep pnlPriseContact;
        private WizardBase.StartStep startStep1;
        private WizardBase.FinishStep finishStep1;
        private WizardBase.IntermediateStep pnlAutreContact;
        private WizardBase.IntermediateStep pnlIsJoignable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbOui;
        private System.Windows.Forms.RadioButton rbnon;
        private System.Windows.Forms.RadioButton rb15;
        private WizardBase.IntermediateStep pnlPoseRDV;
        private System.Windows.Forms.Button btnOpenRHBase;
        private System.Windows.Forms.RadioButton rbDate;
        private System.Windows.Forms.RadioButton rb30;
        private BaseCommonControls.SlidingList cbxutilisateur;
    }
}