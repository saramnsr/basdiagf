using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BO;
using BasCommon_BL;
using BaseCommonControls;
using System.IO;
using Microsoft.Win32;
namespace BASEDiagAdulte.Ctrls
{
    public partial class Ctrl9a99ans : UserControl
    {
        TemplateActePG actegestion = TemplateApctePGMgmt.getTemplatesActeGestion("ORTHP");


        

        bool ConsentementIsPrinted = false;
        bool DevisIsPrinted = false;

        private FrmLastSummary _owner;
        public FrmLastSummary owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }


        private List<Devis> _Currentdevis;
        public List<Devis> Currentdevis
        {
            get
            {
                return _Currentdevis;
            }
            set
            {
                _Currentdevis = value;
            }
        }

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




        public Ctrl9a99ans(basePatient pat, FrmLastSummary own)
        {
            InitializeComponent();    
            CurrentPatient = pat;
            owner = own;


            BasePrinterSetting ps = PrinterSettingsMgmt.getPrintSettingsByName(PrinterSettingsMgmt.ImpressionPatient);
            lblInfosRapport.Text = "Mail Patient : " + ((CurrentPatient.MainMail == null) ? "" : CurrentPatient.MainMail.Value);
            if ((ps != null) && (ps.settings!=null))
                lblInfosRapport.Text += "\nImprimante courante : " + ps.settings.PrinterName;

          


            btnMail.Enabled = CurrentPatient.MainMail != null;


            if (CurrentPatient.propositions == null)
                CurrentPatient.propositions = PropositionMgmt.getPropositions(CurrentPatient);

            //propositions = PropositionMgmt.getPropositions(CurrentPatient);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateImpressionsPatient(true);
        }


