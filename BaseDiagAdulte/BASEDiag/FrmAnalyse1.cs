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
    public partial class FrmAnalyse1 : FormScreen
    {

        //"analyse Masque facial"


        

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

        public FrmAnalyse1(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;
            
        }

        private void FrmAnalyse1_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;

        }

        public void InitLoad()
        {
            analyse11.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];

            analyse11.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Portrait"] + "," + ConfigurationManager.AppSettings["Attr_Face"];

            analyse11.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
            analyse11.zoomAuto();
            analyse11.Center();
            analyse11.StartSaisie();
            analyse11.OnEndSaisie += new EventHandler(analyse11_OnEndSaisie);




            LoadPPT("Analyse1");



            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            InitDisplay();

            analyse11.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyse1;

            barrePatient1.patient = CurrentPat;
        }

        void analyse11_OnEndSaisie(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.ConvertFromAnalyse1(analyse11.EtageSup, analyse11.EtageMoy, analyse11.EtageInf, analyse11.EtageInfSup, analyse11.EtageInfInf, analyse11.DeviationLevreInf, analyse11.DeviationMenton);

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyse1 = analyse11.ListOfPoints;

            InitDisplay();
            Refresh();
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

        private void InitDisplay()
        {
            rbEffondrement.Checked = ResumeCliniqueMgmt.resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Effondrement;
            rbDiminution.Checked = ResumeCliniqueMgmt.resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Diminution;
            rbAugmentation.Checked = ResumeCliniqueMgmt.resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Augmentation;
            rbNormal.Checked = ResumeCliniqueMgmt.resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Normal;


            RegenerateDiagObjTraitmnt();

            
        }

        private void FrmAnalyse1_Resize(object sender, EventArgs e)
        {
            analyse11.zoomAuto();
            analyse11.Center();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbEffondrement_CheckedChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Normal;

            if (rbEffondrement.Checked) ResumeCliniqueMgmt.resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Effondrement;
            if (rbDiminution.Checked) ResumeCliniqueMgmt.resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Diminution;
            if (rbAugmentation.Checked) ResumeCliniqueMgmt.resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Augmentation;
            if (rbNormal.Checked) ResumeCliniqueMgmt.resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Normal;

            RegenerateDiagObjTraitmnt();
           
            Refresh();


        }

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
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

        private void FrmAnalyse1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ResumeCliniqueMgmt.Analyse1IsValid!="")
            {

                DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous Appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                {
                    ResumeCliniqueMgmt.Analyse1AffectDefault();
                }

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {

                }
                
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ExportToLetter(true);
        }

        public void ExportToLetter(bool DirectPrint)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseMasqueFacial"];
            if (!File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string f = GetAnalyseImage();


            /*
             public float  EtageSup = 0;
        public float EtageMoy = 0;
        public float EtageInf = 0;
        public float EtageMoy2 = 0;
        public float EtageInf2 = 0;
        public float EtageInfSup = 0;
        public float  EtageInfInf = 0;
        public float  DeviationLevreInf = 0;
        public float  DeviationMenton = 0;
             */
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageSupVal", (analyse11.EtageSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoyVal", (analyse11.EtageMoy * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfVal", (analyse11.EtageInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageMoy2Val", (analyse11.EtageMoy2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInf2Val", (analyse11.EtageInf2 * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfSupVal", (analyse11.EtageInfSup * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInfInfVal", (analyse11.EtageInfInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationLevreInfVal", (analyse11.DeviationLevreInf * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationMentonVal", (analyse11.DeviationMenton * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Analyse", f);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);

            if (DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }

        public string GetAnalyseImage()
        {
            Bitmap bmp = new Bitmap(analyse11.Width, analyse11.Height);
            Graphics g = Graphics.FromImage(bmp);
            analyse11.PaintOn(g, new Rectangle(0, 0, analyse11.Width, analyse11.Height), true);
            string f = Path.GetTempFileName();
            bmp.Save(f);
            return f;
        }


        private void CheckDiags()
        {
            int[] AcceptedDiags = new int[] { 1, 2, 3, 4, 5 };

            ResumeCliniqueMgmt.resumeCl.diagnostics.Sort();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (AcceptedDiags.Contains(cd.Id))
                {

                   

                    
                    FrmLittleWizard frm = new FrmLittleWizard(cd,CurrentPat);
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
        }


        private void BTnNext_Click(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes; //means Next
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
                MdiParent.Invoke(BasCommon_BL.CommonCalls.NextScreenHandler, new object[] {this });
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

        private void analyse11_OnSaisie(object sender, EventArgs e)
        {
            
        }

        private void analyse11_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Ext_Face = analyse11.file;
                   
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void rbEffondrement_Paint(object sender, PaintEventArgs e)
        {

            if (ResumeCliniqueMgmt.resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.undefined)
            {
                if ((sender == rbAugmentation) ||(sender == rbDiminution) ||(sender == rbEffondrement) ||(sender == rbNormal))
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

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Refresh();
        }

        Point orgPt;
        Size orgsize;
       

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void analyse11_Load(object sender, EventArgs e)
        {

        }

        private void rbEffondrement_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      

        
    }
}
