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
//using BASEPractice_BO;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls.Ctrls;
using BaseCommonControls.Ctrls.BO;

namespace BaseCommonControls
{
    public partial class FRmWizardNewComment : Form
    {

        DateTime? dteDebutTRmnt = null;
        public DateTime? OtherDate = null;
        public string vquantite;
        public double newprix;
        public Acte acteselected
        {
            get
            {
                if (treeviewActes.SelectedNode.Tag is FamillesActe )
                {
                    return null;
                }
                else
                {
                    return treeviewActes.SelectedNode == null ? null : (Acte)treeviewActes.SelectedNode.Tag;
                }
               
            }
            
        }

       
        
        public Utilisateur PraticienREsp
        {
            get
            {
                if (rbOui.Checked)
                    if (cbxResponsable.SelectedItems.Count > 0)
                    {
                        return (Utilisateur)cbxResponsable.SelectedItems[0].Tag;
                    }
                    else
                    {
                        return null;
                    }

                
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
                    //case EnumForWhen ._3MoisDemi: return 0 ;
                    //case EnumForWhen._4Mois: return 4;
                    case EnumForWhen._6Mois: return 6;
                    //case EnumForWhen._7Mois: return 7;
                    //case EnumForWhen._8Mois: return 8;
                    //case EnumForWhen._14Mois: return 14;
                    //case EnumForWhen._1An: return 12;

                    //case EnumForWhen._10Mois: return 10;
                    //case EnumForWhen._16Mois: return 16;
                    //case EnumForWhen._18Mois: return 18;
                    //case EnumForWhen._20Mois: return 20;
                    //case EnumForWhen._22Mois: return 22;
                    //case EnumForWhen._24Mois: return 24;
                    //case EnumForWhen._26Mois: return 26;
                    

                    //case EnumForWhen._28Mois: return 28;
                    //case EnumForWhen._3Ans: return 36;
                    

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
                    //case EnumForWhen._3MoisDemi: return 105;
                    //case EnumForWhen._4Mois: return 0;
                    case EnumForWhen._6Mois: return 0;
                    //case EnumForWhen._7Mois: return 0;
                    //case EnumForWhen._8Mois: return 0;
                    //case EnumForWhen._14Mois: return 0;
                    //case EnumForWhen._1An: return 0;
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
            //_3MoisDemi,
            //_4Mois,
            _6Mois,
            //_7Mois,
            //_8Mois,
            //_1An,
            //_14Mois,
            //_10Mois,
            //_16Mois,
            //_18Mois,
            //_20Mois,
            //_22Mois,
            //_24Mois,
            //_26Mois,
            //_28Mois,
            //_3Ans,
            ToSend,
            Other
            
        }



        public FRmWizardNewComment()
        {
          
            InitializeComponent();
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

            List<Acte> TmpActe = new List<Acte>();
           // TmpActe = BasCommon_BL.ActesMgmt.GetActes();
           // BasCommon_BL.ActesMgmt.Actes = TmpActe;
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {
                    foreach (Acte a in ActesMgmt.Actes)
                    {
                        //if (a.famille_Acte == null) NonAffectedExist = true;
                        if (a.famille_Acte == node.Tag)
                        {
                            TreeNode ActeNode = node.Nodes.Add(a.acte_libelle+"\n"+a.nomenclature);
                            ActeNode.Tag = a;
                        }
                    }
                    AffecteActesToNode(node.Nodes);
                }
            }



        }

