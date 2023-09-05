namespace BASEDiagAdulte
{
    partial class FrmFindCorrespondant
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
            this.txtbxNom = new System.Windows.Forms.TextBox();
            this.lblNomCorrespondant = new System.Windows.Forms.Label();
            this.lstBxCorrespondants = new System.Windows.Forms.ListBox();
            this.SuggestBxCorrespondant = new BASEDiagAdulte.Ctrls.SuggestBox();
            this.btnAddCorrespondant = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbxNom
            // 
            this.txtbxNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxNom.Location = new System.Drawing.Point(38, 62);
            this.txtbxNom.Name = "txtbxNom";
            this.txtbxNom.Size = new System.Drawing.Size(483, 20);
            this.txtbxNom.TabIndex = 0;
            this.txtbxNom.TextChanged += new System.EventHandler(this.txtbxNom_TextChanged);
            this.txtbxNom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // lblNomCorrespondant
            // 
            this.lblNomCorrespondant.AutoSize = true;
            this.lblNomCorrespondant.Location = new System.Drawing.Point(12, 46);
            this.lblNomCorrespondant.Name = "lblNomCorrespondant";
            this.lblNomCorrespondant.Size = new System.Drawing.Size(82, 13);
            this.lblNomCorrespondant.TabIndex = 3;
            this.lblNomCorrespondant.Text = "Correspondant :";
            // 
            // lstBxCorrespondants
            // 
            this.lstBxCorrespondants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBxCorrespondants.FormattingEnabled = true;
            this.lstBxCorrespondants.Location = new System.Drawing.Point(38, 88);
            this.lstBxCorrespondants.Name = "lstBxCorrespondants";
            this.lstBxCorrespondants.Size = new System.Drawing.Size(564, 368);
            this.lstBxCorrespondants.TabIndex = 4;
            this.lstBxCorrespondants.Click += new System.EventHandler(this.lstBxCorrespondants_Click);
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
            this.SuggestBxCorrespondant.Size = new System.Drawing.Size(625, 31);
            this.SuggestBxCorrespondant.SuggestionList = null;
            this.SuggestBxCorrespondant.TabIndex = 2;
            this.SuggestBxCorrespondant.OnFound += new System.EventHandler(this.SuggestBxCorrespondant_OnFound);
            this.SuggestBxCorrespondant.OnYesClick += new System.EventHandler(this.SuggestBxCorrespondant_OnYesClick);
            // 
            // btnAddCorrespondant
            // 
            this.btnAddCorrespondant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCorrespondant.Location = new System.Drawing.Point(527, 60);
            this.btnAddCorrespondant.Name = "btnAddCorrespondant";
            this.btnAddCorrespondant.Size = new System.Drawing.Size(75, 23);
            this.btnAddCorrespondant.TabIndex = 5;
            this.btnAddCorrespondant.Text = "Nouveau";
            this.btnAddCorrespondant.UseVisualStyleBackColor = true;
            this.btnAddCorrespondant.Click += new System.EventHandler(this.btnAddCorrespondant_Click);
            // 
            // FrmFindCorrespondant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 487);
            this.Controls.Add(this.btnAddCorrespondant);
            this.Controls.Add(this.lstBxCorrespondants);
            this.Controls.Add(this.lblNomCorrespondant);
            this.Controls.Add(this.SuggestBxCorrespondant);
            this.Controls.Add(this.txtbxNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmFindCorrespondant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Correspondant";
            this.Load += new System.EventHandler(this.FrmCorrespondant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxNom;
        private BASEDiagAdulte.Ctrls.SuggestBox SuggestBxCorrespondant;
        private System.Windows.Forms.Label lblNomCorrespondant;
        private System.Windows.Forms.ListBox lstBxCorrespondants;
        private System.Windows.Forms.Button btnAddCorrespondant;
    }
}