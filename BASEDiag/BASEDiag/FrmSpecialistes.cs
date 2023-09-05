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
using BasCommon_BO;
using BasCommon_BL;
using System.IO;
namespace BASEDiag
{
    public partial class FrmSpecialistes : Form
    {

        Dictionary<string, string> DefaultCourrier = null;


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
            }
        }

        public FrmSpecialistes(basePatient currentpatient)
        {
            CurrentPatient = currentpatient;
            InitializeComponent();
        }

        private void FrmSpecialistes_Load(object sender, EventArgs e)
        {
            cbxSpecialite1.Items.Add("Dentiste");
            cbxSpecialite1.Items.Add("Chirurgien");
            cbxSpecialite1.Items.Add("Orthophoniste");
            cbxSpecialite1.Items.Add("Ostéopathe");
            cbxSpecialite1.Items.Add("Posturologue");
            cbxSpecialite1.Items.Add("ORL");
            cbxSpecialite1.Items.Add("Podologue");
            cbxSpecialite1.Items.Add("Généraliste");
            cbxSpecialite1.Items.Add("kiné");
            cbxSpecialite1.Items.Add("Orthoptiste");

            cbxSpecialite2.Items.Add("Dentiste");
            cbxSpecialite2.Items.Add("Chirurgien");
            cbxSpecialite2.Items.Add("Orthophoniste");
            cbxSpecialite2.Items.Add("Ostéopathe");
            cbxSpecialite2.Items.Add("Posturologue");
            cbxSpecialite2.Items.Add("ORL");
            cbxSpecialite2.Items.Add("Podologue");
            cbxSpecialite2.Items.Add("Généraliste");
            cbxSpecialite2.Items.Add("kiné");
            cbxSpecialite2.Items.Add("Orthoptiste");

            cbxSpecialite3.Items.Add("Dentiste");
            cbxSpecialite3.Items.Add("Chirurgien");
            cbxSpecialite3.Items.Add("Orthophoniste");
            cbxSpecialite3.Items.Add("Ostéopathe");
            cbxSpecialite3.Items.Add("Posturologue");
            cbxSpecialite3.Items.Add("ORL");
            cbxSpecialite3.Items.Add("Podologue");
            cbxSpecialite3.Items.Add("Généraliste");
            cbxSpecialite3.Items.Add("kiné");
            cbxSpecialite3.Items.Add("Orthoptiste");

            cbxSpecialite4.Items.Add("Dentiste");
            cbxSpecialite4.Items.Add("Chirurgien");
            cbxSpecialite4.Items.Add("Orthophoniste");
            cbxSpecialite4.Items.Add("Ostéopathe");
            cbxSpecialite4.Items.Add("Posturologue");
            cbxSpecialite4.Items.Add("ORL");
            cbxSpecialite4.Items.Add("Podologue");
            cbxSpecialite4.Items.Add("Généraliste");
            cbxSpecialite4.Items.Add("kiné");
            cbxSpecialite4.Items.Add("Orthoptiste");

            cbxSpecialite5.Items.Add("Dentiste");
            cbxSpecialite5.Items.Add("Chirurgien");
            cbxSpecialite5.Items.Add("Orthophoniste");
            cbxSpecialite5.Items.Add("Ostéopathe");
            cbxSpecialite5.Items.Add("Posturologue");
            cbxSpecialite5.Items.Add("ORL");
            cbxSpecialite5.Items.Add("Podologue");
            cbxSpecialite5.Items.Add("Généraliste");
            cbxSpecialite5.Items.Add("kiné");
            cbxSpecialite5.Items.Add("Orthoptiste");


            string courierpardefautfile = Path.GetDirectoryName(Application.ExecutablePath) + "\\CourriersSpecialiste.config";

            if (System.IO.File.Exists(courierpardefautfile))
                DefaultCourrier = parseCSV(courierpardefautfile);
        }


        public Dictionary<string, string> parseCSV(string path)
        {
            Dictionary<string, string> parsedData = new Dictionary<string, string>();

            try
            {
                using (System.IO.StreamReader readFile = new System.IO.StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split('\t');
                        parsedData.Add(row[0], row[1]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return parsedData;
        }


        private void cbxSpecialite1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSpecialite1.SelectedItem == null) return;
            if (DefaultCourrier == null) return;
            btnFindCorrespondant11.Tag = null;
            btnFindCorrespondant11.Text = "";
            if (DefaultCourrier.ContainsKey((string)cbxSpecialite1.SelectedItem))
            {
                FileInfo nfo = new FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm", "");
                btnCourrier2.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier2.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSpecialite2.SelectedItem == null) return;
            if (DefaultCourrier == null) return;
            btnFindCorrespondant12.Tag = null;
            btnFindCorrespondant12.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite2.SelectedItem))
            {
                FileInfo nfo = new FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm", "");
                btnCourrier3.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier3.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSpecialite3.SelectedItem == null) return;
            if (DefaultCourrier == null) return;
            btnFindCorrespondant13.Tag = null;
            btnFindCorrespondant13.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite3.SelectedItem))
            {
                FileInfo nfo = new FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm", "");
                btnCourrier4.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier4.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSpecialite4.SelectedItem == null) return;
            if (DefaultCourrier == null) return;
            btnFindCorrespondant14.Tag = null;
            btnFindCorrespondant14.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite4.SelectedItem))
            {
                FileInfo nfo = new FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm", "");
                btnCourrier5.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier5.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSpecialite5.SelectedItem == null) return;
            if (DefaultCourrier == null) return;
            btnFindCorrespondant15.Tag = null;
            btnFindCorrespondant15.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite5.SelectedItem))
            {
                FileInfo nfo = new FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm", "");
                btnCourrier1.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier1.Text = ss[ss.Length - 1];
            }
        }

        private void btnFindCorrespondant11_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite1.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier2.Tag != null)
                    pnlspe2.Enabled = true;
            }
        }

        private void btnFindCorrespondant12_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite2.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier3.Tag != null)
                    pnlspe3.Enabled = true;
            }
        }

        private void btnFindCorrespondant13_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite3.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier4.Tag != null)
                    pnlspe4.Enabled = true;
            }
        }

        private void btnFindCorrespondant14_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite4.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier5.Tag != null)
                    pnlspe5.Enabled = true;
            }
        }

        private void btnFindCorrespondant15_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite5.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();
            }
        }

        private void btnCourrier2_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(frm.FileName);

                ((Button)sender).Tag = fi;
                string[] ss = Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe2.Enabled = true;
            }
        }

        private void btnCourrier3_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe3.Enabled = true;
            }
        }

        private void btnCourrier4_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe4.Enabled = true;
            }
        }

        private void btnCourrier5_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe5.Enabled = true;

            }
        }

        private void btnCourrier1_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if ((btnCourrier2.Tag == null) || (btnFindCorrespondant11.Tag == null) || (CurrentPatient.infoscomplementaire.PraticienResponsable == null)) return;
            FileInfo file = (FileInfo)btnCourrier2.Tag;
            Utilisateur Praticien = CurrentPatient.infoscomplementaire.PraticienResponsable;
            Correspondant c = ((Correspondant)btnFindCorrespondant11.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint3_Click(object sender, EventArgs e)
        {
            if ((btnCourrier3.Tag == null) || (btnFindCorrespondant12.Tag == null) || (CurrentPatient.infoscomplementaire.PraticienResponsable == null)) return;
            FileInfo file = (FileInfo)btnCourrier3.Tag;
            Utilisateur Praticien = (Utilisateur)CurrentPatient.infoscomplementaire.PraticienResponsable;
            Correspondant c = ((Correspondant)btnFindCorrespondant12.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint4_Click(object sender, EventArgs e)
        {
            if ((btnCourrier4.Tag == null) || (btnFindCorrespondant13.Tag == null) || (CurrentPatient.infoscomplementaire.PraticienResponsable == null)) return;
            FileInfo file = (FileInfo)btnCourrier4.Tag;
            Utilisateur Praticien = (Utilisateur)CurrentPatient.infoscomplementaire.PraticienResponsable;
            Correspondant c = ((Correspondant)btnFindCorrespondant13.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint5_Click(object sender, EventArgs e)
        {
            if ((btnCourrier5.Tag == null) || (btnFindCorrespondant14.Tag == null) || (CurrentPatient.infoscomplementaire.PraticienResponsable == null)) return;
            FileInfo file = (FileInfo)btnCourrier5.Tag;
            Utilisateur Praticien = (Utilisateur)CurrentPatient.infoscomplementaire.PraticienResponsable;
            Correspondant c = ((Correspondant)btnFindCorrespondant14.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if ((btnCourrier1.Tag == null) || (btnFindCorrespondant15.Tag == null) || (CurrentPatient.infoscomplementaire.PraticienResponsable == null)) return;
            FileInfo file = (FileInfo)btnCourrier1.Tag;
            Utilisateur Praticien = (Utilisateur)CurrentPatient.infoscomplementaire.PraticienResponsable;
            Correspondant c = ((Correspondant)btnFindCorrespondant15.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            btnPrint1_Click(sender, e);
            btnPrint2_Click(sender, e);
            btnPrint3_Click(sender, e);
            btnPrint4_Click(sender, e);
            btnPrint5_Click(sender, e);
        }


        private void GenerateCourrier(string file, Utilisateur Praticien, Correspondant c, bool DirectPrint)
        {

            Correspondant praticien = MgmtCorrespondants.getCorrespondant(Praticien.Id);

            AddCourrierAttributs(praticien, c);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionSpecialistes);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);

        }


        private void AddCourrierAttributs(Correspondant Praticien, Correspondant c)
        {

            if (CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPatient);
            if ((c != null) && (c.contacts == null))
                MgmtCorrespondants.FillContacts(c);


            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", CurrentPatient.Genre == basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", CurrentPatient.Civilite);

            if (CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", CurrentPatient.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", CurrentPatient.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", CurrentPatient.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", CurrentPatient.MainAdresse.adresse.Ville);
            }
            if (CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", CurrentPatient.DateNaissance.Date.ToString());
			   BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", CurrentPatient.NextRDV !=null? CurrentPatient.NextRDV.StartDate.ToString() : "");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", CurrentPatient.DateSoldePatient.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", EcheancesMgmt.GetSolde(CurrentPatient) == null ? "" : EcheancesMgmt.GetSolde(CurrentPatient).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", CurrentPatient.LastRDV !=null ? CurrentPatient.LastRDV.StartDate.ToString() : "");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_CORRESPONDANT", c.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreCorrespondant", c.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomCorrespondant", c.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomCorrespondant", c.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailCorrespondant", c.MainMail == null ? "" : c.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionCorrespondant", c.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeCorrespondant", c.MainTel == null ? "" : c.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProCorrespondant", c.MainTel == null ? "" : c.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxCorrespondant", c.MainFax == null ? "" : c.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", c.MainAdresse==null?"":c.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", c.MainAdresse == null ? "" : c.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", c.MainAdresse == null ? "" : c.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", c.MainAdresse == null ? "" : c.MainAdresse.adresse.Ville);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "M");

            if (c.TuToiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "VOUS");


            if (CurrentPatient.Dentiste != null)
            {

                if (_CurrentPatient.Dentiste.correspondant == null)
                    _CurrentPatient.Dentiste.correspondant = MgmtCorrespondants.getCorrespondant(_CurrentPatient.Dentiste.IdCorrespondance);
                
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DENTISTE", c.Id.ToString());

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", CurrentPatient.Dentiste.correspondant.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", CurrentPatient.Dentiste.correspondant.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", CurrentPatient.Dentiste.correspondant.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", CurrentPatient.Dentiste.correspondant.MainMail == null ? "" : CurrentPatient.Dentiste.correspondant.MainMail.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", CurrentPatient.Dentiste.correspondant.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", CurrentPatient.Dentiste.correspondant.MainFax == null ? "" : CurrentPatient.Dentiste.correspondant.MainFax.Value);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Ville);
                if (CurrentPatient.Dentiste.correspondant.GenreFeminin)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "F");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");

                if (CurrentPatient.Dentiste.correspondant.TuToiement)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "TU");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");

            }

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail==null?"":Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel==null?"":Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel==null?"":Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax==null?"":Praticien.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse.adresse.Ville);
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


        }



        private void SaveSpecialistes()
        {
            CurrentPatient.PersonnesAContacter.Clear();

            if (btnFindCorrespondant15.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant15.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                CurrentPatient.PersonnesAContacter.Add(lc);
            }
            if (btnFindCorrespondant11.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant11.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                CurrentPatient.PersonnesAContacter.Add(lc);
            }

            if (btnFindCorrespondant12.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant12.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                CurrentPatient.PersonnesAContacter.Add(lc);
            }

            if (btnFindCorrespondant13.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant13.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                CurrentPatient.PersonnesAContacter.Add(lc);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSpecialistes();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbxSpecialite1_Load(object sender, EventArgs e)
        {

        }

    }
}
