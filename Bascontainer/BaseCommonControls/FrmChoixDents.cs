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
    public partial class FrmChoixDents : Form
    {


        public string Text
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }
        

        public string value
        {
            get
            {
                return choixDents1.SelectedDents;
            }
            set
            {
                choixDents1.SelectedDents = value;
            }
           
        }
        
        public FrmChoixDents()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FrmChoixDents_Load(object sender, EventArgs e)
        {

        }
    }
}
