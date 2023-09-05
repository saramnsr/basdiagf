using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEPractice_BL;

using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls.Ctrls;
using BaseCommonControls.Ctrls.BO;
using System.Text.RegularExpressions;

namespace BaseCommonControls
{
    public partial class FrmActeTraitement : Form
    {
        private CommTraitement _Parent;
        public CommTraitement Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
        private List<CommActesTraitement> _comms;
        public List<CommActesTraitement> comms
        {
            get
            {
                return _comms;
            }
            set
            {
                _comms = value;
            }
        }
        private List<ActeGroupement> _acteg;
        public List<ActeGroupement> acteg
        {
            get
            {
                return _acteg;
            }
            set
            {
                _acteg = value;
            }
        }

        private ActeTraitement _Value;
        public ActeTraitement Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        double totaleActeSupp = 0;
        string _ChoixFamille;
        public Boolean ChangeItems;
        string vquantite;
        double newprix;
        bool _BloquerNiveau1;
        int _idParent = -1;
        public FrmActeTraitement()
        {
            InitializeComponent();
            isActeGroupementTraitement = true;
            InitDisplayGroupementTraitement();
            acteg = new List<ActeGroupement>();

        }
        public FrmActeTraitement(List<ActeGroupement> ac, int idParent)
        {
            InitializeComponent();
            acteg = ac;
            _idParent = idParent;
            treeviewActes.MultiChoiceVisibilite = true;
            isActeGroupement = true;
            InitDisplayGroupement();

        }
        public FrmActeTraitement(CommTraitement com, int MultiChoix = 0, string ChoixFamille = "", bool BloquerNiveau1 = false, bool isCreated = false)
        {
            ChangeItems = false;
            _ChoixFamille = ChoixFamille;
            _BloquerNiveau1 = BloquerNiveau1;
            InitializeComponent();
            Parent = com;
            treeviewActes.isCreated = isCreated;
            List<CommActesTraitement> TmpActesTraitement = new List<CommActesTraitement>();
            TmpActesTraitement = com.photos;
            // comms = com.ActesSupp;
            if (ChoixFamille.Trim().ToUpper() == "RADIO")
            {
                comms = com.Radios;
            }
            else if (ChoixFamille.Trim().ToUpper() == "PHOTOS")
            {
                comms = com.photos;
            }
            //else if (ChoixFamille.Trim().ToUpper() == "ActesSupp1")
            //{
            //    comms = com.ActesSupp1;
            //}
            else
            {

                comms = com.ActesSupp;

            }

            if (MultiChoix == 0)
            {
                treeviewActes.MultiChoiceVisibilite = false;
                //this.Width = 338;
                //this.Height = 493;
            }
            else
            {
                treeviewActes.MultiChoiceVisibilite = true;

                //this.Width = 500;
                //this.Height = 493;
            }
            InitDisplay();
        }
        private void AffecteActesGrpToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {
                    foreach (Acte a in ActesMgmt.Actes)
                    {

                        if (a.famille_Acte != null)
                        {
                            FamillesActe TF = new FamillesActe();
                            TF = (FamillesActe)node.Tag;
                            if (a.famille_Acte.libelle.Trim() == TF.libelle.Trim() && a.famille_Acte.ordre == TF.ordre)


                                if (a.famille_Acte.Id == TF.Id)
                                {
                                    TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString())+ "\n" +  a.nomenclature);
                                    ActeNode.Tag = a;
                                }

                        }

                    }


