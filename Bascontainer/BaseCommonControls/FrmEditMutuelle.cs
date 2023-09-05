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
using System.Text.RegularExpressions;

namespace BaseCommonControls
{
    public partial class FrmEditMutuelle : Form
    {

        private Regex regTel = new Regex(@"^0[1-9]([-. ]?[0-9]{2}){4}$|^00[0-9]{11,13}$");
        private bool IsMatchTel, IsMatchVille, IsMatchNom = false;

        private Mutuelle _CurrentMutuelle;
        public Mutuelle CurrentMutuelle
        {
            get
            {
                return _CurrentMutuelle;
            }
            set
            {
                _CurrentMutuelle = value;
            }
        }

        public FrmEditMutuelle(Mutuelle mut)
        {
            CurrentMutuelle = mut;
            InitializeComponent();
        }

            public FrmEditMutuelle()
        {
            InitializeComponent();
        }

        private void FrmEditMutuelle_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void InitDisplay()
        {
            if (CurrentMutuelle != null)
            {
                txtbxNomMut.Text = CurrentMutuelle.Nom;
                txtbxNumtel.Text = CurrentMutuelle.Telephone;

                txtbxAdress1.Text = CurrentMutuelle.Adresse_Num + " " + CurrentMutuelle.Adresse_Type_Voie + " " + CurrentMutuelle.Adresse_Nom_Voie;
                txtbxAdress2.Text = CurrentMutuelle.Adresse2;
                txtbxCodePostal.Text = CurrentMutuelle.CodePostal;
                txtbxVille.Text = CurrentMutuelle.Ville;

                txtbxTxRmbrsmnt.Text = CurrentMutuelle.TauxParDefaut.ToString();
                txtbxMntPlafond.Text = CurrentMutuelle.MontantPlafond.ToString();
                txtbxNumMut.Text = CurrentMutuelle.NumMutuelle.ToString();
                chkbxNeedOrder.Checked = CurrentMutuelle.NeedOrder;
           
            }
        }

        private bool Validate()
        {
            if (txtbxNomMut.Text.Trim() == String.Empty)
            {
                txtbxNomMut.BackColor = Color.LightPink;
                IsMatchNom = false;
                MessageBox.Show("Pas de Nom spécifié", "aucun nom", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else IsMatchNom = true;

            IsMatchTel = true;
            if (txtbxNumtel.Text.Trim() == String.Empty)
            {
                txtbxNumtel.BackColor = Color.LightPink;
                IsMatchTel = (MessageBox.Show("Pas de numéro de téléphone.\nSouhaitez-vous continuer?", "Téléphone introuvable", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes);
            }
             

            if ((txtbxCodePostal.Text == String.Empty) && (txtbxVille.Text == string.Empty))
            {
                txtbxCodePostal.BackColor = Color.LightPink;
                txtbxVille.BackColor = Color.LightPink;
                IsMatchVille = false;
            }

           // IsMatchTel = regTel.IsMatch(txtbxNumtel.Text.Trim());

            if (IsMatchTel)
                txtbxNumtel.BackColor = Color.PaleGreen;
            else
                txtbxNumtel.BackColor = Color.LightPink;



            string cp = txtbxCodePostal.Text;
            string ville = txtbxVille.Text;
            IsMatchVille = MgmtVilles.CheckVilleExist(ref cp, ref ville);
            txtbxCodePostal.Text = cp;
            txtbxVille.Text = ville;


            if (IsMatchVille)
            {
                txtbxCodePostal.BackColor = Color.PaleGreen;
                txtbxVille.BackColor = Color.PaleGreen;
            }
            else
            {
                if (IsMatchTel)
                    IsMatchVille = (MessageBox.Show("Cette ville est introuvable.\nSouhaitez-vous continuer?", "Ville introuvable", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes);
                else
                    IsMatchVille = false;
                txtbxCodePostal.BackColor = Color.LightPink;
                txtbxVille.BackColor = Color.LightPink;
            }


            return IsMatchTel && IsMatchVille && IsMatchNom;
        }

        private bool Build()
        {

            try
            {
                if (CurrentMutuelle == null) CurrentMutuelle = new Mutuelle();
                CurrentMutuelle.Nom = txtbxNomMut.Text;
                CurrentMutuelle.Telephone = txtbxNumtel.Text;

                CurrentMutuelle.Adresse_Nom_Voie = txtbxAdress1.Text;
                CurrentMutuelle.Adresse2 = txtbxAdress2.Text;
                CurrentMutuelle.CodePostal = txtbxCodePostal.Text;
                CurrentMutuelle.Ville = txtbxVille.Text;

                if (txtbxTxRmbrsmnt.Text != "")
                    CurrentMutuelle.TauxParDefaut = Convert.ToDouble(txtbxTxRmbrsmnt.Text);
                
                if (txtbxMntPlafond.Text != "")
                    CurrentMutuelle.MontantPlafond = Convert.ToDouble(txtbxMntPlafond.Text);
                
                CurrentMutuelle.NumMutuelle = txtbxNumMut.Text;
                CurrentMutuelle.NeedOrder = chkbxNeedOrder.Checked;

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validate() && Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
