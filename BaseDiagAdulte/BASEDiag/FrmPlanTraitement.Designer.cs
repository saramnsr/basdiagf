namespace BASEDiagAdulte
{
    partial class FrmPlanTraitement
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ImageList Bigimgs;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlanTraitement));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.btnclose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePano = new BASEDiagAdulte.Ctrls.AnalysePlanTraitement();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.imageSourire = new BASEDiagAdulte.Ctrls.AnalysePlanTraitement();
            this.lblTitre = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Bigimgs
            // 
            Bigimgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Bigimgs.ImageStream")));
            Bigimgs.TransparentColor = System.Drawing.Color.Transparent;
            Bigimgs.Images.SetKeyName(0, "mediumPPT.png");
            // 
            // SmallImg
            // 
            this.SmallImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImg.ImageStream")));
            this.SmallImg.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImg.Images.SetKeyName(0, "VerySmallPPT.png");
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(745, 10);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 6;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.imagePano, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.imageSourire, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 89);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 467);
            this.tableLayoutPanel1.TabIndex = 40;
            // 
            // imagePano
            // 
            this.imagePano.AllowDrop = true;
            this.imagePano.AngleDeRotationPhoto = 0F;
            this.imagePano.AngleDeRotationRadio = 0F;
            this.imagePano.Brightness = 0F;
            this.imagePano.Contraste = 1F;
            this.imagePano.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.imagePano.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePano.DrawPointName = false;
            this.imagePano.file = null;
            this.imagePano.HelpFolder = ".\\";
            this.imagePano.HelpImage = null;
            this.imagePano.Location = new System.Drawing.Point(394, 53);
            this.imagePano.Name = "imagePano";
            this.imagePano.PhotoImage = null;
            this.imagePano.RadioImage = null;
            this.imagePano.ReadOnly = false;
            this.imagePano.resources = null;
            this.imagePano.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imagePano.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imagePano.Size = new System.Drawing.Size(386, 411);
            this.imagePano.Synchronized = false;
            this.imagePano.TabIndex = 2;
            this.imagePano.TextIfNoImage = "Pas d\'image";
            this.imagePano.Transparence = 0.99D;
            this.imagePano.zoomPhoto = 1F;
            this.imagePano.zoomRadio = 1F;
            this.imagePano.OnRadioChanged += new System.EventHandler(this.imageCtrl1_OnImageChanged);
            this.imagePano.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagePano_DragDrop);
            this.imagePano.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagePano_DragEnter);
            this.imagePano.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePano_MouseDown);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(783, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // imageSourire
            // 
            this.imageSourire.AllowDrop = true;
            this.imageSourire.AngleDeRotationPhoto = 0F;
            this.imageSourire.AngleDeRotationRadio = 0F;
            this.imageSourire.Brightness = 0F;
            this.imageSourire.Contraste = 1F;
            this.imageSourire.currentmode = BASEDiagAdulte.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.imageSourire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSourire.DrawPointName = false;
            this.imageSourire.file = null;
            this.imageSourire.HelpFolder = ".\\";
            this.imageSourire.HelpImage = null;
            this.imageSourire.Location = new System.Drawing.Point(3, 53);
            this.imageSourire.Name = "imageSourire";
            this.imageSourire.PhotoImage = null;
            this.imageSourire.RadioImage = null;
            this.imageSourire.ReadOnly = false;
            this.imageSourire.resources = null;
            this.imageSourire.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.imageSourire.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.imageSourire.Size = new System.Drawing.Size(385, 411);
            this.imageSourire.Synchronized = false;
            this.imageSourire.TabIndex = 1;
            this.imageSourire.TextIfNoImage = "Pas d\'image";
            this.imageSourire.Transparence = 0.99D;
            this.imageSourire.zoomPhoto = 1F;
            this.imageSourire.zoomRadio = 1F;
            this.imageSourire.OnRadioChanged += new System.EventHandler(this.imageSourire_OnRadioChanged);
            this.imageSourire.DragDrop += new System.Windows.Forms.DragEventHandler(this.imageSourire_DragDrop);
            this.imageSourire.DragEnter += new System.Windows.Forms.DragEventHandler(this.imageSourire_DragEnter);
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(13, 63);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(670, 23);
            this.lblTitre.TabIndex = 39;
            this.lblTitre.Text = "Plan de traitement visuel";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Image = global::BASEDiagAdulte.Properties.Resources.Next;
            this.btnNext.Location = new System.Drawing.Point(741, 562);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 50);
            this.btnNext.TabIndex = 38;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.btnPrint.Location = new System.Drawing.Point(685, 562);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 50);
            this.btnPrint.TabIndex = 37;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(689, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 36;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(68, 561);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(611, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 33;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(12, 561);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 50);
            this.BtnPrevious.TabIndex = 15;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(12, 10);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(671, 50);
            this.barrePatient1.TabIndex = 1;
            this.barrePatient1.Load += new System.EventHandler(this.barrePatient1_Load);
            // 
            // FrmPlanTraitement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(807, 624);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.barrePatient1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPlanTraitement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAnalyse8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse8_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse8_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BASEDiagAdulte.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Ctrls.AnalysePlanTraitement imagePano;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Ctrls.AnalysePlanTraitement imageSourire;
    }
}