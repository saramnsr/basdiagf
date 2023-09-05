using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class FrmLocalisationAnatomique : Form
    {

        private int _localisation = 0;
        public int localisation
        {
            get
            {
                return _localisation;
            }
            set
            {
                _localisation = value;
            }

        }
        public FrmLocalisationAnatomique()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex regexInt = new Regex("^[0-9]+$");

            if ((regexInt.IsMatch(numLocalisation.Text)))
            {
                localisation = Convert.ToInt32(numLocalisation.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Format incorrecte");
                return;
            }

             
        }

        private void FrmLocalisationAnatomique_Load(object sender, EventArgs e)
        {

        }
    }
}
