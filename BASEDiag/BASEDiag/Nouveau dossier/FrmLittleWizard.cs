using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BO;

namespace BASEDiag
{
    public partial class FrmLittleWizard : Form
    {



        private bool _CanBeShown = true;
        public bool CanBeShown
        {
            get
            {
                return _CanBeShown;
            }
            set
            {
                _CanBeShown = value;
            }
        }

        public List<CommonObjectif> values
        {
            get
            {
                List<CommonObjectif> lst = new List<CommonObjectif>();

                foreach (CommonObjectif co in lstBxObjectifs.CheckedItems)
                    lst.Add(co);

                return lst;
            }
            
        }

        private CommonDiagnostic _diagnostique;
        public CommonDiagnostic diagnostique
        {
            get
            {
                return _diagnostique;
            }
            set
            {
                _diagnostique = value;
            }
        }

        public FrmLittleWizard(CommonDiagnostic diagnostic)
        {
            InitializeComponent();
            _diagnostique = diagnostic;



            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(diagnostic);

            if (lstobjs.Count == 0)
            {
                DialogResult = DialogResult.OK;
                CanBeShown = false;
                Close();
            }

            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectifFromDiag cd in lstobjs)
                lstBxObjectifs.Items.Add(cd.objectif);


            lblQuestion.Text = "Que préconisez-vous pour '" + diagnostic.Libelle + "'?";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmLittleWizard_Load(object sender, EventArgs e)
        {

        }
    }
}
