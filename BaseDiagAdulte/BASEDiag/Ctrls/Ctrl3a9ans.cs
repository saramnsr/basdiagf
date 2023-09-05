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

namespace BASEDiag.Ctrls
{
    public partial class Ctrl3a9ans : UserControl
    {
        TemplateActePG actegestion = TemplateApctePGMgmt.getTemplatesActeGestion("ORTHP");
                

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


        private Semestre AddSemestre(DateTime DateDebut, int numSemestre, Traitement traitement, TemplateActePG acteGestion, Double MontantDuSemestre)
        {
            Semestre sem = new Semestre();
            sem.NumSemestre = numSemestre;
            sem.NbSurveillance = 0;
            sem.Parent = traitement;

            sem.Montant_Honoraire = MontantDuSemestre;

            sem.traitementSecu = acteGestion;
            sem.CodeSemestre = "S" + numSemestre.ToString();

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


            sem.MontantSurveillance = surv.Valeur;
            sem.traitementSecuSurveillance = surv;




            sem.DateDebut = DateDebut;
            sem.DateFin = DateDebut.AddMonths(6);

            traitement.semestres.Add(sem);

            return sem;
        }
        
        public bool BuildProposition()
        {

            try
            {
                

                Proposition CurrentProposition = new Proposition();

                CurrentProposition.Etat = Proposition.EtatProposition.Soumis;
                CurrentProposition.DateProposition = DateTime.Now;
                CurrentProposition.DateEvenement = DateTime.Now;
                CurrentProposition.patient = CurrentPatient;
                CurrentProposition.libelle = "Orthopédie";

                Traitement traitement = new Traitement();
                traitement.Parent = CurrentProposition;
                traitement.Libelle = "Orthopédie";
                traitement.Phase = TemplateActePG.EnumPhase.Orthopedique;

                CurrentProposition.traitements.Add(traitement);
                
                Double tarifSemestre = actegestion.Valeur;

                
                if (!Double.TryParse(txtbxTarifReel.Text, out tarifSemestre))
                {
                    MessageBox.Show("Tarif Réel invalide!");
                    return false;
                }



                DateTime dte = dtpDebuTraitement.Value;

                Semestre S1 = null;
                Semestre S2 = null;
                Semestre S3 = null;


                S1 = AddSemestre(dte, 1, traitement, actegestion, tarifSemestre); 
                dte = dte.AddMonths(6);

                if (chkbxSem2.Checked || chkbxSem3.Checked)
                {
                    S2 = AddSemestre(dte, 2, traitement, actegestion, tarifSemestre);
                    dte = dte.AddMonths(6);
                }
                if (chkbxSem3.Checked)
                {
                    S3 = AddSemestre(dte, 3, traitement, actegestion, tarifSemestre);
                    dte = dte.AddMonths(6);
                }

                
                if ((rbRCC.Checked) || (rbRCCASI.Checked))
                {
                    Appareil app = AppareilMgmt.getAppareilByLib("RCC");
                    if (app != null)
                    {
                        PoseAppareil papp = new PoseAppareil();
                        papp.appareil = app;
                        papp.Parent = CurrentProposition;
                        if (S1 != null) papp.semestres.Add(S1);
                        if (S2 != null) papp.semestres.Add(S2);
                        if (S3 != null) papp.semestres.Add(S3);

                        CurrentProposition.poseAppareils.Add(papp);
                    }
                }
                if ((rbRCCASI.Checked))
                {
                    Appareil app = AppareilMgmt.getAppareilByLib("ASI");
                    if (app != null)
                    {
                        PoseAppareil papp = new PoseAppareil();
                        papp.appareil = app;
                        if (S1 != null) papp.semestres.Add(S1);
                        if (S2 != null) papp.semestres.Add(S2);
                        if (S3 != null) papp.semestres.Add(S3);

                        CurrentProposition.poseAppareils.Add(papp);
                    }
                }
                if ((rbDISJMASQUE.Checked))
                {
                    Appareil appMasqueDelaire = AppareilMgmt.getAppareilByLib("DEL");
                    if (appMasqueDelaire != null)
                    {
                        PoseAppareil papp = new PoseAppareil();
                        papp.appareil = appMasqueDelaire;
                        if (S1 != null) papp.semestres.Add(S1);
                        if (S2 != null) papp.semestres.Add(S2);
                        if (S3 != null) papp.semestres.Add(S3);

                        CurrentProposition.poseAppareils.Add(papp);
                    }

                    Appareil appDisjoncteur = AppareilMgmt.getAppareilByLib("DJA");
                    if (appDisjoncteur != null)
                    {
                        PoseAppareil papp = new PoseAppareil();
                        papp.appareil = appDisjoncteur;
                        if (S1 != null) papp.semestres.Add(S1);
                        if (S2 != null) papp.semestres.Add(S2);
                        if (S3 != null) papp.semestres.Add(S3);

                        CurrentProposition.poseAppareils.Add(papp);
                    }
                }

              
                    CurrentPatient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;
                    CurrentPatient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;
                
                



                string check = PropositionMgmt.CheckValiditeRemboursement(CurrentProposition, dtpDebuTraitement.Value, cbxSemEntames.SelectedIndex);

                if (check != "")
                {
                    MessageBox.Show(check);
                    return false;
                }

                CurrentPatient.propositions.Clear();
                CurrentPatient.propositions.Add(CurrentProposition);
                return true;

            }
            catch (System.Exception)
            {
                CurrentPatient.propositions.Clear();
                return false;
            }

        }

