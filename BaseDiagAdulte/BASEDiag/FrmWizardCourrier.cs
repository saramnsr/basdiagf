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
using BasCommon_BL;
using BasCommon_BO;
using System.IO;
namespace BASEDiagAdulte
{
    public partial class FrmWizardCourrier : Form
    {


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


        public FrmWizardCourrier(basePatient pat)
        {

            InitializeComponent();

            _FindCorrespondantTimer = new Timer();
            _FindCorrespondantTimer.Interval = 500;
            _FindCorrespondantTimer.Enabled = false;
            _FindCorrespondantTimer.Tick += new EventHandler(_FindCorrespondantTimer_Tick);

            _patient = pat;

            foreach (LienCorrespondant c in pat.Correspondants)
            {
                baseSmallPersonne sc = MgmtCorrespondants.getSmallCorrespondant(c.IdCorrespondance);
                
                if (sc !=null)
                    lstbxCorrespondant.Items.Add(sc);
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

        private void FrmWizardCourrier_Load(object sender, EventArgs e)
        {
            string TemplateFolder = ConfigurationManager.AppSettings["TEMPLATE_FOLDER"];

            BuildDossierTemplate(TemplateFolder, null);
            tvTemplate.RecalculEmplacementButtons(true);
            RefreshColorButtons();



            

            if (File.Exists(_SelectedFile)) 
                tabControl1.TabPages.Remove(tabChoixCourrier);

            
            
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
        
        private void BuildDossierTemplate(string folder, TreeNode nd)
        {
            if (nd == null) nd = tvTemplate.Root;
            if (Directory.Exists(folder))
            {
                foreach (string s in Directory.GetDirectories(folder))
                {
                    DirectoryInfo nfo = new DirectoryInfo(s);
                    TreeNode n = nd.Nodes.Add(nfo.Name);
                    n.Tag = nfo;
                    BuildDossierTemplate(s, n);
                }

                foreach (string s in Directory.GetFiles(folder, "*.bvm"))
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
            if (lstbxCorrespondant.SelectedItem!=null) 
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
