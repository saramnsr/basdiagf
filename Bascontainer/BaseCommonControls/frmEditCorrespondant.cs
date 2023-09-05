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
    public partial class frmEditCorrespondant : Form
    {



        private List<CustomCategorie> _ccToRemove = new List<CustomCategorie>();
        private List<CustomCategorie> _ccToAdd = new List<CustomCategorie>();
        

            private basePatient _CurrentPatient;
            public basePatient CurrentPatient
            {
                get
                {
                    return _CurrentPatient;
                }
                set
                {
                    _CurrentPatient = value;
                    btnCopy.Visible = CurrentPatient != null;
                }
            }


            private List<CustomCategorie> _lstCateg = new List<CustomCategorie>();
        public List<Contact> lstContacts = new List<Contact>();


        private Correspondant _correspondant;
        public Correspondant correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                _correspondant = value;
            }
        }

        public frmEditCorrespondant()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void PopulateContactLists()
        {


            lvContact.Items.Clear();

            foreach (Contact cont in lstContacts)
            {

                ListViewItem itemc = new ListViewItem();
                itemc.Text = cont.Libelle == null ? "" : cont.Libelle.Libelle;
                itemc.SubItems.Add(cont.Value);

                if (cont.TypeContact == Contact.ContactType.Telephone)
                    itemc.Group = lvContact.Groups[0];
                if (cont.TypeContact == Contact.ContactType.Fax)
                    itemc.Group = lvContact.Groups[1];
                if (cont.TypeContact == Contact.ContactType.Mail)
                    itemc.Group = lvContact.Groups[2];
                if (cont.TypeContact == Contact.ContactType.Adresse)
                    itemc.Group = lvContact.Groups[3];


                itemc.Tag = cont;

                lvContact.Items.Add(itemc);

            }
        }


        private void InitDisplay()
        {
            if (correspondant == null) return;

            lstContacts = MgmtContact.getContactsOf(correspondant.Id);
            _lstCateg = CategoriesMgmt.GetCurrentCategoriesFromIdPersonne(correspondant.Id);

            txtbxNom.Text = correspondant.Nom;
            txtbxPrenom.Text = correspondant.Prenom;


            cbxCivilite.Text = correspondant.Titre;
            cbxSexe.Text = correspondant.GenreFeminin ? "Féminin" : "Masculin";

            rbTu.Checked = correspondant.TuToiement;


            txtbxNumSecu.Text = correspondant.numSecu;
           
            ucNotes1.RealValue = correspondant.Note;

            

            txtbxProfession.Text = correspondant.Profession;


           


            

            PopulateContactLists();
        }

        private bool Build()
        {

            if (txtbxNom.Text == "")
            {
                MessageBox.Show("Nom non saisie !");
                return false;
            }
            if (correspondant != null)
            {
                if (cbxCivilite.Text == "")
                {
                    MessageBox.Show("Civilite non saisie !");
                    return false;
                }

                if (txtbxPrenom.Text == "")
                {
                    MessageBox.Show("Prenom non saisie !");
                    return false;
                }
            }
            if (correspondant == null) correspondant = new Correspondant();


            correspondant.Nom = txtbxNom.Text;
            correspondant.Prenom = txtbxPrenom.Text;


            correspondant.Titre = cbxCivilite.Text;
            correspondant.GenreFeminin = cbxSexe.Text == "Féminin";

            correspondant.TuToiement = rbTu.Checked ;


            correspondant.numSecu = txtbxNumSecu.Text;

            correspondant.DateNaissance = null;

            correspondant.Profession = txtbxProfession.Text;


            CorrespondantType type = MgmtCorrespondants.FindType(correspondant.Profession);

            correspondant.Type = type==null?0:type.Id;
            correspondant.Note = ucNotes1.RealValue;


            return true;
            
        }


        

        private void frmEditCorrespondant_Load(object sender, EventArgs e)
        {
            InitDisplay();

            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();
            lst.Add(new BaseCommonControls.FamilyValue("Masculin", "Dr.", "Dr."));
            lst.Add(new BaseCommonControls.FamilyValue("Masculin", "M.", "M."));
            lst.Add(new BaseCommonControls.FamilyValue("Masculin", "Mr", "Mr"));
            lst.Add(new BaseCommonControls.FamilyValue("Feminin", "Me", "M."));
            lst.Add(new BaseCommonControls.FamilyValue("Feminin", "Mlle", "Mlle"));
            lst.Add(new BaseCommonControls.FamilyValue("Feminin", "MM.", "MM."));
            lst.Add(new BaseCommonControls.FamilyValue("Feminin", "Mme", "Mme"));
            lst.Add(new BaseCommonControls.FamilyValue("Masculin", "Pr.", "Pr."));

            cbxCivilite.LoadFromFamilyValueList(lst);

            lst.Clear();

            lst.Add(new BaseCommonControls.FamilyValue("", "Féminin", "F"));
            lst.Add(new BaseCommonControls.FamilyValue("", "Masculin", "M"));

            cbxSexe.LoadFromFamilyValueList(lst);



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       

        private void btnOk_Click(object sender, EventArgs e)
        {

            bool isnew = correspondant == null;

            if (Build())
            {

                MgmtCorrespondants.SaveCorrespondant(correspondant);

                MgmtContact.DeleteContactOf(correspondant.Id);

                foreach (Contact cc in lstContacts)
                {
                    cc.IdPersonne = correspondant.Id;
                    MgmtContact.InsertContactTo(cc);
                }


                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnAddCorrespondant_Click(object sender, EventArgs e)
        {
            frmAddContact frm;


            baseSmallPersonne sp = new baseSmallPersonne();
            sp.Nom = txtbxNom.Text;
            sp.Prenom = txtbxPrenom.Text;

            frm = new frmAddContact(correspondant);

            DialogResult dr = frm.ShowDialog();

            if (dr == DialogResult.OK)
            {
               // frm.contact.IdPersonne = correspondant.Id;
               // MgmtContact.SaveContactTo( frm.contact);
                lstContacts.Add(frm.contact);
                PopulateContactLists();

            }

            if (dr == DialogResult.Abort)
            {
                List<Contact> lst  = MgmtContact.getContactsOf(correspondant.Id);

                foreach (Contact c in lst)
                    lstContacts.Add(c);

                PopulateContactLists();
            }

            
        }

        private void lvContact_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmAddContact frm;
            if (lvContact.SelectedItems.Count == 0)
                frm = new frmAddContact(correspondant);
            else
            {
                Contact selected = (Contact)(lvContact.SelectedItems[0]).Tag;
                frm = new frmAddContact(correspondant, selected);
            }

            DialogResult dr = frm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                PopulateContactLists();

            }

            if (dr == DialogResult.Abort)
            {
                lstContacts = MgmtContact.getContactsOf(correspondant.Id);
                PopulateContactLists();
            }
        }

        private void lvContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Contact c = (Contact)lvContact.SelectedItems[0].Tag;
                lstContacts.Remove(c);
                PopulateContactLists();
            }
        }

       

        private void cbxProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            baseSmallPersonne bs = null;




            if (CurrentPatient != null) bs = CurrentPatient;
            else
            {
                FrmChoixCorrespondant frmchoo = new FrmChoixCorrespondant();
                if (frmchoo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bs = frmchoo.Value;
                }
            }
            if (bs == null) return;

            if (bs.contacts == null)
            {
                if (bs is basePatient)
                    baseMgmtPatient.FillContacts((basePatient)bs);

                if (bs is Correspondant)
                    MgmtCorrespondants.FillContacts((Correspondant)bs);
            }

            foreach (Contact c in bs.contacts)
            {
                ListViewItem itm = new ListViewItem();
                itm.Text = c.Libelle == null ? "" : c.Libelle.Libelle;
                itm.SubItems.Add(c.Value);

                if (c.TypeContact == Contact.ContactType.Telephone)
                    itm.Group = lvContact.Groups[0];
                if (c.TypeContact == Contact.ContactType.Fax)
                    itm.Group = lvContact.Groups[1];
                if (c.TypeContact == Contact.ContactType.Mail)
                    itm.Group = lvContact.Groups[2];
                if (c.TypeContact == Contact.ContactType.Adresse)
                    itm.Group = lvContact.Groups[3];

                itm.Tag = c;

                lvContact.Items.Add(itm);

                lstContacts.Add(c);
            }
        }

        private void cbxCivilite_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtbxPrenom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbxPrenom_Leave(object sender, EventArgs e)
        {
            txtbxPrenom.Text = txtbxPrenom.Text.Length>=2? txtbxPrenom.Text[0].ToString().ToUpper() + txtbxPrenom.Text.Substring(1):txtbxPrenom.Text;
        }

        private void txtbxNom_Leave(object sender, EventArgs e)
        {
            txtbxNom.Text = txtbxNom.Text.ToUpper();
        }

        private void frmEditCorrespondant_Shown(object sender, EventArgs e)
        {
            txtbxNom.Focus();
        }

        private void envoyerUnEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (correspondant.contacts == null)
                MgmtCorrespondants.FillContacts(correspondant);

            Contact mail = correspondant.MainMail;

            if (mail == null) return;

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "mailto:" + mail.Value;
            p.Start();  
        }
        
    }
}
