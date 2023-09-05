namespace BaseCommonControls
{
    partial class FrmActeTraitement
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
            this.treeviewActes = new BaseCommonControls.Ctrls.TreeViewIcon();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeviewActes
            // 
            this.treeviewActes.AllowDrop = true;
            this.treeviewActes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeviewActes.BackColor = System.Drawing.Color.White;
            this.treeviewActes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeviewActes.ButtonHeight = 75;
            this.treeviewActes.ButtonWidth = 75;
            this.treeviewActes.ChoixFamille = "";
            treeNode1.Name = "";
            treeNode1.Text = "";
            this.treeviewActes.CurrentNode = treeNode1;
            this.treeviewActes.isCreated = false;
            this.treeviewActes.Location = new System.Drawing.Point(16, 12);
            this.treeviewActes.MultiChoiceVisibilite = true;
            this.treeviewActes.Name = "treeviewActes";
            this.treeviewActes.SelectedNode = null;
            this.treeviewActes.Size = new System.Drawing.Size(557, 493);
            this.treeviewActes.TabIndex = 18;
            this.treeviewActes.totaleActeSupp = 0D;
            this.treeviewActes.OnSelected += new System.EventHandler(this.treeviewActes_OnSelected);
            this.treeviewActes.OnRemove += new System.EventHandler(this.treeviewActes_Remove);
            this.treeviewActes.Load += new System.EventHandler(this.treeviewActes_Load);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(498, 511);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 58);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(417, 511);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 58);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 21;
            // 
            // FrmActeTraitement
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(585, 581);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.treeviewActes);
            this.Name = "FrmActeTraitement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Acte";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmActeTraitement_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmActeTraitement_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BaseCommonControls.Ctrls.TreeViewIcon treeviewActes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
    }
}