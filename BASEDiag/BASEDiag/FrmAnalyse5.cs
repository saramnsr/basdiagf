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
using BASEDiag.Ctrls;
using System.Configuration;
using BasCommon_BO;
using BasCommon_BL;
using System.IO;
using Microsoft.Win32;
namespace BASEDiag
{
    public partial class FrmAnalyse5 : FormScreen
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

        public FrmAnalyse5(basePatient pat)
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


        private void CheckDiags()
        {


            int[] AcceptedDiags = new int[] { 26, 27, 28, 29, 30, 31, 32, 48 };


            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (!AcceptedDiags.Contains(cd.Id)) continue;
                if (cd.SelectedObjectif) continue;

                FrmLittleWizard frm = new FrmLittleWizard(cd,CurrentPat);                
                if ((frm.CanBeShown) &&(frm.ShowDialog() == DialogResult.OK))
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

        private void FrmAnalyse4_Load(object sender, EventArgs e)
        {
            tbSurplomb.Items.Add("0 mm ");
            tbSurplomb.Items.Add("1 mm ");
            tbSurplomb.Items.Add("2 mm ");
            tbSurplomb.Items.Add("3 mm ");
            tbSurplomb.Items.Add("4 mm ");
            tbSurplomb.Items.Add("5 mm ");
            tbSurplomb.Items.Add("6 mm ");
            tbSurplomb.Items.Add("7 mm ");
            tbSurplomb.Items.Add("8 mm ");
            tbSurplomb.Items.Add("9 mm ");
            tbSurplomb.Items.Add("10 mm ");
            tbSurplomb.Items.Add("11 mm ");
            tbSurplomb.Items.Add("12 mm ");
            tbSurplomb.Items.Add("13 mm ");
            tbSurplomb.Items.Add("14 mm ");
            tbSurplomb.Items.Add("15 mm ");
            tbSurplomb.Items.Add("16 mm ");
            tbSurplomb.Items.Add("17 mm ");
            tbSurplomb.Items.Add("18 mm ");
            tbSurplomb.Items.Add("19 mm ");
            tbSurplomb.Items.Add("20 mm ");



            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        public void InitLoad()
        {
            barrePatient1.patient = CurrentPat;


            LoadPPT("Analyse4");
            initDisplay();

            TriImg1.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Maxillaire"];
            TriImg2.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Mandibulaire"];
            TriImg3.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Surplomb"];
            
            TriImg1.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg2.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg3.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg1.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Max);
            TriImg2.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Man);
            TriImg3.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_SurPlomb);



            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            InitPos();

            this.tabcontrol1.SelectedIndex = tabcontrol1.TabPages.Count - 1;

