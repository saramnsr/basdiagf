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
    public partial class FrmNewDepense : Form
    {

        private Depense _CurrentDepense = null;
        public Depense CurrentDepense
        {
            get
            {
                return _CurrentDepense;
            }
            set
            {
                _CurrentDepense = value;
            }
        }

        public FrmNewDepense()
        {
            InitializeComponent();
        }

        public FrmNewDepense(Depense d)
        {
            CurrentDepense = d;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private bool Build()
        {
            try
            {
                if (CurrentDepense == null) CurrentDepense = new Depense();

                CurrentDepense.banqueDeRemise = (BanqueDeRemise)cbxBanque.SelectedItem;
                CurrentDepense.Code = txtbxCode.Text;
                CurrentDepense.DateDepense = dtpdepense.Value;
                CurrentDepense.DateValeurBque = dtpvaleurbque.Value;
                CurrentDepense.Details = txtbxDetails.Text;
                CurrentDepense.ModeReglement = cbxModeReglement.Text;
                CurrentDepense.Montant = Convert.ToDouble(txtbxMontant.Text);

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void InitDisplay()
        {
            try
            {
                if (CurrentDepense == null) return;

                cbxBanque.SelectedItem = CurrentDepense.banqueDeRemise;
                txtbxCode.Text = CurrentDepense.Code;
                dtpdepense.Value = CurrentDepense.DateDepense;
                dtpvaleurbque.Value = CurrentDepense.DateValeurBque;
                txtbxDetails.Text = CurrentDepense.Details;
                cbxModeReglement.Text = CurrentDepense.ModeReglement;
                txtbxMontant.Text = CurrentDepense.Montant.ToString();


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void FrmNewDepense_Load(object sender, EventArgs e)
        {
           

            foreach (BanqueDeRemise br in BanqueMgmt.BanquesDeRemise)
                cbxBanque.Items.Add(br);

            cbxBanque.SelectedItem = cbxBanque.Items[0];


            cbxModeReglement.SelectedItem = cbxModeReglement.Items[0];

            InitDisplay();
        }

        private void FrmNewDepense_Shown(object sender, EventArgs e)
        {
            txtbxMontant.Focus();
        }
    }
}
