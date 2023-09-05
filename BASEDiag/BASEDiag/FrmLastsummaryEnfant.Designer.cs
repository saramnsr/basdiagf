namespace BASEDiag
{
    partial class FrmLastsummaryEnfant
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLastsummaryEnfant));
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxProfil = new BASEDiag.Ctrls.Analyse62();
            this.pbxRadio = new BASEDiag.Ctrls.Analyse7();
            this.pbxFace = new BASEDiag.Ctrls.Analyse1();
            this.pbxSourire = new BASEDiag.Ctrls.Analyse2();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxPano = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.pbxOccGauche = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.pbxOccFace = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.pbxOccDroit = new BASEDiag.Ctrls.ImageCtrlAgg();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.smilers = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkToSmilers = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.barrePatient1 = new BASEDiag.Ctrls.BarrePatient();
            this.btnChangeScreen = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(63, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(69, 44);
            this.button5.TabIndex = 75;
            this.button5.Text = "Inv";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(751, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 44);
            this.button2.TabIndex = 73;
            this.button2.Text = "Création Dossier Invisalign";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel5);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(-8, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 494);
            this.panel1.TabIndex = 79;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1037, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(235, 494);
            this.tableLayoutPanel5.TabIndex = 73;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(229, 241);
            this.lstBxDiag.TabIndex = 18;
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 250);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(229, 241);
            this.lstBxObjectifs.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1031, 488);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.pbxProfil, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pbxRadio, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pbxFace, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pbxSourire, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1031, 244);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // pbxProfil
            // 
            this.pbxProfil.AllowDrop = true;
            this.pbxProfil.AngleDeRotationPhoto = 0F;
            this.pbxProfil.AngleDeRotationRadio = 0F;
            this.pbxProfil.Brightness = 0F;
            this.pbxProfil.Contraste = 1F;
            this.pbxProfil.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxProfil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxProfil.DrawPointName = false;
            this.pbxProfil.file = null;
            this.pbxProfil.HelpFolder = ".\\";
            this.pbxProfil.HelpImage = null;
            this.pbxProfil.Location = new System.Drawing.Point(517, 3);
            this.pbxProfil.Name = "pbxProfil";
            this.pbxProfil.PhotoImage = null;
            this.pbxProfil.RadioImage = null;
            this.pbxProfil.ReadOnly = true;
            this.pbxProfil.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxProfil.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxProfil.Size = new System.Drawing.Size(251, 238);
            this.pbxProfil.Synchronized = false;
            this.pbxProfil.TabIndex = 11;
            this.pbxProfil.TextIfNoImage = "Pas d\'image";
            this.pbxProfil.Transparence = 0.99D;
            this.pbxProfil.zoomPhoto = 1F;
            this.pbxProfil.zoomRadio = 1F;
            // 
            // pbxRadio
            // 
            this.pbxRadio.AllowDrop = true;
            this.pbxRadio.AngleDeRotationPhoto = 0F;
            this.pbxRadio.AngleDeRotationRadio = 0F;
            this.pbxRadio.Brightness = 0F;
            this.pbxRadio.Contraste = 1F;
            this.pbxRadio.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxRadio.DrawPointName = false;
            this.pbxRadio.file = null;
            this.pbxRadio.HelpFolder = ".\\";
            this.pbxRadio.HelpImage = null;
            this.pbxRadio.Location = new System.Drawing.Point(774, 3);
            this.pbxRadio.Name = "pbxRadio";
            this.pbxRadio.PhotoImage = null;
            this.pbxRadio.RadioImage = null;
            this.pbxRadio.ReadOnly = true;
            this.pbxRadio.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxRadio.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxRadio.Size = new System.Drawing.Size(254, 238);
            this.pbxRadio.Synchronized = false;
            this.pbxRadio.TabIndex = 10;
            this.pbxRadio.TextIfNoImage = "Pas d\'image";
            this.pbxRadio.Transparence = 0.99D;
            this.pbxRadio.zoomPhoto = 1F;
            this.pbxRadio.zoomRadio = 1F;
            // 
            // pbxFace
            // 
            this.pbxFace.AllowDrop = true;
            this.pbxFace.AngleDeRotationPhoto = 0F;
            this.pbxFace.AngleDeRotationRadio = 0F;
            this.pbxFace.Brightness = 0F;
            this.pbxFace.Contraste = 1F;
            this.pbxFace.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxFace.DrawPointName = false;
            this.pbxFace.file = null;
            this.pbxFace.HelpFolder = ".\\";
            this.pbxFace.HelpImage = null;
            this.pbxFace.Location = new System.Drawing.Point(3, 3);
            this.pbxFace.Name = "pbxFace";
            this.pbxFace.PhotoImage = null;
            this.pbxFace.RadioImage = null;
            this.pbxFace.ReadOnly = true;
            this.pbxFace.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxFace.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxFace.Size = new System.Drawing.Size(251, 238);
            this.pbxFace.Synchronized = false;
            this.pbxFace.TabIndex = 4;
            this.pbxFace.TextIfNoImage = "Pas d\'image";
            this.pbxFace.Transparence = 0.99D;
            this.pbxFace.zoomPhoto = 1F;
            this.pbxFace.zoomRadio = 1F;
            // 
            // pbxSourire
            // 
            this.pbxSourire.AllowDrop = true;
            this.pbxSourire.AngleDeRotationPhoto = 0F;
            this.pbxSourire.AngleDeRotationRadio = 0F;
            this.pbxSourire.Brightness = 0F;
            this.pbxSourire.Contraste = 1F;
            this.pbxSourire.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxSourire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxSourire.DrawPointName = false;
            this.pbxSourire.file = null;
            this.pbxSourire.HelpFolder = ".\\";
            this.pbxSourire.HelpImage = null;
            this.pbxSourire.Location = new System.Drawing.Point(260, 3);
            this.pbxSourire.Name = "pbxSourire";
            this.pbxSourire.PhotoImage = null;
            this.pbxSourire.RadioImage = null;
            this.pbxSourire.ReadOnly = true;
            this.pbxSourire.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxSourire.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxSourire.Size = new System.Drawing.Size(251, 238);
            this.pbxSourire.Synchronized = false;
            this.pbxSourire.TabIndex = 3;
            this.pbxSourire.TextIfNoImage = "Pas d\'image";
            this.pbxSourire.Transparence = 0.99D;
            this.pbxSourire.zoomPhoto = 1F;
            this.pbxSourire.zoomRadio = 1F;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.18791F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.8121F));
            this.tableLayoutPanel4.Controls.Add(this.pbxPano, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pbxOccGauche, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pbxOccFace, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pbxOccDroit, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 247);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1025, 238);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // pbxPano
            // 
            this.pbxPano.AllowDrop = true;
            this.pbxPano.AngleDeRotationPhoto = 0F;
            this.pbxPano.AngleDeRotationRadio = 0F;
            this.pbxPano.Brightness = 0F;
            this.pbxPano.Contraste = 1F;
            this.pbxPano.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxPano.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxPano.DrawPointName = false;
            this.pbxPano.file = null;
            this.pbxPano.HelpFolder = ".\\";
            this.pbxPano.HelpImage = null;
            this.pbxPano.Location = new System.Drawing.Point(534, 3);
            this.pbxPano.Name = "pbxPano";
            this.pbxPano.PhotoImage = null;
            this.pbxPano.RadioImage = null;
            this.pbxPano.ReadOnly = false;
            this.pbxPano.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxPano.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxPano.Size = new System.Drawing.Size(280, 232);
            this.pbxPano.Synchronized = false;
            this.pbxPano.TabIndex = 14;
            this.pbxPano.TextIfNoImage = "Pas d\'image";
            this.pbxPano.Transparence = 0.99D;
            this.pbxPano.zoomPhoto = 1F;
            this.pbxPano.zoomRadio = 1F;
            // 
            // pbxOccGauche
            // 
            this.pbxOccGauche.AllowDrop = true;
            this.pbxOccGauche.AngleDeRotationPhoto = 0F;
            this.pbxOccGauche.AngleDeRotationRadio = 0F;
            this.pbxOccGauche.Brightness = 0F;
            this.pbxOccGauche.Contraste = 1F;
            this.pbxOccGauche.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxOccGauche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxOccGauche.DrawPointName = false;
            this.pbxOccGauche.file = null;
            this.pbxOccGauche.HelpFolder = ".\\";
            this.pbxOccGauche.HelpImage = null;
            this.pbxOccGauche.Location = new System.Drawing.Point(222, 3);
            this.pbxOccGauche.Name = "pbxOccGauche";
            this.pbxOccGauche.PhotoImage = null;
            this.pbxOccGauche.RadioImage = null;
            this.pbxOccGauche.ReadOnly = false;
            this.pbxOccGauche.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxOccGauche.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxOccGauche.Size = new System.Drawing.Size(306, 232);
            this.pbxOccGauche.Synchronized = false;
            this.pbxOccGauche.TabIndex = 12;
            this.pbxOccGauche.TextIfNoImage = "Pas d\'image";
            this.pbxOccGauche.Transparence = 0.99D;
            this.pbxOccGauche.zoomPhoto = 1F;
            this.pbxOccGauche.zoomRadio = 1F;
            // 
            // pbxOccFace
            // 
            this.pbxOccFace.AllowDrop = true;
            this.pbxOccFace.AngleDeRotationPhoto = 0F;
            this.pbxOccFace.AngleDeRotationRadio = 0F;
            this.pbxOccFace.Brightness = 0F;
            this.pbxOccFace.Contraste = 1F;
            this.pbxOccFace.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxOccFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxOccFace.DrawPointName = false;
            this.pbxOccFace.file = null;
            this.pbxOccFace.HelpFolder = ".\\";
            this.pbxOccFace.HelpImage = null;
            this.pbxOccFace.Location = new System.Drawing.Point(3, 3);
            this.pbxOccFace.Name = "pbxOccFace";
            this.pbxOccFace.PhotoImage = null;
            this.pbxOccFace.RadioImage = null;
            this.pbxOccFace.ReadOnly = false;
            this.pbxOccFace.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxOccFace.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxOccFace.Size = new System.Drawing.Size(213, 232);
            this.pbxOccFace.Synchronized = false;
            this.pbxOccFace.TabIndex = 7;
            this.pbxOccFace.TextIfNoImage = "Pas d\'image";
            this.pbxOccFace.Transparence = 0.99D;
            this.pbxOccFace.zoomPhoto = 1F;
            this.pbxOccFace.zoomRadio = 1F;
            // 
            // pbxOccDroit
            // 
            this.pbxOccDroit.AllowDrop = true;
            this.pbxOccDroit.AngleDeRotationPhoto = 0F;
            this.pbxOccDroit.AngleDeRotationRadio = 0F;
            this.pbxOccDroit.Brightness = 0F;
            this.pbxOccDroit.Contraste = 1F;
            this.pbxOccDroit.currentmode = BASEDiag.Ctrls.ImageCtrlAgg.ModeSaisie.None;
            this.pbxOccDroit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxOccDroit.DrawPointName = false;
            this.pbxOccDroit.file = null;
            this.pbxOccDroit.HelpFolder = ".\\";
            this.pbxOccDroit.HelpImage = null;
            this.pbxOccDroit.Location = new System.Drawing.Point(820, 3);
            this.pbxOccDroit.Name = "pbxOccDroit";
            this.pbxOccDroit.PhotoImage = null;
            this.pbxOccDroit.RadioImage = null;
            this.pbxOccDroit.ReadOnly = false;
            this.pbxOccDroit.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.pbxOccDroit.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.pbxOccDroit.Size = new System.Drawing.Size(202, 232);
            this.pbxOccDroit.Synchronized = false;
            this.pbxOccDroit.TabIndex = 6;
            this.pbxOccDroit.TextIfNoImage = "Pas d\'image";
            this.pbxOccDroit.Transparence = 0.99D;
            this.pbxOccDroit.zoomPhoto = 1F;
            this.pbxOccDroit.zoomRadio = 1F;
            // 
            // lvPPT
            // 
            this.lvPPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.Location = new System.Drawing.Point(138, 3);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(607, 44);
            this.lvPPT.TabIndex = 70;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            // 
            // smilers
            // 
            this.smilers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smilers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.smilers.Location = new System.Drawing.Point(900, 2);
            this.smilers.Margin = new System.Windows.Forms.Padding(2);
            this.smilers.Name = "smilers";
            this.smilers.Size = new System.Drawing.Size(146, 46);
            this.smilers.TabIndex = 76;
            this.smilers.Text = "Création Dossier Smilers";
            this.smilers.UseVisualStyleBackColor = true;
            this.smilers.Click += new System.EventHandler(this.smilers_Click_1);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(1155, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 44);
            this.button3.TabIndex = 74;
            this.button3.Text = "Compte rendu";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1051, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 44);
            this.button1.TabIndex = 72;
            this.button1.Text = "Créer le Devis";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.Controls.Add(this.button5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvPPT, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.smilers, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lnkToSmilers, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 540);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1256, 50);
            this.tableLayoutPanel1.TabIndex = 78;
            // 
            // lnkToSmilers
            // 
            this.lnkToSmilers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lnkToSmilers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lnkToSmilers.Location = new System.Drawing.Point(2, 2);
            this.lnkToSmilers.Margin = new System.Windows.Forms.Padding(2);
            this.lnkToSmilers.Name = "lnkToSmilers";
            this.lnkToSmilers.Size = new System.Drawing.Size(56, 46);
            this.lnkToSmilers.TabIndex = 77;
            this.lnkToSmilers.Text = "Smilers";
            this.lnkToSmilers.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(1211, 57);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 10);
            this.button4.TabIndex = 76;
            this.button4.Text = "Params";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(4, 2);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(1137, 49);
            this.barrePatient1.TabIndex = 80;
            // 
            // btnChangeScreen
            // 
            this.btnChangeScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeScreen.Image = global::BASEDiag.Properties.Resources.changeecran;
            this.btnChangeScreen.Location = new System.Drawing.Point(1146, 3);
            this.btnChangeScreen.Name = "btnChangeScreen";
            this.btnChangeScreen.Size = new System.Drawing.Size(53, 50);
            this.btnChangeScreen.TabIndex = 82;
            this.btnChangeScreen.UseVisualStyleBackColor = true;
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.ForeColor = System.Drawing.Color.Black;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(1204, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 81;
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // FrmLastsummaryEnfant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 590);
            this.Controls.Add(this.btnChangeScreen);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.barrePatient1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLastsummaryEnfant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Résumé";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button smilers;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button lnkToSmilers;
        private System.Windows.Forms.Button button4;
        private Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.Button btnChangeScreen;
        private System.Windows.Forms.Button btnclose;
        internal Ctrls.Analyse2 pbxSourire;
        internal Ctrls.Analyse1 pbxFace;
        internal Ctrls.Analyse7 pbxRadio;
        private Ctrls.Analyse62 pbxProfil;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        internal Ctrls.ImageCtrlAgg pbxOccDroit;
        internal Ctrls.ImageCtrlAgg pbxOccFace;
        internal Ctrls.ImageCtrlAgg pbxOccGauche;
        internal Ctrls.ImageCtrlAgg pbxPano;


    }
}