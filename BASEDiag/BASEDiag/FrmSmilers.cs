using BasCommon_BL;
using BasCommon_BO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Windows.Forms;

namespace BASEDiag
{
    public partial class FrmSmilers : Form
    {
        public FrmSmilers(basePatient pat)
        {
            InitializeComponent();
            CurrentPatient = pat;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnsuivant_Click(object sender, EventArgs e)
        {
            pnlInfo.Hide();
            panel1.Show();
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
        private string ConvertToBase64(string url)
        {
            var webClient = new WebClient();
            byte[] imageBytes = webClient.DownloadData(url);
            return Convert.ToBase64String(imageBytes);

        }
        private Stream fromStream(String url)
        {
            var req = System.Net.WebRequest.Create(url);

            return req.GetResponse().GetResponseStream();

        }
        private void initDisplayPhotosRadiosPatient()
        {

            if (IsFileExist(CurrentPatient.Img_Rad_Pano))
            {
                Stream stream = fromStream(CurrentPatient.Img_Rad_Pano);
                PANORAMIC_X_RAY.Image = Bitmap.FromStream(stream);
                PANORAMIC_X_RAY.Tag = ConvertToBase64(CurrentPatient.Img_Rad_Pano);
            }




            if (IsFileExist(CurrentPatient.Img_Ext_Profile))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Profile);
                PROFILE.Image = Bitmap.FromStream(stream);
                PROFILE.Tag = ConvertToBase64(CurrentPatient.Img_Ext_Profile);
            }

            if (IsFileExist(CurrentPatient.Img_Ext_Face_Sourire))
            {
                Stream stream = fromStream(CurrentPatient.Img_Ext_Face_Sourire);
                FRONTAL_VIEW_SMILE.Image = Bitmap.FromStream(stream);
                FRONTAL_VIEW_SMILE.Tag = ConvertToBase64(CurrentPatient.Img_Ext_Face_Sourire);
            }

            if (IsFileExist(CurrentPatient.Img_Int_Face))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Face);
                OCCLUSAL_VIEW_FRONT.Image = Bitmap.FromStream(stream);
                OCCLUSAL_VIEW_FRONT.Tag = ConvertToBase64(CurrentPatient.Img_Int_Face);
            }


