using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;
using System.IO;
using BASEDiag_BL;
using System.Net;
using System.Net.Cache;

namespace BASEDiag
{
    public partial class FrmWizardInvisalign : Form
    {

        /*
           chkbxEncombrement.Checked = false;
            chkbxEspacement.Checked = false;
            chkbxClIIDiv1.Checked = false;
            chkbxClIIDiv2.Checked = false;
            chkbxClIII.Checked = false;
            chkbxBeance.Checked = false;
            chkbxSuppra.Checked = false;
            chkbxArticulareAnt.Checked = false;
            chkbxArticularePost.Checked = false;

            chkbxAnomalieFormeDentaire.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == EntentePrealable.en_FormeArcade.V;

            chkbxArcadeEtroite.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == EntentePrealable.en_FormeArcade.V;
            chkbxProAlveolie.Checked = false;
            chkbxSurplomb.Checked = ResumeCliniqueMgmt.resumeCl.SurplombValue!=0;
            chkbxSourireInesthetique.Checked = false;
            chkbxAutre.Checked = false;
            txtbxAutre.Text = "";

            txtbxRemarques.Text = "";
         */

        public string Remarques
        {
            get
            {
                return txtbxRemarques.Text;
            }



        }

        public string AutreTxt
        {
            get
            {
                return txtbxAutre.Text;
            }



        }

        public bool Autre
        {
            get
            {
                return chkbxAutre.Checked;
            }



        }

        public bool SourireInesthetique
        {
            get
            {
                return chkbxSourireInesthetique.Checked;
            }



        }
        public bool Surplomb
        {
            get
            {
                return chkbxSurplomb.Checked;
            }


        }

        public bool ProAlveolie
        {
            get
            {
                return chkbxProAlveolie.Checked;
            }


        }

        public bool ArcadeEtroite
        {
            get
            {
                return chkbxArcadeEtroite.Checked;
            }


        }

        public bool AnomalieFormeDentaire
        {
            get
            {
                return chkbxAnomalieFormeDentaire.Checked;
            }


        }


        public bool ArticularePost
        {
            get
            {
                return chkbxArticularePost.Checked;
            }


        }


        public bool ArticulareAnt
        {
            get
            {
                return chkbxArticulareAnt.Checked;
            }


        }


        public bool Suppra
        {
            get
            {
                return chkbxSuppra.Checked;
            }


        }

        public bool Beance
        {
            get
            {
                return chkbxBeance.Checked;
            }


        }

        public bool ClIII
        {
            get
            {
                return chkbxClIII.Checked;
            }


        }

        public bool ClIIDiv2
        {
            get
            {
                return chkbxClIIDiv2.Checked;
            }


        }

        public bool ClIIDiv1
        {
            get
            {
                return chkbxClIIDiv1.Checked;
            }


        }
        public bool Espacement
        {
            get
            {
                return chkbxEspacement.Checked;
            }

        }


        public bool Encombrement
        {
            get
            {
                return chkbxEncombrement.Checked;
            }

        }



        public string IntraFace
        {
            get
            {
                return (string)pbxIntra.Tag;
            }

        }

        public string IntraGauche
        {
            get
            {
                return (string)pbxIntraGauche.Tag;
            }

        }

        public string IntraDroit
        {
            get
            {
                return (string)pbxIntraDroit.Tag;
            }

        }


        public string Mand
        {
            get
            {
                return (string)pbxMand.Tag;
            }

        }


        public string Max
        {
            get
            {
                return (string)pbxMax.Tag;
            }

        }


        public string PhotoProfil
        {
            get
            {
                return (string)pbxProfil.Tag;
            }

        }

        public string PhotoFace
        {
            get
            {
                return (string)pbxFace.Tag;
            }

        }

        public string PhotoFaceSourire
        {
            get
            {
                return (string)pbxFaceSourire.Tag;
            }

        }

        

        public string Radio
        {
            get
            {
                return (string)pbxRadio.Tag;
            }

        }

        public string Pano
        {
            get
            {
                return (string)pbxPano.Tag;
            }

        }

        public string PhotoPatient
        {
            get
            {
                return (string)pbxPatient.Tag;
            }
           
        }
        

        private basePatient _ValuePatient;
        public basePatient ValuePatient
        {
            get
            {
                return _ValuePatient;
            }
            set
            {
                _ValuePatient = value;
            }
        }
        

        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }



