namespace BASEDiag
{
    partial class FrmSynchroRadioPhoto
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
         //   this.synchro1 = new BASEDiag.Ctrls.Synchro();
            this.synchro2 = new BASEDiag.Ctrls.Synchro();
            this.synchro1 = new BASEDiag.Ctrls.Synchro();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 7);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleName = "";
            this.splitContainer1.Panel1.Controls.Add(this.synchro1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleName = "";
            this.splitContainer1.Panel2.Controls.Add(this.synchro2);
            this.splitContainer1.Size = new System.Drawing.Size(1502, 640);
            this.splitContainer1.SplitterDistance = 770;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(1188, 669);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(146, 66);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(1342, 669);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(146, 66);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // synchro1
            // 
            this.synchro1.AllowDrop = true;
            this.synchro1.AngleDeRotationPhoto = 0F;
            this.synchro1.AngleDeRotationRadio = 0F;
            this.synchro1.Brightness = 0F;
            this.synchro1.Contraste = 1F;
            this.synchro1.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.synchro1.DrawPointName = false;
            this.synchro1.file = null;
            this.synchro1.HelpFolder = ".\\";
            this.synchro1.HelpImage = null;
            this.synchro1.Location = new System.Drawing.Point(0, -3);
            this.synchro1.Name = "synchro1";
            this.synchro1.PhotoImage = null;
            this.synchro1.RadioImage = null;
            this.synchro1.ReadOnly = false;
            this.synchro1.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.synchro1.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.synchro1.Size = new System.Drawing.Size(770, 640);
            this.synchro1.Synchronized = false;
            this.synchro1.TabIndex = 1;
            this.synchro1.TextIfNoImage = "Pas d\'image";
            this.synchro1.Transparence = 0.99D;
            this.synchro1.zoomPhoto = 1F;
            this.synchro1.zoomRadio = 1F;
            // 
            // synchro2
            // 
            this.synchro2.AllowDrop = true;
            this.synchro2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.synchro2.AngleDeRotationPhoto = 0F;
            this.synchro2.AngleDeRotationRadio = 0F;
            this.synchro2.Brightness = 0F;
            this.synchro2.Contraste = 1F;
            this.synchro2.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.synchro2.DrawPointName = false;
            this.synchro2.file = null;
            this.synchro2.HelpFolder = ".\\";
            this.synchro2.HelpImage = null;
            this.synchro2.Location = new System.Drawing.Point(0, -3);
            this.synchro2.Name = "synchro2";
            this.synchro2.PhotoImage = null;
            this.synchro2.RadioImage = null;
            this.synchro2.ReadOnly = false;
            this.synchro2.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.synchro2.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.synchro2.Size = new System.Drawing.Size(726, 640);
            this.synchro2.Synchronized = false;
            this.synchro2.TabIndex = 2;
            this.synchro2.TextIfNoImage = "Pas d\'image";
            this.synchro2.Transparence = 0.99D;
            this.synchro2.zoomPhoto = 1F;
            this.synchro2.zoomRadio = 1F;
            // 
            // FrmSynchroRadioPhoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 742);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmSynchroRadioPhoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmSynchroRadioPhoto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSynchroRadioPhoto_Load);
            this.Resize += new System.EventHandler(this.FrmSynchroRadioPhoto_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Ctrls.Synchro synchro1;
        private Ctrls.Synchro synchro2;
    }
}