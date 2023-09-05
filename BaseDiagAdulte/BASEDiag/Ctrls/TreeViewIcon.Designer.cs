namespace BASEDiagAdulte.Ctrls
{
    partial class TreeViewIcon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VerticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // VerticalScrollBar
            // 
            this.VerticalScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.VerticalScrollBar.LargeChange = 20;
            this.VerticalScrollBar.Location = new System.Drawing.Point(203, 0);
            this.VerticalScrollBar.Name = "VerticalScrollBar";
            this.VerticalScrollBar.Size = new System.Drawing.Size(15, 454);
            this.VerticalScrollBar.TabIndex = 1;
            this.VerticalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VerticalScrollBar_Scroll);
            // 
            // TreeViewIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.VerticalScrollBar);
            this.Name = "TreeViewIcon";
            this.Size = new System.Drawing.Size(218, 454);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TreeViewIcon_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeViewIcon_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewIcon_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar VerticalScrollBar;
    }
}
