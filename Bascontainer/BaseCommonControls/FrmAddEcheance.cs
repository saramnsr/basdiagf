using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmAddEcheance : Form
    {

        private BaseTempEcheanceDefinition _SelectedEcheance = null;
        public BaseTempEcheanceDefinition SelectedEcheance
        {
            get
            {
                return _SelectedEcheance;
            }
            set
            {
                _SelectedEcheance = value;
                label3.Visible = true;
                numericUpDown1.Visible = true;
                label4.Visible = true;
            }
        }
      

        public double montant
        {
            get
            {


                //return double.Parse(txtbxMontant.Text,System.Globalization . CultureInfo.InvariantCulture);


                return Convert.ToDouble(txtbxMontant.Text);
            }
           
        }

        public DateTime dateEch
        {
            get
            {
                return dtpEch.Value;
            }
           
        }

        public FrmAddEcheance()
        {
            InitializeComponent();
            dtpEch.Value = DateTime.Now;
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

        private void FrmAddEcheance_Shown(object sender, EventArgs e)
        {
            txtbxMontant.Focus();
        }

        private void FrmAddEcheance_Load(object sender, EventArgs e)
        {
            label4.Text = "% de " + (SelectedEcheance.Montant).ToString("C2");

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            txtbxMontant.Text = Math.Round(SelectedEcheance.Montant * (double)(numericUpDown1.Value/100),2).ToString();

        }
    }
}
