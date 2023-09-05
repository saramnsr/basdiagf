namespace BaseCommonControls
{
    partial class FrmEditTempEcheanceDef
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
            this.txtbxMontantEcheance = new System.Windows.Forms.TextBox();
            this.dtpdateEcheance = new System.Windows.Forms.DateTimePicker();
            this.txtbxLibelle = new System.Windows.Forms.TextBox();
            this.chkbxParPrelevement = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkbxVirement = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtbxMontantEcheance
            // 
            this.txtbxMontantEcheance.Location = new System.Drawing.Point(220, 65);
            this.txtbxMontantEcheance.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxMontantEcheance.Name = "txtbxMontantEcheance";
            this.txtbxMontantEcheance.Size = new System.Drawing.Size(320, 25);
            this.txtbxMontantEcheance.TabIndex = 0;
            // 
            // dtpdateEcheance
            // 
            this.dtpdateEcheance.Location = new System.Drawing.Point(220, 115);
            this.dtpdateEcheance.Margin = new System.Windows.Forms.Padding(4);
            this.dtpdateEcheance.Name = "dtpdateEcheance";
            this.dtpdateEcheance.Size = new System.Drawing.Size(320, 25);
            this.dtpdateEcheance.TabIndex = 1;
            // 
            // txtbxLibelle
            // 
            this.txtbxLibelle.Location = new System.Drawing.Point(220, 17);
            this.txtbxLibelle.Margin = new System.Windows.Forms.Padding(4);
            this.txtbxLibelle.Name = "txtbxLibelle";
            this.txtbxLibelle.Size = new System.Drawing.Size(492, 25);
            this.txtbxLibelle.TabIndex = 2;
            // 
            // chkbxParPrelevement
            // 
            this.chkbxParPrelevement.AutoSize = true;
            this.chkbxParPrelevement.Location = new System.Drawing.Point(220, 165);
            this.chkbxParPrelevement.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxParPrelevement.Name = "chkbxParPrelevement";
            this.chkbxParPrelevement.Size = new System.Drawing.Size(130, 22);
            this.chkbxParPrelevement.TabIndex = 3;
            this.chkbxParPrelevement.Text = "Par prelevement";
            this.chkbxParPrelevement.UseVisualStyleBackColor = true;
            this.chkbxParPrelevement.CheckedChanged += new System.EventHandler(this.chkbxParPrelevement_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(510, 172);
            this.btnOk.Margin = new System.Windows.Forms.Padding(6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(97, 72);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(619, 172);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 72);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Libelle : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Montant : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Date d\'echeance : ";
            // 
            // chkbxVirement
            // 
            this.chkbxVirement.AutoSize = true;
            this.chkbxVirement.Location = new System.Drawing.Point(220, 195);
            this.chkbxVirement.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxVirement.Name = "chkbxVirement";
            this.chkbxVirement.Size = new System.Drawing.Size(108, 22);
            this.chkbxVirement.TabIndex = 9;
            this.chkbxVirement.Text = "Par virement";
            this.chkbxVirement.UseVisualStyleBackColor = true;
            this.chkbxVirement.CheckedChanged += new System.EventHandler(this.chkbxVirement_CheckedChanged);
            // 
            // FrmEditTempEcheanceDef
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(736, 261);
            this.Controls.Add(this.chkbxVirement);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkbxParPrelevement);
            this.Controls.Add(this.txtbxLibelle);
            this.Controls.Add(this.dtpdateEcheance);
            this.Controls.Add(this.txtbxMontantEcheance);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmEditTempEcheanceDef";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Echeance";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmEditTempEcheanceDef_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxMontantEcheance;
        private System.Windows.Forms.DateTimePicker dtpdateEcheance;
        private System.Windows.Forms.TextBox txtbxLibelle;
        private System.Windows.Forms.CheckBox chkbxParPrelevement;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkbxVirement;
    }
}