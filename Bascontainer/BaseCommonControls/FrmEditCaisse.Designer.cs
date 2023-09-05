namespace BaseCommonControls
{
    partial class FrmEditCaisse
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtbxNomMut = new System.Windows.Forms.TextBox();
            this.txtbxNumtel = new System.Windows.Forms.TextBox();
            this.txtbxAdress1 = new System.Windows.Forms.TextBox();
            this.txtbxAdress2 = new System.Windows.Forms.TextBox();
            this.txtbxCodePostal = new System.Windows.Forms.TextBox();
            this.txtbxVille = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkbxNeedOrder = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(289, 272);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(109, 41);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(402, 272);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 41);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtbxNomMut
            // 
            this.txtbxNomMut.Location = new System.Drawing.Point(152, 8);
            this.txtbxNomMut.Name = "txtbxNomMut";
            this.txtbxNomMut.Size = new System.Drawing.Size(325, 24);
            this.txtbxNomMut.TabIndex = 5;
            // 
            // txtbxNumtel
            // 
            this.txtbxNumtel.Location = new System.Drawing.Point(152, 38);
            this.txtbxNumtel.Name = "txtbxNumtel";
            this.txtbxNumtel.Size = new System.Drawing.Size(325, 24);
            this.txtbxNumtel.TabIndex = 6;
            // 
            // txtbxAdress1
            // 
            this.txtbxAdress1.Location = new System.Drawing.Point(152, 96);
            this.txtbxAdress1.Name = "txtbxAdress1";
            this.txtbxAdress1.Size = new System.Drawing.Size(325, 24);
            this.txtbxAdress1.TabIndex = 7;
            // 
            // txtbxAdress2
            // 
            this.txtbxAdress2.Location = new System.Drawing.Point(152, 126);
            this.txtbxAdress2.Name = "txtbxAdress2";
            this.txtbxAdress2.Size = new System.Drawing.Size(325, 24);
            this.txtbxAdress2.TabIndex = 8;
            // 
            // txtbxCodePostal
            // 
            this.txtbxCodePostal.Location = new System.Drawing.Point(152, 156);
            this.txtbxCodePostal.Name = "txtbxCodePostal";
            this.txtbxCodePostal.Size = new System.Drawing.Size(97, 24);
            this.txtbxCodePostal.TabIndex = 9;
            // 
            // txtbxVille
            // 
            this.txtbxVille.Location = new System.Drawing.Point(255, 156);
            this.txtbxVille.Name = "txtbxVille";
            this.txtbxVille.Size = new System.Drawing.Size(222, 24);
            this.txtbxVille.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Adresse : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Nom : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tel : ";
            // 
            // chkbxNeedOrder
            // 
            this.chkbxNeedOrder.AutoSize = true;
            this.chkbxNeedOrder.Location = new System.Drawing.Point(152, 228);
            this.chkbxNeedOrder.Name = "chkbxNeedOrder";
            this.chkbxNeedOrder.Size = new System.Drawing.Size(116, 21);
            this.chkbxNeedOrder.TabIndex = 22;
            this.chkbxNeedOrder.Text = "Necessite ordre";
            this.chkbxNeedOrder.UseVisualStyleBackColor = true;
            // 
            // FrmEditCaisse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(527, 329);
            this.Controls.Add(this.chkbxNeedOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbxVille);
            this.Controls.Add(this.txtbxCodePostal);
            this.Controls.Add(this.txtbxAdress2);
            this.Controls.Add(this.txtbxAdress1);
            this.Controls.Add(this.txtbxNumtel);
            this.Controls.Add(this.txtbxNomMut);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmEditCaisse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edition Caisse";
            this.Load += new System.EventHandler(this.FrmEditMutuelle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtbxNomMut;
        private System.Windows.Forms.TextBox txtbxNumtel;
        private System.Windows.Forms.TextBox txtbxAdress1;
        private System.Windows.Forms.TextBox txtbxAdress2;
        private System.Windows.Forms.TextBox txtbxCodePostal;
        private System.Windows.Forms.TextBox txtbxVille;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkbxNeedOrder;
    }
}