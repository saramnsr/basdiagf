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
namespace BASEDiag.Ctrls
{
    public partial class Ctrl9a99ans : UserControl
    {
        TemplateActePG actegestion = TemplateApctePGMgmt.getTemplatesActeGestion("ORTHP");

        public InvisalignAccount AccountInvisalign { get; set; }
        public bool PraticienUnique
        {
            get
            {
                return chkbxPratUnique.Checked;
            }
            set
            {
                chkbxPratUnique.Checked = value;
            }

        }

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
            GenerateImpressionsPatient(false);
        }


        private void GenerateImpressionsPatient(bool DirectPrint)
        {
            Correspondant pr = MgmtCorrespondants.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
            

            string CourriersPatient =System.Configuration.ConfigurationManager.AppSettings["CourriersPatient"];

            if (CourriersPatient == null)
            {
                MessageBox.Show("Aucun courrier Patient parametré !\n cle:CourriersPatient dans .config");
                return;
            }

            string[] courriers = CourriersPatient.Split('\n');

            foreach (string s in courriers)
            {
                string courrier = templateFolder + s;
                AddCourrierAttributs(pr);

                FileInfo nfo = new FileInfo(courrier.Trim());
                if (!nfo.Exists)
                {
                    MessageBox.Show(courrier.Trim() + " non trouvé!");
                    continue;
                }

                BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionPatient);


                if (!DirectPrint)
                    BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(nfo.FullName);
                else

                    BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(nfo.FullName);
            }


        }

        private void GenerateMailPatient()
        {

           // FrmEditMail frm = new FrmEditMail("Envois des mails patient");
           
          //  frm.Sujet = "Compte rendu du Bilan Orthodontique";
         //   frm.txtbxBody.Text = "Veuillez trouver ci joint le compte rendu du bilan orthodontique de " + CurrentPatient.Civilite + " " + CurrentPatient.ToString();


            List<string> lst = new List<string>();


            foreach (LienCorrespondant lco in CurrentPatient.Correspondants)
            {
                if (lco.correspondant == null)
                    lco.correspondant = MgmtCorrespondants.getCorrespondant(lco.IdCorrespondance);
                if (lco.correspondant.contacts == null)
                    MgmtCorrespondants.FillContacts(lco.correspondant);

                if (lco.correspondant.MainMail != null)
                    if (!lst.Contains(lco.correspondant.MainMail.Value))
                        lst.Add(lco.correspondant.MainMail.Value);

            }

            string add = "";
            foreach (string s in lst)
            {
                if (!string.IsNullOrEmpty(add)) add += ";";
                add += s;
            }

          //  frm.txtbxAdress.Text = add;

          //  if (frm.ShowDialog() == DialogResult.OK)
            {

                Correspondant pr = MgmtCorrespondants.getCorrespondant(_CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                

                string CourriersPatient =  System.Configuration.ConfigurationManager.AppSettings["CourriersPatient"];

                if (CourriersPatient == "")
                {
                    MessageBox.Show("Aucun courrier Patient parametré !\n cle:CourriersPatient dans .config");
                    return;
                }

                string[] courriers = CourriersPatient.Split('\n');

                foreach (string s in courriers)
                {
                    string courrier = templateFolder + s;
                    AddCourrierAttributs(pr);
                    BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(courrier.Trim());
                }


            }


        }


        private void AddCourrierAttributs(Correspondant Praticien)
        {

            if (CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPatient);


            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet+ " : Bilan Orthodontique de " + CurrentPatient.Civilite + " " + CurrentPatient.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailBody", "Veuillez trouver ci joint le bilan orthodontique de " + CurrentPatient.Civilite + " " + CurrentPatient.ToString());


            List<string> lst = new List<string>();


            foreach (LienCorrespondant lco in CurrentPatient.Correspondants)
            {
                if (lco.correspondant == null)
                    lco.correspondant = MgmtCorrespondants.getCorrespondant(lco.IdCorrespondance);
                if (lco.correspondant.contacts == null)
                    MgmtCorrespondants.FillContacts(lco.correspondant);

                if (lco.correspondant.MainMail != null)
                    if (!lst.Contains(lco.correspondant.MainMail.Value))
                        lst.Add(lco.correspondant.MainMail.Value);

            }

            string add = "";
            foreach (string s in lst)
            {
                if (!string.IsNullOrEmpty(add)) add += ";";
                add += s;
            }

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", add);



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
            string f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseSourire", f);
            
            ratio = (800f / owner.pbxFace.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxFace.Height * ratio));
            owner.pbxFace.zoomRadio = owner.pbxFace.zoomRadio * ratio;
            owner.pbxFace.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxFace.PaintOn(g, rect,true);
            f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFace", f);

            ratio = (800f / owner.pbxRadio.Width);
            rect = new Rectangle(0, 0, 800, (int)(owner.pbxRadio.Height * ratio));
            owner.pbxRadio.zoomRadio = owner.pbxRadio.zoomRadio * ratio;
            owner.pbxRadio.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            owner.pbxRadio.PaintOn(g, rect);
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

            var comm = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comm==null?"":comm.comment);


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

             CurrentPatient.devis_TK = MgmtDevis.getDevis_TK(CurrentPatient);

            if (Currentdevis.Count == 0 && CurrentPatient.devis_TK.Count  == 0)
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
                foreach (Devis_TK p in CurrentPatient.devis_TK)
                {

                    if ((lastacceptation == null) || (p.DateAcceptation > lastacceptation))
                        lastacceptation = p.DateAcceptation;
                    if (lastacceptation == null)
                        lblDevis.Text = "";
                    else
                        lblDevis.Text = "Dernier Devis accepté le " + lastacceptation.Value.ToString("dd MMM yyyy \n");


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


                try
                {
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
                catch (System.Exception)
                { }
                
                    
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

            //tabControl1.TabPages.Clear();
            //  tabControl1.TabPages.Add(lsttp[0]);

          


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
                if (CurrentPatient.infoscomplementaire.PraticienResponsable == null)
                {
                    MessageBox.Show("Aucun praticien affecté à ce patient");
                }
                else
                {
                    Correspondant prat = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                    foreach (baseSmallPersonne sc in frm.lstCorrespondant)
                    {
                        Correspondant c = MgmtCorrespondants.getCorrespondant(sc.Id);
                        if (c != null)
                        {
                            string sujet = "Bilan Orthodontique";
                            string body = "";
                            GenerateCourrier(frm.FileName, prat, c, false, body, sujet);
                           
                        }
                    }
                }
            }
        }
        public string GetAnalyseImage()
        {
            Ctrls.Analyse7 analyseForIlmpression = new Ctrls.Analyse7();
            analyseForIlmpression.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            analyseForIlmpression.loadPhoto(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            analyseForIlmpression.Transparence = 1f;
            analyseForIlmpression.zoomAuto();
            analyseForIlmpression.Center();
            analyseForIlmpression.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse7;

            if (ResumeCliniqueMgmt.resumeCl.IsSynchronized)
            {
                analyseForIlmpression.SynchroRadioPhoto(ResumeCliniqueMgmt.resumeCl.synchrozoom,
                                    ResumeCliniqueMgmt.resumeCl.synchroangle,
                                    ResumeCliniqueMgmt.resumeCl.synchrooffset);
                analyseForIlmpression.Transparence = .5;
            }
            Bitmap bmp = new Bitmap(analyseForIlmpression.Width, analyseForIlmpression.Height);
            Graphics g = Graphics.FromImage(bmp);
            analyseForIlmpression.PaintOn(g, new Rectangle(0, 0, analyseForIlmpression.Width, analyseForIlmpression.Height));
            string f = Path.GetTempFileName();
            f = f.Replace("tmp", "jpg");
            bmp.Save(f);
            return f;
        }
        string superposition = "";

        private void GenerateCourrier(string file, Correspondant Praticien, Correspondant c, bool DirectPrint, string mailbody,string mailsubject)
        {
          
            AddCourrierAttributs(Praticien, c);

            System.Threading.Thread.Sleep(2000);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet + " : " + mailsubject);


            OLEAccess.BASLetter.AddAttribut("MailBody", mailbody);
            OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", c.MainMail != null ? c.MainMail.Value : "");


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


            Bitmap bmp = new Bitmap(owner.tableLayoutPanel2.Bounds.Width, owner.tableLayoutPanel2.Bounds.Height);

            string tmpfile = Path.GetTempFileName();

            owner.tableLayoutPanel2.DrawToBitmap(bmp, owner.tableLayoutPanel2.Bounds);
            bmp.Save(tmpfile);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ScreenShotResume", tmpfile);



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


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

            var comm = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comm == null ? "" : comm.comment);

            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);



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
            int IdUser;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                //if (ValidityDate > DateTime.Now)
                //{
                prefix = objValidityUser;
                //}
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
        private void button2_Click_1(object sender, EventArgs e)
        {
            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);

            
            frm.FileName = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduEnfant"];
            superposition = GetAnalyseImage();
         
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant prat = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                foreach (baseSmallPersonne sc in frm.lstCorrespondant)
                {
                    Correspondant c = MgmtCorrespondants.getCorrespondant(sc.Id);

                    string sujet = "Compte rendu du Bilan Orthodontique";
                     string body = "Cher Confrère,";
            body+="\n\n";
            body+="J’ai reçu en consultation-Bilan ( ci-joint) "+ CurrentPatient.Civilite+" "+ CurrentPatient.ToString();
            body += @" que vous suivez dans votre patientèle. Nous avons pris les empreintes optiques des dents pour développer un set-virtuel  (le Clincheck®) de  son futur traitement Invisalign®. 
Le traitement  commencera dans 1 mois, je vous donnerai plus de précision dès que les images virtuelles seront terminées. 

En attendant je vous rappel que si des soins dentaires doivent être effectués, il ne faut pas changer la forme des dents pour garder une parfaite adaptation des futur aligneurs. 

Restant à Votre disposition et vous souhaitant une excellente journée,";

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("superposition", superposition);
            GenerateCourrier(frm.FileName, prat, c, false,body, sujet);
          
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
            CurrentPatient.infoscomplementaire.PraticienUnique = chkbxPratUnique.Checked;

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

            
                CurrentPatient.infoscomplementaire.NbSemestresEntame = 0;
           

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


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet + " : Devis pour traitement d'orthodontie");


            string body = "";

            switch (_CurrentPatient.Civilite)
            {
                case "Mr": body = "Monsieur,"; break;
                case "Mme": body = "Madame,"; break;
                case "Mlle": body = "Mademoiselle,"; break;
            }

            body += "\n\n" + "Veuillez trouver ci joint votre devis en date du " + DateTime.Now.ToString();

            OLEAccess.BASLetter.AddAttribut("MailBody", body);
            OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", _CurrentPatient.MainMail != null ? _CurrentPatient.MainMail.Value : "");



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


            var comm = MgmtCommentairesHisto.GetLastCommentaire(_CurrentPatient, CommentHisto.CommentHistoType.Traitement);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comm == null ? "" : comm.comment);


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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet + " : Consentement pour traitement d'orthodontie");


            string body = "";

            switch (_CurrentPatient.Civilite)
            {
                case "Mr": body = "Monsieur,"; break;
                case "Mme": body = "Madame,"; break;
                case "Mlle": body = "Mademoiselle,"; break;
            }

            body += "\n\n" + "Veuillez trouver ci joint votre consentement éclairé à nous retourner signé au cabinet";

            OLEAccess.BASLetter.AddAttribut("MailBody", body);
            OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", _CurrentPatient.MainMail != null ? _CurrentPatient.MainMail.Value : "");



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

            var comm = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comm == null ? "" : comm.comment);


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
          /*  if (nexttab < lsttp.Count)
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(lsttp[nexttab]);
            }*/
            if (nexttab == tabControl1.TabCount) return;
            tabControl1.SelectedTab = tabControl1.TabPages[nexttab];
            //AfterChangeTab(tabControl1.TabPages[nexttab]);
        }

        private void AfterChangeTab(TabPage selectedTabpage)
        {
            
            if (selectedTabpage == tabPrpositions)
            {
                Currentdevis = MgmtDevis.getDevis(CurrentPatient);
                CurrentPatient.devis_TK  = MgmtDevis.getDevis_TK(CurrentPatient);
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


        public static Devis CreateDevisInt(InfoPatientComplementaire infocomplementaire, basePatient CurrentPatient, List<Proposition> CurrentPropositions)
        {
            FrmWizardPropositions frm = new FrmWizardPropositions(infocomplementaire.NbSemestresEntame, CurrentPatient);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string folder = "";
                string file = "";
                  if (frm.Value is NewTraitement)
                {


                    NewTraitement _Traitement = new NewTraitement();
                    _Traitement = TraitementsMgmt.GetFullTraitement(frm.Value.id_Traitement);

                    int pp = 0;
                    ActePG ap = new ActePG();
                    Devis_TK d = new Devis_TK();
                    d.DateProposition = DateTime.Now;
                    d.DateAcceptation = null;
                    d.DateArchivage = null;
                    d.DateEcheance = DateTime.Now.AddDays(15);
                    d.Titre = frm.TitreDevis;
                    d.IdPatient = CurrentPatient.Id;
                    d.Id_Traitement = _Traitement.id_Traitement;
                    d.Traitement = _Traitement;
                    d.DatePrevisionnelDeDebutTraitement = frm.DateDeDebut;
                    d.patient = CurrentPatient;
                    d.DatePrevisionnelDeFinTraitement = frm.DateDeFin;





                    TraitementsMgmt.GetCommTraitements(ref _Traitement);

                    d.actesTraitement = _Traitement.CommTraitement;



                    Double prix_total = 0;


                    double prix_ligne;

                    foreach (CommTraitement trt in d.Traitement.CommTraitement)
                    {

                        prix_ligne = 0;


                        //Gestion des scénarios
                        if (_Traitement.id_Traitement != -1)
                        {
                            trt.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(trt);
                            trt.Radios = TraitementsMgmt.GetCommActeSupTraitements(trt, "R");
                            trt.photos = TraitementsMgmt.GetCommActeSupTraitements(trt, "P");
                            trt.Materiels = TraitementsMgmt.GetCommMaterielsTraitements(trt);
                            trt.AutrePersonnes = TraitementsMgmt.GetTraitementAutrePersonne(trt);
                            trt.echeancestemp = new List<TempEcheanceDefinition>();

                        }

                        if (_Traitement.id_Traitement == -1)
                        {
                            //if (_id_traitement != -1 && DevisTraitement != null)
                            //{
                            trt.ActesSupp = MgmtDevis.GetCommActeSupDevis(trt);
                            trt.Radios = MgmtDevis.GetCommActeSupDevis(trt, "R");
                            trt.photos = MgmtDevis.GetCommActeSupDevis(trt, "P");
                            trt.Materiels = MgmtDevis.GetCommMaterielsDevis(trt);
                            trt.AutrePersonnes = MgmtDevis.GetDevisAutrePersonne(trt);
                            trt.echeancestemp = MgmtDevis.get_tempecheances_TK(trt);
                            if (trt.echeancestemp.Count == 0)
                            {

                                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                                ted.DAteEcheance = trt.devis.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                                ted.Montant = trt.prix;
                                ted.Libelle = trt.Acte.acte_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = trt.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;
                                trt.echeancestemp.Add(ted);
                            }
                            else
                            {
                                foreach (BaseTempEcheanceDefinition bted in trt.echeancestemp)
                                {
                                    bted.acte = ap;
                                    bted.acte.Libelle = trt.Acte.acte_libelle;
                                }
                            }
                        }
                        else
                        {


                            if (d != null)
                            {
                                if (trt.ActesSupp == null)
                                    trt.ActesSupp = new List<CommActesTraitement>();


                                if (trt.Radios == null)
                                    trt.Radios = new List<CommActesTraitement>();

                                if (trt.photos == null)
                                    trt.photos = new List<CommActesTraitement>();

                                if (trt.Materiels == null)
                                    trt.Materiels = new List<CommMaterielTraitement>();

                                trt.Acte.prix_acte = trt.Acte.prix_traitement;

                                if (trt.ActesSupp != null)
                                {
                                    foreach (CommActesTraitement AC in trt.ActesSupp)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }

                                if (trt.Radios != null)
                                {
                                    foreach (CommActesTraitement AC in trt.Radios)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }
                                if (trt.photos != null)
                                {
                                    foreach (CommActesTraitement AC in trt.photos)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }
                                if (trt.Materiels != null)
                                {
                                    foreach (CommMaterielTraitement AC in trt.Materiels)
                                    {
                                        AC.prix_materiel = AC.prix_traitement;
                                    }
                                }

                            }
                        }






                        if (trt.ActesSupp == null)
                            trt.ActesSupp = new List<CommActesTraitement>();


                        if (trt.Radios == null)
                            trt.Radios = new List<CommActesTraitement>();

                        if (trt.photos == null)
                            trt.photos = new List<CommActesTraitement>();

                        if (trt.Materiels == null)
                            trt.Materiels = new List<CommMaterielTraitement>();

                        if (trt.ActesSupp != null)
                        {
                            foreach (CommActesTraitement cap in trt.ActesSupp)
                            {
                                // cap.Parent = trt;
                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                            }
                        }
                        if (trt.Materiels != null)
                        {

                            foreach (CommMaterielTraitement cap in trt.Materiels)
                            {
                                cap.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(cap.idMateriel);
                                if (cap.Famille != null)
                                    if (cap.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || cap.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                                    {
                                        prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                        prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                    }
                            }
                        }
                        if (trt.Radios != null)
                        {

                            foreach (CommActesTraitement cap in trt.Radios)
                            {

                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                            }
                        }
                        if (trt.photos != null)
                        {

                            foreach (CommActesTraitement cap in trt.photos)
                            {

                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));

                            }
                        }
                        prix_total = prix_total + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                        prix_ligne = prix_ligne + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                        trt.prix = prix_ligne;
                        //if (DevisTraitement != null)
                        //  if (DevisTraitement.actesTraitement == null) ;
                        // _DevisTraitement.MontantScenario = _DevisTraitement.MontantScenario + trt.prix;

                    }


                    // frm.value 


                    //d.actesTraitement = _Traitement.CommTraitement;
                    // MgmtDevis.CreateDevis_TK(d);
                    //  CurrentPatient.devis_TK.Add(d);


                    int NombreLabo = 0;
                    int NombreSterilisation = 0;
                    d.Montant = 0;
                    d.MontantAvantRemise = 0;
                    foreach (CommTraitement ct in d.actesTraitement)
                    {
                        d.Montant = d.Montant + ct.Acte.prix_traitement;
                        d.MontantAvantRemise = d.MontantAvantRemise + ct.Acte.prix_acte;
                        foreach (CommActesTraitement c in ct.ActesSupp)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommActesTraitement c in ct.Radios)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommActesTraitement c in ct.photos)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommMaterielTraitement c in ct.Materiels)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_materiel;
                            if (c.Famille != null)
                            {
                                if (c.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0)
                                    NombreLabo += 1;
                                if (c.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                                    NombreSterilisation += 1;
                            }
                        }
                        if (ct.echeancestemp.Count == 0)
                        {
                            int CtrSemestre = 0;
                            DateTime vDateEcheance = DateTime.Now;
                            if (ct.semestres.Count == 0)
                            {
                                for (int i = 1; i <= frm.Duree; i++)
                                {
                                    Semestre s = new Semestre();
                                    s.CodeSemestre = ct.Acte.acte_libelle;
                                    s.DateDebut = vDateEcheance.AddMonths(6);
                                    s.DateFin = s.DateDebut.AddMonths(6);
                                    vDateEcheance = vDateEcheance.AddMonths(6);
                                    s.Montant_Honoraire = TraitementsMgmt.GetPrixCom(ct);
                                    s.Montant_AvantRemise = TraitementsMgmt.GetPrixComAvantRemise(ct);
                                    //s.traitementSecu = tmplte;
                                    //s.Parent = t;
                                    CtrSemestre++;
                                    s.NumSemestre = CtrSemestre;


                                    ct.semestres.Add(s);
                                }
                            }

                        }
                    }
                    d.MontantScenario = d.Montant;
                    MgmtDevis.CreateDevis_TK(d);

                    CurrentPatient.devis_TK.Add(d);
                    Boolean ImpressionEcheances = false;
                    DialogResult dr = MessageBox.Show("Souhaitez-vous imprimer les échéanciers avec le devis ?", "Echeanciers", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                        ImpressionEcheances = true;
                    FrmImpression frmImp = new FrmImpression(d, ImpressionEcheances);
                     
                    frmImp.Show();

                    ////////////////////////////

                }
                else
                {
                if (frm.tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Aucun) return null;
                switch (frm.tpetrmnt)
                {
                    case BasCommon_BO.Devis.enumtypePropositon.Aucun: return null;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthopedie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthodontie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedontie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Adulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Adulte"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Pediatrie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Sucette"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont1: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention1"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont2: folder = templateFolder +  System.Configuration.ConfigurationManager.AppSettings["Devis_Contention2"]; break;

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

                    Devis d = MgmtDevis.CreateDevis(lstProp, frm.ActesMateriel, frm.tpetrmnt, frm.DateDeDebut, frm.DateDeFin);
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

                        BaseCommonControls.CommonActions.PrintDevis(d, CurrentPatient, file);

                    }


                    return d;


                }
                }
            }

            return null;
        }


        private void GererDevis()
        {
            if (CurrentPatient == null) return;
            Currentdevis = MgmtDevis.getDevis(CurrentPatient);
            //Modification NADHEMMMMMM
            CurrentPatient.devis_TK = MgmtDevis.getDevis_TK(CurrentPatient);
        
            bool DevisEnAttente = false;

            //Modification nadhemmmmm
            foreach (Devis_TK d in CurrentPatient.devis_TK)
                if (d.DateAcceptation == null)
                    DevisEnAttente = true;

            foreach (Devis d in Currentdevis)
                if (d.DateAcceptation == null) 
                    DevisEnAttente = true;


            if (DevisEnAttente)
            {


                FrmDevisManager frm = new FrmDevisManager(CurrentPatient, CurrentPatient.infoscomplementaire, CurrentPatient.propositions, Currentdevis, CurrentPatient.devis_TK);
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


            FrmDevisManager frm = new FrmDevisManager(CurrentPatient, CurrentPatient.infoscomplementaire, CurrentPatient.propositions, Currentdevis, CurrentPatient.devis_TK);

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if ((!baseMgmtPatient.GetInvisalignInfos(CurrentPatient)) ||
                (MessageBox.Show(this, "Ce patient existe déja, souhaitez vous juste envoyer la prescription?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No))
            {

                FrmWizardInvisalign frm = new FrmWizardInvisalign(CurrentPatient);
                if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {



                    FrmExportInvisalign frmexp = new FrmExportInvisalign(frm);

                    frmexp.Show(this);
                    Prescription();
                }
            }
            else
                Prescription();
        }



        private void Prescription()
        {
            InvisalignPrescriptionFullObj PrescriptionFull = new InvisalignPrescriptionFullObj(InvisalignPrescriptionFullObj.InvisalignType.Teen);



            List<CommonObjectifFromDiag> lst = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);

            for (int i = lst.Count - 1; i >= 0; i--)
            {
                if (!CurrentPatient.SelectedObjectifs.Contains(lst[i].objectif))
                    lst.Remove(lst[i]);
            }

            PrescriptionFull.Etape10.Extraction = FrmWizardPrescription.ChoixDentToBoolean(ResumeCliniqueMgmt.resumeCl.DentsDeSagesse.Split(','));

            PrescriptionFull.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Teen;

            

            foreach (CommonObjectifFromDiag cod in lst)
            {

                foreach (string s in cod.NumDiag.Split(','))
                {
                    int num = 0;
                    if (int.TryParse(s, out num))
                        Invisalign.ConvertThePrescription(num, PrescriptionFull);
                }

                if (!string.IsNullOrEmpty(cod.SpecialInstruction))
                {
                    if (!string.IsNullOrEmpty(PrescriptionFull.Etape11_SpecialInstruction))
                        PrescriptionFull.Etape11_SpecialInstruction += "\r\n";
                    PrescriptionFull.Etape11_SpecialInstruction += cod.SpecialInstruction;
                }
            }


            FrmWizardPrescription frm = new FrmWizardPrescription(PrescriptionFull);

            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {


                FrmExportInvisalign frmei = new FrmExportInvisalign(PrescriptionFull, CurrentPatient, AccountInvisalign);

                frmei.Show();

                //baseMgmtPatient.GetInvisalignInfos(CurrentPatient);


                //string CurrentError = "\n" + Invisalign.NewConnect();


                //CurrentError += "\n" + Invisalign.Newlogin();

                //HttpWebResponse response = Invisalign.PrescriptionSelectProduct(CurrentPatient.infosinvisalign.IdInvisalign, PrescriptionFull.tpePrescription);

                //if (response != null)
                //    Invisalign.PrescriptionFull(response, PrescriptionFull);


            }
        }

        private void tabPrpositions_Enter(object sender, EventArgs e)
        {
            AfterChangeTab(tabPrpositions);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            CurrentPatient.infoSmilers = SmilersMgmt.getInfoSmilers(CurrentPatient.Id);
            if (CurrentPatient.infoSmilers != null)
            {
                if (MessageBox.Show("Ce patient existe déjà vous voulez ouvrir fichie patient ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Browser br = new Browser(CurrentPatient);
                    br.Show();
                }
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            FrmSmilers frm = new FrmSmilers(CurrentPatient);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentPatient.infoSmilers = SmilersMgmt.getInfoSmilers(CurrentPatient.Id);
                this.Cursor = Cursors.WaitCursor;
                Browser br = new Browser(CurrentPatient);
                br.Show();

            }
            this.Cursor = Cursors.Default;

        }

        


    }
}
