using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Reflection;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls;
using System.IO;
using System.Configuration;

namespace BASEDiagAdulte
{

    public delegate void DelegateSetPatient(int Id);
    public delegate void DelegateOpenPatientFromFile(string filename);


    public partial class FirstScreen : Form, IBasForm
    {

        public InvisalignAccount CompteInvisalign { get; set; }

        private FrmLastSummaryAdult _frmLastSummary = null;
        public FrmLastSummaryAdult frmLastSummary
        {
            get
            {
                return _frmLastSummary;
            }
            set
            {
                _frmLastSummary = value;
            }
        }

        bool needtosave = false;

        public DelegateSetPatient m_DelegateSetPatient;
        public DelegateOpenPatientFromFile m_DelegateOpenPatientFromFile;
        
        System.EventHandler RadioChange;

        Screen[] screenlst;
        int CurrentScreenIdx;

        private basePatient _CurrentPatient = null;
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
           
        public FirstScreen()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);
            // If the return value is null, the key doesn't exist
            if (key == null)
            {

                frmChoixCabinet frm = new frmChoixCabinet();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);
                }
            }
            else
            {
                string objValidityDate = (string)key.GetValue("ValidityDate");
                string objValidityUser = (string)key.GetValue("ValidityCab");

                key.Close();

                DateTime ValidityDate;
                int idCab = 1;
                if (DateTime.TryParse(objValidityDate, out ValidityDate))
                {

                    if (ValidityDate < DateTime.Now || !(int.TryParse(objValidityUser, out idCab)))
                    {
                        frmChoixCabinet frm = new frmChoixCabinet();
                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                        }
                    }


                }
            }
            InitializeComponent();
            this.label1.Text = this.label1.Text + prefix.Replace("_", " ");
            m_DelegateSetPatient = new DelegateSetPatient(RefreshPatient);
            m_DelegateOpenPatientFromFile = new DelegateOpenPatientFromFile(OpenFromPatFile);

            
            screenlst = Screen.AllScreens;

            CurrentScreenIdx = RegistryParameters.GetScreenNumberOf(this.GetType());
            CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            


                FrmChoixPatient frm = new FrmChoixPatient();
                frm.Owner = this;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.SelectedPatient == null) return;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        ResumeIsSaved(false);
                        RefreshPatient(frm.SelectedPatient.Id);

                    
                    }
                    catch (System.Exception) { throw; }
                    finally { Cursor = Cursors.Default; }
                }
            
        }



        void frmAn1_FormClosing(object sender, FormClosingEventArgs e)
        {            
            DialogResult dr = ((Form)sender).DialogResult;
            if (dr == DialogResult.Yes)
                toolStripButton3_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                BtnPreAnalyse_Click(this, new EventArgs());


            needtosave = true;
        }

        
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse1 frm = new FrmAnalyse1(CurrentPatient);

            frm.Owner = this;
            frm.FormClosing += new FormClosingEventHandler(frmAn1_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });
                    
        }


        void frmAn2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton4_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton2_Click(this, new EventArgs());
            needtosave = true;
        }

        void frmAn3_FormClosing(object sender, FormClosingEventArgs e)
        {
             DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton2_Click_1(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton3_Click(this, new EventArgs());
            needtosave = true;
        }

            


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse2 frm = new FrmAnalyse2(CurrentPatient);
            frm.Owner = this;
            frm.FormClosing += new FormClosingEventHandler(frmAn2_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse3 frm = new FrmAnalyse3(CurrentPatient);
            frm.Owner = this;

            frm.FormClosing += new FormClosingEventHandler(frmAn3_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });


            
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse4 frm = new FrmAnalyse4(CurrentPatient);

            frm.FormClosing += new FormClosingEventHandler(frmFonct_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });
        }
        void frmFonct_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton5_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton4_Click(this, new EventArgs());
            needtosave = true;
        }


        void frmAn4_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton6_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton2_Click_1(this, new EventArgs());
            needtosave = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse5 frm = new FrmAnalyse5(CurrentPatient);
            frm.Owner = this;
            frm.FormClosing += new FormClosingEventHandler(frmAn4_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });


            
        }

        void frmAn5_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton7_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton5_Click(this, new EventArgs());
            needtosave = true;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse6 frm = new FrmAnalyse6(CurrentPatient);
            frm.Owner = this;

            frm.FormClosing += new FormClosingEventHandler(frmAn5_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });


           
        }


        void frmAn6_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton8_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton6_Click(this, new EventArgs());
            needtosave = true;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse7 frm = new FrmAnalyse7(CurrentPatient);
            frm.Owner = this;
            frm.FormClosing += new FormClosingEventHandler(frmAn6_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });



            
        }


        void frmAn7_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            if (dr == DialogResult.Yes)
                toolStripButton9_Click(this, new EventArgs());
            if (dr == DialogResult.No)
                toolStripButton7_Click(this, new EventArgs());
            needtosave = true;
        }


        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse8 frm = new FrmAnalyse8(CurrentPatient);
            frm.Owner = this;

            frm.FormClosing += new FormClosingEventHandler(frmAn7_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });


            
        }




        void FrmPlanTraitement_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            needtosave = true;
            if (dr == DialogResult.Yes)
            {
                ResumeIsSaved(true);
                btnResultat_Click(this, new EventArgs());
            }
            if (dr == DialogResult.No)
                toolStripButton8_Click(this, new EventArgs());
        }

        void frmAn8_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = ((Form)sender).DialogResult;

            needtosave = true;
            if (dr == DialogResult.Yes)
            {
                btnTraitement_Click(this, new EventArgs());
            }
            if (dr == DialogResult.No)
                toolStripButton8_Click(this, new EventArgs());
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmAnalyse9 frm = new FrmAnalyse9(CurrentPatient);
            frm.Owner = this;

            frm.FormClosing += new FormClosingEventHandler(frmAn8_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });


            
            
        }

        private void FirstScreen_Activated(object sender, EventArgs e)
        {

            RefreshButtonStates();
            bool isfocused = imgCtrl.Focus();

            if (ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire != imgCtrl.file)
            {

                imgCtrl.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);

                imgCtrl.zoomAuto();
                imgCtrl.Center();

            }
        }

        private void RefreshButtonStates()
        {
            BtnMasqueFacial.Image = global::BASEDiagAdulte.Properties.Resources.MasqueFacial;
            if (ResumeCliniqueMgmt.Analyse1IsStarted && ResumeCliniqueMgmt.Analyse1IsValid != "")
                BtnMasqueFacial.Image = global::BASEDiagAdulte.Properties.Resources.MasqueFacialNOK;
            if (ResumeCliniqueMgmt.Analyse1IsStarted && ResumeCliniqueMgmt.Analyse1IsValid == "")
                BtnMasqueFacial.Image = global::BASEDiagAdulte.Properties.Resources.MasqueFacialOK;


            BtnSourireFace.Image = global::BASEDiagAdulte.Properties.Resources.AnalyseSourire;
            if (ResumeCliniqueMgmt.Analyse2IsStarted && ResumeCliniqueMgmt.Analyse2IsValid != "")
                BtnSourireFace.Image = global::BASEDiagAdulte.Properties.Resources.AnalyseSourireNOK;
            if (ResumeCliniqueMgmt.Analyse2IsStarted && ResumeCliniqueMgmt.Analyse2IsValid == "")
                BtnSourireFace.Image = global::BASEDiagAdulte.Properties.Resources.AnalyseSourireOK;


            BtnOcclusal.Image = global::BASEDiagAdulte.Properties.Resources.Occlusal;
            if (ResumeCliniqueMgmt.Analyse3IsStarted && ResumeCliniqueMgmt.Analyse3IsValid != "")
                BtnOcclusal.Image = global::BASEDiagAdulte.Properties.Resources.OcclusalNOK;
            if (ResumeCliniqueMgmt.Analyse3IsStarted && ResumeCliniqueMgmt.Analyse3IsValid == "")
                BtnOcclusal.Image = global::BASEDiagAdulte.Properties.Resources.OcclusalOK;

            BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.Fonctionnel;
            if (ResumeCliniqueMgmt.AnalyseFonctionnelIsStarted && ResumeCliniqueMgmt.AnalyseFonctionnelIsValid != "")
                BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelNOK;
            if (ResumeCliniqueMgmt.AnalyseFonctionnelIsStarted && ResumeCliniqueMgmt.AnalyseFonctionnelIsValid == "")
                BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelOK;

            //BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.Fonctionnel;
            ////if (ResumeCliniqueMgmt.AnalyseFonctionnelIsStarted && ResumeCliniqueMgmt.AnalyseFonctionnelIsValid == "")
            ////    BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelNOK;
            //if (ResumeCliniqueMgmt.AnalyseFonctionnelIsStarted && ResumeCliniqueMgmt.AnalyseFonctionnelIsValid != "")
            //    BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelOK;
             
            //if (ResumeCliniqueMgmt.AnalyseFonctionnelIsStarted && ResumeCliniqueMgmt.AnalyseFonctionnelIsValid == "")
            //    BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelOK;
            //if (CurrentPatient == null)
            //{
            //    BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.Fonctionnel;

            //}
            //else {
            //    if (ResumeCliniqueMgmt.AnalyseFonctionnelIsValid != "")
            //        BtnFonctionnel.Image = global::BASEDiagAdulte.Properties.Resources.FonctionnelNOK;

            //}
           
            BtnArcades.Image = global::BASEDiagAdulte.Properties.Resources.Arcades;
            if (ResumeCliniqueMgmt.Analyse4IsStarted && ResumeCliniqueMgmt.Analyse4IsValid != "")
                BtnArcades.Image = global::BASEDiagAdulte.Properties.Resources.ArcadesNOK;
            if (ResumeCliniqueMgmt.Analyse4IsStarted && ResumeCliniqueMgmt.Analyse4IsValid == "")
                BtnArcades.Image = global::BASEDiagAdulte.Properties.Resources.ArcadesOK;


            BtnSourires.Image = global::BASEDiagAdulte.Properties.Resources.Sourire;
            if (ResumeCliniqueMgmt.Analyse5IsStarted && ResumeCliniqueMgmt.Analyse5IsValid != "")
                BtnSourires.Image = global::BASEDiagAdulte.Properties.Resources.SourireNOK;
            if (ResumeCliniqueMgmt.Analyse5IsStarted && ResumeCliniqueMgmt.Analyse5IsValid == "")
                BtnSourires.Image = global::BASEDiagAdulte.Properties.Resources.SourireOK;


            BtnProfil.Image = global::BASEDiagAdulte.Properties.Resources.Profil;
            if (ResumeCliniqueMgmt.Analyse6IsStarted && ResumeCliniqueMgmt.Analyse6IsValid != "")
                BtnProfil.Image = global::BASEDiagAdulte.Properties.Resources.ProfilNOK;
            if (ResumeCliniqueMgmt.Analyse6IsStarted && ResumeCliniqueMgmt.Analyse6IsValid == "")
                BtnProfil.Image = global::BASEDiagAdulte.Properties.Resources.ProfilOK;


            BtnRadio.Image = global::BASEDiagAdulte.Properties.Resources.radio;
            if (ResumeCliniqueMgmt.Analyse7IsStarted && ResumeCliniqueMgmt.Analyse7IsValid != "")
                BtnRadio.Image = global::BASEDiagAdulte.Properties.Resources.RadioNOK;
            if (ResumeCliniqueMgmt.Analyse7IsStarted && ResumeCliniqueMgmt.Analyse7IsValid == "")
                BtnRadio.Image = global::BASEDiagAdulte.Properties.Resources.RadioOK;


            BtnPano.Image = global::BASEDiagAdulte.Properties.Resources.Pano;
            if (ResumeCliniqueMgmt.Analyse8IsStarted && ResumeCliniqueMgmt.Analyse8IsValid != "")
                BtnPano.Image = global::BASEDiagAdulte.Properties.Resources.PanoNOK;
            if (ResumeCliniqueMgmt.Analyse8IsStarted && ResumeCliniqueMgmt.Analyse8IsValid == "")
                BtnPano.Image = global::BASEDiagAdulte.Properties.Resources.PanoOK;

            /*
            if ((ResumeCliniqueMgmt.Analyse8IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse7IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse6IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse5IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse4IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse3IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse2IsValid == "") &&
                (ResumeCliniqueMgmt.Analyse1IsValid == ""))
            {
                BASEDiag_BL.DemandeEntenteMgmt.SaveAll();
            }*/
        }

        private void BtnPreAnalyse_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmPreAnalyse frm = new FrmPreAnalyse(CurrentPatient);

            frm.FormClosing += new FormClosingEventHandler(frm_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm,1 });
            

        }

        void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
                toolStripButton2_Click(this, new EventArgs());
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MdiParent != null)
            {
                MdiParent.Invoke(BasCommon_BL.CommonCalls.NextScreenHandler, new object[] { this });
            }
            else
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


        }

        

        private void FirstScreen_Load(object sender, EventArgs e)
        {


            if (System.Configuration.ConfigurationManager.AppSettings["COMRegistration"] != "false") InitialiseCOM();
            if (CurrentScreenIdx >= screenlst.Length)
                this.Bounds = screenlst[0].WorkingArea;
            else
                this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;

            RadioChange = new System.EventHandler(this.imgCtrl_OnRadioChanged);
            this.imgCtrl.OnRadioChanged += RadioChange;

            Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();

            btnFauteuil.Text = IamTheFauteuil == null ? "" : IamTheFauteuil.libelle;



            if (BasCommon_BL.MgmtCommonCache.CurrentPatient != null)
                RefreshPatient(BasCommon_BL.MgmtCommonCache.CurrentPatient);
            

        }

        private void btnResultat_Click(object sender, EventArgs e)
        {
            ResumeIsSaved(false);


            bool cancontinue = ResumeCliniqueMgmt.AllAnalysesAreValid == "";

            if (!cancontinue) 
                MessageBox.Show("L'analyse n'est pas complete !", "Analyse incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (cancontinue)
            {
                if (CurrentPatient == null) return;

                baseMgmtPatient.FillInfocomplementaire(CurrentPatient);
                CurrentPatient.propositions = PropositionMgmt.getPropositions(CurrentPatient);
                CurrentPatient.PersonnesAContacter = baseMgmtPatient.getPersonnesAContacter(CurrentPatient);


                if (frmLastSummary == null)
                {
                    frmLastSummary = new FrmLastSummaryAdult(CurrentPatient, CurrentScreenIdx,CompteInvisalign);
                    frmLastSummary.FormClosed += new FormClosedEventHandler(frmLastSummary_FormClosed);
                }
                frmLastSummary.Show();

            }
            

            

            

        }

        void frmLastSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLastSummary = null;
        }

        private void InitialiseCOM()
        {

            System.Runtime.InteropServices.RegistrationServices services = new System.Runtime.InteropServices.RegistrationServices();
            try
            {

                System.Reflection.Assembly ass = Assembly.GetExecutingAssembly();
                services.RegisterAssembly(ass, System.Runtime.InteropServices.AssemblyRegistrationFlags.SetCodeBase);
                Type t = typeof(OLEServer);
                try
                {
                    Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\{" + t.GUID.ToString() + "}\\InprocServer32");
                    Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\{" + t.GUID.ToString() + "}\\Implemented Categories");


                }
                catch (Exception)
                {
                }

                System.Guid GUID = t.GUID;
                services.RegisterTypeForComClients(t, ref GUID);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to initialise COM Server", e);
            }
        }

        private void RefreshPatient(basePatient patient)
        {
            CurrentPatient = patient;
            if (_CurrentPatient.Correspondants ==null)
                _CurrentPatient.Correspondants = MgmtCorrespondants.getCorrespondantsOf(_CurrentPatient);

            if (_CurrentPatient.ImagesHasBeenLoaded == false)
            {
                BasCommon_BL.baseMgmtPatient.AffectRepertoireToPatient(CurrentPatient);
                BasCommon_BL.ImagesMgmt.AffectImageToPatient(CurrentPatient);
            }

          

            barrePatient1.patient = CurrentPatient;

            if (CurrentPatient.infoSmilers == null)
                CurrentPatient.infoSmilers = SmilersMgmt.getInfoSmilers(CurrentPatient.Id);
            if (_CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPatient);

            if (CurrentPatient.MainAdresse == null)
                MessageBox.Show(this,"Ce patient n'a aucune adresse renseignée", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            List<ResumeClinique> lstr = ResumeCliniqueMgmt.GetResumesClinique(CurrentPatient);

            if (lstr.Count == 0)
            {
                ResumeCliniqueMgmt.resumeCl = new ResumeClinique();
                ResumeCliniqueMgmt.resumeCl.patient = CurrentPatient;
                ResumeCliniqueMgmt.resumeCl.dateResume = DateTime.Now;
            }
            else
                if (lstr.Count == 1)
                {
                    if (MessageBox.Show(this,"Un diagnostique à déja été fait le " + lstr[0].dateResume.ToString("dd MMM yyyy") + "\nSouhaitez-vous en faire un nouveau?", "Nouveau diag?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        ResumeCliniqueMgmt.resumeCl = lstr[0];
                    else
                    {
                        ResumeCliniqueMgmt.resumeCl = new ResumeClinique();
                        ResumeCliniqueMgmt.resumeCl.patient = CurrentPatient;
                        ResumeCliniqueMgmt.resumeCl.dateResume = DateTime.Now;
                    }
                }
                else
                {
                    frmSelectResume frm = new frmSelectResume(CurrentPatient);
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        ResumeCliniqueMgmt.resumeCl = frm.Value;
                    }
                    else
                    {
                        ResumeCliniqueMgmt.resumeCl = new ResumeClinique();
                        ResumeCliniqueMgmt.resumeCl.patient = CurrentPatient;
                        ResumeCliniqueMgmt.resumeCl.dateResume = DateTime.Now;
                    }
                }

            ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement = ResumeCliniqueMgmt.GetPlanTraitementObjects(ResumeCliniqueMgmt.resumeCl.Id);

            ResumeCliniqueMgmt.getObjectifs(ResumeCliniqueMgmt.resumeCl);
            //ResumeCliniqueMgmt.getAppareils(ResumeCliniqueMgmt.resumeCl);

            imgCtrl.OnRadioChanged -= RadioChange;
            imgCtrl.Clear();
            imgCtrl.OnRadioChanged += RadioChange;

            if (ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire != "")
            {

                imgCtrl.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);

                imgCtrl.zoomAuto();
                imgCtrl.Center();

            }
            RefreshButtonStates();
            imgCtrl.Invalidate();
            this.BringToFront();
        }

        private void RefreshPatient(int idPatient)
        {
            basePatient bp =  baseMgmtPatient.GetPatient(idPatient);
            RefreshPatient(bp);
        }

        private void FirstScreen_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void imgCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void imgCtrl_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void imgCtrl_OnRadioChanged(object sender, EventArgs e)
        {
            BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire = imgCtrl.file;

        }

        private void FirstScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResumeIsSaved(false);
        }

        private bool ResumeIsSaved(bool Nowarn)
        {
            if (CurrentPatient == null) 
                return true;

            if (needtosave)
            {
                bool cansave = ((ResumeCliniqueMgmt.resumeCl.Id != -1)&&(ResumeCliniqueMgmt.AllAnalysesAreValid == ""));
                
                
                if (!cansave)
                    if (Nowarn)
                        cansave = true;
                    else
                        cansave = MessageBox.Show("Souhaitez-vous enregistrer le diagnostique ?", "Enregistrer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                if ((cansave)&&(CurrentPatient.Id < 0))
                {
                    MessageBox.Show(this, "Le patient n'existe pas en base, le diagnostique ne sera pas enregistré ", "Sauvegarde impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    needtosave = false;
                    cansave = false;
                }


                if (cansave)
                {
                    ResumeCliniqueMgmt.SaveAll(ResumeCliniqueMgmt.resumeCl);
                    needtosave = false;
                    return true;
                }
                else return false;
            }
            else return true;
        }

        private void btnFauteuil_Click(object sender, EventArgs e)
        {
            FrmChoixFauteuil frm = new FrmChoixFauteuil();
            frm.ShowDialog();

            Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();
            btnFauteuil.Text = IamTheFauteuil == null ? "" : IamTheFauteuil.libelle;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {
           

        }




        public void ChangePatient(int IdPatient)
        {
            RefreshPatient(IdPatient);
        }

        public void ChangePatient()
        {
            if (BasCommon_BL.MgmtCommonCache.CurrentPatient != null)
                RefreshPatient(BasCommon_BL.MgmtCommonCache.CurrentPatient);
        }

        public void ChangePatient(basePatient patient)
        {
            if (patient != null)
                RefreshPatient(patient);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTraitement_Click(object sender, EventArgs e)
        {
            if (CurrentPatient == null) return;
            FrmPlanTraitement frm = new FrmPlanTraitement(CurrentPatient);
            frm.Owner = this;

            frm.Owner = this;
            frm.FormClosing += new FormClosingEventHandler(FrmPlanTraitement_FormClosing);
            frm.Show();

            if ((this.MdiParent != null) && (BasCommon_BL.CommonCalls.CallChangeScreen != null))
                this.MdiParent.Invoke(BasCommon_BL.CommonCalls.CallChangeScreen, new object[] { frm, 1 });
        }

      

        private void OpenFromPatFile(string FileName)
        {
            string imagesfolder = "";
            string comm = "";


            InvisalignAccount acc = null;
            int[] Duree = null;

            BasCommon_BO.basePatient pat = ImagesMgmt.OpenFilePatient(FileName, ref imagesfolder, ref comm, ref acc,ref Duree);
            CompteInvisalign = acc;
            BasCommon_BL.ImagesMgmt.ReaffectStandardFolders(imagesfolder, pat);
            pat.Id = -2;


            RefreshPatient(pat);
        }

        private void barrePatient1_Load(object sender, EventArgs e)
        {

        }

       

       
    }
}
