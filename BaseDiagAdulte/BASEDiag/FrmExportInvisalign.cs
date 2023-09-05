using BasCommon_BL;
using BasCommon_BO;
using BASEDiag_BL;
using BASEDiag_BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmExportInvisalign : Form
    {

        public InvisalignAccount AccountInvisalign  { get; set; }
        

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
        

        private string _CurrentError;
        public string CurrentError
        {
            get
            {
                return _CurrentError;
            }
            set
            {
                _CurrentError = value;
            }
        }
        

        private string _CurrentInfo;
        public string CurrentInfo
        {
            get
            {
                return _CurrentInfo;
            }
            set
            {
                _CurrentInfo = value;
            }
        }
        

        private FrmWizardInvisalign _frm;
        public FrmWizardInvisalign frm
        {
            get
            {
                return _frm;
            }
            set
            {
                _frm = value;
            }
        }


        private InvisalignPrescriptionFullObj _prescription;
        public InvisalignPrescriptionFullObj prescription
        {
            get
            {
                return _prescription;
            }
            set
            {
                _prescription = value;
            }
        }
        


        private BackgroundWorker _bw;
        public BackgroundWorker bw
        {
            get
            {
                return _bw;
            }
            set
            {
                _bw = value;
            }
        }


        public FrmExportInvisalign(FrmWizardInvisalign frmWizard,InvisalignAccount cmpte)
        {
            AccountInvisalign = cmpte;
            frm = frmWizard;

            InitializeComponent();

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
        }

        public FrmExportInvisalign(InvisalignPrescriptionFullObj PrescriptionFull, basePatient pat, InvisalignAccount cmpte)
        {

            AccountInvisalign = cmpte;
            prescription = PrescriptionFull;
            CurrentPatient = pat;

            InitializeComponent();

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.DoWork += bw_DoWorkPrescriptionFull;
            bw.RunWorkerCompleted += bw_RunWorkerPrescriptionCompleted;
        }
        private string responseUri()
        {
            HttpWebResponse response = Invisalign.PrescriptionSelectProduct(CurrentPatient.infosinvisalign.IdInvisalign, prescription.tpePrescription, prescription.tpeProduct, prescription.tpePatient);

            if (response != null)
            {

                string[] queries = response.ResponseUri.Query.Substring(1, response.ResponseUri.Query.Length - 1).Split(',');
                Dictionary<string, string> dicoquery = new Dictionary<string, string>();
                string formId = "";
                foreach (string s in queries)
                {
                    string[] ss = s.Split('=');
                    dicoquery.Add(ss[0], ss[1]);
                }
                try
                {

                    string form =  dicoquery["formId"];
                    response.Close();
                    return form;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    response.Close();
                    return "";
                }
             
            }
            return "";
        }
        private void bw_DoWorkPrescriptionFull(object sender, DoWorkEventArgs e)
        {


            try
            {
                bool created = baseMgmtPatient.GetInvisalignInfos(CurrentPatient);

                DateTime lockTimeOut = DateTime.Now;
                while (created)
                {
                    created = baseMgmtPatient.GetInvisalignInfos(CurrentPatient);
                    if ((lockTimeOut - DateTime.Now).Minutes > 2) return;
                }


                string resultat = "";

                CurrentInfo = "Connection à invisalign ";
                bw.ReportProgress(10);




                string CurrentError = "\n" + Invisalign.NewConnect();

                bw.ReportProgress(20);




                InvisalignAccount inv = new InvisalignAccount();
                inv.login = "pbergegh";
                inv.password = "patrice1201";
                CurrentError += "\n" + Invisalign.Newlogin(inv);



                Invisalign.patient = CurrentPatient;
                int i = 0;
                string formId = "";
                while (i == 0)
                {
                    formId = responseUri();
                    if (formId != String.Empty)
                    {
                        i = 1 ;
                    }
                }

                /*

                HttpWebResponse response = Invisalign.PrescriptionSelectProduct(CurrentPatient.infosinvisalign.IdInvisalign, prescription.tpePrescription,prescription.tpeProduct,prescription.tpePatient);
                if (response != null)
                {
                    string[] queries = response.ResponseUri.Query.Substring(1, response.ResponseUri.Query.Length - 1).Split(',');
                    Dictionary<string, string> dicoquery = new Dictionary<string, string>();
                    foreach (string s in queries)
                    {
                        string[] ss = s.Split('=');
                        dicoquery.Add(ss[0], ss[1]);
                    }
                    try
                    {

                        formId = dicoquery["formId"];
                    }
                    catch(Exception ex){

                    }*/
                CurrentInfo = "Etape 1";
                bw.ReportProgress(30);
                Invisalign.Etape1Full(prescription, formId);



                CurrentInfo = "Etape 2";
                bw.ReportProgress(40);
                Invisalign.Etape2Full(prescription, formId);


                CurrentInfo = "Etape 3";
                bw.ReportProgress(50);
                Invisalign.Etape3Full(prescription, formId);

                CurrentInfo = "Etape 4";
                bw.ReportProgress(60);
                Invisalign.Etape4Full(prescription, formId);

                CurrentInfo = "Etape 5";
                bw.ReportProgress(70);
                Invisalign.Etape5Full(prescription, formId);

                CurrentInfo = "Etape 6";
                bw.ReportProgress(75);
                Invisalign.Etape6Full(prescription, formId);

                CurrentInfo = "Etape 7";
                bw.ReportProgress(75);
                Invisalign.Etape77Full(prescription, formId);

                CurrentInfo = "Etape 8";
                bw.ReportProgress(80);
                Invisalign.Etape7Full(prescription, formId);

                CurrentInfo = "Etape 9";
                bw.ReportProgress(85);
                Invisalign.Etape8Full(prescription, formId);

                CurrentInfo = "Etape 10";
                bw.ReportProgress(90);
                Invisalign.Etape9Full(prescription, formId);

                CurrentInfo = "Etape 11";
                bw.ReportProgress(95);
                Invisalign.Etape10Full(prescription, formId);

                CurrentInfo = "Etape 12";
                bw.ReportProgress(98);
                Invisalign.Etape11Full(prescription, formId);

                CurrentInfo = "Etape 13";
                bw.ReportProgress(98);
                Invisalign.Etape12Full(prescription, formId);


                //}
                //response.Close();
                //response = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void bw_RunWorkerPrescriptionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Operation annulée");
            else
            {
                if (CurrentPatient.infosinvisalign == null)
                    baseMgmtPatient.GetInvisalignInfos(CurrentPatient);

                if (!string.IsNullOrEmpty(CurrentPatient.infosinvisalign.IdInvisalign))
                {
                   
                  
                    if (!Invisalign.CallIEInvisalign(CurrentPatient.infosinvisalign.NomInvisalign, CurrentPatient.infosinvisalign.PrenomInvisalign, CurrentPatient.infosinvisalign.IdInvisalign))
                        MessageBox.Show("Erreur Appel Invisalign");
                }


            }
            this.Close();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Operation annulée");

           this.Close();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblInfo.Text = CurrentInfo;
         //   lblError.Text = CurrentError;
               
        }

        static bool invisalignlocked = false;
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                string resultat = "";

                CurrentInfo = "Connection à invisalign ";
                bw.ReportProgress(10);

                CurrentError += "\n"+ Invisalign.NewConnect();

                bw.ReportProgress(20);
                CurrentError += "\n" + Invisalign.Newlogin(AccountInvisalign);


                bw.ReportProgress(20);
                CurrentInfo = "Création du patient ";
                
                string IdInvisalign = Invisalign.InsertPatientPost(frm.ValuePatient);
                 
                if (!string.IsNullOrEmpty(IdInvisalign))
                {

                    baseMgmtPatient.UpdateAsInvisalign(IdInvisalign, frm.ValuePatient.Id, frm.ValuePatient.Nom, frm.ValuePatient.Prenom);

                    frm.ValuePatient.infosinvisalign = new InfosInvisalign();
                    frm.ValuePatient.infosinvisalign.IdInvisalign = IdInvisalign;
                    frm.ValuePatient.infosinvisalign.NomInvisalign = frm.ValuePatient.Nom;
                    frm.ValuePatient.infosinvisalign.PrenomInvisalign = frm.ValuePatient.Prenom;
                   

                    bw.ReportProgress(40);
                    CurrentInfo = "Envois du portrait du patient ";

                    CurrentError += "\n" + Invisalign.UploadProfilPicture(IdInvisalign, frm.PhotoPatient);


                    bw.ReportProgress(50);
                    CurrentInfo = "Envois des photos du patient ";

                    CurrentError += "\n" + Invisalign.UploadPhotos(IdInvisalign,
                        frm.PhotoProfil,
                        frm.PhotoFace,
                        frm.PhotoFaceSourire,
                        frm.Mand,
                        frm.Max,
                        frm.IntraDroit,
                        frm.IntraFace,
                        frm.IntraGauche
                         );

                    

                    bw.ReportProgress(60);
                    CurrentInfo = "Envois des radios du patient ";
                    CurrentError += "\n" + Invisalign.UploadRadios(IdInvisalign,
                       frm.Radio,
                       frm.Pano
                       );


                    bw.ReportProgress(80);
                    CurrentInfo = "Envois des informations clinique ";

                    string err = Invisalign.UpdateConditionsCliniquePOST(IdInvisalign,
                        frm.Encombrement,
                        frm.Espacement,
                        frm.ClIIDiv1,
                        frm.ClIIDiv2,
                        frm.ClIII,
                        frm.Beance,
                        frm.Suppra,
                        frm.ArticulareAnt,
                        frm.ArticularePost,
                        frm.ArcadeEtroite,
                        frm.ProAlveolie,
                        frm.Surplomb,
                        frm.SourireInesthetique,
                        frm.AnomalieFormeDentaire,
                        frm.Autre,
                        frm.AutreTxt,
                        frm.Remarques
                        );

                    if (err != "")
                    {
                        bw.ReportProgress(90);
                        CurrentInfo = "Envois des informations clinique (GET Method)";
                        bw.ReportProgress(45);
                        err = Invisalign.UpdateConditionsCliniqueGET(IdInvisalign,
                        frm.Encombrement,
                        frm.Espacement,
                        frm.ClIIDiv1,
                        frm.ClIIDiv2,
                        frm.ClIII,
                        frm.Beance,
                        frm.Suppra,
                        frm.ArticulareAnt,
                        frm.ArticularePost,
                        frm.ArcadeEtroite,
                        frm.ProAlveolie,
                        frm.Surplomb,
                        frm.SourireInesthetique,
                        frm.AnomalieFormeDentaire,
                        frm.Autre,
                        frm.AutreTxt,
                        frm.Remarques
                        );
                    }
                    CurrentError += "\n" + err;

                    bw.ReportProgress(100);
                    CurrentInfo = "Transfert terminé ";
                    if (CurrentError.Trim() != "")
                        MessageBox.Show(CurrentError);

                }
                else
                {
                    MessageBox.Show("Le patient n'a pas pu être créé (Existe il déja dans Invisalign ?)");
                }
            }


            catch (System.Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        private void FrmWait_Load(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
        }
    }
}
