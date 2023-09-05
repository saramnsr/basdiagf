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
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag.Ctrls
{
    public partial class frmPlanTraitementDEP : Form
    {



        public string PlanDeTraitement
        {
            get
            {
                return txtbxPlanTraitement.Text;
            }
            set
            {
                if (txtbxPlanTraitement!=null)                
                    txtbxPlanTraitement.Text = value;
            }
        }

        private string _Value;
        public string Value
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

        public frmPlanTraitementDEP(string plandetraitement)
        {
            InitializeComponent();
            PlanDeTraitement = plandetraitement;
            
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

        private void frmPlanTraitementDEP_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            lstbxPlantraitmntDef.Items.Clear();
            foreach (PlanTraitementDEP ped in PlanTraitementDEPMgmt.plantraitementsDEP)
            {
                lstbxPlantraitmntDef.Items.Add(ped);
            }
        }

        private void lstbxPlantraitmntDef_Click(object sender, EventArgs e)
        {
            if (txtbxPlanTraitement.Text != "") txtbxPlanTraitement.Text += " + ";
            txtbxPlanTraitement.Text += ((PlanTraitementDEP)lstbxPlantraitmntDef.SelectedItem).Libelle;
        }
    }
}
