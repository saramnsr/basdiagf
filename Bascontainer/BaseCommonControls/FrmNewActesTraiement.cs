using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;
using System.Globalization;
using System.Drawing.Drawing2D;
using BaseCommonControls.Ctrls;
using System.Text.RegularExpressions;
using BaseCommonControls.Ctrls.BO;

namespace BaseCommonControls
{
    public partial class FrmNewActesTraitement : Form
    {

        double PrixUnitaire;



        private Surveillance _NewSurveillance = null;
        public Surveillance NewSurveillance
        {
            get
            {
                return _NewSurveillance;
            }
            set
            {
                _NewSurveillance = value;
            }
        }

        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }

        private ActePG _CurrentActe = null;
        public ActePG CurrentActe
        {
            get
            {
                return _CurrentActe;
            }
            set
            {
                _CurrentActe = value;
            }
        }
        private List<CommActes> _comms = new List<CommActes>();
        public List<CommActes> comms
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
        private List<CommMateriel> _commMateriel = new List<CommMateriel>();
        public List<CommMateriel> commMateriel
        {
            get
            {
                return _commMateriel;
            }
            set
            {
                _commMateriel = value;
            }
        }
        private CommTraitement _comm = new CommTraitement();
        public CommTraitement comm
        {
            get
            {
                return _comm;
            }
            set
            {
                _comm = value;
            }
        }
        private DataGridViewCellStyle _PayedStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle DesactiveStyle
        {
            get
            {
                return _PayedStyle;
            }
            set
            {
                _PayedStyle = value;
            }
        }

        private DataGridViewCellStyle _NormalStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle NormalStyle
        {
            get
            {
                return _NormalStyle;
            }
            set
            {
                _NormalStyle = value;
            }
        }
        string vFamilleDefaut = "";
        string valueCell = "";
        private void  enableTab(TabPage tab)
        {
            foreach (TabPage ctl in tabControl1.TabPages)
            {
                if (tab != ctl)
                    tabControl1.Controls.Remove((Control)ctl);
         
            }
        }
        public bool isTextBox { get; set; }

        public bool isDatagrid { get; set; }

        public bool espece { get; set; }

        public bool carte { get; set; }
        public DataGridView.HitTestInfo selectedRows { get; set; }
        public bool isAchats { get; set; }

        public bool isActe { get; set; }

        public bool isActeSupp { get; set; }
        public bool isGrpActes { get; set; }

        public bool isBlanchiment { get; set; }
        public int _TypeActe { get; set; }

        public bool isPhoto { get; set; }

        public bool isRadio { get; set; }
        public bool ChangeItems { get; set; }
        bool _isTraitement = false;
        public FrmNewActesTraitement(CommTraitement com, string familleDefaut, int typeActe,bool isTraitement)
        {
            _isTraitement = isTraitement;
            NormalStyle.ForeColor = Color.Black;
            DesactiveStyle.ForeColor = Color.Gray;
            DesactiveStyle.Font = new Font("garamond", 12, FontStyle.Strikeout);
            vFamilleDefaut = familleDefaut;
            CurrentPatient = com.patient;
            InitializeComponent();
            _TypeActe = typeActe;
            comm = com;
            
            treeviewMateriels.MultiChoiceVisibilite = false;
            treeViewActe.MultiChoiceVisibilite = false;
            treeActeSupp.MultiChoiceVisibilite = false;
            treeViewRadio.MultiChoiceVisibilite = false;
            treeViewPhoto.MultiChoiceVisibilite = false;
            InitDisplay();
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            dataGridView1.ClearSelection();
     
        }

