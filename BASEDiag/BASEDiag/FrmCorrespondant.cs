using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag
{
    public partial class FrmCorrespondant : Form
    {


        private TypePers _defautType = null;
        public TypePers defautType
        {
            get
            {
                return _defautType;
            }
            set
            {
                _defautType = value;
            }
        }
        
        
        private Correspondant _Correspondant;
        public Correspondant Correspondant
        {
            get
            {
                return _Correspondant;
            }
            set
            {
                _Correspondant = value;
            }
        }

        public FrmCorrespondant()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BuildDisplay()
        {
            if (Correspondant == null) return;
            txtbxNom.Text = Correspondant.Nom;
            txtbxPrenom.Text = Correspondant.Prenom;
            cbxCivilite.Text = Correspondant.Titre;
            cbxSexe.Text = Correspondant.GenreFeminin?"F":"M";

            cbxTypeCorrespondant.SelectedItem = TypeCorresMgmt.getType(Correspondant.Type);

           
            txtbxProfession.Text = Correspondant.Profession;

            



            
        }

        private void BuildCorres()
        {
            if (Correspondant == null) Correspondant = new Correspondant();

            if (cbxTypeCorrespondant.SelectedItem == null)
                throw new System.Exception("Veuillez choisir un type !");
            if (txtbxNom.Text == "")
                throw new System.Exception("Veuillez choisir un nom !");
            if (txtbxPrenom.Text == "")
                throw new System.Exception("Veuillez choisir un prenom !");
            if (cbxCivilite.Text == "")
                throw new System.Exception("Veuillez choisir une civilité !");
            if (cbxSexe.Text == "")
                throw new System.Exception("Veuillez choisir un genre !");


            Correspondant.Nom = txtbxNom.Text.ToUpper();
            Correspondant.Prenom = txtbxPrenom.Text[0].ToString().ToUpper() + txtbxPrenom.Text.Substring(1).ToLower();
                
            Correspondant.Titre = cbxCivilite.Text;
            Correspondant.GenreFeminin = cbxSexe.Text == "F";
           Correspondant.Type = ((TypePers)cbxTypeCorrespondant.SelectedItem).IdType;

                       
            
            
            Correspondant.Profession = txtbxProfession.Text;

            if (rbCourrier.Checked) Correspondant.PrefCom = Correspondant.EnumPrefCom.Courrier;
            if (rbEmail.Checked) Correspondant.PrefCom = Correspondant.EnumPrefCom.Email;
            if (rbFax.Checked) Correspondant.PrefCom = Correspondant.EnumPrefCom.Fax;

            
           

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                BuildCorres();

                DialogResult = DialogResult.OK;
                Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCorrespondant_Load(object sender, EventArgs e)
        {
            BuildDisplay();

            foreach (TypePers perstype in TypeCorresMgmt.TypePers)
            {
                cbxTypeCorrespondant.Items.Add(perstype);
            }

            if (defautType != null)
            {
                cbxTypeCorrespondant.SelectedItem = defautType;
                txtbxProfession.Text = cbxTypeCorrespondant.Text;
            }


            ThreadPool.QueueUserWorkItem(new WaitCallback(FillAutocomplete));

           

        }

        

        delegate void SetSourceDelegate(List<object> source);
        



        void FillAutocomplete(object state)
        {
            List<object> lstSugested = MgmtCorrespondants.getCorrespondantsSuggested("");
           
            if (lstSugested.Count > 0)
                SetAutocompleteSource(lstSugested);
        }

        void SetAutocompleteSource(List<object> source)
        {
            if (InvokeRequired)
                Invoke(new SetSourceDelegate(SetAutocompleteSource), source);
            else
            {
                SuggestBxCorrespondant.SuggestionList = source;
            }
        }




       
        /*
        private void btnAddMail_Click(object sender, EventArgs e)
        {
            FrmAddContact frm = new FrmAddContact();
            frm.cbxType.Text = Contact.ContactType.Mail.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lstcontact.Add(frm.contact);
                ListViewItem itm = new ListViewItem();
                itm.Text = frm.contact.Value;
                itm.SubItems.Add(frm.contact.Libelle);
                lstviewMail.Items.Add(itm);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddContact frm = new FrmAddContact();
            frm.cbxType.Text = Contact.ContactType.Fax.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lstcontact.Add(frm.contact);
                ListViewItem itm = new ListViewItem();
                itm.Text = frm.contact.Value;
                itm.SubItems.Add(frm.contact.Libelle);
                lstviewNumFax.Items.Add(itm);
            }
        }

        private void btnAddTel_Click(object sender, EventArgs e)
        {
            FrmAddContact frm = new FrmAddContact();
            frm.cbxType.Text = Contact.ContactType.Telephone.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lstcontact.Add(frm.contact);
                ListViewItem itm = new ListViewItem();
                itm.Text = frm.contact.Value;
                itm.SubItems.Add(frm.contact.Libelle);
                lstviewNumTel.Items.Add(itm);
            }
        }
        
        private void lstviewMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem itm in lstviewMail.SelectedItems)
                {
                    lstviewMail.Items.Remove(itm);
                    lstcontact.Remove((Contact)itm.Tag);
                }
            }
        }

        private void lstviewNumFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem itm in lstviewNumFax.SelectedItems)
                {
                    lstviewNumFax.Items.Remove(itm);
                    lstcontact.Remove((Contact)itm.Tag);
                }
            }
        }

        private void lstviewNumTel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem itm in lstviewNumTel.SelectedItems)
                {
                    lstviewNumTel.Items.Remove(itm);
                    lstcontact.Remove((Contact)itm.Tag);
                }
            }
        }

        */

        private void SuggestBxCorrespondant_OnYesClick(object sender, EventArgs e)
        {
            Correspondant = MgmtCorrespondants.getCorrespondant(((baseSmallPersonne)SuggestBxCorrespondant.value).Id);
            
        }

        private void SuggestBxCorrespondant_OnFound(object sender, EventArgs e)
        {
            SuggestBxCorrespondant.LabelText = "Souhaitez-vous parler de " + ((baseSmallPersonne)SuggestBxCorrespondant.value).Nom + " " + ((baseSmallPersonne)SuggestBxCorrespondant.value).Prenom + " ? ";
        }

        private void txtbxNom_KeyUp(object sender, KeyEventArgs e)
        {
            SuggestBxCorrespondant.Suggest(txtbxNom.Text);
        }

       // TextBox txtbxv;
     //   TextBox txtbxcp;

       
     
      
        private void SuggestBxCorrespondant_Load(object sender, EventArgs e)
        {

        }

        private void cbxCivilite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCivilite.Text == "M" ||
                cbxCivilite.Text == "Dr" ||
                cbxCivilite.Text == "Pr" ||
                cbxCivilite.Text == "P" ||
                cbxCivilite.Text == "Sr")
                cbxSexe.Text = "M";
            else
                cbxSexe.Text = "F";

        }

        private void txtbxCodePostal_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbxNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxTypeCorrespondant_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxProfession.Text = cbxTypeCorrespondant.Text;
        }

       


       
    }
}
