using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BO;

namespace BASEDiag
{
    public partial class FrmLittleWizard : Form
    {



        private string _ChoixDents;
        public string ChoixDents
        {
            get
            {
                return _ChoixDents;
            }
            set
            {
                _ChoixDents = value;
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
        


        private bool _CanBeShown = true;
        public bool CanBeShown
        {
            get
            {
                return _CanBeShown;
            }
            set
            {
                _CanBeShown = value;
            }
        }

        public List<CommonObjectifFromDiag> values
        {
            get
            {
                List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();

                foreach (CommonObjectifFromDiag co in lstBxObjectifs.CheckedItems)
                    lst.Add(co);

                return lst;
            }
            
        }

        public List<CommonObjectifFromDiag> UnChecked
        {
            get
            {
                List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();

                for (int i = 0; i <= (lstBxObjectifs.Items.Count - 1); i++)
                {
                    if (!lstBxObjectifs.GetItemChecked(i))
                        lst.Add((CommonObjectifFromDiag)lstBxObjectifs.Items[i]);
                    
                }

                return lst;
            }
            
        }

        

        private CommonDiagnostic _diagnostique;
        public CommonDiagnostic diagnostique
        {
            get
            {
                return _diagnostique;
            }
            set
            {
                _diagnostique = value;
            }
        }

        public FrmLittleWizard(CommonDiagnostic diagnostic,basePatient pat)
        {
            CurrentPatient = pat;

            InitializeComponent();
            _diagnostique = diagnostic;



            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(diagnostic);

            if (lstobjs.Count == 0)
            {
                DialogResult = DialogResult.OK;
                CanBeShown = false;
                Close();
            }



            lstobjs.Sort();
            lstBxObjectifs.Items.Clear();

            for (int i = 0; i < lstobjs.Count; i++)
            {
                lstBxObjectifs.Items.Add(lstobjs[i]);
            }


            for (int i = 0; i < lstBxObjectifs.Items.Count; i++)
            {
                if (CurrentPatient.SelectedObjectifs.Contains(((CommonObjectifFromDiag)lstBxObjectifs.Items[i]).objectif))
                    lstBxObjectifs.SetItemChecked(i, true);
                else
                    lstBxObjectifs.SetItemChecked(i, false);
            }


                


            
            lblQuestion.Text = string.IsNullOrEmpty(diagnostic.question)? "Que préconisez-vous pour '" + diagnostic.Libelle + "'?":diagnostic.question;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstBxObjectifs.Items.Count; i++)            
                lstBxObjectifs.SetItemChecked(i, false);
            

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmLittleWizard_Load(object sender, EventArgs e)
        {
            BuildImageList();
        }

        private void BuildImageList()
        {

          
            if ((diagnostique.Photos.Contains("face")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Ext_Face)))
                CreateImageBtn(CurrentPatient.Img_Ext_Face,"Face");

            if ((diagnostique.Photos.Contains("face sourire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Ext_Face_Sourire)))
                CreateImageBtn(CurrentPatient.Img_Ext_Face_Sourire, "Face sourire");

            if ((diagnostique.Photos.Contains("profil")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Ext_Profile)))
                CreateImageBtn(CurrentPatient.Img_Ext_Profile, "Profil");

            if ((diagnostique.Photos.Contains("profil sourire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Ext_Profile_Sourire)))
                CreateImageBtn(CurrentPatient.Img_Ext_Profile_Sourire, "Profil sourire");

            if ((diagnostique.Photos.Contains("sourire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Ext_Sourire)))
                CreateImageBtn(CurrentPatient.Img_Ext_Sourire, "sourire");

            if ((diagnostique.Photos.Contains("intra droit")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_Droit)))
                CreateImageBtn(CurrentPatient.Img_Int_Droit, "Intra droit");

            if ((diagnostique.Photos.Contains("intra face")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_Face)))
                CreateImageBtn(CurrentPatient.Img_Int_Face, "Intra Face");

            if ((diagnostique.Photos.Contains("intra gauche")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_Gauche)))
                CreateImageBtn(CurrentPatient.Img_Int_Gauche, "Intra gauche");

            if ((diagnostique.Photos.Contains("mandibulaire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_Man)))
                CreateImageBtn(CurrentPatient.Img_Int_Man, "Mandibulaire");

            if ((diagnostique.Photos.Contains("maxilaire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_Max)))
                CreateImageBtn(CurrentPatient.Img_Int_Max, "Maxilaire");

            if ((diagnostique.Photos.Contains("surplomb")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Int_SurPlomb)))
                CreateImageBtn(CurrentPatient.Img_Int_SurPlomb, "Surplomb");

            if ((diagnostique.Photos.Contains("moulage droit")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Moul_Droit)))
                CreateImageBtn(CurrentPatient.Img_Moul_Droit, "Moulage droit");

            if ((diagnostique.Photos.Contains("moulage face")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Moul_Face)))
                CreateImageBtn(CurrentPatient.Img_Moul_Face, "Moulage face");

            if ((diagnostique.Photos.Contains("moulage gauche")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Moul_Gauche)))
                CreateImageBtn(CurrentPatient.Img_Moul_Gauche, "Moulage gauche");

            if ((diagnostique.Photos.Contains("moulage mandibulaire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Moul_Man)))
                CreateImageBtn(CurrentPatient.Img_Moul_Man, "Moulage mandibulaire");

            if ((diagnostique.Photos.Contains("moulage maxilaire")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Moul_Max)))
                CreateImageBtn(CurrentPatient.Img_Moul_Max, "Moulage maxilaire");

            if (((diagnostique.Photos.Contains("radio face")) && !string.IsNullOrEmpty(CurrentPatient.Img_Rad_Face)))
                CreateImageBtn(CurrentPatient.Img_Rad_Face, "Radio Face");

            if (((diagnostique.Photos.Contains("panoramique")) && !string.IsNullOrEmpty(CurrentPatient.Img_Rad_Pano)))
                CreateImageBtn(CurrentPatient.Img_Rad_Pano, "Panoramique");

            if ((diagnostique.Photos.Contains("radio profil")) && (!string.IsNullOrEmpty(CurrentPatient.Img_Rad_Profile)))
                CreateImageBtn(CurrentPatient.Img_Rad_Profile, "Radio Profil");

        
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



        private void CreateImageBtn(string file, string txt)
        {
            if (exist(file))
            {
                var req = System.Net.WebRequest.Create(file);
                using (System.IO.Stream stream = req.GetResponse().GetResponseStream())
                {
                    PictureBox pbx = new PictureBox();
                    pbx.Image = (Bitmap)System.Drawing.Bitmap.FromStream(stream);
                    pbx.SizeMode = PictureBoxSizeMode.Zoom;
                    pbx.Tag = file;
                    pbx.BorderStyle = BorderStyle.FixedSingle;
                    pbx.Click += btn_Click;
                    pnlBtns.Controls.Add(pbx);

                    pbx.Height = pbx.Width;

                }

            }

        }

        void btn_Click(object sender, EventArgs e)
        {


            FrmQuickView frm = new FrmQuickView((string)((PictureBox)sender).Tag);
            frm.Show(this);



        }

        private void pnlBtns_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmLittleWizard_Shown(object sender, EventArgs e)
        {
            Screen s = Screen.FromPoint(this.Location);

            Location = new Point(s.Bounds.Right-this.Width-5,s.Bounds.Bottom-this.Height-5);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BaseCommonControls.FrmChoixDents frm = new BaseCommonControls.FrmChoixDents();
            frm.ShowDialog();
            linkLabel1.Text = frm.value;
            linkLabel1.Tag = frm.value;
        }

        private void lblQuestion_Click(object sender, EventArgs e)
        {

        }
    }
}
