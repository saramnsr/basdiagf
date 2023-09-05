namespace BASEDiagAdulte
{
    partial class FrmObjTrmnt
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
            this.txtbxObjTrmnt = new System.Windows.Forms.TextBox();
            this.lvDescription = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.colLibelle = new System.Windows.Forms.ColumnHeader();
            this.txtbxSearch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtbxObjTrmnt
            // 
            this.txtbxObjTrmnt.AcceptsReturn = true;
            this.txtbxObjTrmnt.AllowDrop = true;
            this.txtbxObjTrmnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxObjTrmnt.Location = new System.Drawing.Point(352, 12);
            this.txtbxObjTrmnt.Multiline = true;
            this.txtbxObjTrmnt.Name = "txtbxObjTrmnt";
            this.txtbxObjTrmnt.Size = new System.Drawing.Size(314, 403);
            this.txtbxObjTrmnt.TabIndex = 0;
            this.txtbxObjTrmnt.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtbxObjTrmnt_DragDrop);
            this.txtbxObjTrmnt.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtbxObjTrmnt_DragEnter);
            // 
            // lvDescription
            // 
            this.lvDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvDescription.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLibelle});
            this.lvDescription.FullRowSelect = true;
            this.lvDescription.HideSelection = false;
            this.lvDescription.Location = new System.Drawing.Point(12, 41);
            this.lvDescription.Name = "lvDescription";
            this.lvDescription.Size = new System.Drawing.Size(334, 374);
            this.lvDescription.TabIndex = 1;
            this.lvDescription.UseCompatibleStateImageBehavior = false;
            this.lvDescription.View = System.Windows.Forms.View.Details;
            this.lvDescription.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDescription_MouseDoubleClick);
            this.lvDescription.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvDescription_MouseMove);
            this.lvDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvDescription_MouseDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(592, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 50);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(511, 421);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 50);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // colLibelle
            // 
            this.colLibelle.Text = "Libellé";
            this.colLibelle.Width = 328;
            // 
            // txtbxSearch
            // 
            this.txtbxSearch.Location = new System.Drawing.Point(12, 12);
            this.txtbxSearch.Name = "txtbxSearch";
            this.txtbxSearch.Size = new System.Drawing.Size(334, 20);
            this.txtbxSearch.TabIndex = 4;
            this.txtbxSearch.TextChanged += new System.EventHandler(this.txtbxSearch_TextChanged);
            // 
            // FrmObjTrmnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 483);
            this.Controls.Add(this.txtbxSearch);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvDescription);
            this.Controls.Add(this.txtbxObjTrmnt);
            this.Name = "FrmObjTrmnt";
            this.Text = "Objectifs";
            this.Load += new System.EventHandler(this.FrmObjTrmnt_Load);
            this.Shown += new System.EventHandler(this.FrmObjTrmnt_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxObjTrmnt;
        private System.Windows.Forms.ListView lvDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader colLibelle;
        private System.Windows.Forms.TextBox txtbxSearch;
    }
}