using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BASEPractice_BL;
using BASEPractice_BO;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls.Ctrls;
using BaseCommonControls.Ctrls.BO;
using BaseCommonControls;

namespace BaseCommonControls
{
    public partial class FRmWizardNewRDV : Form
    {
        TypeFamilleTraitement typeFmTrt = TypeFamilleTraitement.RDV;

        DateTime? dteDebutTRmnt = null;
        public DateTime? OtherDate = null;
        public string vquantite;
        public double newprix;

        public Acte acteselected
        {
            get
            {
                return treeviewActes.SelectedNode == null ? null : (Acte)treeviewActes.SelectedNode.Tag;
            }

        }

        public NewTraitement traitementselected
        {
            get
            {
                return treeviewActes.SelectedNode == null ? null : (NewTraitement)treeviewActes.SelectedNode.Tag;
            }

        }
        public Utilisateur PraticienREsp
        {
            get
            {
                if (rbOui.Checked)
                    return (Utilisateur)cbxResponsable.SelectedItems[0].Tag;
                else
                    return null;
            }

        }

        public int NbMoisFromNow
        {
            get
            {

                switch ((EnumForWhen)slidinglstWhen.SelectedItems[0].Tag)
                {
                    case EnumForWhen._1Sem: return 0;
                    case EnumForWhen._Today: return -1;
                    case EnumForWhen._2Sem: return 0;
                    case EnumForWhen._1Mois: return 1;
                    case EnumForWhen._2Mois: return 2;
                    case EnumForWhen._3Mois: return 3;
                    case EnumForWhen._6Mois: return 6;
                    case EnumForWhen._1An: return 12;
                    case EnumForWhen.DQP: return 0;
                    case EnumForWhen.Other: return 0;
                    case EnumForWhen.ToSend: return 0;
                    default: return 0;

                }
            }

        }



        public int NbJoursFromNow
        {
            get
            {
                switch ((EnumForWhen)slidinglstWhen.SelectedItems[0].Tag)
                {
                    case EnumForWhen._Today: return -1;
                    case EnumForWhen._1Sem: return 7;
                    case EnumForWhen._2Sem: return 14;
                    case EnumForWhen._1Mois: return 0;
                    case EnumForWhen._2Mois: return 0;
                    case EnumForWhen._3Mois: return 0;
                    case EnumForWhen._6Mois: return 0;
                    case EnumForWhen._1An: return 0;
                    case EnumForWhen.DQP: return 1;
                    case EnumForWhen.Other: return 15;
                    case EnumForWhen.ToSend: return 0;
                    default: return 0;

                }
            }

        }

        public int NbJoursFromDebutTrmnt
        {
            get
            {
                if (dteDebutTRmnt != null)
                {
                    int nbdays = (int)(DateTime.Now - dteDebutTRmnt.Value).TotalDays;
                    return NbJoursFromNow + nbdays;
                }
                else
                    return 0;

            }

        }

        public int NbsemFromDebutTrmnt
        {
            get
            {
                return (NbJoursFromDebutTrmnt / 7);

            }

        }




        private basePatient _Currentpatient;
        public basePatient Currentpatient
        {
            get
            {
                return _Currentpatient;
            }
            set
            {
                _Currentpatient = value;
            }
        }

        public enum EnumForWhen
        {
            DQP,
            _Today,
            _1Sem,
            _2Sem,
            _1Mois,
            _2Mois,
            _3Mois,
            _6Mois,
            _1An,
            ToSend,
            Other

        }

        Boolean vtype = false;

        public FRmWizardNewRDV(basePatient pat, TypeFamilleTraitement trtfm)
        {
            // dooner la valeur true à v type pour qu on puisse faire un traitement similaire comme celui du rendez-vous
            // dhaherli zeyed na3ti fi TypeFamille Traitement ;) tant qu'on pourra faire le test avec le IdFamille du tag du traitement
            vtype = true;
            Currentpatient = pat;
            InitializeComponent();
            this.typeFmTrt = trtfm;
            this.startStep1.Subtitle = "Ajout d\'un groupement d'acte 2";
            this.intermediateStep2.Subtitle = "Choisir groupement d'acte 2 pour dans :";
            this.startStep1.Title = "Ajout d\'un groupement d'acte 2 à réaliser";
            this.intermediateStep2.Title = "groupement d'acte 2 à faire";
            this.intermediateStep3.Subtitle = "Un praticien spécifique est il a prevu pour cet rendre-vous ?";
            this.Text = "Ajout d\'un groupement d'acte 2";
        }

