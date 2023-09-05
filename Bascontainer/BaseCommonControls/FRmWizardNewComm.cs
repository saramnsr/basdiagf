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
    public partial class FRmWizardNewComm : Form
    {

        DateTime? dteDebutTRmnt = null;
        public DateTime? OtherDate = null;
        public string vquantite;
        public  double newprix;

     
        public Utilisateur PraticienREsp
        {
            get
            {
                if (rbOui.Checked && cbxResponsable.SelectedItems.Count > 0)
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

        public FRmWizardNewComm(basePatient pat, bool isRDV)
        {
            vtype = isRDV;
            Currentpatient = pat;
            InitializeComponent();
            if (isRDV)
            {
                this.startStep1.Subtitle = "Ajout d\'un rendre-vous pour dans :";
                this.startStep1.Title = "Ajout d\'un rendre-vous";
                this.intermediateStep3.Subtitle = "Un praticien spécifique est il a prevu pour cet rendre-vous ?";
                this.Text = "Ajout d\'un rendre-vous";




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


        private void InitDisplayActes()
        {
         
    

            
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

           
            InitDisplayActes();




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


          //  Utilisateur currentprat = ((Currentpatient.infoscomplementaire != null) && (Currentpatient.infoscomplementaire.PraticienResponsable != null)) ? Currentpatient.infoscomplementaire.PraticienResponsable : null;
            /*
            Utilisateur currentprat = null;
            List<Utilisateur> currentuserlst = UtilisateursMgt.getUtilisateurInFauteuil(Fauteuilsmgt.GetWhoIam(), DateTime.Now);
            foreach (Utilisateur u in currentuserlst)
                if (u.Fonction == "Praticien")
                    currentprat = u;
            */
           // if (currentprat != null) cbxResponsable.SelectByTag(currentprat);


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
          /*
            if (PraticienREsp!=null) txtbxSummary.Text += "\r\navec " + PraticienREsp.ToString();

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
            
            */

           

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
 
        }

        private void rbNon_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbNon_Click(object sender, EventArgs e)
        {
            if(wizardControl1.CurrentStepIndex <1)
            wizardControl1.Next();
      
        }

        private void cbxResponsable_ClickNode(object sender, TreeNodeMouseClickEventArgs e)
        {
           // if (e.Node.Nodes.Count==0)
            //        wizardControl1.Next();
        }


        private void FRmWizardNewComment_ResizeEnd(object sender, EventArgs e)
        {
            //treeviewActes.Width = this.Width;
            //treeviewActes.RecalculEmplacementButtons(false);
        }

        private void cbxResponsable_Load(object sender, EventArgs e)
        {

        }
    }
}
