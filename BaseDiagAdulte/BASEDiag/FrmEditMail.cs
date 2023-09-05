using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiagAdulte
{
    public partial class FrmEditMail : Form
    {

        private string _sujet;
        public string Sujet
        {
            get
            {
                return _sujet;
            }
            set
            {
                _sujet = value;
            }
        }

        public FrmEditMail(string titre)
        {
            InitializeComponent();
            _sujet = titre;
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

        private void FrmEditMail_Load(object sender, EventArgs e)
        {
            txtbxSubject.Text = _sujet;

          
        }

       
        

    }
}
