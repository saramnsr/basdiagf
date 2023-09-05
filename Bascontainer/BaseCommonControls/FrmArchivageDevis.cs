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
    public partial class FrmArchivageDevis : Form
    {

        private Devis _CurrentDevis;
        public Devis CurrentDevis
        {
            get
            {
                return _CurrentDevis;
            }
            set
            {
                _CurrentDevis = value;
            }
        }

        public string emplacement
        {
            get
            {
                return txtbxEmplacement.Text;
            }
            set
            {
                txtbxEmplacement.Text = value;
            }
        }


       public Utilisateur Ecrivain
        {
            get
            {
                return (Utilisateur)cbxEcrivain.SelectedTag;
            }
            set
            {
                cbxEcrivain.SelectedTag = value;
            }
        }

        public string RaisonDeLarchivage
        {
            get
            {
                return txtbxArchivageWhy.Text;
            }
            set
            {
                txtbxArchivageWhy.Text = value;
            }
        }

        public FrmArchivageDevis(Devis devis)
        {
            CurrentDevis = devis;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (Ecrivain == null)
            {
                MessageBox.Show("Veuillez sélectionner qui archive ce devis!");
                return;
            }

            if (RaisonDeLarchivage == "")
            {
                if (MessageBox.Show("Attention aucune raison particuliere pour cet archivage\nSouhaitez-vous continuer ?", "Raison", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;                
            }

            if (emplacement == "")
            {
                if (MessageBox.Show("Aucun emplacement physique pour le devis archivé\nSouhaitez-vous continuer ?", "Emplacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FrmArchivageDevis_Load(object sender, EventArgs e)
        {

        }

        private void FrmArchivageDevis_Shown(object sender, EventArgs e)
        {
            txtbxArchivageWhy.Focus();
            cbxEcrivain.SelectedTag = UtilisateursMgt.getUtilisateurInFauteuil(Fauteuilsmgt.GetWhoIam(), DateTime.Now);
        }
    }
}
