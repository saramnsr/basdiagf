namespace BaseCommonControls
{
    partial class FrmChoiceMateriel
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("");
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.treeviewMateriels = new BaseCommonControls.Ctrls.TreeViewIcon();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(764, 644);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 58);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(845, 644);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 58);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(545, 653);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 23;
            // 
            // treeviewMateriels
            // 
            this.treeviewMateriels.AllowDrop = true;
            this.treeviewMateriels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeviewMateriels.BackColor = System.Drawing.Color.White;
            this.treeviewMateriels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeviewMateriels.ButtonHeight = 75;
            this.treeviewMateriels.ButtonWidth = 75;
            this.treeviewMateriels.ChoixFamille = "";
            treeNode1.Name = "";
            treeNode1.Text = "";
            this.treeviewMateriels.CurrentNode = treeNode1;
            this.treeviewMateriels.Location = new System.Drawing.Point(10, 12);
            this.treeviewMateriels.MultiChoiceVisibilite = true;
            this.treeviewMateriels.Name = "treeviewMateriels";
            this.treeviewMateriels.SelectedNode = null;
            this.treeviewMateriels.Size = new System.Drawing.Size(911, 626);
            this.treeviewMateriels.TabIndex = 19;
            this.treeviewMateriels.OnSelected += new System.EventHandler(this.treeviewMateriels_select);
            this.treeviewMateriels.Load += new System.EventHandler(this.treeviewMateriels_Load);
            // 
            // FrmChoiceMateriel
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(930, 714);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.treeviewMateriels);
            this.Name = "FrmChoiceMateriel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Materiel";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmChoiceMateriel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BaseCommonControls.Ctrls.TreeViewIcon treeviewMateriels;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}