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
    public partial class FrmNewRecette : Form
    {

        private Recette _CurrentRecette = null;
        public Recette CurrentRecette
        {
            get
            {
                return _CurrentRecette;
            }
            set
            {
                _CurrentRecette = value;
            }
        }

        public FrmNewRecette()
        {
            InitializeComponent();
        }

        public FrmNewRecette(Recette d)
        {
            CurrentRecette = d;
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
                if (CurrentRecette == null) CurrentRecette = new Recette();

                CurrentRecette.banqueDeRemise = (BanqueDeRemise)cbxBanque.SelectedItem;
                CurrentRecette.Code = txtbxCode.Text;
                CurrentRecette.DateRemiseEnBanque = dtpdepense.Value;
                CurrentRecette.DateValeurBque = dtpvaleurbque.Value;
                CurrentRecette.Details = txtbxDetails.Text;
                CurrentRecette.Montant = Convert.ToDouble(txtbxMontant.Text);

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
                if (CurrentRecette == null) return;

                cbxBanque.SelectedItem = CurrentRecette.banqueDeRemise;
                txtbxCode.Text = CurrentRecette.Code;
                dtpdepense.Value = CurrentRecette.DateRemiseEnBanque;
                dtpvaleurbque.Value = CurrentRecette.DateValeurBque;
                txtbxDetails.Text = CurrentRecette.Details;
                txtbxMontant.Text = CurrentRecette.Montant.ToString();


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


            InitDisplay();
        }

        private void FrmNewDepense_Shown(object sender, EventArgs e)
        {
            txtbxMontant.Focus();
        }
    }
}
