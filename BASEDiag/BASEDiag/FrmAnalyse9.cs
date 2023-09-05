using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using System.Configuration;
using BasCommon_BL;
using BasCommon_BO;
using System.IO;
using Microsoft.Win32;
namespace BASEDiag
{
    public partial class FrmAnalyse9 : FormScreen
    {



        private basePatient _CurrentPat;
        public basePatient CurrentPat
        {
            get
            {
                return _CurrentPat;
            }
            set
            {
                _CurrentPat = value;
            }
        }

        public FrmAnalyse9(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;
            
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
        private void FrmAnalyse8_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        public void InitLoad()
        {
            barrePatient1.patient = CurrentPat;
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            
            imageCtrl1.loadImage(ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            imageCtrl1.zoomAuto();
            imageCtrl1.Center();

            
            LoadPPT("Analyse8");


            InitDisplay();
        }

        private void FrmAnalyse8_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse8IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse8AffectDefault();
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = chkbxEvolGermeDents.CheckState==CheckState.Checked;

            if (!chkbxEvolGermeDents.Checked)
                tabControl1.TabPages.Remove(tpAutre);
            else
            {
                if (radioButton3.Checked)
                {
                    if (!tabControl1.TabPages.Contains(tpAutre))
                    {
                        tabControl1.TabPages.Add(tpAutre);
                        tabControl1.SelectedTab = tpAutre;
                    }
                }
                else tabControl1.TabPages.Remove(tpAutre);
            }


            if (chkbxEvolGermeDents.CheckState == CheckState.Indeterminate)
                ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
            if (chkbxEvolGermeDents.CheckState == CheckState.Checked)
                ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents = BasCommon_BO.EntentePrealable.en_OuiNon.Oui;
            if (chkbxEvolGermeDents.CheckState == CheckState.Unchecked)
                ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
            

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cdentAutre_Load(object sender, EventArgs e)
        {

        }

        private void rbDefinitive_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cdentAutre_OnChange(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = cdentAutre.SelectedDents;
        }

        

      

        void InitDisplay()
        {
            cdentAgenesie.SelectedDents = ResumeCliniqueMgmt.resumeCl.Agenesie;
            cdentincluses.SelectedDents = ResumeCliniqueMgmt.resumeCl.DentsIncluses;
            cdentSurnum.SelectedDents = ResumeCliniqueMgmt.resumeCl.DentsSurnumeraires;
            cdentSagesse.SelectedDents = ResumeCliniqueMgmt.resumeCl.DentsDeSagesse;
            cdentAutre.SelectedDents = ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur;
            if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur == "Dents définitives")
                rbDefinitive.Checked = true;
            else
                if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur == "Dents de sagesse")
                    radioButton2.Checked = true;
                else
                    radioButton3.Checked = true;

            if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                chkbxEvolGermeDents.CheckState = CheckState.Indeterminate;
            if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                chkbxEvolGermeDents.CheckState = CheckState.Checked;
            if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.Non)
                chkbxEvolGermeDents.CheckState = CheckState.Unchecked;



            panel1.Visible = chkbxEvolGermeDents.CheckState == CheckState.Checked;

            tabControl1.TabPages.Remove(tpAutre);
            if ((chkbxEvolGermeDents.Checked) && (radioButton3.Checked))
                tabControl1.TabPages.Add(tpAutre);


        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void CheckDiags()
        {
            int[] AcceptedDiags = new int[] { 19 };
            
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (!AcceptedDiags.Contains(cd.Id)) continue;
                if (cd.SelectedObjectif) continue;
                FrmLittleWizard frm = new FrmLittleWizard(cd,CurrentPat);
                if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                {
                    foreach (CommonObjectifFromDiag co in frm.values)
                    {
                        if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
                            CurrentPat.SelectedObjectifs.Add(co.objectif);
                    }
                    cd.SelectedObjectif = true;
                }
            }

        }
        private void BTnNext_Click(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes;
            Close();

        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void lvPPT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(((string)lvPPT.SelectedItems[0].Tag));
        }

        private void cdentAgenesie_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Agenesie = cdentAgenesie.SelectedDents;
        }

        private void cdentincluses_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.DentsIncluses = cdentincluses.SelectedDents;
        }

        private void cdentSurnum_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.DentsSurnumeraires = cdentSurnum.SelectedDents;
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.DentsDeSagesse = cdentSagesse.SelectedDents;
        }

        private void cdentAutre_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = cdentAutre.SelectedDents;
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
                this.WindowState = FormWindowState.Normal;

                CurrentScreenIdx++;
                CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;


                this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

                this.Visible = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportToLetter(false);
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
        public void ExportToLetter(bool DirectPrint)
        {
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierAnalysePano"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }
            /*
            Bitmap bmp = new Bitmap(ImgProfil.Width, ImgProfil.Height);
            Graphics g = Graphics.FromImage(bmp);
            ImgProfil.PaintOn(g, new Rectangle(0, 0, ImgProfil.Width, ImgProfil.Height));
            string fileprofil = Path.GetTempFileName();
            bmp.Save(fileprofil);
            */
            string fileprofilsourire = GetAnalyseImage();

            /*
             public string LevreSupRes = "Normo";
        public string LevreInfRes = "Normo";
        public string MentonRes = "Normo";
             */

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Image_Panoramique", fileprofilsourire);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else

                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        public string GetAnalyseImage()
        {
            Bitmap bmp = new Bitmap(imageCtrl1.Width, imageCtrl1.Height);
            Graphics g = Graphics.FromImage(bmp);
            imageCtrl1.PaintOn(g);
            string fileprofilsourire = Path.GetTempFileName();
            bmp.Save(fileprofilsourire);
            return fileprofilsourire;
        }

        private void imageCtrl1_OnImageChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano = imageCtrl1.file;
        }

        private void radioButton3_Paint(object sender, PaintEventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox rb = ((CheckBox)sender);

                switch (rb.CheckState)
                {
                    case CheckState.Indeterminate:
                        e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
                        //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 255, 255, 255)), new Rectangle(0, 0, rb.Width, rb.Height));
                        break;
                    case CheckState.Checked: e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));
                        break;


                }
            }

            if (sender is RadioButton)
            {
                RadioButton rb = ((RadioButton)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));

            }
        }

        private void btnRisque_Click(object sender, EventArgs e)
        {
            FrmAddRisquesPerso frm = new FrmAddRisquesPerso(_CurrentPat);
            frm.ShowDialog();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tpAutre);
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = "Dents de sagesse";
        }

        private void rbDefinitive_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tpAutre);
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = "Dents définitives";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = "";
            if (!tabControl1.TabPages.Contains(tpAutre))
            {
                tabControl1.TabPages.Add(tpAutre);
                tabControl1.SelectedTab = tpAutre;
            }
        }

        private void chkbxEvolGermeDents_MouseDown(object sender, MouseEventArgs e)
        {
            if (((CheckBox)sender).CheckState == CheckState.Indeterminate)
                ((CheckBox)sender).CheckState = CheckState.Unchecked;
        }

        private void chkbxEvolGermeDents_MouseUp(object sender, MouseEventArgs e)
        {
            checkBox1_CheckedChanged(sender, new EventArgs());
        }

        //private void FrmAnalyse8_Load(object sender, EventArgs e)
        //{
        //    InitLoad();
        //    this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        //}

        private void barrePatient1_Load(object sender, EventArgs e)
        {

        }
    }
}
