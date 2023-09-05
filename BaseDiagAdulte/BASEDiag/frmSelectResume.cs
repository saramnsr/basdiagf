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
    public partial class frmSelectResume : Form
    {

        private ResumeClinique _Value;
        public ResumeClinique Value
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

        private List<ResumeClinique> _resumes;
        public List<ResumeClinique> resumes
        {
            get
            {
                return _resumes;
            }
            set
            {
                _resumes = value;
            }
        }

        private basePatient _CurrentPatient = null;
        public basePatient CurrentPatient
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

        public frmSelectResume(basePatient patient)
        {
            CurrentPatient = patient;
            InitializeComponent();
        }

        public frmSelectResume(basePatient patient, List<ResumeClinique> lst)
        {
            CurrentPatient = patient;
            resumes = lst;
            InitializeComponent();
        }

        private void InitDisplay()
        {
            if (CurrentPatient == null) return;

            if (resumes==null)
                resumes = ResumeCliniqueMgmt.GetResumesClinique(CurrentPatient);


            foreach (ResumeClinique rc in resumes)
            {
                object[] objs = new object[] { rc.dateResume};

                 int idx = dgvDiags.Rows.Add(objs);
                dgvDiags.Rows[idx].Tag = rc;
            }

        }

        private void frmSelectResume_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvDiags.SelectedRows.Count == 0) return;
            Value = (ResumeClinique)dgvDiags.SelectedRows[0].Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dgvDiags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }
    }
}
