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
    public partial class FrmPlanTraitement : FormScreen
    {


        private string _CurrentResource = null;
        public string CurrentResource
        {
            get
            {
                return _CurrentResource;
            }
            set
            {
                _CurrentResource = value;
            }
        }

        private Dictionary<string, Bitmap> _resources = new Dictionary<string, Bitmap>();
        public Dictionary<string, Bitmap> resources
        {
            get
            {
                return _resources;
            }
            set
            {
                _resources = value;
            }
        }

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

        public FrmPlanTraitement(basePatient pat)
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

            
            imagePano.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            imagePano.zoomAuto();
            imagePano.Center();

             imageSourire.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire);
             imageSourire.zoomAuto();
             imageSourire.Center();

            barrePatient1.patient = CurrentPat;
            LoadPPT("PlanTraitement");

            LoadResources();

            InitDisplay();
        }

        private void LoadResources()
        {



            string resourcesfolder = System.Configuration.ConfigurationManager.AppSettings["ResourcesPlanTraitement"];

            try
            {
                foreach (string s in Directory.GetFiles(resourcesfolder))
                {
                    try
                    {
                        FileInfo nfo = new FileInfo(s);
                        Bitmap bmp = new Bitmap(nfo.FullName);
                        resources.Add(nfo.Name, bmp);

                        RadioButton rb = new RadioButton();

                        rb.Appearance = System.Windows.Forms.Appearance.Button;
                        rb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        rb.Name = "rb" + nfo.Name;
                        rb.Size = new System.Drawing.Size(45, 45);
                        rb.TabIndex = 2;
                        rb.TabStop = true;
                        rb.UseVisualStyleBackColor = true;
                        rb.BackgroundImage = bmp;
                        rb.BackgroundImageLayout = ImageLayout.Stretch;
                        rb.MouseClick += new MouseEventHandler(rb_MouseClick);
                        rb.Tag = nfo.Name;
                        rb.FlatAppearance.CheckedBackColor = Color.Gray;
                        rb.MouseMove += rb_MouseMove;

                        flowLayoutPanel1.Controls.Add(rb);


                    }
                    catch (System.Exception) { }
                }
            }
            catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                MessageBox.Show(resourcesfolder);
            }
            imagePano.resources = resources;
            imageSourire.resources = resources;
        }

        void rb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DataObject objtodrag = new DataObject("Traitement", ((RadioButton)sender));
                ((RadioButton)sender).DoDragDrop(objtodrag, DragDropEffects.Copy);
            }
        }

        void rb_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentResource = (string)((RadioButton)sender).Tag;
            imagePano.startPoseResource(CurrentResource);
            imageSourire.startPoseResource(CurrentResource);
        }

        void rbSelect_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentResource = null;
            imagePano.StopPoseResource(CurrentResource);
            imageSourire.StopPoseResource(CurrentResource);
        }

        

        private void FrmAnalyse8_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement == null)
                ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement = new List<PlanTraitementObject>();
            else
                ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement.Clear();

            

            foreach(Ctrls.PtObject o in  imageSourire.objectsToDraw)
            {
                PlanTraitementObject pto = new PlanTraitementObject();
                pto.Point1 = o.FirstPointOnRadio;
                pto.Point2 = o.SecondPointOnRadio;
                 pto.IdResumclinique = ResumeCliniqueMgmt.resumeCl.Id;
                 pto.Resumclinique = ResumeCliniqueMgmt.resumeCl;
                 pto.ResourceName = o.resourcekey;
                 pto.CtrlKey = imageSourire.Name;
                 ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement.Add(pto);
            } 
            
            foreach (Ctrls.PtObject o in imagePano.objectsToDraw)
            {
                PlanTraitementObject pto = new PlanTraitementObject();
                pto.Point1 = o.FirstPointOnRadio;
                pto.Point2 = o.SecondPointOnRadio;
                pto.IdResumclinique = ResumeCliniqueMgmt.resumeCl.Id;
                pto.Resumclinique = ResumeCliniqueMgmt.resumeCl;
                pto.ResourceName = o.resourcekey;
                pto.CtrlKey = imagePano.Name;
                ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement.Add(pto);
            }



            if (ResumeCliniqueMgmt.AnalysePlanTraitementIsValid!="")
            {
                DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                {
                    ResumeCliniqueMgmt.AnalysePlanTraitementAffectDefault();
                }
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }


        void InitDisplay()
        {
            if (ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement != null)
            {
                foreach (PlanTraitementObject pto in ResumeCliniqueMgmt.resumeCl.objectsForPlanTraitement)
                {

                    Ctrls.PtObject o = new Ctrls.PtObject();
                    o.FirstPointOnRadio = pto.Point1;
                    o.SecondPointOnRadio = pto.Point2;
                    o.resourcekey = pto.ResourceName;

                    if (pto.CtrlKey == imageSourire.Name)
                        imageSourire.objectsToDraw.Add(o);


                    if (pto.CtrlKey == imagePano.Name)
                        imagePano.objectsToDraw.Add(o);

                }

                imageSourire.RecalculateAllPos();
                imagePano.RecalculateAllPos();
            }
           
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
                if (cd == null) return;
                if (!AcceptedDiags.Contains(cd.Id)) continue;
                FrmLittleWizard frm = new FrmLittleWizard(cd, CurrentPat);
                if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                {
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
            CheckDiags();
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

        public void ExportToLetter(bool DirectPrint)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierAnalysePlanTraitement"];
            if (!File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string filesourire = GetAnalyseimageSourire();
            string filepano = GetAnalyseimagePano();



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Image_Panoramique", filepano);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Image_Sourire", filesourire);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();


            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else

                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }


        public string GetAnalyseimageSourire()
        {
            Bitmap bmp = new Bitmap(imageSourire.Width, imageSourire.Height);
            Graphics g = Graphics.FromImage(bmp);
            imageSourire.PaintOn(g, new Rectangle(0, 0, imageSourire.Width, imageSourire.Height),true);
            string f = Path.GetTempFileName();
            bmp.Save(f);
            return f;
        }

        public string GetAnalyseimagePano()
        {
            Bitmap bmp = new Bitmap(imagePano.Width, imagePano.Height);
            Graphics g = Graphics.FromImage(bmp);
            imagePano.PaintOn(g, new Rectangle(0, 0, imagePano.Width, imagePano.Height), true);
            string f = Path.GetTempFileName();
            bmp.Save(f);
            return f;
        }


       

        private void imageCtrl1_OnImageChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano = imagePano.file;
        }

        private void chkbxEvolGermeDents_MouseDown(object sender, MouseEventArgs e)
        {
            if (((CheckBox)sender).CheckState == CheckState.Indeterminate)
                ((CheckBox)sender).CheckState = CheckState.Unchecked;
        }

        private void imageSourire_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire = imageSourire.file;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imageSourire_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;

            /*
            if (e.Data.GetDataPresent(typeof(String)))
            {
                string dt = (string)e.Data.GetData(typeof(String));

                if (resources.ContainsKey(dt))
                {
                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
             * */
        }

        private void imageSourire_DragDrop(object sender, DragEventArgs e)
        {

            /*DataObject objtodrag = new DataObject("Traitement", ((RadioButton)sender));
            ((RadioButton)sender).DoDragDrop(objtodrag, DragDropEffects.Copy);*/


            if (e.Data.GetDataPresent("Traitement"))
            {
                RadioButton dt = (RadioButton)e.Data.GetData("Traitement");
                imageSourire.AddResourceToCursor((string)dt.Tag);
            }
            else
            {
                string dt = (string)e.Data.GetData(typeof(String));
                imageSourire.AddResourceToCursor(dt);
            }
        }

        private void imagePano_DragDrop(object sender, DragEventArgs e)
        {


            if (e.Data.GetDataPresent("Traitement"))
            {
                RadioButton dt = (RadioButton)e.Data.GetData("Traitement");
                imagePano.AddResourceToCursor((string)dt.Tag);
            }
            else
            {
                string dt = (string)e.Data.GetData(typeof(String));
                imagePano.AddResourceToCursor(dt);
            }

            
        }

        private void imagePano_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void imagePano_MouseDown(object sender, MouseEventArgs e)
        {

            imagePano.startPoseResource(CurrentResource);
            
        }

        private void barrePatient1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
