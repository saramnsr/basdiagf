using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEPractice_BL;
//using BASEPractice_BO;
using BasCommon_BO;
using BasCommon_BL;

namespace BaseCommonControls
{
    public partial class FrmChoixAutrePersonne : Form
    {


        private CommTraitement  _com;
        public CommTraitement Currentcom
        {
            get
            {
                return _com;
            }
            set
            {
                _com = value;
            }
        }

        private List<CommAutrePersonne> _values = new List<CommAutrePersonne>();
        public List<CommAutrePersonne> values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }


        public FrmChoixAutrePersonne(CommTraitement  com )
        {
            Currentcom = com;
            InitializeComponent();
            InitDisplay();

        }

        private void Search()
        {
            List<baseSmallPersonne> correspondants = MgmtCorrespondants.getSmallCorrespondants(textBox1.Text);

            lbxCorres.Items.Clear();

            foreach (baseSmallPersonne c in correspondants)
                lbxCorres.Items.Add(c);

        }


        private void InitDisplay()
        {

            foreach (CommAutrePersonne cap in Currentcom.AutrePersonnes)
            {
                baseSmallPersonne sc = new baseSmallPersonne();
                sc.Nom = cap.Nom;
                sc.Prenom = cap.Prenom;
                sc.Id = cap.IdCorrespondant;
                lstbxSelected.Items.Add(sc);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Search();
            ((Timer)sender).Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timerSearch.Enabled = false;
            timerSearch.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Build();
            DialogResult = DialogResult.OK;
            Close();

        }

        private void Build()
        {
            foreach (baseSmallPersonne c in lstbxSelected.Items)
            {
                CommAutrePersonne ca = new CommAutrePersonne();
                ca.IdCorrespondant = c.Id;
                ca.Nom = c.Nom;
                ca.Prenom = c.Prenom;
                //ca.Parent = Currentcom;
                values.Add(ca); 
            }
        }

        private void btnCancel_Click()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstbxSelected_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                lstbxSelected.Items.Remove(lstbxSelected.SelectedItem);
        }

        private void lbxCorres_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbxCorres.SelectedItem!=null)
                if (!lstbxSelected.Items.Contains(lbxCorres.SelectedItem)) lstbxSelected.Items.Add(lbxCorres.SelectedItem);
        }
    }
}
