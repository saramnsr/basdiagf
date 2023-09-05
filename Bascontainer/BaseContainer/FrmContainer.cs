using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using BasCommon_BL;
using BasCommon_BO;
using Microsoft.Win32;
using BaseCommonControls;

namespace WindowsFormsApplication1
{
    public partial class FrmContainer : Form
    {

        
        private List<Process> appsProcesses = new List<Process>();


        private List<Form> _lstScreenContainer = new List<Form>();
        public List<Form> lstScreenContainer
        {
            get
            {
                return _lstScreenContainer;
            }
            set
            {
                _lstScreenContainer = value;
            }
        }
                

        private List<ProcessBAS> _lstprocesses = new List<ProcessBAS>();
        public List<ProcessBAS> lstprocesses
        {
            get
            {
                return _lstprocesses;
            }
            set
            {
                _lstprocesses = value;
            }
        }
        
        public FrmContainer()
        {
            InitializeComponent();

            string BackScreenFile = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\FondEcran.png";

            if (System.IO.File.Exists(BackScreenFile))
            {
                this.BackgroundImage = Bitmap.FromFile(BackScreenFile);
                this.BackgroundImageLayout = ImageLayout.Center;
            }

            BasCommon_BL.CommonCalls.CallChangeScreen = new ChangeScreenHandler(ChangeScreen);
            BasCommon_BL.CommonCalls.NextScreenHandler = new NextScreenHandler(NextScreen);
            BasCommon_BL.CommonCalls.RHBasGotoDate = new RHBaseGotoDate(RHBasGotoDatefct);
            BasCommon_BL.CommonCalls.NouvelleDemandeInStandBy = new NouvelleDemandeInStandBy(NouvelleDemandeInStandByfct);
            BasCommon_BL.CommonCalls.FrmContainer = this;
        }



        private void NouvelleDemandeInStandByfct(int IdPatient)
        {
            ProcessBAS pb = OpenApp("MainFrm", OLEAccess.BaseLAbo.CLASSNAME);

            ((IBaseLaboForm)pb.FrmReferenced).NouvelleDemandeInStandByfct(IdPatient);
           

        }


        private void RHBasGotoDatefct(DateTime dte)
        {
            ProcessBAS pb = OpenApp("NewMainFrm", OLEAccess.RHBASE.CLASSNAME);
            
            ((IRHBasForm)pb.FrmReferenced).GoToDate(DateTime.Now);
           

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenApp("NewMainFrm", OLEAccess.RHBASE.CLASSNAME);
            ChangePatient("NewMainFrm", OLEAccess.RHBASE.CLASSNAME); 
        }

        private void ChangeScreen(ProcessBAS pb)
        {

            if (pb == null) return;
             if (Screen.AllScreens.Length == 1) { 
                pb.FrmReferenced.BringToFront(); 
                return; 
            }
            int screenNum =  pb.ScreenIndex +1;
            if (screenNum >= Screen.AllScreens.Length) screenNum = 0;

            ChangeScreen(pb, screenNum);
        }

        private void ChangeScreen(ProcessBAS pb, int screenNum)
        {
            pb.ScreenIndex = screenNum;

            ChangeScreen(pb.FrmReferenced,pb.ScreenIndex);

            
        }


        private static string _RegistryKey = "Software\\BASE\\BASECommon";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _ScreenRegistryKey = _RegistryKeyPref + "\\Screens";


        private int GetScreenPos(ProcessBAS pb)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ScreenRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return 0;

            object obj = key.GetValue(pb.classname,0);
            if (obj == null)
                return 0;
            else
                return (int)obj;
        }