        private void GenerateImpressionsPatient(bool DirectPrint)
        {
            Correspondant pr = MgmtCorrespondants.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
            AddCourrierAttributs(pr);

            string CourriersPatient = System.Configuration.ConfigurationManager.AppSettings["CourriersPatient"];

            if (CourriersPatient == null)
            {
                MessageBox.Show("Aucun courrier Patient parametré !\n cle:CourriersPatient dans .config");
                return;
            }

            string[] courriers = CourriersPatient.Split('\n');

            foreach (string s in courriers)
            {
                string courrier = templateFolder + s;
                BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionPatient);


                if (!DirectPrint)
                    BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(courrier.Trim());
                else

                    BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(courrier.Trim());
            }


        }

        private void GenerateMailPatient()
        {

            FrmEditMail frm = new FrmEditMail("Envois des mails patient");
            frm.txtbxAdress.Text = CurrentPatient.MainMail == null ? "" : CurrentPatient.MainMail.Value;
            frm.txtbxSubject.Text = "Bilan Orthodontique de " + CurrentPatient.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                Correspondant pr = MgmtCorrespondants.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                AddCourrierAttributs(pr);

                string CourriersPatient = System.Configuration.ConfigurationManager.AppSettings["CourriersPatient"];

                if (CourriersPatient == "")
                {
                    MessageBox.Show("Aucun courrier Patient parametré !\n cle:CourriersPatient dans .config");
                    return;
                }

                string[] courriers = CourriersPatient.Split('\n');

                foreach (string s in courriers)
                {
                    string courrier = templateFolder + s;
                    BASEDiag_BL.OLEAccess.BASLetter.MailFrom(courrier.Trim(), frm.txtbxSubject.Text, frm.txtbxBody.Text, frm.txtbxAdress.Text);
                }

            }


        }


        private void AddCourrierAttributs(Correspondant Praticien)
        {

            if (CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPatient);


            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageSupVal", (owner.pbxFace.EtageSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoyVal", (owner.pbxFace.EtageMoy * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfVal", (owner.pbxFace.EtageInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoy2Val", (owner.pbxFace.EtageMoy2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInf2Val", (owner.pbxFace.EtageInf2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfSupVal", (owner.pbxFace.EtageInfSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfInfVal", (owner.pbxFace.EtageInfInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationLevreInfVal", (owner.pbxFace.DeviationLevreInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationMentonVal", (owner.pbxFace.DeviationMenton * 100).ToString("0.0"));

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EspaceDentaireBuccal", (owner.pbxSourire.EspaceDentaireBuccal * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireDroit", (owner.pbxSourire.IncisiveMolaireDroit * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireGauche", (owner.pbxSourire.IncisiveMolaireGauche * 100).ToString("0.0"));

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FMA", owner.pbxRadio.FMA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNA", owner.pbxRadio.SNA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNB", owner.pbxRadio.SNB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ANB", owner.pbxRadio.ANB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IF", owner.pbxRadio.IF.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IM", owner.pbxRadio.IM.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("I2F", owner.pbxRadio.I2F.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittal", ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensVertical", ResumeCliniqueMgmt.resumeCl.SensVertical.ToString());



            float ratio = (800f / owner.pbxSourire.Width);
            Rectangle rect = new Rectangle(0, 0, 800, (int)(owner.pbxSourire.Height * ratio));
            owner.pbxSourire.zoomRadio = owner.pbxSourire.zoomRadio * ratio;
            owner.pbxSourire.Center(rect);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            owner.pbxSourire.PaintOn(g, rect, true);
            string f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseSourire", f);
            
            ratio = (800f / owner.pbxFace.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxFace.Height * ratio));
            owner.pbxFace.zoomRadio = owner.pbxFace.zoomRadio * ratio;
            owner.pbxFace.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxFace.PaintOn(g, rect, true);
            f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFace", f);

            ratio = (800f / owner.pbxRadio.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxRadio.Height * ratio));
            owner.pbxRadio.zoomRadio = owner.pbxRadio.zoomRadio * ratio;
            owner.pbxRadio.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxRadio.PaintOn(g, rect, true);
            f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseCephalo", f);


            owner.pbxOccFace.zoomAuto();
            owner.pbxOccDroit.zoomAuto();
            owner.pbxRadio.zoomAuto();
            owner.pbxSourire.zoomAuto();
            owner.pbxFace.zoomAuto();
            owner.pbxPano.zoomAuto();
            owner.pbxOccGauche.zoomAuto();

            owner.pbxOccFace.Center();
            owner.pbxOccDroit.Center();
            owner.pbxRadio.Center();
            owner.pbxSourire.Center();
            owner.pbxFace.Center();
            owner.pbxPano.Center();
            owner.pbxOccGauche.Center();

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            if (CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
            }
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			   BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail == null ? "" : Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax == null ? "" : Praticien.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Ville);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comment == null ? "" : comment.comment);


        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            GenerateMailPatient();
        }

        private void btnPrintDEP_Click(object sender, EventArgs e)
        {


            List<Devis> lstdevis = new List<Devis>();
            List<Proposition> lstpropositions = new List<Proposition>();

            foreach (Devis d in Currentdevis)
            {
                if (d.DateAcceptation == null)
                    lstdevis.Add(d);

                if (d.propositions == null)
                    d.propositions = PropositionMgmt.getPropositions(d);
                foreach (Proposition p in d.propositions)
                    if (p.Etat == Proposition.EtatProposition.Soumis)
                            lstpropositions.Add(p);
            }




            if (lstpropositions.Count <= 0)
            {
                MessageBox.Show("Aucune proposition sélectionnée !");
                return;
            }

            FrmDEP frm = new FrmDEP(CurrentPatient);
          

            string cotation = "TO90";

            List<Semestre> semdelaDEP = new List<Semestre>();
            foreach (Proposition p in lstpropositions)
                if (p.Etat == Proposition.EtatProposition.Soumis)
                foreach (Traitement t in p.traitements)
                {
                    cotation = t.semestres[0].traitementSecu.Code.Code + t.semestres[0].traitementSecu.Coeff.ToString();                    
                    if (t.semestres[0].traitementSecu.Code.Code != "HN")
                        semdelaDEP.Add(t.semestres[0]);
                }



            if (semdelaDEP.Count == 0)
            {
                MessageBox.Show("Aucun semestre ne peut contenir de DEP\nSemestres en HN");
                return;
            }


            if (ResumeCliniqueMgmt.resumeCl.IdModelEntente > 0)
            {
                EntentePrealable ep = new EntentePrealable();
                ep.patient = CurrentPatient;

                MgmtDemandeEntente.FillDiagEntente(ResumeCliniqueMgmt.resumeCl.IdModelEntente, ref ep, CurrentPatient.Id);


                frm.EnableModele = false;
                frm.EnableDiag = false;

                if (ep.IdDiag > 0)
                    MgmtDemandeEntente.FillDiagWithoutModele(ep.IdDiag, ref ep);
                
                

                 frm.entente = ep;
            }
            else
            {

                frm.entente = BASEDiag_BL.DemandeEntenteMgmt.CreateEntenteFromResume(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl);
                frm.EnableModele = true;
                frm.EnableDiag = true;
                
            }
           // frm.CotationDesActes = cotation;

            if (frm.ShowDialog() == DialogResult.OK)
            {

                if (frm.DEPSaved)
                {
                    BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.IdModelEntente = frm.entente.IdModele;
                    BASEDiag_BL.DemandeEntenteMgmt.UpdateIdModeleEntente(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Id, BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.IdModelEntente);
                }
                

                if (frm.entente.IdModele != -1)
                    foreach (Semestre s in semdelaDEP)
                    {
                        SemestreMgmt.AssocierDEP(s, frm.entente);
                    }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
        }



        private void InitDevisDisplay()
        {
            Currentdevis = MgmtDevis.getDevis(CurrentPatient);


            if (Currentdevis.Count == 0)
            {
                lblDevis.Text = "Aucun Devis";
                return;
            }
            else
            {

                DateTime? lastacceptation = null;
                int Enattente = 0;
                foreach (Devis p in Currentdevis)
                {

                    if ((lastacceptation == null) || (p.DateAcceptation > lastacceptation))
                        lastacceptation = p.DateAcceptation;
                    if (lastacceptation == null)
                        lblDevis.Text = "";
                    else
                        lblDevis.Text = "Dernier Devis accepté le " + lastacceptation.Value.ToString("dd MMM yyyy");


                    if (p.DateAcceptation == null) Enattente++;
                }


                if (Enattente > 0)
                    lblDevis.Text += (Enattente.ToString() + " devis en attente d'acceptation");



            }
        }

        private void InitDisplay()
        {

            Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();
            List<Utilisateur> lst = UtilisateursMgt.getUtilisateurInFauteuil(IamTheFauteuil, DateTime.Now);

            foreach (Utilisateur u in lst)
            {
                if (u.Fonction == "Praticien")
                    cbxPratResp.SelectedTag = u;
                else
                    cbxAssResp.SelectedTag = u;
            }

            if ((CurrentPatient.infoscomplementaire!=null) && (CurrentPatient.infoscomplementaire.PraticienResponsable!=null))
                cbxPratResp.SelectedTag = CurrentPatient.infoscomplementaire.PraticienResponsable;

            if ((CurrentPatient.infoscomplementaire!=null) && (CurrentPatient.infoscomplementaire.AssistanteResponsable != null))
                cbxAssResp.SelectedTag = CurrentPatient.infoscomplementaire.AssistanteResponsable;

            if (CurrentPatient.propositions.Count > 0)
            {
                InitDevisDisplay();

                if (CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut != null)
                {

                    DateTime dte = DateTime.Now;

                    if ((CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut == null) ||
                        (CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut < dtpDebuTraitement.MinDate))
                        dte = DateTime.Now;
                    else
                        dte = CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut;

                    dtpDebuTraitement.Value = dte < DateTime.Now ? DateTime.Now : dte;
                }
                
                    
            }





            if ((CurrentPatient.infoscomplementaire!=null) && (CurrentPatient.infoscomplementaire.NbSemestresEntame != -1))
            {
                cbxSemEntames.SelectedIndices[0] = CurrentPatient.infoscomplementaire.NbSemestresEntame;
                cbxSemEntames.Enabled = false;
            }
            lblDEP.Text = "";

            EntentePrealable ep = new EntentePrealable();
            ep.patient = CurrentPatient;

            /*
            if (DemandeEntenteMgmt.FillFirstDiagEntente(ep))
                lblDEP.Text = "La Demande d'entente à déja été imprimé\n le " + ep.DateImpression.ToString();
            */


            

        }

        List<TabPage> lsttp = new List<TabPage>();
        private void Ctrl9a99ans_Load(object sender, EventArgs e)
        {

           
            foreach (TabPage tp in tabControl1.TabPages)
                lsttp.Add(tp);

            tabControl1.TabPages.Clear();
        tabControl1.TabPages.Add(lsttp[0]);

            cbxSemEntames.Items.Add(0);
            cbxSemEntames.Items.Add(1);
            cbxSemEntames.Items.Add(2);
            cbxSemEntames.Items.Add(3);
            cbxSemEntames.Items.Add(4);
            cbxSemEntames.Items.Add(5);
            cbxSemEntames.Items.Add(6);

            cbxSemEntames.SelectedIndices.Add(0);


            dtpDebuTraitement.Value = DateTime.Now.AddDays(15);


            

            InitDisplay();
        }

        private void lstBxPersonneAContacter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmSpecialistes frm = new FrmSpecialistes(_CurrentPatient);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                baseMgmtPatient.setPersonnesAContacter(_CurrentPatient);

                RefreshListePersonneAContacter();

            }
        }

        private void RefreshListePersonneAContacter()
        {
            lstBxPersonneAContacter.Items.Clear();
            foreach (LienCorrespondant lc in _CurrentPatient.PersonnesAContacter)
            {
                lstBxPersonneAContacter.Items.Add(lc);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant prat = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                foreach (baseSmallPersonne sc in frm.lstCorrespondant)
                {
                    Correspondant c = MgmtCorrespondants.getCorrespondant(sc.Id);
                    GenerateCourrier(frm.FileName, prat, c, false);
                }
            }
        }


        private void GenerateCourrier(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {
            AddCourrierAttributs(Praticien, c);


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionCompteRendu);

            if (DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }

        private void AddCourrierAttributs(Correspondant Praticien, Correspondant c)
        {


            if (CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPatient);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre==basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            if (CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
            }else
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", "");
            }
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", CurrentPatient.NextRDV != null ? _CurrentPatient.NextRDV.StartDate.ToString() : "");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _CurrentPatient.DateSoldePatient.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _CurrentPatient.Solde!=null? _CurrentPatient.Solde.Value.ToString("0.0"):"");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", CurrentPatient.LastRDV != null ? _CurrentPatient.LastRDV.StartDate.ToString() : "");

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


            if (_CurrentPatient.Dentiste != null)
            {
                if (_CurrentPatient.Dentiste.correspondant == null)
                    _CurrentPatient.Dentiste.correspondant = MgmtCorrespondants.getCorrespondant(_CurrentPatient.Dentiste.IdCorrespondance);
                if (_CurrentPatient.Dentiste.correspondant.contacts == null)
                    MgmtCorrespondants.FillContacts(_CurrentPatient.Dentiste.correspondant);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DENTISTE", c.Id.ToString());
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", _CurrentPatient.Dentiste.correspondant.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", _CurrentPatient.Dentiste.correspondant.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _CurrentPatient.Dentiste.correspondant.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", _CurrentPatient.Dentiste.correspondant.MainMail == null ? "" : _CurrentPatient.Dentiste.correspondant.MainMail.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", _CurrentPatient.Dentiste.correspondant.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", _CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : _CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", _CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : _CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", _CurrentPatient.Dentiste.correspondant.MainFax == null ? "" : _CurrentPatient.Dentiste.correspondant.MainFax.Value);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Ville);
                if (_CurrentPatient.Dentiste.correspondant.GenreFeminin)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "F");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");

                if (_CurrentPatient.Dentiste.correspondant.TuToiement)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "TU");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");

            }
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail == null ? "" : Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax == null ? "" : Praticien.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Ville);
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



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", BASEDiag_BL.ResumeCliniqueMgmt.GenerateCompteRenduClinique());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Objectifs", BASEDiag_BL.ResumeCliniqueMgmt.GenerateObjectifs());

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comment == null ? "" : comment.comment);
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);

            if (CurrentPatient.AgeNbYears<=16)
                frm.FileName =templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduEnfant"];
            else
                frm.FileName = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduAdulte"];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant prat = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                foreach (baseSmallPersonne sc in frm.lstCorrespondant)
                {
                    Correspondant c = MgmtCorrespondants.getCorrespondant(sc.Id);
                    GenerateCourrier(frm.FileName, prat, c, false);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void BtnNext1_Click(object sender, EventArgs e)
        {
            
        }


        private bool SaveInfoComplementaires()
        {
            if (CurrentPatient.infoscomplementaire == null)
                CurrentPatient.infoscomplementaire = new InfoPatientComplementaire();

            CurrentPatient.infoscomplementaire.IdPatient = CurrentPatient.Id;
            CurrentPatient.infoscomplementaire.PraticienResponsable = cbxPratResp.SelectedValue;
            CurrentPatient.infoscomplementaire.AssistanteResponsable = cbxAssResp.SelectedValue;

            if (CurrentPatient.infoscomplementaire.PraticienResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner un praticien responsable");
                return false;
            }

            if (CurrentPatient.infoscomplementaire.AssistanteResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner une assistante responsable");
                return false;
            }


            if (dtpDebuTraitement.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La date de début de traitement ne peut pas être passée");
                return false;
            }

            if (cbxSemEntames.SelectedIndices[0] == -1)
            {
                CurrentPatient.infoscomplementaire.NbSemestresEntame = 0;
            }
            else
            {

                if (cbxSemEntames.SelectedIndices[0] == 7)
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = -1;
                else
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = cbxSemEntames.SelectedIndices[0];
            }


            CurrentPatient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;

            baseMgmtPatient.setinfocomplementaire(CurrentPatient.infoscomplementaire);
            return true;
        }


        
        public static void AddCourrierAttributsDevis(Correspondant Praticien,
                                                        Devis dev,
                                                        basePatient _CurrentPatient)
        {


            if (_CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(_CurrentPatient);

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
            TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DEVIS", dev.Id);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbProps", dev.propositions.Count);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbOptions", dev.actesHorstraitement.Count);

            int Optionnum = 1;
            foreach (ActePGPropose acte in dev.actesHorstraitement)
            {

                if ((acte.template == null) && (acte.IdTemplateActePG >= 0))
                    acte.template = TemplateApctePGMgmt.getCodeSecu(acte.IdTemplateActePG);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Montant", acte.Qte * acte.Montant);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Qte", acte.Qte);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Libelle", acte.Libelle);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartSecu", acte.template.Coeff * acte.template.Code.Valeur);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartMutuelle", (acte.Qte * acte.Montant) - (acte.template.Coeff * acte.template.Code.Valeur));
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_LibSecu", acte.template.DisplayCodeNVal);
                Optionnum++;
            }


            int propnum = 1;
            foreach (Proposition prop in dev.propositions)
            {
                if (prop.Etat > Proposition.EtatProposition.Soumis) continue;


                int i = 0;



                foreach (Traitement t in prop.traitements)
                {
                    double tariftotal = TraitementMgmt.getTotal(t);
                    int nbMois = 0;
                    double RmbMutuelleParSemestre = 0;
                    double RmbSecuParSemestre = 0;
                    string LibSecuParSemestre = "";

                    foreach (Semestre s in t.semestres)
                    {
                        nbMois += s.traitementSecu.NBMois.Value;
                    }

                    double secu = t.semestres[0].traitementSecu.Code.Valeur * t.semestres[0].traitementSecu.Coeff;

                    RmbSecuParSemestre += secu;
                    RmbMutuelleParSemestre += t.semestres[0].Montant_Honoraire - secu;
                    if (LibSecuParSemestre != "") LibSecuParSemestre += "+";
                    LibSecuParSemestre += "(" + t.semestres[0].traitementSecu.DisplayCodeNVal + ")";


                    double totalParMois = tariftotal / nbMois;



                    PropositionObjectForLetters ob = new PropositionObjectForLetters();
                    ob.TarifParMois = totalParMois;
                    ob.Honoraires = t.semestres[0].Montant_Honoraire;
                    ob.NbMois = nbMois;
                    ob.PartSecu = RmbSecuParSemestre;
                    ob.PartMutuelle = RmbMutuelleParSemestre;
                    ob.LibSecu = LibSecuParSemestre;
                    ob.CodeTraitement = t.CodeTraitement;

                    //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Propositions", ob);


                    if (!double.IsInfinity(totalParMois)) BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifParMois", totalParMois.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Honoraires", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbMois", nbMois.ToString());
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartSecu", RmbSecuParSemestre.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartMutuelle", RmbMutuelleParSemestre.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_LibSecu", LibSecuParSemestre);
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_CodeTraitement", t.CodeTraitement.Trim());



                    i++;
                }

                /*
                double tariftotalContention = 0;
                int nbMoisContention = 0;

                foreach (Traitement t in prop.traitements)
                {

                    if (t.Phase == BasCommon_BO.Traitement.EnumPhase.Contention)
                    {
                        tariftotalContention += TraitementMgmt.getTotal(t);
                        foreach (Semestre s in t.semestres)
                            nbMoisContention += s.traitementSecu.NBMois.Value;


                    }
                }
                if (tariftotalContention > 0)
                {
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_TarifParMois", (tariftotalContention / nbMoisContention).ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_NbMois", nbMoisContention.ToString());
                }

                propnum++;


                */


                /*

                if (devis != "") devis += "\n\n";
                devis += prop.libelle + " : " + PropositionMgmt.GetTotal(prop).ToString("C2");

                foreach (Traitement t in prop.traitements)
                {
                    if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthopedique) || (t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthodontique))
                    {
                        double totalsansremise = TraitementMgmt.getTotalSansRemise(t);
                        double totalremise = TraitementMgmt.getTotal(t);
                        double partSecu = TraitementMgmt.GetPartSecu(t);

                        devis += "\n\t" + t.Libelle;
                        devis += " sur " + t.semestres.Count.ToString() + " semestre(s)";
                        devis += "\n\t\tTarif : " + (totalsansremise).ToString("C2");
                        if (totalsansremise != totalremise) devis += "\n\t\tTarif remisé: " + (totalremise).ToString("0.00");
                        if (partSecu > 0) devis += "\n\t\tRemboursement Secu: " + partSecu.ToString("0.00");
                    }
                    if (t.Phase == BasCommon_BO.Traitement.EnumPhase.Contention)
                    {
                        double totalsansremise = TraitementMgmt.getTotalSansRemise(t);
                        double totalremise = TraitementMgmt.getTotal(t);
                        double partSecu = TraitementMgmt.GetPartSecu(t);

                        int nbmois = 0;
                        foreach (Semestre s in t.semestres)
                            nbmois += s.traitementSecu.NBMois.Value;

                        devis += "\n\t" + t.Libelle;
                        devis += " pendant " + nbmois.ToString() + " mois";
                        devis += "\n\t\tTarif : " + (totalsansremise).ToString("0.00");
                        if (totalsansremise != totalremise) devis += "\n\t\tTarif remisé: " + (totalremise).ToString("0.00");
                        if (partSecu > 0) devis += "\n\t\tRemboursement Secu: " + partSecu.ToString("0.00");
                    }

                }
                 * */

                propnum++;
            }


            // BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Devis", devis);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre==basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);

            if (_CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.Adresse1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.Adresse2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.Ville);
            }
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.Tel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.Tel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);




            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.Ville);

            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




        }





        public static void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, Devis d, basePatient patient)
        {


            AddCourrierAttributsDevis(Praticien, d, patient);
            BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);


        }

        private void GenerateAndPrintConsentement(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {


            AddCourrierAttributsConsentement(Praticien);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionConsentement);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);

        }

        
       private void AddCourrierAttributsConsentement(Correspondant Praticien)
        {


            if (_CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(_CurrentPatient);


            List<string> lstRisques = new List<string>();
            foreach (Proposition p in _CurrentPatient.propositions)
            {
                foreach (string r in PropositionMgmt.GetRisques(p))
                {
                    if (!lstRisques.Contains(r))
                        lstRisques.Add(r);
                }
            }

            string risques = lstRisques.Count == 0 ? "" : lstRisques.Aggregate((i, j) => i + "\n" + j);

            string objectifs = "";

            foreach (CommonObjectif co in CurrentPatient.SelectedObjectifs)
            {
                if (objectifs != "") objectifs += "\n";
                objectifs += co.ToString();

            }



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("objectifs", objectifs);



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Risques", risques);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            if (CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
            }
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail == null ? "" : Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax==null?"":Praticien.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.CodePostal );
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Ville);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comment == null ? "" : comment.comment);

        }


        private void BtnNext_Click(object sender, EventArgs e)
        {

            Next();
        }


        private bool beforeTabChange(TabPage selectedpage)
        {
            if (tabControl1.SelectedTab == tabPrpositions)
            {

                if (!ConsentementIsPrinted)
                {

                    ImpressionConsentement();


                }


            }

            if (tabControl1.SelectedTab == tabInfoComp)
            {
                if (cbxPratResp.SelectedTag == null)
                {
                    MessageBox.Show("Veuillez sélectionner un Praticien responsable!");
                    return false;
                }

                if (cbxAssResp.SelectedTag == null)
                {
                    MessageBox.Show("Veuillez sélectionner une Assistante responsable!");

                    return false;
                }

                if (!SaveInfoComplementaires())
                    return false;
            }

            if (tabControl1.SelectedTab == tabSpecialistes)
            {
                BASEDiag_BL.OLEAccess.BasePractice.SetPatientCourantById(CurrentPatient.Id);
                BASEDiag_BL.OLEAccess.BasePractice.Activate();
                owner.Close();
            }
            return true;
        }

        private void Next()
        {


            if (!(beforeTabChange(tabControl1.TabPages[0]))) return;

            int nexttab = lsttp.IndexOf(tabControl1.SelectedTab) + 1;
            if (nexttab < lsttp.Count)
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(lsttp[nexttab]);
            }


            AfterChangeTab(tabControl1.TabPages[0]);
        }

        private void AfterChangeTab(TabPage selectedTabpage)
        {
            if (selectedTabpage == tabPrpositions)
            {
                Currentdevis = MgmtDevis.getDevis(CurrentPatient);

                Devis d = CreateDevisInt(CurrentPatient.infoscomplementaire, CurrentPatient, CurrentPatient.propositions);
                if (d != null)
                    Currentdevis.Add(d);
                InitDevisDisplay();
            }
        }

        private void BtnPGOrthalis_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (BasCommon_BL.CommonCalls.FrmContainer != null)
            {
                BasCommon_BL.CommonCalls.FrmContainer.Invoke(BasCommon_BL.CommonCalls.NouvelleDemandeInStandBy, new object[] { CurrentPatient.Id });
            }
            else
            {
                BASEDiag_BL.OLEAccess.BASELabo.NouvelleDemandeInStandBy(CurrentPatient.Id);
            }

           
        }

        private void tabDuree_Click(object sender, EventArgs e)
        {

        }

        private void lstBxPersonneAContacter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPrpositions_Click(object sender, EventArgs e)
        {

        }

        private void btnAddProp_Click(object sender, EventArgs e)
        {
           

        }

        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = CabinetMgmt.Cabinet.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        private static string _templateFolder = "";
        public static string templateFolder
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TEMPLATE_FOLDER" + prefix];
            }
            set
            {
                _templateFolder = "_" + value;
            }


        }
        public static Devis CreateDevisInt(InfoPatientComplementaire infocomplementaire, basePatient CurrentPatient, List<Proposition> CurrentPropositions)
        {
            FrmWizardPropositions frm = new FrmWizardPropositions(infocomplementaire.NbSemestresEntame, CurrentPatient);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string folder = "";
                string file = "";

                if (frm.tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Aucun) return null;
                switch (frm.tpetrmnt)
                {
                    case BasCommon_BO.Devis.enumtypePropositon.Aucun: return null;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthopedie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthodontie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedontie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Adulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Adulte"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Pediatrie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Sucette"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont1: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention1"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont2: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention2"]; break;

                }

                bool cancontinue = true;
                try
                {
                    string[] ss = Directory.GetFiles(folder);



                    if (ss.Length == 1)
                        file = ss[0];
                    else
                    {
                        FrmWizardCourrierForSummary frmletter = new FrmWizardCourrierForSummary(folder);
                        if (frmletter.ShowDialog() == DialogResult.OK)
                        {
                            file = frmletter.FileName;
                        }
                        else
                            cancontinue = false;

                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Courrier des devis introuvable!\n Le devis sera créé sans impression:\n\n" + ex.Message);
                }


                if (cancontinue)
                {

                    List<Proposition> lstProp = new List<Proposition>();

                    foreach (Proposition p in frm.value)
                    {
                        PropositionMgmt.InsertFullProposition(p);
                        lstProp.Add(p);
                    }

                    Devis d = MgmtDevis.CreateDevis(lstProp, frm.ActesMateriel, frm.tpetrmnt,frm.DateDeDebut,frm.DateDeFin);
                    string devis = file;

                    //if (devis != "")
                    //{
                    //    Correspondant c = MgmtCorrespondants.getCorrespondant(CurrentPatient.Id);
                    //    Correspondant praticien = MgmtCorrespondants.getCorrespondant(infocomplementaire.PraticienResponsable.Id);
                    //    GenerateAndPrintDevis(devis, praticien, c, d, CurrentPatient);

                    //}


                    if (devis != "")
                    {
                          

                        foreach (Proposition p in d.propositions)
                            if ((p.echeancestemp == null) || p.echeancestemp.Count == 0)
                                p.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(p);

                        BaseCommonControls.CommonActions.PrintDevis(d, CurrentPatient,file);

                    }


                    return d;



                }
            }

            return null;
        }


        private void GererDevis()
        {
            if (CurrentPatient == null) return;
            Currentdevis = MgmtDevis.getDevis(CurrentPatient);

            bool DevisEnAttente = false;

            foreach (Devis d in Currentdevis)
                if (d.DateAcceptation == null) 
                    DevisEnAttente = true;


            if (DevisEnAttente)
            {


                FrmDevisManager frm = new FrmDevisManager(CurrentPatient, CurrentPatient.infoscomplementaire, CurrentPatient.propositions, Currentdevis);
                frm.ShowDialog();

            }
            else
            {
                if (CurrentPatient.infoscomplementaire.PraticienResponsable == null)
                {
                    MessageBox.Show("Aucun praticien responsable!", "Praticien responsable absent", MessageBoxButtons.OK);

                }
                else
                {


                    FrmDevisManager.CreateDevisInt(CurrentPatient);

                }
            }
        }


       

      
        private void button1_Click_3(object sender, EventArgs e)
        {
            
        }

        private void ImpressionConsentement()
        {
            ConsentementIsPrinted = true;
            string consentement = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierConsentement"];


            Correspondant c = MgmtCorrespondants.getCorrespondant(_CurrentPatient.Id);
            Correspondant praticien = MgmtCorrespondants.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
            GenerateAndPrintConsentement(consentement, praticien, c, false);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
        }

        private void tabInfoComp_Click(object sender, EventArgs e)
        {

        }

        private void lblAffectation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string stat = "";
            stat = EntiteJuridiqueMgmt.GetNbPatientParEntity();

            lblAffectation.Text = stat;
            lblAffectation.TextAlign = ContentAlignment.MiddleLeft;
            this.Cursor = Cursors.Default;
        }

        private void cbxAssResp_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblDevis_Click(object sender, EventArgs e)
        {

            
            FrmDevisManager frm = new FrmDevisManager(CurrentPatient,CurrentPatient.infoscomplementaire,CurrentPatient.propositions,Currentdevis);

            frm.ShowDialog();
            InitDevisDisplay();
        }

        private void cbxSemEntames_OnSelectionChange(object sender, EventArgs e)
        {
            
        }

        private void tabNbSemestreEnt_Click(object sender, EventArgs e)
        {

        }

        private void cbxSemEntames_OnSelectionChange_1(object sender, EventArgs e)
        {

        }

        private void tabPratAssResponsable_Click(object sender, EventArgs e)
        {

        }

        
       


    }
}