        private void RefreshActes()
        {

            treeviewActes.Clear();

          //  List<FamillesActe> TmpFamille = new List<FamillesActe>();
            //TmpFamille = BasCommon_BL.ActesMgmt.GetFamillesActe();
            //BasCommon_BL.ActesMgmt.famillesacte = TmpFamille;

            foreach (FamillesActe f in ActesMgmt.famillesacte)
                if (f.ParentFamillesActeId == -1)
                    RefreshFamilies(treeviewActes.Root.Nodes, f);

            AffecteActesToNode(treeviewActes.Root.Nodes);



            //List<Acte> TmpActe = new List<Acte>();
            //TmpActe = BasCommon_BL.ActesMgmt.GetActes();
            //BasCommon_BL.ActesMgmt.Actes = TmpActe;

            foreach (Acte a in ActesMgmt.Actes)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Acte == null) && (a.id_acte != -1))
                {
                    TreeNode ActeNode = treeviewActes.Root.Nodes.Add(a.acte_libelle+"/n"+a.nomenclature);
                    ActeNode.Tag = a;
                }
            }

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

            bool IsSelected = false;

            IsSelected = (node == treeviewActes.SelectedNode);

            if (node.Tag is Acte)
                cl = ((Acte)node.Tag).acte_couleur;
            if (node.Tag is FamillesActe)
                cl = ((FamillesActe)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            if (node.Tag is FamillesActe)
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


        private void InitDisplayActes()
        {
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
            //fv = new BaseCommonControls.FamilyValue("", "3 mois\net demi", EnumForWhen._3MoisDemi);
            //lst.Add(fv);
            //fv = new BaseCommonControls.FamilyValue("", "4 mois", EnumForWhen._4Mois);
            //lst.Add(fv);
            fv = new BaseCommonControls.FamilyValue("", "6 mois", EnumForWhen._6Mois);
            lst.Add(fv);
            //fv = new BaseCommonControls.FamilyValue("", "7 mois", EnumForWhen._7Mois);
            //lst.Add(fv);
            //fv = new BaseCommonControls.FamilyValue("", "8 mois", EnumForWhen._8Mois);
            //lst.Add(fv);
            //fv = new BaseCommonControls.FamilyValue("", "1 an", EnumForWhen._1An);
            //lst.Add(fv);

            //fv = new BaseCommonControls.FamilyValue("", "10 mois", EnumForWhen._10Mois);
            //lst.Add(fv);

           


            //fv = new BaseCommonControls.FamilyValue("", "14 mois", EnumForWhen._14Mois);
            //lst.Add(fv);

            //fv = new BaseCommonControls.FamilyValue("", "16 mois", EnumForWhen._16Mois);
            //lst.Add(fv);
          
            //fv = new BaseCommonControls.FamilyValue("", "18 mois", EnumForWhen._18Mois);
            //lst.Add(fv);
          
            //fv = new BaseCommonControls.FamilyValue("", "20 mois", EnumForWhen._20Mois);
            //lst.Add(fv);
          
            //fv = new BaseCommonControls.FamilyValue("", "22 mois", EnumForWhen._22Mois);
            //lst.Add(fv);
          
            //fv = new BaseCommonControls.FamilyValue("", "24 mois", EnumForWhen._24Mois);
            //lst.Add(fv);


            //fv = new BaseCommonControls.FamilyValue("", "26 mois", EnumForWhen._26Mois);
            //lst.Add(fv);

            //fv = new BaseCommonControls.FamilyValue("", "28 mois", EnumForWhen._28Mois);
            //lst.Add(fv);

            //fv = new BaseCommonControls.FamilyValue("", "3 ans", EnumForWhen._3Ans);
            //lst.Add(fv);

            
            fv = new BaseCommonControls.FamilyValue("", "Autre", EnumForWhen.Other);
            lst.Add(fv);

            slidinglstWhen.LoadFromFamilyValueList(lst);

           
            InitDisplayActes();


           // treeviewActes.Size = new Size(492,247);


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


         //   Utilisateur currentprat = ((Currentpatient.infoscomplementaire != null) && (Currentpatient.infoscomplementaire.PraticienResponsable != null)) ? Currentpatient.infoscomplementaire.PraticienResponsable : null;
            /*
            Utilisateur currentprat = null;
            List<Utilisateur> currentuserlst = UtilisateursMgt.getUtilisateurInFauteuil(Fauteuilsmgt.GetWhoIam(), DateTime.Now);
            foreach (Utilisateur u in currentuserlst)
                if (u.Fonction == "Praticien")
                    currentprat = u;
            */


      //      cbxResponsable.AddToSelectionByTag(cbxResponsable.lstbuttons[0].Tag);


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
                        OtherDate = DateTime.Now.AddDays( frm.nbJours);
                    }
                }
            //    BaseCommonControls.FrmDate frm = new BaseCommonControls.FrmDate("Choix d'une date", "Choix d'une date");
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
            if (acteselected!=null) txtbxSummary.Text = "Acte " + acteselected.acte_libelle + " ("  + vquantite + ")";
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
            if (treeviewActes.SelectedNode.Tag is Acte)
            {
                
                        Acte TmpActe = new  Acte();
                        TmpActe = (Acte)treeviewActes.SelectedNode.Tag;
                        FrmString frmS = new FrmString("Quantité", "Quantité", TmpActe.quantite);
                         vquantite = TmpActe.quantite;
                        if (frmS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            vquantite = frmS.Value;

                        }
                        else
                        {
                            return;
                        }
                        ActePGPropose TmpActePG = new ActePGPropose();
                        TmpActePG.MontantAvantRemise = TmpActe.prix_acte;
                        FrmRistourne frmR = new FrmRistourne(TmpActePG);
                         newprix = TmpActe.prix_acte;
                        if (frmR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            newprix = frmR.Value;
                        }
                       
                        wizardControl1.Next();
                    }
          
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
            if (e.Node.Nodes.Count==0)
                    wizardControl1.Next();
        }

        private void wizardControl1_NextButtonClick(object sender, CancelEventArgs e)
        {
            if (wizardControl1.CurrentStepIndex == 1)
            {
                if (treeviewActes.SelectedNode != null)
                {
                    if (treeviewActes.SelectedNode.Tag is FamillesActe)
                    {
                        MessageBox.Show("Vous devez choisir un acte");
                        e.Cancel = true;
                    }
                    else
                    {
                        if (Convert.ToDouble(((Acte)treeviewActes.SelectedNode.Tag).nombre_points) <= 0)
                            {
                        MessageBox.Show("Acte sans nombre de points");
                        e.Cancel = true;
                    }
                        
                    }
                    
                }
                else
                {
                    MessageBox.Show("Vous devez choisir un acte");
                    e.Cancel = true;
                }
            }
            if (wizardControl1.CurrentStepIndex == 2)
            {
                if (rbOui.Checked && cbxResponsable.SelectedItems .Count == 0)
                {
                    MessageBox.Show("Vous devez choisir un praticien");
                    e.Cancel = true;
                }
            }
            
        }
    }
}
