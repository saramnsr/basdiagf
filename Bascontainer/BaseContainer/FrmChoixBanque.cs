using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;

namespace WindowsFormsApplication1
{
    public partial class FrmChoixBanque : Form
    {
        public BanqueDeRemise Value
        {
            get
            {
                return (BanqueDeRemise)cbxBanque.SelectedItem;
            }
            set
            {
                cbxBanque.SelectedItem = value;
            }
        }
        

        public FrmChoixBanque(String Text, String Caption)
        {
            InitializeComponent();
            label1.Text = Text;
            this.Text = Caption;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbxBanque.SelectedItem == null) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmChoixBanque_Load(object sender, EventArgs e)
        {
            foreach (BanqueDeRemise bdr in BanqueMgmt.BanquesDeRemise)
            {
                cbxBanque.Items.Add(bdr);
            }
        }

        
    }
}
