using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;
using FrmContainer_BO;
using FrmContainer_BL;

namespace WindowsFormsApplication1
{
    public partial class FrmListesAssistante : Form
    {
        public FrmListesAssistante()
        {
            InitializeComponent();
        }




        private void InitDisplayListePatientsSoldeNegatif()
        {
            List<PatientSoldeNegatifCeJour> lst = ListeAssistanteMgmt.GetPatientsSoldeNegatifCeJour(((Utilisateur)cbxAssistante.SelectedItem));

            foreach (PatientSoldeNegatifCeJour par in lst)
            {
                object[] obs = new object[]
                {
                    par.Nom+' '+par.Prenom,
                    par.DateDeDerniereEcheance,
                    par.Montant
                };

                int idx = dgvSoldeNeg.Rows.Add(obs);
                dgvSoldeNeg.Rows[idx].Tag = par;
            }
            
        }

        private void InitDisplayListePatientsWithStatut(StatusCliniqueManuel status, Utilisateur AssResponsable)
        {
            List<baseSmallPersonne> lst = ListeAssistanteMgmt.getPatientsEnStatus(status, AssResponsable);

            dgvpatientstatut.Rows.Clear();

            foreach (baseSmallPersonne par in lst)
            {
                object[] obs = new object[]
                {
                    par.Nom+' '+par.Prenom
                };

                int idx = dgvpatientstatut.Rows.Add(obs);
                dgvpatientstatut.Rows[idx].Tag = par;
            }

        }

        private void OuvrirSelection(object pat)
        {
            
            try
            {
                              

                if (pat is PatientEnRecontact)
                {
                    if (((PatientEnRecontact)pat).IsPatientOrthalis)
                    {
                        //if (app == null) return;
                        //if (app.patient == null) return;

                        ApplicationMgmt.OpenOrthalis();
                        System.Threading.Thread.Sleep(500);
                        OLEAccess.Orthalis.SetPatientCourantByNomPrenom(((PatientEnRecontact)pat).Nom, ((PatientEnRecontact)pat).Prenom, false);
                        OLEAccess.Orthalis.Activate();

                    }
                    else
                        MgmtCommonCache.Change(((PatientEnRecontact)pat).Id);
                    
                     //   OLEServer.MainForm.Invoke(OLEServer.MainForm.setPatientById, new object[1] { ((PatientEnRecontact)pat).Id });
                }

            }
            catch (SystemException ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        private void InitDisplayListePatientsEnRecontact()
        {

            if (dgvRelance.Columns.Count == 0) return;
            dgvRelance.Rows.Clear();
            List<PatientEnRecontact> lst = ListeAssistanteMgmt.getPatientsEnRecontact(cbxAssistante.SelectedItem is Utilisateur?((Utilisateur)cbxAssistante.SelectedItem):null);

            foreach (PatientEnRecontact par in lst)
            {
                object[] obs = new object[]
                {
                    par.Nom+' '+par.Prenom,
                    par.DateDernierRDV,
                    par.Motif,
                    par.DepuisLe,
                    par.NumTentative==0?"aucune tentatives":par.NumTentative.ToString()+" essai(s) -> "+ par.DerniereTentative.ToString("dd/MM/yyyy")
                };

                int idx = dgvRelance.Rows.Add(obs);
                dgvRelance.Rows[idx].Tag = par;
            }

        }

        /*
        private void InitDisplayListePatientsSansRDV()
        {
            List<PatientARecontacter> lst = MgmtPatient.getPatientsSansProchainsRDV();

            foreach (PatientARecontacter par in lst)
            {
                object[] obs = new object[]
                {
                    par.Nom+' '+par.Prenom,
                    par.DateDernierRDV
                };

                int idx = dgvEncaissements.Rows.Add(obs);
                dgvEncaissements.Rows[idx].Tag = par;
            }
            
        }
        */

        private void FrmListesAssistante_Load(object sender, EventArgs e)
        {
            // InitDisplayListePatientsSansRDV();
            InitDisplayListePatientsSoldeNegatif();
            InitDisplayListePatientsEnRecontact();


            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();


            foreach (StatusCliniqueManuel scm in StatusCliniqueManuelMgmt.status)
                lst.Add(new BaseCommonControls.FamilyValue(scm.Organisation,scm.Libelle,scm));

            lstbxStatus.LoadFromFamilyValueList(lst);

            cbxAssistante.Items.Add("Tous");
            foreach (Utilisateur usr in UtilisateursMgt.utilisateurs)
                if ((usr.Actif))
                    cbxAssistante.Items.Add(usr);

        }

        //private void dgvEncaissements_MouseDoubleClick(object sender, MouseEventArgs e)
        //{

        //    PatientARecontacter pat = ((PatientARecontacter)dgvEncaissements.SelectedRows[0].Tag);

        //    FrmResumeContact frm = new FrmResumeContact(pat.Id);
        //    frm.Text = pat.Nom + " " + pat.Prenom;
        //    frm.ShowDialog();
        //}

        private void dgvSoldeNeg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            PatientSoldeNegatifCeJour pat = ((PatientSoldeNegatifCeJour)dgvSoldeNeg.SelectedRows[0].Tag);

            FrmResumeContact frm = new FrmResumeContact(pat.Id);
            frm.Text = pat.Nom + " " + pat.Prenom;
            frm.ShowDialog();
             */
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void lstbxStatus_OnSelectionChange(object sender, EventArgs e)
        {
            SearchStatus();
        }

        private void SearchStatus()
        {
            if ((lstbxStatus.SelectedItems.Count > 0) && (lstbxStatus.SelectedItems[0].Tag is StatusCliniqueManuel))
            {
                StatusCliniqueManuel s = ((StatusCliniqueManuel)lstbxStatus.SelectedItems[0].Tag);
                InitDisplayListePatientsWithStatut(s, cbxAssistante.SelectedItem is Utilisateur ? ((Utilisateur)cbxAssistante.SelectedItem) : null);
            }
        }

        private void dgvpatientstatut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            baseSmallPersonne pat = ((baseSmallPersonne)dgvpatientstatut.SelectedRows[0].Tag);

            FrmResumeContact frm = new FrmResumeContact(pat.Id);
            frm.Text = pat.Nom + " " + pat.Prenom;
            frm.ShowDialog();
             */
        }

        private void dgvRelance_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PatientEnRecontact pat = ((PatientEnRecontact)dgvRelance.SelectedRows[0].Tag);


            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OuvrirSelection));
            th.Start(pat);