        private void InitDisplay()
        {
          
            switch (_TypeActe)
            {
                case 0: isActe = true; enableTab(tabActe); InitDisplay(treeViewActe); initDGV(comm); break;
                case 1: isActeSupp = true; enableTab(tabActeSupp); InitDisplay(treeActeSupp); foreach (CommActesTraitement c in comm.ActesSupp) initDGVActeSupp(c); break;
                case 3: isPhoto = true; enableTab(tabPhoto); InitDisplay(treeViewPhoto); foreach (CommActesTraitement c in comm.photos) initDGVActeSupp(c); break;
                case 2: isRadio = true; enableTab(tabRadio); InitDisplay(treeViewRadio); foreach (CommActesTraitement c in comm.Radios) initDGVActeSupp(c); break;
                case 4: isAchats = true; enableTab(tabMateriels); InitDisplay(treeviewMateriels); foreach (CommMaterielTraitement c in comm.Materiels) initDGVMateriel(c); break;
                case 5: isActe = true;  InitDisplay(treeViewActe); break;

               // case 6: isGrpActes = true; enableTab(tabGrpActes); InitDisplay(treeActeSupp); foreach (CommActesTraitement c in comm.ActesSupp) initDGVActeSupp(c); break;
            }
            
        }



        private void BuildandClose()
        {
            try
            {
                try
                {
                    BuildActePG();
                }
                catch (System.ArgumentException ex)
                {
                    if (MessageBox.Show(ex.Message + "\nSouhaitez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void refreshlabesls()
        {
            double total  = 0; 
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Tag is CommTraitement)
                {         
                        total += ((CommTraitement)r.Tag).Acte.prix_traitement;
                }
                else
                    if (r.Tag is CommActesTraitement)
                    {
                        if (!((CommActesTraitement)r.Tag).desactive)
                                total += ((CommActesTraitement)r.Tag).prix_traitement;
                    }
                    else
                if (r.Tag is CommMaterielTraitement)
                    {
                         total += ((CommMaterielTraitement)r.Tag).prix_materiel_traitement;
                    }
                       

            }
            mtTotal.Text = total.ToString("C2");
               
        }
        void initDGV(CommTraitement com)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Tag is CommTraitement)
                    if (((CommTraitement)r.Tag).Acte.id_acte == com.Acte.id_acte)
                {
                    int tmpq = Convert.ToInt32(r.Cells[0].Value);
                    tmpq++;
                    r.Cells[0].Value = tmpq.ToString();
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = r.Cells[0];                   
                    refresh();
                    ChangeItems = true;
                    return;
                }

            }
            object[] obj = new object[]
            {
               
               com.Acte.quantite ,
                com.Acte.acte_libelle,
                com.Acte.prix_traitement,               
                (com.Acte.prix_traitement * Convert.ToInt32(com.Acte.quantite)).ToString("C2"),
                DateTime.Now.ToString("dd/MM/yyyuy")

            };

            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = com;
            dataGridView1.Rows[rowidx].DefaultCellStyle.BackColor =com.Acte.acte_couleur;
            refreshlabesls();
        }
        void initDGVActeSupp(CommActesTraitement acte)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Tag is CommActesTraitement)
                    if (((CommActesTraitement)r.Tag).IdActe == acte.IdActe)
                {
                    int tmpq = Convert.ToInt32(r.Cells[0].Value);
                    tmpq++;
                    r.Cells[0].Value = tmpq.ToString();
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = r.Cells[0];
                    refresh();
                    ChangeItems = true;
                    return;
                }

            }
            object[] obj = new object[]
            {
               acte.Qte ,
                acte.LibActe,
                acte.prix_traitement,               
                (acte.prix_traitement * acte.Qte).ToString("C2"),
                DateTime.Now.ToString("dd/MM/yyyuy")

            };
            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = acte;
            dataGridView1.Rows[rowidx].DefaultCellStyle = acte.desactive == true ? DesactiveStyle : NormalStyle;
            dataGridView1.Rows[rowidx].DefaultCellStyle.BackColor = acte.acte_couleur;

            refreshlabesls();
        }
        void initDGVMateriel(CommMateriel materiel )
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Tag is CommMateriel)
                   if (((CommMateriel)r.Tag).idMateriel == materiel.idMateriel)
                    {
                        int tmpq = Convert.ToInt32(r.Cells[0].Value);
                        tmpq++;
                        r.Cells[0].Value = tmpq.ToString();
                        dataGridView1.ClearSelection();
                        dataGridView1.CurrentCell = r.Cells[0];
                        refresh();
                        ChangeItems = true;
                        return;
                    }
               
            }
            object[] obj = new object[]
            {
               materiel.Qte ,
                materiel.Libelle,
                materiel.prix_materiel,
                materiel.prix_materiel * materiel.Qte,
                   DateTime.Now.ToString("dd/MM/yyyuy")
            };
            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = materiel;
            dataGridView1.Rows[rowidx].DefaultCellStyle.BackColor = materiel.materiel_couleur;

            refreshlabesls();
        }
        private void treeviewMateriels_select(object sender, EventArgs e)
        {

            if (treeviewMateriels.SelectedNode.Level > 1)
            {

                CommMateriel commMateriel = new CommMateriel((Materiel)treeviewMateriels.SelectedNode.Tag);
                initDGVMateriel(commMateriel);
                ChangeItems = true;
                
            }

        }
        void BuildActePG()
        {
            lstAchats = new List<ActePG>();
            comms = new List<CommActes>();
            switch (_TypeActe)
            {
                case 1: comm.ActesSupp = new List<CommActesTraitement>(); break;
                case 2 : comm.Materiels = new List<CommMaterielTraitement>(); break;
                case 3: comm.Materiels = new List<CommMaterielTraitement>(); break;
               
            }
            
            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                if (rows.Tag is CommActesTraitement)
                {

                    CommActesTraitement tmpActesupp = (CommActesTraitement)rows.Tag;
                    string number = Regex.Replace(rows.Cells[2].Value.ToString(), "[^0-9.,+]", "").Replace('.', ',').Replace('.', ',');
                    tmpActesupp.prix_traitement = Convert.ToDouble(number);
                    tmpActesupp.Qte = Convert.ToInt32(rows.Cells[0].Value.ToString());
                    comm.ActesSupp.Add(tmpActesupp);
                }
                else
                    if (rows.Tag is CommTraitement)
                    {
                       
                        CommTraitement tmpComm = (CommTraitement)rows.Tag;
                        string number = Regex.Replace(rows.Cells[2].Value.ToString(), "[^0-9.,+]", "").Replace('.', ',').Replace('.', ',');
                        tmpComm.Acte.prix_traitement = Convert.ToDouble(number);
                        tmpComm.Acte.quantite = rows.Cells[0].Value.ToString();

                        comm = tmpComm;
                    }
                        
                    else
                    {
                        CommMaterielTraitement tmpMat = (CommMaterielTraitement)rows.Tag;
                        string number = Regex.Replace(rows.Cells[2].Value.ToString(), "[^0-9.,+]", "").Replace('.', ',').Replace('.', ',');
                        tmpMat.prix_materiel_traitement = Convert.ToDouble(number);
                        tmpMat.Qte = Convert.ToInt32(rows.Cells[0].Value.ToString());
                        comm.Materiels.Add(tmpMat);
                    }

       

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

      

        private void btnOk_Click(object sender, EventArgs e)
        {
      
         
                BuildandClose();
        }

 
        private void AffecteMaterielsToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesMateriels)
                {
                    foreach (Materiel m in MaterielsMgmt.Materiels)
                    {

                        FamillesMateriels TF = new FamillesMateriels();
                        TF = (FamillesMateriels)node.Tag;
                        if (m.famille_Materiel.libelle.Trim() == TF.libelle.Trim() && m.famille_Materiel.ordre == TF.ordre)
                        {
                            TreeNode MaterielNode = new TreeNode();
                            MaterielNode.Text = m.materiel_shortlib + "\n" + m.prix_materiel.ToString("C2");
                            MaterielNode.ToolTipText = m.materiel_libelle;
                            //  MaterielNode.Tag = m.id_materiel;
                            node.Nodes.Add(MaterielNode);

                            //TreeNode MaterielNode = node.Nodes.Add( m.materiel_shortlib  );
                            //MaterielNode.ToolTipText = m.materiel_libelle;
                            MaterielNode.Tag = m;
                        }
                        //if (m.famille_Materiel  == node.Tag)
                        //{

                        //}
                    }
                    AffecteMaterielsToNode(node.Nodes);
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
        private void RefreshMateriels(TreeViewIcon treeview)
        {

            treeview.Clear();



            foreach (FamillesMateriels f in MaterielsMgmt.famillesmateriel)
                if (f.ParentFamillesMaterielId == -1)
                    RefreshFamilies(treeview.Root.Nodes, f);

            AffecteMaterielsToNode(treeview.Root.Nodes);

            foreach (Materiel m in MaterielsMgmt.Materiels)
            {
                if ((m.famille_Materiel == null) && (m.id_materiel != -1))
                {
                    TreeNode MaterielNode = treeview.Root.Nodes.Add(m.materiel_libelle);
                    MaterielNode.Tag = m;
                }
            }

            treeview.RecalculEmplacementButtons(true);


        }
        private void PaintIcons(object sender, PaintEventArgs e)
        {

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.Word;
            GraphicsPath path;
            TreeNode node = ((TreeNode)sender);
            Color cl = Color.WhiteSmoke;

            bool IsSelected = false;

            if(isActeSupp || isActe || isPhoto || isRadio)
            {
                if (isActe)
                    IsSelected = (node == treeViewActe.SelectedNode);
                else
                    if (isActeSupp)
                        IsSelected = (node == treeActeSupp.SelectedNode);
                    else
                        if (isPhoto)
                            IsSelected = (node == treeViewPhoto.SelectedNode);
                        else
                            if (isRadio)
                                IsSelected = (node == treeViewRadio.SelectedNode);
            if (node.Tag is Acte)
                cl = ((Acte)node.Tag).acte_couleur;
            if (node.Tag is FamillesActe)
                cl = ((FamillesActe)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            // get path
            if (node.Tag is FamillesActe)
            {
                Rectangle r = e.ClipRectangle;
                r.X += 2;
                r.Y += 2;
               }
            }
            else
            {
                if(isAchats)
                IsSelected = (node == treeviewMateriels.SelectedNode);               

                if (node.Tag is Materiel)
                    cl = ((Materiel)node.Tag).materiel_couleur;
                if (node.Tag is FamillesMateriels)
                    cl = ((FamillesMateriels)node.Tag).couleur;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // get the corner path
                // get path
                if (node.Tag is FamillesMateriels)
                {
                    Rectangle r = e.ClipRectangle;
                    r.X += 2;
                    r.Y += 2;
                }
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
            if (isActe || isActeSupp || isPhoto || isRadio)
            {
                if (node.Tag is FamillesActe)
                    cl = ((FamillesActe)node.Tag).couleur;
            }
            else
            {
                if (node.Tag is FamillesMateriels)
                    cl = ((FamillesMateriels)node.Tag).couleur;
            }
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
        private void AffecteActesToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {
                if (node.Tag is FamillesActe)
                {
                    foreach (Acte a in ActesMgmt.Actes)
                    {
                        //if (a.famille_Acte == null) NonAffectedExist = true;
                        if (a.famille_Acte == node.Tag  && (a.famille_Acte.libelle.Trim() == vFamilleDefaut.Trim() || vFamilleDefaut == string.Empty))
                        {

                            TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString()));
                            ActeNode.Tag = a;
                        }
                    }
                    AffecteActesToNode(node.Nodes);
                }
            }



        }


        private void RefreshFamilies(TreeViewIcon treeview, TreeNodeCollection collec, FamillesActe f)
        {


            TreeNode familynode = collec.Add(f.libelle);
            familynode.Tag = f;
            if (f.libelle == vFamilleDefaut)
            {
                treeview.CurrentNode = familynode;

            }

            foreach (FamillesActe fa in f.ChildFamillesActe)
                RefreshFamilies(treeview,familynode.Nodes, fa);
        }
        string _ChoixFamille ="" ;
        private void RefreshActes(TreeViewIcon treeview)
        {
            treeview.Clear();



            foreach (FamillesActe f in ActesMgmt.famillesacte)
                if (f.ParentFamillesActeId == -1 && (f.libelle == vFamilleDefaut || vFamilleDefaut == string.Empty))
                    RefreshFamilies(treeview,treeview.Root.Nodes, f);

            AffecteActesToNode(treeview.Root.Nodes);

            foreach (Acte a in ActesMgmt.Actes)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Acte == null) && (a.id_acte != -1))
                {
                    TreeNode ActeNode = treeview.Root.Nodes.Add(a.acte_libelle);
                    ActeNode.Tag = a;
                }
            }

            treeview.RecalculEmplacementButtons(true);
            if (_ChoixFamille != "")
            {
                foreach (trButton b in treeview.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeview.CurrentNode = ((TreeNode)b.Tag);
                    }
                }
                treeview.RecalculEmplacementButtons(true);
            }
        }
        void InitDisplay(TreeViewIcon treeview)
        {


            if (isAchats || isBlanchiment)
                RefreshMateriels(treeview);
            else
                RefreshActes(treeview);
            treeview.ButtonPaint += new PaintEventHandler(PaintIcons);
            treeview.HeaderPaint += new PaintEventHandler(PaintHeader);
            



        }


        private void FrmNewAchat_Load(object sender, EventArgs e)
        {
            InitDisplay(treeViewActe);

            NumberFormatInfo nfi = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

            lst.Add(new BaseCommonControls.FamilyValue("", "Offert", 100));
            lst.Add(new BaseCommonControls.FamilyValue("", "5%", 5));
            lst.Add(new BaseCommonControls.FamilyValue("", "10%", 10));
            lst.Add(new BaseCommonControls.FamilyValue("", "15%", 15));
            lst.Add(new BaseCommonControls.FamilyValue("", "20%", 20));
            lst.Add(new BaseCommonControls.FamilyValue("", "25%", 25));
            lst.Add(new BaseCommonControls.FamilyValue("", "30%", 30));
            lst.Add(new BaseCommonControls.FamilyValue("", "35%", 35));
            lst.Add(new BaseCommonControls.FamilyValue("", "40%", 40));
            lst.Add(new BaseCommonControls.FamilyValue("", "45%", 45));
            lst.Add(new BaseCommonControls.FamilyValue("", "50%", 50));
            lst.Add(new BaseCommonControls.FamilyValue("", "Normal", 0));

            slidingList1.LoadFromFamilyValueList(lst);
        }



        private void pnlSesamVital_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
        }

        private void btnEnregistreretEncaisser_Click(object sender, EventArgs e)
        {
        }

        private void pnlSumaries_Paint(object sender, PaintEventArgs e)
        {

        }


        private void chkbxPrelevement_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxVirement_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtbxQte_TextChanged(object sender, EventArgs e)
        {


        }

        private void pnlGeneralInformations_Paint(object sender, PaintEventArgs e)
        {

        }



        private void sldLstActes_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }



        private void lblSummaries_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void setButton(int Number)
        {
            if (isDatagrid)
            {
                //Update our New Cell Value Data
                if (dataGridView1.CurrentCell != null && (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 0))
                {



                    if (dataGridView1.CurrentCell.ColumnIndex == 2)
                    {
                        string ancienMontant = Regex.Replace(dataGridView1.CurrentCell.EditedFormattedValue.ToString(), "[^0-9.,+]", "").Replace('.', ',');
                       if (selectLength == 0)
                        {
                            string newvalue = ancienMontant.Insert(selectStart, Number.ToString());
                            dataGridView1.CurrentCell.Value = Convert.ToDouble(newvalue).ToString("C2");
                            selectStart++;

                        }
                        else
                        {
                            dataGridView1.CurrentCell.Value = Number;

                            //MessageBox.Show("Number incorrect");
                        }
                    }

                    else
                    {
                        if(selectLength == 0)
                        dataGridView1.CurrentCell.Value += Number.ToString();
                        else
                            dataGridView1.CurrentCell.Value = Number;
                    }

                    selectLength = 0;

                }
                refresh();
                ChangeItems = true;
            }
       
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            setButton(1);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            setButton(2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setButton(3);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            setButton(4);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            setButton(5);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            setButton(6);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            setButton(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setButton(8);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            setButton(9);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void slidingList1_OnSelectionChange(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            refreshlabesls();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            setButton(0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.BeginEdit(false);
                selectStart = ((TextBox)dataGridView1.EditingControl).SelectionStart;
                selectLength = ((TextBox)dataGridView1.EditingControl).SelectionLength;
            }
            catch (Exception exx)
            {
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell.Value != null)
            valueCell = dataGridView1.CurrentCell.Value.ToString();
            isTextBox = false;
            isDatagrid = true;

            //  dataGridView1.CurrentCell.Value = "";
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString() == String.Empty)
                dataGridView1.CurrentCell.Value = valueCell;
           refresh();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            refresh();
         
        }

        public bool verifModif { get; set; }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
       
                
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null &&  dataGridView1.CurrentCell.ColumnIndex == 2)
                dataGridView1.CurrentCell.Value = 0;
            else
              if(  dataGridView1.CurrentCell != null &&  dataGridView1.CurrentCell.ColumnIndex == 0)
                   dataGridView1.CurrentCell.Value = 1;
           
           
        }

        public int selectStart { get; set; }
        private void dataGridView1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {

            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 3) //Desired Column
            {
                e.Control.ContextMenuStrip = contextMenuStrip1;
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
         
            selectStart = ((TextBox)dataGridView1.EditingControl).SelectionStart;

        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && dataGridView1.CurrentCell.ColumnIndex != 0 && dataGridView1.CurrentCell.ColumnIndex != 3)
            {
                e.Handled = true;
            }
           
        }
        private void refresh()
        {
            
            if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                string number = Regex.Replace(dataGridView1.CurrentRow.Cells[2].EditedFormattedValue.ToString(), "[^0-9.,+]", "").Replace('.', ',');
                if (dataGridView1.CurrentCell.ColumnIndex == 2)
                    dataGridView1.CurrentCell.Value = Convert.ToDouble(number).ToString("C2");
                else
                    dataGridView1.CurrentCell.Value = dataGridView1.CurrentCell.EditedFormattedValue;
                double montant = (Convert.ToDouble(number) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[0].Value));
                dataGridView1.CurrentRow.Cells[3].Value = montant.ToString("C2");
                ChangeItems = true;
                refreshlabesls();
            }
        }
        public int selectLength { get; set; }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        public List<ActePG> lstAchats { get; set; }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
  
        }

        private void button16_Click(object sender, EventArgs e)
        {
          
        }

        private void button17_Click(object sender, EventArgs e)
        {
        
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dataGridView1.CurrentCell = null;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
          //  textBox1.Text = string.Format("C2", textBox1.Text);
            isTextBox = true;
            isDatagrid = false;
        }

      

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void supprimerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag == comm)
                comm.Acte = null;
                this.dataGridView1.Rows.RemoveAt(selectedRows.RowIndex);
                if (comm.Acte == null)
                    btnOk.Visible = false;
                ChangeItems = true;
                refreshlabesls();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectedRows = dataGridView1.HitTest(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right && selectedRows.RowIndex > -1)
            {
                supprimerToolStripMenuItem.Visible = true;
                if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommTraitement || this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommMaterielTraitement || _isTraitement)
                    deToolStripMenuItem.Visible = false;
                else
                    deToolStripMenuItem.Visible = true;
                if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommActesTraitement)
                deToolStripMenuItem.Text = ((CommActesTraitement)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive == true ? "Activer" : "Désactiver";

            }
            else
            {
                deToolStripMenuItem.Visible = false;
                supprimerToolStripMenuItem.Visible = false;
            }
        }

        private void slidingList1_OnSelectionChange_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                string number = "";
                if (dataGridView1.CurrentRow.Tag is CommMaterielTraitement)
                    number = MaterielsMgmt.Materiels.Find(x => x.id_materiel == ((CommMaterielTraitement)dataGridView1.CurrentRow.Tag).idMateriel).prix_materiel.ToString();
                else
                    if(dataGridView1.CurrentRow.Tag is CommTraitement)
                        number = ActesMgmt.Actes.Find(x => x.id_acte == ((CommTraitement)dataGridView1.CurrentRow.Tag).IdActe).prix_acte.ToString();
                    else
                    number = ActesMgmt.Actes.Find(x => x.id_acte == ((CommActesTraitement)dataGridView1.CurrentRow.Tag).IdActe).prix_acte.ToString();
                dataGridView1.CurrentCell.Value = (Convert.ToDouble(slidingList1.SelectedItems[0].Tag) * Convert.ToDouble(number)) / (double )100 - Convert.ToDouble(number);
                ChangeItems = true;
                refresh();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAchats = false;
            isActe = false;
            isActeSupp = false;
            isPhoto = false;
            isRadio = false;
            switch (tabControl1.SelectedIndex)
            {
                case 0: isActe = true; vFamilleDefaut = "";  InitDisplay(treeViewActe); if (comm.Acte == null) comm.Acte = new ActeTraitement(); initDGV(comm); break;
                case 1: isActeSupp = true; vFamilleDefaut = "";  InitDisplay(treeActeSupp); if (comm.ActesSupp == null) comm.ActesSupp = new List<CommActesTraitement>(); foreach (CommActesTraitement c in comm.ActesSupp) initDGVActeSupp(c); break;
                case 4: isAchats = true; vFamilleDefaut = ""; InitDisplay(treeviewMateriels); if (comm.Materiels == null) comm.Materiels = new List<CommMaterielTraitement>(); foreach (CommMaterielTraitement c in comm.Materiels) initDGVMateriel(c); break;
                case 3: isPhoto = true; vFamilleDefaut = "Photos"; InitDisplay(treeViewPhoto); if (comm.photos == null) comm.photos = new List<CommActesTraitement>(); foreach (CommActesTraitement c in comm.photos) initDGVActeSupp(c); break;
                case 2: isRadio = true; vFamilleDefaut = "radio"; InitDisplay(treeViewRadio); if (comm.Radios == null) comm.Radios = new List<CommActesTraitement>(); foreach (CommActesTraitement c in comm.Radios) initDGVActeSupp(c); break;
            }

        }


        private void treeViewActe_OnSelected(object sender, EventArgs e)
        {
            if (treeViewActe.SelectedNode.Level > 1)
            {

                if (comm.Acte != null)
                {
                    MessageBox.Show("vous pouvez pas ajouter deux actes principale");
                    return;
                }
                comm.Acte = new ActeTraitement((Acte)treeViewActe.SelectedNode.Tag);
                comm.prix = ((Acte)treeViewActe.SelectedNode.Tag).prix_acte;              
                initDGV(comm);
                btnOk.Visible = true;
                ChangeItems = true;
            }
        }

        private void treeActeSupp_OnSelected(object sender, EventArgs e)
        {
            if (treeActeSupp.SelectedNode.Level > 1)
            {
                CommActesTraitement commActe = new CommActesTraitement((Acte)treeActeSupp.SelectedNode.Tag);
                initDGVActeSupp(commActe);
                ChangeItems = true;
            }
        }

        private void treeViewBlanchiment_OnSelected(object sender, EventArgs e)
        {
            if (treeViewRadio.SelectedNode.Level > 1)
            {


                CommActesTraitement commRadio = new CommActesTraitement((Acte)treeViewRadio.SelectedNode.Tag);
                initDGVActeSupp(commRadio);
                ChangeItems = true;
            }
        }
   

        private void treeViewPhoto_OnSelected(object sender, EventArgs e)
        {
            if (treeViewPhoto.SelectedNode.Level > 1)
            {
                CommActesTraitement commPhoto = new CommActesTraitement((Acte)treeViewRadio.SelectedNode.Tag);
                initDGVActeSupp(commPhoto);
                ChangeItems = true;
            }
        }

        private void deToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommActesTraitement)
            {
                ((CommActesTraitement)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive = !((CommActesTraitement)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive;
                this.dataGridView1.Rows[selectedRows.RowIndex].DefaultCellStyle = ((CommActesTraitement)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive == true ? DesactiveStyle : NormalStyle;
                string ancienMontant = Regex.Replace(this.dataGridView1.Rows[selectedRows.RowIndex].Cells[2].Value.ToString(), "[^0-9.,+]", "").Replace('.', ',');
                  ((CommActesTraitement)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).prix_traitement = Convert.ToDouble(ancienMontant);
                  ChangeItems = true;
            }
       
            refreshlabesls();
        }

     
    }
}
