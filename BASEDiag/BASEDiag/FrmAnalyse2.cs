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
namespace BASEDiag
{
    public partial class FrmAnalyse2 : FormScreen
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

        public FrmAnalyse2(basePatient pat)
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

        private void FrmAnalyse2_Load(object sender, EventArgs e)
        {


            tbDecInterIncisiveBas.Items.Add("10 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("9 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("8 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("7 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("6 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("5 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("4 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("3 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("2 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("1 mm à gauche");
            tbDecInterIncisiveBas.Items.Add("Aucun");
            tbDecInterIncisiveBas.Items.Add("1 mm à droite");
            tbDecInterIncisiveBas.Items.Add("2 mm à droite");
            tbDecInterIncisiveBas.Items.Add("3 mm à droite");
            tbDecInterIncisiveBas.Items.Add("4 mm à droite");
            tbDecInterIncisiveBas.Items.Add("5 mm à droite");
            tbDecInterIncisiveBas.Items.Add("6 mm à droite");
            tbDecInterIncisiveBas.Items.Add("7 mm à droite");
            tbDecInterIncisiveBas.Items.Add("8 mm à droite");
            tbDecInterIncisiveBas.Items.Add("9 mm à droite");
            tbDecInterIncisiveBas.Items.Add("10 mm à droite");


            tbDecInterIncisiveHaut.Items.Add("10 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("9 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("8 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("7 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("6 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("5 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("4 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("3 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("2 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("1 mm à gauche");
            tbDecInterIncisiveHaut.Items.Add("Aucun");
            tbDecInterIncisiveHaut.Items.Add("1 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("2 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("3 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("4 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("5 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("6 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("7 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("8 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("9 mm à droite");
            tbDecInterIncisiveHaut.Items.Add("10 mm à droite");
                        
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        public  void InitLoad()
        {

            analyse21.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Portrait"] + "," + ConfigurationManager.AppSettings["Attr_Face"] + "," + ConfigurationManager.AppSettings["Attr_Sourire"];


            analyse21.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);
            analyse21.zoomAuto();
            analyse21.Center();
            analyse21.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            analyse21.StartSaisie();
            analyse21.OnEndSaisie += new EventHandler(analyse21_OnEndSaisie);
            analyse21.OnSaisie += new EventHandler(analyse21_OnSaisie);

            LoadPPT("Analyse2");
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            InitDisplay();

            barrePatient1.patient = CurrentPat;


           

            analyse21.AffectList(ResumeCliniqueMgmt.resumeCl.LstPtAnalyse2);
        }

        void analyse21_OnSaisie(object sender, EventArgs e)
        {
            
        }

        void analyse21_OnEndSaisie(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.ConvertFromAnalyse2(analyse21.EspaceDentaireBuccal, analyse21.IncisiveMolaireDroit, analyse21.IncisiveMolaireGauche);

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyse2 = analyse21.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        private void InitDisplay()
        {
            rbLarge.Checked = ResumeCliniqueMgmt.resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Large;
            rbEtroit.Checked = ResumeCliniqueMgmt.resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit;
            rbNormal.Checked = ResumeCliniqueMgmt.resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal;

            rbTriBiLateral.Checked = ResumeCliniqueMgmt.resumeCl.TNL == BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Droit_Et_Gauche;
            rbTriDroit.Checked = ResumeCliniqueMgmt.resumeCl.TNL == BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Droit;
            rbTriGauche.Checked = ResumeCliniqueMgmt.resumeCl.TNL == BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Gauche;
            rbTriAucun.Checked = ResumeCliniqueMgmt.resumeCl.TNL == BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Aucun;

            tbDecInterIncisiveHaut.SelectedIndex = (tbDecInterIncisiveHaut.Items.Count/2)+ ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut;
            tbDecInterIncisiveBas.SelectedIndex = (tbDecInterIncisiveBas.Items.Count / 2) + ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas;

            RegenerateDiagObjTraitmnt();
        }

        private void CheckDiags()
        {

            int[] AcceptedDiags = new int[] { 6, 7, 8, 9, 10, 11, 12, 13,49,33,34 };


            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (AcceptedDiags.Contains(cd.Id))
                {

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


        private void rbEffondrement_CheckedChanged(object sender, EventArgs e)
        {
           
            if (rbLarge.Checked) ResumeCliniqueMgmt.resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Large;
            if (rbEtroit.Checked) ResumeCliniqueMgmt.resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit;
            if (rbNormal.Checked) ResumeCliniqueMgmt.resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal;

            switch (tbDecInterIncisiveHaut.SelectedIndex)
            {
                case 0: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -10; break;
                case 1: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -9; break;
                case 2: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -8; break;
                case 3: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -7; break;
                case 4: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -6; break;
                case 5: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -5; break;
                case 6: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -4; break;
                case 7: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -3; break;
                case 8: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -2; break;
                case 9: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = -1; break;
                case 10: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 0; break;
                case 11: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 1; break;
                case 12: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 2; break;
                case 13: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 3; break;
                case 14: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 4; break;
                case 15: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 5; break;
                case 16: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 6; break;
                case 17: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 7; break;
                case 18: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 8; break;
                case 19: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 9; break;
                case 20: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut = 10; break;
            }

            switch (tbDecInterIncisiveBas.SelectedIndex)
            {
                case 0: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -10; break;
                case 1: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -9; break;
                case 2: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -8; break;
                case 3: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -7; break;
                case 4: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -6; break;
                case 5: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -5; break;
                case 6: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -4; break;
                case 7: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -3; break;
                case 8: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -2; break;
                case 9: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = -1; break;
                case 10: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 0; break;
                case 11: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 1; break;
                case 12: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 2; break;
                case 13: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 3; break;
                case 14: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 4; break;
                case 15: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 5; break;
                case 16: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 6; break;
                case 17: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 7; break;
                case 18: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 8; break;
                case 19: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 9; break;
                case 20: ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas = 10; break;
            }
            

            if (rbTriBiLateral.Checked) ResumeCliniqueMgmt.resumeCl.TNL = BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Droit_Et_Gauche;
            if (rbTriDroit.Checked) ResumeCliniqueMgmt.resumeCl.TNL = BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Droit;
            if (rbTriGauche.Checked) ResumeCliniqueMgmt.resumeCl.TNL = BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Gauche;
            if (rbTriAucun.Checked) ResumeCliniqueMgmt.resumeCl.TNL = BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.Aucun;


            RegenerateDiagObjTraitmnt();

            Refresh();
        }

        private void FrmAnalyse2_Resize(object sender, EventArgs e)
        {
            analyse21.zoomAuto();
            analyse21.Center();
        }

        

       

        private void FrmAnalyse2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse2IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse2AffectDefault();
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

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void BTnNext_Click(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes;
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

        public void ExportToLetter(bool DirectPrint)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseFaceSourire"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string f = GetAnalyseImage();



            /*
             public float EspaceDentaireBuccal = 0;
        public float IncisiveMolaireDroit = 0;
        public float IncisiveMolaireGauche = 0;
             */
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EspaceDentaireBuccal", (analyse21.EspaceDentaireBuccal * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireDroit", (analyse21.IncisiveMolaireDroit * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveMolaireGauche", (analyse21.IncisiveMolaireGauche * 100).ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseSourire", f);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);

        }

        public string GetAnalyseImage()
        {
            Bitmap bmp = new Bitmap(analyse21.Width, analyse21.Height);
            Graphics g = Graphics.FromImage(bmp);
            analyse21.PaintOn(g, new Rectangle(0, 0, analyse21.Width, analyse21.Height));
            string f = Path.GetTempFileName();
            bmp.Save(f);
            return f;
        }

        private void analyse21_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire = analyse21.file;
        }

        private void easyTrackBar2_Load(object sender, EventArgs e)
        {

        }

        private void rbLarge_Paint(object sender, PaintEventArgs e)
        {

            if (ResumeCliniqueMgmt.resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined)
            {
                if ((sender == rbEtroit)||(sender == rbLarge)||(sender == rbNormal))
                { e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0)); return; }
                
            }

            if (ResumeCliniqueMgmt.resumeCl.TNL == BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.undefined)
            {
                if ((sender == rbTriAucun) || (sender == rbTriDroit) || (sender == rbTriGauche) || (sender == rbTriBiLateral))
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

        bool MiniatureIsCollapsed = true;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point pt = PointToScreen(pictureBox1.Location);

            double ratio = (float)pictureBox1.Image.Width / pictureBox1.Image.Height;

            if (MiniatureIsCollapsed)
            {
                int h= Screen.GetWorkingArea(this).Height-pt.Y;
                pictureBox1.Size = new Size((int)(h*ratio), h);
                MiniatureIsCollapsed = false;
            }
            else
            {
                int h =100;
                pictureBox1.Size = new Size((int)(h * ratio), h);
                MiniatureIsCollapsed = true;
            }
            pictureBox1.BringToFront();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Point pt = PointToScreen(pictureBox2.Location);

            double ratio = (float)pictureBox2.Image.Width / pictureBox2.Image.Height;

            if (MiniatureIsCollapsed)
            {
                int w = analyse21.Width;
                pictureBox2.Size = new Size(w,(int)(w / ratio));
                MiniatureIsCollapsed = false;
            }
            else
            {
                int w = 100;
                pictureBox2.Size = new Size(w, (int)(w / ratio));
                MiniatureIsCollapsed = true;
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

        
    }
}