            FrmRecontactMgmt frm = new FrmRecontactMgmt(pat.Id);

            frm.FormClosing += new FormClosingEventHandler(frm_FormClosing);
            frm.Show();
           
        }

        void frm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (((FrmRecontactMgmt)sender).DialogResult == DialogResult.OK)
            {
                if (((FrmRecontactMgmt)sender).RecontactToAdd != null)
                {
                    MgmtRecontact.AddRecontact(((FrmRecontactMgmt)sender).RecontactToAdd);
                    InitDisplayListePatientsEnRecontact();
                }

                if (((FrmRecontactMgmt)sender).RecontactToValidate != null)
                {
                    MgmtRecontact.ValidateRecontacts (((FrmRecontactMgmt)sender).RecontactToValidate .IdPatient );
                    InitDisplayListePatientsEnRecontact();
                }
            }
        }

        private void cbxAssistante_SelectedIndexChanged(object sender, EventArgs e)
        {

            REfreshSearch();
            
        }

        private void REfreshSearch()
        {
            if (tabControl1.SelectedTab == tabPage3)
                SearchStatus();

            if (tabControl1.SelectedTab == tabPage4)
                InitDisplayListePatientsEnRecontact();


            if (tabControl1.SelectedTab == tabPage2)
                InitDisplayListePatientsSoldeNegatif();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            REfreshSearch();
        }

        private void dgvRelance_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void lstbxStatus_MouseClick(object sender, MouseEventArgs e)
        {
            SearchStatus();
        }
    }
}