        public FrmWizardInvisalign(basePatient pat)
        {
            InitializeComponent(); 
            CurrentPatient = pat;         
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmWizardInvisalign_Load(object sender, EventArgs e)
        {
            initDisplayInfosPatient();
            initDisplayPhotosRadiosPatient();
            initDisplayClinique();
            ShowPanel(pnlPatient, false);

        }

        private void initDisplayInfosPatient()
        {
            txtbxNom.Text = CurrentPatient.Nom;
            txtbxPrenom.Text = CurrentPatient.Prenom;
            rbF.Checked = CurrentPatient.Genre == basePatient.Sexe.Feminin;
            rbH.Checked = !rbF.Checked;
            dtpDateNaiss.Value = CurrentPatient.DateNaissance;

            if (IsFileExist(CurrentPatient.Img_Ext_Face))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Face);
                pbxPatient.Image = Bitmap.FromStream(stream);
                pbxPatient.Tag = CurrentPatient.Img_Ext_Face;

            }

        }

        private bool BuildInfosPatient()
        {
            try
            {
                ValuePatient = new basePatient();
                ValuePatient.Nom = txtbxNom.Text;
                ValuePatient.Id = CurrentPatient.Id;
                ValuePatient.Prenom = txtbxPrenom.Text;
                ValuePatient.Genre = rbF.Checked ? basePatient.Sexe.Feminin : basePatient.Sexe.Masculin;
                ValuePatient.DateNaissance = dtpDateNaiss.Value;
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            


        }

        private void initDisplayClinique()
        {
            chkbxEncombrement.Checked = false;
            chkbxEspacement.Checked = false;
            chkbxClIIDiv1.Checked = false;
            chkbxClIIDiv2.Checked = false;
            chkbxClIII.Checked = false;
            chkbxBeance.Checked = false;
            chkbxSuppra.Checked = false;
            chkbxArticulareAnt.Checked = false;
            chkbxArticularePost.Checked = false;

            chkbxAnomalieFormeDentaire.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == EntentePrealable.en_FormeArcade.V;

            chkbxArcadeEtroite.Checked = ResumeCliniqueMgmt.resumeCl.FormeArcade == EntentePrealable.en_FormeArcade.V;
            chkbxProAlveolie.Checked = false;
            chkbxSurplomb.Checked = ResumeCliniqueMgmt.resumeCl.SurplombValue!=0;
            chkbxSourireInesthetique.Checked = false;
            chkbxAutre.Checked = false;
            txtbxAutre.Text = "";

            txtbxRemarques.Text = "";


        }

        private void initDisplayPhotosRadiosPatient()
        {

            if (IsFileExist(CurrentPatient.Img_Rad_Pano))
            {
                Stream stream = fromStream(CurrentPatient.Img_Rad_Pano);
                pbxPano.Image = Bitmap.FromStream(stream);
                pbxPano.Tag = CurrentPatient.Img_Rad_Pano;
            }




            if (IsFileExist(CurrentPatient.Img_Ext_Profile))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Profile);
                pbxProfil.Image = Bitmap.FromStream(stream);
                pbxProfil.Tag = CurrentPatient.Img_Ext_Profile;
            }

            if (IsFileExist(CurrentPatient.Img_Ext_Face))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Face);
                pbxFace.Image = Bitmap.FromStream(stream);
                pbxFace.Tag = CurrentPatient.Img_Ext_Face;
            }

            if (IsFileExist(CurrentPatient.Img_Ext_Face_Sourire))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Face_Sourire);
                pbxFaceSourire.Image = Bitmap.FromStream(stream);
                pbxFaceSourire.Tag = CurrentPatient.Img_Ext_Face_Sourire;
            }


            if (IsFileExist(CurrentPatient.Img_Int_Man))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Man);
                pbxMand.Image = Bitmap.FromStream(stream);
                pbxMand.Tag = CurrentPatient.Img_Int_Man;
            }

            if (IsFileExist(CurrentPatient.Img_Int_Max))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Max);
                pbxMax.Image = Bitmap.FromStream(stream);
                pbxMax.Tag = CurrentPatient.Img_Int_Max;
            }

            if (IsFileExist(CurrentPatient.Img_Int_Gauche))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Gauche);
                pbxIntraGauche.Image = Bitmap.FromStream(stream);
                pbxIntraGauche.Tag = CurrentPatient.Img_Int_Gauche;
            }

            if (IsFileExist(CurrentPatient.Img_Int_Droit))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Droit);
                pbxIntraDroit.Image = Bitmap.FromStream(stream);
                pbxIntraDroit.Tag = CurrentPatient.Img_Int_Droit;
            }

            if (IsFileExist(CurrentPatient.Img_Int_Face))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Face);
                pbxIntra.Image = Bitmap.FromStream(stream);
                pbxIntra.Tag = CurrentPatient.Img_Int_Face;
            }
            if (IsFileExist(CurrentPatient.Img_Rad_Profile))
            {
                Stream stream = fromStream(CurrentPatient.Img_Rad_Profile);
                pbxRadio.Image = Bitmap.FromStream(stream);
                pbxRadio.Tag = CurrentPatient.Img_Rad_Profile;
            }

        }
        public bool IsFileExist(string path)
        {
            try
            {
                HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(
                path);
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Reload);
                lxRequest.CachePolicy = noCachePolicy;
                using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        private Stream fromStream(String url)
        {
            var req = System.Net.WebRequest.Create(url);

            return req.GetResponse().GetResponseStream();

        }

        private void pnlPhotos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {

            Bitmap bmp = null;
            string f = "";
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    bmp = (Bitmap)Bitmap.FromFile(files[0]);
                    f = files[0];
                }
                catch (System.Exception)
                {
                    bmp = null;
                    f = "";
                }


            }

            if (bmp != null)
            {
                ((PictureBox)sender).Image = bmp;
                ((PictureBox)sender).Tag = f;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlClinique);
        }


        Stack<Panel> histopanel = new Stack<Panel>();

        private void ShowPanel(Panel pnl)
        {
            ShowPanel(pnl, true);
        }


        Panel currentpnl = null;


        private void ShowPanel(Panel pnl, bool withhisto)
        {
            if ((currentpnl != null) && (withhisto))
                histopanel.Push(currentpnl);

            currentpnl = pnl;
            foreach (Control c in pnlContainer.Controls)
            {
                c.Visible = false;
            }

            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(pnl);
            pnl.Show();


        }


        private void BackPanel()
        {
            if (histopanel.Count == 0) return;
            Panel p = histopanel.Pop();
            ShowPanel(p, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlRadios);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void pnlClinique_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlPhotos);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BuildInfosPatient())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void FrmWizardInvisalign_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pbxPatient_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ((PictureBox)sender).Image = Bitmap.FromFile(ofd.FileName);
                    ((PictureBox)sender).Tag = ofd.FileName;
                }
                catch (System.Exception ex)
                {

                }
            }
        }
    }
}
