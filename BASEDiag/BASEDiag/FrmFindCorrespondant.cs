using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BASEDiag_BO;
using BASEDiag_BL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag
{
    public partial class FrmFindCorrespondant : Form
    {

        private string _profession;
        public string profession
        {
            get
            {
                return _profession;
            }
            set
            {
                _profession = value;
            }
        }

        private Correspondant _correspondant;
        public Correspondant correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                _correspondant = value;
            }
        }

        public FrmFindCorrespondant(string Profession)
        {
            profession = Profession;
            InitializeComponent();


        }


        private void refreshlstBx()
        {
            if (profession == null) return;
            lstBxCorrespondants.Items.Clear();
            List<baseSmallPersonne> lstSugested = MgmtCorrespondants.getCorrespondantsSuggested(profession, txtbxNom.Text);
            foreach (baseSmallPersonne rc in lstSugested)
            {
                lstBxCorrespondants.Items.Add(rc);

            }

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SuggestBxCorrespondant.Suggest(txtbxNom.Text);

            refreshlstBx();
        }

        private void SuggestBxCorrespondant_OnYesClick(object sender, EventArgs e)
        {
            correspondant = MgmtCorrespondants.getCorrespondant(((baseSmallPersonne)SuggestBxCorrespondant.value).Id);
            DialogResult = DialogResult.OK;
            Close();
        }

        delegate void SetSourceDelegate(List<object> source);
        
        void FillAutocomplete(object state)
        {
            List<object> lstSugested = MgmtCorrespondants.getCorrespondantsSuggested("");

            if (lstSugested.Count > 0)
                SetAutocompleteSource(lstSugested);
        }

        void SetAutocompleteSource(List<object> source)
        {
            if (InvokeRequired)
                Invoke(new SetSourceDelegate(SetAutocompleteSource), source);
            else
            {
                SuggestBxCorrespondant.SuggestionList = source;
            }
        }

        private void SuggestBxCorrespondant_OnFound(object sender, EventArgs e)
        {

            SuggestBxCorrespondant.LabelText = ((baseSmallPersonne)SuggestBxCorrespondant.value).Nom + " " + ((baseSmallPersonne)SuggestBxCorrespondant.value).Prenom + " ? ";


        }

        private void FrmCorrespondant_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(FillAutocomplete));

            Text = "Rechercher un spécialiste (" + profession + ")";

            refreshlstBx();
            
        }

        private void txtbxNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstBxCorrespondants_Click(object sender, EventArgs e)
        {
            correspondant = MgmtCorrespondants.getCorrespondant(((baseSmallPersonne)lstBxCorrespondants.SelectedItem).Id);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnAddCorrespondant_Click(object sender, EventArgs e)
        {
            FrmCorrespondant frm = new FrmCorrespondant();
            frm.defautType = TypeCorresMgmt.getTypeByName(profession);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MgmtCorrespondants.SaveCorrespondant(frm.Correspondant);
                refreshlstBx();

            }
        }
    }
}
