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
    public partial class FrmNewActe : Form
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
        private CommClinique _comm = new CommClinique();
        public CommClinique comm
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
        string vFamilleDefaut = "";
        string valueCell = "";
        private void enableTab(TabPage tab)
        {
            foreach (TabPage ctl in tabControl1.TabPages)
            {
                if (tab != ctl)
                    tabControl1.Controls.Remove((Control)ctl);

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

        public bool verifModif { get; set; }
        public bool isAchats { get; set; }

        public bool isActe { get; set; }
        public bool isActeGroupement { get; set; }

        public bool isActeSupp { get; set; }

        public bool isBlanchiment { get; set; }
        public int _TypeActe { get; set; }
        public bool isTextBoxVerse { get; set; }
        public bool isTextBoxPayer { get; set; }
        public bool isDatagrid { get; set; }

        public bool espece { get; set; }

        public bool carte { get; set; }
        public DataGridView.HitTestInfo selectedRows { get; set; }
        public int selectLength { get; set; }
        public int selectStart { get; set; }
        public FrmNewActe(CommClinique com, string familleDefaut, int typeActe)
        {


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
            treeViewBlanchiment.MultiChoiceVisibilite = false;
            treeViewGrpActes.MultiChoiceVisibilite = false;
            string currency = 0.ToString("C2").Substring(5, (0.ToString("C2").Length - 1) - 4);
            dataGridView1.ClearSelection();
            Currency.Text = currency;
            Currency1.Text = currency;
            ttverse.Hide();
            Currency.Hide();
            aRendre.Hide();
            label12.Hide();
            label13.Hide();
            InitDisplay();
            //  isActe = true;

            if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + baseMgmtPatient.prefix] == "FR"))
            {
               this.tabControl1.TabPages.Remove(tabActeSupp);
               this.tabControl1.TabPages.Remove(tabGrpActes);

            }
        }

        private void InitDisplay()
        {
            switch (_TypeActe)
            {
                case 0: isActe = true; enableTab(tabActe); break;
                case 1: isActeSupp = true; enableTab(tabActeSupp); break;
                case 2: isAchats = true; enableTab(tabAchats); break;
                case 3: isBlanchiment = true; enableTab(tabBlanchiment); break;
                case 6: isActeGroupement = true; enableTab(tabGrpActes); break;
            }
            switch (_TypeActe)
            {
                case 0: isActe = true; InitDisplay(treeViewActe); initDGV(comm); break;
                case 1: isActeSupp = true; InitDisplay(treeActeSupp); foreach (CommActes c in comm.ActesSupp) initDGVActeSupp(c); break;
                case 2: isAchats = true; InitDisplay(treeviewMateriels); vFamilleDefaut = System.Configuration.ConfigurationManager.AppSettings["FamilleAchatDefault"]; foreach (CommMateriel c in comm.Materiels) { if (c.Famille.libelle == vFamilleDefaut)initDGVMateriel(c); } break;
                case 3: isBlanchiment = true; InitDisplay(treeViewBlanchiment); vFamilleDefaut = System.Configuration.ConfigurationManager.AppSettings["FamilleBlanchimentDefault"]; foreach (CommMateriel c in comm.Materiels) { if (c.Famille.libelle == vFamilleDefaut) initDGVMateriel(c); } break;
                case 5: isActe = true; InitDisplay(treeViewActe); break;
                case 6: isActeGroupement = true; InitDisplay(treeViewGrpActes); break;
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

            if (dataGridView1.Rows.Count == 0) return;

            double total = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Tag is CommClinique)
                {
                    total += ((CommClinique)r.Tag).prix_traitement;
                }
                else
                    if (r.Tag is CommActes)
                    {
                        if (!((CommActes)r.Tag).desactive)
                            total += ((CommActes)r.Tag).prix_traitement;
                    }
                    else
                        if (r.Tag is CommMateriel)
                        {
                            total += ((CommMateriel)r.Tag).prix_materiel_traitement;
                        }


            }
            //  mtTotal.Text = Math.Round(total,2).ToString("C2");

            MtInit.Text = dataGridView1.Rows.Cast<DataGridViewRow>()
        .Sum(t => converttoDouble(t.Cells[2].Value.ToString()) * converttoDouble(t.Cells[0].Value.ToString())).ToString("C2");

            ttPayer.Text = Math.Round(dataGridView1.Rows.Cast<DataGridViewRow>().Where(x => ((x.Tag is CommActes && ((CommActes)x.Tag).desactive == false) || (x.Tag is CommClinique && ((CommClinique)x.Tag).desactive == false) || (x.Tag is CommMateriel && ((CommMateriel)x.Tag).desactive == false)))
             .Sum(t => converttoDouble(t.Cells[4].Value.ToString())), 1).ToString();
            mtTotal.Text = converttoDouble(ttPayer.Text).ToString("C2");
        }

        void initDGV(CommClinique com, bool init = true)
        {
            if (!init)
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Tag is CommClinique)
                        if (((CommClinique)r.Tag).Acte.id_acte == com.Acte.id_acte)
                        {
                            int tmpq = Convert.ToInt32(r.Cells[0].Value);
                            tmpq++;
                            r.Cells[0].Value = tmpq.ToString();
                            dataGridView1.ClearSelection();
                            dataGridView1.CurrentCell = r.Cells[0];
                            refresh();

                            return;
                        }

                }
            object[] obj = new object[]
            {
               com.Acte.quantite ,
                com.Acte.acte_libelle,
                com.Acte.prix_acte ,
                com.rabais,
                (com.prix_traitement * Convert.ToInt32(com.Acte.quantite)).ToString("C2"),

            };

            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = com;
            changeColorRow(dataGridView1.Rows[rowidx], com.Acte.acte_couleur);
            if (com.Id != 0 && com.Id != -1)
            {
                ActePG actepg = new ActePG();
                actepg = ActesPGMgmt.GetActesPGByIdComm(com.Id);
                if (actepg != null)
                {
                    actepg.lstEcheances = EcheancesMgmt.GetEcheances(actepg, true);
                    if (actepg.lstEcheances.Find(x => x.ID_Encaissement != -1) != null)
                    {
                        dataGridView1.Rows[rowidx].ReadOnly = true;
                    }
                }
            }

            refreshlabesls();
        }
        void changeColorRow(DataGridViewRow dr, Color color)
        {
            dr.DefaultCellStyle.BackColor = color;
        }
        void initDGVActeSupp(CommActes acte, bool init = true)
        {
            if (!init)
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Tag is CommActes)
                        if (((CommActes)r.Tag).IdActe == acte.IdActe)
                        {
                            int tmpq = Convert.ToInt32(r.Cells[0].Value);
                            tmpq++;
                            r.Cells[0].Value = tmpq.ToString();
                            dataGridView1.ClearSelection();
                            dataGridView1.CurrentCell = r.Cells[0];
                            refresh();

                            return;
                        }

                }
            object[] obj = new object[]
            {
               acte.Qte ,
                acte.LibActe,
                acte.prix_acte.ToString("C2"), 
                acte.rabais,
                (acte.prix_traitement).ToString("C2")
               

            };
            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = acte;
            dataGridView1.Rows[rowidx].DefaultCellStyle = acte.desactive == true ? DesactiveStyle : NormalStyle;




            if (((CommActes)this.dataGridView1.Rows[rowidx].Tag).IdComm != 0 && ((CommActes)this.dataGridView1.Rows[rowidx].Tag).IdComm != -1)
            {
                ActePG actepg = new ActePG();
                actepg = ActesPGMgmt.GetActesPGByIdComm(((CommActes)this.dataGridView1.Rows[rowidx].Tag).IdComm);
                if (actepg != null)
                {
                    actepg.lstEcheances = EcheancesMgmt.GetEcheances(actepg, true);
                    if (actepg.lstEcheances.Find(x => x.ID_Encaissement != -1) != null)
                    {
                        dataGridView1.Rows[rowidx].ReadOnly = true;
                    }
                }
            }

            refreshlabesls();
            changeColorRow(dataGridView1.Rows[rowidx], acte.acte_couleur);
        }
        void initDGVMateriel(CommMateriel materiel, bool init = true)
        {
            if (!init)
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

                            return;
                        }

                }
            object[] obj = new object[]
            {
               materiel.Qte ,
                materiel.Libelle,
                materiel.prix_materiel.ToString("C2"),
                materiel.rabais,
                (materiel.prix_materiel_traitement * materiel.Qte).ToString("C2")
                 
            };
            int rowidx = dataGridView1.Rows.Add(obj);
            dataGridView1.Rows[rowidx].Tag = materiel;
            dataGridView1.Rows[rowidx].DefaultCellStyle.BackColor = materiel.materiel_couleur;

            if (((CommMateriel)this.dataGridView1.Rows[rowidx].Tag).IdComm != 0 && ((CommMateriel)this.dataGridView1.Rows[rowidx].Tag).IdComm != -1)
            {
                ActePG actepg = new ActePG();
                actepg = ActesPGMgmt.GetActesPGByIdMateriel(((CommMateriel)this.dataGridView1.Rows[rowidx].Tag).IdComm, ((CommMateriel)this.dataGridView1.Rows[rowidx].Tag).idMateriel);
                if (actepg != null)
                {
                    actepg.lstEcheances = EcheancesMgmt.GetEcheances(actepg, true);
                    if (actepg.lstEcheances.Find(x => x.ID_Encaissement != -1) != null)
                    {
                        dataGridView1.Rows[rowidx].ReadOnly = true;
                    }
                }

            }
            refreshlabesls();
        }
        private void treeviewMateriels_select(object sender, EventArgs e)
        {

            if (!(treeviewMateriels.SelectedNode.Tag is FamillesMateriels))
            {

                CommMateriel commMateriel = new CommMateriel((Materiel)treeviewMateriels.SelectedNode.Tag);
                initDGVMateriel(commMateriel, false);

            }

        }
        private bool verifDureeRDV()
        {
            if (FrmAccessREquest.CheckUser() == true)
            {
                if (comm.IdRDV != -1)
                {
                    RHAppointment app = AppointementsMgmt.getAppointment(comm.IdRDV);
                    if (app.DateArrive != null && app.DateArrive.Value.Date == DateTime.Now.Date)
                        return true;
                    double duree = 0;
                    duree = comm.Acte.acte_durestd;
                    foreach (DataGridViewRow rows in dataGridView1.Rows)
                    {
                        if (rows.Tag is CommActes)
                        {

                            duree += ((CommActes)rows.Tag).acte_durestd;
                        }
                    }
                    app.EndDate = app.StartDate.AddMinutes(duree);
                    if (AppointementsMgmt.VerifNewValidAppointment(app))
                    {
                        app.patient = CurrentPatient;
                        AppointementsMgmt.UpdateAppointment(app, UtilisateursMgt.CurrentUtilisateur.Utilisateur);
                        return true;
                    }
                    if((MessageBox.Show("Durer de rendez-vous non disponible dans l'agenda \n Vous voulez continuer", "Durer de rendez-vous non disponible", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                    return false;
                }
                return true;
            }
            else return false;
        }

        void BuildActePG()
        {

            lstAchats = new List<ActePG>();
            ActePG acte;
            comms = new List<CommActes>();
            switch (_TypeActe)
            {
                case 1: comm.ActesSupp = new List<CommActes>(); break;
                case 2: comm.Materiels = new List<CommMateriel>(); break;
                case 3: comm.Materiels = new List<CommMateriel>(); break;

            }

            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                if (rows.Tag is CommActes)
                {

                    CommActes tmpActesupp = (CommActes)rows.Tag;
                    string rabais = converttoDouble(rows.Cells[3].Value.ToString()).ToString();
                    tmpActesupp.rabais = converttoDouble(rabais);
                    string number = converttoDouble(rows.Cells[4].Value.ToString()).ToString();
                    tmpActesupp.Qte = Convert.ToInt32(rows.Cells[0].Value.ToString());
                    if (converttoDouble(tmpActesupp.Qte.ToString()) == 0)
                        tmpActesupp.prix_traitement = 0;
                    tmpActesupp.prix_traitement = Math.Round(converttoDouble(number) / converttoDouble(tmpActesupp.Qte.ToString()), 2);
                    number = converttoDouble(rows.Cells[2].Value.ToString()).ToString();
                    tmpActesupp.prix_acte = converttoDouble(number);
                    comm.ActesSupp.Add(tmpActesupp);
                }
                else
                    if (rows.Tag is CommClinique)
                    {
                        CommClinique tmpComm = (CommClinique)rows.Tag;
                        string rabais = converttoDouble(rows.Cells[3].Value.ToString()).ToString();
                        tmpComm.Acte = comm.Acte;
                        tmpComm.rabais = converttoDouble(rabais);
                        tmpComm.Acte.quantite = rows.Cells[0].Value.ToString();
                        string number = converttoDouble(rows.Cells[4].Value.ToString()).ToString();
                        if (converttoDouble(tmpComm.Acte.quantite) == 0)
                            tmpComm.prix_traitement = 0;
                        else
                        tmpComm.prix_traitement = Math.Round(converttoDouble(number) / converttoDouble(tmpComm.Acte.quantite), 2);
                        number = converttoDouble(rows.Cells[2].Value.ToString()).ToString();
                        tmpComm.prix = converttoDouble(number);
                        comm = tmpComm;
                    }
                    else
                    {
                        CommMateriel tmpMat = (CommMateriel)rows.Tag;
                        string rabais = converttoDouble(rows.Cells[3].Value.ToString()).ToString();
                        tmpMat.rabais = converttoDouble(rabais);
                        string number = converttoDouble(rows.Cells[4].Value.ToString()).ToString();
                        //   tmpMat.prix_materiel_traitement = converttoDouble(number);
                        tmpMat.Qte = Convert.ToInt32(rows.Cells[0].Value.ToString());
                        if (converttoDouble(tmpMat.Qte.ToString()) == 0)
                            tmpMat.prix_materiel_traitement = 0;
                        tmpMat.prix_materiel_traitement = Math.Round(converttoDouble(number) / converttoDouble(tmpMat.Qte.ToString()), 2);
                        number = converttoDouble(rows.Cells[2].Value.ToString()).ToString();
                        tmpMat.prix_materiel = converttoDouble(number);
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



        Stack<Panel> histopanel = new Stack<Panel>();




        Panel currentpnl = null;




        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comm.Acte == null)
            {
                MessageBox.Show("Veuillez ajouter un acte principale");
                return;
            }
            if (verifDureeRDV())
            {

                if (espece)
                {
                    if (ttverse.Text == "")
                        ttverse.Text = "0";
                    string number = converttoDouble(ttPayer.Text).ToString();
                    if (converttoDouble(ttverse.Text) >= converttoDouble(number))
                        //NextPanel();
                        BuildandClose();
                    else
                    {
                        MessageBox.Show("Total versé inférieur  au total à payer ");
                        return;
                    }
                }
                else
                    BuildandClose();
            }
           

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
                        if (m.famille_Materiel.libelle.Trim() == TF.libelle.Trim() && m.famille_Materiel.ordre == TF.ordre && m.famille_Materiel.libelle.Trim() == vFamilleDefaut.Trim())
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
            if (f.libelle == vFamilleDefaut)
            {
                treeviewMateriels.CurrentNode = familynode;

            }

            foreach (FamillesMateriels fm in f.ChildFamillesMateriel)
                RefreshFamilies(familynode.Nodes, fm);
        }
        private void RefreshMateriels(TreeViewIcon treeview)
        {

            treeview.Clear();



            foreach (FamillesMateriels f in MaterielsMgmt.famillesmateriel)
                if (f.ParentFamillesMaterielId == -1 && f.libelle == vFamilleDefaut)
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

            if (isActeSupp || isActe || isActeGroupement)
            {
                if (isActe || isActeGroupement)
                    IsSelected = (node == treeViewActe.SelectedNode);
                else
                    IsSelected = (node == treeActeSupp.SelectedNode);
                if (node.Tag is Acte)
                    cl = ((Acte)node.Tag).acte_couleur;
                if (node.Tag is FamillesActe)
                    cl = ((FamillesActe)node.Tag).couleur;
                if (node.Tag is ActeGroupement)
                    cl = ((Acte)node.Tag).acte_couleur;
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
                if (isAchats)
                    IsSelected = (node == treeviewMateriels.SelectedNode);
                else
                    IsSelected = (node == treeViewBlanchiment.SelectedNode);

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
            if (isActe || isActeSupp)
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
                        if (a.famille_Acte == node.Tag)
                        {

                            TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString()));
                            ActeNode.Tag = a;
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
                    foreach (Acte a in ActesMgmt.ActesGroupement)
                    {
                        //if (a.famille_Acte == null) NonAffectedExist = true;
                        if (a.famille_Acte == node.Tag)
                        {

                            TreeNode ActeNode = node.Nodes.Add(a.acte_libelle + "\n" + string.Format("{0:f2}", a.prix_acte.ToString()));
                            ActeNode.Tag = a;
                        }
                    }
                    AffecteActesGroupementToNode(node.Nodes);
                }
            }



        }

        private void RefreshFamilies(TreeNodeCollection collec, FamillesActe f)
        {

            TreeNode familynode = null;
            if (_TypeActe == 6 && ActesMgmt.ActesGroupement.FindAll(w => w.idParent == f.Id).Sum(x => x.prixTraitement * x.qte) > 0)
            {
                List<ActeGroupement> lst = ActesMgmt.ActesGroupement.FindAll(w => w.idParent == f.Id);
            double somme=0;
                foreach(ActeGroupement act in lst)
                {
                    somme+=act.prixTraitement * act.qte;
                }
                                familynode = collec.Add(f.libelle + "\n" + somme.ToString());

            }
            else
                familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesActe fa in f.ChildFamillesActe)
                RefreshFamilies(familynode.Nodes, fa);
        }
        string _ChoixFamille = "";
        public bool isfulltime;
        private void RefreshActesGroupement(TreeViewIcon treeview)
        {
            treeview.Clear();



            foreach (FamillesActe f in ActesMgmt.famillesactegrp)
                if (f.ParentFamillesActeId == -1)
                    RefreshFamilies(treeview.Root.Nodes, f);

            //AffecteActesGroupementToNode(treeview.Root.Nodes);

            //foreach (ActeGroupement a in ActesMgmt.ActesGroupement)
            //{
            //    //if (a.famille_Acte == null) NonAffectedExist = true;
            //    if ((a.famille_Acte == null) && (a.id_acte != -1))
            //    {
            //        TreeNode ActeNode = treeview.Root.Nodes.Add(a.acte_libelle);
            //        ActeNode.Tag = a;
            //    }
            //}


            if (_ChoixFamille != "")
            {
                foreach (trButton b in treeview.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeview.CurrentNode = ((TreeNode)b.Tag);
                    }
                }

            }
            treeview.RecalculEmplacementButtons(true);
        }
        private void RefreshActes(TreeViewIcon treeview)
        {
            treeview.Clear();



            foreach (FamillesActe f in ActesMgmt.famillesacte)
                if (f.ParentFamillesActeId == -1)
                    RefreshFamilies(treeview.Root.Nodes, f);

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


            if (_ChoixFamille != "")
            {
                foreach (trButton b in treeview.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeview.CurrentNode = ((TreeNode)b.Tag);
                    }
                }

            }
            treeview.RecalculEmplacementButtons(true);
        }
        void InitDisplay(TreeViewIcon treeview)
        {


            if (isAchats || isBlanchiment)
                RefreshMateriels(treeview);
            else if (isActeGroupement)
                RefreshActesGroupement(treeview);
            else
                RefreshActes(treeview);
            treeview.ButtonPaint += new PaintEventHandler(PaintIcons);
            treeview.HeaderPaint += new PaintEventHandler(PaintHeader);




        }


        private void FrmNewAchat_Load(object sender, EventArgs e)
        {
            //  InitDisplay(treeViewActe);

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

            setButton(".");
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
        private void setButton(string Number)
        {
            if (isDatagrid)
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 4)
                {



                    if (dataGridView1.CurrentCell.ColumnIndex == 4)
                    {
                        string number = converttoDouble(dataGridView1.CurrentCell.EditedFormattedValue.ToString()).ToString();
                        if (selectLength == 0)
                        {
                            string newvalue = dataGridView1.CurrentCell.Value + Number.ToString();
                            dataGridView1.CurrentCell.Value = newvalue;
                            selectStart++;

                        }
                        else
                        {
                            dataGridView1.CurrentCell.Value = Number.ToString();
                        }
                    }

                    else
                    {
                        if (selectLength == 0)
                            dataGridView1.CurrentCell.Value += Number.ToString();
                        else
                            dataGridView1.CurrentCell.Value = Number;
                    }

                    selectLength = 0;

                }
                refresh();
            }
            else
            {
                if (isTextBoxVerse)
                {
                    ttverse.Text = ttverse.Text.Insert(ttverse.SelectionStart, Number.ToString());
                    ttverse.Focus();
                    ttverse.SelectionStart = ttverse.TextLength;
                }
                else
                {
                    ttPayer.Text = ttPayer.Text.Insert(ttPayer.SelectionStart, Number.ToString());
                    ttPayer.Focus();
                    ttPayer.SelectionStart = ttPayer.TextLength;
                }
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            setButton("1");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            setButton("2");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setButton("3");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            setButton("4");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            setButton("5");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            setButton("6");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            setButton("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setButton("8");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            setButton("9");
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
            setButton("0");
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
            valueCell = dataGridView1.CurrentCell.Value.ToString();
            isTextBoxVerse = false;
            isDatagrid = true;
            isTextBoxPayer = false;
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentCell.Value.ToString() == String.Empty)
                dataGridView1.CurrentCell.Value = valueCell;
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                double number = converttoDouble(dataGridView1.CurrentCell.Value.ToString());
                if (converttoDouble(valueCell) != converttoDouble(dataGridView1.CurrentCell.Value.ToString())) ;
                {
                    refresh();
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                double number = converttoDouble(dataGridView1.CurrentCell.Value.ToString());
                if (converttoDouble(valueCell) != converttoDouble(dataGridView1.CurrentCell.Value.ToString()))
                {
                    refresh();
                }
            }

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null && (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4))
                dataGridView1.CurrentCell.Value = 0;
            else
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex == 0)
                    dataGridView1.CurrentCell.Value = 1;
                else
                    if (isTextBoxVerse)
                        ttverse.Text = "0";
                    else
                        if (isTextBoxPayer)
                            ttPayer.Text = "0";
            refresh();
        }

        private void dataGridView1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {

            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (sender.ToString().IndexOf('.') > -1 && (e.KeyChar != '.' || e.KeyChar == ',')))
            {
                e.Handled = true;
            }

        }
        private void refresh()
        {
            if (dataGridView1.RowCount == 0) return;
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                double number = converttoDouble(dataGridView1.CurrentCell.EditedFormattedValue.ToString());
                double montant = 0;
                switch (dataGridView1.CurrentCell.ColumnIndex)
                {
                    case 0: dataGridView1.CurrentCell.Value = number;
                        double per = converttoDouble(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                        montant = converttoDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString()) * converttoDouble(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    //    dataGridView1.CurrentRow.Cells[4].Value = montant - ((montant * per) / 100.00);
                        dataGridView1.CurrentRow.Cells[4].Value = montant;
                        refreshlabesls(); break;
                    case 4:

                        montant = (converttoDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString()) * converttoDouble(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        //if (montant >= number)
                        //{
                        //    double percentage = ((montant - number) / montant) * 100.00;
                        //    dataGridView1.CurrentRow.Cells[3].Value = Math.Round(percentage, 2);
                        //}
                        //else
                            dataGridView1.CurrentRow.Cells[3].Value = 0;
                        dataGridView1.CurrentCell.Value = number; refreshlabesls(); break;

                }
            }
        }


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
            if (espece)
            {
                espece = false;
                ttverse.Hide();
                Currency.Hide();
                aRendre.Hide();
                label12.Hide();
                label13.Hide();
                button16.UseVisualStyleBackColor = true;
            }
            else
            {
                button16.UseVisualStyleBackColor = false;
                button17.UseVisualStyleBackColor = true;
                espece = true;
                carte = false;
                ttverse.Show();
                Currency.Show();
                aRendre.Show();
                label12.Show();
                label13.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (carte)
            {

                carte = false;
                button17.UseVisualStyleBackColor = true;
            }
            else
            {
                button17.UseVisualStyleBackColor = false;
                button16.UseVisualStyleBackColor = true;
                espece = false;
                carte = true;
                ttverse.Hide();
                Currency.Hide();
                aRendre.Hide();
                label12.Hide();
                label13.Hide();
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ttverse.TextLength > 0 && dataGridView1.Rows.Count > 0)
            {
                string number = converttoDouble(ttPayer.Text).ToString();
                aRendre.Text = (converttoDouble(ttverse.Text) - converttoDouble(number)).ToString("C2");
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dataGridView1.CurrentCell = null;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isTextBoxVerse = true;
            isDatagrid = false;
            isTextBoxPayer = false;
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
            refreshlabesls();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectedRows = dataGridView1.HitTest(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right && selectedRows.RowIndex > -1 && dataGridView1.Rows[selectedRows.RowIndex].ReadOnly == false)
            {
                supprimerToolStripMenuItem.Visible = true;
                if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommClinique || this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommMateriel)
                    désactiverToolStripMenuItem.Visible = false;
                else
                    désactiverToolStripMenuItem.Visible = true;
                if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommActes)
                    désactiverToolStripMenuItem.Text = ((CommActes)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive == true ? "Activer" : "Désactiver";

            }
            else
            {
                désactiverToolStripMenuItem.Visible = false;
                supprimerToolStripMenuItem.Visible = false;
            }
        }

        private void slidingList1_OnSelectionChange_1(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.ReadOnly == false)
            {
                double montant = converttoDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString()) * converttoDouble(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                dataGridView1.CurrentRow.Cells[4].Value = montant - (montant * Convert.ToInt32(slidingList1.SelectedItems[0].Tag)) / (double)100;

                dataGridView1.CurrentRow.Cells[3].Value = slidingList1.SelectedItems[0].Tag;
                refresh();
                refreshlabesls();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAchats = false;
            isActe = false;
            isActeSupp = false;
            isBlanchiment = false;
            isActeGroupement= false;
            switch (tabControl1.SelectedIndex)
            {
                case 0: isActe = true; InitDisplay(treeViewActe); break;
                case 1: isActeSupp = true; InitDisplay(treeActeSupp); break;
                case 2: isAchats = true; vFamilleDefaut = System.Configuration.ConfigurationManager.AppSettings["FamilleAchatDefault"]; InitDisplay(treeviewMateriels); break;
                case 3: isBlanchiment = true; vFamilleDefaut = System.Configuration.ConfigurationManager.AppSettings["FamilleBlanchimentDefault"]; InitDisplay(treeViewBlanchiment); break;
                case 4: isActeGroupement = true; InitDisplay(treeViewGrpActes); break;
            }

        }


        private void treeViewActe_OnSelected(object sender, EventArgs e)
        {


            if (isActe)
            {
                if (!(treeViewActe.SelectedNode.Tag is FamillesActe))
                {

                    if (comm.Acte != null)
                    {
                        MessageBox.Show("vous pouvez pas ajouter deux actes principale");
                        return;
                    }

                    comm.Acte = new Acte();
                    comm.Acte = (Acte)treeViewActe.SelectedNode.Tag;
                    comm.prix_traitement = ((Acte)treeViewActe.SelectedNode.Tag).prix_acte;

                    initDGV(comm, false);
                    // ShowPanel(pnlGeneralInformations);
                    btnOk.Visible = true;
                }
            }
        }

        private void treeActeSupp_OnSelected(object sender, EventArgs e)
        {
            if (!(treeActeSupp.SelectedNode.Tag is FamillesActe))
            {

                CommActes commActe = new CommActes((Acte)treeActeSupp.SelectedNode.Tag);
                initDGVActeSupp(commActe, false);
                // ShowPanel(pnlGeneralInformations);
                //  btnOk.Visible = true;
            }
        }

        private void treeViewBlanchiment_OnSelected(object sender, EventArgs e)
        {
            if (!(treeViewBlanchiment.SelectedNode.Tag is FamillesMateriels))
            {


                CommMateriel commMateriel = new CommMateriel((Materiel)treeViewBlanchiment.SelectedNode.Tag);
                initDGVMateriel(commMateriel, false);
                // ShowPanel(pnlGeneralInformations);
                // btnOk.Visible = true;
            }
        }



        private void désactiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows[selectedRows.RowIndex].Tag is CommActes)
            {
                ((CommActes)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive = !((CommActes)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive;
                this.dataGridView1.Rows[selectedRows.RowIndex].DefaultCellStyle = ((CommActes)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).desactive == true ? DesactiveStyle : NormalStyle;
                string ancienMontant = converttoDouble(this.dataGridView1.Rows[selectedRows.RowIndex].Cells[2].Value.ToString()).ToString();
                ((CommActes)this.dataGridView1.Rows[selectedRows.RowIndex].Tag).prix_traitement = converttoDouble(ancienMontant);
            }

            refreshlabesls();
        }

        private void ttPayer_TextChanged(object sender, EventArgs e)
        {


        }

        private void ttPayer_Leave(object sender, EventArgs e)
        {
            isTextBoxPayer = true;
            isTextBoxVerse = false;
            isDatagrid = false;
            double ttotale = converttoDouble(MtInit.Text);
            if (ttotale < converttoDouble(ttPayer.Text))
            {
                MessageBox.Show("Montant supérieur au montant total");
                return;
            }
            double tmpTT = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                double tt = 0;
                double ttligne = Math.Round(converttoDouble(r.Cells[2].Value.ToString()) * converttoDouble(r.Cells[0].Value.ToString()), 2);
                tt = Math.Round(ttligne - ((ttligne / ttotale) * (ttotale - converttoDouble(ttPayer.Text))), 2);
                r.Cells[3].Value = Math.Round((1 - (tt / ttligne)) * 100, 2);
                r.Cells[4].Value = tt;
                tmpTT += tt;
            }
            if (tmpTT != converttoDouble(ttPayer.Text))
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (converttoDouble(r.Cells[4].Value.ToString()) - (tmpTT - converttoDouble(ttPayer.Text)) > 0)
                    {
                        r.Cells[4].Value = converttoDouble(r.Cells[4].Value.ToString()) - (tmpTT - converttoDouble(ttPayer.Text));
                        break;
                    }
                }
            refresh();
            refreshlabesls();
        }
        private double converttoDouble(string montant)
        {
            if (montant == string.Empty) return 0;
            return Convert.ToDouble(Regex.Replace(montant, "[^0-9.,+]", "").Replace('.', ','));
        }

        private void ttPayer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (sender.ToString().IndexOf('.') > -1 && (e.KeyChar != '.' || e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void ttverse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (sender.ToString().IndexOf('.') > -1 && (e.KeyChar != '.' || e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void treeViewGrpActes_OnSelected(object sender, EventArgs e)
        {
            FamillesActe TmpActe = ((FamillesActe)treeViewGrpActes.SelectedNode.Tag);
            if (ActesMgmt.ActesGroupement.Find(w => w.idParent == TmpActe.Id) != null)
            {
                foreach (ActeGroupement acg in ActesMgmt.ActesGroupement.FindAll(w => w.idParent == TmpActe.Id))
                {


                    CommActes commActe = new CommActes(acg);
                    initDGVActeSupp(commActe, false);


                }

            }
        }

        private void treeViewActe_Load(object sender, EventArgs e)
        {

        }

        private void slidingList1_Load(object sender, EventArgs e)
        {

        }

        private void treeViewBlanchiment_Load(object sender, EventArgs e)
        {

        }

    

    }
}
