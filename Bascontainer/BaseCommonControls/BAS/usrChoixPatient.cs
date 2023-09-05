using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;

namespace BaseCommonControls.BAS
{
    [DefaultEvent("OnSelectionChange")]
    public partial class usrChoixPatient : Button
    {
        FrmChoixPatient frm = null;


        private bool _CanFindPatientsOrthalis = false;
        public bool CanFindPatientsOrthalis
        {
            get
            {
                return _CanFindPatientsOrthalis;
            }
            set
            {
                _CanFindPatientsOrthalis = value;
            }
        }
        
        public event EventHandler OnSelectionChange;


        private baseSmallPersonne _selectedpatient;
        public baseSmallPersonne selectedpatient
        {
            get
            {
                return _selectedpatient;
            }
            set
            {
                _selectedpatient = value;

                this.Text = _selectedpatient == null ? "" : _selectedpatient.ToString();

                if (OnSelectionChange != null)
                    OnSelectionChange(this, new EventArgs());
            }
        }

        public usrChoixPatient()
        {
            InitializeComponent();

            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Font = new Font("Garamond", 11, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = Color.WhiteSmoke;
        }

        protected override void OnClick(EventArgs e)
        {
            if (frm != null) return;
            frm = new FrmChoixPatient();
            frm.CanFindPatientsOrthalis = CanFindPatientsOrthalis;
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.Owner = this.FindForm();
            frm.Show(this);
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                selectedpatient = frm.SelectedPatient;
            }
            frm = null;
        }

       
    }
}
