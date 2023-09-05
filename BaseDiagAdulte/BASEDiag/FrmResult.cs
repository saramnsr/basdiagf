using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmResult : Form
    {

        int CurrentScreenIdx = 0;

        private Patient _CurrentPatient;
        public Patient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
                

                devis = DevisMgmt.getLastDevis(_CurrentPatient);
                

                
            }
        }

        


        private Devis _devis;
        public Devis devis
        {
            get
            {
                return _devis;
            }
            set
            {
                _devis = value;
            }
        }

        
        public FrmResult(Patient patient, int screenidx )
        {
            CurrentPatient = patient;
            InitializeComponent();
            CurrentScreenIdx = screenidx;
        }

        private void FrmResult_Load(object sender, EventArgs e)
        {
            List<ObjSuivi> lstobj =  SuiviTravauxBaseLaboMgmt.GetAllAppareils(CurrentPatient);
            foreach (ObjSuivi o in lstobj)
            {
                if (lblAppDejaPoses.Text == "") lblAppDejaPoses.Text += "\r\n";
                lblAppDejaPoses.Text += o.NatureTravaux + "posé le " + o.PoseApp.Value.ToString("dd MMM yyyy à hh:mm");
            }
            if (lblAppDejaPoses.Text == "") lblAppDejaPoses.Text = "Aucun appareil posé!";

            Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();

            if (IamTheFauteuil == null)
            {
                FrmChoixFauteuil frm = new FrmChoixFauteuil();
                frm.ShowDialog();
                IamTheFauteuil = frm.Selection;
            }


            List<Utilisateur> currentutilisateurs = UtilisateursMgt.getUtilisateurInFauteuil(IamTheFauteuil, DateTime.Now);

            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
            {
                if (u.Actif)
                {
                    if (u.Fonction == "Praticien")
                        cbxPratResp.Items.Add(u);
                    else
                        cbxAssResp.Items.Add(u);
                }
            }

            foreach (Utilisateur u in currentutilisateurs)
            {
                if (cbxPratResp.Items.Contains(u)) cbxPratResp.SelectedItem = u;
                if (cbxAssResp.Items.Contains(u)) cbxAssResp.SelectedItem = u;
            }


           

            foreach (TypeDevis td in TypeDevisMgmt.lstTypeDevis)
            {
                cbxDevis.Items.Add(td);
            }

            InitDisplay();
            
        }


        private void BuildInfocomplementaire()
        {
            if (CurrentPatient.infoscomplementaire == null)
            {
                CurrentPatient.infoscomplementaire = new InfoPatientComplementaire();
                CurrentPatient.infoscomplementaire.IdPatient = CurrentPatient.Id;
            }

            CurrentPatient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;
            CurrentPatient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;

            
        }

        private void InitDisplay()
        {

            

           
            EntentePrealable ep = DemandeEntenteMgmt.CreateEntenteFromResume(ResumeCliniqueMgmt.resumeCl);
            txtbxResumeSecu.Text = DemandeEntenteMgmt.getResume(ep);

            txtbxResume.Text = ResumeCliniqueMgmt.Resume;
            txtBxComptRenduCourrier.Text = ResumeCliniqueMgmt.GenerateCompteRenduClinique();
            //txtBxComptRenduCourrier.Text = ResumeCliniqueMgmt.GenerateAmeliorations();
            txtBxComptRenduCourrier.Text = txtBxComptRenduCourrier.Text.Replace("\n", "\r\n");



            if (CurrentPatient.AgeNbYears < 3)
            {
                cbxDevis.SelectedItem = TypeDevisMgmt.getTypeDevis(TypeDevis.CategorieDevis.Sucette);//Sucette
                
            }
            else
            {
                if (CurrentPatient.AgeNbYears < 10)
                {
                    cbxDevis.SelectedItem = TypeDevisMgmt.getTypeDevis(TypeDevis.CategorieDevis.Orthopedique);//orthopédique
                  
                }
                else
                {
                    if (CurrentPatient.AgeNbYears < 16)
                    {
                        cbxDevis.SelectedItem = TypeDevisMgmt.getTypeDevis(TypeDevis.CategorieDevis.Orthodontique);//orthodontique bagues metal
                
                    }
                    else
                    {
                        if (CurrentPatient.AgeNbYears >= 16)
                        {
                            cbxDevis.SelectedItem = TypeDevisMgmt.getTypeDevis(TypeDevis.CategorieDevis.Invisalign);//Traitement d’alignement de canine à canine* haut et bas
                    
                        }
                    }
                }
            }




            if (CurrentPatient.infoscomplementaire != null)
            {
                cbxPratResp.SelectedItem = CurrentPatient.infoscomplementaire.PraticienResponsable;
                cbxAssResp.SelectedItem = CurrentPatient.infoscomplementaire.AssistanteResponsable;



            }
            if (devis != null) cbxDevis.SelectedItem = devis.typedevis;

            InitDisplayForDevis();


            
        }

        private void InitDisplayForDevis()
        {
            btnDevis.Enabled = (devis == null);
            cbxDevis.Enabled = (devis == null);
        }

        private void txtbxResume_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmProperties frm= new FrmProperties();
            frm.ShowDialog();
            txtbxResume.Text = ResumeCliniqueMgmt.Resume;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDEP frm = new FrmDEP(CurrentPatient);
            frm.PlanDeTraitement = txtbxPlanTraitement.Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.IsPrinted)
                {
                    if (frm.depCtrl1.entente.IdModele == -1)
                    {
                        DemandeEntenteMgmt.InsertDiagEntente(frm.depCtrl1.entente);
                        DemandeEntenteMgmt.InsertEntenteWithoutDiag(frm.depCtrl1.entente);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            BuildInfocomplementaire();
            PatientMgmt.setinfocomplementaire(CurrentPatient.infoscomplementaire);
            DialogResult = DialogResult.OK;
            Close();
        }

       
        
        private void btnBasLabo_Click(object sender, EventArgs e)
        {
            BASEDiag_BL.OLEAccess.BASELabo.NouvelleDemande(CurrentPatient.Id);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant Praticien = frm.Praticien;

                foreach (SmallCorrespondant sc in frm.lstCorrespondant)
                {
                    Correspondant c = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(sc.Id);

                    GenerateCourrier(frm.FileName, Praticien, c);
                }
            }
        }

        private void GenerateCourrier(string file, Correspondant Praticien, Correspondant c)
        {
            AddCourrierAttributs(Praticien, c);

            
                AddCourrierAttributsForDevis();

            BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }

        private void AddCourrierAttributsForDevis()
        {

            if (devis != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Devis", devis.Id.ToString());
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TypeDeDevis", devis.typedevis.libelle);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IdTypeDeDevis", devis.typedevis.Id.ToString());
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DureeTraitement", devis.Duree.ToString());
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateProposition", devis.DateProposition.ToString());

                if (devis.typedevis.DevisInvisalign)
                {

                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Facette_Esthetique", devis.FacetteEsthetique ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Kit_Eclaircissement", devis.KitEclaircissement ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Kit_Traction", devis.KitTractionSurMiniVis ? "VRAI" : "FAUX");

                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_1_Arcade", devis.ContentionBAS1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_2_Arcades", devis.ContentionBAS1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_Jeu", devis.ContentionBASJeu ? "VRAI" : "FAUX");

                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_1_Arcade", devis.ContentionVIVERA1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_2_Arcades", devis.ContentionVIVERA2Arcades ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_Jeu", devis.ContentionVIVERAJeu ? "VRAI" : "FAUX");

                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Metal_1_Arcade", devis.ContentionFilMetal1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Metal_2_Arcades", devis.ContentionFilMetal2Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Or_1_Arcade", devis.ContentionFilOr1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Or_2_Arcades", devis.ContentionFilOr2Arcades ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Fibre_1_Arcade", devis.ContentionFilFibre1Arcade ? "VRAI" : "FAUX");
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Fibre_2_Arcades", devis.ContentionFilFibre2Arcades ? "VRAI" : "FAUX");

                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbDeMiniVis", devis.NbMiniVis.ToString());

                }
            }
            else
            {

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Devis", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TypeDeDevis", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IdTypeDeDevis", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DureeTraitement", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateProposition", "");
                
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Facette_Esthetique", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Kit_Eclaircissement", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Kit_Traction", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_1_Arcade", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_2_Arcades", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_BAS_Jeu", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_1_Arcade", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_2_Arcades", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("contention_VIVERA_Jeu", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Metal_1_Arcade", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Metal_2_Arcades", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Or_1_Arcade", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Or_2_Arcades", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Fibre_1_Arcade", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maintien_Fil_Fibre_2_Arcades", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbDeMiniVis", "0");

            
            }
        }

        private void AddCourrierAttributs(Correspondant Praticien, Correspondant c)
        {
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", _CurrentPatient.AgeNbYears.ToString() + " ans");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.Ville);
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", _CurrentPatient.NextRDV.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _CurrentPatient.DateSoldePatient.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _CurrentPatient.Solde.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", _CurrentPatient.LastRDV.ToString());


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreCorrespondant", c.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomCorrespondant", c.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomCorrespondant", c.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailCorrespondant", c.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionCorrespondant", c.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeCorrespondant", c.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProCorrespondant", c.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortableCorrespondant", c.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxCorrespondant", c.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", c.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", c.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", c.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", c.VilleOffice);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "M");

            if (c.TuToiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "VOUS");


            if (_CurrentPatient.Dentiste != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", _CurrentPatient.Dentiste.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", _CurrentPatient.Dentiste.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _CurrentPatient.Dentiste.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", _CurrentPatient.Dentiste.Mail);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", _CurrentPatient.Dentiste.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", _CurrentPatient.Dentiste.TelFixe);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", _CurrentPatient.Dentiste.TelProfessionnel);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortableDentiste", _CurrentPatient.Dentiste.TelPortable);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", _CurrentPatient.Dentiste.Fax);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _CurrentPatient.Dentiste.Adresse1Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _CurrentPatient.Dentiste.Adresse2Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _CurrentPatient.Dentiste.CodePostalOffice);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _CurrentPatient.Dentiste.VilleOffice);
                if (_CurrentPatient.Dentiste.GenreFeminin)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "F");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");

                if (_CurrentPatient.Dentiste.TuToiement)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "TU");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");

            }
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.VilleOffice);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneFaceSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneProfil", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneProfilSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalDroit", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalGauche", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMandibulaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Man);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMaxilaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalSurplomb", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_SurPlomb);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageDroit", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageGauche", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Gauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageMaxilaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageMandibulaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Man);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioProfil", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioPanoramique", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", txtBxComptRenduCourrier.Text);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);

            
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            BuildInfocomplementaire();

            if (devis == null)
            {
                devis = new Devis();
                devis.IdPatient = CurrentPatient.Id;
            }

            devis.typedevis = (TypeDevis)cbxDevis.SelectedItem;
           // devis.Duree = CurrentPatient.infoscomplementaire.DureeTraitement;

            if (devis.typedevis == null)
            {
                MessageBox.Show("Aucun devis sélectionné","Erreur",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            



            FrmWizardDevis frm = new FrmWizardDevis(_CurrentPatient, devis.typedevis);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                devis.DateProposition = DateTime.Now;


                if (MessageBox.Show("Le devis a t-il été signé ?", "Devis signé ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    devis.DateSignature = DateTime.Now;

                    if (devis.typedevis.DevisInvisalign)
                    {
                        FrmOptionDevisInvisalign frminvi = new FrmOptionDevisInvisalign(devis);
                        if (frminvi.ShowDialog() != DialogResult.OK)
                            return;


                    }
                }

                DevisMgmt.Save(devis);
                InitDisplayForDevis();



                Correspondant Praticien = frm.Praticien;

                foreach (SmallCorrespondant sc in frm.lstCorrespondant)
                {
                    Correspondant c = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(sc.Id);

                    GenerateCourrier(frm.FileName, Praticien, c);

                }


            }
            else
                devis = null;
        }

        private void btnAddObjTrmnt_Click(object sender, EventArgs e)
        {
            FrmObjTrmnt frm = new FrmObjTrmnt();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtbxPlanTraitement.Text = frm.Resultat;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse1 frm1 = new FrmAnalyse1(CurrentPatient, CurrentScreenIdx);
            frm1.InitLoad();
            frm1.ExportToLetter();

            FrmAnalyse2 frm2 = new FrmAnalyse2(CurrentPatient, CurrentScreenIdx);
            frm2.InitLoad();
            frm2.ExportToLetter();

            FrmAnalyse3 frm3 = new FrmAnalyse3(CurrentPatient, CurrentScreenIdx);
            frm3.InitLoad();
            frm3.ExportToLetter();

            FrmAnalyse4 frm4 = new FrmAnalyse4(CurrentPatient, CurrentScreenIdx);
            frm4.InitLoad();
            frm4.ExportToLetter();

            FrmAnalyse5 frm5 = new FrmAnalyse5(CurrentPatient, CurrentScreenIdx);
            frm5.InitLoad();
            frm5.ExportToLetter();

            FrmAnalyse6 frm6 = new FrmAnalyse6(CurrentPatient, CurrentScreenIdx);
            frm6.InitLoad();
            frm6.ExportToLetter();

            FrmAnalyse7 frm7 = new FrmAnalyse7(CurrentPatient, CurrentScreenIdx);
            frm7.InitLoad();
            frm7.ExportToLetter();

            FrmAnalyse8 frm8 = new FrmAnalyse8(CurrentPatient, CurrentScreenIdx);
            frm8.InitLoad();
            frm8.ExportToLetter();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmLastSummary frm = new FrmLastSummary(CurrentPatient, CurrentScreenIdx);

            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