            rbPosterieur_CheckedChanged(this, new EventArgs());
        }

        private void InitPos()
        {
            int x = 0;
            int y = 0;
            TriImg1.Location = new Point(x, y);
            TriImg1.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height);

            x += tabTriImg.Width / 3;
            TriImg2.Location = new Point(x, y);
            TriImg2.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height);

            x += tabTriImg.Width / 3;
            TriImg3.Location = new Point(x, y);
            TriImg3.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height);

            TriImg1.zoomAuto();
            TriImg1.Center();
            TriImg2.zoomAuto();
            TriImg2.Center();
            TriImg3.zoomAuto();
            TriImg3.Center();
        }

        private void pnlCmd2_Paint(object sender, PaintEventArgs e)
        {

        }

        void initDisplay()
        {

            chkbxInterposPos.Checked = (ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur)||((ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur));
            chkbxInterposAnt.Checked = (ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur) || ((ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur));
            
            rbFormeU.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.U;
            rbFormeV.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.V;

            if (ResumeCliniqueMgmt.resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                ResumeCliniqueMgmt.resumeCl.DDM = BasCommon_BO.EntentePrealable.en_OuiNon.Oui;

            if (ResumeCliniqueMgmt.resumeCl.DDM != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) 
                chkbxDDM.CheckState = (ResumeCliniqueMgmt.resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;



            if (ResumeCliniqueMgmt.resumeCl.DDD != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) 
                chkBxDDD.CheckState = (ResumeCliniqueMgmt.resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;

            if (ResumeCliniqueMgmt.resumeCl.LangueBasse != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkbxLangueBas.CheckState = (ResumeCliniqueMgmt.resumeCl.LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;
            if (ResumeCliniqueMgmt.resumeCl.FreinLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkbxFreinLabial.CheckState = (ResumeCliniqueMgmt.resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;
            if (ResumeCliniqueMgmt.resumeCl.FreinLingual != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) 
                chkbxFreinLingual.CheckState  = (ResumeCliniqueMgmt.resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked:CheckState.Unchecked;

            tbSurplomb.SelectedIndex = ResumeCliniqueMgmt.resumeCl.SurplombValue;

            RegenerateDiagObjTraitmnt();
        }

        private void rbPosterieur_CheckedChanged(object sender, EventArgs e)
        {

            if (chkbxInterposPos.Checked && chkbxInterposAnt.Checked)
            {
                ResumeCliniqueMgmt.resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur;
            }
            else
            {
                if (!chkbxInterposPos.Checked && !chkbxInterposAnt.Checked) ResumeCliniqueMgmt.resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal;
                if (chkbxInterposPos.Checked) ResumeCliniqueMgmt.resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur;
                if (chkbxInterposAnt.Checked) ResumeCliniqueMgmt.resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur;
            }

            
            if (chkbxDDM.CheckState!=CheckState.Indeterminate)ResumeCliniqueMgmt.resumeCl.DDM = chkbxDDM.CheckState==CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (chkBxDDD.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.DDD = chkBxDDD.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;

            if (chkbxLangueBas.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.LangueBasse = chkbxLangueBas.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (chkbxFreinLabial.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.FreinLabial = chkbxFreinLabial.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (chkbxFreinLingual.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.FreinLingual = chkbxFreinLingual.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;

            if (rbFormeU.Checked) ResumeCliniqueMgmt.resumeCl.FormeArcade = BasCommon_BO.EntentePrealable.en_FormeArcade.U;
            if (rbFormeV.Checked) ResumeCliniqueMgmt.resumeCl.FormeArcade = BasCommon_BO.EntentePrealable.en_FormeArcade.V;

            ResumeCliniqueMgmt.resumeCl.SurplombValue = (int)tbSurplomb.SelectedIndex;

            RegenerateDiagObjTraitmnt();

           // Refresh();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmAnalyse4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse4IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.AnalyseFonctionnelAffectDefault();
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

        private void tabTriImg_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkbxInterposAnt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxInterposAnt_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void chkbxInterposPos_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        

        private void chkbxDDM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxLangueBas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxDysharmonie_CheckedChanged(object sender, EventArgs e)
        {

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
                //   this.WindowState = FormWindowState.Normal;

                CurrentScreenIdx++;
                CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;



                this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

                //   this.WindowState = FormWindowState.Maximized;
                this.Visible = true;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
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
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseArcades"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string fmaxilaire = GetAnalyseImageMax();

            string fmandibulaire = GetAnalyseImageMand();

            string fSurplomb = GetAnalyseImageSurplomb();

            /*
             public float EspaceDentaireBuccal = 0;
        public float IncisiveMolaireDroit = 0;
        public float IncisiveMolaireGauche = 0;
             */
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Maxilaire", fmaxilaire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Mandibulaire", fmandibulaire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Surplomb", fSurplomb);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        public string GetAnalyseImageSurplomb()
        {
            Bitmap bmp = new Bitmap(TriImg3.Width, TriImg3.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg3.PaintOn(g, new Rectangle(0, 0, TriImg3.Width, TriImg3.Height));
            string fSurplomb = Path.GetTempFileName();
            bmp.Save(fSurplomb);
            return fSurplomb;
        }

        public string GetAnalyseImageMand()
        {
            Bitmap bmp = new Bitmap(TriImg2.Width, TriImg2.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg2.PaintOn(g, new Rectangle(0, 0, TriImg2.Width, TriImg2.Height));
            string fmandibulaire = Path.GetTempFileName();
            bmp.Save(fmandibulaire);
            return fmandibulaire;
        }

        public string GetAnalyseImageMax()
        {
            Bitmap bmp = new Bitmap(TriImg1.Width, TriImg1.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg1.PaintOn(g, new Rectangle(0, 0, TriImg1.Width, TriImg1.Height));
            string fmaxilaire = Path.GetTempFileName();
            bmp.Save(fmaxilaire);
            return fmaxilaire;
        }

        private void TriImg3_OnRadioChanged(object sender, EventArgs e)
        {
            
            ResumeCliniqueMgmt.resumeCl.Img_Int_SurPlomb = TriImg3.file;
        }

        private void TriImg2_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Int_Man = TriImg2.file;
        }

        private void TriImg1_OnRadioChanged(object sender, EventArgs e)
        {

            ResumeCliniqueMgmt.resumeCl.Img_Int_Max = TriImg1.file;
        }

        private void chkbxFreinLabial_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbFormeU_Paint(object sender, PaintEventArgs e)
        {
            if (ResumeCliniqueMgmt.resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.undefined)
            {
                if ((sender == rbFormeU) || (sender == rbFormeV))
                { e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0)); return; }

            }

           


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

        private void chkbxDDM_MouseDown(object sender, MouseEventArgs e)
        {
            if (((CheckBox)sender).CheckState == CheckState.Indeterminate)
                ((CheckBox)sender).CheckState = CheckState.Unchecked;
           
        }

        private void chkbxLangueBas_MouseUp(object sender, MouseEventArgs e)
        {           

            rbPosterieur_CheckedChanged(sender, new EventArgs());
        }

        bool MiniatureIsCollapsed = true;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point pt = PointToScreen(pictureBox1.Location);

            double ratio = (float)pictureBox1.Image.Width / pictureBox1.Image.Height;

            if (MiniatureIsCollapsed)
            {
                int w = tabcontrol1.Width;
                pictureBox1.Size = new Size(w, (int)(w / ratio));
                MiniatureIsCollapsed = false;
            }
            else
            {
                int w = 100;
                pictureBox1.Size = new Size(w, (int)(w / ratio));
                MiniatureIsCollapsed = true;
            }
            /*
            Point pt = PointToScreen(pictureBox1.Location);

            double ratio = (float)pictureBox1.Image.Width / pictureBox1.Image.Height;

            if (MiniatureIsCollapsed)
            {
                int h = Screen.GetWorkingArea(this).Height - pt.Y;
                pictureBox1.Size = new Size((int)(h * ratio), h);
                MiniatureIsCollapsed = false;
            }
            else
            {
                int h = 100;
                pictureBox1.Size = new Size((int)(h * ratio), h);
                MiniatureIsCollapsed = true;
            }
             */
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

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Refresh();
        }

        private void TriImg1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((ImageCtrlAgg)sender).Parent == this.pnlFullScreen)
            {
                pnlFullScreen.Visible = false;
                this.pnlFullScreen.Controls.Remove((ImageCtrlAgg)sender);
                this.tabTriImg.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.tabTriImg;
                ((ImageCtrlAgg)sender).Dock = DockStyle.None;
                InitPos();
            }
            else
            {

                this.tabTriImg.Controls.Remove((ImageCtrlAgg)sender);
                this.pnlFullScreen.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.pnlFullScreen;
                ((ImageCtrlAgg)sender).Dock = DockStyle.Fill;
                pnlFullScreen.Visible = true;
                ((ImageCtrlAgg)sender).zoomAuto();
                ((ImageCtrlAgg)sender).Center();
            }
        }

        private void tbSurplomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, new EventArgs());
        }
    }
}
