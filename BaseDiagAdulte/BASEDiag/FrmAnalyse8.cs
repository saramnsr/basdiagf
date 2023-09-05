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
namespace BASEDiagAdulte
{
    public partial class FrmAnalyse8 : FormScreen
    {

        bool IWantSynchro = true;


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

        public FrmAnalyse8(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;

        }

        private void CheckDiags()
        {

            // 14 = Pas d'objectifs pour classe 1
            int[] AcceptedDiags = new int[] { 15, 16, 17, 18 };


            ResumeCliniqueMgmt.resumeCl.diagnostics.Sort();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (!AcceptedDiags.Contains(cd.Id)) continue;

                FrmLittleWizard frm = new FrmLittleWizard(cd, CurrentPat);
                if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                {
                    foreach (CommonObjectifFromDiag co in frm.UnChecked)
                    {
                        if (CurrentPat.SelectedObjectifs.Contains(co.objectif))
                            CurrentPat.SelectedObjectifs.Remove(co.objectif);
                    }
                    foreach (CommonObjectifFromDiag co in frm.values)
                    {
                        if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
                            CurrentPat.SelectedObjectifs.Add(co.objectif);
                    }


                }
            }
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

        private void FrmAnalyse7_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                if (cd != null)
                    lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifs(ResumeCliniqueMgmt.resumeCl.diagnostics);

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
            analyse71.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;


            analyse71.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Radio"] + "," + ConfigurationManager.AppSettings["Attr_Profil"];


            analyse71.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            analyse71.loadPhoto(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            analyse71.Transparence = 1f;
            analyse71.zoomAuto();
            analyse71.Center();
            analyse71.OnEndSaisie += new EventHandler(analyse71_OnEndSaisie);
            analyse71.OnSaisie += new EventHandler(analyse71_OnSaisie);

            analyse71.StartSaisie();
            LoadPPT("Analyse7");


            InitDisplay();

            analyse71.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse7;

            barrePatient1.patient = CurrentPat;


            if (ResumeCliniqueMgmt.resumeCl.IsSynchronized)
            {
                analyse71.SynchroRadioPhoto(ResumeCliniqueMgmt.resumeCl.synchrozoom,
                                    ResumeCliniqueMgmt.resumeCl.synchroangle,
                                    ResumeCliniqueMgmt.resumeCl.synchrooffset);
                analyse71.Transparence = .5;
            }


        }

        void analyse71_OnSaisie(object sender, EventArgs e)
        {




        }


        void InitDisplay()
        {
            rbSensSagittalCI.Checked = ResumeCliniqueMgmt.resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_I;
            rbSensSagittalCII.Checked = ResumeCliniqueMgmt.resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_II;
            rbSensSagittalCIII.Checked = ResumeCliniqueMgmt.resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_III;

            rbSensVertHyper.Checked = ResumeCliniqueMgmt.resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent;
            rbSensVertHypo.Checked = ResumeCliniqueMgmt.resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent;
            rbSensVertNormo.Checked = ResumeCliniqueMgmt.resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Normodivergent;


            rbIncSupPro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;
            rbIncSupRetro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbIncSupNormo.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;

            rbIncSupInfPro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;
            rbIncSupInfRetro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbIncSupInfNormo.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;

            RegenerateDiagObjTraitmnt();
        }

