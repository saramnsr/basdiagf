namespace BASEDiag
{
    partial class FrmAddPoseAppareillage
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
            this.pnlChoixApp = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lstbxAppareil = new System.Windows.Forms.ListBox();
            this.pnlDetailsAppareil = new System.Windows.Forms.Panel();
            this.btnBaseLabo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlSemestre = new System.Windows.Forms.Panel();
            this.chkbxSem8 = new System.Windows.Forms.CheckBox();
            this.chkbxSem7 = new System.Windows.Forms.CheckBox();
            this.chkbxSem6 = new System.Windows.Forms.CheckBox();
            this.chkbxSem5 = new System.Windows.Forms.CheckBox();
            this.chkbxSem4 = new System.Windows.Forms.CheckBox();
            this.chkbxSem3 = new System.Windows.Forms.CheckBox();
            this.chkbxSem2 = new System.Windows.Forms.CheckBox();
            this.chkbxSem1 = new System.Windows.Forms.CheckBox();
            this.pnlPlanification = new System.Windows.Forms.Panel();
            this.pnlContainer.SuspendLayout();
            this.pnlChoixApp.SuspendLayout();
            this.pnlDetailsAppareil.SuspendLayout();
            this.pnlSemestre.SuspendLayout();
            this.pnlPlanification.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.Controls.Add(this.pnlChoixApp);
            this.pnlContainer.Controls.Add(this.pnlDetailsAppareil);
            this.pnlContainer.Controls.Add(this.pnlPlanification);
            this.pnlContainer.Location = new System.Drawing.Point(12, 12);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(638, 289);
            this.pnlContainer.TabIndex = 0;
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
            // 
            // pnlDetailsAppareil
            // 
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
            // pnlPlanification
            // 
            this.pnlPlanification.Controls.Add(this.pnlSemestre);
            this.pnlPlanification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlanification.Location = new System.Drawing.Point(0, 0);
            this.pnlPlanification.Name = "pnlPlanification";
            this.pnlPlanification.Size = new System.Drawing.Size(638, 289);
            this.pnlPlanification.TabIndex = 3;
            this.pnlPlanification.Visible = false;
            // 
            // FrmAddPoseAppareillage
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
            this.Name = "FrmAddPoseAppareillage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traitement";
            this.Load += new System.EventHandler(this.FrmAddTraitmnt_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlChoixApp.ResumeLayout(false);
            this.pnlChoixApp.PerformLayout();
            this.pnlDetailsAppareil.ResumeLayout(false);
            this.pnlSemestre.ResumeLayout(false);
            this.pnlSemestre.PerformLayout();
            this.pnlPlanification.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlChoixApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstbxAppareil;
        private System.Windows.Forms.Panel pnlDetailsAppareil;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBaseLabo;
        private System.Windows.Forms.Panel pnlPlanification;
        private System.Windows.Forms.Panel pnlSemestre;
        private System.Windows.Forms.CheckBox chkbxSem1;
        private System.Windows.Forms.CheckBox chkbxSem2;
        private System.Windows.Forms.CheckBox chkbxSem3;
        private System.Windows.Forms.CheckBox chkbxSem4;
        private System.Windows.Forms.CheckBox chkbxSem5;
        private System.Windows.Forms.CheckBox chkbxSem6;
        private System.Windows.Forms.CheckBox chkbxSem7;
        private System.Windows.Forms.CheckBox chkbxSem8;
    }
}