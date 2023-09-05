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
//using BASEPractice_BO;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls.Ctrls;
using BaseCommonControls.Ctrls.BO;
using System.Text.RegularExpressions;


namespace BaseCommonControls
{
    public partial class FrmChoiceMateriel : Form
    {
        private CommTraitement  _Parent;
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
        public Boolean ChangeItems;
        private List<CommMaterielTraitement> _comms;
        public List<CommMaterielTraitement> comms
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

        private string lblidmat ;
        public string idMat
        {
            get
            {
                return lblidmat;
            }
            set
            {
                lblidmat = value;
            }
        }

        public FrmChoiceMateriel(CommTraitement com, int MultiChoix = 0,bool isCreated= false)
        {
           
            Parent = com;
            comms = com.Materiels ;
            InitializeComponent();
            treeviewMateriels.isCreated = isCreated;
            //if (MultiChoix == 0)
            //{
            //    treeviewMateriels.MultiChoiceVisibilite = false;
            //    this.Width = 338;
            //    this.Height = 493;
            //}
            //else
            //{
            //    treeviewMateriels.MultiChoiceVisibilite = true;

            //    this.Width = 500;
            //    this.Height = 493;
            //}
        }

        private void AffecteMaterielsToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesMateriels)
                {
                    foreach (Materiel  m in MaterielsMgmt .Materiels )
                    {
                        if (m.famille_Materiel != null)
                        {
                            FamillesMateriels TF = new FamillesMateriels();
                            TF = (FamillesMateriels)node.Tag;
                            if (m.famille_Materiel.libelle.Trim() == TF.libelle.Trim() && m.famille_Materiel.ordre == TF.ordre)


                            {

                            //if (m.famille_Materiel == node.Tag)
                            //{
                                TreeNode MaterielNode = new TreeNode();
                                MaterielNode.Text = m.materiel_shortlib;
                                MaterielNode.ToolTipText = m.materiel_libelle;
                                
                                //  MaterielNode.Tag = m.id_materiel;
                                node.Nodes.Add(MaterielNode);
                               
                                //TreeNode MaterielNode = node.Nodes.Add( m.materiel_shortlib  );
                                //MaterielNode.ToolTipText = m.materiel_libelle;
                                MaterielNode.Tag = m;
                            }
                        }
                    }
                    AffecteMaterielsToNode (node.Nodes);
                }
            }

        }


        private void RefreshFamilies(TreeNodeCollection collec, FamillesMateriels f)
        {


            TreeNode familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesMateriels fm in f.ChildFamillesMateriel)
                RefreshFamilies(familynode.Nodes, fm);
        }

        private void RefreshMateriels()
        {

            treeviewMateriels.Clear();



            foreach (FamillesMateriels f in MaterielsMgmt .famillesmateriel)
                if (f.ParentFamillesMaterielId  == -1)
                    RefreshFamilies(treeviewMateriels.Root.Nodes, f);

            AffecteMaterielsToNode (treeviewMateriels.Root.Nodes);

            foreach (Materiel  m in MaterielsMgmt .Materiels )
            {
                if ((m.famille_Materiel  == null) && (m.id_materiel != -1))
                {
                    TreeNode MaterielNode = treeviewMateriels .Root.Nodes.Add(m.materiel_libelle + "\n" + m.prix_materiel);
                    MaterielNode .Tag = m;
                }
            }

            treeviewMateriels.RecalculEmplacementButtons(true);


        }

        private void InitDisplay()
        {
            RefreshMateriels();
            ChangeItems = false;
            treeviewMateriels.ButtonPaint += new PaintEventHandler(PaintMateriel );
            treeviewMateriels.HeaderPaint += new PaintEventHandler(PaintHeader);

                foreach (object comm in comms)
            {
                CommMaterielTraitement test = (CommMaterielTraitement)comm;
              //  MessageBox.Show(test.Libelle .ToString ());
                treeviewMateriels.Alimenter_listBox(test.ShortLib, test.idMateriel + "$" + test.Libelle + "$" + test.Parent + "$" + test.materiel_couleur.A + "$" + test.materiel_couleur.R + "$" + test.materiel_couleur.G + "$" + test.materiel_couleur.B   + "$" + " "  + "$" + test.prix_materiel + "$" + test.prix_traitement + "$"  +  test.Qte + "$" + test.desactive,false);
                    //Color.FromArgb(128, 210, 210, 255));
          
                }
        }
         
        private void FrmChoiceMateriel_Load(object sender, EventArgs e)
        {
            InitDisplay();

            
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
            if (node.Tag is FamillesMateriels)
                cl = ((FamillesMateriels)node.Tag).couleur;

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

        private void PaintMateriel(object sender, PaintEventArgs e)
        {

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.Word;

            TreeNode node = ((TreeNode)sender);
            Color cl = Color.WhiteSmoke;

            bool IsSelected = false;

            IsSelected = (node == treeviewMateriels.SelectedNode);

            if (node.Tag is Materiel )
                cl = ((Materiel )node.Tag).materiel_couleur;
            if (node.Tag is FamillesMateriels)
                cl = ((FamillesMateriels)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            if (node.Tag is FamillesMateriels)
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
                ChangeItems = treeviewMateriels.ChangeItems;
            comms.Clear();
            ListBox listBoxMC = treeviewMateriels.Controls.Find("listBox1", true).FirstOrDefault() as ListBox;
            
            foreach (var listBoxItem in listBoxMC .Items )
            {
                BaseCommonControls.Ctrls.ListBoxItem item = (BaseCommonControls.Ctrls.ListBoxItem)listBoxItem;
                if (item.Text != "")
                {
                    string[] words = item.Tag.ToString().Split('$');
                    CommMaterielTraitement cr;
                    cr = new CommMaterielTraitement();
                    cr.Libelle = words[1];
                    cr.ShortLib = item.Name ;
                    cr.idMateriel = Int32.Parse(words[0]);
                    cr.prix_materiel = Convert.ToDouble(words[8]);
                    cr.prix_traitement = Convert.ToDouble(words[9]);
                    cr.materiel_couleur = Color.FromArgb(Int32.Parse(Regex.Match(words[3], @"\d+").Value), Int32.Parse(Regex.Match(words[4], @"\d+").Value), Int32.Parse(Regex.Match(words[5], @"\d+").Value), Int32.Parse(Regex.Match(words[6], @"\d+").Value));
                    cr.Qte = Convert.ToInt32 (words [10]);
                    cr.desactive = Convert.ToBoolean(words[words.Length - 1]);
                    comms.Add(cr);
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void treeviewMateriels_Load(object sender, EventArgs e)
        {

        }

        private void treeviewMateriels_select(object sender, EventArgs e)
        {
            if (treeviewMateriels.MultiChoiceVisibilite)
            {
                if (treeviewMateriels.SelectedNode.Tag is Materiel )
                {
                    FrmString frm = new FrmString("Quantité", "Quantité", ((Materiel)treeviewMateriels.SelectedNode.Tag).Qte.ToString ());
                    string vquantite = ((Materiel)treeviewMateriels.SelectedNode.Tag).Qte.ToString ();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        vquantite = frm.Value;

                    }
                    ActePGPropose TmpActePG = new ActePGPropose();
                    TmpActePG.MontantAvantRemise = ((Materiel)treeviewMateriels.SelectedNode.Tag).prix_materiel;
                    FrmRistourne frmR = new FrmRistourne(TmpActePG);
                    double newprix = ((Materiel)treeviewMateriels.SelectedNode.Tag).prix_materiel;
                    if (frmR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        newprix = frmR.Value;
                    }
                    treeviewMateriels.Alimenter_listBox(treeviewMateriels.SelectedNode.Text, ((Materiel)treeviewMateriels.SelectedNode.Tag).id_materiel.ToString() + "$" + treeviewMateriels.SelectedNode.ToolTipText + "$" + Parent + "$" + ((Materiel)treeviewMateriels.SelectedNode.Tag).materiel_couleur.A + "$" + ((Materiel)treeviewMateriels.SelectedNode.Tag).materiel_couleur.R + "$" + ((Materiel)treeviewMateriels.SelectedNode.Tag).materiel_couleur.G + "$" + ((Materiel)treeviewMateriels.SelectedNode.Tag).materiel_couleur.B + "$" + " " + "$" + ((Materiel)treeviewMateriels.SelectedNode.Tag).prix_materiel + "$" + newprix + "$" + vquantite + "$" + "False", false);
                    ChangeItems = true;
                }
            }
            else
            {
                if (treeviewMateriels.SelectedNode.FirstNode == null)
                    ChangeItems = true;
            }
        }
     
    
    }
}
