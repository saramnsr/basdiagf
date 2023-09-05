using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls;
using FrmContainer_BO;
using FrmContainer_BL;

namespace WindowsFormsApplication1
{
    public partial class FrmAdminGestion : Form
    {
        string courrier = "";



        

        private Relance.ModeRelance _statusrelance = Relance.ModeRelance.Aucun;
        public Relance.ModeRelance statusrelance
        {
            get
            {
                return _statusrelance;
            }
            set
            {
                _statusrelance = value;
            }
        }

        private List<PatientARelancer> _LstPatToRel = new List<PatientARelancer>();
        public List<PatientARelancer> LstPatToRel
        {
            get
            {
                return _LstPatToRel;
            }
            set
            {
                _LstPatToRel = value;
            }
        }

        private InfoPatientComplementaire _nfocompl;
        public InfoPatientComplementaire nfocompl
        {
            get
            {
                return _nfocompl;
            }
            set
            {
                _nfocompl = value;
            }
        }


        private DataGridViewCellStyle _NotYetStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle NotYetStyle
        {
            get
            {
                return _NotYetStyle;
            }
            set
            {
                _NotYetStyle = value;
            }
        }

        
        private DataGridViewCellStyle _NormalStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle NormalStyle
        {
            get
            {
                return _NormalStyle;
            }
            set
            {
                _NormalStyle = value;
            }
        }


        public FrmAdminGestion(InfoPatientComplementaire compl)
        {
            InitializeComponent();

            NotYetStyle.ForeColor = Color.Gray;
            NormalStyle.ForeColor = Color.Black;
            nfocompl = compl;
            
        }





        private void InitARemettreEnBanque()
        {

            dgvARemettreEnBanque.Rows.Clear();

            List<PaiementReel> lstPaiementReel = MgmtEncaissement.GetPaiementReelsARemettreEnBanque(dtpRemisDte1.Value.Date, dtpRemisDte2.Value.Date, (PaiementReel.TypeEncaissement)Enum.Parse(typeof(PaiementReel.TypeEncaissement), ((string)cbxTypeEncaissement.SelectedItem)), cbxentityARemettre.SelectedItem is EntiteJuridique?((EntiteJuridique)cbxentityARemettre.SelectedItem):null);


            foreach (PaiementReel ec in lstPaiementReel)
            {
                

                    List<object> lstCell = new List<object>();


                    lstCell.Add(ec.payeur);
                    lstCell.Add(ec.Patients);

                    lstCell.Add(ec.typeencaissement.ToString());

                    lstCell.Add(ec.NumCheque);


                    lstCell.Add(ec.BanqueEmetrice == null ? "" : ec.BanqueEmetrice.Libelle);

                    lstCell.Add(ec.DateEcheance);
                    lstCell.Add(ec.Montant);

                    if (ec.EntiteJuridique != null) lstCell.Add(ec.EntiteJuridique.Nom); else lstCell.Add("");

                    int idx = dgvARemettreEnBanque.Rows.Add(lstCell.ToArray());
                    dgvARemettreEnBanque.Rows[idx].Tag = ec;

                    if (ec.DateEcheance > DateTime.Now.Date)
                        dgvARemettreEnBanque.Rows[idx].DefaultCellStyle = NotYetStyle;
                    else
                        dgvARemettreEnBanque.Rows[idx].DefaultCellStyle = NormalStyle;
                

            }


            double total = 0;
            foreach (DataGridViewRow itm in dgvARemettreEnBanque.Rows)
            {
                total += ((PaiementReel)itm.Tag).Montant;
            }

            lblTotalARemettre.Text = total.ToString("C2");

        }

