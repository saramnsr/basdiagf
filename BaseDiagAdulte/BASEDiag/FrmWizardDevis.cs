using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BASEDiag_BO;
using BASEDiag_BL;
using BasCommon_BO;
using BasCommon_BL;
using System.IO;
namespace BASEDiagAdulte
{
    public partial class FrmWizardDevis : Form
    {

        private TypeDevis _tpeDevis;
        public TypeDevis tpeDevis
        {
            get
            {
                return _tpeDevis;
            }
            set
            {
                _tpeDevis = value;
            }
        }

        public Correspondant Praticien
        {
            get
            {
                return MgmtCorrespondants.getCorrespondant(((Utilisateur)lstBxPraticien.SelectedItem).Id); ;
            }
           
        }

        public List<baseSmallPersonne> lstCorrespondant
        {
            get
            {
                List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

                foreach (baseSmallPersonne c in lstbxDestinataires.Items)
                    lst.Add(c);

                return lst;
            }
            
        }

        private basePatient _patient;
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }
        
        private Timer _FindCorrespondantTimer;
       
        private string _SelectedFile;
        public string FileName
        {
            get
            {
                return _SelectedFile;
            }
            set
            {
                _SelectedFile = value;
            }
        }


        public FrmWizardDevis(basePatient pat, TypeDevis Devis)
        {

            InitializeComponent();

            _FindCorrespondantTimer = new Timer();
            _FindCorrespondantTimer.Interval = 500;
            _FindCorrespondantTimer.Enabled = false;
            _FindCorrespondantTimer.Tick += new EventHandler(_FindCorrespondantTimer_Tick);

            _patient = pat;

            tpeDevis = Devis;

            foreach (LienCorrespondant c in pat.Correspondants)
            {
                baseSmallPersonne sc = new baseSmallPersonne();
                sc.Id = c.correspondant.Id;
                sc.Nom = c.correspondant.Nom;
                sc.Prenom = c.correspondant.Prenom;
                lstbxCorrespondant.Items.Add(sc);

                
            }

            foreach (LienCorrespondant c in pat.Correspondants)
                if (c.TypeDeLien == "Pa")
                {
                    baseSmallPersonne sc = new baseSmallPersonne();
                    sc.Id = c.correspondant.Id;
                    sc.Nom = c.correspondant.Nom;
                    sc.Prenom = c.correspondant.Prenom;
                    lstbxDestinataires.Items.Add(sc);
                    continue;
                }            

            if (lstbxDestinataires.Items.Count == 0)
                foreach (LienCorrespondant c in pat.Correspondants)
                    if (c.TypeDeLien == "Rs")
                    {
                        baseSmallPersonne sc = new baseSmallPersonne();
                        sc.Id = c.correspondant.Id;
                        sc.Nom = c.correspondant.Nom;
                        sc.Prenom = c.correspondant.Prenom;
                        lstbxDestinataires.Items.Add(sc);
                        continue;
                    }
            if (lstbxDestinataires.Items.Count == 0)                
                    {
                        baseSmallPersonne sc = new baseSmallPersonne();
                        sc.Id = pat.Id;
                        sc.Nom = pat.Nom;
                        sc.Prenom = pat.Prenom;
                        lstbxDestinataires.Items.Add(sc);
                    }

            
        }

        void _FindCorrespondantTimer_Tick(object sender, EventArgs e)
        {
            lstbxCorrespondant.Items.Clear();

            List<baseSmallPersonne> lst = MgmtCorrespondants.getCorrespondants(txtbxSearchCorrespondant.Text);
            foreach (baseSmallPersonne c in lst)
                lstbxCorrespondant.Items.Add(c);

            _FindCorrespondantTimer.Enabled = false;
        }

        private void FrmWizardDevis_Load(object sender, EventArgs e)
        {
            string TemplateFolder = ConfigurationManager.AppSettings["DEVIS_FOLDER"];

            BuildDossierTemplate(TemplateFolder, null,tpeDevis);
            tvTemplate.RecalculEmplacementButtons(true);
            RefreshColorButtons();



            foreach (Utilisateur c in UtilisateursMgt.Praticiens)
                lstBxPraticien.Items.Add(c);
            
            
            lstBxPraticien.SelectedIndex = 0;
                
            
        }

        private void RefreshColorButtons()
        {
            foreach (BASEDiagAdulte.Ctrls.BO.trButton btn in tvTemplate.ButtonList)
            {
                if (((TreeNode)btn.Tag).Tag is DirectoryInfo)
                    btn.Color = Color.Gray;

                if (((TreeNode)btn.Tag).Tag is FileInfo)
                    btn.Color = Color.LightGray;
            }
        }

        void tvTemplate_OnChangeLevel(object sender, EventArgs e)
        {
            RefreshColorButtons();
        }

        void tvTemplate_OnSelected(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (((TreeNode)sender).Tag is FileInfo)
                {
                    FileName = ((FileInfo)((TreeNode)sender).Tag).FullName;
                    tabControl1.SelectedIndex = tabControl1.SelectedIndex + 1;
                }
            }
            catch (SystemException)
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        private void BuildDossierTemplate(string folder, TreeNode nd,TypeDevis tpeDevis)
        {
            string fd = folder;
            if (tpeDevis.Categorie == TypeDevis.CategorieDevis.Sucette) fd = folder + "\\Sucette";
            if (tpeDevis.Categorie == TypeDevis.CategorieDevis.Orthopedique) fd = folder + "\\Orthopedique";
            if (tpeDevis.Categorie == TypeDevis.CategorieDevis.Orthodontique) fd = folder + "\\Orthodontique";
            if (tpeDevis.Categorie == TypeDevis.CategorieDevis.Invisalign) fd = folder + "\\Adulte";


            if (!Directory.Exists(fd)) fd = folder;

            if (nd == null) nd = tvTemplate.Root;
            if (Directory.Exists(fd))
            {
                foreach (string s in Directory.GetDirectories(fd))
                {
                    DirectoryInfo nfo = new DirectoryInfo(s);
                    TreeNode n = nd.Nodes.Add(nfo.Name);
                    n.Tag = nfo;
                    BuildDossierTemplate(s, n, tpeDevis);
                }

                foreach (string s in Directory.GetFiles(fd, "*.bvm"))
                {
                    FileInfo nfo = new FileInfo(s);
                    TreeNode n = nd.Nodes.Add(nfo.Name);
                    n.Tag = nfo;
                }
            }
        }

        private void txtbxSearchCorrespondant_TextChanged(object sender, EventArgs e)
        {
            _FindCorrespondantTimer.Enabled = false;
            _FindCorrespondantTimer.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstbxCorrespondant_Click(object sender, EventArgs e)
        {
            lstbxDestinataires.Items.Add(lstbxCorrespondant.SelectedItem);
        }

        private void lstbxDestinataires_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                lstbxDestinataires.Items.Remove(lstbxDestinataires.SelectedItem);
            }
        }
    }
}
