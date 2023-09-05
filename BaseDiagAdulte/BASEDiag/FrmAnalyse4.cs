using BasCommon_BL;
using BasCommon_BO;
using BASEDiag_BL;
using BASEDiag_BO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmAnalyse4 : BASEDiagAdulte.FormScreen
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

        public FrmAnalyse4(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;
        }



        private void FrmFonctionnel_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;



        }

        private void InitDisplay()
        {
            //throw new NotImplementedException();

            chkbxInterposPos.Checked = (ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur) || ((ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur));
            chkbxInterposAnt.Checked = (ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur) || ((ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur));

            tendbucc.Checked = ResumeCliniqueMgmt.resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.buccale;
            buccexc.Checked = ResumeCliniqueMgmt.resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.exclusive;

            chkbxLaterolDeviationFonctionnel.Checked = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel);
            chkbxLaterodeviationRC.Checked = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM);

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



            if (tendbucc.Checked) ResumeCliniqueMgmt.resumeCl.FormeRespiration = BasCommon_BO.EntentePrealable.en_Respiration.buccale;
            if (buccexc.Checked) ResumeCliniqueMgmt.resumeCl.FormeRespiration = BasCommon_BO.EntentePrealable.en_Respiration.exclusive;

            if (chkbxLaterolDeviationFonctionnel.Checked && chkbxLaterodeviationRC.Checked)
            {
                ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel_Et_RCetOIM;
            }
            else
            {
                if (!chkbxLaterolDeviationFonctionnel.Checked && !chkbxLaterodeviationRC.Checked) ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel;
                if (chkbxLaterolDeviationFonctionnel.Checked) ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel;
                if (chkbxLaterodeviationRC.Checked) ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM;
            }
            //if ( chkbxLaterolDeviationFonctionnel.Checked = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel) || (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel_Et_RCetOIM)) ;
            // if( chkbxLaterodeviationRC.Checked = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM) || (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel_Et_RCetOIM)) ;

            //if (chkbxLaterolDeviationFonctionnel.Checked) ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel;
            //if (chkbxLaterodeviationRC.Checked) ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM;

            //ResumeCliniqueMgmt.resumeCl.SurplombValue = (int)tbSurplomb.SelectedIndex;

            RegenerateDiagObjTraitmnt();
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


        private void InitLoad()
        {
            barrePatient1.patient = CurrentPat;
            //TriImg1.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Maxillaire"];
            //TriImg2.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Mandibulaire"];
         TriImg1.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Portrait"] + "," + ConfigurationManager.AppSettings["Attr_Face"];
           
        TriImg2.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Face"];


            TriImg1.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
          TriImg2.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Face);

           TriImg1.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
         //  TriImg1.resumeclinique = ResumeCliniqueMgmt.resumeCl;

           TriImg2.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
       //   TriImg2.resumeclinique = ResumeCliniqueMgmt.resumeCl;



            
         
            LoadPPT("Fonctionnel");
            InitDisplay();
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;
            InitPos();

            TriImg1.zoomAuto();
            TriImg1.Center();
            TriImg2.zoomAuto();
            TriImg2.Center();

        }
        private void InitPos()
        {
            int x = 0;
            int y = 0;
            TriImg1.Location = new Point(x, y);
            TriImg1.Size = new Size(tabTriImg.Width / 2, tabTriImg.Height);

            x += tabTriImg.Width / 2;
            TriImg2.Location = new Point(x, y);
            TriImg2.Size = new Size(tabTriImg.Width / 2, tabTriImg.Height);

            //  x += tabTriImg.Width / 2;
            // TriImg3.Location = new Point(x, y);
            //   TriImg3.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height - 250);

            TriImg1.zoomAuto();
            TriImg1.Center();
            TriImg2.zoomAuto();
            TriImg2.Center();
            // TriImg3.zoomAuto();
            //  TriImg3.Center();
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

                  this.WindowState = FormWindowState.Maximized;
                this.Visible = true;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRisque_Click(object sender, EventArgs e)
        {
            FrmAddRisquesPerso frm = new FrmAddRisquesPerso(_CurrentPat);
            frm.ShowDialog();
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
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseOcclusal"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string ffGauche = GetAnalyseImageFFace();

            string ffFace = GetAnalyseImageFFace();




            /*
             public float EspaceDentaireBuccal = 0;
        public float IncisiveMolaireDroit = 0;
        public float IncisiveMolaireGauche = 0;
             */
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFGauche", ffGauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseFFace", ffFace);


            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }


        public string GetAnalyseImageFFace()
        {
            Bitmap bmp = new Bitmap(TriImg2.Width, TriImg2.Height);
            Graphics g = Graphics.FromImage(bmp);
        //TriImg2.PaintOn(g, new Rectangle(0, 0, TriImg2.Width, TriImg2.Height), true);
        string fSurplomb = Path.GetTempFileName();
        bmp.Save(fSurplomb);
        return fSurplomb;
        }

        public string GetAnalyseFImage()
        {
            Bitmap bmp = new Bitmap(TriImg1.Width, TriImg1.Height);
            Graphics g = Graphics.FromImage(bmp);
           //TriImg1.PaintOn(g, new Rectangle(0, 0, TriImg1.Width, TriImg1.Height));
           string fmandibulaire = Path.GetTempFileName();
           bmp.Save(fmandibulaire);
           return fmandibulaire;
        }
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        void TriImg1_OnEndSaisie(object sender, EventArgs e)
        {

           // ResumeCliniqueMgmt.resumeCl.LstPtAnalyse1 = TriImg1.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        void TriImg2_OnEndSaisie(object sender, EventArgs e)
        {
         //   ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccF = TriImg2.ListOfPoints;

            InitDisplay();
            Refresh();
        }
        private void BTnNext_Click(object sender, EventArgs e)
        {
            //CheckDiags();
            //DialogResult = DialogResult.Yes;
            //Close();
        }

        private void barrePatient1_Load(object sender, EventArgs e)
        {

        }

        private void FrmFonctionnel_Load_1(object sender, EventArgs e)
        {

        }

        //private void FrmFonctionnel_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (DialogResult == System.Windows.Forms.DialogResult.Yes)
        //        {
        //      if (ResumeCliniqueMgmt.AnalyseFonctionnelIsValid != "")
        //       {
        //           DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.AnalyseFonctionnelIsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

        //          if (dr == DialogResult.Yes)
        //          {
        //              ResumeCliniqueMgmt.Analyse3AffectDefault();
        //          }
        //          if (dr == DialogResult.Cancel)
        //       {
        //             e.Cancel = true;
        //          }
        //      }
        //   }
        //}

        private void chkbxInterposAnt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxInterposAnt_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void chkbxInterposPos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxInterposPos_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void BTnNext_Click_1(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void CheckDiags()
        {

            //int[] AcceptedDiags = new int[] { 26, 27, 28, 29, 30, 31, 32, 48 };


            //foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            //{
            //    if (AcceptedDiags.Contains(cd.Id))
            //    {
            //        if (cd.SelectedObjectif) continue;

            //        FrmLittleWizard frm = new FrmLittleWizard(cd, CurrentPat);
            //        if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
            //        {
            //            foreach (CommonObjectifFromDiag co in frm.values)
            //            {
            //                if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
            //                    CurrentPat.SelectedObjectifs.Add(co.objectif);
            //            }
            //            cd.SelectedObjectif = true;
            //        }
            //    }
            //}
        }
        private void BtnPrevious_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
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

        private void TriImg2_OnRadioChanged(object sender, EventArgs e)
        {
          //  ResumeCliniqueMgmt.resumeCl.Img_Int_Face = TriImg2.file;
        }

        private void TriImg1_OnRadioChanged(object sender, EventArgs e)
        {
           // ResumeCliniqueMgmt.resumeCl.Img_Ext_Face = TriImg1.file;
        }


        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Refresh();
        }

        //private void FrmAnalyseFonctionnel_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        //{
        // if (DialogResult == System.Windows.Forms.DialogResult.Yes)
        //{
        //   if (ResumeCliniqueMgmt.AnalyseFonctionnelIsValid != "")
        //    {
        //        DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

        //        if (dr == DialogResult.Yes)
        //        {
        //           ResumeCliniqueMgmt.AnalyseFonctionnelAffectDefault();
        //        }
        //        if (dr == DialogResult.Cancel)
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}
        // }



        private void FrmFonctionnel_Resize(object sender, System.EventArgs e)
        {
            InitPos();
        }

        private void buccexc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tendbuccale__CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tendbuccale_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void buccexc_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void chkbxLaterolDeviationFonctionnel_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void chkbxLaterolDeviationFonctionnel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxLaterodeviationRC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxLaterodeviationRC_Click(object sender, EventArgs e)
        {
            rbPosterieur_CheckedChanged(sender, e);
        }

        private void rbFormeU_Paint(object sender, PaintEventArgs e)
        {
            if (ResumeCliniqueMgmt.resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
            {
                if ((sender == chkbxInterposAnt) || (sender == chkbxInterposPos))
                { e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0)); return; }

            }




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
            base.OnPaint(e);
        }

        private void frmFonct_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse4IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.AnalyseFonctionnelIsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lblTitre_Click(object sender, EventArgs e)
        {

        }

        private void pnlFullScreen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstBxObjectifs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvPPT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TriImg2_Load(object sender, EventArgs e)
        {

        }

        private void TriImg1_Load(object sender, EventArgs e)
        {

        }

        private void imageCtrl1_Load(object sender, EventArgs e)
        {

        }

        private void BTnNext_Click_2(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void barrePatient1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void MolGClasseI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbOccNormal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TriImg1_OnEndSaisie_1(object sender, EventArgs e)
        {

        }

        private void TriImg1_Load_1(object sender, EventArgs e)
        {

        }

        private void lstBxObjectifs_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}
