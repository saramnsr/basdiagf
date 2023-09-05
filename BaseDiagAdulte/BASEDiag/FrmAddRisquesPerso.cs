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

namespace BASEDiagAdulte
{
    public partial class FrmAddRisquesPerso : Form
    {

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
        
        public FrmAddRisquesPerso(basePatient pat)
        {
            patient = pat;
            InitializeComponent();
        }

        private void FrmRisquesPerso_Load(object sender, EventArgs e)
        {

            baseMgmtPatient.getRisques(patient);
            if (patient.Risques.Count > 0)
                txtbxRisques.Text = patient.Risques.Aggregate((i, j) => i + "\r\n" + j);
             
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            patient.Risques = txtbxRisques.Text.Replace("\r", "").Split('\n').ToList();
            baseMgmtPatient.setRisques(patient);
            DialogResult = DialogResult.OK;
            Close();
             
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmRisquesPerso_Shown(object sender, EventArgs e)
        {
            txtbxRisques.Focus();
        }
    }
}
