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
    public partial class FrmTexte : Form
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
                return txtbxText.Text;
            }
            set
            {
                txtbxText.Text = value;
            }
        }
        public FrmTexte(String Text, String Caption)
        {
            InitializeComponent();
            txtbxText.Text = "";
            label1.Text = Text;
            this.Text = Caption;
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

        private void FrmTexte_Load(object sender, EventArgs e)
        {
        
        }

        private void FrmTexte_Shown(object sender, EventArgs e)
        {
            txtbxText.Focus();
        }

        
    }
}