        private void InitRemisEnBanque()
        {

            dgvARemisEnBanque.Rows.Clear();

            List<PaiementReel> lstPaiementReel = MgmtEncaissement.GetPaiementReelsRemisEnBanque(dtpRemisBqe1.Value.Date, dtpRemisBqe2.Value.Date.AddDays(1), (PaiementReel.TypeEncaissement)Enum.Parse(typeof(PaiementReel.TypeEncaissement), ((string)cbxtpeRemisBque.SelectedItem)),((EntiteJuridique)cbxEntityRemis.SelectedItem));


            double totalerm = 0;

            foreach (PaiementReel ec in lstPaiementReel)
            {

                totalerm += ec.Montant;
                List<object> lstCell = new List<object>();

                if ((ec.IdPayeur > 0))
                {
                    baseSmallPersonne sp = MgmtCorrespondants.getSmallCorrespondant(ec.IdPayeur);
                    lstCell.Add(sp==null?"":sp.ToString());
                }else
                    lstCell.Add(ec.payeur);

                lstCell.Add(ec.Patients);

                lstCell.Add(ec.typeencaissement.ToString());

                lstCell.Add(ec.NumCheque);


                lstCell.Add(ec.BanqueEmetrice == null ? "" : ec.BanqueEmetrice.Libelle);

                lstCell.Add(ec.DateEcheance);
                lstCell.Add(Math.Round(ec.Montant,2));

                if (ec.EntiteJuridique != null) lstCell.Add(ec.EntiteJuridique.Nom); else lstCell.Add("");

                lstCell.Add(ec.BanqueDeRemise);

                int idx = dgvARemisEnBanque.Rows.Add(lstCell.ToArray());
                dgvARemisEnBanque.Rows[idx].Tag = ec;

                dgvARemisEnBanque.Rows[idx].DefaultCellStyle = NormalStyle;


            }
            lblTotalRemisEnBanque.Text = totalerm.ToString("C2");
        }

        private void InitEncaissements()
        {
            if (cbxTpeEnc.SelectedItem == null) return;
            dgvEncaissements.Rows.Clear();

            List<Encaissement> lstEncaissements = MgmtEncaissement.GetEncaissements(true, dtpEnc1.Value.Date, dtpEnc2.Value.Date.AddDays(1), (PaiementReel.TypeEncaissement)Enum.Parse(typeof(PaiementReel.TypeEncaissement), ((string)cbxTpeEnc.SelectedItem)), cbxEntityEncaissement.SelectedItem is EntiteJuridique?((EntiteJuridique)cbxEntityEncaissement.SelectedItem):null);

            double TotalEnc = 0;
            foreach (Encaissement ec in lstEncaissements)
            {

                TotalEnc += ec.MontantEncaisse;
                List<object> lstCell = new List<object>();

                if ((ec.paiementreel == null) && (ec.IdPaiementReel > 0))
                    ec.paiementreel = MgmtEncaissement.GetPaiementReel(ec.IdPaiementReel);

                if ((ec.paiementreel.payeur == null))
                {
                    baseSmallPersonne sp = MgmtCorrespondants.getSmallCorrespondant(ec.paiementreel.IdPayeur);
                    lstCell.Add(sp.ToString());
                }
                else
                    lstCell.Add(ec.paiementreel.payeur);

                baseSmallPersonne pat = null;
                if (ec.patient == null)
                    pat = MgmtCorrespondants.getSmallCorrespondant(ec.IdPatient);
                lstCell.Add(pat);

                lstCell.Add(ec.paiementreel.typeencaissement.ToString());

                lstCell.Add(ec.paiementreel.NumCheque);


                lstCell.Add(ec.paiementreel.BanqueEmetrice == null ? "" : ec.paiementreel.BanqueEmetrice.Libelle);

                lstCell.Add(ec.paiementreel.DateEncaissement);
                lstCell.Add(ec.MontantEncaisse);

                if (ec.paiementreel.EntiteJuridique != null) lstCell.Add(ec.paiementreel.EntiteJuridique.Nom); else lstCell.Add("");

                lstCell.Add(ec.paiementreel.BanqueDeRemise);

                int idx = dgvEncaissements.Rows.Add(lstCell.ToArray());
                dgvEncaissements.Rows[idx].Tag = ec;

                dgvEncaissements.Rows[idx].DefaultCellStyle = NormalStyle;


            }
            lblTotalEncaissements.Text = TotalEnc.ToString("C2");
        }