        public Ctrl3a9ans(Patient pat,FrmLastSummary own)
        {
            InitializeComponent();    
            CurrentPatient = pat;
            owner = own;


            BasePrinterSetting ps = PrinterSettingsMgmt.getPrintSettingsByName(PrinterSettingsMgmt.ImpressionPatient);
            lblInfosRapport.Text = "Mail Patient : " + CurrentPatient.Mail;
            if (ps!=null)
                lblInfosRapport.Text += "\nImprimante courante : " + ps.settings.PrinterName;

            if (ps != null)
                lblInfosCompteRendu.Text = "\nImprimante courante : " + ps.settings.PrinterName;
           


            btnMail.Enabled = CurrentPatient.Mail != "";

            txtbxTarifReel.Text = actegestion.Valeur.ToString();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateImpressionsPatient(true);
        }


        private void GenerateImpressionsPatient(bool DirectPrint)
        {
            Correspondant pr = CorrespondantMgmt.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
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

                BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionPatient);


                if (!DirectPrint)
                    BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(s.Trim());
                else
                {

                    BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(s.Trim());
                }
            }


        }

        private void GenerateMailPatient()
        {

            FrmEditMail frm = new FrmEditMail("Envois des mails patient");
            frm.txtbxAdress.Text = CurrentPatient.Mail;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                Correspondant pr = CorrespondantMgmt.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                AddCourrierAttributs(pr);

                string CourriersPatient = System.Configuration.ConfigurationManager.AppSettings["CourriersPatient"];

                if (CourriersPatient == "")
                {
                    MessageBox.Show("Aucun courrier Patient parametré !\n cle:CourriersPatient dans .config");
                    return;
                }

                string[] courriers = CourriersPatient.Split('\n');

                foreach (string s in courriers)
                    BASEDiag_BL.OLEAccess.BASLetter.MailFrom(s.Trim(), frm.txtbxSubject.Text, frm.txtbxBody.Text, frm.txtbxAdress.Text);


            }


        }


        private void AddCourrierAttributs(Correspondant Praticien)
        {
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
            owner.pbxSourire.PaintOn(g, rect);
            string f = System.IO.Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseSourire", f);
            owner.pbxSourire.zoomAuto();


            ratio = (800f / owner.pbxFace.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxFace.Height * ratio));
            owner.pbxFace.zoomRadio = owner.pbxFace.zoomRadio * ratio;
            owner.pbxFace.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxFace.PaintOn(g, rect);
            f = System.IO.Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFace", f);
            owner.pbxFace.zoomAuto();

            ratio = (800f / owner.pbxRadio.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxRadio.Height * ratio));
            owner.pbxRadio.zoomRadio = owner.pbxRadio.zoomRadio * ratio;
            owner.pbxRadio.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxRadio.PaintOn(g, rect);
            f = System.IO.Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseCephalo", f);
            owner.pbxRadio.zoomAuto();

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
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
			 OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
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
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());


        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            GenerateMailPatient();
        }


        List<Semestre> semdelaDEP = new List<Semestre>();
        EntentePrealable DEPAAssocier = null;

        private void btnPrintDEP_Click(object sender, EventArgs e)
        {
            
                

                


                if (CurrentPatient.propositions.Count<=0)
                {
                    MessageBox.Show("Aucune proposition sélectionnée !");
                    return;
                }

                FrmDEP frm = new FrmDEP(CurrentPatient);

                foreach (PoseAppareil pa in CurrentPatient.propositions[0].poseAppareils)
                    if (pa.appareil != null)
                        frm.PlanDeTraitement += " " + pa.appareil.InfoDEP + "\n";


                
                string cotation = "TO90";
                
                foreach (Traitement t in CurrentPatient.propositions[0].traitements)
                {
                    cotation = t.semestres[0].traitementSecu.Code.Code + t.semestres[0].traitementSecu.Coeff.ToString();
                    semdelaDEP.Add(t.semestres[0]);
                }
           

                frm.CotationDesActes = cotation;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DEPAAssocier = frm.ententeprealable;
                    
                }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void InitDisplay()
        {

            Fauteuil IamTheFauteuil = BASEDiag_BL.Fauteuilsmgt.GetWhoIam();
            List<Utilisateur> lst = UtilisateursMgt.getUtilisateurInFauteuil(IamTheFauteuil, DateTime.Now);

            foreach (Utilisateur u in lst)
            {
                if (u.Fonction == "Praticien")
                    cbxPratResp.SelectedItem = u;
                else
                    cbxAssResp.SelectedItem = u;
            }

            if (CurrentPatient.infoscomplementaire.PraticienResponsable!=null)
                 cbxPratResp.SelectedItem = CurrentPatient.infoscomplementaire.PraticienResponsable;

            if (CurrentPatient.infoscomplementaire.AssistanteResponsable != null)
                cbxAssResp.SelectedItem = CurrentPatient.infoscomplementaire.AssistanteResponsable;

            if (CurrentPatient.propositions.Count > 0)
            {
                foreach (Traitement t in CurrentPatient.propositions[0].traitements)
                {
                    foreach (Semestre s in t.semestres)
                    {
                        ((CheckBox)pnlSemestre.Controls[s.NumSemestre - 1]).Checked = true;
                    }
                }

                try
                {
                    txtbxTarifReel.Text = CurrentPatient.propositions[0].traitements[0].semestres[0].Montant_Honoraire.ToString();
                }
                catch (System.Exception)
                {
                }

                if (CurrentPatient.propositions[0].poseAppareils.Count > 0)
                {
                    rbRCC.Checked = ((CurrentPatient.propositions[0].poseAppareils[0].appareil.Code == "RCC") && (CurrentPatient.propositions[0].poseAppareils.Count == 1));
                    rbRCCASI.Checked = ((CurrentPatient.propositions[0].poseAppareils.Count == 2) && (CurrentPatient.propositions[0].poseAppareils[0].appareil.Code == "RCC") && (CurrentPatient.propositions[0].poseAppareils[1].appareil.Code == "ASI"));
                    rbDISJMASQUE.Checked = ((CurrentPatient.propositions[0].poseAppareils.Count == 2) && (CurrentPatient.propositions[0].poseAppareils[0].appareil.Code == "MD") && (CurrentPatient.propositions[0].poseAppareils[1].appareil.Code == "DISJ"));
                }

                try
                {
                    if (CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut != null)
                        dtpDebuTraitement.Value = CurrentPatient.propositions[0].traitements[0].semestres[0].DateDebut.Value;
                }
                catch (System.Exception) { }
                    
            }


            if (CurrentPatient.infoscomplementaire.IdPatient !=-1)
            {
                cbxSemEntames.SelectedIndex = CurrentPatient.infoscomplementaire.NbSemestresEntame;
                cbxSemEntames.Enabled = false;
            }

            lblDEP.Text = "";

            EntentePrealable ep = new EntentePrealable();
            ep.patient = CurrentPatient;

            if (DemandeEntenteMgmt.FillFirstDiagEntente(ep))
                lblDEP.Text = "La Demande d'entente à déja été imprimé le " + ep.DateImpression.ToString();



            

        }

        List<TabPage> lsttp = new List<TabPage>();
        private void Ctrl3a9ans_Load(object sender, EventArgs e)
        {

            foreach (TabPage tp in tabControl1.TabPages)
                lsttp.Add(tp);

            tabControl1.TabPages.Clear();
        tabControl1.TabPages.Add(lsttp[0]);

            cbxSemEntames.Items.Add("Aucun");
            cbxSemEntames.Items.Add("1");
            cbxSemEntames.Items.Add("2");
            cbxSemEntames.Items.Add("3");
            cbxSemEntames.Items.Add("4");
            cbxSemEntames.Items.Add("5");
            cbxSemEntames.Items.Add("6");

            cbxSemEntames.SelectedIndex = 0;


            dtpDebuTraitement.Value = DateTime.Now.AddDays(15);


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


            InitDisplay();
        }

        private void lstBxPersonneAContacter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmSpecialistes frm = new FrmSpecialistes(_CurrentPatient);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CorrespondantMgmt.setPersonnesAContacter(_CurrentPatient);

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
                Correspondant prat = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                foreach (SmallCorrespondant sc in frm.lstCorrespondant)
                {
                    Correspondant c = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(sc.Id);
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

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_CORRESPONDANT", c.Id.ToString());
            
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
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DENTISTE", c.Id.ToString());
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
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



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", BASEDiag_BL.ResumeCliniqueMgmt.GenerateCompteRenduClinique());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Objectifs", BASEDiag_BL.ResumeCliniqueMgmt.GenerateObjectifs());

            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);
            frm.FileName = System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduEnfant"];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant prat = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                foreach (SmallCorrespondant sc in frm.lstCorrespondant)
                {
                    Correspondant c = BASEDiag_BL.CorrespondantMgmt.getCorrespondant(sc.Id);
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
            CurrentPatient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;
            CurrentPatient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;

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

            if (cbxSemEntames.SelectedIndex == -1)
            {
                CurrentPatient.infoscomplementaire.NbSemestresEntame = 0;
            }
            else
            {

                if (cbxSemEntames.SelectedIndex == 7)
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = -1;
                else
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = cbxSemEntames.SelectedIndex;
            }


            CurrentPatient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;

            PatientMgmt.setinfocomplementaire(CurrentPatient);
            return true;
        }


        private void PrintDevisEtConsentement()
        {
            if (SaveInfoComplementaires())
            {
                string chk = "";
                foreach (Proposition p in CurrentPatient.propositions)
                {
                    if (chk != "") chk += "\n";

                    string tmpchk = PropositionMgmt.CheckValiditeRemboursement(p, CurrentPatient.infoscomplementaire.DateDebutTraitement.Value, CurrentPatient.infoscomplementaire.NbSemestresEntame);
                    chk += tmpchk != "" ? p.libelle + " : " + tmpchk : "";
                }
             
                if (chk != "")
                {
                    if (MessageBox.Show(chk+"\n Souhaitez-vous continuer ?","Attention",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                    {
                        GenerateNPrintDEvisConsentement(false);
                    }
                }else
                    GenerateNPrintDEvisConsentement(false);

            }
        }

        private void GenerateNPrintDEvisConsentement(bool printDirect)
        {
            string devis = System.Configuration.ConfigurationManager.AppSettings["CourrierDevis"];
            string consentement = System.Configuration.ConfigurationManager.AppSettings["CourrierConsentement"];

            if (!System.IO.File.Exists(devis))
            {
                FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
                frm.Text = "Choix du devis";
                if (frm.ShowDialog() != DialogResult.OK) return; else devis = frm.FileName;
            }

            Correspondant c = CorrespondantMgmt.getCorrespondant(_CurrentPatient.Id);
            Correspondant praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintDevis(devis, praticien, c, printDirect);


            foreach (Proposition p in _CurrentPatient.propositions)
                p.Etat = Proposition.EtatProposition.Soumis;


            if (!System.IO.File.Exists(consentement))
            {
                FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
                frm.Text = "Choix du consentement";
                if (frm.ShowDialog() != DialogResult.OK) return; else consentement = frm.FileName;
            }

            c = CorrespondantMgmt.getCorrespondant(_CurrentPatient.Id);
            praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintConsentement(consentement, praticien, c, printDirect);
        }


        private void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {


            AddCourrierAttributsDevis(Praticien);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionDevis);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
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

        private void AddCourrierAttributsDevis(Correspondant Praticien)
        {
            string devis = "";

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
            TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");

            int propnum = 1;
            foreach (Proposition prop in _CurrentPatient.propositions)
            {

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbPhases", prop.traitements.Count.ToString());

                int i = 0;

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_NbMois", "");


                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_NbMois", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_NbMois", "");


                foreach (Traitement t in prop.traitements)
                {
                    double tariftotal = TraitementMgmt.getTotal(t);
                    int nbMois = 0;
                    foreach (Semestre s in t.semestres)
                        nbMois += s.traitementSecu.NBMois;


                    double totalParMois = tariftotal / nbMois;

                    if ((t.Phase == TemplateActePG.EnumPhase.Orthopedique) || (t.Phase == TemplateActePG.EnumPhase.Pédiatrique))
                    {

                        int idxapp = 1;
                        foreach (PoseAppareil pa in prop.poseAppareils)
                        {
                            if (((pa.semestres[0].NumSemestre <= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[0].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[0].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre))
                                )
                            {
                                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_Appareil" + idxapp.ToString(), pa.appareil.Libelle);
                                idxapp++;
                            }
                        }
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1", t.Phase.ToString());
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_TarifParMois", totalParMois.ToString("0.00"));
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_NbMois", nbMois.ToString());

                    }
                    if (t.Phase == TemplateActePG.EnumPhase.Orthodontique)
                    {
                        int idxapp = 1;
                        foreach (PoseAppareil pa in prop.poseAppareils)
                        {
                            if (((pa.semestres[0].NumSemestre <= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[0].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[0].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre))
                                )
                            {
                                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_Appareil" + idxapp.ToString(), pa.appareil.Libelle);
                                idxapp++;
                            }
                        }

                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2", t.Phase.ToString());
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_TarifParMois", totalParMois.ToString("0.00"));
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_NbMois", nbMois.ToString());

                    }
                    i++;
                }


                double tariftotalContention = 0;
                int nbMoisContention = 0;

                foreach (Traitement t in prop.traitements)
                {

                    if (t.Phase == TemplateActePG.EnumPhase.Contention)
                    {
                        tariftotalContention += TraitementMgmt.getTotal(t);
                        foreach (Semestre s in t.semestres)
                            nbMoisContention += s.traitementSecu.NBMois;


                    }
                }
                if (tariftotalContention > 0)
                {
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_TarifParMois", (tariftotalContention / nbMoisContention).ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_NbMois", nbMoisContention.ToString());
                }

                propnum++;







                if (devis != "") devis += "\n\n";
                devis += prop.libelle + " : " + PropositionMgmt.GetTotal(prop).ToString("C2");

                foreach (Traitement t in prop.traitements)
                {
                    if ((t.Phase == TemplateActePG.EnumPhase.Orthopedique) || (t.Phase == TemplateActePG.EnumPhase.Orthodontique))
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
                    if (t.Phase == TemplateActePG.EnumPhase.Contention)
                    {
                        double totalsansremise = TraitementMgmt.getTotalSansRemise(t);
                        double totalremise = TraitementMgmt.getTotal(t);
                        double partSecu = TraitementMgmt.GetPartSecu(t);

                        int nbmois = 0;
                        foreach (Semestre s in t.semestres)
                            nbmois += s.traitementSecu.NBMois;

                        devis += "\n\t" + t.Libelle;
                        devis += " pendant " + nbmois.ToString() + " mois";
                        devis += "\n\t\tTarif : " + (totalsansremise).ToString("0.00");
                        if (totalsansremise != totalremise) devis += "\n\t\tTarif remisé: " + (totalremise).ToString("0.00");
                        if (partSecu > 0) devis += "\n\t\tRemboursement Secu: " + partSecu.ToString("0.00");
                    }

                }
            }


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Devis", devis);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+ m.ToString()+" mois");
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
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
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());


        }

        private void AddCourrierAttributsConsentement(Correspondant Praticien)
        {

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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
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
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

        }


        private void BtnNext_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabInfoComp)
            {
                if (cbxPratResp.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner un Praticien responsable!");
                    return;
                }

                if (cbxAssResp.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner une Assistante responsable!");
                    return;
                }
            }

            if (tabControl1.SelectedTab == tabDuree)
            {

                if ((!chkbxSem1.Checked) &&
                 (!chkbxSem2.Checked) &&
                 (!chkbxSem3.Checked))
                {
                    MessageBox.Show("Aucun semestre n'est sélectionné !");
                    return;
                }

                if (dtpDebuTraitement.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("La date de début de traitement ne peut pas etre passée");
                    return;
                }

                BuildProposition();


                

                PrintDevisEtConsentement();
            }

           

            if (tabControl1.SelectedTab == tabSpecialistes)
            {


                foreach (Proposition p in _CurrentPatient.propositions)
                {
                    p.Etat = Proposition.EtatProposition.NonImprimé;
                    PropositionMgmt.InsertFullProposition(p);
                }
                

                MgmtDevis.CreateDevis(_CurrentPatient.propositions);

                if (DEPAAssocier != null)
                {
                    foreach (Semestre s in semdelaDEP)
                    {
                        SemestreMgmt.AssocierDEP(s, DEPAAssocier);
                    }
                }

                BASEDiag_BL.OLEAccess.BasePractice.SetPatientCourantById(CurrentPatient.Id);
                BASEDiag_BL.OLEAccess.BasePractice.Activate();
                owner.Close();
                return;
            }


            int nexttab = lsttp.IndexOf(tabControl1.SelectedTab) + 1;
            if (nexttab < lsttp.Count)
            {
                tabControl1.TabPages.Clear();            
                tabControl1.TabPages.Add(lsttp[nexttab]);
            }
        }

        private void BtnPGOrthalis_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void txtbxTarifReel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CurrentPatient.propositions.Count > 0)
            {
                FrmVisuProposition frm = new FrmVisuProposition(CurrentPatient);
                frm.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BASEDiag_BL.OLEAccess.BASELabo.NouvelleDemandeInStandBy(CurrentPatient.Id);
        }

        private void tabDuree_Click(object sender, EventArgs e)
        {

        }

        private void lstBxPersonneAContacter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxPratResp_Load(object sender, EventArgs e)
        {

        }

        private void tabInfoComp_Click(object sender, EventArgs e)
        {

        }


    }
}
