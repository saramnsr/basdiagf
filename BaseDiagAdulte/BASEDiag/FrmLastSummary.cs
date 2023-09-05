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
using Microsoft.Win32;
namespace BASEDiagAdulte
{
    public partial class FrmLastSummary : Form
    {


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

        public FrmLastSummary(basePatient patient, int screenidx)
        {


            screenlst = Screen.AllScreens;
            CurrentPatient = patient;
            InitializeComponent();

            CurrentScreenIdx = RegistryParameters.GetScreenNumberOf(this.GetType());
            CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;
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


            pbxOccDroit.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            pbxOccFace.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            pbxOccGauche.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);

            pbxOccDroit.zoomAuto();
            pbxOccDroit.Center();
            pbxOccFace.zoomAuto();
            pbxOccFace.Center();
            pbxOccGauche.zoomAuto();
            pbxOccGauche.Center();

            pbxPano.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            pbxPano.zoomAuto();
            pbxPano.Center();
            barrePatient1.patient = CurrentPatient;


//            if(CurrentPatient.AgeNbYears>=9)
                this.wizard = new BASEDiagAdulte.Ctrls.Ctrl9a99ans(CurrentPatient, this);
           // else
           //     this.wizard = new BASEDiag.Ctrls.Ctrl3a9ans(CurrentPatient,this);

            
            wizard.Dock = DockStyle.Fill;

            pnlContainerWizard.Controls.Add(wizard);

            LoadPPT("Wizard");
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
                lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifs(ResumeCliniqueMgmt.resumeCl.diagnostics);

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

            if ((e.ItemState&ListViewItemStates.Focused)==ListViewItemStates.Focused)
                e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, e.Bounds);
           

            //e.DrawFocusRectangle(e.Bounds);
            e.DrawText();
            if (e.ColumnIndex == 0)
            {
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.check, new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Height, e.Bounds.Height));
                if (p.Etat == Proposition.EtatProposition.Soumis)
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Height, e.Bounds.Height));
               
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
            pbxSourire.PaintOn(g, rect, true);
            string  f = Path.GetTempFileName();
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
            pbxRadio.PaintOn(g, rect, true);
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail==null?"":Praticien.MainMail.Value);
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre== basePatient.Sexe.Feminin?"F":"M");
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", CurrentPatient.NextRDV != null ? _CurrentPatient.NextRDV.StartDate.ToString() : "");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _CurrentPatient.DateSoldePatient.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _CurrentPatient.Solde==null?"":_CurrentPatient.Solde.Value.ToString("0.0"));
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", c.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", c.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", c.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", c.MainAdresse.adresse.Ville);
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

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse==null?"":_CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse==null?"":_CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse==null?"":_CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _CurrentPatient.Dentiste.correspondant.MainAdresse==null?"":_CurrentPatient.Dentiste.correspondant.MainAdresse.adresse.Ville);
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
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail==null?"":Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax == null ? "" : Praticien.MainFax.Value);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Ville);
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

            int nbttmt;
            var comment = MgmtCommentairesHisto.GetLastCommentaire(CurrentPatient, CommentHisto.CommentHistoType.Traitement, out nbttmt);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", comment == null ? "" : comment.comment);

            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", txtbxPlanTraitement.Text);



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


            RegistryParameters.SetScreenNumberOf(this.GetType(),CurrentScreenIdx);
            this.Bounds = screenlst[CurrentScreenIdx].Bounds;

            //   this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
        }

       
        


       
    }
}
