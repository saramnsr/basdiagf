using BasCommon_BL;
using BasCommon_BO;
using BASEDiag_BL;
using BASEDiag_BO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiag
{
    public partial class FrmLastsummaryEnfant : Form
    {
           public InvisalignAccount CompteInvisalign { get; set; }

        UserControl wizard;
        Screen[] screenlst;
        int CurrentScreenIdx = 0;


        private List<Correspondant> _SpecialistesAvoir = new List<Correspondant>();
        public List<Correspondant> SpecialistesAvoir
        {
            get
            {
                return _SpecialistesAvoir;
            }
            set
            {
                _SpecialistesAvoir = value;
            }
        }




        Font ft = new Font("Garamond", 12, FontStyle.Regular);

        public FrmLastsummaryEnfant(basePatient patient, int screenidx, InvisalignAccount compteinvisalign)
        {
            CompteInvisalign = compteinvisalign;
            screenlst = Screen.AllScreens;
            CurrentPatient = patient;
            InitializeComponent();

            CurrentScreenIdx = RegistryParameters.GetScreenNumberOf(this.GetType());
            CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;
        }

        public InvisalignAccount compteinvisalign { get; set; }
        
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
        private void FrmLastSummary_Load(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            this.Text = CurrentPatient.ToString();

            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;


            pbxFace.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
            pbxFace.zoomAuto();
            pbxFace.Center();
            pbxFace.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse1;

            pbxSourire.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);
            pbxSourire.zoomAuto();
            pbxSourire.Center();
            pbxSourire.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse2;

            pbxProfil.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            pbxProfil.zoomAuto();
            pbxProfil.Center();
            pbxProfil.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse62;

            pbxRadio.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            pbxRadio.loadPhoto(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            pbxRadio.Transparence = 1f;
            pbxRadio.zoomAuto();
            pbxRadio.Center();
            pbxRadio.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse7;

            if (ResumeCliniqueMgmt.resumeCl.IsSynchronized)
            {
                pbxRadio.SynchroRadioPhoto(ResumeCliniqueMgmt.resumeCl.synchrozoom,
                                    ResumeCliniqueMgmt.resumeCl.synchroangle,
                                    ResumeCliniqueMgmt.resumeCl.synchrooffset);
                pbxRadio.Transparence = .5;
            }

            ///// imprimer superposition
          
            superposition = GetAnalyseImage();
            /////
            pbxOccDroit.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            pbxOccFace.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            pbxOccGauche.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);
        //    anSourire.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire);

            pbxOccDroit.zoomAuto();
            pbxOccDroit.Center();
            pbxOccDroit.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccD;
            pbxOccFace.zoomAuto();
            pbxOccFace.Center();
            pbxOccFace.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccF;
            pbxOccGauche.zoomAuto();
            pbxOccGauche.Center();
            pbxOccGauche.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccG;


        //   anSourire1.zoomAutoReverse();
       //    anSourire1.Center();
        //   anSourire1.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseSourire;

            pbxPano.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            pbxPano.zoomAuto();
            pbxPano.Center();
            barrePatient1.patient = CurrentPatient;

            LoadPPT("Wizard");

            RegenerateDiagObjTraitmnt();
        }

        private void LoadPPT(string subFolder)
        {
            try
            {
                string pptfolder = System.Configuration.ConfigurationManager.AppSettings["PPTFolder"] + "\\" + subFolder;
                lvPPT.Items.Clear();
                foreach (System.String s in Directory.GetFiles(pptfolder))
                {
                    FileInfo nfo = new FileInfo(s);

                    ListViewItem itm = new ListViewItem();
                    itm.ImageIndex = 0;
                    itm.Text = nfo.Name;
                    itm.Tag = s;

                    lvPPT.Items.Add(itm);
                }
            }
            catch (System.Exception)
            {

            }


        }


        private void RefreshInfos()
        {

            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                if (cd != null)
                    lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);

            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectif cd in CurrentPatient.SelectedObjectifs)
                lstBxObjectifs.Items.Add(cd);

        }

        private void imageCtrlAgg2_Load(object sender, EventArgs e)
        {

        }





        private void txtbxRisques_TextChanged(object sender, EventArgs e)
        {

        }



        private void pnlPlanTraitement_Paint(object sender, PaintEventArgs e)
        {


        }

        private void pnlPlanTraitementSecu_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlRisques_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlRisques_Click(object sender, EventArgs e)
        {
        }

        private void txtbxDetailsAppareillage_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }












        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvproposition_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void FrmLastSummary_Activated(object sender, EventArgs e)
        {
            if (CurrentPatient.infoscomplementaire != null) RefreshInfos();
        }

        private void lvproposition_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;

        }

        private void lvproposition_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {

            Proposition p = (Proposition)e.Item.Tag;
            e.DrawBackground();

            if ((e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
                e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, e.Bounds);


            //e.DrawFocusRectangle(e.Bounds);
            e.DrawText();
            if (e.ColumnIndex == 0)
            {
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Height, e.Bounds.Height));
                if (p.Etat == Proposition.EtatProposition.Soumis)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Height, e.Bounds.Height));

            }
        }

        private void lvproposition_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();

        }


        private void AddCourrierAttributs(Correspondant Praticien)
        {
            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            Bitmap ScreenShotResumebmp = new Bitmap(tableLayoutPanel2.Bounds.Width, tableLayoutPanel2.Bounds.Height);

            string tmpfile = Path.GetTempFileName();

            tableLayoutPanel2.DrawToBitmap(ScreenShotResumebmp, tableLayoutPanel2.Bounds);
            ScreenShotResumebmp.Save(tmpfile);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ScreenShotResume", tmpfile);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageSupVal", (pbxFace.EtageSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoyVal", (pbxFace.EtageMoy * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfVal", (pbxFace.EtageInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoy2Val", (pbxFace.EtageMoy2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInf2Val", (pbxFace.EtageInf2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfSupVal", (pbxFace.EtageInfSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfInfVal", (pbxFace.EtageInfInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationLevreInfVal", (pbxFace.DeviationLevreInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationMentonVal", (pbxFace.DeviationMenton * 100).ToString("0.0"));

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EspaceDentaireBuccal", (pbxSourire.EspaceDentaireBuccal * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireDroit", (pbxSourire.IncisiveMolaireDroit * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireGauche", (pbxSourire.IncisiveMolaireGauche * 100).ToString("0.0"));

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FMA", pbxRadio.FMA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNA", pbxRadio.SNA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNB", pbxRadio.SNB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ANB", pbxRadio.ANB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IF", pbxRadio.IF.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IM", pbxRadio.IM.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("I2F", pbxRadio.I2F.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittal", ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensVertical", ResumeCliniqueMgmt.resumeCl.SensVertical.ToString());



            float ratio = (800f / pbxSourire.Width);
            Rectangle rect = new Rectangle(0, 0, 800, (int)(pbxSourire.Height * ratio));
            pbxSourire.zoomRadio = pbxSourire.zoomRadio * ratio;
            pbxSourire.Center(rect);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
          //  pbxSourire.PaintOn(g, rect, true);
            string f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseSourire", f);
            pbxSourire.zoomAuto();


            ratio = (800f / pbxFace.Width);
            rect = new Rectangle(0, 0, 800, (int)(pbxFace.Height * ratio));
            pbxFace.zoomRadio = pbxFace.zoomRadio * ratio;
            pbxFace.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            pbxFace.PaintOn(g, rect, true);
            f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFace", f);
            pbxFace.zoomAuto();

            ratio = (800f / pbxRadio.Width);
            rect = new Rectangle(0, 0, 800, (int)(pbxRadio.Height * ratio));
            pbxRadio.zoomRadio = pbxRadio.zoomRadio * ratio;
            pbxRadio.Center(rect);
            bmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(bmp);
            pbxRadio.PaintOn(g, rect);
            f = Path.GetTempFileName();
            bmp.Save(f);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseCephalo", f);
            pbxRadio.zoomAuto();


            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse.adresse.Ville);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comment == null ? "" : comment.comment);

        }

        private void GenerateMailPatient()
        {

            FrmEditMail frm = new FrmEditMail("Envois des mails patient");
            frm.txtbxAdress.Text = CurrentPatient.MainMail.Value;
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




        private void GenerateCourrier(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {
            AddCourrierAttributs(Praticien, c);

            System.Threading.Thread.Sleep(2000);
            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionCompteRendu);

            if (DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }


        private void AddCourrierAttributs(Correspondant Praticien, Correspondant c)
        {


            if (_CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(_CurrentPatient);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailSubject", "Compte rendu du Bilan Orthodontique");


            string body = "Cher Confrère,";
            body+="\n\n";
            body+="J’ai reçu en consultation-Bilan ( ci-joint) "+ CurrentPatient.Civilite+" "+ CurrentPatient.ToString();
            body += @" que vous suivez dans votre patientèle. Nous avons pris les empreintes optiques des dents pour développer un set-virtuel  (le Clincheck®) de  son futur traitement Invisalign®. 
Le traitement  commencera dans 1 mois, je vous donnerai plus de précision dès que les images virtuelles seront terminées. 

En attendant je vous rappel que si des soins dentaires doivent être effectués, il ne faut pas changer la forme des dents pour garder une parfaite adaptation des futur aligneurs. 

Restant à Votre disposition et vous souhaitant une excellente journée,";

            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailBody", body);


            List<string> lst = new List<string>();

            
            foreach (LienCorrespondant lco in CurrentPatient.Correspondants)
            { 
                if (lco.correspondant == null)
                    lco.correspondant = MgmtCorrespondants.getCorrespondant(lco.IdCorrespondance);
                if (lco.correspondant == null) continue;
                if (lco.correspondant.contacts == null)
                    MgmtCorrespondants.FillContacts(lco.correspondant);

                if (lco.correspondant.MainMail!=null)
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", CurrentPatient.NextRDV != null ? _CurrentPatient.NextRDV.StartDate.ToString() : "");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _CurrentPatient.DateSoldePatient.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _CurrentPatient.Solde == null ? "" : _CurrentPatient.Solde.Value.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", CurrentPatient.LastRDV != null ? _CurrentPatient.LastRDV.StartDate.ToString() : "");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_CORRESPONDANT", c.Id.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreCorrespondant", c.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomCorrespondant", c.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomCorrespondant", c.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionCorrespondant", c.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeCorrespondant", c.MainTel == null ? "" : c.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProCorrespondant", c.MainTel == null ? "" : c.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxCorrespondant", c.MainFax == null ? "" : c.MainFax.Value);

            if (c.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", c.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", c.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", c.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", c.MainAdresse.adresse.Ville);
            }else
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", "");
            }
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


                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DENTISTE", _CurrentPatient.Id.ToString());

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", _CurrentPatient.Dentiste.correspondant.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", _CurrentPatient.Dentiste.correspondant.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _CurrentPatient.Dentiste.correspondant.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", _CurrentPatient.Dentiste.correspondant.MainMail == null ? "" : _CurrentPatient.Dentiste.correspondant.MainMail.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", _CurrentPatient.Dentiste.correspondant.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", _CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : _CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", _CurrentPatient.Dentiste.correspondant.MainTel == null ? "" : _CurrentPatient.Dentiste.correspondant.MainTel.Value);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", _CurrentPatient.Dentiste.correspondant.MainFax == null ? "" : _CurrentPatient.Dentiste.correspondant.MainFax.Value);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null || _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null ||  _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null || _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse == null  || _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse == null ? "" : _CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Ville);
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Ville);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");



            Bitmap bmp = new Bitmap(panel1.Bounds.Width, panel1.Bounds.Height);

            string tmpfile = Path.GetTempFileName();

            panel1.DrawToBitmap(bmp, panel1.Bounds);
            bmp.Save(tmpfile);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ScreenShotResume", tmpfile);

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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("superposition", superposition);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", BASEDiag_BL.ResumeCliniqueMgmt.GenerateCompteRenduClinique());
           // BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PropositionDevis", BASEDiag_BL.ResumeCliniqueMgmt.GeneratePropositionDevis(CurrentPatient));

            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement",comment==null?"": comment.comment);



        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmPrinterSettings frm = new FrmPrinterSettings();
            frm.Show();
        }





        private void btnChangeScreen_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //   this.WindowState = FormWindowState.Normal;

            CurrentScreenIdx++;
            CurrentScreenIdx = CurrentScreenIdx >= screenlst.Length ? 0 : CurrentScreenIdx;


            RegistryParameters.SetScreenNumberOf(this.GetType(), CurrentScreenIdx);
            this.Bounds = screenlst[CurrentScreenIdx].Bounds;

            //   this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //if (CurrentPatient.Id < 0)
            //{
            //    MessageBox.Show(this,"Ce patient n'existe pas en base. Création de Devis impossible !","Devis impossible",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    return;
            //}


            //BaseCommonControls.FrmAccessREquest.CheckUser();
            //if ((UtilisateursMgt.CurrentUtilisateur==null) || (UtilisateursMgt.CurrentUtilisateur.Utilisateur==null))
            //    return;

            //if ((CurrentPatient.infoscomplementaire == null)||
            //    (CurrentPatient.infoscomplementaire.PraticienResponsable==null))
            //{
            //    frmChoixASsPrat frmassprat = new frmChoixASsPrat();
            //    if (frmassprat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        CurrentPatient.infoscomplementaire = new InfoPatientComplementaire();
            //        CurrentPatient.infoscomplementaire.AssistanteResponsable = frmassprat.assistante;
            //        CurrentPatient.infoscomplementaire.PraticienResponsable = frmassprat.praticien;
            //        CurrentPatient.infoscomplementaire.DateDebutTraitement = frmassprat.DateDebutTraitement;
            //        CurrentPatient.infoscomplementaire.IdPatient = CurrentPatient.Id;
            //        BasCommon_BL.baseMgmtPatient.setinfocomplementaire(CurrentPatient.infoscomplementaire);

            //    }
            //    else
            //        return;
            //}

            //FrmWizardPropositions frm = new FrmWizardPropositions(CurrentPatient.infoscomplementaire.NbSemestresEntame, CurrentPatient);

            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //    if (frm.Value is NewTraitement)
            //    {
            //        // frm.value 
            //        NewTraitement _Traitement = new NewTraitement();
            //        _Traitement = TraitementsMgmt.GetFullTraitement(frm.Value.id_Traitement);
            //        TraitementsMgmt.GetCommTraitements(ref _Traitement);
            //        int pp = 0;
            //        ActePG ap = new ActePG();
            //        Devis_TK d = new Devis_TK();
            //        d.DateProposition = DateTime.Now;
            //        d.DateAcceptation = null;
            //        d.DateArchivage = null;
            //        d.DateEcheance = DateTime.Now.AddDays(15);
            //        d.Titre = frm.TitreDevis;
            //        d.IdPatient = CurrentPatient.Id;
            //        d.Id_Traitement = _Traitement.id_Traitement;
            //        d.Traitement = _Traitement;
            //        d.DatePrevisionnelDeDebutTraitement = frm.DateDeDebut;
            //        d.patient = CurrentPatient;
            //        d.DatePrevisionnelDeFinTraitement = frm.DateDeFin;

            //        FrmChoixActes frmActes = new FrmChoixActes(_Traitement.id_Traitement, _Traitement.Traitement_libelle, ref d, false, frm.Duree);
            //        if (frmActes.ShowDialog() == DialogResult.OK)
            //        {

            //            int NombreLabo = 0;
            //            int NombreSterilisation = 0;
            //            d.Montant = 0;
            //            d.MontantAvantRemise = 0;
            //            foreach (CommTraitement ct in d.actesTraitement)
            //            {
            //                d.Montant = d.Montant + ct.Acte.prix_traitement;
            //                d.MontantAvantRemise = d.MontantAvantRemise + ct.Acte.prix_acte;
            //                foreach (CommActesTraitement c in ct.ActesSupp)
            //                {
            //                    d.Montant = d.Montant + c.prix_traitement;
            //                    d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
            //                }
            //                foreach (CommActesTraitement c in ct.Radios)
            //                {
            //                    d.Montant = d.Montant + c.prix_traitement;
            //                    d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
            //                }
            //                foreach (CommActesTraitement c in ct.photos)
            //                {
            //                    d.Montant = d.Montant + c.prix_traitement;
            //                    d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
            //                }
            //                foreach (CommMaterielTraitement c in ct.Materiels)
            //                {
            //                    d.Montant = d.Montant + c.prix_traitement;
            //                    d.MontantAvantRemise = d.MontantAvantRemise + c.prix_materiel;
            //                    if (c.Famille != null)
            //                    {
            //                        if (c.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0)
            //                            NombreLabo += 1;
            //                        if (c.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
            //                            NombreSterilisation += 1;
            //                    }
            //                }
            //                if (ct.echeancestemp.Count == 0)
            //                {
            //                    int CtrSemestre = 0;
            //                    DateTime vDateEcheance = DateTime.Now;
            //                    if (ct.semestres.Count == 0)
            //                    {
            //                        for (int i = 1; i <= frm.Duree; i++)
            //                        {
            //                            Semestre s = new Semestre();
            //                            s.CodeSemestre = ct.Acte.acte_libelle;
            //                            s.DateDebut = vDateEcheance.AddMonths(6);
            //                            s.DateFin = s.DateDebut.AddMonths(6);
            //                            vDateEcheance = vDateEcheance.AddMonths(6);
            //                            s.Montant_Honoraire = TraitementsMgmt.GetPrixCom(ct);
            //                            s.Montant_AvantRemise = TraitementsMgmt.GetPrixComAvantRemise(ct);
            //                            //s.traitementSecu = tmplte;
            //                            //s.Parent = t;
            //                            CtrSemestre++;
            //                            s.NumSemestre = CtrSemestre;


            //                            ct.semestres.Add(s);
            //                        }
            //                    }
            //                    Proposition p = new Proposition(d);

            //                    BaseCommonControls.FrmFinancement frmP = new BaseCommonControls.FrmFinancement(p, p.patient, ct.echeancestemp, ct.Id);
            //                    //BaseCommonControls.FrmFinancement frmP = new BaseCommonControls.FrmFinancement(p, p.patient, ct.echeancestemp);
            //                    //FrmFinancement frmP = new FrmFinancement(p, p.patient, ct.echeancestemp);
            //                    ct.echeancestemp.Clear();
            //                    BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);
            //                    foreach (BaseTempEcheanceDefinition ted in frmP.Montants)
            //                    {
            //                        if (ted.acte != null)
            //                        {
            //                            TempEcheanceDefinition tted = new TempEcheanceDefinition()
            //                            {
            //                                acte = ted.acte,
            //                                AlreadyPayed = ted.AlreadyPayed,
            //                                CanRecalculate = ted.CanRecalculate,
            //                                DAteEcheance = ted.DAteEcheance,
            //                                Id = ted.Id,
            //                                IdSemestre = ted.acte.Semestre,
            //                                Libelle = ted.Libelle,
            //                                Montant = ted.Montant,
            //                                ParPrelevement = ted.ParPrelevement,
            //                                ParVirement = ted.ParVirement,
            //                                payeur = ted.payeur,


            //                            };
            //                            ct.echeancestemp.Add(tted);

            //                        }

            //                    }
            //                }
            //            }
            //            d.MontantScenario = d.Montant;
            //            MgmtDevis.CreateDevis_TK(d);
            //            OLEAccess.BASEPRACTICE.SetPatientCourantById(CurrentPatient.Id);
            //        }
            //    }
                
            //    else  if ((frm.tpetrmnt == Devis.enumtypePropositon.ALaCarte) && (frm.devisALaCarte != null))
            //    {

            //        FrmDetailDevisALaCarte frmupd = new FrmDetailDevisALaCarte(frm.devisALaCarte, CurrentPatient, false);
            //        if (frmupd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            MgmtDevis.CreateDevis(frmupd.devis);
            //            if (CurrentPatient.devis == null)
            //                CurrentPatient.devis = new List<Devis>();
            //            CurrentPatient.devis.Add(frmupd.devis);

            //            BasCommon_BL.MgmtDevis.UpdateDevisandPropositions(frmupd.devis);
            //        }
            //        DialogResult = System.Windows.Forms.DialogResult.OK;
            //        Close();
            //    }
            //    else
            //    {
            //        List<Proposition> lstProp = new List<Proposition>();

            //        foreach (Proposition p in frm.value)
            //        {
            //            PropositionMgmt.InsertFullProposition(p);
            //            lstProp.Add(p);
            //        }

            //        Devis d = MgmtDevis.CreateDevis(lstProp, frm.ActesMateriel, frm.tpetrmnt, frm.DateDeDebut, frm.DateDeFin);
            //        d.TypeDevis = frm.tpetrmnt;

            //        FrmUpdateTarifProposition frmupd = new FrmUpdateTarifProposition(d, CurrentPatient, false);
            //        frmupd.CanCancel = true;
            //        if (CurrentPatient.devis == null)
            //            CurrentPatient.devis = new List<Devis>();

            //        if (frmupd.ShowDialog() == DialogResult.Cancel)
            //            MgmtDevis.DeleteDevis(d);
            //        else
            //            CurrentPatient.devis.Add(d);

            //        DialogResult = System.Windows.Forms.DialogResult.OK;
            //        Close();

            //        OLEAccess.BASEPRACTICE.SetPatientCourantById(CurrentPatient.Id);
            //    }


        //    }

           


            
        }
        


        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);


            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectif cd in CurrentPatient.SelectedObjectifs)
                if (cd!=null)
                    lstBxObjectifs.Items.Add(cd);

            /*
            foreach (CommonObjectifFromDiag cd in lstobjs)
                if (!CurrentPat.SelectedObjectifs.Contains(cd.objectif))
                    lstBxObjectifs.Items.Add(cd);
            */



        }

      

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstBxObjectifs_DrawItem(object sender, DrawItemEventArgs e)
        {
            CommonObjectif obj = null;
            if (e.Index == -1) return;


            if (lstBxObjectifs.Items[e.Index] is CommonObjectifFromDiag)
                obj = ((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).objectif;

            if (lstBxObjectifs.Items[e.Index] is CommonObjectif)
                obj = ((CommonObjectif)lstBxObjectifs.Items[e.Index]);


            Brush b = Brushes.Black;

            if (CurrentPatient.SelectedObjectifs.Contains(obj))
                b = Brushes.Green;

            CommonDiagnostic selecteddiag = (CommonDiagnostic)lstBxDiag.SelectedItem;
            if (lstBxObjectifs.Items[e.Index] is CommonObjectifFromDiag)
            {
                obj = ((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).objectif;
                if (((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).diagnostic == selecteddiag)
                    b = Brushes.Blue;
            }

            e.Graphics.DrawString(lstBxObjectifs.Items[e.Index].ToString(), lstBxObjectifs.Font, b, e.Bounds.Location);

        }


        InvisalignPrescriptionFullObj PrescriptionFull = null;
        Boolean PrescriptionFullIsReady = false;
        Boolean PatientIsReady = false;
        private void button2_Click(object sender, EventArgs e)
        {

        //    if (CompteInvisalign != null)
        //    {
        //        if (MessageBox.Show(this, "Les informations du patient seront envoyées sur le compte : " + CompteInvisalign.login, "Compte invisalign", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
        //            return;

        //    }

        //    if ((baseMgmtPatient.GetInvisalignInfos(CurrentPatient)) ||
        //        (MessageBox.Show(this, "Ce patient existe déja, souhaitez vous juste envoyer la prescription?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No))
        //    {

        //        FrmWizardInvisalign frm = new FrmWizardInvisalign(CurrentPatient);
        //        if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        //        {



        //            FrmExportInvisalign frmexp = new FrmExportInvisalign(frm, CompteInvisalign);
        //            frmexp.bw.ProgressChanged += bw_ProgressChanged;
        //            frmexp.Show(this);

        //            PrescriptionFull = new InvisalignPrescriptionFullObj(InvisalignPrescriptionFullObj.InvisalignType.Compréhensive);
        //            PreparePrescription(PrescriptionFull);
        //            FrmWizardPrescription frmp = new FrmWizardPrescription(PrescriptionFull);

        //            if (frmp.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        //            {
        //                PrescriptionFullIsReady = true;
        //                if (PatientIsReady == true)
        //                {
        //                    FrmExportInvisalign frmei = new FrmExportInvisalign(PrescriptionFull, CurrentPatient, CompteInvisalign);
        //                    frmei.Show();
        //                    PatientIsReady = false;
        //                    PrescriptionFullIsReady = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            PrescriptionFull = new InvisalignPrescriptionFullObj(InvisalignPrescriptionFullObj.InvisalignType.Compréhensive);
        //            PreparePrescription(PrescriptionFull);
        //            FrmWizardPrescription frmp = new FrmWizardPrescription(PrescriptionFull);



        //            if (frmp.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        //            {
        //                FrmExportInvisalign frmei = new FrmExportInvisalign(PrescriptionFull, CurrentPatient, CompteInvisalign);
        //                frmei.Show();
        //            }
        //        }
        //    }
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage >= 40)
            //{
            //    PatientIsReady = true;
            //    if (PrescriptionFullIsReady == true)
            //    {
            //        FrmExportInvisalign frmei = new FrmExportInvisalign(PrescriptionFull, CurrentPatient, CompteInvisalign);
            //        frmei.Show();
            //        PatientIsReady = false;
            //        PrescriptionFullIsReady = false;
            //   }
            //}
        }

       

        private void PreparePrescription(InvisalignPrescriptionFullObj PrescriptionFull)
        {
            List<CommonObjectifFromDiag> lst = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);

            for (int i = lst.Count - 1; i >= 0; i--)
            {
                if (!CurrentPatient.SelectedObjectifs.Contains(lst[i].objectif))
                    lst.Remove(lst[i]);
            }

            PrescriptionFull.Etape10.Extraction = FrmWizardPrescription.ChoixDentToBoolean(ResumeCliniqueMgmt.resumeCl.DentsDeSagesse.Split(','));
            PrescriptionFull.Etape2.DoNotMoveTheseTeeth = FrmWizardPrescription.ChoixDentToBoolean(ResumeCliniqueMgmt.resumeCl.NoMvts.Split(','));
            PrescriptionFull.Etape3.TeethPermittedForAttachement = FrmWizardPrescription.ChoixDentToBoolean(ResumeCliniqueMgmt.resumeCl.NoTaquets.Split(','));
            
            //switch(ResumeCliniqueMgmt.resumeCl.ClasseCanG)
            //{
            //    case EntentePrealable.en_Class.Class_I: PrescriptionFull.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain; break;
            //    case EntentePrealable.en_Class.Class_II: PrescriptionFull.Etape4. = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain; break;

            //}
            int maxdevis = 1;
            foreach (CommonObjectifFromDiag cod in lst)
                if (maxdevis < cod.NumDevis) maxdevis = cod.NumDevis;

            switch (maxdevis)
            {
                case 1:
                    PrescriptionFull.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Lite;
                    break;
                case 2:
                    PrescriptionFull.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Compréhensive;
                    break;
                case 3:
                    PrescriptionFull.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.I7;
                    break;
                case 4:
                    PrescriptionFull.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.I7;
                    break;

            }

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
        }

        private void pbxFace_Load(object sender, EventArgs e)
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
        private void button3_Click(object sender, EventArgs e)
        {


            if (CurrentPatient.Id<0)
            {
                if (CurrentPatient.infoscomplementaire == null)
                {
                    CurrentPatient.infoscomplementaire = new InfoPatientComplementaire();
                    CurrentPatient.infoscomplementaire.PraticienResponsable = UtilisateursMgt.Praticiens[0];

                }
            }

            if (CurrentPatient.infoscomplementaire == null)
                CurrentPatient.infoscomplementaire = baseMgmtPatient.getinfocomplementaire(CurrentPatient.Id);

            if ((CurrentPatient.infoscomplementaire==null) ||(CurrentPatient.infoscomplementaire.PraticienResponsable == null))
            {
                MessageBox.Show("Aucun praticien responsable!");
                return;
            }

            FrmWizardCourrier frm = new FrmWizardCourrier(_CurrentPatient);

            if (CurrentPatient.AgeNbYears <= 16)
                frm.FileName = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduEnfant"];
            else
                frm.FileName =templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierCompteRenduAdulte"];

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Correspondant prat = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                if (prat == null)
                {
                    MessageBox.Show("Aucun praticien responsable");
                    return;
                }
                else
                {
                    foreach (baseSmallPersonne sc in frm.lstCorrespondant)
                    {
                        
                        Correspondant c = MgmtCorrespondants.getCorrespondant(sc.Id);
                        if (c == null) continue;
                        GenerateCourrier(frm.FileName, prat, c, false);
                    }
                }
            }
        }


        


        private void button5_Click(object sender, EventArgs e)
        {

            
            

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            
                  

        }

        private void button5_Click_2(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            if (CurrentPatient.infosinvisalign == null)
                baseMgmtPatient.GetInvisalignInfos(CurrentPatient);

            if (!string.IsNullOrEmpty(CurrentPatient.infosinvisalign.IdInvisalign))
                Invisalign.CallIEInvisalign(CurrentPatient.infosinvisalign.NomInvisalign, CurrentPatient.infosinvisalign.PrenomInvisalign, CurrentPatient.infosinvisalign.IdInvisalign);

            Cursor = Cursors.Default;
        }

        private void smilers_Click(object sender, EventArgs e)
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

        private void lnkToSmilers_Click(object sender, EventArgs e)
        {
            CurrentPatient.infoSmilers = SmilersMgmt.getInfoSmilers(CurrentPatient.Id);
            Browser br = new Browser(CurrentPatient);
            br.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void smilers_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

      
    }

    
}