        public FRmWizardNewRDV(basePatient pat, bool type = false)
        {
            vtype = type;
            Currentpatient = pat;
            InitializeComponent();
            if (type)
            {
                this.startStep1.Subtitle = "Ajout d\'un rendez-vous pour dans :";
                this.startStep1.Title = "Ajout d\'un rendez-vous";
                this.intermediateStep2.Subtitle = "Choisir rendez-vous à faire";
                this.intermediateStep2.Title = "Rendez-vous à faire";
                this.intermediateStep3.Subtitle = "Un praticien spécifique est il a prevu pour cet Rendez-vous ?";
                this.Text = "Ajout d\'un rendez-vous";




            }
        }

        private void wizardControl1_Click(object sender, EventArgs e)
        {

        }

        private void RefreshFamilies(TreeNodeCollection collec, FamillesActe f)
        {


            TreeNode familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesActe fa in f.ChildFamillesActe)
                RefreshFamilies(familynode.Nodes, fa);
        }



        private void AffecteActesToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {
                    foreach (Acte a in ActesMgmt.Actes)
                    {
                        //if (a.famille_Acte == null) NonAffectedExist = true;
                        if (a.famille_Acte == node.Tag)
                        {
                            TreeNode ActeNode = node.Nodes.Add(a.acte_libelle);
                            ActeNode.Tag = a;
                        }
                    }
                    AffecteActesToNode(node.Nodes);
                }
            }



        }
        private void RefreshFamilies(TreeNodeCollection collec, FamillesTraitements f)
        {


            TreeNode familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesTraitements fa in f.ChildFamillesTraitement)
                RefreshFamilies(familynode.Nodes, fa);
        }
        private void AffecteTraitementsToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {

                if (node.Tag is FamillesTraitements)
                {
                    foreach (NewTraitement a in TraitementsMgmt.Traitements)
                    {
                        if (a.famille_Traitement != null)
                        {
                            FamillesTraitements TF = new FamillesTraitements();
                            TF = (FamillesTraitements)node.Tag;
                            if (a.famille_Traitement.libelle.Trim() == TF.libelle.Trim() && a.famille_Traitement.ordre == TF.ordre)


                            //if (a.famille_Acte == null) NonAffectedExist = true;
                            //if (a.famille_Traitement == node.Tag)
                            {
                                TreeNode TraitementNode = node.Nodes.Add(a.Traitement_shortlib + "\n" + a.Montant_Scenario);
                                TraitementNode.Tag = a;
                            }
                        }
                    }
                    AffecteTraitementsToNode(node.Nodes);
                }
            }
        }


        private void RefreshActes()
        {

            treeviewActes.Clear();



            foreach (FamillesActe f in ActesMgmt.famillesacte)
                if (f.ParentFamillesActeId == -1)
                    RefreshFamilies(treeviewActes.Root.Nodes, f);

            AffecteActesToNode(treeviewActes.Root.Nodes);

            foreach (Acte a in ActesMgmt.Actes)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Acte == null) && (a.id_acte != -1))
                {
                    TreeNode ActeNode = treeviewActes.Root.Nodes.Add(a.acte_libelle);
                    ActeNode.Tag = a;
                }
            }
            treeviewActes.Width = this.Width;
            treeviewActes.RecalculEmplacementButtons(true);


        }


        private void PaintHeader(object sender, PaintEventArgs e)
        {

            string Title = "";
            TreeNode node = ((TreeViewIcon)sender).CurrentNode;
            TreeNode parent = node;

            while (parent != null)
            {
                if (Title != "") Title = "/" + Title;
                Title = parent.Text + Title;
                parent = parent.Parent;
            }

            Color cl = Color.WhiteSmoke;
            if (node.Tag is FamillesActe)
                cl = ((FamillesActe)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path

            path = GraphicUtils.CreateRoundedRectanglePath(e.ClipRectangle, 5);
            e.Graphics.FillPath(new SolidBrush(cl), path);
            e.Graphics.DrawPath(new Pen(Brushes.Black), path);

            // little transparent
            Color start = Color.FromArgb(255, cl);
            Color end = Color.FromArgb(180, cl);

            if (e.ClipRectangle.Width > 0)
                using (LinearGradientBrush aGB = new LinearGradientBrush(e.ClipRectangle, start, end, LinearGradientMode.Vertical))
                    e.Graphics.FillPath(aGB, path);


            Font ft = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            PointF txts = new PointF(e.Graphics.MeasureString(Title, ft).Width, e.Graphics.MeasureString(node.Text, ft).Height);
            e.Graphics.DrawString(Title, ft, Brushes.Black, new PointF(e.ClipRectangle.X + (e.ClipRectangle.Width - txts.X) / 2, e.ClipRectangle.Y + e.ClipRectangle.Height - txts.Y - 2));


        }

        private void PaintActes(object sender, PaintEventArgs e)
        {

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.Word;

            TreeNode node = ((TreeNode)sender);
            Color cl = Color.WhiteSmoke;

            bool IsSelected;
            bool IsHover;

            IsSelected = (node == treeviewActes.SelectedNode);
            IsHover = (node == treeviewActes.HoveredNode);

            if (node.Tag is BasCommon_BO.NewTraitement)
                cl = ((BasCommon_BO.NewTraitement)node.Tag).Traitement_couleur;
            if (node.Tag is BasCommon_BO.FamillesTraitements)
                cl = ((BasCommon_BO.FamillesTraitements)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            if (node.Tag is BasCommon_BO.FamillesTraitements)
            {
                Rectangle r = e.ClipRectangle;
                r.X += 2;
                r.Y += 2;
                path = GraphicUtils.CreateRoundedRectanglePath(r, 5);
                e.Graphics.DrawPath(new Pen(Brushes.Black, 1), path);
            }

            path = GraphicUtils.CreateRoundedRectanglePath(e.ClipRectangle, 5);
            e.Graphics.DrawPath(new Pen(Brushes.Black, 1), path);

            // little transparent
            Color start = Color.FromArgb(255, cl);
            Color end = Color.FromArgb(180, cl);

            if (e.ClipRectangle.Width > 0)
                using (LinearGradientBrush aGB = new LinearGradientBrush(e.ClipRectangle, start, end, LinearGradientMode.Vertical))
                    e.Graphics.FillPath(aGB, path);
            if (IsSelected)
                e.Graphics.DrawPath(new Pen(Brushes.GreenYellow, 5), path);

            if (IsHover)
                e.Graphics.DrawPath(new Pen(Brushes.GreenYellow, 2), path);





            float fl = 12;
            Font ft = new Font("Segoe UI", fl, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF s = e.Graphics.MeasureString(node.Text, ft, e.ClipRectangle.Size, sf);


            while (s.Width > e.ClipRectangle.Width)
            {
                fl--;
                if (fl < 1) { fl = 2; break; }
                ft = new Font("Segoe UI", fl, FontStyle.Regular, GraphicsUnit.Pixel);
                s = e.Graphics.MeasureString(node.Text, ft);

            }

            e.Graphics.DrawString(node.Text, ft, Brushes.Black, e.ClipRectangle, sf);



        }

        private void RefreshTraitements()
        {
            // modif içi pour afficher la treeView GrpAct2
            treeviewActes.Clear();


            TraitementsMgmt.famillesTraitement = null;
            foreach (FamillesTraitements f in TraitementsMgmt.famillesTraitement)
                if (f.ParentFamillesTraitementId == -1)
                {
                     if (f.typeFamilleTraitement == TypeFamilleTraitement.RDV)
                        RefreshFamilies(treeviewActes.Root.Nodes, f);
                }

            AffecteTraitementsToNode(treeviewActes.Root.Nodes);

            foreach (NewTraitement a in TraitementsMgmt.Traitements)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Traitement == null) && (a.id_Traitement != -1))
                {
                    TreeNode TraitementNode = treeviewActes.Root.Nodes.Add(a.Traitement_libelle);
                    TraitementNode.Tag = a;

                }
            }

            treeviewActes.RecalculEmplacementButtons(true);

        }

        private void RefreshGrpActe2()
        {
            treeviewActes.Clear();


            TraitementsMgmt.famillesTraitement = null;
            foreach (FamillesTraitements f in TraitementsMgmt.famillesTraitement)
                if (f.typeFamilleTraitement == TypeFamilleTraitement.GroupementActe2)
                {
                    if (f.ParentFamillesTraitementId == -1)
                    {
                        RefreshFamilies(treeviewActes.Root.Nodes, f);
                    }

                    AffecteTraitementsToNode(treeviewActes.Root.Nodes);
                }


            foreach (NewTraitement a in TraitementsMgmt.Traitements)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Traitement == null) && (a.id_Traitement != -1))
                {
                    TreeNode TraitementNode = treeviewActes.Root.Nodes.Add(a.Traitement_libelle);
                    TraitementNode.Tag = a;

                }
            }

            treeviewActes.RecalculEmplacementButtons(true);
        }


        private void InitDisplayActes()
        {
            if (this.typeFmTrt == TypeFamilleTraitement.GroupementActe2)
            {
                RefreshGrpActe2();
                treeviewActes.ButtonPaint += new PaintEventHandler(PaintActes);
                treeviewActes.HeaderPaint += new PaintEventHandler(PaintHeader);
                return;
            }

            if (vtype)
                RefreshTraitements();
            else
                RefreshActes();

            treeviewActes.ButtonPaint += new PaintEventHandler(PaintActes);
            treeviewActes.HeaderPaint += new PaintEventHandler(PaintHeader);
        }

        private void FRmWizardNewComment_Load(object sender, EventArgs e)
        {
            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

            BaseCommonControls.FamilyValue fv = new BaseCommonControls.FamilyValue("", "Des que possible", EnumForWhen.DQP);
            fv = new BaseCommonControls.FamilyValue("", "Aujourd'hui", EnumForWhen._Today);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "1 sem", EnumForWhen._1Sem);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "2 sem", EnumForWhen._2Sem);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "1 mois", EnumForWhen._1Mois);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "2 mois", EnumForWhen._2Mois);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "3 mois", EnumForWhen._3Mois);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "6 mois", EnumForWhen._6Mois);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "1 an", EnumForWhen._1An);
            lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "Autre", EnumForWhen.Other);
            lst.Add(fv);

            slidinglstWhen.LoadFromFamilyValueList(lst);

            // creation de la treeView
            // à modifier et adapter afin qu'elle puisse faire afficher la treeGrpAct2 au lieu de TreeViewActe
            // il y on a bcp du travail içi ...

            InitDisplayActes();


            treeviewActes.Size = new Size(this.Width, this.Height);


            lst = new List<BaseCommonControls.FamilyValue>();


            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                if (u.Actif)
                {
                    if ((u.Fonction == "Praticien") || (u.type == Utilisateur.typeUtilisateur.Praticien))
                        lst.Add(new BaseCommonControls.FamilyValue("", u.LastNameShort, u));

                    /*
                    if ((u.Fonction == "Praticien"))
                        lst.Add(new BaseCommonControls.FamilyValue("Praticiens", u.LastNameShort, u));

                    if ((u.Fonction != "Praticien"))
                        lst.Add(new BaseCommonControls.FamilyValue("Assistantes" + BaseCommonControls.FamilyValue.famillyseparator + u.Prenom[0].ToString(), u.NameShort, u));
                    */
                }



            cbxResponsable.LoadFromFamilyValueList(lst);

            if (Currentpatient != null)
            {
                Utilisateur currentprat = ((Currentpatient.infoscomplementaire != null) && (Currentpatient.infoscomplementaire.PraticienResponsable != null)) ? Currentpatient.infoscomplementaire.PraticienResponsable : null;
                if (currentprat != null) cbxResponsable.SelectByTag(currentprat);

            }
        }

        private void slidinglstWhen_ClickNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((EnumForWhen)e.Node.Tag != EnumForWhen.Other))
                wizardControl1.Next();
            else
            {
                FrmChoisxDate frm = new FrmChoisxDate();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.checkDate)
                    {
                        if (frm.date < DateTime.Now)
                            MessageBox.Show("La date ne peut pas être passée");
                        else
                        {
                            OtherDate = frm.date;
                            wizardControl1.Next();
                        }
                    }
                    else
                    {
                        OtherDate = DateTime.Now.AddDays(frm.nbJours);
                    }
                }
                //BaseCommonControls.FrmDate frm = new BaseCommonControls.FrmDate("Choix d'une date", "Choix d'une date");
                //if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    if (frm.Value < DateTime.Now)
                //    {
                //        MessageBox.Show("La date ne peut pas être passée");
                //    }
                //    else
                //    {
                //        OtherDate = frm.Value;
                //        wizardControl1.Next();
                //    }
                //}
            }

        }

        private void intermediateStep3_Click(object sender, EventArgs e)
        {

        }

        private void rbOui_CheckedChanged(object sender, EventArgs e)
        {
            cbxResponsable.Visible = rbOui.Checked;
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void finishStep1_OnShow(object sender, EventArgs e)
        {
            if (vtype)
            {
                txtbxSummary.Text = "Traitement " + traitementselected.Traitement_libelle;
            }
            else
                if (acteselected != null)
                {
                    txtbxSummary.Text = "Acte " + acteselected.acte_libelle + " (" + vquantite + ")";
                }
            if (PraticienREsp != null) txtbxSummary.Text += "\r\navec " + PraticienREsp.ToString();

            string dur = "";
            if (NbMoisFromNow >= 0)
            {
                if (OtherDate != null)
                {
                    txtbxSummary.Text += "\r\nPrevus pour le " + OtherDate.Value.ToLongDateString();



                }
            }
            else
            {
                if (NbMoisFromNow >= 0)
                {
                    dur = NbMoisFromNow > 0 ? NbMoisFromNow.ToString() + " mois" : "";
                    if (NbJoursFromNow > 0 && dur != "") dur += " et ";
                    dur += NbJoursFromNow > 0 ? NbJoursFromNow.ToString() + " jours" : "";

                    if (dur != "")
                        txtbxSummary.Text += "\r\nPrevus pour dans " + dur;


                    if (dteDebutTRmnt != null)
                    {


                        dur = NbMoisFromNow > 0 ? NbMoisFromNow.ToString() + " mois" : "";
                        if (NbJoursFromDebutTrmnt > 0 && dur != "") dur += " et ";
                        dur += NbJoursFromDebutTrmnt > 0 ? NbJoursFromDebutTrmnt.ToString() + " jours" : "";



                        if (dur != "")
                            txtbxSummary.Text += "\r\nsoit " + dur + " apres le debut du traitement ";
                    }
                }
                else
                {
                    dur = "Aujourd'hui";
                    txtbxSummary.Text += "\r\nPrevus Aujourd'hui ";
                }

            }





        }

        private void intermediateStep1_Click(object sender, EventArgs e)
        {

        }

        private void startStep1_Click(object sender, EventArgs e)
        {

        }

        private void slidinglstWhen_Load(object sender, EventArgs e)
        {

        }

        private void treeviewActes_OnSelected(object sender, EventArgs e)
        {
            // ca verifie si j'ai selectionné un acte ou un NewTraitement
            // un New Traitement pourrat etre soit un rendezVous ou bien un GrpActe2
            if (treeviewActes.SelectedNode.Tag is Acte || treeviewActes.SelectedNode.Tag is NewTraitement || treeviewActes.SelectedNode.Tag is Traitement)
                wizardControl1.Next();
        }

        private void rbNon_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbNon_Click(object sender, EventArgs e)
        {
            wizardControl1.Next();
        }

        private void cbxResponsable_ClickNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
                wizardControl1.Next();
        }

        private void wizardControl1_NextButtonClick(object sender, CancelEventArgs e)
        {
            if (wizardControl1.CurrentStepIndex == 1)
            {
                if (treeviewActes.SelectedNode != null)
                {
                    if (!(treeviewActes.SelectedNode.Tag is Acte) && !(treeviewActes.SelectedNode.Tag is NewTraitement))

                    //if (!(treeviewActes.SelectedNode.Tag is Acte) && !(treeviewActes.SelectedNode.Tag is NewTraitement ) && !(treeviewActes.SelectedNode.Tag is Traitement))
                    {
                        MessageBox.Show("Il faut choisir un acte");
                        e.Cancel = true;
                    }
                    else
                        // c'est bon j ai modifier le constructeur vtype == true il sera Considerer comme RDV
                        if (!vtype)
                        {



                            Acte TmpActe = new Acte();
                            TmpActe = (Acte)treeviewActes.SelectedNode.Tag;
                            FrmString frmS = new FrmString("Quantité", "Quantité", TmpActe.quantite);
                            vquantite = TmpActe.quantite;
                            if (frmS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                vquantite = frmS.Value;

                            }

                            ActePGPropose TmpActePG = new ActePGPropose();
                            TmpActePG.MontantAvantRemise = TmpActe.prix_acte;
                            FrmRistourne frmR = new FrmRistourne(TmpActePG);
                            newprix = TmpActe.prix_acte;
                            if (frmR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {

                                newprix = frmR.Value;
                            }

                        }
                }
                if (treeviewActes.SelectedNode == null)
                {
                    MessageBox.Show("Il faut choisir un acte");
                    e.Cancel = true;
                }
            }
        }

        private void FRmWizardNewComment_ResizeEnd(object sender, EventArgs e)
        {
            //treeviewActes.Width = this.Width;
            //treeviewActes.RecalculEmplacementButtons(false);
        }
    }
}
