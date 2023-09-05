using BaseCommonControls.Ctrls;
namespace BaseCommonControls
{
    partial class FRmWizardNewRDV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRmWizardNewRDV));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("");
            this.finishStep1 = new WizardBase.FinishStep();
            this.txtbxSummary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.intermediateStep3 = new WizardBase.IntermediateStep();
            this.cbxResponsable = new BaseCommonControls.SlidingList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbOui = new System.Windows.Forms.RadioButton();
            this.rbNon = new System.Windows.Forms.RadioButton();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.treeviewActes = new BaseCommonControls.Ctrls.TreeViewIcon();
            this.slidinglstWhen = new BaseCommonControls.SlidingList();
            this.lblNbJoursDepuisDbut = new System.Windows.Forms.Label();
            this.startStep1 = new WizardBase.StartStep();
            this.wizardControl1 = new WizardBase.WizardControl();
            this.finishStep1.SuspendLayout();
            this.intermediateStep3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.startStep1.SuspendLayout();
            this.SuspendLayout();
            // 
            // finishStep1
            // 
            this.finishStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BindingImage")));
            this.finishStep1.Controls.Add(this.label3);
            this.finishStep1.Controls.Add(this.txtbxSummary);
            this.finishStep1.Name = "finishStep1";
            this.finishStep1.OnShow += new System.EventHandler(this.finishStep1_OnShow);
            // 
            // txtbxSummary
            // 
            this.txtbxSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbxSummary.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxSummary.Location = new System.Drawing.Point(77, 100);
            this.txtbxSummary.Multiline = true;
            this.txtbxSummary.Name = "txtbxSummary";
            this.txtbxSummary.Size = new System.Drawing.Size(467, 281);
            this.txtbxSummary.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Résumé";
            // 
            // intermediateStep3
            // 
            this.intermediateStep3.BackColor = System.Drawing.Color.White;
            this.intermediateStep3.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep3.BindingImage")));
            this.intermediateStep3.Controls.Add(this.panel1);
            this.intermediateStep3.Controls.Add(this.cbxResponsable);
            this.intermediateStep3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep3.Name = "intermediateStep3";
            this.intermediateStep3.Subtitle = "Un praticien spécifique est il a prevu pour cet acte ?";
            this.intermediateStep3.Title = "Praticien spécifique ?";
            this.intermediateStep3.Click += new System.EventHandler(this.intermediateStep3_Click);
            // 
            // cbxResponsable
            // 
            this.cbxResponsable.ButtonSize = new System.Drawing.SizeF(80F, 60F);
            this.cbxResponsable.imagelist = null;
            this.cbxResponsable.Location = new System.Drawing.Point(6, 207);
            this.cbxResponsable.MultiSelectMode = false;
            this.cbxResponsable.Name = "cbxResponsable";
            this.cbxResponsable.Size = new System.Drawing.Size(484, 73);
            this.cbxResponsable.TabIndex = 0;
            this.cbxResponsable.Visible = false;
            this.cbxResponsable.WrapMode = false;
            this.cbxResponsable.ClickNode += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.cbxResponsable_ClickNode);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbNon);
            this.panel1.Controls.Add(this.rbOui);
            this.panel1.Location = new System.Drawing.Point(107, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 100);
            this.panel1.TabIndex = 1;
            // 
            // rbOui
            // 
            this.rbOui.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbOui.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rbOui.FlatAppearance.BorderSize = 0;
            this.rbOui.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbOui.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.rbOui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbOui.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOui.Location = new System.Drawing.Point(49, 18);
            this.rbOui.Name = "rbOui";
            this.rbOui.Size = new System.Drawing.Size(86, 63);
            this.rbOui.TabIndex = 0;
            this.rbOui.Text = "OUI";
            this.rbOui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbOui.UseVisualStyleBackColor = false;
            this.rbOui.CheckedChanged += new System.EventHandler(this.rbOui_CheckedChanged);
            // 
            // rbNon
            // 
            this.rbNon.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNon.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rbNon.Checked = true;
            this.rbNon.FlatAppearance.BorderSize = 0;
            this.rbNon.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbNon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.rbNon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbNon.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNon.Location = new System.Drawing.Point(160, 18);
            this.rbNon.Name = "rbNon";
            this.rbNon.Size = new System.Drawing.Size(86, 63);
            this.rbNon.TabIndex = 1;
            this.rbNon.TabStop = true;
            this.rbNon.Text = "NON";
            this.rbNon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbNon.UseVisualStyleBackColor = false;
            this.rbNon.CheckedChanged += new System.EventHandler(this.rbNon_CheckedChanged);
            this.rbNon.Click += new System.EventHandler(this.rbNon_Click);
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BackColor = System.Drawing.Color.White;
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.treeviewActes);
            this.intermediateStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "Choisir l\'acte à faire";
            this.intermediateStep2.Title = "Acte à faire";
            // 
            // treeviewActes
            // 
            this.treeviewActes.AllowDrop = true;
            this.treeviewActes.BackColor = System.Drawing.Color.White;
            this.treeviewActes.ButtonHeight = 75;
            this.treeviewActes.ButtonWidth = 75;
            this.treeviewActes.ChoixFamille = "";
            treeNode1.Name = "";
            treeNode1.Text = "";
            this.treeviewActes.CurrentNode = treeNode1;
            this.treeviewActes.isCreated = false;
            this.treeviewActes.Location = new System.Drawing.Point(4, 65);
            this.treeviewActes.MultiChoiceVisibilite = false;
            this.treeviewActes.Name = "treeviewActes";
            this.treeviewActes.SelectedNode = null;
            this.treeviewActes.Size = new System.Drawing.Size(594, 362);
            this.treeviewActes.TabIndex = 19;
            this.treeviewActes.totaleActeSupp = 0D;
            this.treeviewActes.OnSelected += new System.EventHandler(this.treeviewActes_OnSelected);
            // 
            // slidinglstWhen
            // 
            this.slidinglstWhen.ButtonSize = new System.Drawing.SizeF(75F, 75F);
            this.slidinglstWhen.imagelist = null;
            this.slidinglstWhen.Location = new System.Drawing.Point(175, 46);
            this.slidinglstWhen.MultiSelectMode = false;
            this.slidinglstWhen.Name = "slidinglstWhen";
            this.slidinglstWhen.Size = new System.Drawing.Size(414, 249);
            this.slidinglstWhen.TabIndex = 3;
            this.slidinglstWhen.WrapMode = true;
            this.slidinglstWhen.ClickNode += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.slidinglstWhen_ClickNode);
            this.slidinglstWhen.Load += new System.EventHandler(this.slidinglstWhen_Load);
            // 
            // lblNbJoursDepuisDbut
            // 
            this.lblNbJoursDepuisDbut.Location = new System.Drawing.Point(172, 279);
            this.lblNbJoursDepuisDbut.Name = "lblNbJoursDepuisDbut";
            this.lblNbJoursDepuisDbut.Size = new System.Drawing.Size(315, 26);
            this.lblNbJoursDepuisDbut.TabIndex = 5;
            this.lblNbJoursDepuisDbut.Text = " ";
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.lblNbJoursDepuisDbut);
            this.startStep1.Controls.Add(this.slidinglstWhen);
            this.startStep1.Icon = null;
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "Ajout d\'un acte pour dans :";
            this.startStep1.Title = "Ajout d\'un acte";
            this.startStep1.Click += new System.EventHandler(this.startStep1_Click);
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = true;
            this.wizardControl1.BackButtonText = "< Précedent";
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonText = "Annuler";
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.EulaButtonEnabled = true;
            this.wizardControl1.EulaButtonText = "eula";
            this.wizardControl1.EulaButtonVisible = false;
            this.wizardControl1.FinishButtonText = "Terminer";
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonVisible = false;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonText = "Suivant >";
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(601, 513);
            this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.intermediateStep2,
            this.intermediateStep3,
            this.finishStep1});
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
            this.wizardControl1.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_NextButtonClick);
            this.wizardControl1.Click += new System.EventHandler(this.wizardControl1_Click);
            // 
            // FRmWizardNewComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 513);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FRmWizardNewComment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajout d\'un acte";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FRmWizardNewComment_Load);
            this.ResizeEnd += new System.EventHandler(this.FRmWizardNewComment_ResizeEnd);
            this.finishStep1.ResumeLayout(false);
            this.finishStep1.PerformLayout();
            this.intermediateStep3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.intermediateStep2.ResumeLayout(false);
            this.startStep1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbxSummary;
        private WizardBase.IntermediateStep intermediateStep3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbNon;
        private System.Windows.Forms.RadioButton rbOui;
        private BaseCommonControls.SlidingList cbxResponsable;
        private WizardBase.IntermediateStep intermediateStep2;
        private TreeViewIcon treeviewActes;
        private BaseCommonControls.SlidingList slidinglstWhen;
        private System.Windows.Forms.Label lblNbJoursDepuisDbut;
        private WizardBase.StartStep startStep1;
        private WizardBase.WizardControl wizardControl1;

    }
}