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
    public partial class FrmSpecialiste : Form
    {



        public List<Correspondant> specialistes
        {
            get
            {
                List<Correspondant> lst = new List<Correspondant>();
                if (btnFindCorrespondant1.Tag != null) lst.Add((Correspondant)btnFindCorrespondant1.Tag);
                if (btnFindCorrespondant2.Tag != null) lst.Add((Correspondant)btnFindCorrespondant2.Tag);
                if (btnFindCorrespondant3.Tag != null) lst.Add((Correspondant)btnFindCorrespondant3.Tag);
                if (btnFindCorrespondant4.Tag != null) lst.Add((Correspondant)btnFindCorrespondant4.Tag);
                if (btnFindCorrespondant5.Tag != null) lst.Add((Correspondant)btnFindCorrespondant5.Tag);
                return lst;
            }
        }

        private string _plantraitement;

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
            }
        }

        private Utilisateur _Praticien;


        public FrmSpecialiste(Utilisateur praticien, Patient patient, string plantraitement)
        {
            _Praticien = praticien;
            _CurrentPatient = patient;
            _plantraitement = plantraitement;
            InitializeComponent();
        }

        private void FrmSpecialiste_Load(object sender, EventArgs e)
        {
            tvSpecialite1.Items.Add("Dentiste");
            tvSpecialite1.Items.Add("Chirurgien maxilo/stomato");
            tvSpecialite1.Items.Add("Orthophoniste");
            tvSpecialite1.Items.Add("Ostéopathe");
            tvSpecialite1.Items.Add("Posturologue");
            tvSpecialite1.Items.Add("ORL");
            tvSpecialite1.Items.Add("Podologue");
            tvSpecialite1.Items.Add("Généraliste");
            tvSpecialite1.Items.Add("kiné");
            tvSpecialite1.Items.Add("Orthoptiste");

            tvSpecialite2.Items.Add("Dentiste");
            tvSpecialite2.Items.Add("Chirurgien maxilo/stomato");
            tvSpecialite2.Items.Add("Orthophoniste");
            tvSpecialite2.Items.Add("Ostéopathe");
            tvSpecialite2.Items.Add("Posturologue");
            tvSpecialite2.Items.Add("ORL");
            tvSpecialite2.Items.Add("Podologue");
            tvSpecialite2.Items.Add("Généraliste");
            tvSpecialite2.Items.Add("kiné");
            tvSpecialite2.Items.Add("Orthoptiste");

            tvSpecialite3.Items.Add("Dentiste");
            tvSpecialite3.Items.Add("Chirurgien maxilo/stomato");
            tvSpecialite3.Items.Add("Orthophoniste");
            tvSpecialite3.Items.Add("Ostéopathe");
            tvSpecialite3.Items.Add("Posturologue");
            tvSpecialite3.Items.Add("ORL");
            tvSpecialite3.Items.Add("Podologue");
            tvSpecialite3.Items.Add("Généraliste");
            tvSpecialite3.Items.Add("kiné");
            tvSpecialite3.Items.Add("Orthoptiste");

            tvSpecialite4.Items.Add("Dentiste");
            tvSpecialite4.Items.Add("Chirurgien maxilo/stomato");
            tvSpecialite4.Items.Add("Orthophoniste");
            tvSpecialite4.Items.Add("Ostéopathe");
            tvSpecialite4.Items.Add("Posturologue");
            tvSpecialite4.Items.Add("ORL");
            tvSpecialite4.Items.Add("Podologue");
            tvSpecialite4.Items.Add("Généraliste");
            tvSpecialite4.Items.Add("kiné");
            tvSpecialite4.Items.Add("Orthoptiste");

            tvSpecialite5.Items.Add("Dentiste");
            tvSpecialite5.Items.Add("Chirurgien maxilo/stomato");
            tvSpecialite5.Items.Add("Orthophoniste");
            tvSpecialite5.Items.Add("Ostéopathe");
            tvSpecialite5.Items.Add("Posturologue");
            tvSpecialite5.Items.Add("ORL");
            tvSpecialite5.Items.Add("Podologue");
            tvSpecialite5.Items.Add("Généraliste");
            tvSpecialite5.Items.Add("kiné");
            tvSpecialite5.Items.Add("Orthoptiste");

        }



        private void tvSpecialite1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Enabled = true;
        }

        private void tvSpecialite2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel3.Enabled = true;
        }

        private void tvSpecialite3_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel4.Enabled = true;
        }

        private void tvSpecialite4_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel5.Enabled = true;
        }

        private void btnFindCorrespondant_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant("");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                ((Button)sender).Text = fi.Name;
            }
        }

        private void tvSpecialite1_Load(object sender, EventArgs e)
        {
            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
            {
                if (u.Actif)
                {
                    if (u.Fonction == "Praticien")
                        cbxPraticien.Items.Add(u);
                }
            }

            cbxPraticien.SelectedItem = _Praticien;
        }


        private void GenerateCourrier(string file, Utilisateur Praticien, Correspondant c)
        {
            AddCourrierAttributs(Praticien, c);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionSpecialistes);
            BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }



        private void AddCourrierAttributs(Utilisateur Praticien, Correspondant c)
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
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



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", _plantraitement);



        }


        private void btnPrint1_Click(object sender, EventArgs e)
        {

            if ((btnCourrier1.Tag == null) || (btnFindCorrespondant1.Tag == null) || (cbxPraticien.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier1.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPraticien.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant1.Tag);
            GenerateCourrier(file.FullName, Praticien, c);
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if ((btnCourrier2.Tag == null) || (btnFindCorrespondant2.Tag == null) || (cbxPraticien.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier2.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPraticien.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant2.Tag);
            GenerateCourrier(file.FullName, Praticien, c);
        }

        private void btnPrint3_Click(object sender, EventArgs e)
        {
            if ((btnCourrier3.Tag == null) || (btnFindCorrespondant3.Tag == null) || (cbxPraticien.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier3.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPraticien.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant3.Tag);
            GenerateCourrier(file.FullName, Praticien, c);
        }

        private void btnPrint4_Click(object sender, EventArgs e)
        {
            if ((btnCourrier4.Tag == null) || (btnFindCorrespondant4.Tag == null) || (cbxPraticien.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier4.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPraticien.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant4.Tag);
            GenerateCourrier(file.FullName, Praticien, c);
        }

        private void btnPrint5_Click(object sender, EventArgs e)
        {
            if ((btnCourrier5.Tag == null) || (btnFindCorrespondant5.Tag == null) || (cbxPraticien.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier5.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPraticien.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant5.Tag);
            GenerateCourrier(file.FullName, Praticien, c);
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            btnPrint1_Click(sender, e);
            btnPrint2_Click(sender, e);
            btnPrint3_Click(sender, e);
            btnPrint4_Click(sender, e);
            btnPrint5_Click(sender, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
