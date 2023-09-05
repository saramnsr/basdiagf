using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag.Ctrls;

namespace BASEDiag
{
    public partial class FrmPreAnalyse : Form
    {
        int CurrentScreenIdx = 0;

        private Patient _CurrentPat;
        public Patient CurrentPat
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

        public FrmPreAnalyse(Patient pat, int screenidx)
        {
            InitializeComponent();
            CurrentPat = pat;
            CurrentScreenIdx = screenidx;
        }

        private void InitPos()
        {
            int x = 0;
            int y = 0;
            ImgProfil.Location = new Point(x,y);
            ImgProfil.Size = new Size(pnlAnalyse.Width / 2, pnlAnalyse.Height / 3);

            x += pnlAnalyse.Width / 2;
            ImgFace.Location = new Point(x, y);
            ImgFace.Size = new Size(pnlAnalyse.Width / 2, pnlAnalyse.Height / 3);


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
            ImgFace.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);

            imgOccDroit.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            imgOccFace.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            imgOccGauche.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);

            imgPano.loadRadio(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);

            txtbxresumeQ1CS.Text = CurrentPat.ResumeQ1CS;

            InitPos();

            barrePatient1.patient = CurrentPat;

            this.Bounds = Screen.AllScreens[CurrentScreenIdx].Bounds;
            
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["CourrierPreAnalyse"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }





            int idxWindow = BASEDiag_BL.OLEAccess.BASLetter.Open(file);


            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.Generate();
            BASEDiag_BL.OLEAccess.BASLetter.CloseWindow(idxWindow);
        }

        private void lnkCloseFullScreen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImgProfil_DoubleClick(this.pnlFullScreen.Controls[1], new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //   this.WindowState = FormWindowState.Normal;

            CurrentScreenIdx++;
            CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;



            this.Bounds = Screen.AllScreens[CurrentScreenIdx].Bounds;

            //   this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
        }
    }
}
