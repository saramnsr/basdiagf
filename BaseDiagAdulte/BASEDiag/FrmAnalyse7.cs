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
using BasCommon_BO;
using BasCommon_BL;
using System.IO;

namespace BASEDiagAdulte
{
    public partial class FrmAnalyse7 : FormScreen
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


        public FrmAnalyse7(basePatient pat)
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

        private void FrmAnalyse6_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        public void InitLoad()
        {


            ImgProfilSourire.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Portrait"] + "," + ConfigurationManager.AppSettings["Attr_Sourire"] + "," + ConfigurationManager.AppSettings["Attr_Profil"];
            

            ImgProfilSourire.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            LoadPPT("Analyse6");

            InitDisplay();
            barrePatient1.patient = CurrentPat;
            /*
            ImgProfil.loadImage(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            ImgProfil.OnSaisie += new EventHandler(ImgProfil_OnSaisie);
            ImgProfil.zoomAuto();
            ImgProfil.Center();
            ImgProfil.OnEndSaisie += new EventHandler(ImgProfil_OnEndSaisie);
             * */
            ImgProfilSourire.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            ImgProfilSourire.zoomAuto();
            ImgProfilSourire.Center();
            ImgProfilSourire.OnEndSaisie += new EventHandler(ImgProfil_OnEndSaisie);

            //ImgProfil.StartSaisie();
            ImgProfilSourire.StartSaisie();

            //ImgProfil.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse6;
            ImgProfilSourire.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse62;


            //TriImg1.loadImage(ImgProfil.OriginalImage);
            //TriImg2.loadImage(ImgProfilSourire.OriginalImage);


            // this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;


        }

       
       
        void InitDisplay()
        {
            rbLevreSupRetro.Checked = ResumeCliniqueMgmt.resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbLevreSupNormo.Checked = ResumeCliniqueMgmt.resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            rbLevreSupPro.Checked = ResumeCliniqueMgmt.resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            rbLevreInfRetro.Checked = ResumeCliniqueMgmt.resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbLevreInfNormo.Checked = ResumeCliniqueMgmt.resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            rbLevreInfPro.Checked = ResumeCliniqueMgmt.resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            rbMentonRetro.Checked = ResumeCliniqueMgmt.resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbMentonNormo.Checked = ResumeCliniqueMgmt.resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            rbMentonPro.Checked = ResumeCliniqueMgmt.resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            rbIncisiveSupRetro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            rbIncisiveSupNormo.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            rbIncisiveSupPro.Checked = ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro;



            RegenerateDiagObjTraitmnt();
        }
       
        void ImgProfil_OnEndSaisie(object sender, EventArgs e)
        {
            RegenerateDiagObjTraitmnt();

            ResumeCliniqueMgmt.ConvertFromAnalyse6(ImgProfilSourire.LevreSupRes,
                ImgProfilSourire.LevreInfRes,
                ImgProfilSourire.MentonRes,
                ImgProfilSourire.IncisiveSupRes,
                ImgProfilSourire.InclinaisonRes,
                ImgProfilSourire.Inclinaison);

            //ResumeCliniqueMgmt.resumeCl.LstPtAnalyse6 = ImgProfil.ListOfPoints;
            ResumeCliniqueMgmt.resumeCl.LstPtAnalyse62 = ImgProfilSourire.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        private void FrmAnalyse6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.No)
            {
                if (ResumeCliniqueMgmt.Analyse6IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse6AffectDefault();
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void rbLevreInfRetro_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLevreSupRetro.Checked) ResumeCliniqueMgmt.resumeCl.LevreSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbLevreSupNormo.Checked) ResumeCliniqueMgmt.resumeCl.LevreSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (rbLevreSupPro.Checked) ResumeCliniqueMgmt.resumeCl.LevreSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            if (rbLevreInfRetro.Checked) ResumeCliniqueMgmt.resumeCl.LevreInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbLevreInfNormo.Checked) ResumeCliniqueMgmt.resumeCl.LevreInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (rbLevreInfPro.Checked) ResumeCliniqueMgmt.resumeCl.LevreInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            if (rbMentonRetro.Checked) ResumeCliniqueMgmt.resumeCl.Menton = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbMentonNormo.Checked) ResumeCliniqueMgmt.resumeCl.Menton = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (rbMentonPro.Checked) ResumeCliniqueMgmt.resumeCl.Menton = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            if (rbIncisiveSupRetro.Checked) ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (rbIncisiveSupNormo.Checked) ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (rbIncisiveSupPro.Checked) ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            RegenerateDiagObjTraitmnt();

            Refresh();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbIncliIncRetro_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportToLetter(true);
        }

        public void ExportToLetter(bool DirectPrint)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseProfil"];
            if (!File.Exists(file))
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LevreSup", ImgProfilSourire.LevreSupRes);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LevreInf", ImgProfilSourire.LevreInfRes);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Menton", ImgProfilSourire.MentonRes);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveSup", ImgProfilSourire.IncisiveSupRes);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittal", ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensVertical", ResumeCliniqueMgmt.resumeCl.SensVertical.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ImageProfil", fileprofil);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ImageProfilSourire", fileprofilsourire);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        public string GetAnalyseImage()
        {
            Bitmap bmp = new Bitmap(ImgProfilSourire.Width, ImgProfilSourire.Height);
            Graphics g = Graphics.FromImage(bmp);
            ImgProfilSourire.PaintOn(g, new Rectangle(0, 0, ImgProfilSourire.Width, ImgProfilSourire.Height),true);
            string fileprofilsourire = Path.GetTempFileName();
            bmp.Save(fileprofilsourire);
            return fileprofilsourire;
        }


        private void CheckDiags()
        {




            int[] AcceptedDiags = new int[] { 6, 8, 9, 10, 11, 12, 13 };

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

        private void ImgProfilSourire_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire = ImgProfilSourire.file;
        }

        private void rbIncisiveSupPro_Paint(object sender, PaintEventArgs e)
        {

            if ((ResumeCliniqueMgmt.resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbLevreInfPro) || (sender == rbLevreInfNormo) || (sender == rbLevreInfRetro))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbLevreSupPro) || (sender == rbLevreSupNormo) || (sender == rbLevreSupRetro))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbMentonPro) || (sender == rbMentonNormo) || (sender == rbMentonRetro))
                    e.Graphics.DrawImage(global::BASEDiagAdulte.Properties.Resources.Interogation, new Point(0, 0));
            if ((ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                if ((sender == rbIncisiveSupPro) || (sender == rbIncisiveSupNormo) || (sender == rbIncisiveSupRetro))
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

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                if (cd!=null) 
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

    }
}
