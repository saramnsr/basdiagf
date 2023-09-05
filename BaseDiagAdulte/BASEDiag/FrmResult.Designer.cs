namespace BASEDiag
{
    partial class FrmResult
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
            this.btnOk = new System.Windows.Forms.Button();
            this.txtbxResume = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBxComptRenduCourrier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxResumeSecu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDevis = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxPlanTraitement = new System.Windows.Forms.TextBox();
            this.lstBxAppareils = new System.Windows.Forms.ListBox();
            this.lstBxPlanGestion = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBasLabo = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDevis = new ControlsLibrary.TreeViewIconCbx();
            this.cbxPratResp = new ControlsLibrary.TreeViewIconCbx();
            this.cbxAssResp = new ControlsLibrary.TreeViewIconCbx();
            this.btnAddObjTrmnt = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblAppDejaPoses = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(909, 688);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(126, 72);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Fermer";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtbxResume
            // 
            this.txtbxResume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtbxResume.Location = new System.Drawing.Point(14, 30);
            this.txtbxResume.Multiline = true;
            this.txtbxResume.Name = "txtbxResume";
            this.txtbxResume.ReadOnly = true;
            this.txtbxResume.Size = new System.Drawing.Size(298, 313);
            this.txtbxResume.TabIndex = 2;
            this.txtbxResume.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtbxResume_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Résumé";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 672);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 88);
            this.button1.TabIndex = 4;
            this.button1.Text = "Entente Préalable";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBxComptRenduCourrier
            // 
            this.txtBxComptRenduCourrier.AcceptsReturn = true;
            this.txtBxComptRenduCourrier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBxComptRenduCourrier.Location = new System.Drawing.Point(318, 58);
            this.txtBxComptRenduCourrier.Multiline = true;
            this.txtBxComptRenduCourrier.Name = "txtBxComptRenduCourrier";
            this.txtBxComptRenduCourrier.Size = new System.Drawing.Size(370, 454);
            this.txtBxComptRenduCourrier.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Compte rendu (pour les courriers)";
            // 
            // txtbxResumeSecu
            // 
            this.txtbxResumeSecu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtbxResumeSecu.Location = new System.Drawing.Point(14, 362);
            this.txtbxResumeSecu.Multiline = true;
            this.txtbxResumeSecu.Name = "txtbxResumeSecu";
            this.txtbxResumeSecu.ReadOnly = true;
            this.txtbxResumeSecu.Size = new System.Drawing.Size(298, 70);
            this.txtbxResumeSecu.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Plan de traitement Sécu";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(114, 672);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 88);
            this.button2.TabIndex = 9;
            this.button2.Text = "Courriers";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDevis
            // 
            this.btnDevis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDevis.Location = new System.Drawing.Point(216, 672);
            this.btnDevis.Name = "btnDevis";
            this.btnDevis.Size = new System.Drawing.Size(96, 88);
            this.btnDevis.TabIndex = 10;
            this.btnDevis.Text = "Devis";
            this.btnDevis.UseVisualStyleBackColor = true;
            this.btnDevis.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Objectif/Plan de traitement";
            // 
            // txtbxPlanTraitement
            // 
            this.txtbxPlanTraitement.AcceptsReturn = true;
            this.txtbxPlanTraitement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtbxPlanTraitement.Location = new System.Drawing.Point(14, 462);
            this.txtbxPlanTraitement.Multiline = true;
            this.txtbxPlanTraitement.Name = "txtbxPlanTraitement";
            this.txtbxPlanTraitement.Size = new System.Drawing.Size(298, 196);
            this.txtbxPlanTraitement.TabIndex = 11;
            // 
            // lstBxAppareils
            // 
            this.lstBxAppareils.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBxAppareils.FormattingEnabled = true;
            this.lstBxAppareils.Items.AddRange(new object[] {
            "A définir"});
            this.lstBxAppareils.Location = new System.Drawing.Point(697, 30);
            this.lstBxAppareils.Name = "lstBxAppareils";
            this.lstBxAppareils.Size = new System.Drawing.Size(330, 251);
            this.lstBxAppareils.TabIndex = 17;
            // 
            // lstBxPlanGestion
            // 
            this.lstBxPlanGestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBxPlanGestion.FormattingEnabled = true;
            this.lstBxPlanGestion.Items.AddRange(new object[] {
            "A définir"});
            this.lstBxPlanGestion.Location = new System.Drawing.Point(696, 393);
            this.lstBxPlanGestion.Name = "lstBxPlanGestion";
            this.lstBxPlanGestion.Size = new System.Drawing.Size(331, 121);
            this.lstBxPlanGestion.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(694, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Appareils proposés";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(694, 377);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Plans de gestion proposés";
            // 
            // btnBasLabo
            // 
            this.btnBasLabo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBasLabo.Location = new System.Drawing.Point(318, 672);
            this.btnBasLabo.Name = "btnBasLabo";
            this.btnBasLabo.Size = new System.Drawing.Size(96, 88);
            this.btnBasLabo.TabIndex = 21;
            this.btnBasLabo.Text = "BASLabo";
            this.btnBasLabo.UseVisualStyleBackColor = true;
            this.btnBasLabo.Click += new System.EventHandler(this.btnBasLabo_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Praticien responsable : ";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(369, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Assistante responsable : ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(101, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Devis proposé : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDevis);
            this.groupBox1.Controls.Add(this.cbxPratResp);
            this.groupBox1.Controls.Add(this.cbxAssResp);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(318, 518);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 141);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations complémentaires";
            // 
            // cbxDevis
            // 
            this.cbxDevis.Location = new System.Drawing.Point(191, 16);
            this.cbxDevis.Name = "cbxDevis";
            this.cbxDevis.SelectedIndex = -1;
            this.cbxDevis.SelectedItem = null;
            this.cbxDevis.Size = new System.Drawing.Size(485, 33);
            this.cbxDevis.TabIndex = 31;
            this.cbxDevis.VisibleItems = 5;
            // 
            // cbxPratResp
            // 
            this.cbxPratResp.Location = new System.Drawing.Point(191, 55);
            this.cbxPratResp.Name = "cbxPratResp";
            this.cbxPratResp.SelectedIndex = -1;
            this.cbxPratResp.SelectedItem = null;
            this.cbxPratResp.Size = new System.Drawing.Size(172, 33);
            this.cbxPratResp.TabIndex = 29;
            this.cbxPratResp.VisibleItems = 5;
            // 
            // cbxAssResp
            // 
            this.cbxAssResp.Location = new System.Drawing.Point(496, 55);
            this.cbxAssResp.Name = "cbxAssResp";
            this.cbxAssResp.SelectedIndex = -1;
            this.cbxAssResp.SelectedItem = null;
            this.cbxAssResp.Size = new System.Drawing.Size(180, 33);
            this.cbxAssResp.TabIndex = 28;
            this.cbxAssResp.VisibleItems = 5;
            // 
            // btnAddObjTrmnt
            // 
            this.btnAddObjTrmnt.Location = new System.Drawing.Point(151, 435);
            this.btnAddObjTrmnt.Name = "btnAddObjTrmnt";
            this.btnAddObjTrmnt.Size = new System.Drawing.Size(75, 21);
            this.btnAddObjTrmnt.TabIndex = 29;
            this.btnAddObjTrmnt.Text = "Ajouter";
            this.btnAddObjTrmnt.UseVisualStyleBackColor = true;
            this.btnAddObjTrmnt.Click += new System.EventHandler(this.btnAddObjTrmnt_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(420, 672);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 88);
            this.button3.TabIndex = 30;
            this.button3.Text = "Ouvrir tous les comptes rendus";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // lblAppDejaPoses
            // 
            this.lblAppDejaPoses.Location = new System.Drawing.Point(697, 284);
            this.lblAppDejaPoses.Name = "lblAppDejaPoses";
            this.lblAppDejaPoses.Size = new System.Drawing.Size(330, 75);
            this.lblAppDejaPoses.TabIndex = 31;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(522, 672);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 88);
            this.button4.TabIndex = 32;
            this.button4.Text = "Test LastSummary";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FrmResult
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 772);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblAppDejaPoses);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAddObjTrmnt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBasLabo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lstBxPlanGestion);
            this.Controls.Add(this.lstBxAppareils);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtbxPlanTraitement);
            this.Controls.Add(this.btnDevis);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbxResumeSecu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxComptRenduCourrier);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbxResume);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmResult";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Résumé";
            this.Load += new System.EventHandler(this.FrmResult_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtbxResume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBxComptRenduCourrier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxResumeSecu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDevis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbxPlanTraitement;
        private System.Windows.Forms.ListBox lstBxAppareils;
        private System.Windows.Forms.ListBox lstBxPlanGestion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBasLabo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddObjTrmnt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblAppDejaPoses;
        private ControlsLibrary.TreeViewIconCbx cbxAssResp;
        private ControlsLibrary.TreeViewIconCbx cbxPratResp;
        private ControlsLibrary.TreeViewIconCbx cbxDevis;
        private System.Windows.Forms.Button button4;
    }
}