        void analyse71_OnEndSaisie(object sender, EventArgs e)
        {
            RegenerateDiagObjTraitmnt();
            ResumeCliniqueMgmt.ConvertFromAnalyse7(analyse71.FMA, analyse71.ANB, analyse71.SNA, analyse71.SNB, analyse71.IF, analyse71.IM, analyse71.SPP);

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyse7 = analyse71.ListOfPoints;

            InitDisplay();


            if ((IWantSynchro) && (!analyse71.Synchronized) && (analyse71.PhotoImage != null) && (analyse71.RadioImage != null))
            {
                if (MessageBox.Show("Souhaitez-vous synchroniser la radio avec la photo ?\n(Appuyer sur 's' pour le faire plus tard)", "Synchroniser?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    analyse71.StartSynchro();
                    analyse71.ShowLabel = true;
                }
                else
                    IWantSynchro = false;
            }
            Refresh();
        }

        void FrmAnalyse7_Resize(object sender, System.EventArgs e)
        {
            analyse71.zoomAuto();
            analyse71.Center();
        }



        private void rbSensVertHypo_Click(object sender, EventArgs e)
        {
            if (rbSensSagittalCI.Checked)
                ResumeCliniqueMgmt.resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (rbSensSagittalCII.Checked)
                ResumeCliniqueMgmt.resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_II;
            if (rbSensSagittalCIII.Checked)
                ResumeCliniqueMgmt.resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_III;

            if (rbSensVertHyper.Checked)
                ResumeCliniqueMgmt.resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent;
            if (rbSensVertHypo.Checked)
                ResumeCliniqueMgmt.resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent;
            if (rbSensVertNormo.Checked)
                ResumeCliniqueMgmt.resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Normodivergent;


            if (rbIncSupPro.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;
            if (rbIncSupRetro.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbIncSupNormo.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;

            if (rbIncSupInfPro.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;
            if (rbIncSupInfRetro.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbIncSupInfNormo.Checked)
                ResumeCliniqueMgmt.resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;


            Refresh();


            RegenerateDiagObjTraitmnt();
        }

        private void FrmAnalyse7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.No)
            {
                if (ResumeCliniqueMgmt.Analyse7IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse7AffectDefault();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportToLetter(true);
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



            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseCephalo"];
            if (!File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string f = GetAnalyseImage();


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FMA", analyse71.FMA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNA", analyse71.SNA.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SNB", analyse71.SNB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ANB", analyse71.ANB.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IF", analyse71.IF.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IM", analyse71.IM.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("I2F", analyse71.I2F.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittal", ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensVertical", ResumeCliniqueMgmt.resumeCl.SensVertical.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseCephalo", f);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();
           //System.Threading.Thread.Sleep(3000);
            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        public string GetAnalyseImage()
        {
            Bitmap bmp = new Bitmap(analyse71.Width, analyse71.Height);
            Graphics g = Graphics.FromImage(bmp);
            analyse71.PaintOn(g, new Rectangle(0, 0, analyse71.Width, analyse71.Height), true);
            string f = Path.GetTempFileName();
            f = f.Replace("tmp","jpg");
            bmp.Save(f);
            return f;
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

        private void analyse71_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile = analyse71.file;
        }

        private void FrmAnalyse7_Shown(object sender, EventArgs e)
        {
           // ExportToLetter(true);
        }

        private void analyse71_OnEndSynchro(object sender, EventArgs e)
        {
            EndSynchro();
        }

        private void EndSynchro()
        {
            ResumeCliniqueMgmt.resumeCl.IsSynchronized = true;
            ResumeCliniqueMgmt.resumeCl.synchrozoom = analyse71.zoomPhoto;
            ResumeCliniqueMgmt.resumeCl.synchroangle = analyse71.AngleDeRotationPhoto;
            ResumeCliniqueMgmt.resumeCl.synchrooffset = analyse71.OffsetPhoto;
            btnPlus.Visible = true;
            btnmoins.Visible = true;
            analyse71.Transparence = .5;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            analyse71.Transparence += .1;
        }

        private void btnmoins_Click(object sender, EventArgs e)
        {
            analyse71.Transparence -= .1;
        }

        private void rbSensVertHypo_Paint(object sender, PaintEventArgs e)
        {
            if ((ResumeCliniqueMgmt.resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.undefined))
                if ((sender == rbSensVertHyper) || (sender == rbSensVertHypo) || (sender == rbSensVertNormo))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.undefined))
                if ((sender == rbSensSagittalCI) || (sender == rbSensSagittalCII) || (sender == rbSensSagittalCIII))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbIncSupRetro) || (sender == rbIncSupNormo) || (sender == rbIncSupPro))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbIncSupInfRetro) || (sender == rbIncSupInfNormo) || (sender == rbIncSupInfPro))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));


            if (sender is CheckBox)
            {
                CheckBox rb = ((CheckBox)sender);

                switch (rb.CheckState)
                {
                    case CheckState.Indeterminate:
                        e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
                        //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 255, 255, 255)), new Rectangle(0, 0, rb.Width, rb.Height));
                        break;
                    case CheckState.Checked: e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.check, new Point(0, 0));
                        break;


                }
            }

            if (sender is RadioButton)
            {
                RadioButton rb = ((RadioButton)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.check, new Point(0, 0));

            }
        }

        private void btnRisque_Click(object sender, EventArgs e)
        {
            FrmAddRisquesPerso frm = new FrmAddRisquesPerso(_CurrentPat);
            frm.ShowDialog();
        }
        private static bool exist(string path)
        {
            try
            {
                var req = System.Net.WebRequest.Create(path);
                using (System.IO.Stream stream = req.GetResponse().GetResponseStream())
                {

                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        private void btnEnvoie_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["PHOTO_FOLDER_PATH" + prefix];
                path += "/" + CurrentPat.Repertoire;
                ImagesMgmt.CreatePatientDossier(path);
                Uri url = new Uri(path);
                string pathTest = url.AbsolutePath;
                pathTest = pathTest.Substring(1, pathTest.Length - 1);
                Bitmap bmp = new Bitmap(analyse71.Width, analyse71.Height);
                Graphics g = Graphics.FromImage(bmp);
                analyse71.PaintOn(g, new Rectangle(0, 0, analyse71.Width, analyse71.Height), true);
                string f = Path.GetTempFileName();
                f = Path.GetFileNameWithoutExtension(f);
                f = f + ".jpg";


                Byte[] bytes = ImagesMgmt.ImageToByte(bmp);
                ImagesMgmt.saveImage(bytes, f, pathTest);
                BasCommon_BL.ImagesMgmt.saveToBasView(bytes, CurrentPat.Id, f, path + "/" + f, path);
                MessageBox.Show("Image envoyé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            analyse71.StartSynchro();
        }

        private void analyse71_Load(object sender, EventArgs e)
        {
            
        }

    }
}
