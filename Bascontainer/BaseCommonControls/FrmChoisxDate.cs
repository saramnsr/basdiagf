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
    public partial class FrmChoisxDate : Form
    {
        private bool _checkDate=false;
        public bool checkDate
        {
            get
            { return _checkDate; }
            set { _checkDate = value; }
        }
        private bool _checkNb = false;
        public bool checkNb
        {
            get
            { return _checkNb; }
            set { _checkNb = value; }
        }
        private DateTime _date = new DateTime();
        public DateTime date
        {
            get
            { return _date; }
            set {_date=value ;}
        }
        private int _nbJours = 0;
        public int nbJours
        {
            get
            { return _nbJours; }
            set { _nbJours = value; }
        }
        public FrmChoisxDate()
        {
            InitializeComponent();
            checkDate = rbDate.Checked;
        }

        private void rbMontant_CheckedChanged(object sender, EventArgs e)
        {
            checkDate = true;
            checkNb = false;
        }

        private void rbMontantRemise_CheckedChanged(object sender, EventArgs e)
        {
            checkDate = false;
            checkNb = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (checkNb)
            {
                if (txtnbJours.Text == String.Empty)
                {
                    MessageBox.Show("Champs obligatoire");
                    return;
                }
                nbJours = Convert.ToInt32(txtnbJours.Text);
            }
            date = dateTimePicker1.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
           
        }

        private void txtnbJours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( !char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }
    }
}