        private void InitEncaissementTiers()
        {
            dgvEncTier.Rows.Clear();

            List<Echeance> lstEcheance = EcheancesMgmt.GetEcheanceAEncaisserParUnTiers();


            foreach (Echeance ec in lstEcheance)
            {


                List<object> lstCell = new List<object>();

                switch (ec.payeur)
                {
                    case Echeance.typepayeur.Mutuelle:
                        lstCell.Add(ec.mutuelle);
                        break;
                    case Echeance.typepayeur.Banque:
                        lstCell.Add("Banque");
                        break;
                    case Echeance.typepayeur.Secu:
                        lstCell.Add("Secu");
                        break;
            }

                lstCell.Add(ec.Montant);


                baseSmallPersonne sp = MgmtCorrespondants.getSmallCorrespondant(ec.IdPatient);


                lstCell.Add(sp==null?"":sp.ToString());

                
                if ((ec.acte == null) && (ec.IdActe > 0))
                    ec.acte = ActesPGMgmt.GetActesPG(ec.IdActe);


               // lstCell.Add(ec.acte.Libelle);
                

                InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ec.IdPatient);

                if (nfo.PraticienResponsable != null)
                    lstCell.Add(nfo.PraticienResponsable.EntiteJuridique.Nom);
                else
                    lstCell.Add("Aucun praticien responsable!!");

                if ((ec.acte != null)&&(ec.acte.NbMois !=null))
                {
                    lstCell.Add(ec.acte.DateExecution.AddDays(ec.acte.NbJours.Value).AddMonths(ec.acte.NbMois.Value));
                }else
                    lstCell.Add(ec.DateEcheance);

                int idx = dgvEncTier.Rows.Add(lstCell.ToArray());
                dgvEncTier.Rows[idx].Tag = ec;


            }
        }

