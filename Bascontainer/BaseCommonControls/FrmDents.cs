using BasCommon_BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class FrmDents : Form
    {
        private string _SelectedDent;
        public string SelectedDent
        {
            get
            {
                return _SelectedDent;
            }
            set
            {
                _SelectedDent = value;
            }

        }
        NewTraitement _traitement;
        public FrmDents(NewTraitement traitement)
        {
            InitializeComponent();
            _traitement = traitement;
        }

        private void cdentControle_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void FrmDents_Load(object sender, EventArgs e)
        {
        }

   
     

        private void FrmDents_Load_1(object sender, EventArgs e)
        {
            cdentControle.SelectedDents = SelectedDent;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string vDentNonPEnCharge = System.Configuration.ConfigurationManager.AppSettings["dents_non_prises_en_charge"];
            string vScenarioDentNonPEnCharge = System.Configuration.ConfigurationManager.AppSettings["Scenario_dents_non_prises_en_charge"];

            string[] tmp = vDentNonPEnCharge == null ? null : vDentNonPEnCharge.Split(',');
            string[] tmp1 = vScenarioDentNonPEnCharge == null ? null : vScenarioDentNonPEnCharge.Split(',');
            if(tmp != null && tmp1 != null)
            foreach (string s in tmp1)
            {
                if(s != "")
                if (Convert.ToInt32(s) == _traitement.id_Traitement)
                {
                    foreach (string s1 in tmp)
                    {
                     if(cdentControle.SelectedDents != "" && s1 != string.Empty)
                        foreach(string s2 in cdentControle.SelectedDents.Split(','))
                        {
                            if (s2 == s1)
                            {
                                MessageBox.Show("Dents non prises en charge dans le panier de bien CMUC");
                                return;
                            }
                        }
                    }
                }
            }
            SelectedDent = cdentControle.SelectedDents;

        }
    }
}