                    AffecteActesToNode(node.Nodes);
                }
            }



        }
        private void AffecteActesGroupementToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {

                    foreach (ActeGroupement a in ActesMgmt.ActesGroupement)
                    {

                        if (a.famille_Acte != null)
                        {
                            FamillesActe TF = new FamillesActe();
                            TF = (FamillesActe)node.Tag;
                            if (a.famille_Acte.libelle.Trim() == TF.libelle.Trim() && a.famille_Acte.ordre == TF.ordre)


                                if (a.famille_Acte == node.Tag)
                                {
                                    TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString())+a.nomenclature);
                                    ActeNode.Tag = a;
                                }

                        }

                    }

                    AffecteActesGroupementToNode(node.Nodes);
                }
            }



        }
        private void AffecteActesToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {

                    foreach (Acte a in ActesMgmt.Actes)
                    {

                        if (a.famille_Acte != null)
                        {
                            FamillesActe TF = new FamillesActe();
                            TF = (FamillesActe)node.Tag;
                            if (a.famille_Acte.libelle.Trim() == TF.libelle.Trim() && a.famille_Acte.ordre == TF.ordre)


                                if (a.famille_Acte == node.Tag)
                                {
                                    TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString()) + a.nomenclature);
                                    ActeNode.Tag = a;
                                }

                        }

                    }

                    AffecteActesToNode(node.Nodes);
                }
            }



        }


        private void RefreshFamilies(TreeNodeCollection collec, FamillesActe f)
        {
            TreeNode familynode = null;
            if (isActeGroupementTraitement && ActesMgmt.ActesGroupement.FindAll(w => w.id_famille == f.Id).Sum(x => x.prixTraitement * x.qte) > 0)
                familynode = collec.Add(f.libelle + "\n" + ActesMgmt.ActesGroupement.FindAll(w => w.id_famille == f.Id).Sum(x => x.prixTraitement * x.qte).ToString());
            else
                familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesActe fa in f.ChildFamillesActe)
                RefreshFamilies(familynode.Nodes, fa);
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
                if ((a.famille_Acte == null) && (a.id_acte != -1))
                {
                    TreeNode ActeNode = treeviewActes.Root.Nodes.Add(a.acte_libelle+a.nomenclature);
                    ActeNode.Tag = a;
                }
            }

            treeviewActes.RecalculEmplacementButtons(true);
            if (_ChoixFamille != null && _ChoixFamille != "")
            {
                foreach (trButton b in treeviewActes.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeviewActes.CurrentNode = ((TreeNode)b.Tag);
                    }
                }
                treeviewActes.RecalculEmplacementButtons(true);
            }
        }
        private void RefreshActesGrouepement()
        {

            treeviewActes.Clear();
            foreach (FamillesActe f in ActesMgmt.famillesactegrp)
                if (f.ParentFamillesActeId == -1)
                    RefreshFamilies(treeviewActes.Root.Nodes, f);


            treeviewActes.RecalculEmplacementButtons(true);
            if (_ChoixFamille != null && _ChoixFamille != "")
            {
                foreach (trButton b in treeviewActes.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeviewActes.CurrentNode = ((TreeNode)b.Tag);
                    }
                }
                treeviewActes.RecalculEmplacementButtons(true);
            }
        }
        private void InitDisplayGroupementTraitement()
        {
            RefreshActesGrouepement();

            treeviewActes.ButtonPaint += new PaintEventHandler(PaintActes);
            treeviewActes.HeaderPaint += new PaintEventHandler(PaintHeader);
        }
        private void InitDisplayGroupement()
        {
            RefreshActes();

            treeviewActes.ButtonPaint += new PaintEventHandler(PaintActes);
            treeviewActes.HeaderPaint += new PaintEventHandler(PaintHeader);
            totaleActeSupp = 0;

            foreach (object ac in acteg)
            {
                ActeGroupement test = (ActeGroupement)ac;
                int idx1 = test.acte_libelle.IndexOf("\n");
                if (idx1 > -1)
                    test.acte_libelle = test.acte_libelle.Remove(idx1, test.acte_libelle.Length - idx1);
                treeviewActes.Alimenter_listBox(test.acte_libelle, test.id_acte + "$" + test.acte_libelle + "$" + test.idParent + "$" + test.acte_couleur.A + "$" + test.acte_couleur.R + "$" + test.acte_couleur.G + "$" + test.acte_couleur.B + "$" + test.acte_durestd + "$" + test.prix_acte + "$" + test.prixTraitement + "$" + test.qte + "$" + false, false);

            }
            totaleActeSupp += treeviewActes.totaleActeSupp;
            label1.Text = "Total: " + totaleActeSupp.ToString("C2");
        }
        private void InitDisplay()
        {
            RefreshActes();

            treeviewActes.ButtonPaint += new PaintEventHandler(PaintActes);
            treeviewActes.HeaderPaint += new PaintEventHandler(PaintHeader);
            if (treeviewActes.MultiChoiceVisibilite)
            {
                totaleActeSupp = 0;
                foreach (object comm in comms)
                {
                    CommActesTraitement test = (CommActesTraitement)comm;
                    int idx1 = test.LibActe.IndexOf("\n");
                    if (idx1 > -1)
                        test.LibActe = test.LibActe.Remove(idx1, test.LibActe.Length - idx1);
                    treeviewActes.Alimenter_listBox(test.LibActe, test.IdActe + "$" + test.LibActe + "$" + test.Parent + "$" + test.acte_couleur.A + "$" + test.acte_couleur.R + "$" + test.acte_couleur.G + "$" + test.acte_couleur.B + "$" + test.acte_durestd + "$" + test.prix_acte + "$" + test.prix_traitement + "$" + test.Qte + "$" + test.desactive, false);

                }
                totaleActeSupp += treeviewActes.totaleActeSupp;
                label1.Text = "Total: " + totaleActeSupp.ToString("C2");
            }
        }

        private void FrmActeTraitement_Load(object sender, EventArgs e)
        {
            // InitDisplay();
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ChangeItems == false)
                ChangeItems = treeviewActes.ChangeItems;

            if (treeviewActes.MultiChoiceVisibilite)
            {

                if (isActeGroupement || isActeGroupementTraitement)
                    acteg.Clear();
                else
                    comms.Clear();

                ListBox listBoxMC = treeviewActes.Controls.Find("listBox1", true).FirstOrDefault() as ListBox;

                foreach (var listBoxItem in listBoxMC.Items)
                {


                    BaseCommonControls.Ctrls.ListBoxItem item = (BaseCommonControls.Ctrls.ListBoxItem)listBoxItem;
                    //  MessageBox.Show(item.Text + "**" + item.Tag);
                    if (item.Text != "")
                    {
                        string[] words = item.Tag.ToString().Split('$');
                        if (isActeGroupement || isActeGroupementTraitement)
                        {

                            ActeGroupement tmpActeG = null;
                            tmpActeG = new ActeGroupement(ActesMgmt.Actes.Find(x => x.id_acte == Int32.Parse(words[0])));
                            tmpActeG.prixTraitement = Convert.ToDouble(words[9]);
                            tmpActeG.qte = Convert.ToInt32(words[10]);
                            tmpActeG.idParent = _idParent;
                            acteg.Add(tmpActeG);

                        }
                        else
                        {

                            CommActesTraitement cr;
                            cr = new CommActesTraitement();
                            cr.acte_durestd = Convert.ToInt32(words[7]);
                            cr.LibActe = item.Name;
                            cr.ShortLib = item.Name;
                            // treenode node = treeviewmateriels.selectednode;
                            cr.IdActe = Int32.Parse(words[0]);
                            //cr.idmateriel  =  (treeviewmateriels .selectednode .tag).id_materiel;
                            //cr.Parent = Parent;
                            cr.prix_acte = Convert.ToDouble(words[8]);
                            cr.prix_traitement = Convert.ToDouble(words[9]);
                            cr.Qte = Convert.ToInt32(words[10]);
                            cr.desactive = Convert.ToBoolean(words[words.Length - 1]);
                            if (cr.desactive)
                                cr.echeancestemp = 0;
                            cr.acte_couleur = Color.FromArgb(Int32.Parse(Regex.Match(words[3], @"\d+").Value), Int32.Parse(Regex.Match(words[4], @"\d+").Value), Int32.Parse(Regex.Match(words[5], @"\d+").Value), Int32.Parse(Regex.Match(words[6], @"\d+").Value));
                            comms.Add(cr);
                        }


                    }
                }
            }

            else
            {
                if (treeviewActes.SelectedNode == null)
                {
                    MessageBox.Show("Vous devez choisir un acte");
                    return;
                }
                if (treeviewActes.SelectedNode.Tag is FamillesActe)
                {
                    MessageBox.Show("Vous devez choisir un acte");
                    return;
                }
                else
                {
                    Acte TmpActe = ((Acte)treeviewActes.SelectedNode.Tag);
                    FrmString frmActe = new FrmString("Quantité", "Quantité", TmpActe.quantite);
                    string vquantite = TmpActe.quantite;
                    if (frmActe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        vquantite = frmActe.Value;

                    }

                    ActePGPropose TmpActePG = new ActePGPropose();
                    TmpActePG.MontantAvantRemise = TmpActe.prix_acte;
                    FrmRistourne frmR = new FrmRistourne(TmpActePG);
                    double newprix = TmpActe.prix_acte;
                    if (frmR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        newprix = frmR.Value;
                    }

                    //((Acte)treeviewActes.SelectedNode.Tag).quantite = vquantite;

                    ActeTraitement ff = new ActeTraitement((Acte)treeviewActes.SelectedNode.Tag);
                    ff.prix_traitement = newprix;
                    ff.quantite = vquantite;
                    Value = (treeviewActes.SelectedNode == null) ? null : ff; //((Acte)cbxActes.SelectedItem)
                }
            }




            DialogResult = DialogResult.OK;
            Close();
        }

        private void treeviewActes_OnSelected(object sender, EventArgs e)
        {

            if (treeviewActes.MultiChoiceVisibilite)
            {
                if (isActeGroupementTraitement)
                {
                    FamillesActe TmpActe = ((FamillesActe)treeviewActes.SelectedNode.Tag);
                    if (ActesMgmt.ActesGroupement.Find(w => w.idParent == TmpActe.Id) != null)
                    {
                        foreach (ActeGroupement acg in ActesMgmt.ActesGroupement.FindAll(w => w.idParent == TmpActe.Id))
                        {
                            treeviewActes.Alimenter_listBox(acg.acte_libelle, acg.id_acte.ToString() + "$" + acg.acte_libelle + "$" + Parent + "$" + acg.acte_couleur.A + "$" + acg.acte_couleur.R + "$" + acg.acte_couleur.G + "$" + acg.acte_couleur.B + "$" + acg.acte_durestd + "$" + acg.prix_acte + "$" + acg.prixTraitement + "$" + acg.qte + "$" + "False", false);
                        }
                        totaleActeSupp = treeviewActes.totaleActeSupp;
                        label1.Text = "Total: " + totaleActeSupp.ToString("C2");
                    }
                }
                else
                    if (treeviewActes.SelectedNode.FirstNode == null)
                    {
                        try
                        {

                            Acte TmpActe = ((Acte)treeviewActes.SelectedNode.Tag);
                            FrmString frm = new FrmString("Quantité", "Quantité", TmpActe.quantite);
                            vquantite = TmpActe.quantite;
                            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                vquantite = frm.Value;

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
                            string libelle = treeviewActes.SelectedNode.Text;
                            int idx1 = libelle.IndexOf("\n");
                            if (idx1 > -1)
                                libelle = libelle.Remove(idx1, libelle.Length - idx1);
                            if (!treeviewActes.Alimenter_listBox(libelle, TmpActe.id_acte.ToString() + "$" + libelle + "$" + Parent + "$" + TmpActe.acte_couleur.A + "$" + TmpActe.acte_couleur.R + "$" + TmpActe.acte_couleur.G + "$" + TmpActe.acte_couleur.B + "$" + TmpActe.acte_durestd + "$" + TmpActe.prix_acte + "$" + newprix + "$" + vquantite + "$" + "False", false))
                            {
                                MessageBox.Show("Nombre limite des actes supplémentaires , est atteint!");
                            }
                            else
                            {
                                ChangeItems = true;
                            }

                            totaleActeSupp = treeviewActes.totaleActeSupp;
                            label1.Text = "Total: " + totaleActeSupp.ToString("C2");
                           

                        }
                        catch (System.Exception ex)
                        {

                            MessageBox.Show("Famille sans Actes!" + ex.Message);
                        }

                    }

            }

            else
            {
                if (treeviewActes.SelectedNode.FirstNode == null)
                {

                    ChangeItems = true;
                }

            }
        }

        private void FrmActeTraitement_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeviewActes_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeviewActes_Remove(object sender, EventArgs e)
        {
            label1.Text = "Total: " + treeviewActes.totaleActeSupp.ToString("C2");
        }


        private bool isActeGroupement = false;
        private bool isActeGroupementTraitement = false;

        private void treeviewActes_Load(object sender, EventArgs e)
        {

        }
    }
}
