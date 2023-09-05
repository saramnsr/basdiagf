using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BasCommon_BL;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class frmAddContact : Form
    {

        Contact.ContactType currentcontacttype = Contact.ContactType.Telephone;

        private Regex regTel = new Regex(@"^0[1-9]([-. ]?[0-9]{2}){4}$|^00[0-9]{11,13}$");
        private Regex regMail = new Regex(@"^[a-zA-Z][\w\.-]{0,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        private bool IsMatchTel, IsMatchMail, IsMatchFax, IsMatchVille = false;


        private baseSmallPersonne _currentpersonne;
        public baseSmallPersonne currentpersonne
        {
            get
            {
                return _currentpersonne;
            }
            set
            {
                _currentpersonne = value;
            }
        }


        private bool ValidateMe()
        {

            IsMatchTel = true;
            if (txtbxMail.Text == String.Empty)
            {
                txtbxMail.BackColor = Color.White;
            }

            if (txtValTelephone.Text.Trim() == String.Empty || txtInd.Text.Trim() == String.Empty)
            {
                txtValTelephone.BackColor = Color.White;
                txtInd.BackColor = Color.White;
            }

            if ((txtbxCP.Text == String.Empty) && (txtbxVille.Text == string.Empty))
            {
                txtbxCP.BackColor = Color.White;
                txtbxVille.BackColor = Color.White;
            }
            if (currentcontacttype == Contact.ContactType.Telephone)
            {
                if (txtValTelephone.Text.Trim() == String.Empty || txtInd.Text.Trim() == String.Empty)
                {
                    IsMatchTel = false;
                }
            }

            // Validate
            //if (currentcontacttype == Contact.ContactType.Telephone )
            //{
            //    IsMatchTel = regTel.IsMatch(txtValTelephone.Text.Trim());

            //    if (IsMatchTel)
            //        txtValTelephone.BackColor = Color.PaleGreen;
            //    else
            //        txtValTelephone.BackColor = Color.LightPink;
            //}
            


            if (currentcontacttype == Contact.ContactType.Mail)
            {
                IsMatchMail = regMail.IsMatch(txtbxMail.Text);

                if (IsMatchMail)
                    txtbxMail.BackColor = Color.PaleGreen;
                else
                    txtbxMail.BackColor = Color.LightPink;
            }

            if (currentcontacttype == Contact.ContactType.Adresse)
            {
                string cp = txtbxCP.Text;
                string ville = txtbxVille.Text;
                IsMatchVille = MgmtVilles.CheckVilleExist(ref cp, ref ville);
                txtbxCP.Text = cp;
                txtbxVille.Text = ville;


                if (IsMatchVille)
                {
                    txtbxCP.BackColor = Color.PaleGreen;
                    txtbxVille.BackColor = Color.PaleGreen;
                }
                else
                {
                    IsMatchVille = (MessageBox.Show("Cette ville est introuvable.\nSouhaitez-vous continuer?", "Ville introuvable", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes);
                    txtbxCP.BackColor = Color.LightPink;
                    txtbxVille.BackColor = Color.LightPink;
                }
            }

            return IsMatchFax || IsMatchMail || IsMatchTel || IsMatchVille;
        }


        public frmAddContact(Correspondant cor)
        {
            baseSmallPersonne sp = null;

            if (cor != null)
            {
                sp = new baseSmallPersonne();
                sp.Id = cor.Id;
                sp.Nom = cor.Nom;
                sp.Prenom = cor.Prenom;
            }

            _contact = new Contact();
            currentpersonne = sp;
            InitializeComponent();
        }



        public frmAddContact(basePatient pat)
        {
            baseSmallPersonne sp = new baseSmallPersonne();
            sp.Id = pat.Id;
            sp.Nom = pat.Nom;
            sp.Prenom = pat.Prenom;

            _contact = new Contact();
            currentpersonne = sp;
            InitializeComponent();
        }

        public frmAddContact(baseSmallPersonne personne)
        {
            _contact = new Contact();
            currentpersonne = personne;
            InitializeComponent();
        }

        public frmAddContact(baseSmallPersonne personne, Contact contact)
        {
            currentpersonne = personne;
            _contact = contact;
            InitializeComponent();

        }

        public frmAddContact(Correspondant cor, Contact contact)
        {
            baseSmallPersonne sp = null;
            if (cor != null)
            {
                sp = new baseSmallPersonne();
                sp.Id = cor.Id;
                sp.Nom = cor.Nom;
                sp.Prenom = cor.Prenom;
            }
            _contact = contact;
            currentpersonne = sp;
            InitializeComponent();
        }

        public frmAddContact(basePatient pat, Contact contact)
        {
            baseSmallPersonne sp = new baseSmallPersonne();
            sp.Id = pat.Id;
            sp.Nom = pat.Nom;
            sp.Prenom = pat.Prenom;

            _contact = contact;
            currentpersonne = sp;
            InitializeComponent();
        }

        private Contact _contact;
        public Contact contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        private void frmAddContact_Load(object sender, EventArgs e)
        {

            List<BaseCommonControls.FamilyValue> lstTel = new List<BaseCommonControls.FamilyValue>();
            List<BaseCommonControls.FamilyValue> lstfax = new List<BaseCommonControls.FamilyValue>();
            List<BaseCommonControls.FamilyValue> lstmail = new List<BaseCommonControls.FamilyValue>();
            List<BaseCommonControls.FamilyValue> lstadress = new List<BaseCommonControls.FamilyValue>();

            txtInd.Items.AddRange(MgmtPays.pays.ToArray());



            ShowPanel(pnlTelephone);
            foreach (ContactLib cl in MgmtContactLib.contactslibs)
            {
                if ((cl.AffectedTo == ContactLib.AffecteA.Correspondant) && (currentpersonne is Correspondant) ||
                    (cl.AffectedTo == ContactLib.AffecteA.Patient) && (currentpersonne is basePatient) ||
                    (cl.AffectedTo == ContactLib.AffecteA.Tous))
                {
                    if (cl.typeCtact == Contact.ContactType.Telephone)
                        lstTel.Add(new BaseCommonControls.FamilyValue("", cl.Libelle, cl));
                    if (cl.typeCtact == Contact.ContactType.Fax)
                        lstfax.Add(new BaseCommonControls.FamilyValue("", cl.Libelle, cl));
                    if (cl.typeCtact == Contact.ContactType.Mail)
                        lstmail.Add(new BaseCommonControls.FamilyValue("", cl.Libelle, cl));
                    if (cl.typeCtact == Contact.ContactType.Adresse)
                        lstadress.Add(new BaseCommonControls.FamilyValue("", cl.Libelle, cl));
                }
            }

            cbxLibTel.LoadFromFamilyValueList(lstTel);
            cbxLibMail.LoadFromFamilyValueList(lstmail);
            cbxLibAddr.LoadFromFamilyValueList(lstadress);

            cbxLibTel.SelectedTag = lstTel[0].Tag;
            cbxLibMail.SelectedTag = lstmail[0].Tag;
            cbxLibAddr.SelectedTag = lstadress[0].Tag;

            BuildDisplay();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateMe())
            {
                BuildContact();
                DialogResult = DialogResult.OK;
            }
        }

        public void BuildDisplay()
        {
           

            if (_contact.TypeContact == Contact.ContactType.Telephone)
            {
                rbTel.Checked = true;
                cbxLibTel.SelectedTag = _contact.Libelle;
                if (_contact.pays != null)
                    txtInd.SelectedIndex = txtInd.FindStringExact(_contact.pays.ToString()) ;
                checkBox1.Checked = contact.isSms;
                ShowPanel(pnlTelephone);
                txtValTelephone.Text = _contact.Value;
              
            }
            if (_contact.TypeContact == Contact.ContactType.Mail)
            {
                rbMail.Checked = true;
                cbxLibMail.SelectedTag = _contact.Libelle;

                ShowPanel(pnlMail);
                txtbxMail.Text = _contact.Value;
            }
            if (_contact.TypeContact == Contact.ContactType.Adresse)
            {
                rbAdresse.Checked = true;
                ShowPanel(pnlAdresse);
                cbxLibAddr.SelectedTag = _contact.Libelle;
                txtbxAdr1.Text = _contact.adresse.Adr1;
                txtbxAdr2.Text = _contact.adresse.Adr2;
                txtbxCP.Text = _contact.adresse.CodePostal;
                txtbxVille.Text = _contact.adresse.Ville;

            }

            // txtLibelle.Text = _contact.Libelle;
        }

        public void BuildContact()
        {
            if (_contact == null) _contact = new Contact();
            if (rbTel.Checked)
            {
                _contact.TypeContact = Contact.ContactType.Telephone;
                _contact.Libelle = (ContactLib)cbxLibTel.SelectedTag;
                _contact.isSms = checkBox1.Checked;
                _contact.id_pays = ((Pays)txtInd.SelectedItem).id;
                _contact.pays = MgmtPays.getPaysById(_contact.id_pays);
                _contact.Value = txtValTelephone.Text;
            }


            if (rbMail.Checked)
            {
                _contact.TypeContact = Contact.ContactType.Mail;
                _contact.Libelle = (ContactLib)cbxLibMail.SelectedTag;
                _contact.Value = txtbxMail.Text;
            }
            if (rbAdresse.Checked)
            {
                _contact.TypeContact = Contact.ContactType.Adresse;
                _contact.Libelle = (ContactLib)cbxLibAddr.SelectedTag;
                _contact.adresse = new ContactAdresse();
                _contact.adresse.Adr1 = txtbxAdr1.Text;
                _contact.adresse.Adr2 = txtbxAdr2.Text;
                _contact.adresse.CodePostal = txtbxCP.Text;
                _contact.adresse.Ville = txtbxVille.Text;

            }



            //_contact.Libelle = txtLibelle.Text;
            //_contact.isMain = false;
        }

        private void validating(object sender, CancelEventArgs e)
        {
            Regex rTel = new Regex(@"^0[1-9]([-. ]?[0-9]{2}){4}$");
            if (txtValTelephone.Text.Length > 0)
            {
                if (!rTel.IsMatch(txtValTelephone.Text))
                {
                    MessageBox.Show("Le numéro de téléphone ne semble pas valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtValTelephone.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlAdresse_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShowPanel(Panel pnlToShow)
        {

            foreach (Panel pnl in pnlContainer.Controls)
            {
                pnl.Visible = false;
            }
            pnlToShow.Visible = true;

        }


        private void cbxTypeContact_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cbxAdrLib_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbTel_CheckedChanged(object sender, EventArgs e)
        {
            ShowPanel(pnlTelephone);
            currentcontacttype = Contact.ContactType.Telephone;
        }



        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ShowPanel(pnlMail);
            currentcontacttype = Contact.ContactType.Mail;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ShowPanel(pnlAdresse);
            currentcontacttype = Contact.ContactType.Adresse;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cbxLibTel_Click(object sender, EventArgs e)
        {

        }


    }
}
