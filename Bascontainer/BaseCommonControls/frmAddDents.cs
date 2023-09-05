using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class frmAddDents : Form
    {


        private string _dents;
        public string dents
        {
            get
            {
                return _dents;
            }
            set
            {
                _dents = value;
            }
        }
        public frmAddDents(string __dents)
        {
            InitializeComponent();
            txtbxDents.Text = __dents;
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            DialogResult = DialogResult.Cancel;
            Close();
        }


       

        

        private void btnOk_Click(object sender, EventArgs e)
        {
            dents = txtbxDents.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitDisplay()
        {



            
        }
           


        

        private void AddCommClinique_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void FrmAddCommClinique_Shown(object sender, EventArgs e)
        {
            txtbxDents.Focus();
        }

        private void txtbxFait_TextChanged(object sender, EventArgs e)
        {

        }

        

        
           

        
       
    }
}
