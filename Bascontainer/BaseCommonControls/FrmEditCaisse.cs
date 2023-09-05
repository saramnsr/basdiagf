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
    public partial class FrmEditCaisse : Form
    {

        private Regex regTel = new Regex(@"^0[1-9]([-. ]?[0-9]{2}){4}$|^00[0-9]{11,13}$");
        private bool IsMatchTel, IsMatchVille, IsMatchNom = false;

        private Caisse _CurrentCaisse;
        public Caisse CurrentCaisse
        {
            get
            {
                return _CurrentCaisse;
            }
            set
            {
                _CurrentCaisse = value;
            }
        }

        public FrmEditCaisse(Caisse c)
        {
            CurrentCaisse = c;
            InitializeComponent();
        }

        private void FrmEditMutuelle_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void InitDisplay()
        {
            if (CurrentCaisse != null)
            {
                txtbxNomMut.Text = CurrentCaisse.Nom;
                txtbxNumtel.Text = CurrentCaisse.TelFixe;

                txtbxAdress1.Text = CurrentCaisse.Adresse_Num + " " + CurrentCaisse.Adresse_Type_Voie + " " + CurrentCaisse.Adresse_Nom_Voie;
                txtbxAdress2.Text = CurrentCaisse.Adresse2;
                txtbxCodePostal.Text = CurrentCaisse.CodePostal;
                txtbxVille.Text = CurrentCaisse.Ville;
                chkbxNeedOrder.Checked = CurrentCaisse.NeedOrder;
                
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
                if (CurrentCaisse == null) CurrentCaisse = new Caisse();
                CurrentCaisse.Nom = txtbxNomMut.Text;
                CurrentCaisse.TelFixe = txtbxNumtel.Text;

                CurrentCaisse.Adresse_Nom_Voie = txtbxAdress1.Text;
                CurrentCaisse.Adresse2 = txtbxAdress2.Text;
                CurrentCaisse.CodePostal = txtbxCodePostal.Text;
                CurrentCaisse.Ville = txtbxVille.Text;
                CurrentCaisse.NeedOrder = chkbxNeedOrder.Checked;

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
