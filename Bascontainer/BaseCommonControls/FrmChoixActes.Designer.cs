using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEPractice_BL;
//using BASEPractice_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using BasCommon_BO;
using BasCommon_BL;

namespace BaseCommonControls
{
    partial class FrmChoixActes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChoixActes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BTN_ADD = new System.Windows.Forms.Button();
            this.BTN_DELETE = new System.Windows.Forms.Button();
            this.TxtPrixTotal = new System.Windows.Forms.Label();
            this.LabelPrix = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.imgsStatus = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.Echeancier = new System.Windows.Forms.Button();
            this.btnRistourneGlobal = new System.Windows.Forms.Button();
            this.TxtPrixTotal_AvantRemise = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPrixAvR_Docteur = new System.Windows.Forms.Label();
            this.theBigCtrlDataGridViewPersonneColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewAutrePersonneColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.theBigCtrlDataGridViewDateColumnGrid1 = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.theBigCtrlDataGridViewActeColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.theBigCtrlDataGridViewActeSuppColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.theBigCtrlDataGridViewRadioColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.theBigCtrlDataGridViewPhotoColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.theBigCtrlDataGridViewMaterielColumn1 = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.theBigCtrlDataGridViewPersonneColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn6 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewAutrePersonneColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.theBigCtrlDataGridViewDateColumnGrid2 = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.theBigCtrlDataGridViewActeColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.theBigCtrlDataGridViewActeSuppColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.theBigCtrlDataGridViewRadioColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.theBigCtrlDataGridViewPhotoColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.theBigCtrlDataGridViewMaterielColumn2 = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.theBigCtrlDataGridViewPersonneColumn7 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn8 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn9 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewAutrePersonneColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.theBigCtrlDataGridViewDateColumnGrid3 = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.theBigCtrlDataGridViewActeColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.theBigCtrlDataGridViewActeSuppColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.theBigCtrlDataGridViewRadioColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.theBigCtrlDataGridViewPhotoColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.theBigCtrlDataGridViewMaterielColumn3 = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.theBigCtrlDataGridViewPersonneColumn10 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn11 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn12 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewAutrePersonneColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.theBigCtrlDataGridViewDateColumnGrid4 = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.theBigCtrlDataGridViewActeColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.theBigCtrlDataGridViewActeSuppColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.theBigCtrlDataGridViewRadioColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.theBigCtrlDataGridViewPhotoColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.theBigCtrlDataGridViewMaterielColumn4 = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.theBigCtrlDataGridViewPersonneColumn13 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn14 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewPersonneColumn15 = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.theBigCtrlDataGridViewAutrePersonneColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.theBigCtrlDataGridViewDateColumnGrid5 = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.theBigCtrlDataGridViewActeColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theBigCtrlDataGridViewActeSuppColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.theBigCtrlDataGridViewRadioColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.theBigCtrlDataGridViewPhotoColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.theBigCtrlDataGridViewMaterielColumn5 = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIM = new BaseCommonControls.TheBigCtrlDataGridViewTIMColumn();
            this.AppH = new BaseCommonControls.TheBigCtrlDataGridViewAppareilHautColumn();
            this.AccH = new BaseCommonControls.TheBigCtrlDataGridViewAccessoiresHautColumn();
            this.AppB = new BaseCommonControls.TheBigCtrlDataGridViewAppareilBasColumn();
            this.AccB = new BaseCommonControls.TheBigCtrlDataGridViewAccessoiresBasColumn();
            this.Extr = new BaseCommonControls.TheBigCtrlDataGridViewExtractionColumn();
            this.Hyg = new BaseCommonControls.TheBigCtrlDataGridViewHygieneColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAddRdv = new System.Windows.Forms.Button();
            this.btnGrpActes_Ok = new System.Windows.Forms.Button();
            this.Manual = new System.Windows.Forms.DataGridViewImageColumn();
            this.colActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPrat = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.colAss = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.colSec = new BaseCommonControls.TheBigCtrlDataGridViewPersonneColumn();
            this.Autre = new BaseCommonControls.TheBigCtrlDataGridViewAutrePersonneColumn();
            this.Date = new BaseCommonControls.TheBigCtrlDataGridViewDateColumnGrid();
            this.colDent = new BaseCommonControls.TheBigCtrlDataGridViewDentsColumn();
            this.Acte = new BaseCommonControls.TheBigCtrlDataGridViewActeColumn();
            this.Libellé = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActeSupp = new BaseCommonControls.TheBigCtrlDataGridViewActeSuppColumn();
            this.Radios = new BaseCommonControls.TheBigCtrlDataGridViewRadioColumn();
            this.Photos = new BaseCommonControls.TheBigCtrlDataGridViewPhotoColumn();
            this.Mat = new BaseCommonControls.TheBigCtrlDataGridViewMaterielColumn();
            this.Prix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Réduction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepartition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Echéance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_ADD
            // 
            this.BTN_ADD.Location = new System.Drawing.Point(13, 85);
            this.BTN_ADD.Name = "BTN_ADD";
            this.BTN_ADD.Size = new System.Drawing.Size(75, 45);
            this.BTN_ADD.TabIndex = 1;
            this.BTN_ADD.Text = "Ajouter un acte";
            this.BTN_ADD.UseVisualStyleBackColor = true;
            this.BTN_ADD.Click += new System.EventHandler(this.BTN_ADD_Click);
            // 
            // BTN_DELETE
            // 
            this.BTN_DELETE.Location = new System.Drawing.Point(13, 134);
            this.BTN_DELETE.Name = "BTN_DELETE";
            this.BTN_DELETE.Size = new System.Drawing.Size(75, 45);
            this.BTN_DELETE.TabIndex = 2;
            this.BTN_DELETE.Text = "Supprimer un acte";
            this.BTN_DELETE.UseVisualStyleBackColor = true;
            this.BTN_DELETE.Click += new System.EventHandler(this.BTN_DELETE_Click);
            // 
            // TxtPrixTotal
            // 
            this.TxtPrixTotal.AutoSize = true;
            this.TxtPrixTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrixTotal.Location = new System.Drawing.Point(875, 237);
            this.TxtPrixTotal.Name = "TxtPrixTotal";
            this.TxtPrixTotal.Size = new System.Drawing.Size(59, 20);
            this.TxtPrixTotal.TabIndex = 8;
            this.TxtPrixTotal.Text = "ddddd";
            this.TxtPrixTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelPrix
            // 
            this.LabelPrix.AutoSize = true;
            this.LabelPrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPrix.Location = new System.Drawing.Point(820, 237);
            this.LabelPrix.Name = "LabelPrix";
            this.LabelPrix.Size = new System.Drawing.Size(49, 20);
            this.LabelPrix.TabIndex = 7;
            this.LabelPrix.Text = "Total";
            this.LabelPrix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Manual,
            this.colActive,
            this.colPrat,
            this.colAss,
            this.colSec,
            this.Autre,
            this.Date,
            this.colDent,
            this.Acte,
            this.Libellé,
            this.ActeSupp,
            this.Radios,
            this.Photos,
            this.Mat,
            this.Prix,
            this.Réduction,
            this.colRepartition,
            this.Echéance});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(91, 35);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1222, 192);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridView1_CellStateChanged);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // imgsStatus
            // 
            this.imgsStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgsStatus.ImageStream")));
            this.imgsStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imgsStatus.Images.SetKeyName(0, "emptylarge.png");
            this.imgsStatus.Images.SetKeyName(1, "pluslarge.png");
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(1245, 266);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 58);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "Enregistrer";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Echeancier
            // 
            this.Echeancier.Font = new System.Drawing.Font("Garamond", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Echeancier.Location = new System.Drawing.Point(989, 232);
            this.Echeancier.Name = "Echeancier";
            this.Echeancier.Size = new System.Drawing.Size(88, 30);
            this.Echeancier.TabIndex = 24;
            this.Echeancier.Text = "Echéancier";
            this.Echeancier.UseVisualStyleBackColor = true;
            this.Echeancier.Click += new System.EventHandler(this.Echeancier_Click);
            // 
            // btnRistourneGlobal
            // 
            this.btnRistourneGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRistourneGlobal.Location = new System.Drawing.Point(1127, 232);
            this.btnRistourneGlobal.Name = "btnRistourneGlobal";
            this.btnRistourneGlobal.Size = new System.Drawing.Size(32, 30);
            this.btnRistourneGlobal.TabIndex = 25;
            this.btnRistourneGlobal.Text = "%";
            this.btnRistourneGlobal.UseVisualStyleBackColor = true;
            this.btnRistourneGlobal.Click += new System.EventHandler(this.btnRistourneGlobal_Click);
            // 
            // TxtPrixTotal_AvantRemise
            // 
            this.TxtPrixTotal_AvantRemise.AutoSize = true;
            this.TxtPrixTotal_AvantRemise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrixTotal_AvantRemise.Location = new System.Drawing.Point(831, 285);
            this.TxtPrixTotal_AvantRemise.Name = "TxtPrixTotal_AvantRemise";
            this.TxtPrixTotal_AvantRemise.Size = new System.Drawing.Size(0, 20);
            this.TxtPrixTotal_AvantRemise.TabIndex = 26;
            this.TxtPrixTotal_AvantRemise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TxtPrixTotal_AvantRemise.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(649, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Total avant réduction";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(623, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "Avant réduction Docteur";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtPrixAvR_Docteur
            // 
            this.TxtPrixAvR_Docteur.AutoSize = true;
            this.TxtPrixAvR_Docteur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrixAvR_Docteur.Location = new System.Drawing.Point(833, 266);
            this.TxtPrixAvR_Docteur.Name = "TxtPrixAvR_Docteur";
            this.TxtPrixAvR_Docteur.Size = new System.Drawing.Size(0, 20);
            this.TxtPrixAvR_Docteur.TabIndex = 28;
            this.TxtPrixAvR_Docteur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // theBigCtrlDataGridViewPersonneColumn1
            // 
            this.theBigCtrlDataGridViewPersonneColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn1.HeaderText = "Prat";
            this.theBigCtrlDataGridViewPersonneColumn1.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn1.Name = "theBigCtrlDataGridViewPersonneColumn1";
            this.theBigCtrlDataGridViewPersonneColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn1.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn2
            // 
            this.theBigCtrlDataGridViewPersonneColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn2.HeaderText = "Ass";
            this.theBigCtrlDataGridViewPersonneColumn2.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn2.Name = "theBigCtrlDataGridViewPersonneColumn2";
            this.theBigCtrlDataGridViewPersonneColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn2.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn3
            // 
            this.theBigCtrlDataGridViewPersonneColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn3.HeaderText = "Sec";
            this.theBigCtrlDataGridViewPersonneColumn3.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn3.Name = "theBigCtrlDataGridViewPersonneColumn3";
            this.theBigCtrlDataGridViewPersonneColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn3.Width = 30;
            // 
            // theBigCtrlDataGridViewAutrePersonneColumn1
            // 
            this.theBigCtrlDataGridViewAutrePersonneColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewAutrePersonneColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewAutrePersonneColumn1.HeaderText = "Autre";
            this.theBigCtrlDataGridViewAutrePersonneColumn1.Name = "theBigCtrlDataGridViewAutrePersonneColumn1";
            this.theBigCtrlDataGridViewAutrePersonneColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewAutrePersonneColumn1.Visible = false;
            this.theBigCtrlDataGridViewAutrePersonneColumn1.Width = 30;
            // 
            // theBigCtrlDataGridViewDateColumnGrid1
            // 
            this.theBigCtrlDataGridViewDateColumnGrid1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewDateColumnGrid1.FillWeight = 80F;
            this.theBigCtrlDataGridViewDateColumnGrid1.HeaderText = "Date";
            this.theBigCtrlDataGridViewDateColumnGrid1.Name = "theBigCtrlDataGridViewDateColumnGrid1";
            this.theBigCtrlDataGridViewDateColumnGrid1.ReadOnly = true;
            this.theBigCtrlDataGridViewDateColumnGrid1.Width = 80;
            // 
            // theBigCtrlDataGridViewActeColumn1
            // 
            this.theBigCtrlDataGridViewActeColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeColumn1.HeaderText = "Acte";
            this.theBigCtrlDataGridViewActeColumn1.Name = "theBigCtrlDataGridViewActeColumn1";
            this.theBigCtrlDataGridViewActeColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewActeColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewActeColumn1.Width = 50;
            // 
            // theBigCtrlDataGridViewActeSuppColumn1
            // 
            this.theBigCtrlDataGridViewActeSuppColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeSuppColumn1.HeaderText = "Acte Supp";
            this.theBigCtrlDataGridViewActeSuppColumn1.Name = "theBigCtrlDataGridViewActeSuppColumn1";
            this.theBigCtrlDataGridViewActeSuppColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewActeSuppColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // theBigCtrlDataGridViewRadioColumn1
            // 
            this.theBigCtrlDataGridViewRadioColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewRadioColumn1.HeaderText = "Radios";
            this.theBigCtrlDataGridViewRadioColumn1.Name = "theBigCtrlDataGridViewRadioColumn1";
            this.theBigCtrlDataGridViewRadioColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewRadioColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewRadioColumn1.Width = 90;
            // 
            // theBigCtrlDataGridViewPhotoColumn1
            // 
            this.theBigCtrlDataGridViewPhotoColumn1.FillWeight = 30F;
            this.theBigCtrlDataGridViewPhotoColumn1.HeaderText = "Photos";
            this.theBigCtrlDataGridViewPhotoColumn1.Name = "theBigCtrlDataGridViewPhotoColumn1";
            this.theBigCtrlDataGridViewPhotoColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewPhotoColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewPhotoColumn1.Width = 90;
            // 
            // theBigCtrlDataGridViewMaterielColumn1
            // 
            this.theBigCtrlDataGridViewMaterielColumn1.FillWeight = 50F;
            this.theBigCtrlDataGridViewMaterielColumn1.HeaderText = "Mat";
            this.theBigCtrlDataGridViewMaterielColumn1.Name = "theBigCtrlDataGridViewMaterielColumn1";
            this.theBigCtrlDataGridViewMaterielColumn1.ReadOnly = true;
            this.theBigCtrlDataGridViewMaterielColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewMaterielColumn1.Width = 50;
            // 
            // theBigCtrlDataGridViewPersonneColumn4
            // 
            this.theBigCtrlDataGridViewPersonneColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn4.HeaderText = "Prat";
            this.theBigCtrlDataGridViewPersonneColumn4.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn4.Name = "theBigCtrlDataGridViewPersonneColumn4";
            this.theBigCtrlDataGridViewPersonneColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn4.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn5
            // 
            this.theBigCtrlDataGridViewPersonneColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn5.HeaderText = "Ass";
            this.theBigCtrlDataGridViewPersonneColumn5.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn5.Name = "theBigCtrlDataGridViewPersonneColumn5";
            this.theBigCtrlDataGridViewPersonneColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn5.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn6
            // 
            this.theBigCtrlDataGridViewPersonneColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn6.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn6.HeaderText = "Sec";
            this.theBigCtrlDataGridViewPersonneColumn6.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn6.Name = "theBigCtrlDataGridViewPersonneColumn6";
            this.theBigCtrlDataGridViewPersonneColumn6.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn6.Width = 30;
            // 
            // theBigCtrlDataGridViewAutrePersonneColumn2
            // 
            this.theBigCtrlDataGridViewAutrePersonneColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewAutrePersonneColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewAutrePersonneColumn2.HeaderText = "Autre";
            this.theBigCtrlDataGridViewAutrePersonneColumn2.Name = "theBigCtrlDataGridViewAutrePersonneColumn2";
            this.theBigCtrlDataGridViewAutrePersonneColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewAutrePersonneColumn2.Visible = false;
            this.theBigCtrlDataGridViewAutrePersonneColumn2.Width = 30;
            // 
            // theBigCtrlDataGridViewDateColumnGrid2
            // 
            this.theBigCtrlDataGridViewDateColumnGrid2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewDateColumnGrid2.FillWeight = 80F;
            this.theBigCtrlDataGridViewDateColumnGrid2.HeaderText = "Date";
            this.theBigCtrlDataGridViewDateColumnGrid2.Name = "theBigCtrlDataGridViewDateColumnGrid2";
            this.theBigCtrlDataGridViewDateColumnGrid2.ReadOnly = true;
            this.theBigCtrlDataGridViewDateColumnGrid2.Width = 80;
            // 
            // theBigCtrlDataGridViewActeColumn2
            // 
            this.theBigCtrlDataGridViewActeColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeColumn2.HeaderText = "Acte";
            this.theBigCtrlDataGridViewActeColumn2.Name = "theBigCtrlDataGridViewActeColumn2";
            this.theBigCtrlDataGridViewActeColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewActeColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewActeColumn2.Width = 50;
            // 
            // theBigCtrlDataGridViewActeSuppColumn2
            // 
            this.theBigCtrlDataGridViewActeSuppColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeSuppColumn2.HeaderText = "Acte Supp";
            this.theBigCtrlDataGridViewActeSuppColumn2.Name = "theBigCtrlDataGridViewActeSuppColumn2";
            this.theBigCtrlDataGridViewActeSuppColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewActeSuppColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // theBigCtrlDataGridViewRadioColumn2
            // 
            this.theBigCtrlDataGridViewRadioColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewRadioColumn2.HeaderText = "Radios";
            this.theBigCtrlDataGridViewRadioColumn2.Name = "theBigCtrlDataGridViewRadioColumn2";
            this.theBigCtrlDataGridViewRadioColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewRadioColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewRadioColumn2.Width = 90;
            // 
            // theBigCtrlDataGridViewPhotoColumn2
            // 
            this.theBigCtrlDataGridViewPhotoColumn2.FillWeight = 30F;
            this.theBigCtrlDataGridViewPhotoColumn2.HeaderText = "Photos";
            this.theBigCtrlDataGridViewPhotoColumn2.Name = "theBigCtrlDataGridViewPhotoColumn2";
            this.theBigCtrlDataGridViewPhotoColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewPhotoColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewPhotoColumn2.Width = 90;
            // 
            // theBigCtrlDataGridViewMaterielColumn2
            // 
            this.theBigCtrlDataGridViewMaterielColumn2.FillWeight = 50F;
            this.theBigCtrlDataGridViewMaterielColumn2.HeaderText = "Mat";
            this.theBigCtrlDataGridViewMaterielColumn2.Name = "theBigCtrlDataGridViewMaterielColumn2";
            this.theBigCtrlDataGridViewMaterielColumn2.ReadOnly = true;
            this.theBigCtrlDataGridViewMaterielColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewMaterielColumn2.Width = 50;
            // 
            // theBigCtrlDataGridViewPersonneColumn7
            // 
            this.theBigCtrlDataGridViewPersonneColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn7.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn7.HeaderText = "Prat";
            this.theBigCtrlDataGridViewPersonneColumn7.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn7.Name = "theBigCtrlDataGridViewPersonneColumn7";
            this.theBigCtrlDataGridViewPersonneColumn7.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn7.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn8
            // 
            this.theBigCtrlDataGridViewPersonneColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn8.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn8.HeaderText = "Ass";
            this.theBigCtrlDataGridViewPersonneColumn8.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn8.Name = "theBigCtrlDataGridViewPersonneColumn8";
            this.theBigCtrlDataGridViewPersonneColumn8.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn8.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn9
            // 
            this.theBigCtrlDataGridViewPersonneColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn9.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn9.HeaderText = "Sec";
            this.theBigCtrlDataGridViewPersonneColumn9.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn9.Name = "theBigCtrlDataGridViewPersonneColumn9";
            this.theBigCtrlDataGridViewPersonneColumn9.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn9.Width = 30;
            // 
            // theBigCtrlDataGridViewAutrePersonneColumn3
            // 
            this.theBigCtrlDataGridViewAutrePersonneColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewAutrePersonneColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewAutrePersonneColumn3.HeaderText = "Autre";
            this.theBigCtrlDataGridViewAutrePersonneColumn3.Name = "theBigCtrlDataGridViewAutrePersonneColumn3";
            this.theBigCtrlDataGridViewAutrePersonneColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewAutrePersonneColumn3.Visible = false;
            this.theBigCtrlDataGridViewAutrePersonneColumn3.Width = 30;
            // 
            // theBigCtrlDataGridViewDateColumnGrid3
            // 
            this.theBigCtrlDataGridViewDateColumnGrid3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewDateColumnGrid3.FillWeight = 80F;
            this.theBigCtrlDataGridViewDateColumnGrid3.HeaderText = "Date";
            this.theBigCtrlDataGridViewDateColumnGrid3.Name = "theBigCtrlDataGridViewDateColumnGrid3";
            this.theBigCtrlDataGridViewDateColumnGrid3.ReadOnly = true;
            this.theBigCtrlDataGridViewDateColumnGrid3.Width = 80;
            // 
            // theBigCtrlDataGridViewActeColumn3
            // 
            this.theBigCtrlDataGridViewActeColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeColumn3.HeaderText = "Acte";
            this.theBigCtrlDataGridViewActeColumn3.Name = "theBigCtrlDataGridViewActeColumn3";
            this.theBigCtrlDataGridViewActeColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewActeColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewActeColumn3.Width = 50;
            // 
            // theBigCtrlDataGridViewActeSuppColumn3
            // 
            this.theBigCtrlDataGridViewActeSuppColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeSuppColumn3.HeaderText = "Acte Supp";
            this.theBigCtrlDataGridViewActeSuppColumn3.Name = "theBigCtrlDataGridViewActeSuppColumn3";
            this.theBigCtrlDataGridViewActeSuppColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewActeSuppColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // theBigCtrlDataGridViewRadioColumn3
            // 
            this.theBigCtrlDataGridViewRadioColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewRadioColumn3.HeaderText = "Radios";
            this.theBigCtrlDataGridViewRadioColumn3.Name = "theBigCtrlDataGridViewRadioColumn3";
            this.theBigCtrlDataGridViewRadioColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewRadioColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewRadioColumn3.Width = 90;
            // 
            // theBigCtrlDataGridViewPhotoColumn3
            // 
            this.theBigCtrlDataGridViewPhotoColumn3.FillWeight = 30F;
            this.theBigCtrlDataGridViewPhotoColumn3.HeaderText = "Photos";
            this.theBigCtrlDataGridViewPhotoColumn3.Name = "theBigCtrlDataGridViewPhotoColumn3";
            this.theBigCtrlDataGridViewPhotoColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewPhotoColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewPhotoColumn3.Width = 90;
            // 
            // theBigCtrlDataGridViewMaterielColumn3
            // 
            this.theBigCtrlDataGridViewMaterielColumn3.FillWeight = 50F;
            this.theBigCtrlDataGridViewMaterielColumn3.HeaderText = "Mat";
            this.theBigCtrlDataGridViewMaterielColumn3.Name = "theBigCtrlDataGridViewMaterielColumn3";
            this.theBigCtrlDataGridViewMaterielColumn3.ReadOnly = true;
            this.theBigCtrlDataGridViewMaterielColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewMaterielColumn3.Width = 50;
            // 
            // theBigCtrlDataGridViewPersonneColumn10
            // 
            this.theBigCtrlDataGridViewPersonneColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn10.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn10.HeaderText = "Prat";
            this.theBigCtrlDataGridViewPersonneColumn10.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn10.Name = "theBigCtrlDataGridViewPersonneColumn10";
            this.theBigCtrlDataGridViewPersonneColumn10.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn10.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn11
            // 
            this.theBigCtrlDataGridViewPersonneColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn11.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn11.HeaderText = "Ass";
            this.theBigCtrlDataGridViewPersonneColumn11.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn11.Name = "theBigCtrlDataGridViewPersonneColumn11";
            this.theBigCtrlDataGridViewPersonneColumn11.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn11.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn12
            // 
            this.theBigCtrlDataGridViewPersonneColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn12.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn12.HeaderText = "Sec";
            this.theBigCtrlDataGridViewPersonneColumn12.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn12.Name = "theBigCtrlDataGridViewPersonneColumn12";
            this.theBigCtrlDataGridViewPersonneColumn12.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn12.Width = 30;
            // 
            // theBigCtrlDataGridViewAutrePersonneColumn4
            // 
            this.theBigCtrlDataGridViewAutrePersonneColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewAutrePersonneColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewAutrePersonneColumn4.HeaderText = "Autre";
            this.theBigCtrlDataGridViewAutrePersonneColumn4.Name = "theBigCtrlDataGridViewAutrePersonneColumn4";
            this.theBigCtrlDataGridViewAutrePersonneColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewAutrePersonneColumn4.Visible = false;
            this.theBigCtrlDataGridViewAutrePersonneColumn4.Width = 30;
            // 
            // theBigCtrlDataGridViewDateColumnGrid4
            // 
            this.theBigCtrlDataGridViewDateColumnGrid4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewDateColumnGrid4.FillWeight = 80F;
            this.theBigCtrlDataGridViewDateColumnGrid4.HeaderText = "Date";
            this.theBigCtrlDataGridViewDateColumnGrid4.Name = "theBigCtrlDataGridViewDateColumnGrid4";
            this.theBigCtrlDataGridViewDateColumnGrid4.ReadOnly = true;
            this.theBigCtrlDataGridViewDateColumnGrid4.Width = 80;
            // 
            // theBigCtrlDataGridViewActeColumn4
            // 
            this.theBigCtrlDataGridViewActeColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeColumn4.HeaderText = "Acte";
            this.theBigCtrlDataGridViewActeColumn4.Name = "theBigCtrlDataGridViewActeColumn4";
            this.theBigCtrlDataGridViewActeColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewActeColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewActeColumn4.Width = 50;
            // 
            // theBigCtrlDataGridViewActeSuppColumn4
            // 
            this.theBigCtrlDataGridViewActeSuppColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeSuppColumn4.HeaderText = "Acte Supp";
            this.theBigCtrlDataGridViewActeSuppColumn4.Name = "theBigCtrlDataGridViewActeSuppColumn4";
            this.theBigCtrlDataGridViewActeSuppColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewActeSuppColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // theBigCtrlDataGridViewRadioColumn4
            // 
            this.theBigCtrlDataGridViewRadioColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewRadioColumn4.HeaderText = "Radios";
            this.theBigCtrlDataGridViewRadioColumn4.Name = "theBigCtrlDataGridViewRadioColumn4";
            this.theBigCtrlDataGridViewRadioColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewRadioColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewRadioColumn4.Width = 90;
            // 
            // theBigCtrlDataGridViewPhotoColumn4
            // 
            this.theBigCtrlDataGridViewPhotoColumn4.FillWeight = 30F;
            this.theBigCtrlDataGridViewPhotoColumn4.HeaderText = "Photos";
            this.theBigCtrlDataGridViewPhotoColumn4.Name = "theBigCtrlDataGridViewPhotoColumn4";
            this.theBigCtrlDataGridViewPhotoColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewPhotoColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewPhotoColumn4.Width = 90;
            // 
            // theBigCtrlDataGridViewMaterielColumn4
            // 
            this.theBigCtrlDataGridViewMaterielColumn4.FillWeight = 50F;
            this.theBigCtrlDataGridViewMaterielColumn4.HeaderText = "Mat";
            this.theBigCtrlDataGridViewMaterielColumn4.Name = "theBigCtrlDataGridViewMaterielColumn4";
            this.theBigCtrlDataGridViewMaterielColumn4.ReadOnly = true;
            this.theBigCtrlDataGridViewMaterielColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewMaterielColumn4.Width = 50;
            // 
            // theBigCtrlDataGridViewPersonneColumn13
            // 
            this.theBigCtrlDataGridViewPersonneColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn13.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn13.HeaderText = "Prat";
            this.theBigCtrlDataGridViewPersonneColumn13.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn13.Name = "theBigCtrlDataGridViewPersonneColumn13";
            this.theBigCtrlDataGridViewPersonneColumn13.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn13.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn14
            // 
            this.theBigCtrlDataGridViewPersonneColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn14.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn14.HeaderText = "Ass";
            this.theBigCtrlDataGridViewPersonneColumn14.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn14.Name = "theBigCtrlDataGridViewPersonneColumn14";
            this.theBigCtrlDataGridViewPersonneColumn14.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn14.Width = 30;
            // 
            // theBigCtrlDataGridViewPersonneColumn15
            // 
            this.theBigCtrlDataGridViewPersonneColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewPersonneColumn15.FillWeight = 30F;
            this.theBigCtrlDataGridViewPersonneColumn15.HeaderText = "Sec";
            this.theBigCtrlDataGridViewPersonneColumn15.infocomplementaire = null;
            this.theBigCtrlDataGridViewPersonneColumn15.Name = "theBigCtrlDataGridViewPersonneColumn15";
            this.theBigCtrlDataGridViewPersonneColumn15.ReadOnly = true;
            this.theBigCtrlDataGridViewPersonneColumn15.Width = 30;
            // 
            // theBigCtrlDataGridViewAutrePersonneColumn5
            // 
            this.theBigCtrlDataGridViewAutrePersonneColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewAutrePersonneColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewAutrePersonneColumn5.HeaderText = "Autre";
            this.theBigCtrlDataGridViewAutrePersonneColumn5.Name = "theBigCtrlDataGridViewAutrePersonneColumn5";
            this.theBigCtrlDataGridViewAutrePersonneColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewAutrePersonneColumn5.Visible = false;
            this.theBigCtrlDataGridViewAutrePersonneColumn5.Width = 30;
            // 
            // theBigCtrlDataGridViewDateColumnGrid5
            // 
            this.theBigCtrlDataGridViewDateColumnGrid5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.theBigCtrlDataGridViewDateColumnGrid5.FillWeight = 80F;
            this.theBigCtrlDataGridViewDateColumnGrid5.HeaderText = "Date";
            this.theBigCtrlDataGridViewDateColumnGrid5.Name = "theBigCtrlDataGridViewDateColumnGrid5";
            this.theBigCtrlDataGridViewDateColumnGrid5.ReadOnly = true;
            this.theBigCtrlDataGridViewDateColumnGrid5.Width = 80;
            // 
            // theBigCtrlDataGridViewActeColumn5
            // 
            this.theBigCtrlDataGridViewActeColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeColumn5.HeaderText = "Acte";
            this.theBigCtrlDataGridViewActeColumn5.Name = "theBigCtrlDataGridViewActeColumn5";
            this.theBigCtrlDataGridViewActeColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewActeColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewActeColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Acte";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 170;
            // 
            // theBigCtrlDataGridViewActeSuppColumn5
            // 
            this.theBigCtrlDataGridViewActeSuppColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewActeSuppColumn5.HeaderText = "Acte Supp";
            this.theBigCtrlDataGridViewActeSuppColumn5.Name = "theBigCtrlDataGridViewActeSuppColumn5";
            this.theBigCtrlDataGridViewActeSuppColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewActeSuppColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // theBigCtrlDataGridViewRadioColumn5
            // 
            this.theBigCtrlDataGridViewRadioColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewRadioColumn5.HeaderText = "Radios";
            this.theBigCtrlDataGridViewRadioColumn5.Name = "theBigCtrlDataGridViewRadioColumn5";
            this.theBigCtrlDataGridViewRadioColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewRadioColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewRadioColumn5.Width = 90;
            // 
            // theBigCtrlDataGridViewPhotoColumn5
            // 
            this.theBigCtrlDataGridViewPhotoColumn5.FillWeight = 30F;
            this.theBigCtrlDataGridViewPhotoColumn5.HeaderText = "Photos";
            this.theBigCtrlDataGridViewPhotoColumn5.Name = "theBigCtrlDataGridViewPhotoColumn5";
            this.theBigCtrlDataGridViewPhotoColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewPhotoColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewPhotoColumn5.Width = 90;
            // 
            // theBigCtrlDataGridViewMaterielColumn5
            // 
            this.theBigCtrlDataGridViewMaterielColumn5.FillWeight = 50F;
            this.theBigCtrlDataGridViewMaterielColumn5.HeaderText = "Mat";
            this.theBigCtrlDataGridViewMaterielColumn5.Name = "theBigCtrlDataGridViewMaterielColumn5";
            this.theBigCtrlDataGridViewMaterielColumn5.ReadOnly = true;
            this.theBigCtrlDataGridViewMaterielColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.theBigCtrlDataGridViewMaterielColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.HeaderText = "Prix";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.HeaderText = "Réduction";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn4.HeaderText = "Echéance";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn5.HeaderText = "Echéance";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // TIM
            // 
            this.TIM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TIM.FillWeight = 50F;
            this.TIM.HeaderText = "TIM";
            this.TIM.Name = "TIM";
            this.TIM.ReadOnly = true;
            this.TIM.Width = 50;
            // 
            // AppH
            // 
            this.AppH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AppH.FillWeight = 50F;
            this.AppH.HeaderText = "AppH";
            this.AppH.Name = "AppH";
            this.AppH.ReadOnly = true;
            this.AppH.Width = 50;
            // 
            // AccH
            // 
            this.AccH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AccH.FillWeight = 50F;
            this.AccH.HeaderText = "AccH";
            this.AccH.Name = "AccH";
            this.AccH.ReadOnly = true;
            this.AccH.Width = 50;
            // 
            // AppB
            // 
            this.AppB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AppB.FillWeight = 50F;
            this.AppB.HeaderText = "AppB";
            this.AppB.Name = "AppB";
            this.AppB.ReadOnly = true;
            this.AppB.Width = 50;
            // 
            // AccB
            // 
            this.AccB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AccB.FillWeight = 50F;
            this.AccB.HeaderText = "AccB";
            this.AccB.Name = "AccB";
            this.AccB.ReadOnly = true;
            this.AccB.Width = 50;
            // 
            // Extr
            // 
            this.Extr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Extr.FillWeight = 50F;
            this.Extr.HeaderText = "Extr";
            this.Extr.Name = "Extr";
            this.Extr.ReadOnly = true;
            this.Extr.Width = 50;
            // 
            // Hyg
            // 
            this.Hyg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Hyg.FillWeight = 50F;
            this.Hyg.HeaderText = "Hyg";
            this.Hyg.Name = "Hyg";
            this.Hyg.ReadOnly = true;
            this.Hyg.Width = 50;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAddRdv
            // 
            this.btnAddRdv.Location = new System.Drawing.Point(12, 185);
            this.btnAddRdv.Name = "btnAddRdv";
            this.btnAddRdv.Size = new System.Drawing.Size(75, 45);
            this.btnAddRdv.TabIndex = 30;
            this.btnAddRdv.Text = "Ajouter un rendez-vous";
            this.btnAddRdv.UseVisualStyleBackColor = true;
            this.btnAddRdv.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btnGrpActes_Ok
            // 
            this.btnGrpActes_Ok.Location = new System.Drawing.Point(12, 37);
            this.btnGrpActes_Ok.Name = "btnGrpActes_Ok";
            this.btnGrpActes_Ok.Size = new System.Drawing.Size(75, 45);
            this.btnGrpActes_Ok.TabIndex = 31;
            this.btnGrpActes_Ok.Text = "Ajouter un grp d\'actes";
            this.btnGrpActes_Ok.UseVisualStyleBackColor = true;
            this.btnGrpActes_Ok.Click += new System.EventHandler(this.btnGrpActes_Ok_Click);
            // 
            // Manual
            // 
            this.Manual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Manual.FillWeight = 5F;
            this.Manual.HeaderText = "";
            this.Manual.Name = "Manual";
            this.Manual.Width = 20;
            // 
            // colActive
            // 
            this.colActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colActive.HeaderText = "";
            this.colActive.Name = "colActive";
            this.colActive.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colActive.Width = 20;
            // 
            // colPrat
            // 
            this.colPrat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPrat.FillWeight = 30F;
            this.colPrat.HeaderText = "Prat";
            this.colPrat.infocomplementaire = null;
            this.colPrat.Name = "colPrat";
            this.colPrat.ReadOnly = true;
            this.colPrat.Width = 30;
            // 
            // colAss
            // 
            this.colAss.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAss.FillWeight = 30F;
            this.colAss.HeaderText = "Ass";
            this.colAss.infocomplementaire = null;
            this.colAss.Name = "colAss";
            this.colAss.ReadOnly = true;
            this.colAss.Width = 30;
            // 
            // colSec
            // 
            this.colSec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSec.FillWeight = 30F;
            this.colSec.HeaderText = "Sec";
            this.colSec.infocomplementaire = null;
            this.colSec.Name = "colSec";
            this.colSec.ReadOnly = true;
            this.colSec.Width = 30;
            // 
            // Autre
            // 
            this.Autre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Autre.FillWeight = 30F;
            this.Autre.HeaderText = "Autre";
            this.Autre.Name = "Autre";
            this.Autre.ReadOnly = true;
            this.Autre.Visible = false;
            this.Autre.Width = 30;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Date.FillWeight = 80F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 80;
            // 
            // colDent
            // 
            this.colDent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colDent.HeaderText = "Dents";
            this.colDent.Name = "colDent";
            this.colDent.ReadOnly = true;
            this.colDent.Visible = false;
            this.colDent.Width = 41;
            // 
            // Acte
            // 
            this.Acte.FillWeight = 30F;
            this.Acte.HeaderText = "Acte";
            this.Acte.Name = "Acte";
            this.Acte.ReadOnly = true;
            this.Acte.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Acte.Width = 50;
            // 
            // Libellé
            // 
            this.Libellé.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Libellé.FillWeight = 30F;
            this.Libellé.HeaderText = "Acte";
            this.Libellé.Name = "Libellé";
            this.Libellé.ReadOnly = true;
            this.Libellé.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Libellé.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Libellé.Width = 170;
            // 
            // ActeSupp
            // 
            this.ActeSupp.FillWeight = 30F;
            this.ActeSupp.HeaderText = "Sous Actes";
            this.ActeSupp.Name = "ActeSupp";
            this.ActeSupp.ReadOnly = true;
            this.ActeSupp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Radios
            // 
            this.Radios.FillWeight = 30F;
            this.Radios.HeaderText = "Radios";
            this.Radios.Name = "Radios";
            this.Radios.ReadOnly = true;
            this.Radios.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Radios.Width = 90;
            // 
            // Photos
            // 
            this.Photos.FillWeight = 30F;
            this.Photos.HeaderText = "Photos";
            this.Photos.Name = "Photos";
            this.Photos.ReadOnly = true;
            this.Photos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Photos.Width = 90;
            // 
            // Mat
            // 
            this.Mat.FillWeight = 50F;
            this.Mat.HeaderText = "Mat";
            this.Mat.Name = "Mat";
            this.Mat.ReadOnly = true;
            this.Mat.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Mat.Width = 50;
            // 
            // Prix
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Prix.DefaultCellStyle = dataGridViewCellStyle2;
            this.Prix.HeaderText = "Prix";
            this.Prix.Name = "Prix";
            this.Prix.ReadOnly = true;
            this.Prix.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Prix.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Prix.Width = 50;
            // 
            // Réduction
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Réduction.DefaultCellStyle = dataGridViewCellStyle3;
            this.Réduction.HeaderText = "Réduction";
            this.Réduction.Name = "Réduction";
            this.Réduction.ReadOnly = true;
            this.Réduction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Réduction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Réduction.Width = 70;
            // 
            // colRepartition
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colRepartition.DefaultCellStyle = dataGridViewCellStyle4;
            this.colRepartition.HeaderText = "Répartition";
            this.colRepartition.Name = "colRepartition";
            this.colRepartition.ReadOnly = true;
            this.colRepartition.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRepartition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRepartition.Visible = false;
            // 
            // Echéance
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Echéance.DefaultCellStyle = dataGridViewCellStyle5;
            this.Echéance.HeaderText = "Echéance";
            this.Echéance.Name = "Echéance";
            this.Echéance.ReadOnly = true;
            this.Echéance.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Echéance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Echéance.Width = 70;
            // 
            // FrmChoixActes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1371, 334);
            this.Controls.Add(this.btnGrpActes_Ok);
            this.Controls.Add(this.btnAddRdv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtPrixAvR_Docteur);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtPrixTotal_AvantRemise);
            this.Controls.Add(this.btnRistourneGlobal);
            this.Controls.Add(this.Echeancier);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.TxtPrixTotal);
            this.Controls.Add(this.LabelPrix);
            this.Controls.Add(this.BTN_DELETE);
            this.Controls.Add(this.BTN_ADD);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmChoixActes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scénarios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmChoixActes_FormClosed);
            this.Load += new System.EventHandler(this.FrmChoixActes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;

        private TheBigCtrlDataGridViewTIMColumn colTIM;
        private TheBigCtrlDataGridViewAppareilHautColumn colAppH;
        private TheBigCtrlDataGridViewAccessoiresHautColumn colAccH;
        private TheBigCtrlDataGridViewAppareilBasColumn colAppB;
        private TheBigCtrlDataGridViewAccessoiresBasColumn colAccB;
        private TheBigCtrlDataGridViewExtractionColumn colExt;
        private TheBigCtrlDataGridViewHygieneColumn colHyg;


        private TheBigCtrlDataGridViewDateColumnGrid colDteRDV;
        private TheBigCtrlDataGridViewAutrePersonneColumn colAutre;
        private DataGridViewImageColumn colmanualmode;
      


        private System.Windows.Forms.Button BTN_ADD;
        private System.Windows.Forms.Button BTN_DELETE;
        private Label TxtPrixTotal;
        private Label LabelPrix;
        private DataGridViewTextBoxColumn Reduction;
        public ImageList imgsStatus;
        private TheBigCtrlDataGridViewTIMColumn TIM;
        private TheBigCtrlDataGridViewAppareilHautColumn AppH;
        private TheBigCtrlDataGridViewAccessoiresHautColumn AccH;
        private TheBigCtrlDataGridViewAppareilBasColumn AppB;
        private TheBigCtrlDataGridViewAccessoiresBasColumn AccB;
        private TheBigCtrlDataGridViewExtractionColumn Extr;
        private TheBigCtrlDataGridViewHygieneColumn Hyg;
        private Button btnOk;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn1;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn2;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn3;
        private TheBigCtrlDataGridViewAutrePersonneColumn theBigCtrlDataGridViewAutrePersonneColumn1;
        private TheBigCtrlDataGridViewDateColumnGrid theBigCtrlDataGridViewDateColumnGrid1;
        private TheBigCtrlDataGridViewActeColumn theBigCtrlDataGridViewActeColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private TheBigCtrlDataGridViewActeSuppColumn theBigCtrlDataGridViewActeSuppColumn1;
        private TheBigCtrlDataGridViewRadioColumn theBigCtrlDataGridViewRadioColumn1;
        private TheBigCtrlDataGridViewPhotoColumn theBigCtrlDataGridViewPhotoColumn1;
        private TheBigCtrlDataGridViewMaterielColumn theBigCtrlDataGridViewMaterielColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Button Echeancier;
        private Button btnRistourneGlobal;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label TxtPrixTotal_AvantRemise;
        private Label label1;
        private Label label2;
        private Label TxtPrixAvR_Docteur;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn4;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn5;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn6;
        private TheBigCtrlDataGridViewAutrePersonneColumn theBigCtrlDataGridViewAutrePersonneColumn2;
        private TheBigCtrlDataGridViewDateColumnGrid theBigCtrlDataGridViewDateColumnGrid2;
        private TheBigCtrlDataGridViewActeColumn theBigCtrlDataGridViewActeColumn2;
        private TheBigCtrlDataGridViewActeSuppColumn theBigCtrlDataGridViewActeSuppColumn2;
        private TheBigCtrlDataGridViewRadioColumn theBigCtrlDataGridViewRadioColumn2;
        private TheBigCtrlDataGridViewPhotoColumn theBigCtrlDataGridViewPhotoColumn2;
        private TheBigCtrlDataGridViewMaterielColumn theBigCtrlDataGridViewMaterielColumn2;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn7;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn8;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn9;
        private TheBigCtrlDataGridViewAutrePersonneColumn theBigCtrlDataGridViewAutrePersonneColumn3;
        private TheBigCtrlDataGridViewDateColumnGrid theBigCtrlDataGridViewDateColumnGrid3;
        private TheBigCtrlDataGridViewActeColumn theBigCtrlDataGridViewActeColumn3;
        private TheBigCtrlDataGridViewActeSuppColumn theBigCtrlDataGridViewActeSuppColumn3;
        private TheBigCtrlDataGridViewRadioColumn theBigCtrlDataGridViewRadioColumn3;
        private TheBigCtrlDataGridViewPhotoColumn theBigCtrlDataGridViewPhotoColumn3;
        private TheBigCtrlDataGridViewMaterielColumn theBigCtrlDataGridViewMaterielColumn3;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn10;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn11;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn12;
        private TheBigCtrlDataGridViewDentsColumn theBigCtrlDataGridViewDateColumnGrid7;

        private TheBigCtrlDataGridViewAutrePersonneColumn theBigCtrlDataGridViewAutrePersonneColumn4;
        private TheBigCtrlDataGridViewDateColumnGrid theBigCtrlDataGridViewDateColumnGrid4;
        private TheBigCtrlDataGridViewActeColumn theBigCtrlDataGridViewActeColumn4;
        private TheBigCtrlDataGridViewActeSuppColumn theBigCtrlDataGridViewActeSuppColumn4;
        private TheBigCtrlDataGridViewRadioColumn theBigCtrlDataGridViewRadioColumn4;
        private TheBigCtrlDataGridViewPhotoColumn theBigCtrlDataGridViewPhotoColumn4;
        private TheBigCtrlDataGridViewMaterielColumn theBigCtrlDataGridViewMaterielColumn4;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn13;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn14;
        private TheBigCtrlDataGridViewPersonneColumn theBigCtrlDataGridViewPersonneColumn15;
        private TheBigCtrlDataGridViewAutrePersonneColumn theBigCtrlDataGridViewAutrePersonneColumn5;
        private TheBigCtrlDataGridViewDateColumnGrid theBigCtrlDataGridViewDateColumnGrid5;
        private TheBigCtrlDataGridViewActeColumn theBigCtrlDataGridViewActeColumn5;
        private TheBigCtrlDataGridViewActeSuppColumn theBigCtrlDataGridViewActeSuppColumn5;
        private TheBigCtrlDataGridViewRadioColumn theBigCtrlDataGridViewRadioColumn5;
        private TheBigCtrlDataGridViewPhotoColumn theBigCtrlDataGridViewPhotoColumn5;
        private TheBigCtrlDataGridViewMaterielColumn theBigCtrlDataGridViewMaterielColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Timer timer1;
        private Button btnAddRdv;
        private Button btnGrpActes_Ok;
        private DataGridViewImageColumn Manual;
        private DataGridViewCheckBoxColumn colActive;
        private TheBigCtrlDataGridViewPersonneColumn colPrat;
        private TheBigCtrlDataGridViewPersonneColumn colAss;
        private TheBigCtrlDataGridViewPersonneColumn colSec;
        private TheBigCtrlDataGridViewAutrePersonneColumn Autre;
        private TheBigCtrlDataGridViewDateColumnGrid Date;
        private TheBigCtrlDataGridViewDentsColumn colDent;
        private TheBigCtrlDataGridViewActeColumn Acte;
        private DataGridViewTextBoxColumn Libellé;
        private TheBigCtrlDataGridViewActeSuppColumn ActeSupp;
        private TheBigCtrlDataGridViewActeSuppColumn1 ActeSupp1;
        private TheBigCtrlDataGridViewRadioColumn Radios;
        private TheBigCtrlDataGridViewPhotoColumn Photos;
        private TheBigCtrlDataGridViewMaterielColumn Mat;
        private DataGridViewTextBoxColumn Prix;
        private DataGridViewTextBoxColumn Réduction;
        private DataGridViewTextBoxColumn colRepartition;
        private DataGridViewTextBoxColumn Echéance;


    }
}