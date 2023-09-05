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
using BaseCommonControls;

namespace WindowsFormsApplication1
{
    public partial class FrmRecontactMgmt : Form
    {

        private Recontact _RecontactToValidate = null;
        public Recontact RecontactToValidate
        {
            get
            {
                return _RecontactToValidate;
            }
            set
            {
                _RecontactToValidate = value;
            }
        }


        private Recontact _RecontactToAdd = null;
        public Recontact RecontactToAdd
        {
            get
            {
                return _RecontactToAdd;
            }
            set
            {
                _RecontactToAdd = value;
            }
        }

        private basePatient _currentpatient = null;
        public basePatient currentpatient
        {
            get
            {
                return _currentpatient;
            }
            set
            {
                _currentpatient = value;
            }
        }

        public FrmRecontactMgmt(basePatient pat)
        {
            currentpatient = pat;
            InitializeComponent();
        }

        public FrmRecontactMgmt(int Idpat)
        {
            currentpatient = baseMgmtPatient.GetPatient(Idpat);
            currentpatient.contacts = MgmtContact.getContactsOf(currentpatient.Id);
            currentpatient.recontact = MgmtRecontact.GetCurrentRecontact(currentpatient);
            InitializeComponent();
        }

        

        private void PopulateContactLists()
        {
            lvContact.Items.Clear();


            foreach (Contact cont in currentpatient.contacts)
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
        

        private void FrmRecontactMgmt_Load(object sender, EventArgs e)
        {
            PopulateContactLists();


            foreach (RecontactLib rl in MgmtMotifRecontact.LibsRecontact)
                cbxMotifRecontact.Items.Add(rl);


            if (currentpatient.recontact!=null)
                cbxMotifRecontact.Text = currentpatient.recontact.Motif;



            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();


            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                if (u.Actif)
                {
                    if (u.Fonction == "Praticien")
                        lst.Add(new BaseCommonControls.FamilyValue("Praticien", u.LastNameShort, u));
                    else
                        lst.Add(new BaseCommonControls.FamilyValue("Assistante/" + u.Prenom[0].ToString(), u.NameShort, u));

                }



            cbxutilisateur.LoadFromFamilyValueList(lst);




            cbxutilisateur.SelectByTag(UtilisateursMgt.getUtilisateurInFauteuil(Fauteuilsmgt.GetWhoIam(), DateTime.Now));
        }

        private void lvContact_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmAddContact frm;
            if (lvContact.SelectedItems.Count == 0)
                frm = new frmAddContact(currentpatient);
            else
            {
                Contact selected = (Contact)(lvContact.SelectedItems[0]).Tag;
                frm = new frmAddContact(currentpatient, selected);
            }

            DialogResult dr = frm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                MgmtContact.SaveContactTo(currentpatient.Id, frm.contact);
                
                PopulateContactLists();

            }

            if (dr == DialogResult.Abort)
            {
                currentpatient.contacts = MgmtContact.getContactsOf(currentpatient.Id);
                PopulateContactLists();
            }
        }