        private void InitAPrelever()
        {



            dgvPrelevmnt.Rows.Clear();

            //List<Echeance> lstEcheance = EcheancesMgmt.GetEcheanceAPrelever(dtpAPrelever1.Value.Date,dtpAPrelever2.Value.Date.AddDays(1));
            List<Echeance> lstEcheance = new List<Echeance >();

            foreach (Echeance ec in lstEcheance)
            {


                List<object> lstCell = new List<object>();


                lstCell.Add(ec.patient.ToString());

                lstCell.Add(ec.Montant);

                lstCell.Add(ec.DateEcheance);

                InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ec.IdPatient);

                if (nfo.PraticienResponsable != null)
                    lstCell.Add(nfo.PraticienResponsable.EntiteJuridique.Nom);
                else
                    lstCell.Add("Aucun praticien responsable!!");

                int idx = dgvPrelevmnt.Rows.Add(lstCell.ToArray());
                dgvPrelevmnt.Rows[idx].Tag = ec;
                               

            }

        }


        private void tabRemisEnBanque_Click(object sender, EventArgs e)
        {

        }

        private void FrmAdminGestion_Load(object sender, EventArgs e)
        {


            MyPrintDocument.PrintPage += new PrintPageEventHandler(MyPrintDocument_PrintPage);

            string[] ss = Enum.GetNames(typeof(PaiementReel.TypeEncaissement));

            foreach (string s in ss)
            {
                cbxTypeEncaissement.Items.Add(s);
                cbxTpeEnc.Items.Add(s);
                cbxtpeRemisBque.Items.Add(s);
            }

            cbxEntityEncaissement.Items.Add("");
            cbxentityARemettre.Items.Add("");
            cbxEntityRemis.Items.Add("");

            foreach (EntiteJuridique ej in EntiteJuridiqueMgmt.entites)
            {
                cbxEntityEncaissement.Items.Add(ej);
                cbxentityARemettre.Items.Add(ej);
                cbxEntityRemis.Items.Add(ej);
            }


            cbxTypeEncaissement.SelectedIndex = 0;
            cbxTpeEnc.SelectedIndex = 0;
            cbxtpeRemisBque.SelectedIndex = 0;

            dtpRemisDte1.Value = DateTime.Now;
            dtpRemisDte2.Value = DateTime.Now;
            dtpRemisBqe1.Value = DateTime.Now;
            dtpRemisBqe2.Value = DateTime.Now;
            dtpAPrelever1.Value = DateTime.Now;
            dtpAPrelever2.Value = DateTime.Now;

            InitARemettreEnBanque();
            InitAPrelever();
        }

      

        void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true; 
        }

        private void dgvARemettreEnBanque_MouseClick(object sender, MouseEventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow itm in dgvARemettreEnBanque.SelectedRows)
            {
                total += ((PaiementReel)itm.Tag).Montant;
            }

            lblSomme.Text = total.ToString("C2");
        }

        private void btnRemise_Click(object sender, EventArgs e)
        {

            FrmChoixBanque frm = new FrmChoixBanque("Choix de la banque de destination","Banque destinatrice");

            if (frm.ShowDialog() == DialogResult.OK)
            {

                double total = 0;
                foreach (DataGridViewRow itm in dgvARemettreEnBanque.SelectedRows)
                {

                    MgmtEncaissement.RemiseEnBanque(((PaiementReel)itm.Tag), frm.Value);

                    total += ((PaiementReel)itm.Tag).Montant;
                }

                InitARemettreEnBanque();
                MessageBox.Show(total.ToString("C2") + " ont été remis en banque");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errors = "";
            double total = 0;

            FrmChoixBanque frm = new FrmChoixBanque("Choix de la banque de destination", "Banque destinatrice");

            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewRow itm in dgvPrelevmnt.SelectedRows)
                {

                    Echeance ech = ((Echeance)itm.Tag);


                    MgmtEncaissement.Prelever(ech, frm.Value);


                    total += ech.Montant;

                }
            }
            if (errors != "")
                MessageBox.Show(errors, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            InitAPrelever();
            MessageBox.Show(total.ToString("C2") + " ont été réglés");
        }

        

        private void tabPrelevement_Click(object sender, EventArgs e)
        {

        }

        private void dgvPrelevmnt_MouseClick(object sender, MouseEventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow itm in dgvPrelevmnt.SelectedRows)
            {
                total += ((Echeance)itm.Tag).Montant;
            }

            lblSommeAPrelever.Text = total.ToString("C2");
        }

        private void dgvARemettreEnBanque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void RefreshRelances()
        {
            RefreshRelances(null,null,Relance.ModeRelance.Aucun);
        }

        private void RefreshRelances(DateTime? dte1, DateTime? dte2,Relance.ModeRelance relanceVoulu)
        {

            
            dgvRelances.Rows.Clear();

            foreach (PatientARelancer pr in LstPatToRel)
            {
                if ((dte1 == null) || (pr.DueDepuis <= dte1))
                {
                    if ((dte2 == null) || (pr.DueDepuis > dte2))
                    {

                        if ((relanceVoulu == Relance.ModeRelance.Aucun) || (pr.CurrentStatus < relanceVoulu))
                        {
                            object[] objs = new object[]{
                        pr.patient,
                        pr.ResponsableFi,
                        pr.SommesDue,
                        pr.DueDepuis,
                        (DateTime.Now - pr.DueDepuis).TotalDays
                        };


                            int idx = dgvRelances.Rows.Add(objs);
                            dgvRelances.Rows[idx].Tag = pr;
                        }
                    }
                }
            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabRelance)
            {
                DataRefresh();
            }

            if (tabControl1.SelectedTab == tabEncaissementsTier)
            {
                InitEncaissementTiers();
            }
        }

        private void DataRefresh()
        {
            this.Cursor = Cursors.WaitCursor;
            
            LstPatToRel = ListeAssistanteMgmt.getPatientsARelancer();
            RefreshRelances();

            this.Cursor = Cursors.Default;
            
        }

        private void tabRelance_Click(object sender, EventArgs e)
        {

        }


        private void AddCourrierAttributs(Double Somme, DateTime depuis, Correspondant ResponsableFi, Correspondant Praticien, basePatient patient)
        {
            OLEAccess.BASLetter.AddAttribut("Somme", Somme.ToString("C2"));
            OLEAccess.BASLetter.AddAttribut("DatePremiereEch", depuis.ToString());


            if (ResponsableFi != null)
            {
                OLEAccess.BASLetter.AddAttribut("NomResponsableFi", ResponsableFi.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomResponsableFi", ResponsableFi.Prenom);
                OLEAccess.BASLetter.AddAttribut("GenreResponsableFi", ResponsableFi.GenreFeminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitreResponsableFi", ResponsableFi.Titre);
                OLEAccess.BASLetter.AddAttribut("Adresse1ResponsableFi", ResponsableFi.Adresse1);
                OLEAccess.BASLetter.AddAttribut("Adresse2ResponsableFi", ResponsableFi.Adresse2);
                OLEAccess.BASLetter.AddAttribut("CodePostalResponsableFi", ResponsableFi.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VilleResponsableFi", ResponsableFi.Ville);
                if (patient.Tutoiement)
                    OLEAccess.BASLetter.AddAttribut("TutoiementResponsableFi", "TU");
                else
                    OLEAccess.BASLetter.AddAttribut("TutoiementResponsableFi", "VOUS");

            }
            else
            {
                OLEAccess.BASLetter.AddAttribut("NomResponsableFi", patient.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomResponsableFi", patient.Prenom);
                OLEAccess.BASLetter.AddAttribut("GenreResponsableFi", patient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitreResponsableFi", patient.Civilite);

                
                if (patient.MainAdresse != null)
                {
                    OLEAccess.BASLetter.AddAttribut("Adresse1ResponsableFi", patient.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("Adresse2ResponsableFi", patient.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalResponsableFi", patient.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VilleResponsableFi", patient.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("PaysResponsableFi", patient.MainAdresse.adresse.Pays);
                }


                if (patient.Tutoiement)
                    OLEAccess.BASLetter.AddAttribut("TutoiementResponsableFi", "TU");
                else
                    OLEAccess.BASLetter.AddAttribut("TutoiementResponsableFi", "VOUS");

            }

            OLEAccess.BASLetter.AddAttribut("ID_PATIENT", patient.Id.ToString());
            OLEAccess.BASLetter.AddAttribut("NomPatient", patient.Nom);
            OLEAccess.BASLetter.AddAttribut("PrenomPatient", patient.Prenom);
            OLEAccess.BASLetter.AddAttribut("AgePatient", patient.AgeNbYears.ToString() + " ans");
            OLEAccess.BASLetter.AddAttribut("GenrePatient", patient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
            OLEAccess.BASLetter.AddAttribut("TitrePatient", patient.Civilite);

            if (patient.MainAdresse != null)
            {
                OLEAccess.BASLetter.AddAttribut("Adresse1Patient", patient.MainAdresse.adresse.Adr1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Patient", patient.MainAdresse.adresse.Adr2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPatient", patient.MainAdresse.adresse.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePatient", patient.MainAdresse.adresse.Ville);
                OLEAccess.BASLetter.AddAttribut("PaysPatient", patient.MainAdresse.adresse.Pays);
            }



            if (patient.Tutoiement)
                OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", patient.DateNaissance.Date.ToString());
			 OLEAccess.BASLetter.AddAttribut("NumSecu", patient.NumSecu.ToString());
            OLEAccess.BASLetter.AddAttribut("NumDossierPatient", patient.Dossier.ToString());


            if (Praticien != null)
            {
                OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id.ToString());

                OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
                OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
                OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
                OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
                OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

                OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.Ville);
                if (Praticien.GenreFeminin)
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
                else
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");

            }
        }


        private void btnRElanceCourrier_Click(object sender, EventArgs e)
        {
            if (statusrelance == Relance.ModeRelance.Aucun)
            {
                MessageBox.Show("Veuillez choisir un niveau de relance");
                return;
            }
            foreach (DataGridViewRow dr in dgvRelances.SelectedRows)
            {
                PatientARelancer pr = (PatientARelancer)dr.Tag;

                Correspondant Praticien = ((nfocompl==null) || (nfocompl.PraticienResponsable==null))?null:MgmtCorrespondants.getCorrespondant(nfocompl.PraticienResponsable.Id);
                Correspondant REspFi = MgmtCorrespondants.getCorrespondant(pr.IdResponsableFi);
                basePatient pat = baseMgmtPatient.GetPatient(pr.IdPatient);

                AddCourrierAttributs(pr.SommesDue, pr.DueDepuis, REspFi, Praticien, pat);


                switch (statusrelance)
                {
                    case Relance.ModeRelance.Releve: courrier = System.Configuration.ConfigurationManager.AppSettings["Releve"]; break;
                    case Relance.ModeRelance.Relance1: courrier = System.Configuration.ConfigurationManager.AppSettings["Relance1"]; break;
                    case Relance.ModeRelance.PreContentieux: courrier = System.Configuration.ConfigurationManager.AppSettings["PreContentieux"]; break;
                    case Relance.ModeRelance.PreContentieux2: courrier = System.Configuration.ConfigurationManager.AppSettings["PreContentieux2"]; break;
                    case Relance.ModeRelance.Contentieux: courrier = System.Configuration.ConfigurationManager.AppSettings["Contentieux"]; break;
                }
                pr.CurrentStatus = statusrelance;
                baseMgmtPatient.ChangerStatusRelance(pr.IdPatient, statusrelance);
                OLEAccess.BASLetter.PrintFrom(courrier.Trim());
                RefreshRelances();

            }
        }

        private void BtnReleve_Click(object sender, EventArgs e)
        {
            RefreshRelances(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-70),Relance.ModeRelance.Releve);
            statusrelance = Relance.ModeRelance.Releve;
        }

        private void btnRelance1_Click(object sender, EventArgs e)
        {
            RefreshRelances(DateTime.Now.AddDays(-70), DateTime.Now.AddDays(-180), Relance.ModeRelance.Relance1);
            statusrelance = Relance.ModeRelance.Relance1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshRelances(DateTime.Now.AddDays(-180), DateTime.Now.AddDays(-210), Relance.ModeRelance.PreContentieux);
            statusrelance = Relance.ModeRelance.PreContentieux;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshRelances(DateTime.Now.AddDays(-210), DateTime.Now.AddDays(-365),Relance.ModeRelance.PreContentieux2);
            statusrelance = Relance.ModeRelance.PreContentieux2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshRelances(DateTime.Now.AddDays(-365), null,Relance.ModeRelance.Contentieux);
            statusrelance = Relance.ModeRelance.Contentieux;
        }

        private void cbxTypeEncaissement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            InitARemettreEnBanque();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InitRemisEnBanque();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xls");




            List<PaiementReel> lstPaiementReel = MgmtEncaissement.GetPaiementReelsARemettreEnBanque(dtpRemisDte1.Value, dtpRemisDte2.Value, (PaiementReel.TypeEncaissement)Enum.Parse(typeof(PaiementReel.TypeEncaissement), ((string)cbxTypeEncaissement.SelectedItem)), cbxEntityEncaissement.SelectedItem is EntiteJuridique?((EntiteJuridique)cbxEntityEncaissement.SelectedItem):null);

            List<object[]> lstobjs = new List<object[]>();

            foreach (PaiementReel ec in lstPaiementReel)
            {


                List<object> lstCell = new List<object>();


                lstCell.Add(ec.payeur);
                lstCell.Add(ec.Patients);

                lstCell.Add(ec.typeencaissement.ToString());

                lstCell.Add(ec.NumCheque);


                lstCell.Add(ec.BanqueEmetrice == null ? "" : ec.BanqueEmetrice.Libelle);

                lstCell.Add(ec.DateEncaissement.Date);
                lstCell.Add(ec.DateEcheance);
                lstCell.Add(ec.Montant);

                if (ec.EntiteJuridique != null) lstCell.Add(ec.EntiteJuridique.Nom); else lstCell.Add("");

                lstobjs.Add(lstCell.ToArray());
                
            }


            ExportExcel.AremettreEnBanque(lstobjs,tempFile);

            System.Diagnostics.Process.Start(tempFile);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xls");




            List<PaiementReel> lstPaiementReel = MgmtEncaissement.GetPaiementReelsRemisEnBanque(dtpRemisBqe1.Value, dtpRemisBqe2.Value, (PaiementReel.TypeEncaissement)Enum.Parse(typeof(PaiementReel.TypeEncaissement), ((string)cbxtpeRemisBque.SelectedItem)),(EntiteJuridique)cbxEntityRemis.SelectedItem);

            List<object[]> lstobjs = new List<object[]>();

            foreach (PaiementReel ec in lstPaiementReel)
            {


                List<object> lstCell = new List<object>();


                lstCell.Add(ec.payeur);
                lstCell.Add(ec.Patients);

                lstCell.Add(ec.typeencaissement.ToString());

                lstCell.Add(ec.NumCheque);


                lstCell.Add(ec.BanqueEmetrice == null ? "" : ec.BanqueEmetrice.Libelle);

                lstCell.Add(ec.DateEncaissement.Date);
                lstCell.Add(ec.DateEcheance);
                lstCell.Add(ec.Montant);
                
                if (ec.EntiteJuridique != null) lstCell.Add(ec.EntiteJuridique.Nom); else lstCell.Add("");
                lstCell.Add(ec.BanqueDeRemise==null?"":ec.BanqueDeRemise.Libelle);

                lstobjs.Add(lstCell.ToArray());

            }


            ExportExcel.RemisEnBanque(lstobjs, tempFile);

            System.Diagnostics.Process.Start(tempFile);
        }

        private void modifierLaDateDéchéanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvARemettreEnBanque.SelectedRows.Count < 1) return;

            PaiementReel ec = (((PaiementReel)dgvARemettreEnBanque.SelectedRows[0].Tag));

            if (ec.typeencaissement != PaiementReel.TypeEncaissement.Cheque)
            {
                MessageBox.Show("L'encaissement n'a pas été effectué par cheque");
                return;
            }

            if (ec.DateRemiseEnBanque != null)
            {
                MessageBox.Show("Le chèque à déja été remis en banque");
                return;
            }


            FrmDate frm = new FrmDate("Date de remise prevue", "Date");
            frm.Value = ec.DateEcheance.Value ;

            if (frm.ShowDialog() == DialogResult.OK)
            {

                if (frm.Value < DateTime.Now)
                    MessageBox.Show("La date ne peut pas être passée");
                else
                {
                    ec.DateEcheance = frm.Value;
                    MgmtEncaissement.UpdatePaiementReel(ec);
                    InitARemettreEnBanque();
                }

            }
        }

        private void dtpRemisDte1_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRemisDte2.Value < dtpRemisDte1.Value)
                dtpRemisDte2.Value = dtpRemisDte1.Value;
        }

        private void dtpRemisDte2_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRemisDte2.Value < dtpRemisDte1.Value)
                dtpRemisDte1.Value = dtpRemisDte2.Value;
        }

        private void dtpRemisBqe1_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRemisBqe2.Value < dtpRemisBqe1.Value)
                dtpRemisBqe2.Value = dtpRemisBqe1.Value;
        }

        private void dtpRemisBqe2_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRemisBqe2.Value < dtpRemisBqe1.Value)
                dtpRemisBqe2.Value = dtpRemisBqe1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dtpAPrelever2.Value < dtpAPrelever1.Value)
                dtpAPrelever2.Value = dtpAPrelever1.Value;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InitAPrelever();
        }

        private void dtpAPrelever2_ValueChanged(object sender, EventArgs e)
        {
            if (dtpAPrelever2.Value < dtpAPrelever1.Value)
                dtpAPrelever2.Value = dtpAPrelever1.Value;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InitEncaissementTiers();
        }

        private void dgvEncTier_MouseClick(object sender, MouseEventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow itm in dgvEncTier.SelectedRows)
            {
                total += ((Echeance)itm.Tag).Montant;
            }

            lblSommeEncTier.Text = total.ToString("C2");
        }

        private void btnConfirmRemBanque_Click(object sender, EventArgs e)
        {
            string errors = "";
            double total = 0;

            FrmChoixBanque frm = new FrmChoixBanque("Choix de la banque de destination", "Banque destinatrice");

            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewRow itm in dgvEncTier.SelectedRows)
                {

                    Echeance ech = ((Echeance)itm.Tag);


                    MgmtEncaissement.Virement(ech, frm.Value);


                    total += ech.Montant;

                }
            }

            if (errors != "")
                MessageBox.Show(errors, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            InitEncaissementTiers();
            MessageBox.Show(total.ToString("C2") + " ont été réglés");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xls");




            //List<Echeance> lstEcheance = EcheancesMgmt.GetEcheanceAPrelever(dtpAPrelever1.Value, dtpAPrelever2.Value);
            List<Echeance> lstEcheance = new List<Echeance>();

            List<object[]> lstobjs = new List<object[]>();

            foreach (Echeance ec in lstEcheance)
            {


                List<object> lstCell = new List<object>();


                lstCell.Add(ec.patient.ToString());

                lstCell.Add(ec.Montant);

                lstCell.Add(ec.DateEcheance);

                InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ec.IdPatient);

                if (nfo.PraticienResponsable != null)
                    lstCell.Add(nfo.PraticienResponsable.EntiteJuridique.Nom);
                else
                    lstCell.Add("Aucun praticien responsable!!");

                lstobjs.Add(lstCell.ToArray());

            }


            ExportExcel.APrelever(lstobjs, tempFile);

            System.Diagnostics.Process.Start(tempFile);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xls");


            List<object[]> lstobjs = new List<object[]>();

            List<Echeance> lstEcheance = EcheancesMgmt.GetEcheanceAEncaisserParUnTiers();


            foreach (Echeance ec in lstEcheance)
            {


                List<object> lstCell = new List<object>();

                switch (ec.payeur)
                {
                    case Echeance.typepayeur.Mutuelle:
                        lstCell.Add(ec.mutuelle.ToString());
                        break;
                    case Echeance.typepayeur.Banque:
                        lstCell.Add("Banque");
                        break;
                    case Echeance.typepayeur.Secu:
                        lstCell.Add("Secu");
                        break;
                }

                lstCell.Add(ec.Montant);


                baseSmallPersonne sp = MgmtCorrespondants.getSmallCorrespondant(ec.IdPatient);


                lstCell.Add(sp.ToString());

                /*
                if ((ec.acte == null) && (ec.IdActe > 0))
                    ec.acte = ActesPGMgmt.GetActesPG(ec.IdActe);


                lstCell.Add(ec.acte.Libelle);
                */

                InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ec.IdPatient);

                if (nfo.PraticienResponsable != null)
                    lstCell.Add(nfo.PraticienResponsable.EntiteJuridique.Nom);
                else
                    lstCell.Add("Aucun praticien responsable!!");

                lstobjs.Add(lstCell.ToArray());

            }

            ExportExcel.EncaissementTiers(lstobjs, tempFile);

            System.Diagnostics.Process.Start(tempFile);
        }

        // The PrintDocument to be used for printing.
        PrintDocument MyPrintDocument = new PrintDocument();
        // The class that will do the printing process.
        DataGridViewPrinter MyDataGridViewPrinter;

        private bool SetupThePrinting(DataGridView MyDataGridView, string Title)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = Title;
            MyPrintDocument.PrinterSettings =
                                MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings =
            MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins =
                             new Margins(40, 40, 40, 40);

            MyPrintDocument.DefaultPageSettings.Landscape = true;

            MyDataGridViewPrinter = new DataGridViewPrinter(MyDataGridView,
                MyPrintDocument, true, true, Title, new Font("Tahoma", 18,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            

            return true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(dgvARemettreEnBanque, "A remettre en banque"))
                MyPrintDocument.Print();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(dgvARemisEnBanque,"Remis en banque"))
                MyPrintDocument.Print();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(dgvPrelevmnt, "A prélever"))
                MyPrintDocument.Print();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(dgvEncTier, "Encaissements Tiers en attente"))
                MyPrintDocument.Print();
        }

        private void nonRemisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelRemise_Click(object sender, EventArgs e)
        {
            if (dgvARemisEnBanque.SelectedRows.Count < 1) return;

            int nb = 0;
            foreach (DataGridViewRow dr in dgvARemisEnBanque.SelectedRows)
            {

                PaiementReel ec = (((PaiementReel)dr.Tag));

                if (ec.EstRemisEnBanque == PaiementReel.RemisEnBanque.Non)
                {
                    MessageBox.Show("Ce reglement n'est pas marqué comme 'Remis en banque'\n"+ec.Montant.ToString("C2")+":"+ec.payeur);
                    return;
                }

                

                    ec.DateRemiseEnBanque = null;
                    ec.EstRemisEnBanque = PaiementReel.RemisEnBanque.Non;
                    ec.BanqueDeRemise = null;
                    MgmtEncaissement.UpdatePaiementReel(ec);
                    InitARemettreEnBanque();
                    nb++;
            }

            MessageBox.Show(nb.ToString()+" remise(s) ont été annulée(s)");
            InitRemisEnBanque();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            InitEncaissements();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabEncaissements_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting(dgvEncaissements, "Encaissements"))
                MyPrintDocument.Print();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xls");




            
            List<object[]> lstobjs = new List<object[]>();

            
            foreach (DataGridViewRow dr in dgvEncaissements.Rows)
            {

                Encaissement ec = ((Encaissement)dr.Tag);

                List<object> lstCell = new List<object>();


                if ((ec.paiementreel == null) && (ec.IdPaiementReel > 0))
                    ec.paiementreel = MgmtEncaissement.GetPaiementReel(ec.IdPaiementReel);

                if ((ec.paiementreel.payeur == null))
                {
                    baseSmallPersonne sp = MgmtCorrespondants.getSmallCorrespondant(ec.paiementreel.IdPayeur);
                    lstCell.Add(sp.ToString());
                }
                else
                    lstCell.Add(ec.paiementreel.payeur);

                baseSmallPersonne pat = null;
                if (ec.patient == null)
                    pat = MgmtCorrespondants.getSmallCorrespondant(ec.IdPatient);
                lstCell.Add(pat.ToString());

                lstCell.Add(ec.paiementreel.typeencaissement.ToString());

                lstCell.Add(ec.paiementreel.NumCheque);


                lstCell.Add(ec.paiementreel.BanqueEmetrice == null ? "" : ec.paiementreel.BanqueEmetrice.Libelle);

                lstCell.Add(ec.paiementreel.DateEncaissement);
                lstCell.Add(ec.MontantEncaisse);

                if (ec.paiementreel.EntiteJuridique != null) lstCell.Add(ec.paiementreel.EntiteJuridique.Nom); else lstCell.Add("");

                lstCell.Add(ec.paiementreel.BanqueDeRemise==null?"":ec.paiementreel.BanqueDeRemise.Libelle);

                lstobjs.Add(lstCell.ToArray());

            }


            ExportExcel.Encaissement(lstobjs, tempFile);

            System.Diagnostics.Process.Start(tempFile);
        }

        private void tabRemis_Click(object sender, EventArgs e)
        {

        }
    }
}
