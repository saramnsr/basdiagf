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
    public partial class FrmString : Form
    {

        public string text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public string Value
        {
            get
            {
                return txtbxtext.Text;
            }
            set
            {
                txtbxtext.Text = value;
            }
        }
        public FrmString(String Text, String Caption,String defaultval)
        {
            InitializeComponent();
            txtbxtext.Text = defaultval;
            label1.Text = Text;
            this.Text = Caption;
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();

            //DialogResult = DialogResult.OK;
            //int  number1 = 0;
            //bool canConvert = int.TryParse(txtbxtext.Text, out number1);
            //if (canConvert == true)
            //    Close();
            //else
            //{
            //    MessageBox.Show("Valeur Erronée");
            //    this.DialogResult = DialogResult.None;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmString_Load(object sender, EventArgs e)
        {

        }

        private void FrmString_Shown(object sender, EventArgs e)
        {
            txtbxtext.Focus();
            txtbxtext.SelectAll();
        }

        
    }
}
