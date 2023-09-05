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
    public partial class FrmAnalyse6 : FormScreen
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

        public FrmAnalyse6(basePatient pat)
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
        private void FrmAnalyse5_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                if (cd!=null)
                    lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);

            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectif cd in CurrentPat.SelectedObjectifs)
                lstBxObjectifs.Items.Add(cd);
            /*
            foreach (CommonObjectifFromDiag cd in lstobjs)
                if (!CurrentPat.SelectedObjectifs.Contains(cd.objectif))
                    lstBxObjectifs.Items.Add(cd);

            */


           
        }

        public void InitLoad()
        {

            tbLabial.Items.Add("1 mm");
            tbLabial.Items.Add("2 mm");
            tbLabial.Items.Add("3 mm");
            tbLabial.Items.Add("4 mm");
            tbLabial.Items.Add("5 mm");
            tbLabial.Items.Add("6 mm");
            tbLabial.Items.Add("7 mm");
            tbLabial.Items.Add("8 mm");
            tbLabial.Items.Add("9 mm");
            tbLabial.Items.Add("10 mm");
            tbLabial.Items.Add("11 mm");
            tbLabial.Items.Add("12 mm");
            tbLabial.Items.Add("13 mm");
            tbLabial.Items.Add("14 mm");
            tbLabial.Items.Add("15 mm");
            tbLabial.Items.Add("16 mm");
            tbLabial.Items.Add("17 mm");
            tbLabial.Items.Add("18 mm");
            tbLabial.Items.Add("19 mm");
            tbLabial.Items.Add("20 mm");
            tbLabial.Items.Add("21 mm");
            tbLabial.Items.Add("22 mm");
            tbLabial.Items.Add("23 mm");
            tbLabial.Items.Add("24 mm");
            tbLabial.Items.Add("25 mm");
            tbLabial.Items.Add("26 mm");
            tbLabial.Items.Add("27 mm");
            tbLabial.Items.Add("28 mm");
            tbLabial.Items.Add("29 mm");
            tbLabial.Items.Add("30 mm");
            tbLabial.Items.Add("31 mm");
            tbLabial.Items.Add("32 mm");
            tbLabial.Items.Add("33 mm");
            tbLabial.Items.Add("34 mm");
            tbLabial.Items.Add("35 mm");
            tbLabial.Items.Add("36 mm");
            tbLabial.Items.Add("37 mm");
            tbLabial.Items.Add("38 mm");
            tbLabial.Items.Add("39 mm");
            tbLabial.Items.Add("40 mm");
            tbLabial.Items.Add("41 mm");
            tbLabial.Items.Add("42 mm");
            tbLabial.Items.Add("43 mm");
            tbLabial.Items.Add("44 mm");
            tbLabial.Items.Add("45 mm");
            tbLabial.Items.Add("46 mm");
            tbLabial.Items.Add("47 mm");
            tbLabial.Items.Add("48 mm");
            tbLabial.Items.Add("49 mm");
            tbLabial.Items.Add("50 mm");


            tbGingivSup.Items.Add("1 mm");
            tbGingivSup.Items.Add("2 mm");
            tbGingivSup.Items.Add("3 mm");
            tbGingivSup.Items.Add("4 mm");
            tbGingivSup.Items.Add("5 mm");
            tbGingivSup.Items.Add("6 mm");
            tbGingivSup.Items.Add("7 mm");
            tbGingivSup.Items.Add("8 mm");
            tbGingivSup.Items.Add("9 mm");
            tbGingivSup.Items.Add("10 mm");
            tbGingivSup.Items.Add("11 mm");
            tbGingivSup.Items.Add("12 mm");
            tbGingivSup.Items.Add("13 mm");
            tbGingivSup.Items.Add("14 mm");
            tbGingivSup.Items.Add("15 mm");
            tbGingivSup.Items.Add("16 mm");
            tbGingivSup.Items.Add("17 mm");
            tbGingivSup.Items.Add("18 mm");
            tbGingivSup.Items.Add("19 mm");
            tbGingivSup.Items.Add("20 mm");
            tbGingivSup.Items.Add("21 mm");
            tbGingivSup.Items.Add("22 mm");
            tbGingivSup.Items.Add("23 mm");
            tbGingivSup.Items.Add("24 mm");
            tbGingivSup.Items.Add("25 mm");
            tbGingivSup.Items.Add("26 mm");
            tbGingivSup.Items.Add("27 mm");
            tbGingivSup.Items.Add("28 mm");
            tbGingivSup.Items.Add("29 mm");
            tbGingivSup.Items.Add("30 mm");
            tbGingivSup.Items.Add("31 mm");
            tbGingivSup.Items.Add("32 mm");
            tbGingivSup.Items.Add("33 mm");
            tbGingivSup.Items.Add("34 mm");
            tbGingivSup.Items.Add("35 mm");
            tbGingivSup.Items.Add("36 mm");
            tbGingivSup.Items.Add("37 mm");
            tbGingivSup.Items.Add("38 mm");
            tbGingivSup.Items.Add("39 mm");
            tbGingivSup.Items.Add("40 mm");
            tbGingivSup.Items.Add("41 mm");
            tbGingivSup.Items.Add("42 mm");
            tbGingivSup.Items.Add("43 mm");
            tbGingivSup.Items.Add("44 mm");
            tbGingivSup.Items.Add("45 mm");
            tbGingivSup.Items.Add("46 mm");
            tbGingivSup.Items.Add("47 mm");
            tbGingivSup.Items.Add("48 mm");
            tbGingivSup.Items.Add("49 mm");
            tbGingivSup.Items.Add("50 mm");


            tbGingivInf.Items.Add("1 mm");
            tbGingivInf.Items.Add("2 mm");
            tbGingivInf.Items.Add("3 mm");
            tbGingivInf.Items.Add("4 mm");
            tbGingivInf.Items.Add("5 mm");
            tbGingivInf.Items.Add("6 mm");
            tbGingivInf.Items.Add("7 mm");
            tbGingivInf.Items.Add("8 mm");
            tbGingivInf.Items.Add("9 mm");
            tbGingivInf.Items.Add("10 mm");
            tbGingivInf.Items.Add("11 mm");
            tbGingivInf.Items.Add("12 mm");
            tbGingivInf.Items.Add("13 mm");
            tbGingivInf.Items.Add("14 mm");
            tbGingivInf.Items.Add("15 mm");
            tbGingivInf.Items.Add("16 mm");
            tbGingivInf.Items.Add("17 mm");
            tbGingivInf.Items.Add("18 mm");
            tbGingivInf.Items.Add("19 mm");
            tbGingivInf.Items.Add("20 mm");
            tbGingivInf.Items.Add("21 mm");
            tbGingivInf.Items.Add("22 mm");
            tbGingivInf.Items.Add("23 mm");
            tbGingivInf.Items.Add("24 mm");
            tbGingivInf.Items.Add("25 mm");
            tbGingivInf.Items.Add("26 mm");
            tbGingivInf.Items.Add("27 mm");
            tbGingivInf.Items.Add("28 mm");
            tbGingivInf.Items.Add("29 mm");
            tbGingivInf.Items.Add("30 mm");
            tbGingivInf.Items.Add("31 mm");
            tbGingivInf.Items.Add("32 mm");
            tbGingivInf.Items.Add("33 mm");
            tbGingivInf.Items.Add("34 mm");
            tbGingivInf.Items.Add("35 mm");
            tbGingivInf.Items.Add("36 mm");
            tbGingivInf.Items.Add("37 mm");
            tbGingivInf.Items.Add("38 mm");
            tbGingivInf.Items.Add("39 mm");
            tbGingivInf.Items.Add("40 mm");
            tbGingivInf.Items.Add("41 mm");
            tbGingivInf.Items.Add("42 mm");
            tbGingivInf.Items.Add("43 mm");
            tbGingivInf.Items.Add("44 mm");
            tbGingivInf.Items.Add("45 mm");
            tbGingivInf.Items.Add("46 mm");
            tbGingivInf.Items.Add("47 mm");
            tbGingivInf.Items.Add("48 mm");
            tbGingivInf.Items.Add("49 mm");
            tbGingivInf.Items.Add("50 mm");




            imageCtrl1.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];

            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;



            imageCtrl1.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Portrait"] + "," + ConfigurationManager.AppSettings["Attr_Sourire"];

            imageCtrl1.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            imageCtrl1.zoomAuto();
            imageCtrl1.Center();
            barrePatient1.patient = CurrentPat;
            LoadPPT("Analyse5");


            InitDisplay();
        }

        private void InitDisplay()
        {
            chkbxGingivalSup.Checked = ResumeCliniqueMgmt.resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.Oui;
            chkbxGingivalInf.Checked = ResumeCliniqueMgmt.resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.Oui;
            chkbxLabial.Checked = ResumeCliniqueMgmt.resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui;

            chkbxNormal.Checked = (ResumeCliniqueMgmt.resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Non) &&
                                   (ResumeCliniqueMgmt.resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.Non) &&
                                   (ResumeCliniqueMgmt.resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.Non);

            tbGingivSup.SelectedIndex = ResumeCliniqueMgmt.resumeCl.GingivalSupValue;
            tbGingivInf.SelectedIndex = ResumeCliniqueMgmt.resumeCl.GingivalInfValue ;
            tbLabial.SelectedIndex = ResumeCliniqueMgmt.resumeCl.LabialValue;

            RegenerateDiagObjTraitmnt();
        }

        private void chkbxGingivalSup_CheckedChanged(object sender, EventArgs e)
        {
            
            ResumeCliniqueMgmt.resumeCl.SourireGingivalSup = chkbxGingivalSup.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui:BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            ResumeCliniqueMgmt.resumeCl.SourireGingivalInf = chkbxGingivalInf.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            ResumeCliniqueMgmt.resumeCl.SourireLabial = chkbxLabial.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            ResumeCliniqueMgmt.resumeCl.GingivalSupValue = (int)tbGingivSup.SelectedIndex+1;
            ResumeCliniqueMgmt.resumeCl.GingivalInfValue = (int)tbGingivInf.SelectedIndex + 1;
            ResumeCliniqueMgmt.resumeCl.LabialValue = (int)tbLabial.SelectedIndex + 1;

            RegenerateDiagObjTraitmnt();

            Refresh();
        }

        private void chkbxGingivalInf_Click(object sender, EventArgs e)
        {
            chkbxLabial.Checked = false;
            chkbxNormal.Checked = false;
            chkbxGingivalSup_CheckedChanged(sender,e);
        }

        private void chkbxLabial_Click(object sender, EventArgs e)
        {
            chkbxGingivalSup.Checked = false;
            chkbxGingivalInf.Checked = false;
            chkbxNormal.Checked = false;
            chkbxGingivalSup_CheckedChanged(sender, e);
        }

        private void chkbxNormal_Click(object sender, EventArgs e)
        {
            chkbxGingivalSup.Checked = false;
            chkbxGingivalInf.Checked = false;
            chkbxLabial.Checked = false;
            chkbxGingivalSup_CheckedChanged(sender, e);
        }

        private void FrmAnalyse5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse5IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse5AffectDefault();
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkbxGingivalSup_CheckedChanged_1(object sender, EventArgs e)
        {
            tbGingivSup.Enabled = chkbxGingivalSup.Checked;
            tbGingivInf.Enabled = chkbxGingivalInf.Checked;
            tbLabial.Enabled = chkbxLabial.Checked;
        }


        private void CheckDiags()
        {




            int[] AcceptedDiags = new int[] { 33, 34, 35, 36, 37 };


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
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseSourire"];
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

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ImageSourire", fileprofilsourire);


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
            imageCtrl1.PaintOn(g, new Rectangle(0, 0, imageCtrl1.Width, imageCtrl1.Height));
            string fileprofilsourire = Path.GetTempFileName();
            bmp.Save(fileprofilsourire);
            return fileprofilsourire;
        }

        private void imageCtrl1_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire = imageCtrl1.file;
        }

        private void chkbxLabial_Paint(object sender, PaintEventArgs e)
        {

            if ((ResumeCliniqueMgmt.resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.undefined) && (sender == chkbxGingivalSup))
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.undefined) && (sender == chkbxGingivalInf))
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined) && (sender == chkbxLabial))
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.undefined) && (sender == chkbxNormal))
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
                        
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
            base.OnPaint(e);
        }

        private void btnRisque_Click(object sender, EventArgs e)
        {
            FrmAddRisquesPerso frm = new FrmAddRisquesPerso(_CurrentPat);
            frm.ShowDialog();
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

            if (CurrentPat.SelectedObjectifs.Contains(obj))
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

        private void lstBxObjectifs_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void lstBxObjectifs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Refresh();
        }

        private void imageCtrl1_load(object sender, EventArgs e)
        {

        }
    }
}
