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

namespace BASEDiagAdulte
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
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            
            imageCtrl1.loadImage(ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            imageCtrl1.zoomAuto();
            imageCtrl1.Center();

            barrePatient1.patient = CurrentPat;
            LoadPPT("Analyse8");


            InitDisplay();
        }

        private void FrmAnalyse8_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.No)
            {
                if (ResumeCliniqueMgmt.Analyse8IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse8IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

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

            if (chkbxEvolGermeDents.Checked)
            {
                if (radioButton3.Checked)
                {
                    BaseCommonControls.FrmChoixDents frmcd = new BaseCommonControls.FrmChoixDents();
                    Screen s = Screen.FromPoint(this.Location);
                    frmcd.Location = new Point(s.Bounds.Right - frmcd.Width - 5, s.Bounds.Bottom - frmcd.Height - 5);

                    frmcd.value = ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur;
                    frmcd.Text = "Manque de place pour l'evolution des germes des dents :";
                    if (frmcd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = frmcd.value;
                    }
                }

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
           
        }

        

      

        void InitDisplay()
        {
            cdentControle.SelectedDents = ResumeCliniqueMgmt.resumeCl.Controle;
            cdentAgenesie.SelectedDents = ResumeCliniqueMgmt.resumeCl.Agenesie;
            cdentincluses.SelectedDents = ResumeCliniqueMgmt.resumeCl.DentsIncluses;
            cdentSurnum.SelectedDents = ResumeCliniqueMgmt.resumeCl.DentsSurnumeraires;
           

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



        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void CheckDiags()
        {
            int[] AcceptedDiags = new int[] { 19, 75, 76 };

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


            BaseCommonControls.FrmChoixDents frmcd = new BaseCommonControls.FrmChoixDents();
            Screen s = Screen.FromPoint(this.Location);
            frmcd.Location = new Point(s.Bounds.Right - frmcd.Width - 5, s.Bounds.Bottom - frmcd.Height - 5);
            frmcd.value = ResumeCliniqueMgmt.resumeCl.DentsDeSagesse;
            frmcd.Text = "Dents à extraire";
            if (frmcd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ResumeCliniqueMgmt.resumeCl.DentsDeSagesse = frmcd.value;
            }

            frmcd = new BaseCommonControls.FrmChoixDents();
            frmcd.Location = new Point(s.Bounds.Right - frmcd.Width - 5, s.Bounds.Bottom - frmcd.Height - 5);
            frmcd.value = ResumeCliniqueMgmt.resumeCl.NoTaquets;
            frmcd.Text = "Ne pas placer de Taquets sur les dents : ";
            if (frmcd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ResumeCliniqueMgmt.resumeCl.NoTaquets = frmcd.value;
            }

            frmcd = new BaseCommonControls.FrmChoixDents();
            frmcd.Location = new Point(s.Bounds.Right - frmcd.Width - 5, s.Bounds.Bottom - frmcd.Height - 5);
            frmcd.value = ResumeCliniqueMgmt.resumeCl.NoMvts;
            frmcd.Text = "Ne pas déplacer les dents suivantes :";
            if (frmcd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ResumeCliniqueMgmt.resumeCl.NoMvts = frmcd.value;
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
            ExportToLetter(true);
        }

        public void ExportToLetter(bool DirectPrint)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalysePano"];
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

        private void radioButton2_Click(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = "Dents de sagesse";
        }

        private void rbDefinitive_Click(object sender, EventArgs e)
        {
           
            ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = "Dents définitives";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            BaseCommonControls.FrmChoixDents frmcd = new BaseCommonControls.FrmChoixDents();
            frmcd.value = ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur;
            frmcd.Text = "Manque de place pour l'evolution des germes des dents :";
            if (frmcd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur = frmcd.value;
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

        private void cdentControle_MouseUp(object sender, MouseEventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Controle = cdentControle.SelectedDents;
        }
    
    }
}