            if (IsFileExist(CurrentPatient.Img_Int_Gauche))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Gauche);
                OCCLUSAL_VIEW_LEFT.Image = Bitmap.FromStream(stream);
                OCCLUSAL_VIEW_LEFT.Tag = ConvertToBase64(CurrentPatient.Img_Int_Gauche);
            }

            if (IsFileExist(CurrentPatient.Img_Int_Droit))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Droit);
                OCCLUSAL_VIEW_RIGHT.Image = Bitmap.FromStream(stream);
                OCCLUSAL_VIEW_RIGHT.Tag = ConvertToBase64(CurrentPatient.Img_Int_Droit);
            }

            if (IsFileExist(CurrentPatient.Img_Int_Max))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Max);
                OCCLUSAL_VIEW_MAXILLA.Image = Bitmap.FromStream(stream);
                OCCLUSAL_VIEW_MAXILLA.Tag = ConvertToBase64(CurrentPatient.Img_Int_Max);
            }

            if (IsFileExist(CurrentPatient.Img_Int_Man))
            {
                Stream stream = fromStream(CurrentPatient.Img_Int_Man);
                OCCLUSAL_VIEW_MANDIBULA.Image = Bitmap.FromStream(stream);
                OCCLUSAL_VIEW_MANDIBULA.Tag = ConvertToBase64(CurrentPatient.Img_Int_Man);
            }

            if (IsFileExist(CurrentPatient.Img_Innoclusion))
            {
                Stream stream = fromStream(CurrentPatient.Img_Innoclusion);
                INOCCLUSION.Image = Bitmap.FromStream(stream);
                INOCCLUSION.Tag = ConvertToBase64(CurrentPatient.Img_Innoclusion);
            }
            if (IsFileExist(CurrentPatient.Img_Rad_Profile))
            {
                Stream stream = fromStream(CurrentPatient.Img_Rad_Profile);
                TELERADIO.Image = Bitmap.FromStream(stream);
                TELERADIO.Tag = ConvertToBase64(CurrentPatient.Img_Rad_Profile);
            }
            this.Cursor = Cursors.Default;

        }
        private void initDisplayInfosPatient()
        {

            txtbxNom.Text = baseMgmtPatient.RemoveDiacritics(CurrentPatient.Nom);
            txtbxPrenom.Text = baseMgmtPatient.RemoveDiacritics(CurrentPatient.Prenom);
            string civility = CurrentPatient.Civilite.ToLower().Replace(".", "");
            if (civility == "mr" || civility == "m")
                rdMrs.Checked = true;
            else
                if (civility == "ms" || civility == "mlle")
                    rdMs.Checked = !rdMrs.Checked;
                else
                    if (civility == "mrs" || civility == "mme")
                        rdMrs.Checked = true;
                    else
                        if (civility == null || civility == "")
                        {
                            if (CurrentPatient.Genre == basePatient.Sexe.Feminin)
                                rdMs.Checked = true;
                            else
                                rdMrs.Checked = true;
                        }

            try
            {
                dtpDateNaiss.Value = CurrentPatient.DateNaissance;
            }
            catch (System.Exception)
            {
                dtpDateNaiss.Value = dtpDateNaiss.MinDate;
            }



        }
        private void pnlInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmSmilers_Load(object sender, EventArgs e)
        {
            initDisplayInfosPatient();
            initDisplayPhotosRadiosPatient();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            pnlInfo.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string json = buildJson();
            string result = SmilersMgmt.createAndUpload(json);
            JObject jObject = JObject.Parse(result);
            JToken token = jObject.SelectToken("caseData");
            int orderid = (int)token.SelectToken("orderId");
            InfoSmilers info = new InfoSmilers();
            info.orderid = orderid;
            info.idPatient = CurrentPatient.Id;
            info.genre = civility;
            SmilersMgmt.insertSmiler(info);
            CurrentPatient.infoSmilers = new InfoSmilers();
            CurrentPatient.infoSmilers = info;
            DialogResult = DialogResult.OK;
            this.Close();
        }
        string civility = "";
        private string buildJson()
        {
            if (rdMr.Checked)
                civility = "Mr";
            else
                if (rdMs.Checked)
                    civility = "Ms";
                else
                    if (rdMrs.Checked)
                        civility = "Mrs";
            string photos = "";
            //    +  ? "" : // base64 du fichier + nom du fichier 
            if (FRONTAL_VIEW_SMILE.Tag != null)
                photos += "\"PIC1\": [\"data:image/jpg;base64," + FRONTAL_VIEW_SMILE.Tag + "\",\"FRONTAL_VIEW_SMILE.JPG\"]";
            if (PROFILE.Tag != null)
                photos += ",\"PIC2\": [\"data:image/jpg;base64," + PROFILE.Tag + "\",\"PROFILE.JPG\"]";
            if (OCCLUSAL_VIEW_FRONT.Tag != null)
                photos += ",\"PIC3\": [\"data:image/jpg;base64," + OCCLUSAL_VIEW_FRONT.Tag + "\",\"OCCLUSAL_VIEW_FRONT.JPG\"]";
            if (OCCLUSAL_VIEW_LEFT.Tag != null)
                photos += ",\"PIC4\": [\"data:image/jpg;base64," + OCCLUSAL_VIEW_LEFT.Tag + "\",\"OCCLUSAL_VIEW_LEFT.JPG\"]";
            if (OCCLUSAL_VIEW_RIGHT.Tag != null)
                photos += ",\"PIC5\": [\"data:image/jpg;base64," + OCCLUSAL_VIEW_RIGHT.Tag + "\",\"OCCLUSAL_VIEW_RIGHT.JPG\"]";
            if (OCCLUSAL_VIEW_MAXILLA.Tag != null)
                photos += ",\"PIC6\": [\"data:image/jpg;base64," + OCCLUSAL_VIEW_MAXILLA.Tag + "\",\"OCCLUSAL_VIEW_MAXILLA.JPG\"]";
            if (OCCLUSAL_VIEW_MANDIBULA.Tag != null)
                photos += ",\"PIC7\": [\"data:image/jpg;base64," + OCCLUSAL_VIEW_MANDIBULA.Tag + "\",\"OCCLUSAL_VIEW_MANDIBULA.JPG\"]";
            if (PANORAMIC_X_RAY.Tag != null)
                photos += ",\"PIC8\": [\"data:image/jpg;base64," + PANORAMIC_X_RAY.Tag + "\",\"PANORAMIC_X_RAY.JPG\"]";
            if (INOCCLUSION.Tag != null)
                photos += ",\"PIC9\": [\"data:image/jpg;base64," + INOCCLUSION.Tag + "\",\"INOCCLUSION.JPG\"]";
            if (TELERADIO.Tag != null)
                photos += ",\"PIC10\": [\"data:image/jpg;base64," + TELERADIO.Tag + "\",\"TELERADIO.JPG\"]";
            string json = "{"
          + "\"case\": { "

          + "\"patient\": { "

           + "\"firstName\": \"" + txtbxPrenom.Text + "\", "

           + "\"lastName\": \"" + txtbxNom.Text + "\", "

           + "\"birthDate\": \"" + dtpDateNaiss.Value.ToString("dd/MM/yyyy") + "\", " // Format jj/mm/aaaa ou aaaammjj 

           + "\"civility\": \"" + civility + "\"" // Choix: mr/mrs/ms 

        + "  }, "

           //+ "\"impressions\": { "

          // +"\"zip\": [\"data:@file/zip;base64,xxx\",\"test.zip\"]" // base64 du fichier + nom du fichier 

          //+ " }, "

          + "\"photos\": { "

              + photos

        + "}"
          + "}"
       + "   } ";

            return json;
        }


    }
}
