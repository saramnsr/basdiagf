using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEPractice_BL;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmEditTempEcheanceDef : Form
    {



        private BaseTempEcheanceDefinition _echeancedef;
        public BaseTempEcheanceDefinition echeancedef
        {
            get
            {
                return _echeancedef;
            }
            set
            {
                _echeancedef = value;
            }
        }

        public FrmEditTempEcheanceDef(BaseTempEcheanceDefinition echeance)
        {
            echeancedef = echeance;
            InitializeComponent();
        }

        private bool Build()
        {
            if (echeancedef == null) echeancedef = new TempEcheanceDefinition();

            try
            {
                echeancedef.Montant = Convert.ToDouble(txtbxMontantEcheance.Text);
                echeancedef.DAteEcheance = dtpdateEcheance.Value;
                echeancedef.Libelle = txtbxLibelle.Text;
                echeancedef.ParPrelevement = chkbxParPrelevement.Checked;
                echeancedef.ParVirement = chkbxVirement.Checked;
                
                echeancedef.CanRecalculate = false;
                return true;
            }
            catch (System.Exception) { return false; }

        }

        private void InitDisplay()
        {
            if (echeancedef == null) return;
            txtbxMontantEcheance.Text = echeancedef.Montant.ToString();
            try
            {
                dtpdateEcheance.Value = echeancedef.DAteEcheance;
            }
            catch (System.Exception)
            {
                dtpdateEcheance.Value = DateTime.Now;
            }
            txtbxLibelle.Text = echeancedef.Libelle;
            chkbxParPrelevement.Checked = echeancedef.ParPrelevement;
            chkbxVirement.Checked = echeancedef.ParVirement;

        }

        private void FrmEditTempEcheanceDef_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Build();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkbxParPrelevement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxParPrelevement.Checked) chkbxVirement.Checked = false;
        }

        private void chkbxVirement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxVirement.Checked) chkbxParPrelevement.Checked = false;
        }
    }
}
