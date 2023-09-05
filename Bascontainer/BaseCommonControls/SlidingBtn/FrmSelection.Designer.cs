namespace BaseCommonControls
{
    partial class FrmSelection
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
            this.ucTreeListBtn1 = new BaseCommonControls.SlidingList();
            this.SuspendLayout();
            // 
            // ucTreeListBtn1
            // 
            this.ucTreeListBtn1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucTreeListBtn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucTreeListBtn1.ButtonSize = new System.Drawing.SizeF(80F, 80F);
            this.ucTreeListBtn1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeListBtn1.Location = new System.Drawing.Point(0, 0);
            this.ucTreeListBtn1.Name = "ucTreeListBtn1";
            this.ucTreeListBtn1.Size = new System.Drawing.Size(644, 79);
            this.ucTreeListBtn1.TabIndex = 0;
            // 
            // FrmSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 79);
            this.Controls.Add(this.ucTreeListBtn1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmSelection";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FrmSelection_Deactivate);
            this.Shown += new System.EventHandler(this.FrmSelection_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        internal SlidingList ucTreeListBtn1;

    }
}