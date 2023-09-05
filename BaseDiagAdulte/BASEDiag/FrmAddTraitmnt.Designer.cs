namespace BASEDiag
{
    partial class FrmAddTraitmnt
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlGestion = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxTarifReel = new System.Windows.Forms.TextBox();
            this.lblTarif = new System.Windows.Forms.Label();
            this.lblDescriptifGestion = new System.Windows.Forms.Label();
            this.cbxActePlanGestion = new System.Windows.Forms.ComboBox();
            this.lblActeGestion = new System.Windows.Forms.Label();
            this.pnlTypeTraitement = new System.Windows.Forms.Panel();
            this.chkbxOrthodontique = new System.Windows.Forms.RadioButton();
            this.chkbxOrthopedie = new System.Windows.Forms.RadioButton();
            this.chkBxPediatrie = new System.Windows.Forms.RadioButton();
            this.pnlChoixApp = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lstbxAppareil = new System.Windows.Forms.ListBox();
            this.pnlDetailsAppareil = new System.Windows.Forms.Panel();
            this.btnBaseLabo = new System.Windows.Forms.Button();
            this.pnlPlanification = new System.Windows.Forms.Panel();
            this.pnlSemestre = new System.Windows.Forms.Panel();
            this.chkbxSem1 = new System.Windows.Forms.CheckBox();
            this.chkbxSem2 = new System.Windows.Forms.CheckBox();
            this.chkbxSem3 = new System.Windows.Forms.CheckBox();
            this.chkbxSem4 = new System.Windows.Forms.CheckBox();
            this.chkbxSem5 = new System.Windows.Forms.CheckBox();
            this.chkbxSem6 = new System.Windows.Forms.CheckBox();
            this.chkbxSem7 = new System.Windows.Forms.CheckBox();
            this.chkbxSem8 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.pnlGestion.SuspendLayout();
            this.pnlTypeTraitement.SuspendLayout();
            this.pnlChoixApp.SuspendLayout();
            this.pnlDetailsAppareil.SuspendLayout();
            this.pnlPlanification.SuspendLayout();
            this.pnlSemestre.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.Controls.Add(this.pnlGestion);
            this.pnlContainer.Controls.Add(this.pnlTypeTraitement);
            this.pnlContainer.Controls.Add(this.pnlChoixApp);
            this.pnlContainer.Controls.Add(this.pnlDetailsAppareil);
            this.pnlContainer.Location = new System.Drawing.Point(12, 12);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(638, 289);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlGestion
            // 
            this.pnlGestion.Controls.Add(this.label3);
            this.pnlGestion.Controls.Add(this.txtbxTarifReel);
            this.pnlGestion.Controls.Add(this.lblTarif);
            this.pnlGestion.Controls.Add(this.lblDescriptifGestion);
            this.pnlGestion.Controls.Add(this.cbxActePlanGestion);
            this.pnlGestion.Controls.Add(this.lblActeGestion);
            this.pnlGestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGestion.Location = new System.Drawing.Point(0, 0);
            this.pnlGestion.Name = "pnlGestion";
            this.pnlGestion.Size = new System.Drawing.Size(638, 289);
            this.pnlGestion.TabIndex = 3;
            this.pnlGestion.Visible = false;
            this.pnlGestion.VisibleChanged += new System.EventHandler(this.pnlTarifs_VisibleChanged);
            this.pnlGestion.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGestion_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "€";
            // 
            // txtbxTarifReel
            // 
            this.txtbxTarifReel.AcceptsReturn = true;
            this.txtbxTarifReel.Location = new System.Drawing.Point(254, 200);
            this.txtbxTarifReel.Name = "txtbxTarifReel";
            this.txtbxTarifReel.Size = new System.Drawing.Size(111, 20);
            this.txtbxTarifReel.TabIndex = 4;
            // 
            // lblTarif
            // 
            this.lblTarif.AutoSize = true;
            this.lblTarif.Location = new System.Drawing.Point(151, 203);
            this.lblTarif.Name = "lblTarif";
            this.lblTarif.Size = new System.Drawing.Size(97, 13);
            this.lblTarif.TabIndex = 3;
            this.lblTarif.Text = "Tarif du semestre : ";
            // 
            // lblDescriptifGestion
            // 
            this.lblDescriptifGestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDescriptifGestion.Location = new System.Drawing.Point(154, 59);
            this.lblDescriptifGestion.Name = "lblDescriptifGestion";
            this.lblDescriptifGestion.Size = new System.Drawing.Size(366, 121);
            this.lblDescriptifGestion.TabIndex = 2;
            this.lblDescriptifGestion.Text = "label3";
            this.lblDescriptifGestion.Click += new System.EventHandler(this.lblDescriptifGestion_Click);
            // 
            // cbxActePlanGestion
            // 
            this.cbxActePlanGestion.FormattingEnabled = true;
            this.cbxActePlanGestion.Location = new System.Drawing.Point(154, 33);
            this.cbxActePlanGestion.Name = "cbxActePlanGestion";
            this.cbxActePlanGestion.Size = new System.Drawing.Size(366, 21);
            this.cbxActePlanGestion.TabIndex = 1;
            this.cbxActePlanGestion.SelectedIndexChanged += new System.EventHandler(this.cbxActePlanGestion_SelectedIndexChanged);
            // 
            // lblActeGestion
            // 
            this.lblActeGestion.AutoSize = true;
            this.lblActeGestion.Location = new System.Drawing.Point(3, 10);
            this.lblActeGestion.Name = "lblActeGestion";
            this.lblActeGestion.Size = new System.Drawing.Size(225, 13);
            this.lblActeGestion.TabIndex = 0;
            this.lblActeGestion.Text = "Acte de gestion correspondant au traitement : ";
            // 
            // pnlTypeTraitement
            // 
            this.pnlTypeTraitement.Controls.Add(this.chkbxOrthodontique);
            this.pnlTypeTraitement.Controls.Add(this.chkbxOrthopedie);
            this.pnlTypeTraitement.Controls.Add(this.chkBxPediatrie);
            this.pnlTypeTraitement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTypeTraitement.Location = new System.Drawing.Point(0, 0);
            this.pnlTypeTraitement.Name = "pnlTypeTraitement";
            this.pnlTypeTraitement.Size = new System.Drawing.Size(638, 289);
            this.pnlTypeTraitement.TabIndex = 6;
            this.pnlTypeTraitement.Visible = false;
            this.pnlTypeTraitement.VisibleChanged += new System.EventHandler(this.pnlTypeTraitement_VisibleChanged);
            // 
            // chkbxOrthodontique
            // 
            this.chkbxOrthodontique.AutoSize = true;
            this.chkbxOrthodontique.Location = new System.Drawing.Point(281, 135);
            this.chkbxOrthodontique.Name = "chkbxOrthodontique";
            this.chkbxOrthodontique.Size = new System.Drawing.Size(92, 17);
            this.chkbxOrthodontique.TabIndex = 2;
            this.chkbxOrthodontique.Text = "Orthodontique";
            this.chkbxOrthodontique.UseVisualStyleBackColor = true;
            this.chkbxOrthodontique.Click += new System.EventHandler(this.chkbxOrthodontique_Click);
            this.chkbxOrthodontique.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxOrthodontique_MouseDown);
            // 
            // chkbxOrthopedie
            // 
            this.chkbxOrthopedie.AutoSize = true;
            this.chkbxOrthopedie.Location = new System.Drawing.Point(282, 106);
            this.chkbxOrthopedie.Name = "chkbxOrthopedie";
            this.chkbxOrthopedie.Size = new System.Drawing.Size(89, 17);
            this.chkbxOrthopedie.TabIndex = 1;
            this.chkbxOrthopedie.Text = "Orthopedique";
            this.chkbxOrthopedie.UseVisualStyleBackColor = true;
            this.chkbxOrthopedie.Click += new System.EventHandler(this.chkbxOrthopedie_Click);
            this.chkbxOrthopedie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkbxOrthopedie_MouseDown);
            this.chkbxOrthopedie.CheckedChanged += new System.EventHandler(this.chkbxOrthopedie_CheckedChanged);
            // 
            // chkBxPediatrie
            // 
            this.chkBxPediatrie.AutoSize = true;
            this.chkBxPediatrie.Location = new System.Drawing.Point(282, 76);
            this.chkBxPediatrie.Name = "chkBxPediatrie";
            this.chkBxPediatrie.Size = new System.Drawing.Size(78, 17);
            this.chkBxPediatrie.TabIndex = 0;
            this.chkBxPediatrie.Text = "Pédiatrique";
            this.chkBxPediatrie.UseVisualStyleBackColor = true;
            this.chkBxPediatrie.Click += new System.EventHandler(this.chkBxPediatrie_Click);
            this.chkBxPediatrie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkBxPediatrie_MouseDown);
            // 
            // pnlChoixApp
            // 
            this.pnlChoixApp.Controls.Add(this.label1);
            this.pnlChoixApp.Controls.Add(this.lstbxAppareil);
            this.pnlChoixApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChoixApp.Location = new System.Drawing.Point(0, 0);
            this.pnlChoixApp.Name = "pnlChoixApp";
            this.pnlChoixApp.Size = new System.Drawing.Size(638, 289);
            this.pnlChoixApp.TabIndex = 0;
            this.pnlChoixApp.Visible = false;
            this.pnlChoixApp.VisibleChanged += new System.EventHandler(this.pnlChoixApp_VisibleChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choix de l\'appareil : ";
            // 
            // lstbxAppareil
            // 
            this.lstbxAppareil.FormattingEnabled = true;
            this.lstbxAppareil.Location = new System.Drawing.Point(185, 49);
            this.lstbxAppareil.Name = "lstbxAppareil";
            this.lstbxAppareil.Size = new System.Drawing.Size(308, 199);
            this.lstbxAppareil.TabIndex = 0;
            this.lstbxAppareil.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstbxAppareil_MouseDoubleClick);
            this.lstbxAppareil.SelectedIndexChanged += new System.EventHandler(this.lstbxAppareil_SelectedIndexChanged);
            // 
            // pnlDetailsAppareil
            // 
            this.pnlDetailsAppareil.Controls.Add(this.pnlPlanification);
            this.pnlDetailsAppareil.Controls.Add(this.btnBaseLabo);
            this.pnlDetailsAppareil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailsAppareil.Location = new System.Drawing.Point(0, 0);
            this.pnlDetailsAppareil.Name = "pnlDetailsAppareil";
            this.pnlDetailsAppareil.Size = new System.Drawing.Size(638, 289);
            this.pnlDetailsAppareil.TabIndex = 2;
            this.pnlDetailsAppareil.Visible = false;
            this.pnlDetailsAppareil.VisibleChanged += new System.EventHandler(this.pnlDetailsAppareil_VisibleChanged);
            // 
            // btnBaseLabo
            // 
            this.btnBaseLabo.Location = new System.Drawing.Point(239, 113);
            this.btnBaseLabo.Name = "btnBaseLabo";
            this.btnBaseLabo.Size = new System.Drawing.Size(134, 46);
            this.btnBaseLabo.TabIndex = 0;
            this.btnBaseLabo.Text = "BAS Labo";
            this.btnBaseLabo.UseVisualStyleBackColor = true;
            this.btnBaseLabo.Click += new System.EventHandler(this.button3_Click);
            // 
            // pnlPlanification
            // 
            this.pnlPlanification.Controls.Add(this.pnlSemestre);
            this.pnlPlanification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlanification.Location = new System.Drawing.Point(0, 0);
            this.pnlPlanification.Name = "pnlPlanification";
            this.pnlPlanification.Size = new System.Drawing.Size(638, 289);
            this.pnlPlanification.TabIndex = 3;
            this.pnlPlanification.Visible = false;
            this.pnlPlanification.VisibleChanged += new System.EventHandler(this.pnlPlanification_VisibleChanged);
            // 
            // pnlSemestre
            // 
            this.pnlSemestre.Controls.Add(this.chkbxSem1);
            this.pnlSemestre.Controls.Add(this.chkbxSem2);
            this.pnlSemestre.Controls.Add(this.chkbxSem3);
            this.pnlSemestre.Controls.Add(this.chkbxSem4);
            this.pnlSemestre.Controls.Add(this.chkbxSem5);
            this.pnlSemestre.Controls.Add(this.chkbxSem6);
            this.pnlSemestre.Controls.Add(this.chkbxSem7);
            this.pnlSemestre.Controls.Add(this.chkbxSem8);
            this.pnlSemestre.Location = new System.Drawing.Point(15, 90);
            this.pnlSemestre.Name = "pnlSemestre";
            this.pnlSemestre.Size = new System.Drawing.Size(611, 33);
            this.pnlSemestre.TabIndex = 0;
            // 
            // chkbxSem1
            // 
            this.chkbxSem1.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem1.AutoSize = true;
            this.chkbxSem1.Location = new System.Drawing.Point(3, 3);
            this.chkbxSem1.Name = "chkbxSem1";
            this.chkbxSem1.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem1.TabIndex = 0;
            this.chkbxSem1.Text = "Semestre 1";
            this.chkbxSem1.UseVisualStyleBackColor = true;
            // 
            // chkbxSem2
            // 
            this.chkbxSem2.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem2.AutoSize = true;
            this.chkbxSem2.Location = new System.Drawing.Point(78, 3);
            this.chkbxSem2.Name = "chkbxSem2";
            this.chkbxSem2.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem2.TabIndex = 1;
            this.chkbxSem2.Text = "Semestre 2";
            this.chkbxSem2.UseVisualStyleBackColor = true;
            // 
            // chkbxSem3
            // 
            this.chkbxSem3.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem3.AutoSize = true;
            this.chkbxSem3.Location = new System.Drawing.Point(153, 3);
            this.chkbxSem3.Name = "chkbxSem3";
            this.chkbxSem3.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem3.TabIndex = 2;
            this.chkbxSem3.Text = "Semestre 3";
            this.chkbxSem3.UseVisualStyleBackColor = true;
            // 
            // chkbxSem4
            // 
            this.chkbxSem4.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem4.AutoSize = true;
            this.chkbxSem4.Location = new System.Drawing.Point(228, 3);
            this.chkbxSem4.Name = "chkbxSem4";
            this.chkbxSem4.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem4.TabIndex = 3;
            this.chkbxSem4.Text = "Semestre 4";
            this.chkbxSem4.UseVisualStyleBackColor = true;
            // 
            // chkbxSem5
            // 
            this.chkbxSem5.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem5.AutoSize = true;
            this.chkbxSem5.Location = new System.Drawing.Point(303, 3);
            this.chkbxSem5.Name = "chkbxSem5";
            this.chkbxSem5.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem5.TabIndex = 4;
            this.chkbxSem5.Text = "Semestre 5";
            this.chkbxSem5.UseVisualStyleBackColor = true;
            // 
            // chkbxSem6
            // 
            this.chkbxSem6.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem6.AutoSize = true;
            this.chkbxSem6.Location = new System.Drawing.Point(378, 3);
            this.chkbxSem6.Name = "chkbxSem6";
            this.chkbxSem6.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem6.TabIndex = 5;
            this.chkbxSem6.Text = "Semestre 6";
            this.chkbxSem6.UseVisualStyleBackColor = true;
            // 
            // chkbxSem7
            // 
            this.chkbxSem7.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem7.AutoSize = true;
            this.chkbxSem7.Location = new System.Drawing.Point(453, 3);
            this.chkbxSem7.Name = "chkbxSem7";
            this.chkbxSem7.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem7.TabIndex = 6;
            this.chkbxSem7.Text = "Semestre 7";
            this.chkbxSem7.UseVisualStyleBackColor = true;
            // 
            // chkbxSem8
            // 
            this.chkbxSem8.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbxSem8.AutoSize = true;
            this.chkbxSem8.Location = new System.Drawing.Point(528, 3);
            this.chkbxSem8.Name = "chkbxSem8";
            this.chkbxSem8.Size = new System.Drawing.Size(70, 23);
            this.chkbxSem8.TabIndex = 7;
            this.chkbxSem8.Text = "Semestre 8";
            this.chkbxSem8.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(575, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "Suivant";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(494, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 49);
            this.button2.TabIndex = 2;
            this.button2.Text = "Precedent";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 49);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAddTraitmnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(660, 368);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAddTraitmnt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traitement";
            this.Load += new System.EventHandler(this.FrmAddTraitmnt_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlGestion.ResumeLayout(false);
            this.pnlGestion.PerformLayout();
            this.pnlTypeTraitement.ResumeLayout(false);
            this.pnlTypeTraitement.PerformLayout();
            this.pnlChoixApp.ResumeLayout(false);
            this.pnlChoixApp.PerformLayout();
            this.pnlDetailsAppareil.ResumeLayout(false);
            this.pnlPlanification.ResumeLayout(false);
            this.pnlSemestre.ResumeLayout(false);
            this.pnlSemestre.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlChoixApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstbxAppareil;
        private System.Windows.Forms.Panel pnlPlanification;
        private System.Windows.Forms.Panel pnlDetailsAppareil;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlSemestre;
        private System.Windows.Forms.CheckBox chkbxSem8;
        private System.Windows.Forms.CheckBox chkbxSem7;
        private System.Windows.Forms.CheckBox chkbxSem6;
        private System.Windows.Forms.CheckBox chkbxSem5;
        private System.Windows.Forms.CheckBox chkbxSem4;
        private System.Windows.Forms.CheckBox chkbxSem3;
        private System.Windows.Forms.CheckBox chkbxSem2;
        private System.Windows.Forms.CheckBox chkbxSem1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlGestion;
        private System.Windows.Forms.ComboBox cbxActePlanGestion;
        private System.Windows.Forms.Label lblActeGestion;
        private System.Windows.Forms.Label lblTarif;
        private System.Windows.Forms.Label lblDescriptifGestion;
        private System.Windows.Forms.TextBox txtbxTarifReel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlTypeTraitement;
        private System.Windows.Forms.RadioButton chkbxOrthodontique;
        private System.Windows.Forms.RadioButton chkbxOrthopedie;
        private System.Windows.Forms.RadioButton chkBxPediatrie;
        private System.Windows.Forms.Button btnBaseLabo;
    }
}