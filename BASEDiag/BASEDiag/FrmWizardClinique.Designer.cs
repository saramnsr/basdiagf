namespace BASEDiag
{
    partial class FrmWizardClinique
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlLstBxDiag = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.pnlDefinitionPlanTraitement = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstBxObjectifsDefinitifs = new System.Windows.Forms.ListBox();
            this.lstBxAppareillages = new System.Windows.Forms.ListBox();
            this.pnlPlanTraitement = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.btnSaveAsModel = new System.Windows.Forms.Button();
            this.btnAddApp = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.propositionCtrl1 = new BASEDiag.Ctrls.PropositionCtrlV2.PropositionCtrlV2();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.appareilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semestresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifierLeTarifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surveillanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traitementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifierLeTarifDuSemestreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.lstBxPersonneAContacter = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSigne = new System.Windows.Forms.Button();
            this.btnDEP = new System.Windows.Forms.Button();
            this.BtnShowRisques = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDebuTraitement = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxSemEntames = new ControlsLibrary.TreeViewIconCbx();
            this.cbxPratResp = new ControlsLibrary.TreeViewIconCbx();
            this.cbxAssResp = new ControlsLibrary.TreeViewIconCbx();
            this.btnAddContention = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddTraitement = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.pnlContainer.SuspendLayout();
            this.pnlLstBxDiag.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlDefinitionPlanTraitement.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlPlanTraitement.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlLstBxDiag);
            this.pnlContainer.Controls.Add(this.pnlDefinitionPlanTraitement);
            this.pnlContainer.Location = new System.Drawing.Point(12, 12);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1178, 585);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlLstBxDiag
            // 
            this.pnlLstBxDiag.Controls.Add(this.tableLayoutPanel1);
            this.pnlLstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLstBxDiag.Location = new System.Drawing.Point(0, 0);
            this.pnlLstBxDiag.Name = "pnlLstBxDiag";
            this.pnlLstBxDiag.Size = new System.Drawing.Size(1178, 585);
            this.pnlLstBxDiag.TabIndex = 0;
            this.pnlLstBxDiag.Visible = false;
            this.pnlLstBxDiag.VisibleChanged += new System.EventHandler(this.pnlLstBxDiag_VisibleChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1178, 585);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(203, 3);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(972, 576);
            this.lstBxObjectifs.TabIndex = 20;
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(194, 572);
            this.lstBxDiag.TabIndex = 19;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // pnlDefinitionPlanTraitement
            // 
            this.pnlDefinitionPlanTraitement.Controls.Add(this.tableLayoutPanel2);
            this.pnlDefinitionPlanTraitement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDefinitionPlanTraitement.Location = new System.Drawing.Point(0, 0);
            this.pnlDefinitionPlanTraitement.Name = "pnlDefinitionPlanTraitement";
            this.pnlDefinitionPlanTraitement.Size = new System.Drawing.Size(1178, 585);
            this.pnlDefinitionPlanTraitement.TabIndex = 1;
            this.pnlDefinitionPlanTraitement.Visible = false;
            this.pnlDefinitionPlanTraitement.VisibleChanged += new System.EventHandler(this.pnlDefinitionPlanTraitement_VisibleChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlPlanTraitement, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1178, 585);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 579);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstBxObjectifsDefinitifs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstBxAppareillages);
            this.splitContainer1.Size = new System.Drawing.Size(188, 533);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstBxObjectifsDefinitifs
            // 
            this.lstBxObjectifsDefinitifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifsDefinitifs.FormattingEnabled = true;
            this.lstBxObjectifsDefinitifs.Location = new System.Drawing.Point(0, 0);
            this.lstBxObjectifsDefinitifs.Name = "lstBxObjectifsDefinitifs";
            this.lstBxObjectifsDefinitifs.Size = new System.Drawing.Size(188, 329);
            this.lstBxObjectifsDefinitifs.TabIndex = 0;
            // 
            // lstBxAppareillages
            // 
            this.lstBxAppareillages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxAppareillages.FormattingEnabled = true;
            this.lstBxAppareillages.Location = new System.Drawing.Point(0, 0);
            this.lstBxAppareillages.Name = "lstBxAppareillages";
            this.lstBxAppareillages.Size = new System.Drawing.Size(188, 199);
            this.lstBxAppareillages.TabIndex = 0;
            this.lstBxAppareillages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstBxAppareillages_MouseMove);
            this.lstBxAppareillages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstBxAppareillages_MouseDown);
            // 
            // pnlPlanTraitement
            // 
            this.pnlPlanTraitement.Controls.Add(this.button5);
            this.pnlPlanTraitement.Controls.Add(this.btnSaveAsModel);
            this.pnlPlanTraitement.Controls.Add(this.btnAddApp);
            this.pnlPlanTraitement.Controls.Add(this.button3);
            this.pnlPlanTraitement.Controls.Add(this.propositionCtrl1);
            this.pnlPlanTraitement.Controls.Add(this.button1);
            this.pnlPlanTraitement.Controls.Add(this.lstBxPersonneAContacter);
            this.pnlPlanTraitement.Controls.Add(this.label3);
            this.pnlPlanTraitement.Controls.Add(this.btnSigne);
            this.pnlPlanTraitement.Controls.Add(this.btnDEP);
            this.pnlPlanTraitement.Controls.Add(this.BtnShowRisques);
            this.pnlPlanTraitement.Controls.Add(this.label2);
            this.pnlPlanTraitement.Controls.Add(this.dtpDebuTraitement);
            this.pnlPlanTraitement.Controls.Add(this.label1);
            this.pnlPlanTraitement.Controls.Add(this.label10);
            this.pnlPlanTraitement.Controls.Add(this.label9);
            this.pnlPlanTraitement.Controls.Add(this.cbxSemEntames);
            this.pnlPlanTraitement.Controls.Add(this.cbxPratResp);
            this.pnlPlanTraitement.Controls.Add(this.cbxAssResp);
            this.pnlPlanTraitement.Controls.Add(this.btnAddContention);
            this.pnlPlanTraitement.Controls.Add(this.button2);
            this.pnlPlanTraitement.Controls.Add(this.btnAddTraitement);
            this.pnlPlanTraitement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlanTraitement.Location = new System.Drawing.Point(203, 3);
            this.pnlPlanTraitement.Name = "pnlPlanTraitement";
            this.pnlPlanTraitement.Size = new System.Drawing.Size(972, 579);
            this.pnlPlanTraitement.TabIndex = 1;
            this.pnlPlanTraitement.VisibleChanged += new System.EventHandler(this.pnlPlanTraitement_VisibleChanged);
            this.pnlPlanTraitement.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPlanTraitement_Paint);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 352);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(151, 23);
            this.button5.TabIndex = 67;
            this.button5.Text = "Ouvrir un modele";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnSaveAsModel
            // 
            this.btnSaveAsModel.Location = new System.Drawing.Point(3, 323);
            this.btnSaveAsModel.Name = "btnSaveAsModel";
            this.btnSaveAsModel.Size = new System.Drawing.Size(151, 23);
            this.btnSaveAsModel.TabIndex = 66;
            this.btnSaveAsModel.Text = "Enregistrer comme modele";
            this.btnSaveAsModel.UseVisualStyleBackColor = true;
            this.btnSaveAsModel.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnAddApp
            // 
            this.btnAddApp.Location = new System.Drawing.Point(3, 269);
            this.btnAddApp.Name = "btnAddApp";
            this.btnAddApp.Size = new System.Drawing.Size(151, 23);
            this.btnAddApp.TabIndex = 65;
            this.btnAddApp.Text = "Ajouter un appareil";
            this.btnAddApp.UseVisualStyleBackColor = true;
            this.btnAddApp.Click += new System.EventHandler(this.btnAddApp_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(151, 23);
            this.button3.TabIndex = 64;
            this.button3.Text = "Surveillances";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // propositionCtrl1
            // 
            this.propositionCtrl1.AllowDrop = true;
            this.propositionCtrl1.ContextMenuStrip = this.contextMenuStrip1;
            this.propositionCtrl1.CurrentPatient = null;
            this.propositionCtrl1.Location = new System.Drawing.Point(160, 153);
            this.propositionCtrl1.Name = "propositionCtrl1";
            this.propositionCtrl1.Size = new System.Drawing.Size(809, 418);
            this.propositionCtrl1.TabIndex = 63;
            this.propositionCtrl1.OnSelectionChange += new System.EventHandler(this.propositionCtrl1_OnSelectionChange);
            this.propositionCtrl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.propositionCtrl1_DragDrop);
            this.propositionCtrl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.propositionCtrl1_DragEnter);
            this.propositionCtrl1.DragOver += new System.Windows.Forms.DragEventHandler(this.propositionCtrl1_DragOver);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appareilsToolStripMenuItem,
            this.semestresToolStripMenuItem,
            this.traitementToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 70);
            // 
            // appareilsToolStripMenuItem
            // 
            this.appareilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem,
            this.ajouterToolStripMenuItem});
            this.appareilsToolStripMenuItem.Name = "appareilsToolStripMenuItem";
            this.appareilsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.appareilsToolStripMenuItem.Text = "Appareils";
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.DropDownOpening += new System.EventHandler(this.supprimerToolStripMenuItem_DropDownOpening);
            // 
            // ajouterToolStripMenuItem
            // 
            this.ajouterToolStripMenuItem.Name = "ajouterToolStripMenuItem";
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ajouterToolStripMenuItem.Text = "Ajouter";
            this.ajouterToolStripMenuItem.Click += new System.EventHandler(this.ajouterToolStripMenuItem_Click);
            // 
            // semestresToolStripMenuItem
            // 
            this.semestresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifierLeTarifToolStripMenuItem,
            this.surveillanceToolStripMenuItem});
            this.semestresToolStripMenuItem.Name = "semestresToolStripMenuItem";
            this.semestresToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.semestresToolStripMenuItem.Text = "Semestre";
            // 
            // modifierLeTarifToolStripMenuItem
            // 
            this.modifierLeTarifToolStripMenuItem.Name = "modifierLeTarifToolStripMenuItem";
            this.modifierLeTarifToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.modifierLeTarifToolStripMenuItem.Text = "Modifier le tarif";
            this.modifierLeTarifToolStripMenuItem.Click += new System.EventHandler(this.modifierLeTarifToolStripMenuItem_Click);
            // 
            // surveillanceToolStripMenuItem
            // 
            this.surveillanceToolStripMenuItem.Name = "surveillanceToolStripMenuItem";
            this.surveillanceToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.surveillanceToolStripMenuItem.Text = "Surveillance";
            this.surveillanceToolStripMenuItem.Click += new System.EventHandler(this.surveillanceToolStripMenuItem_Click);
            // 
            // traitementToolStripMenuItem
            // 
            this.traitementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifierLeTarifDuSemestreToolStripMenuItem});
            this.traitementToolStripMenuItem.Name = "traitementToolStripMenuItem";
            this.traitementToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.traitementToolStripMenuItem.Text = "Traitement";
            // 
            // modifierLeTarifDuSemestreToolStripMenuItem
            // 
            this.modifierLeTarifDuSemestreToolStripMenuItem.Name = "modifierLeTarifDuSemestreToolStripMenuItem";
            this.modifierLeTarifDuSemestreToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.modifierLeTarifDuSemestreToolStripMenuItem.Text = "Modifier le tarif du semestre";
            this.modifierLeTarifDuSemestreToolStripMenuItem.Click += new System.EventHandler(this.modifierLeTarifDuSemestreToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 23);
            this.button1.TabIndex = 62;
            this.button1.Text = "Devis et consentement";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_4);
            // 
            // lstBxPersonneAContacter
            // 
            this.lstBxPersonneAContacter.FormattingEnabled = true;
            this.lstBxPersonneAContacter.Location = new System.Drawing.Point(635, 52);
            this.lstBxPersonneAContacter.Name = "lstBxPersonneAContacter";
            this.lstBxPersonneAContacter.Size = new System.Drawing.Size(334, 95);
            this.lstBxPersonneAContacter.TabIndex = 61;
            this.lstBxPersonneAContacter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstBxPersonneAContacter_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(632, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Personnes à contacter avant traitement : ";
            // 
            // btnSigne
            // 
            this.btnSigne.Location = new System.Drawing.Point(3, 490);
            this.btnSigne.Name = "btnSigne";
            this.btnSigne.Size = new System.Drawing.Size(151, 23);
            this.btnSigne.TabIndex = 58;
            this.btnSigne.Text = "Signer la proposition";
            this.btnSigne.UseVisualStyleBackColor = true;
            this.btnSigne.Click += new System.EventHandler(this.btnSigne_Click);
            // 
            // btnDEP
            // 
            this.btnDEP.Enabled = false;
            this.btnDEP.Location = new System.Drawing.Point(3, 519);
            this.btnDEP.Name = "btnDEP";
            this.btnDEP.Size = new System.Drawing.Size(151, 23);
            this.btnDEP.TabIndex = 57;
            this.btnDEP.Text = "Imprimer la 1ere DEP";
            this.btnDEP.UseVisualStyleBackColor = true;
            this.btnDEP.Click += new System.EventHandler(this.btnDEP_Click);
            // 
            // BtnShowRisques
            // 
            this.BtnShowRisques.Enabled = false;
            this.BtnShowRisques.Location = new System.Drawing.Point(3, 548);
            this.BtnShowRisques.Name = "BtnShowRisques";
            this.BtnShowRisques.Size = new System.Drawing.Size(151, 23);
            this.BtnShowRisques.TabIndex = 56;
            this.BtnShowRisques.Text = "Voir les risques";
            this.BtnShowRisques.UseVisualStyleBackColor = true;
            this.BtnShowRisques.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Date de début de traitement (a priori) : ";
            // 
            // dtpDebuTraitement
            // 
            this.dtpDebuTraitement.Location = new System.Drawing.Point(413, 113);
            this.dtpDebuTraitement.Name = "dtpDebuTraitement";
            this.dtpDebuTraitement.Size = new System.Drawing.Size(200, 20);
            this.dtpDebuTraitement.TabIndex = 53;
            this.dtpDebuTraitement.ValueChanged += new System.EventHandler(this.dtpDebuTraitement_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Nombre de semestres entamés : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(311, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 48;
            this.label10.Text = "Assistante responsable : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Praticien responsable : ";
            // 
            // cbxSemEntames
            // 
            this.cbxSemEntames.ft = new System.Drawing.Font("Garamond", 12F);
            this.cbxSemEntames.Location = new System.Drawing.Point(118, 100);
            this.cbxSemEntames.Name = "cbxSemEntames";
            this.cbxSemEntames.SelectedIndex = -1;
            this.cbxSemEntames.SelectedItem = null;
            this.cbxSemEntames.Size = new System.Drawing.Size(172, 33);
            this.cbxSemEntames.TabIndex = 51;
            this.cbxSemEntames.VisibleItems = 5;
            this.cbxSemEntames.Load += new System.EventHandler(this.cbxSemEntames_Load);
            this.cbxSemEntames.SelectedIndexChanged += new System.EventHandler(this.cbxSemEntames_SelectedIndexChanged);
            // 
            // cbxPratResp
            // 
            this.cbxPratResp.ft = new System.Drawing.Font("Garamond", 12F);
            this.cbxPratResp.Location = new System.Drawing.Point(118, 49);
            this.cbxPratResp.Name = "cbxPratResp";
            this.cbxPratResp.SelectedIndex = -1;
            this.cbxPratResp.SelectedItem = null;
            this.cbxPratResp.Size = new System.Drawing.Size(172, 33);
            this.cbxPratResp.TabIndex = 50;
            this.cbxPratResp.VisibleItems = 5;
            this.cbxPratResp.Load += new System.EventHandler(this.cbxPratResp_Load);
            this.cbxPratResp.SelectedIndexChanged += new System.EventHandler(this.cbxPratResp_SelectedIndexChanged);
            // 
            // cbxAssResp
            // 
            this.cbxAssResp.ft = new System.Drawing.Font("Garamond", 12F);
            this.cbxAssResp.Location = new System.Drawing.Point(413, 49);
            this.cbxAssResp.Name = "cbxAssResp";
            this.cbxAssResp.SelectedIndex = -1;
            this.cbxAssResp.SelectedItem = null;
            this.cbxAssResp.Size = new System.Drawing.Size(172, 33);
            this.cbxAssResp.TabIndex = 49;
            this.cbxAssResp.VisibleItems = 5;
            this.cbxAssResp.SelectedIndexChanged += new System.EventHandler(this.cbxAssResp_SelectedIndexChanged);
            // 
            // btnAddContention
            // 
            this.btnAddContention.Location = new System.Drawing.Point(3, 211);
            this.btnAddContention.Name = "btnAddContention";
            this.btnAddContention.Size = new System.Drawing.Size(151, 23);
            this.btnAddContention.TabIndex = 3;
            this.btnAddContention.Text = "Ajouter de la contention";
            this.btnAddContention.UseVisualStyleBackColor = true;
            this.btnAddContention.Click += new System.EventHandler(this.btnAddContention_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Ajouter une proposition";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddTraitement
            // 
            this.btnAddTraitement.Enabled = false;
            this.btnAddTraitement.Location = new System.Drawing.Point(3, 182);
            this.btnAddTraitement.Name = "btnAddTraitement";
            this.btnAddTraitement.Size = new System.Drawing.Size(151, 23);
            this.btnAddTraitement.TabIndex = 0;
            this.btnAddTraitement.Text = "Ajouter un traitement";
            this.btnAddTraitement.UseVisualStyleBackColor = true;
            this.btnAddTraitement.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(1115, 603);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 53);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Suivant";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Location = new System.Drawing.Point(12, 603);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 53);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "Précedent";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.Location = new System.Drawing.Point(93, 603);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(1016, 56);
            this.lvPPT.TabIndex = 10;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // FrmWizardClinique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1202, 662);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmWizardClinique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Definition du traitement clinique";
            this.Load += new System.EventHandler(this.FrmWizardClinique_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlLstBxDiag.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlDefinitionPlanTraitement.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlPlanTraitement.ResumeLayout(false);
            this.pnlPlanTraitement.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlLstBxDiag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Panel pnlDefinitionPlanTraitement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pnlPlanTraitement;
        private System.Windows.Forms.Button btnAddTraitement;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAddContention;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDebuTraitement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private ControlsLibrary.TreeViewIconCbx cbxSemEntames;
        private ControlsLibrary.TreeViewIconCbx cbxPratResp;
        private ControlsLibrary.TreeViewIconCbx cbxAssResp;
        private System.Windows.Forms.Button BtnShowRisques;
        private System.Windows.Forms.Button btnDEP;
        private System.Windows.Forms.Button btnSigne;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstBxPersonneAContacter;
        private System.Windows.Forms.Button button1;
        private BASEDiag.Ctrls.PropositionCtrlV2.PropositionCtrlV2 propositionCtrl1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnAddApp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem appareilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semestresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifierLeTarifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traitementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifierLeTarifDuSemestreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surveillanceToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnSaveAsModel;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstBxObjectifsDefinitifs;
        private System.Windows.Forms.ListBox lstBxAppareillages;
    }
}