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

namespace BASEDiag
{
    public partial class FrmVisuProposition : Form
    {

        private Patient _CurrentPatient;
        public Patient CurrentPatient
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

        public FrmVisuProposition(Patient patient)
        {
            CurrentPatient = patient;
            InitializeComponent();
        }

        private void FrmVisuProposition_Load(object sender, EventArgs e)
        {

            foreach (Proposition p in CurrentPatient.propositions)
                propositionCtrlV21.AddProposition(p);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
