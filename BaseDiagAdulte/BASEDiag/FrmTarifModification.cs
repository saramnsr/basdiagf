using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiagAdulte
{
    public partial class FrmTarifModification : Form
    {

        public TemplateActePG TarifSecu
        {
            get
            {
                return ((TemplateActePG)cbxRemboursement.SelectedItem);
            }
            set
            {
                foreach (TemplateActePG t in cbxRemboursement.Items)
                    if (t == value)
                    {
                        cbxRemboursement.SelectedItem = t;
                    }

            }
        }

        public Double TarifApplique
        {
            get
            {
                return Convert.ToDouble(txtbxTarifCabinet.Text);
            }
            set
            {
                txtbxTarifCabinet.Text = value.ToString();

            }
            
        }
        public FrmTarifModification()
        {
            InitializeComponent();
            cbxRemboursement.Items.Clear();
            foreach (TemplateActePG template in TemplateApctePGMgmt.templates)
            {
                cbxRemboursement.Items.Add(template);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmTarifModification_Load(object sender, EventArgs e)
        {
            
        }

        private void cbxRemboursement_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxTarifCabinet.Text = ((TemplateActePG)cbxRemboursement.SelectedItem).Valeur.ToString();
        }
    }
}
