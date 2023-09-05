using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using BasCommon_BL;
using BasCommon_BO;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Configuration;
using BaseCommonControls;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Outlook;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;


namespace BaseCommonControls
{
    public partial class FrmImpression : Form
    {
        private static MySqlConnection connection = null;
        private static string _CommonImageFolder = null;
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
        public static string CommonImageFolder
        {
            get
            {
                if (_CommonImageFolder == null)
                    _CommonImageFolder = System.Configuration.ConfigurationManager.AppSettings["TEMPLATE_FOLDER" + prefix];
                return _CommonImageFolder;
            }

            set
            {

                if ((value != null) && (Directory.Exists(value))) _CommonImageFolder = value;
            }

        }
        basePatient _patient = new basePatient();
        bool _Visible = true;
        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }

        Boolean _ImpressionEcheances;
        bool vSendToBasView = false;
        public Devis_TK _dtk = new Devis_TK();
        public FrmImpression(Devis_TK devis_tk, Boolean ImpressionEcheances,bool visible=true,bool sendToBasView=false)
        {
            vSendToBasView = sendToBasView;
            _Visible = visible;
            _ImpressionEcheances = ImpressionEcheances;
            _dtk = devis_tk;
            InitializeComponent();
            _patient = _dtk.patient;
            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR")

                switch (_dtk.Traitement.TypeScenario)
                {
                    case NewTraitement.typeScenario.Prothése:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-santeclaire.rdlc"; break;
                    case NewTraitement.typeScenario.Invisalign:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-invisalign.rdlc"; break;
                    case NewTraitement.typeScenario.RCC:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-RCC-CMUC.rdlc"; break;
                    case NewTraitement.typeScenario.Prothése_CMUC:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-CMUC.rdlc"; break;
                    case NewTraitement.typeScenario.Contention:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-contention.rdlc"; break;
                    case NewTraitement.typeScenario.Invisalign_Teen:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-invisalign.rdlc"; break;
                    case NewTraitement.typeScenario.RCC_CMUC:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-RCC-CMUC.rdlc"; break;
                    case NewTraitement.typeScenario.Invisalign_S3FULL:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-invisalign.rdlc"; break;
                    case NewTraitement.typeScenario.prothése_santéclair:
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK-santeclaire.rdlc"; break;

                }
            else
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK.rdlc"; 
            }
        }

        private void FrmImpression_Load(object sender, EventArgs e)
        {
            this.Visible = _Visible;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            reportViewer1.LocalReport.EnableExternalImages = true;

            ReportParameter numDevis = new ReportParameter("numDevis", _dtk.Id.ToString());
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { numDevis });
          

            ReportParameter nomPatient = new ReportParameter("nomPatient", _dtk.patient.Nom.ToString());
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { nomPatient });
            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR")
            {
               
                ReportParameter NumSS = new ReportParameter("NumSS", _dtk.patient.NumSecu.ToString());  
               reportViewer1.LocalReport.SetParameters(new ReportParameter[] { NumSS });
                if(_dtk.patient.Assurepar!=null)
                    if (_dtk.patient.Assurepar.patient == null)
                    {
                        _dtk.patient.Assurepar.patient = baseMgmtPatient.GetPatient(_dtk.patient.Assurepar.IdPatient);

                        ReportParameter NomAssure = new ReportParameter("NomAssure", _dtk.patient.Assurepar.patient == null ? "" : _dtk.patient.Assurepar.patient.ToString());
                        reportViewer1.LocalReport.SetParameters(new ReportParameter[] { NomAssure });
                    }
                    else
                    {
                        ReportParameter NomAssure = new ReportParameter("NomAssure","");
                        reportViewer1.LocalReport.SetParameters(new ReportParameter[] { NomAssure });
                    }
                ReportParameter DateNaissance = new ReportParameter("DateNaissance", _dtk.patient.DateNaissance.ToString("dd/MM/yyyy"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { DateNaissance });
            }
            string vImpressionEcheances;
            if (_ImpressionEcheances)
                vImpressionEcheances = "1";
            else
                vImpressionEcheances = "0";
            ReportParameter RImpressionEcheances = new ReportParameter("ImpressionEcheances", vImpressionEcheances);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { RImpressionEcheances });


            ReportParameter prenomPatient = new ReportParameter("prenomPatient", _dtk.patient.Prenom.ToString());
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { prenomPatient });

            double vmontantTarif = 0;
            double vmontantDepassement = 0;
            double vmontantRembMutuelle = 0;
            double vmontantPartPatient = 0;
            double vmontantLabo = 0;
            double vmontantBaseRemb = 0;
            double vmontantRemb = 0;
            double vmontantSterilisation = 0;
            double vmontantAchats = 0;
            double vmontantpropose = 0;
            double vmontant = 0;
            double vpoints = 0;
            string vTitre = "";
            if (_dtk.Titre == "")
                vTitre = " ";
            else
                vTitre = _dtk.Titre;
            ReportParameter titredevis = new ReportParameter("titredevis", vTitre);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { titredevis });


            ReportParameter civilite = new ReportParameter("civilite", _dtk.patient.Civilite);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { civilite });
            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "CH")
            {
                //hadhemi
                ReportParameter commentaire = new ReportParameter("commentaire", _dtk.Traitement.Traitement_commentaire.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { commentaire });
                ////afficher text de remise
                double remiseTotal = 0;
                remiseTotal = Math.Round((((Convert.ToDouble(_dtk.MontantAvantRemise) - Convert.ToDouble(_dtk.MontantDocteur)) / Convert.ToDouble(_dtk.MontantAvantRemise)) * 100));

                ReportParameter remise = new ReportParameter("remise", remiseTotal.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { remise });


                ReportParameter montantDevis = new ReportParameter("montantDevis", Convert.ToDouble(_dtk.MontantAvantRemise).ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantDevis });
            }




            foreach (CommTraitement cm in _dtk.actesTraitement)
            {
                if (cm.desactive) continue;
                    vmontantBaseRemb = vmontantBaseRemb + (cm.Acte.BaseRemboursement * Convert.ToInt32(cm.Acte.quantite));
                vmontantRemb = vmontantRemb + (cm.Acte.Remboursement * Convert.ToInt32(cm.Acte.quantite));
                vmontantDepassement = vmontantDepassement + (cm.Acte.Depassement * Convert.ToInt32(cm.Acte.quantite));
                vmontantRembMutuelle = vmontantRembMutuelle + (cm.RembMutuelle * Convert.ToInt32(cm.Acte.quantite));
                vmontantPartPatient = vmontantPartPatient + (cm.partPatient * Convert.ToInt32(cm.Acte.quantite));
                vmontantTarif = vmontantTarif + (cm.Acte.Tarif * Convert.ToInt32(cm.Acte.quantite));
                vmontant = vmontant + (cm.Acte.prix_acte * Convert.ToInt32(cm.Acte.quantite));
                vmontantpropose = vmontantpropose + (cm.Acte.prix_traitement * Convert.ToInt32(cm.Acte.quantite));
                vpoints = vpoints + (Convert.ToDouble(cm.Acte.nombre_points) * Convert.ToInt32(cm.Acte.quantite));

                foreach (CommActesTraitement ct in cm.ActesSupp)
                {
                    if (ct.desactive) continue;
                    if (ct.BaseRemboursement > 0)
                        vmontantBaseRemb = vmontantBaseRemb + (ct.BaseRemboursement * ct.Qte);
                    vmontantRemb = vmontantRemb + (ct.Remboursement * ct.Qte);
                    vmontantDepassement = vmontantDepassement + (ct.Depassement * ct.Qte);
                    vmontantRembMutuelle = vmontantRembMutuelle + (ct.RembMutuelle * ct.Qte);
                    vmontantPartPatient = vmontantPartPatient + (ct.partPatient * ct.Qte);
                    vmontantTarif = vmontantTarif + (ct.Tarif * ct.Qte);
                    vmontant = vmontant + (ct.prix_acte * ct.Qte);
                    vmontantpropose = vmontantpropose + (ct.prix_traitement * ct.Qte);
                    vpoints = vpoints + (Convert.ToDouble(ct.nombre_points) * ct.Qte);
                }
                foreach (CommActesTraitement ct in cm.Radios)
                {
                    if (ct.desactive) continue;
                    if (ct.BaseRemboursement > 0)
                        vmontantBaseRemb = vmontantBaseRemb + (ct.BaseRemboursement * ct.Qte);
                    vmontantRemb = vmontantRemb + (ct.Remboursement * ct.Qte);
                    vmontantDepassement = vmontantDepassement + (ct.Depassement * ct.Qte);
                    vmontantRembMutuelle = vmontantRembMutuelle + (ct.RembMutuelle * ct.Qte);
                    vmontantPartPatient = vmontantPartPatient + (ct.partPatient * ct.Qte);
                    vmontantTarif = vmontantTarif + (ct.Tarif * ct.Qte);
                    vmontant = vmontant + (ct.prix_acte * ct.Qte);
                    vmontantpropose = vmontantpropose + (ct.prix_traitement * ct.Qte);
                    vpoints = vpoints + (Convert.ToDouble(ct.nombre_points) * ct.Qte);
                }
                foreach (CommActesTraitement ct in cm.photos)
                {
                    if (ct.desactive) continue;
                    if (ct.BaseRemboursement > 0)
                        vmontantBaseRemb = vmontantBaseRemb + (ct.BaseRemboursement * ct.Qte);
                    vmontantRemb = vmontantRemb + (ct.Remboursement * ct.Qte);
                    vmontantDepassement = vmontantDepassement + (ct.Depassement * ct.Qte);
                    vmontantRembMutuelle = vmontantRembMutuelle + (ct.RembMutuelle * ct.Qte);
                    vmontantPartPatient = vmontantPartPatient + (ct.partPatient * ct.Qte);
                    vmontant = vmontant + (ct.prix_acte * ct.Qte);
                    vmontantTarif = vmontantTarif + (ct.Tarif * ct.Qte) ;
                    vmontantpropose = vmontantpropose + (ct.prix_traitement * ct.Qte);
                    vpoints = vpoints + (Convert.ToDouble(ct.nombre_points) * ct.Qte);
                }
                foreach (CommMaterielTraitement mt in cm.Materiels)
                {
                    if (mt.desactive) continue;
                    if (mt.Famille != null)
                    {
                        if (mt.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 && mt.ShortLib != "STE")
                            vmontantLabo = vmontantLabo + (mt.prix_traitement * mt.Qte);
                        if (mt.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 && mt.ShortLib == "STE")
                            vmontantSterilisation = vmontantSterilisation + (mt.prix_traitement * mt.Qte);
                        if (mt.Famille.libelle.ToLower().IndexOf("achats") >= 0)
                            vmontantAchats = vmontantAchats + (mt.prix_traitement * mt.Qte);
                    }
                }


            }
            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR")
            {
                if (_dtk.Traitement.TypeScenario != NewTraitement.typeScenario.Contention && _dtk.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
                {
                    ReportParameter typeDevis = new ReportParameter("typeDevis", Convert.ToString(_dtk.Traitement.TypeScenario));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { typeDevis });
                }
                if (_dtk.Traitement.TypeScenario == NewTraitement.typeScenario.Prothése_CMUC || _dtk.Traitement.TypeScenario == NewTraitement.typeScenario.prothése_santéclair || _dtk.Traitement.TypeScenario == NewTraitement.typeScenario.Prothése)
                {
                    ReportParameter localisationAnatomique = new ReportParameter("localisationAnatomique", Convert.ToString(_dtk.localisationAnatomiuque));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { localisationAnatomique });
                }

                ReportParameter totalMutuelle = new ReportParameter("totalMutuelle", vmontantRembMutuelle == 0 ? "" : vmontantRembMutuelle.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalMutuelle });

                ReportParameter totalPatient = new ReportParameter("totalPatient", vmontantPartPatient == 0 ? "" : vmontantPartPatient.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalPatient });

                ReportParameter totalDepassement = new ReportParameter("totalDepassement", vmontantDepassement == 0 ? "" : vmontantDepassement.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalDepassement });

                if (_dtk.Traitement.TypeScenario == NewTraitement.typeScenario.Prothése_CMUC)
                {
                    ReportParameter totalTarif = new ReportParameter("totalTarif", vmontantTarif == 0 ? "" : vmontantTarif.ToString("C2"));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalTarif });
                }
                ReportParameter montantRemb = new ReportParameter("montantRemb", vmontantRemb == 0 ? "" : vmontantRemb.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantRemb });

                ReportParameter montantBaseRemb = new ReportParameter("montantBaseRemb", vmontantBaseRemb == 0 ? "" : vmontantBaseRemb.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantBaseRemb });
            }
            else
            {
                ReportParameter montantAchats = new ReportParameter("montantAchats", vmontantAchats.ToString("C2"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantAchats });
            }
            ReportParameter montantlabo = new ReportParameter("montantlabo", vmontantLabo.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantlabo });

            ReportParameter montantsterilisation = new ReportParameter("montantsterilisation", vmontantSterilisation.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantsterilisation });
          
         

            string montantaffiche = "";


            ReportParameter montant = new ReportParameter("montant", vmontant.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montant });



            ReportParameter montantpropose = new ReportParameter("montantpropose", vmontantpropose.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { montantpropose });



            double vtotalmontant = 0;
            double vtotalmontantpropose = 0;


            vtotalmontantpropose = vmontantpropose + vmontantLabo + vmontantSterilisation;
            vtotalmontant = vmontant + vmontantLabo + vmontantSterilisation;


            ReportParameter totalmontantpropose = new ReportParameter("totalmontantpropose", vtotalmontantpropose.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalmontantpropose });


            ReportParameter totalmontant = new ReportParameter("totalmontant", vtotalmontant.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalmontant });
            double vMontantDocteur = 0;
            if (_dtk.MontantDocteur != null)
                vMontantDocteur = (double)_dtk.MontantDocteur;
            ReportParameter totalmontantDocteur = new ReportParameter("totalmontantDocteur", vMontantDocteur.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalmontantDocteur });

            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "CH")
            {

                ReportParameter nbrePoints = new ReportParameter("nbrePoints", _dtk.actesTraitement[0].Acte.cotation.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { nbrePoints });

                ReportParameter totalpoints = new ReportParameter("totalpoints", vpoints.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalpoints });
            }
            else
            {
                ReportParameter nbrePoints = new ReportParameter("nbrePoints", 0.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { nbrePoints });

                ReportParameter totalpoints = new ReportParameter("totalpoints", 0.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalpoints });
            }
            double vTotalAffiche = 0;

            if (vMontantDocteur == 0)
            {
                if (vtotalmontantpropose != vtotalmontant)
                    vTotalAffiche = vtotalmontantpropose;
                else
                    vTotalAffiche = vtotalmontant;
            }
            else
                vTotalAffiche = vMontantDocteur;


            ReportParameter totalaffiche = new ReportParameter("totalaffiche", vTotalAffiche.ToString("C2"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { totalaffiche });

            ReportParameter nomPraticien = new ReportParameter("nomPraticien", _dtk.patient.infoscomplementaire.PraticienResponsable.Nom);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { nomPraticien });
            ReportParameter prenomPraticien = new ReportParameter("prenomPraticien", _dtk.patient.infoscomplementaire.PraticienResponsable.Prenom);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { prenomPraticien });
            ReportParameter localisation = new ReportParameter("localisation", BasCommon_BL.InfoCabinetMgmt.informationsCabinet.VilleCabinet);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { localisation });
            ReportParameter statue = new ReportParameter("statue", _dtk.patient.infoscomplementaire.PraticienResponsable.Civilite);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { statue });
            string FilePath = @"file:" + CommonImageFolder + _dtk.patient.infoscomplementaire.PraticienResponsable.Nom + ".bmp";
            // FilePath  = CommonImageFolder + _dtk.patient .infoscomplementaire.PraticienResponsable.Nom + ".jpg";
            ReportParameter ImgPath = new ReportParameter("ImgPath", FilePath);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { ImgPath });
            string FilePiedPath = @"file:" + CommonImageFolder + "Pied" + _dtk.patient.infoscomplementaire.PraticienResponsable.Nom + ".bmp";
            //   FilePiedPath = CommonImageFolder + "Pied" + _dtk.patient.infoscomplementaire.PraticienResponsable.Nom + ".jpg";
            ReportParameter ImgPied = new ReportParameter("ImgPied", FilePiedPath);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { ImgPied });



            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(getrds(_dtk.Id));
            this.reportViewer1.LocalReport.DataSources.Add(getrdsec(_dtk.Id));
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();

            /////////envoie devis vers basphoto
            if (vSendToBasView)
            {
                try
                {
                    string path = ConfiguredPath + "/" + _dtk.patient.Id.ToString();

                    string filename = System.IO.Path.GetTempFileName();

                    System.IO.FileInfo nfo = new System.IO.FileInfo(filename);
                    string pdfName = nfo.Name.Replace(".tmp", ".pdf");
                    byte[] bytes = reportViewer1.LocalReport.Render("PDF");
                    string absolutePath = ImagesMgmt.CreatePatientDossier(path);
                    BasCommon_BL.ImagesMgmt.saveImage(bytes, pdfName, absolutePath);
                    //System.IO.FileInfo destfilenfo = new System.IO.FileInfo(GetUniqueFileName("D:\\" + pdfName));

                    //using (FileStream fs = new FileStream(path + "\\" + pdfName, FileMode.Create))
                    //{
                    //    fs.Write(bytes, 0, bytes.Length);
                    //}

                    bytes = reportViewer1.LocalReport.Render("Image");


                    BasCommon_BL.ImagesMgmt.saveToBasView(bytes, _dtk.patient.Id, pdfName, path + "/" + pdfName, absolutePath);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void CreateEmailMessage(string strToIds, string strSubject, string strMessage, string[] arrAttachPaths)
        {

            try
            {

                // System.Diagnostics.Process.Start(@"mailto:lombard.flavien@free.fr?attachment=""c:\test.jpg""");



                Outlook.Application oApp = GetApplicationObject();




                Outlook.MailItem mailItem = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
                mailItem.Subject = strSubject;
                mailItem.To = strToIds;
                mailItem.Body = string.IsNullOrEmpty(strMessage) ? " " : strMessage;
                mailItem.Importance = Outlook.OlImportance.olImportanceLow;

                foreach (string a in arrAttachPaths)
                {

                    FileInfo fi = new FileInfo(a);

                    if (fi.Exists)
                    {
                        mailItem.Attachments.Add(fi.FullName);
                    }
                }


                mailItem.Display(false);


            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == unchecked((int)(0x80040154)))
                    MessageBox.Show("Outlook n'est pas installé");
                else
                    MessageBox.Show("Outlook Erreur : " + ex.ErrorCode.ToString("X"));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        Outlook.Application GetApplicationObject()
        {

            Outlook.Application application = null;


            Process[] prs = Process.GetProcessesByName("OUTLOOK");

            // Check whether there is an Outlook process running.
            if (prs.Count() > 0)
            {

                // If so, use the GetActiveObject method to obtain the process and cast it to an Application object.
                application = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            else
            {

                // If not, create a new instance of Outlook and log on to the default profile.
                application = new Outlook.Application();
                Outlook.NameSpace nameSpace = application.GetNamespace("MAPI");
                nameSpace.Logon("", "", Missing.Value, Missing.Value);
                nameSpace = null;
            }

            // Return the Outlook Application object.
            return application;
        }
        public static string connectionString = "";
        private static bool ArchiveMode = false;
        public static void getConnection()
        {
            //    If the connection string is null, use a default.


            try
            {
                if (connectionString == "")
                {
                    MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();


                    if (ArchiveMode)
                    {
                        cs.Server = ConfigurationManager.AppSettings["ArchiveDataSource"];
                        cs.Database = ConfigurationManager.AppSettings["ArchiveDatabase"];
                        cs.UserID = ConfigurationManager.AppSettings["ArchiveUserID"];
                        cs.Password = ConfigurationManager.AppSettings["ArchivePassword"];
                      //  cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["ArchiveDialect"]);
                      //  cs.ServerType = (FbServerType)Enum.Parse(typeof(FbServerType), ConfigurationManager.AppSettings["ServerType"]);

                    }
                    else
                    {
                        cs.Server = ConfigurationManager.AppSettings["DataSource"];
                        cs.Database = ConfigurationManager.AppSettings["Database"];
                        cs.UserID = ConfigurationManager.AppSettings["UserID"];
                        cs.Password = ConfigurationManager.AppSettings["Password"];
                     //   cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["Dialect"]);
                      //  cs.ServerType = (FbServerType)Enum.Parse(typeof(FbServerType), ConfigurationManager.AppSettings["ServerType"]);

                    }
                    connectionString = cs.ToString();



                }
#if TRACE
                Console.WriteLine("connectionString : " + connectionString);
#endif

                connection = new MySqlConnection(connectionString);
            }
            catch (System.Exception ex)
            {
#if TRACE
                Console.WriteLine("connectionString Error : " + ex.ToString());
#endif
                throw ex;
            }
        }
        public ReportDataSource getrdsOLD(int iddevis)
        {
            DataSetTK dstk = new DataSetTK();

            bool verif = false;
            if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR")
            {
                verif = true;
            }

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            /*  string selectQuery = "SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT.ACTE_LIBELLE AS TRAITEMENT, a.ID_ACTE, ACT.ACTE_LIBELLE,a.MONTANTAVANTPROPOSITION,a.MONTANT";
              selectQuery += " FROM            DEVIS_COMMENTS a, actes ACT";
              selectQuery += " WHERE        (a.ID_DEVIS = @idDevis) AND (ACT.id_acte = a.ID_ACTE)";
              selectQuery += " UNION";
              selectQuery += " SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT.ACTE_LIBELLE AS TRAITEMENT, BDM.ID_MATERIEL, MAT.MATERIEL_LIBELLE, BDM.MONTANT,  ";
              selectQuery += "                 BDM.MONTANTAVANTREMISE";
              selectQuery += " FROM            DEVIS_COMMENTS a, BASE_DEVIS_MAT BDM, MATERIELS MAT, actes ACT";
              selectQuery += " WHERE        a.ID = BDM.ID AND BDM.ID_MATERIEL = MAT.ID_MATERIEL AND (a.ID_DEVIS = @idDevis) AND (BDM.ID IN";
              selectQuery += "                             (SELECT        ID";
              selectQuery += "                               FROM            DEVIS_COMMENTS a";
              selectQuery += "                               WHERE        (ID_DEVIS = @idDevis))) AND (ACT.id_acte = a.ID_ACTE)";
              selectQuery += " UNION";
              selectQuery += " SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT2.ACTE_LIBELLE AS TRAITEMENT, a.ID_ACTE, ACT.acte_libelle, a.MONTANTAVANTREMISE, a.MONTANT";
              selectQuery += " FROM            BASE_DEVIS_ACTES_TK a, DEVIS_COMMENTS DC, actes ACT, actes ACT2";
              selectQuery += " WHERE        a.ID = DC.ID AND a.ID = DC.ID AND (a.ID IN";
              selectQuery += "                            (SELECT        ID";
              selectQuery += "                             FROM            DEVIS_COMMENTS a";
              selectQuery += "                             WHERE        (ID_DEVIS = @idDevis))) AND (ACT.id_acte = a.ID_ACTE) AND (ACT2.id_acte = DC.ID_ACTE)";*/


            string selectquery = "select nomenclature, sum(quantite) as quantite,id_acte,trim(acte_libelle_estimation) as acte_libelle_estimation,valeur, ";
            selectquery += "sum(points) as points,sum(montantavantremise)as montantavantremise,sum(montantpropose)as montantpropose,sum(acte_base_remboursement) as acte_base_remboursement ,sum(acte_remboursement) as acte_remboursement,sum(acte_depassement) as acte_depassement,acte_code_transposotion,sum(rembmutuelle) as rembmutuelle  ,sum(partpatient) as partpatient,trim(acte_libelle) as acte_libelle,sum(tarif) as tarif";
            if (verif)
                selectquery += ",dte";
            selectquery += " from ";
            selectquery += "( ";
            selectquery += "select coalesce(a.nomenclature,'') as nomenclature ,coalesce ( d.qte,0) as quantite, d.id_acte, a.acte_libelle_estimation , ";
            selectquery += "coalesce(a.cotation,'') as valeur, (coalesce ( a.nombre_points,0) * d.qte) as points, (d.montantavantproposition * d.qte) as montantavantremise, ";
            selectquery += "(d.montant * d.qte) as montantpropose,(a.acte_base_remboursement * d.qte) as acte_base_remboursement,(a.acte_remboursement  * d.qte) as acte_remboursement,(a.acte_depassement  * d.qte) as acte_depassement,a.acte_code_transposotion as acte_code_transposotion,(d.rembmutuelle * d.qte) as rembmutuelle,(d.partpatient * d.qte) as partpatient,trim(acte_libelle) as acte_libelle,(a.tarif * d.qte) as tarif";
            selectquery += ", d.date_comm as dte";
            selectquery += " from devis_comments d   left join actes a on a.id_acte = d.id_acte where d.id_devis = @iddevis and   d.montant > 0 and d.desactive=@false ";
            selectquery += "union all ";
            selectquery += "select coalesce(ac.nomenclature,'') , ";
            selectquery += "coalesce ( a.qte,0) as quantite, a.id_acte, ac.acte_libelle_estimation as libelle , coalesce(ac.cotation,'') as valeur, ";
            selectquery += "(coalesce ( ac.nombre_points,0) * a.qte) as points, (a.montantavantremise * a.qte), (a.montant * a.qte) as montantpropose, (ac.acte_base_remboursement  * a.qte) as acte_base_remboursement,(ac.acte_remboursement * a.qte) as acte_remboursement,(ac.acte_depassement * a.qte) as acte_depassement,ac.acte_code_transposotion as acte_code_transposotion,(a.rembmutuelle * a.qte) as rembmutuelle,(a.partpatient * a.qte) as partpatient,trim(acte_libelle) as acte_libelle ,(ac.tarif * a.qte) as tarif";
            selectquery += ",a.date_execution as dte";
            selectquery += " from  base_devis_actes_tk a  ";
            selectquery += "join devis_comments dc on dc.id = a.id and dc.id_devis = @iddevis and dc.desactive=@false and a.desactive=@false ";
            selectquery += "left join actes ac on ac.id_acte = a.id_acte where   a.montant > 0 ";

            selectquery += " union all ";
            selectquery += "select coalesce(ac.nomenclature,'') , coalesce ( a.qte,0) as quantite, a.id_materiel as id_acte, ac.materiel_libelle_estimation as libelle , ";
            selectquery += "coalesce(ac.cotation,'') as valeur, 0 as points, (a.montantavantremise * a.qte), (a.montant * a.qte) as ";
            selectquery += "montantpropose, (ac.acte_base_remboursement  * a.qte) as acte_base_remboursement,(ac.acte_remboursement * a.qte) as acte_remboursement, ";
            selectquery += "(ac.acte_depassement * a.qte) as acte_depassement,ac.acte_code_transposotion as acte_code_transposotion,(a.rembmutuelle * a.qte) as rembmutuelle, ";
            selectquery += "(a.partpatient * a.qte) as partpatient,trim(materiel_libelle) as acte_libelle ,1 as tarif ,''  as dte from  base_devis_mat a  ";
            selectquery += "join devis_comments dc on dc.id = a.id and dc.id_devis = @iddevis and (dc.desactive=@false or dc.desactive is null) and (a.desactive=@false or a.desactive is null) ";
            selectquery += "left join materiels ac on ac.id_materiel = a.id_materiel ";
            selectquery += "where   a.montant > 0 and  ac.isfacture = 1";
            selectquery += ") as t1 ";
            if (verif)
            {
                selectquery += "group by dte,nomenclature, id_acte,acte_libelle_estimation,valeur ,acte_code_transposotion,acte_libelle";
                selectquery += "order by nomenclature DESC";

            }

            else
            {
                selectquery += "group by nomenclature, id_acte,acte_libelle_estimation,valeur ,acte_code_transposotion,acte_libelle";
                selectquery += "order by nomenclature DESC";
            }
                

            /*
string selectQuery = "select coalesce(a.NOMENCLATURE,'') AS NOMENCLATURE ,SUM(coalesce ( a.QUANTITE,0)) AS QUANTITE, d.ID_ACTE, a.ACTE_LIBELLE_ESTIMATION";
selectQuery += " , coalesce(a.COTATION,'') AS VALEUR, SUM(coalesce ( a.NOMBRE_POINTS,0)) AS POINTS, SUM(d.MONTANTAVANTPROPOSITION) AS MONTANTAVANTREMISE,SUM(d.MONTANT) AS MONTANTPROPOSE";
selectQuery += " FROM DEVIS_COMMENTS d ";
selectQuery += "  LEFT JOIN ACTES a on a.ID_ACTE = d.ID_ACTE";
selectQuery += " where d.ID_DEVIS = @idDevis";
selectQuery += " GROUP BY";
selectQuery += " a.NOMENCLATURE , d.ID_ACTE, a.ACTE_LIBELLE_ESTIMATION";
selectQuery += " , a.COTATION";
selectQuery += " union";
selectQuery += " select coalesce(ac.NOMENCLATURE,'') ,SUM(coalesce ( ac.QUANTITE,0)) AS QUANTITE, a.ID_ACTE, ac.ACTE_LIBELLE_ESTIMATION as LIBELLE";
selectQuery += " , coalesce(ac.COTATION,'') AS VALEUR, SUM(coalesce ( ac.NOMBRE_POINTS,0)) AS POINTS, SUM(a.MONTANTAVANTREMISE), SUM(a.MONTANT)";
selectQuery += " FROM  BASE_DEVIS_ACTES_TK a";
selectQuery += " JOIN DEVIS_COMMENTS DC ON DC.ID = a.ID and DC.ID_DEVIS = @idDevis";
selectQuery += " LEFT JOIN ACTES ac on ac.ID_ACTE = a.ID_ACTE";
selectQuery += " GROUP BY  ac.NOMENCLATURE , a.ID_ACTE, ac.ACTE_LIBELLE_ESTIMATION ";
selectQuery += " , ac.COTATION ";
    */


 
            MySqlCommand command = new MySqlCommand(selectquery, connection);
            command.Parameters.AddWithValue("@idDevis", iddevis);
            command.Parameters.AddWithValue("@false", "False");
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            dstk.EnforceConstraints = false;
            da.Fill(dstk, dstk.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("DataTableTraitement", dstk.Tables[0]);
            connection.Close();
            return rds;

        }
        public static string PathRest
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["PathRest" + prefix];

            }

        }
        public static string token
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["token" + prefix];

            }
        }
        public ReportDataSource getrds(int iddevis)
        {
            DataSetTK dstk = new DataSetTK();       
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            client.Headers.Add("Authorization", "bearer " + token);
            var response = client.DownloadString(PathRest+"/Devis/getrds/"+iddevis);
            JArray j = JArray.Parse(response.ToString());
            System.Data.DataTable dt = JsonConvert.DeserializeObject<System.Data.DataTable>(j.ToString());
          //  DataSet dts = JsonConvert.DeserializeObject<DataSet>(j.ToString());
            dt.TableName = "DataTableTraitement";
            dstk.EnforceConstraints = false;
            dstk.Tables.Remove("DataTableTraitement");
            dstk.Tables.Add(dt);

            ReportDataSource rds = new ReportDataSource("DataTableTraitement", dstk.Tables[dstk.Tables.Count -1]);
            return rds;

        }
        public ReportDataSource getrdsec(int iddevis)
        {
            DataSetTK dstk = new DataSetTK();
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            client.Headers.Add("Authorization", "bearer " + token);
            var response = client.DownloadString(PathRest + "/Devis/getrdsec/" + iddevis);
            JArray j = JArray.Parse(response.ToString());
            System.Data.DataTable dt = JsonConvert.DeserializeObject<System.Data.DataTable>(j.ToString());
            //  DataSet dts = JsonConvert.DeserializeObject<DataSet>(j.ToString());
            dt.TableName = "DataTableEcheance";
            dstk.EnforceConstraints = false;
            dstk.Tables.Remove("DataTableEcheance");
            dstk.Tables.Add(dt);

            ReportDataSource rds = new ReportDataSource("DataTableEcheance", dt);
            return rds;

        }
        public ReportDataSource getrdsecOLD(int iddevis)
        {
            DataSetTK dstk = new DataSetTK();
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            /* string selectQuery = "SELECT        ID, MONTANT, DTEECHEANCE, LIBELLE, ID_DEVIS_COMMENT ";
              selectQuery += "FROM            BAS_ECHEANCES_DEVIS  ";
              selectQuery += "WHERE        (ID_DEVIS_COMMENT IN";
              selectQuery += "         (SELECT        b.ID";
              selectQuery += "           FROM            DEVIS_COMMENTS b";
              selectQuery += "          WHERE        (b.ID_DEVIS = @idDevis)))";*/

            string selectQuery = "SELECT sum(montant) MONTANT,cast(DTEECHEANCE as date) as DTEECHEANCE , trim(LIBELLE) as LIBELLE ";
            selectQuery += "FROM bas_echeances_devis  ";
            selectQuery += "WHERE (ID_DEVIS_COMMENT IN  ";
            selectQuery += "(SELECT b.ID  FROM  ";
            selectQuery += "devis_comments b  WHERE DESACTIVE=@false and ";
            selectQuery += "(b.ID_DEVIS =  @idDevis))) ";
            selectQuery += "group by DTEECHEANCE, LIBELLE ";

            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@idDevis", iddevis);
            command.Parameters.AddWithValue("@false", "False");
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            dstk.EnforceConstraints = false;
            da.Fill(dstk, dstk.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("DataTableEcheance", dstk.Tables[0]);
            connection.Close();
            return rds;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public static string GetUniqueFileName(string filePathAndName)
        {
            string newFilename = filePathAndName;
            int nr = 1;
            string path = Path.GetDirectoryName(filePathAndName);
            string ext = Path.GetExtension(filePathAndName);
            string fname = Path.GetFileNameWithoutExtension(filePathAndName);


            while (File.Exists(newFilename))
            {
                newFilename = Path.Combine(path, fname + "[" + nr + "]" + ext);
                nr++;
            }


            return newFilename;
        }

        public static string ConfiguredPath
        {
            get
            {
                return ConfigurationSettings.AppSettings["PHOTO_FOLDER_PATH" + prefix];
            }
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
           

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string path = ConfiguredPath + "\\" + BasCommon_BL.ImagesMgmt.FindRepertoirePatient(_dtk.patient.Id);

            //string filename = System.IO.Path.GetTempFileName();

            //System.IO.FileInfo nfo = new System.IO.FileInfo(filename);
            //string pdfName = nfo.Name.Replace(".tmp", ".pdf");
            //byte[] bytes = reportViewer1.LocalReport.Render("PDF");

            //if (!System.IO.Directory.Exists(path))
            //    System.IO.Directory.CreateDirectory(path);

            //System.IO.FileInfo destfilenfo = new System.IO.FileInfo(GetUniqueFileName(path + "\\" + pdfName));

            //using (FileStream fs = new FileStream(path + "\\" + pdfName, FileMode.Create))
            //{
            //    fs.Write(bytes, 0, bytes.Length);
            //}

            //bytes = reportViewer1.LocalReport.Render("Image");


            //BasCommon_BL.ImagesMgmt.saveToBasView(bytes, _dtk.patient.Id, pdfName, destfilenfo.ToString(), path);
            //MessageBox.Show("Le devis à été ajouté !");
        }

        private void reportViewer1_Print(object sender, ReportPrintEventArgs e)
        {
         /*   FrmWizardCourrier frm = new FrmWizardCourrier(CurrentPatient);
            if (frm.ShowDialog() == DialogResult.Cancel) return;

            opencourrier(frm.FileName, frm.lstCorrespondant, frm.Praticien, frm.DirectPrint);*/



            
        }

        private void reportViewer1_PrintingBegin(object sender, ReportPrintEventArgs e)
        {

            if (_dtk.Traitement.contention)
            {
                if (_patient.contacts == null)
                    BasCommon_BL.baseMgmtPatient.FillContacts(_patient);

                Correspondant _SelectedDentiste = null;
                Correspondant _SelectedPraticien = null;




                _SelectedDentiste = _patient.Dentiste == null ? null : _patient.Dentiste.correspondant;
                if (_SelectedDentiste != null)
                    if (_SelectedDentiste.contacts == null)
                        BasCommon_BL.MgmtCorrespondants.FillContacts(_SelectedDentiste);


                if (_patient.infoscomplementaire.PraticienResponsable != null) _SelectedPraticien = MgmtCorrespondants.getCorrespondant(_patient.infoscomplementaire.PraticienResponsable.Id);




                Correspondant _SelectedCorrespondant = MgmtCorrespondants.getCorrespondant(_patient.Id);
                if (_SelectedDentiste != null)
                    if (_SelectedDentiste.contacts == null)
                        BasCommon_BL.MgmtCorrespondants.FillContacts(_SelectedCorrespondant);

                string modelecourrier = CommonImageFolder +  ConfigurationSettings.AppSettings["ContentionScenario"];



                OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _patient.Id.ToString());
                OLEAccess.BASLetter.AddAttribut("TitrePatient", _patient.Civilite);
                OLEAccess.BASLetter.AddAttribut("NomPatient", _patient.Nom);
                OLEAccess.BASLetter.AddAttribut("IdPatient", _patient.Id.ToString());
                OLEAccess.BASLetter.AddAttribut("PrenomPatient", _patient.Prenom);
                OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _patient.MainAdresse == null ? "" : _patient.MainAdresse.adresse.Adr1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _patient.MainAdresse == null ? "" : _patient.MainAdresse.adresse.Adr2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _patient.MainAdresse == null ? "" : _patient.MainAdresse.adresse.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePatient", _patient.MainAdresse == null ? "" : _patient.MainAdresse.adresse.Ville);
                OLEAccess.BASLetter.AddAttribut("AgePatient", _patient.Age());
                OLEAccess.BASLetter.AddAttribut("GenrePatient", _patient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TutoiementPatient", _patient.Tutoiement ? "TU" : "VOUS");
                OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _patient.DateNaissance.ToShortDateString());
                OLEAccess.BASLetter.AddAttribut("NumSecu", _patient.NumSecu.ToString());
                OLEAccess.BASLetter.AddAttribut("NumSecu", _patient.NumSecu.ToString());
                OLEAccess.BASLetter.AddAttribut("ProchainRDV", _patient.NextRDV == null ? "" : _patient.NextRDV.StartDate.ToString());
                OLEAccess.BASLetter.AddAttribut("NumDossier", _patient.Dossier.ToString());
                OLEAccess.BASLetter.AddAttribut("IOlogin", _patient.IOlogin.ToString());
                OLEAccess.BASLetter.AddAttribut("IOPassword", _patient.password.ToString());



                string diag = _patient.Diagnostic;

                OLEAccess.BASLetter.AddAttribut("Diagnostique", _patient == null ? "" : diag);

                string obj = _patient.Objectif;

                OLEAccess.BASLetter.AddAttribut("Objectif", _patient == null ? "" : obj);

                string trai = _patient.Traitement;



                OLEAccess.BASLetter.AddAttribut("PlanDeTraitement", _patient == null ? "" : trai);

                Echeance FirstEchARegler = EcheancesMgmt.GetFirstEcheanceARegler(_patient);
                OLEAccess.BASLetter.AddAttribut("DateSoldePatient", FirstEchARegler == null ? "" : FirstEchARegler.DateEcheance.ToShortDateString());
                OLEAccess.BASLetter.AddAttribut("Solde", EcheancesMgmt.GetSolde(_patient).ToString());
                OLEAccess.BASLetter.AddAttribut("DateDernierRdv", _patient.LastRDV == null ? "" : _patient.LastRDV.StartDate.ToString());
                OLEAccess.BASLetter.AddAttribut("DateDuJour", DateTime.Now.ToShortDateString());
                OLEAccess.BASLetter.AddAttribut("ID_CORRESPONDANT", _SelectedCorrespondant.Id.ToString());
                OLEAccess.BASLetter.AddAttribut("TitreCorrespondant", _SelectedCorrespondant.Titre);
                OLEAccess.BASLetter.AddAttribut("NomCorrespondant", _SelectedCorrespondant.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomCorrespondant", _SelectedCorrespondant.Prenom);
                OLEAccess.BASLetter.AddAttribut("MailCorrespondant", _SelectedCorrespondant.MainMail == null ? "" : _SelectedCorrespondant.MainMail.Value);
                OLEAccess.BASLetter.AddAttribut("ProfessionCorrespondant", _SelectedCorrespondant.Profession);
                OLEAccess.BASLetter.AddAttribut("AutreProfessionCorrespondant", _SelectedCorrespondant.AutreProfession);
                OLEAccess.BASLetter.AddAttribut("TelFixeCorrespondant", _SelectedCorrespondant.MainTel == null ? "" : _SelectedCorrespondant.MainTel.Value);
                OLEAccess.BASLetter.AddAttribut("FaxCorrespondant", _SelectedCorrespondant.MainFax == null ? "" : _SelectedCorrespondant.MainFax.Value);
                OLEAccess.BASLetter.AddAttribut("TelProCorrespondant", _SelectedCorrespondant.MainTel == null ? "" : _SelectedCorrespondant.MainTel.Value);
                OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", _SelectedCorrespondant.MainAdresse == null ? "" : _SelectedCorrespondant.MainAdresse.adresse.Adr1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", _SelectedCorrespondant.MainAdresse == null ? "" : _SelectedCorrespondant.MainAdresse.adresse.Adr2);
                OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", _SelectedCorrespondant.MainAdresse == null ? "" : _SelectedCorrespondant.MainAdresse.adresse.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", _SelectedCorrespondant.MainAdresse == null ? "" : _SelectedCorrespondant.MainAdresse.adresse.Ville);
                OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", _SelectedCorrespondant.GenreFeminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", _SelectedCorrespondant.TuToiement ? "TU" : "VOUS");

                if (_patient.ResponsableFinancier != null)
                {
                    Correspondant respfi = MgmtCorrespondants.getCorrespondant(_patient.ResponsableFinancier.IdCorrespondance);
                    OLEAccess.BASLetter.AddAttribut("TitreResponsableFi", respfi.Titre);
                    OLEAccess.BASLetter.AddAttribut("NomResponsableFi", respfi.Nom);
                    OLEAccess.BASLetter.AddAttribut("PrenomResponsableFi", respfi.Prenom);
                    OLEAccess.BASLetter.AddAttribut("MailResponsableFi", respfi.MainMail == null ? "" : respfi.MainMail.Value);
                    OLEAccess.BASLetter.AddAttribut("ProfessionResponsableFi", respfi.Profession);
                    OLEAccess.BASLetter.AddAttribut("AutreProfessionResponsableFi", respfi.AutreProfession);
                    OLEAccess.BASLetter.AddAttribut("TelFixeResponsableFi", respfi.MainTel == null ? "" : respfi.MainTel.Value);
                    OLEAccess.BASLetter.AddAttribut("FaxResponsableFi", respfi.MainFax == null ? "" : respfi.MainFax.Value);
                    OLEAccess.BASLetter.AddAttribut("TelProResponsableFi", respfi.MainTel == null ? "" : respfi.MainTel.Value);
                    OLEAccess.BASLetter.AddAttribut("Adresse1ResponsableFi", respfi.MainAdresse == null ? "" : respfi.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("Adresse2ResponsableFi", respfi.MainAdresse == null ? "" : respfi.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalResponsableFi", respfi.MainAdresse == null ? "" : respfi.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VilleResponsableFi", respfi.MainAdresse == null ? "" : respfi.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("GenreResponsableFi", respfi.GenreFeminin ? "F" : "M");
                    OLEAccess.BASLetter.AddAttribut("TutoiementResponsableFi", respfi.TuToiement ? "TU" : "VOUS");
                }


                if (_SelectedDentiste != null)
                {
                    OLEAccess.BASLetter.AddAttribut("ID_Dentiste", _SelectedDentiste.Id.ToString());
                    OLEAccess.BASLetter.AddAttribut("NomDentiste", _SelectedDentiste.Nom);
                    OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _SelectedDentiste.Prenom);
                    OLEAccess.BASLetter.AddAttribut("GenreDentiste", _SelectedDentiste.GenreFeminin ? "F" : "M");
                    OLEAccess.BASLetter.AddAttribut("TitreDentiste", _SelectedDentiste.Titre);
                    OLEAccess.BASLetter.AddAttribut("AdresseProfDentiste", _SelectedDentiste.MainAdresse == null ? "" : _SelectedDentiste.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("AdresseComplProfDentiste", _SelectedDentiste.MainAdresse == null ? "" : _SelectedDentiste.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _SelectedDentiste.MainAdresse == null ? "" : _SelectedDentiste.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VilleDentiste", _SelectedDentiste.MainAdresse == null ? "" : _SelectedDentiste.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", _SelectedDentiste.TuToiement ? "TU" : "VOUS");
                    OLEAccess.BASLetter.AddAttribut("TelPersonnelDentiste", _SelectedDentiste.MainTel == null ? "" : _SelectedDentiste.MainTel.Value);
                    OLEAccess.BASLetter.AddAttribut("TelProDentiste", _SelectedDentiste.MainTel == null ? "" : _SelectedDentiste.MainTel.Value);
                }
                else
                {
                    OLEAccess.BASLetter.AddAttribut("NomDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("PrenomDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");
                    OLEAccess.BASLetter.AddAttribut("TitreDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("AdresseProfDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("AdresseComplProfDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("VilleDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");
                    OLEAccess.BASLetter.AddAttribut("TelPersonnelDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("TelProDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("InternetIdentifiantDentiste", "");
                    OLEAccess.BASLetter.AddAttribut("InternetMdpDentiste", "");
                }

                if (_SelectedPraticien != null)
                {
                    OLEAccess.BASLetter.AddAttribut("ID_Praticien", _SelectedPraticien.Id.ToString());
                    OLEAccess.BASLetter.AddAttribut("NomPraticien", _SelectedPraticien.Nom);
                    OLEAccess.BASLetter.AddAttribut("PrenomPraticien", _SelectedPraticien.Prenom);
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", _SelectedPraticien.GenreFeminin ? "F" : "M");
                    OLEAccess.BASLetter.AddAttribut("TitrePraticien", _SelectedPraticien.Titre);
                    OLEAccess.BASLetter.AddAttribut("AdresseProfPraticien", _SelectedPraticien.MainAdresse == null ? "" : _SelectedPraticien.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("AdresseComplProfPraticien", _SelectedPraticien.MainAdresse == null ? "" : _SelectedPraticien.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", _SelectedPraticien.MainAdresse == null ? "" : _SelectedPraticien.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VillePraticien", _SelectedPraticien.MainAdresse == null ? "" : _SelectedPraticien.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("TutoiementPraticien", _SelectedPraticien.TuToiement ? "TU" : "VOUS");
                    OLEAccess.BASLetter.AddAttribut("TelPersonnelPraticien", _SelectedPraticien.MainTel == null ? "" : _SelectedPraticien.MainTel.Value);
                    OLEAccess.BASLetter.AddAttribut("TelProPraticien", _SelectedPraticien.MainTel == null ? "" : _SelectedPraticien.MainTel.Value);
                }
                else
                {
                    OLEAccess.BASLetter.AddAttribut("NomPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("PrenomPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");
                    OLEAccess.BASLetter.AddAttribut("TitrePraticien", "");
                    OLEAccess.BASLetter.AddAttribut("AdresseProfPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("AdresseComplProfPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("VillePraticien", "");
                    OLEAccess.BASLetter.AddAttribut("TutoiementPraticien", "VOUS");
                    OLEAccess.BASLetter.AddAttribut("TelPersonnelPraticien", "");
                    OLEAccess.BASLetter.AddAttribut("TelProPraticien", "");
                }


                OLEAccess.BASLetter.AddAttribut("PhotoExterneFace", _patient.Img_Ext_Face);
                OLEAccess.BASLetter.AddAttribut("PhotoExterneFaceSourire", _patient.Img_Ext_Face_Sourire);
                OLEAccess.BASLetter.AddAttribut("PhotoExterneProfil", _patient.Img_Ext_Profile);
                OLEAccess.BASLetter.AddAttribut("PhotoExterneProfilSourire", _patient.Img_Ext_Profile_Sourire);
                OLEAccess.BASLetter.AddAttribut("PhotoExterneSourire", _patient.Img_Ext_Sourire);
                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalDroit", _patient.Img_Int_Droit);
                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalFace", _patient.Img_Int_Face);
                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalGauche", _patient.Img_Int_Gauche);

                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMandibulaire", _patient.Img_Int_Man);
                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMaxilaire", _patient.Img_Int_Max);
                OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalSurplomb", _patient.Img_Int_SurPlomb);

                OLEAccess.BASLetter.AddAttribut("PhotoMoulageDroit", _patient.Img_Moul_Droit);
                OLEAccess.BASLetter.AddAttribut("PhotoMoulageFace", _patient.Img_Moul_Face);
                OLEAccess.BASLetter.AddAttribut("PhotoMoulageGauche", _patient.Img_Moul_Gauche);
                OLEAccess.BASLetter.AddAttribut("PhotoMoulageMaxilaire", _patient.Img_Moul_Max);
                OLEAccess.BASLetter.AddAttribut("PhotoMoulageMandibulaire", _patient.Img_Moul_Man);

                OLEAccess.BASLetter.AddAttribut("PhotoRadioFace", _patient.Img_Rad_Face);
                OLEAccess.BASLetter.AddAttribut("PhotoRadioProfil", _patient.Img_Rad_Profile);
                OLEAccess.BASLetter.AddAttribut("PhotoRadioPanoramique", _patient.Img_Rad_Pano);

                OLEAccess.BASLetter.PrintFrom(modelecourrier);

                Thread.Sleep(10000);
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("BaseLetter");
                foreach (System.Diagnostics.Process process in processes)
                {
                    process.Kill();
                }
            }
        
        }        
    }
}
