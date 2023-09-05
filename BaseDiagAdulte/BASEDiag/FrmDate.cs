using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmDate : Form
    {

        public DateTime Value
        {
            get
            {
                return dateTimePicker1.Value;
            }
            set
            {
                dateTimePicker1.Value = value;
            }
        }
        public FrmDate(String Text,String Caption)
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
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

        
    }
}