        private void SaveScreenPos(ProcessBAS pb, int screennum)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ScreenRegistryKey,RegistryKeyPermissionCheck.ReadWriteSubTree,System.Security.AccessControl.RegistryRights.FullControl);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_ScreenRegistryKey);
            }

            key.SetValue(pb.classname, screennum);
        }

        private void ChangeScreen(Form frm, int screenNum)
        {
            
            


            if (screenNum >= Screen.AllScreens.Length) screenNum = 0;



            foreach (ProcessBAS pb in lstprocesses)
                if (pb.FrmReferenced == frm)
                {
                    SaveScreenPos(pb, screenNum);
                }
            //  pb.FrmReferenced.Hide();

            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            frm.Dock = DockStyle.None;
            //pb.FrmReferenced.WindowState = FormWindowState.Normal;
            frm.IsMdiContainer = false;
            frm.MdiParent = null;
            frm.MdiParent = lstScreenContainer[screenNum];
            //pb.FrmReferenced.WindowState = FormWindowState.Maximized;
            frm.Dock = DockStyle.Fill;


            if (!frm.Visible)
                frm.Show();

            frm.BringToFront();

            frm.Top = 0;
            frm.Left = 0;
            if (screenNum > 0)
                frm.Height = lstScreenContainer[screenNum].ClientRectangle.Size.Height - 4;
            else
                frm.Height = lstScreenContainer[screenNum].ClientRectangle.Size.Height - menuStrip1.Height - 4;


            frm.Width = lstScreenContainer[screenNum].ClientRectangle.Size.Width - 4;








        }

        private void NextScreen(Form frm)
        {
            //foreach (Form frmcnt in lstScreenContainer)

            for (int idxscreen=0;idxscreen<lstScreenContainer.Count;idxscreen++)
            {
                foreach (Form frmchild in lstScreenContainer[idxscreen].MdiChildren)
                {
                    if (frmchild == frm)
                    {
                        int newscreen = idxscreen + 1;
                        if (newscreen >= Screen.AllScreens.Length) newscreen = 0;
                        ChangeScreen(frm, newscreen);
                        return;
                    }
                }
            }

        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenApp("frmBasStat", OLEAccess.BaseStat.CLASSNAME);
        }




        private Form ChangePatient(string AppliCode, string classname)
        {


            ProcessBAS pb = getPBFromCode(AppliCode, classname);

            if (pb == null) return null;

            if (pb.FrmReferenced is IBasForm)
                ((IBasForm)pb.FrmReferenced).ChangePatient();



            return pb.FrmReferenced;
        }

        private Form ChangePatient(string AppliCode, string classname,basePatient pat)
        {


            ProcessBAS pb = getPBFromCode(AppliCode, classname);

            if (pb.FrmReferenced is IBasForm)
                ((IBasForm)pb.FrmReferenced).ChangePatient(pat);



            return pb.FrmReferenced;
        }

       

        private ProcessBAS OpenApp(string AppliCode, string classname)
        {

            ProcessBAS pb = getPBFromCode(AppliCode, classname);


            if (pb == null)
            {
                string exefile = ApplicationMgmt.GetExePathFromRegistry(classname);

                if (!System.IO.File.Exists(exefile))
                {
                    MessageBox.Show("Application introuvable ! \n fichier : " + exefile + "\n CLASSNAME : " + classname);
                    return null;
                }

                pb = new ProcessBAS();
                pb.MainForm = AppliCode;
                pb.ProcessCode = pb.MainForm;
                pb.exefile = exefile;
                pb.classname = classname;


                OpenExeFile(pb);
            }

            if (pb.FrmReferenced!=null)
                pb.FrmReferenced.BringToFront();

            return pb;




           
        }
        
                
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenApp("FrmBASEPractice", OLEAccess.BASEPRACTICE.CLASSNAME);
            ChangePatient("FrmBASEPractice", OLEAccess.BASEPRACTICE.CLASSNAME);     
        }


        private ProcessBAS getPBFromCode(string code, string classname)
        {
            foreach (ProcessBAS pb in lstprocesses)
            {
                if ((pb.ProcessCode == code) && (pb.classname==classname))
                    return pb;
            }
            return null;
        }

        private ProcessBAS getPBFromClassname(string classname)
        {
            foreach (ProcessBAS pb in lstprocesses)
            {
                if (pb.classname==classname)
                    return pb;
            }
            return null;
        }

        

        private static void MapConfigFile(string configfile)
        {
            ExeConfigurationFileMap fm = new ExeConfigurationFileMap();
            fm.ExeConfigFilename = configfile;
            System.Configuration.Configuration remoteconfig = ConfigurationManager.OpenMappedExeConfiguration(fm, ConfigurationUserLevel.None);



            // Open App.Config of executable
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);



            foreach (KeyValueConfigurationElement kv in remoteconfig.AppSettings.Settings)
               if (!config.AppSettings.Settings.AllKeys.Contains(kv.Key.ToUpper()))
                   config.AppSettings.Settings.Add(kv.Key.ToUpper(), kv.Value);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified, true);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");

        }

        private Form OpenExeFile(ProcessBAS pb)
        {
            string configfile = pb.exefile + ".config";

            MapConfigFile(configfile);

            Assembly assembly = Assembly.LoadFrom(pb.exefile);

            //AssemblyName[] asss = assembly.GetReferencedAssemblies();

            string f = System.IO.Path.GetFileNameWithoutExtension(pb.exefile);
            

            Type tpe = assembly.GetType(f + "." + pb.MainForm);


            if (tpe == null)
            {
                foreach (Type tp in assembly.GetTypes())
                {
                    if (tp.Name == pb.MainForm)
                    {
                        tpe = tp;
                        break;
                    }
                }
            }
           
                if (tpe!= null)
                {
                    Form form = (Form)Activator.CreateInstance(tpe);
                    form.FormClosed +=new FormClosedEventHandler(form_FormClosed);
                                        
                    pb.FrmReferenced = form;

                    int idscrn = GetScreenPos(pb);
                    ChangeScreen(pb, idscrn);

                    lstprocesses.Add(pb);

                    return form;
                }
            
            return null;
        }

        private Form OpenExeNotMDI(ProcessBAS pb)
        {
            string configfile = pb.exefile + ".config";

            MapConfigFile(configfile);

            Assembly assembly = Assembly.LoadFrom(pb.exefile);

           // AssemblyName[] asss = assembly.GetReferencedAssemblies();

            Type[] tpes = assembly.GetTypes();



            foreach (Type tpe in tpes)
            {
                if (tpe.Name == pb.MainForm)
                {
                    Form form = (Form)Activator.CreateInstance(tpe);

                    form.TopMost = false;
                    form.WindowState = FormWindowState.Normal;
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    form.StartPosition = FormStartPosition.Manual;

                    form.FormClosed += new FormClosedEventHandler(form_FormClosed);

                    Rectangle r = lstScreenContainer[pb.ScreenIndex].ClientRectangle;
                    form.Bounds = r;

                    form.Show(); // Or Application.Run(form)





                    pb.FrmReferenced = form;

                    lstprocesses.Add(pb);

                    return form;
                }
            }
            return null;
        }


        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ProcessBAS pb in lstprocesses)
            {
                if (pb.FrmReferenced == sender)
                {
                    lstprocesses.Remove(pb);
                    break;
                }
            }
        }

        private void FrmContainer_Load(object sender, EventArgs e)
        {

            Screen currentsc = Screen.FromControl(this);
            lstScreenContainer.Add(this);
            foreach (Screen s in Screen.AllScreens)
            {
                if (s.Primary) continue;
                FrmScreenChild fsc = new FrmScreenChild(s);
                fsc.Show();
                lstScreenContainer.Add(fsc);
            }
        }

        private void orthalisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LoadApplicationFromName(OLEAccess.Orthalis.CLASSNAME);

        }

        private void orqualCephToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LoadApplicationFromName(OLEAccess.OrqualCeph.CLASSNAME);

           
        }




        private void Execute(string classname)
        {

            string applicationName = ApplicationMgmt.GetExePathFromRegistry(classname);
            string ProcessName = ApplicationMgmt.GetExeProcessFromRegistry(classname);

            if (applicationName == "") return;
            //Process process = null;

            Process process = Process.Start(applicationName);

            System.Threading.Thread.Sleep(3000);

            ApplicationParams(null, classname, applicationName);

          
        }



        private Process LoadApplicationFromName(string classname)
        {

            string applicationName = ApplicationMgmt.GetExePathFromRegistry(classname);
            string processName = ApplicationMgmt.GetExeProcessFromRegistry(classname);

            Process process = null;

            //Recherche du processus par nom
            Process foundprocess = null;
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                try
                {
                    if (p.MainModule.ModuleName.StartsWith(processName))
                        foundprocess = p;
                }
                catch (System.Exception)
                {
                }
            }
            //Il y a au moins un processus démarré
            if (foundprocess != null)
            {
                //On récupère l'objet Process en fonction du tableau
                //le premier élément du tableau suffit
                process = foundprocess;
                appsProcesses.Add(process);

                //Le processus a quitté?
                if (process.HasExited)
                {
                    //On l'éxecute
                    Execute(classname);

                }
                else
                {
                    //Sinon on active sa fenetre au premier plan
                    ApplicationMgmt.ActivateWindow(process.MainWindowHandle);
                    //On ré-applique les paramètres associé
                    //(si changement de variables dans un autre logiciel que celui-ci par exemple)
                    ApplicationParams(process, classname, applicationName);
                }
            }
            else
            {
                //Démarrage normal
                Execute(classname);

            }

            return process;
        }

        private void ApplicationParams(Process pr, string classname, string applicationName)
        {


            //Parametres pour BASELABO
            if (classname == OLEAccess.BaseLAbo.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.BaseLAbo.NouvelleDemande(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));

            }

            if (classname == OLEAccess.Orthalis.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.Orthalis.SetPatient(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));

            }



            //Parametres pour BASEVIEW
            if (classname == OLEAccess.BASEVIEW.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.BASEVIEW.SetPatient(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));

            }

            //Parametres pour BASEDIAG
            if (classname == OLEAccess.BASEDiag.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.BASEDiag.SetPatient(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));

            }

            //Parametres pour BASE_CONTACT
            if (classname == OLEAccess.BASECONTACT.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("NomPatient") &&
                    Attributs.DictAttributs.ContainsKey("PrenomPatient"))
                {

                    OLEAccess.BASECONTACT.SetPatientFromNomPrenom(Attributs.DictAttributs["NomPatient"], Attributs.DictAttributs["PrenomPatient"]);



                }
            }

            //Paramètres pour BASE_PRACTICE
            if (classname == OLEAccess.BASEPRACTICE.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.BASEPRACTICE.SetPatientCourantById(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));

            }

            //PARAMETRES RHBASE
            if (classname == OLEAccess.RHBASE.CLASSNAME)
            {
                if (Attributs.DictAttributs.ContainsKey("DateCourante"))
                {
                    DateTime dictDate = Convert.ToDateTime(Attributs.DictAttributs["DateCourante"]);
                    OLEAccess.RHBASE.SetDateCourante(dictDate);
                }
                if (Attributs.DictAttributs.ContainsKey("IdPatient"))
                    OLEAccess.RHBASE.setIdPatient(Convert.ToInt32(Attributs.DictAttributs["IdPatient"]));


            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            string AppliCode = "frmMain";
            string OleClass = OLEAccess.Base1er.CLASSNAME;

            OpenApp(AppliCode, OleClass);

        }

        private void BtnBaseView_Click(object sender, EventArgs e)
        {
            OpenApp("frmMain", OLEAccess.BASEVIEW.CLASSNAME);
            ChangePatient("frmMain", OLEAccess.BASEVIEW.CLASSNAME); 
        }

        private void btnQ1CS_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenQ1CS();
        }

        private void btnBaseDiag_Click(object sender, EventArgs e)
        {
            OpenApp("FirstScreen", OLEAccess.BASEDiag.CLASSNAME);

            ChangePatient("FirstScreen", OLEAccess.BASEDiag.CLASSNAME); 
            /*

            if (CurrentPatient == null) return;
            bool RespFiFound = false;
            bool RespAdmFound = false;
            bool RespLegFound = false;

            List<LienCorrespondant> lst = MgmtPatient.getCorrespondantsOf(CurrentPatient);
            foreach (LienCorrespondant lc in lst)
            {


                if (lc.TypeDeLien == "Rs")
                {
                    RespLegFound = true;
                }

                if (lc.IsAssure)
                {
                    RespAdmFound = true;
                }

                if (lc.IsPayeur)
                {
                    RespFiFound = true;
                }



            }

            if (RespFiFound && RespAdmFound && RespLegFound)
            {
                BASEPractice_BL.ApplicationMngr.OpenBaseDiag();
                System.Threading.Thread.Sleep(3000);
                OLEAccess.BASEDiag.SetPatient(CurrentPatient.Id);
                //BASEPractice_BL.ApplicationMngr.OpenBaseDiag();
            }
            else
            {
                MessageBox.Show("Les responsable Legal,Financier et Assure doivent être renseignés", "Responsables non trouvés", MessageBoxButtons.OK);
                frmCorrespondants = new FrmCorrespondance(CurrentPatient);
                frmCorrespondants.OnNeedRefresh += new EventHandler(frmCorrespondants_OnNeedRefresh);
                AddFrmToList(frmCorrespondants);
                frmCorrespondants.Show();
            }
            */
        }

        private void BtnContact_Click(object sender, EventArgs e)
        {
            OpenApp("FrmMain", OLEAccess.BASECONTACT.CLASSNAME);
            ChangePatient("FrmMain", OLEAccess.BASECONTACT.CLASSNAME); 
        }

        private void BtnLetter_Click(object sender, EventArgs e)
        {

            string exefile = ApplicationMgmt.GetExePathFromRegistry(OLEAccess.BASLetter.CLASSNAME);

            System.Diagnostics.Process.Start(exefile);

            /*
            OpenApp("MainForm", OLEAccess.BaseLEtter.CLASSNAME);
            ChangePatient("MainForm", OLEAccess.BaseLEtter.CLASSNAME); 
             */
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            OpenApp("MainFrm", OLEAccess.BaseLAbo.CLASSNAME);
            ChangePatient("MainFrm", OLEAccess.BaseLAbo.CLASSNAME); 
        }

        private void proceduresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenUrgenceProcedure();
        }

        private void numToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenUrgenceNum();
        }

        private void accidentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenUrgenceAccident();
        }

        private void laReserveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ApplicationMgmt.OpenDiapoLaReserve();
            ApplicationMgmt.OpenSiteLaReserve();
        }

        private void invisalignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenDiapoInvisalign();
        }

        private void esthétiqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenDiapoEsthetique();
        }

        private void casCliniqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenDiapoClinique();
        }

        private void informatiqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSuiviInfo();
        }

        private void hornToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSuiviHorn();
        }

        private void youngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSuiviYung();
        }

        private void pbLogicielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenpbLogiciel();
        }

        private void casSimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenDossierCasSimilaire();
        }

        private void cabinetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenDossierCabinet();
        }

        private void basSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteBas();
        }

        private void inviLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenLabsInviInternet();
        }

        private void inviInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenLabsInviInternet();
        }

        private void cahierProthesesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenLabsCahierProthese();
        }

        private void cahierOccToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenLabsCahierOccl();
        }

        private void cahierInviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenLabsCahierInvi();
        }

        private void baseSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenBasCommandeBasSite();
        }

        private void cliniqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenBasCommandeClinique();
        }

        private void secretariatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenBasCommandeSecretariat();
        }

        private void iSFESOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteIsfeso();
        }

        private void bASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteLaReserve();
        }

        private void laReserveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteLaReserve();
        }

        private void formationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteFormation();
        }

        private void toulonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteToulonSourire();
        }

        private void urgencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMgmt.OpenSiteUrgence();
        }


        private void button1_Click(object sender, EventArgs e)
        {

           
        }



        private void toolStripMenuItem2_DropDownOpening(object sender, EventArgs e)
        {
           

        }

        private void changerDécranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenApp("frmMain", OLEAccess.Base1er.CLASSNAME);
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            BaseCommonControls.FrmChoixPatient frmchoixPatient = new BaseCommonControls.FrmChoixPatient();
            //frmchoixPatient.FormClosed += new FormClosedEventHandler(frmchoixPatient_FormClosed);
            if ((frmchoixPatient.ShowDialog() == DialogResult.OK) && (frmchoixPatient.SelectedPatient != null))
            {
                basePatient patient = baseMgmtPatient.GetPatient(frmchoixPatient.SelectedPatient.Id);
                if (patient != null)
                {
                    MgmtCommonCache.Change(patient);
                    OpenApp("FrmBASEPractice", OLEAccess.BASEPRACTICE.CLASSNAME);
                    ChangePatient("FrmBASEPractice", OLEAccess.BASEPRACTICE.CLASSNAME); 
                }


            }
            frmchoixPatient = null;
        }

        private void toolStripMenuItem4_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem4.DropDownItems.Clear();

            foreach (basePatient bp in MgmtCommonCache.HistoPatient)
            {
                ToolStripItem itm = new ToolStripMenuItem(bp.ToString());
                itm.Tag = bp;
                itm.Click += new EventHandler(itm_Click);
                toolStripMenuItem4.DropDownItems.Add(itm);
            }
        }

        void itm_Click(object sender, EventArgs e)
        {
            MgmtCommonCache.Change(((basePatient)((ToolStripItem)sender).Tag));
            ChangePatient("FrmBASEPractice", OLEAccess.BASEPRACTICE.CLASSNAME, ((basePatient)((ToolStripItem)sender).Tag)); 
        }

        private void toolStripMenuItem2_DoubleClick(object sender, EventArgs e)
        {
            

        }

        private void changerEcranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppliCode = "frmMain";
            string OleClass = OLEAccess.Base1er.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
            string AppliCode = "FrmBASEPractice";
            string OleClass = OLEAccess.BASEPRACTICE.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            
            string AppliCode = "NewMainFrm";
            string OleClass = OLEAccess.RHBASE.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string AppliCode = "frmMain";
            string OleClass = OLEAccess.BASEVIEW.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string AppliCode = "FirstScreen";
            string OleClass = OLEAccess.BASEDiag.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string AppliCode = "FrmMain";
            string OleClass = OLEAccess.BASECONTACT.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void changerÉcranToolStripMenuItem5_Click(object sender, EventArgs e)
        {
           
        }

        private void changerÉcranToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string AppliCode = "MainFrm";
            string OleClass = OLEAccess.BaseLAbo.CLASSNAME;

            ProcessBAS pb = getPBFromCode(AppliCode, OleClass);



            ChangeScreen(pb);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAdminGestion frm = new FrmAdminGestion(null);
            frm.Show();
        }

        private void rappelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListesAssistante frm = new FrmListesAssistante();

            frm.Show();
        }

      


    }
}
