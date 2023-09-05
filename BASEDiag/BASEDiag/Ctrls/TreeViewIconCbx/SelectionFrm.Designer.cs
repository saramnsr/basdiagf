namespace ControlsLibrary
{
    partial class SelectionFrm
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
            this.btnHaut = new System.Windows.Forms.Button();
            this.btnBas = new System.Windows.Forms.Button();
            this.lstBxChoices = new ControlsLibrary.NoScrollBarListBox();
            this.SuspendLayout();
            // 
            // btnHaut
            // 
            this.btnHaut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHaut.BackColor = System.Drawing.Color.Snow;
            this.btnHaut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHaut.Location = new System.Drawing.Point(0, 6);
            this.btnHaut.Name = "btnHaut";
            this.btnHaut.Size = new System.Drawing.Size(293, 23);
            this.btnHaut.TabIndex = 1;
            this.btnHaut.Text = "Haut";
            this.btnHaut.UseVisualStyleBackColor = false;
            this.btnHaut.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBas
            // 
            this.btnBas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBas.BackColor = System.Drawing.Color.Snow;
            this.btnBas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBas.Location = new System.Drawing.Point(0, 240);
            this.btnBas.Name = "btnBas";
            this.btnBas.Size = new System.Drawing.Size(293, 23);
            this.btnBas.TabIndex = 2;
            this.btnBas.Text = "Bas";
            this.btnBas.UseVisualStyleBackColor = false;
            this.btnBas.Click += new System.EventHandler(this.button2_Click);
            // 
            // lstBxChoices
            // 
            this.lstBxChoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBxChoices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.lstBxChoices.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxChoices.FormattingEnabled = true;
            this.lstBxChoices.Location = new System.Drawing.Point(0, 40);
            this.lstBxChoices.Name = "lstBxChoices";
            this.lstBxChoices.Size = new System.Drawing.Size(293, 199);
            this.lstBxChoices.TabIndex = 0;
            this.lstBxChoices.VerticalScrollbar = false;
            this.lstBxChoices.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstBxChoices_MouseDoubleClick);
            this.lstBxChoices.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxChoices_DrawItem);
            this.lstBxChoices.Click += new System.EventHandler(this.lstBxChoices_Click);
            // 
            // SelectionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 266);
            this.ControlBox = false;
            this.Controls.Add(this.btnBas);
            this.Controls.Add(this.btnHaut);
            this.Controls.Add(this.lstBxChoices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectionFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectionFrm";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.SelectionFrm_Deactivate);
            this.Load += new System.EventHandler(this.SelectionFrm_Load);
            this.Leave += new System.EventHandler(this.SelectionFrm_Leave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectionFrm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public NoScrollBarListBox lstBxChoices;
        public System.Windows.Forms.Button btnHaut;
        public System.Windows.Forms.Button btnBas;


    }
}