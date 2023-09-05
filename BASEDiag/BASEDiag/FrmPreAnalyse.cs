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
using BasCommon_BO;
using BasCommon_BL;
using Microsoft.Win32;

namespace BASEDiag
{
    public partial class FrmPreAnalyse : FormScreen
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

        public FrmPreAnalyse(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;


        }

        private void InitPos()
        {
            int x = 0;
            int y = 0;
            ImgProfil.Location = new Point(x,y);
            ImgProfil.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);

            x += pnlAnalyse.Width / 3;
            ImgFace.Location = new Point(x, y);
            ImgFace.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);
            
            x += pnlAnalyse.Width / 3;
            ImgRadioProfil.Location = new Point(x, y);
            ImgRadioProfil.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);

            
            x = 0;
            y += pnlAnalyse.Height / 3;
            imgOccGauche.Location = new Point(x, y);
            imgOccGauche.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);

            x += pnlAnalyse.Width / 3;
            imgOccFace.Location = new Point(x, y);
            imgOccFace.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);

            x += pnlAnalyse.Width / 3;
            imgOccDroit.Location = new Point(x, y);
            imgOccDroit.Size = new Size(pnlAnalyse.Width / 3, pnlAnalyse.Height / 3);



            x = 0;
            y += pnlAnalyse.Height / 3;
            imgPano.Location = new Point(x, y);
            imgPano.Size = new Size(pnlAnalyse.Width, pnlAnalyse.Height / 3);


            ImgRadioProfil.zoomAuto();
            ImgRadioProfil.Center();
            ImgProfil.zoomAuto();
            ImgProfil.Center();
            ImgFace.zoomAuto();
            ImgFace.Center();

            imgOccDroit.zoomAuto();
            imgOccDroit.Center();
            imgOccFace.zoomAuto();
            imgOccFace.Center();
            imgOccGauche.zoomAuto();
            imgOccGauche.Center();

            imgPano.zoomAuto();
            imgPano.Center();
        }

        private void FrmPreAnalyse_Load(object sender, EventArgs e)
        {
            ImgProfil.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            ImgRadioProfil.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            ImgFace.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);

            imgOccDroit.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            imgOccFace.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            imgOccGauche.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);

            imgPano.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);


            if (CurrentPat.ResumeQ1CS == null)
                CurrentPat.ResumeQ1CS = Q1CSMgmt.GetResume(CurrentPat.Id);

            txtbxresumeQ1CS.Text = CurrentPat.ResumeQ1CS;

            InitPos();

            barrePatient1.patient = CurrentPat;

            if (Screen.AllScreens.Length >= CurrentScreenIdx) CurrentScreenIdx = 0;
            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;
            
        }

        private void FrmPreAnalyse_Resize(object sender, EventArgs e)
        {
            InitPos();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImgProfil_DoubleClick(object sender, EventArgs e)
        {
            if (((ImageCtrlAgg)sender).Parent == this.pnlFullScreen)
            {
                pnlFullScreen.Visible = false;
                this.pnlFullScreen.Controls.Remove((ImageCtrlAgg)sender);
                this.pnlAnalyse.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.pnlAnalyse;
                ((ImageCtrlAgg)sender).Dock = DockStyle.None;                
                InitPos();
            }
            else
            {

                this.pnlAnalyse.Controls.Remove((ImageCtrlAgg)sender);
                this.pnlFullScreen.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.pnlFullScreen;
                ((ImageCtrlAgg)sender).Dock = DockStyle.Fill;
                pnlFullScreen.Visible = true;
                ((ImageCtrlAgg)sender).zoomAuto();
                ((ImageCtrlAgg)sender).Center();
            }
        }

        private void pnlFullScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierPreAnalyse"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }







            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();
            BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
        }

        private void lnkCloseFullScreen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImgProfil_DoubleClick(this.pnlFullScreen.Controls[1], new EventArgs());
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

        private void imgPano_OnRadioChanged(object sender, EventArgs e)
        {
            if (sender == ImgProfil) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile = ImgProfil.file;
            if (sender == ImgRadioProfil) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile = ImgRadioProfil.file;  
            if (sender == ImgFace) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire = ImgFace.file;
            if (sender == imgOccDroit) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Droit = imgOccDroit.file;
            if (sender == imgOccFace) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Face = imgOccFace.file;
            if (sender == imgOccGauche) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche = imgOccGauche.file;
            if (sender == imgPano) BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano = imgPano.file;
                
            

        }

        private void BTnNext_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes; //means Next
            Close();
        }

        private void motifDeConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
             #region Motif de Consultation

            CommentHisto ch = new CommentHisto();

            
            ch.comment = txtbxresumeQ1CS.SelectedText;
            ch.DateCommentaire = DateTime.Now;
            ch.Ecrivain = BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.patient.infoscomplementaire.PraticienResponsable;
            ch.patient = BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.patient;
            ch.typecomment = CommentHisto.CommentHistoType.MotifConsultation;
            MgmtCommentairesHisto.InsertCommentaire(ch);

            #endregion

            
        }

        private void pnlAnalyse_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
