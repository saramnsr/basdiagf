using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;

namespace BASEDiag
{
    public partial class FrmPrinterSetting : Form
    {

        private BasePrinterSetting _printersetting = new BasePrinterSetting();
        public BasePrinterSetting printersetting
        {
            get
            {
                return _printersetting;
            }
            set
            {
                _printersetting = value;
            }
        }


        public FrmPrinterSetting()
        {
            InitializeComponent();
        }

        private void FrmPrinterSetting_Load(object sender, EventArgs e)
        {
            if (printersetting != null)
            {
                txtbxLibelle.Text = printersetting.Libelle;
                txtbxDescriptif.Text = printersetting.Descriptif;
                button3.Text = printersetting.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == DialogResult.OK)
            {
                printersetting.settings = pd.PrinterSettings;
                button3.Text = printersetting.ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            printersetting.Libelle = txtbxLibelle.Text;
            printersetting.Descriptif = txtbxDescriptif.Text;

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