        private void lvContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lvContact.SelectedItems.Count > 0)
                {
                    Contact c = (Contact)lvContact.SelectedItems[0].Tag;
                    MgmtContact.DeleteContact(c);
                    currentpatient.contacts.Remove(c);
                    PopulateContactLists();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddContact frm;




            frm = new frmAddContact(currentpatient);

            DialogResult dr = frm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                MgmtContact.SaveContactTo(currentpatient.Id, frm.contact);
                currentpatient.contacts.Add(frm.contact);
                PopulateContactLists();

            }

            if (dr == DialogResult.Abort)
            {
                currentpatient.contacts = MgmtContact.getContactsOf(currentpatient.Id);
                PopulateContactLists();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOpenRHBase_Click(object sender, EventArgs e)
        {
            OLEAccess.RHBASE.setIdPatient(currentpatient.Id);
            OLEAccess.RHBASE.GotoDate(DateTime.Now);
        }

      

        private void pnlIsJoignable_OnNext(object sender, CancelEventArgs e)
        {
            
            
        }

        private void wizardControl1_Click(object sender, EventArgs e)
        {

        }

        

        private void rbOui_CheckedChanged(object sender, EventArgs e)
        {
            int idxoui = 0;
            int idxNon = 0;
            int idxEnd = 0;
            int i = 0;

            foreach (WizardBase.WizardStep step in wizardControl1.WizardSteps)
            {

                if (step == finishStep1)
                    idxEnd = i;

                if (step == pnlPoseRDV)
                    idxoui = i;

                if (step == pnlAutreContact)
                    idxNon = i;

                i++;
            }

            if (rbOui.Checked)
            {
                wizardControl1.WizardSteps[wizardControl1.CurrentStepIndex].NextStepIndex = idxoui;
                
            }
            else
            {
                wizardControl1.WizardSteps[wizardControl1.CurrentStepIndex].NextStepIndex = idxNon;
            }
            pnlPoseRDV.BackStepIndex = wizardControl1.CurrentStepIndex;
            pnlPoseRDV.NextStepIndex = idxEnd;
            pnlAutreContact.BackStepIndex = wizardControl1.CurrentStepIndex;
            pnlAutreContact.NextStepIndex = idxEnd;
            wizardControl1.Next();
        }

        private void wizardControl1_NextButtonClick(object sender, CancelEventArgs e)
        {
            if (wizardControl1.WizardSteps[wizardControl1.CurrentStepIndex] == pnlIsJoignable)
            {
                if ((!rbOui.Checked) && (!rbnon.Checked))
                {
                    MessageBox.Show("Choisir une réponse!");
                    e.Cancel = true;
                }
            }

            if (wizardControl1.WizardSteps[wizardControl1.CurrentStepIndex] == startStep1)
            {
                if (cbxutilisateur.SelectedItems.Count<=0)
                {
                    MessageBox.Show("Aucun utilisateur selectionné !");
                    e.Cancel = true;
                }
            }
        }

        private void pnlAutreContact_OnNext(object sender, CancelEventArgs e)
        {
            if (cbxMotifRecontact.Text == "")
            {
                MessageBox.Show("Aucun motif ! ");
                e.Cancel = true;
                return;
            }

            Recontact recToAdd = new Recontact();
            recToAdd.ARecontacterDepuisLe = currentpatient.recontact==null?DateTime.Now:currentpatient.recontact.ARecontacterDepuisLe;
            //recToAdd.NumRecord = currentpatient.recontact == null ? -1 : currentpatient.recontact.NumRecord;
            recToAdd.NumTentative = currentpatient.recontact == null ? -1 : currentpatient.recontact.NumTentative + 1;
            recToAdd.Creator = ((Utilisateur)cbxutilisateur.SelectedItems[0].Tag).ToString();

            if (rb15.Checked)
                recToAdd.DateProchaineTentative = DateTime.Now.AddDays(15);
            if (rb30.Checked)
                recToAdd.DateProchaineTentative = DateTime.Now.AddDays(30);
            if (rbDate.Checked)
                recToAdd.DateProchaineTentative = dtpDateRecontact.Value;

            recToAdd.DateTentative = currentpatient.recontact == null ?null : (DateTime?)DateTime.Now;
            recToAdd.IdPatient = currentpatient.Id;
            recToAdd.IdUserTentative = currentpatient.recontact == null ? -1 : ((Utilisateur)cbxutilisateur.SelectedItems[0].Tag).Id;
            recToAdd.Motif = cbxMotifRecontact.Text;
            recToAdd.patient = currentpatient;
            recToAdd.usertentative = currentpatient.recontact == null ? null : ((Utilisateur)cbxutilisateur.SelectedItems[0].Tag);

            RecontactToValidate = null;
            RecontactToAdd = recToAdd;

        }

        private void pnlPoseRDV_OnShow(object sender, EventArgs e)
        {
            RecontactToValidate = currentpatient.recontact;
            RecontactToAdd = null;
        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void pnlAutreContact_Click(object sender, EventArgs e)
        {

        }

        private void cbxutilisateur_ClickNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                wizardControl1.Next();
            }
        }

      
    }
}
