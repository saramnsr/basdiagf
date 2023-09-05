using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Configuration;
using System.Data;
using BasCommon_DAL;
using System.IO;
using System.Xml;
using Ionic.Zip;
using System.Diagnostics;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
namespace BasCommon_BL
{
    public static class ImagesMgmt
    {
        public static void AffectImageToPatient(basePatient p_pat)
        {
            AffectImageToPatient(basePatient.RepertoireImage + "/" + p_pat.Repertoire, p_pat);
        }
        public static void AffectImageToPatient(string imagefolder, basePatient p_pat)
        {

            if (!Directory.Exists(p_pat.Repertoire))
                baseMgmtPatient.AffectRepertoireToPatient(p_pat);

             if (p_pat.lstObjFrmBasPhoto == null)
            {
                p_pat.lstObjFrmBasPhoto = new List<ObjImage>();



                List<ObjImage> lst = getObjectOf(p_pat);

                foreach (ObjImage obj in lst)
                    p_pat.lstObjFrmBasPhoto.Add(obj);

                ReaffectStandardFolders(basePatient.RepertoireImage + "/" + p_pat.Repertoire, p_pat);
            }

        }

        public static void ReaffectStandardFolders(string imagefolder, basePatient p_pat)
        {
            p_pat.lstObjFrmBasPhoto.Sort();
            foreach (ObjImage obj in p_pat.lstObjFrmBasPhoto)
            {

                if (obj.HasAttribut(ConfigurationManager.AppSettings["document"]))
                {
                    if (obj.HasAttribut(ConfigurationManager.AppSettings["Questionnaire médical signé"]))
                        p_pat.QuestionnaireMedical = imagefolder + "/" + obj.fichier;

                    if (obj.HasAttribut(ConfigurationManager.AppSettings["Consentement signé"]))
                        p_pat.ConsentementSigne = imagefolder + "/" + obj.fichier;

                    if (obj.HasAttribut(ConfigurationManager.AppSettings["Devis signé"]))
                        p_pat.DevisSigne = imagefolder + "/" + obj.fichier;
                }
                


                // Portrail du profil : portrait + profil
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Profile = imagefolder + "/" + obj.fichier;

                // Portrait du face : portrait + face
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Face = imagefolder + "/" + obj.fichier;


                // Portrait face sourire : portrait + face + sourire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Face_Sourire = imagefolder + "/" + obj.fichier;

                // Portrait profil sourire : portrait + face + sourire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Profile_Sourire = imagefolder + "/" + obj.fichier;

                // Sourire face : sourire + portrait
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Sourire = imagefolder + "/" + obj.fichier;

                // Maxillaire : intrabuccale + maxillaire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Maxillaire"]))
                    p_pat.Img_Int_Max = imagefolder + "/" + obj.fichier;


                // Mandibulaire : intrabuccale + mandibulaire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Mandibulaire"]))
                    p_pat.Img_Int_Man = imagefolder + "/" + obj.fichier;

                // Intra droite : intrabuccale + droite
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Droite"]))
                    p_pat.Img_Int_Droit = imagefolder + "/" + obj.fichier;

                // Intra surplomb : intrabuccale + surplomb
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Surplomb"]))
                    p_pat.Img_Int_SurPlomb = imagefolder + "/" + obj.fichier;


                // Intra face : intrabuccale + face
                //

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Int_Face = imagefolder + "/" + obj.fichier;

                // Intra gauche : intrabuccale + gauche
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Gauche"]))
                    p_pat.Img_Int_Gauche = imagefolder + "/" + obj.fichier;

                // Radio profil : Radio + Profil
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]))
                    p_pat.Img_Rad_Profile = imagefolder + "/" + obj.fichier;


                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Panoramique"]))
                    p_pat.Img_Rad_Pano = imagefolder + "/" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Rad_Face = imagefolder + "/" + obj.fichier;

                //moulages
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Maxillaire"]))
                    p_pat.Img_Moul_Max = imagefolder + "/" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Mandibulaire"]))
                    p_pat.Img_Moul_Man = imagefolder + "/" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Droite"]))
                    p_pat.Img_Moul_Droit = imagefolder + "/" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Gauche"]))
                    p_pat.Img_Moul_Gauche = imagefolder + "/" + obj.fichier;


                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Moul_Face = imagefolder + "/" + obj.fichier;

                //bouche ouvert
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                   !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Bouche"]) &&
                   !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Ouvert"]) &&
                   obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]))
                    p_pat.Img_Innoclusion = imagefolder + "/" + obj.fichier;




            }
        }
        
        private static void AffectAttributsToImage(List<ObjImage> img)
        {
            JArray json = DAC.getMethodeJsonArray("/AttributPatient/" + img[0].Idpatient);

            foreach (JObject r in json)
            {
                int idObjet = Convert.ToInt32(r["pkObjet"]);
               
                Attribut att = Builders.BuildAttribut.BuildAttributJson(r);
              if(img.Find(o => o.Id == idObjet) != null)
                img.Find(o => o.Id == idObjet).attributs.Add(att);
            }
         
        }
        private static void AffectAttributsToImageOLDJ(ObjImage img)
        {
            JArray json = DAC.getMethodeJsonArray("/Attributss/" + img.Id);


            foreach (JObject r in json)
            {
                Attribut att = Builders.BuildAttribut.BuildAttributJson(r);
                img.attributs.Add(att);
            }

        }
        private static void AffectAttributsToImageOLD(ObjImage img)
        {
            DataTable dt = DAC.getAttribut(img);

            List<ObjImage> lst = new List<ObjImage>();

            foreach (DataRow r in dt.Rows)
            {
                Attribut att = Builders.BuildAttribut.Build(r);
                img.attributs.Add(att);
            }

        }
        private static DataTable AffectAttributsToImageByPatients(int idPatient)
        {
           return  DAC.getAttributsByPatients(idPatient);
        }
        public static string SaveFilePatient(basePatient p_pat, string savedfile, string commentaire,int[] Duree)
        {
            string err = "";


            if ((string.IsNullOrEmpty(p_pat.Nom))||
                string.IsNullOrEmpty(p_pat.Prenom)||
                (p_pat.DateNaissance.Year<1914))
            {
                err = "Erreur dans les données administartives du patient";
                return err;
            }

            if (p_pat.contacts == null)
                baseMgmtPatient.FillContacts(p_pat);

            AffectImageToPatient(p_pat);

            //int[] GabaritObligatoire = new int[]{45,50,49,41,40,57,51,37,58,38,42,69};
            Dictionary<int, string> GabaritObligatoire = new Dictionary<int, string>();
            GabaritObligatoire.Add(45,"Intra-Face");
            GabaritObligatoire.Add(50,"Intra-Droit");
            GabaritObligatoire.Add(49,"Intra-Gauche");
            GabaritObligatoire.Add(41,"Intra-Mandibulaire");
            GabaritObligatoire.Add(40,"Intra-Maxilaire");
            GabaritObligatoire.Add(57,"Surplomb");
            GabaritObligatoire.Add(51,"Sourire Face");
            GabaritObligatoire.Add(37,"Face repos");
            GabaritObligatoire.Add(58, "Face sourire");
            GabaritObligatoire.Add(38, "Profil repos");
            GabaritObligatoire.Add(42,"Profil Sourire");
            GabaritObligatoire.Add(67, "Bouche ouverte Face");


            Dictionary<int,bool> GabaritOk = new Dictionary<int,bool>();
            foreach(var i in GabaritObligatoire)
                GabaritOk.Add(i.Key,false);

            foreach (var o in p_pat.lstObjFrmBasPhoto)
            {
                if (GabaritOk.ContainsKey(o.IdGabarit))
                    GabaritOk[o.IdGabarit] = true;
            }

            string lstimgob = "";

            foreach(var kv in GabaritOk)
                if (!kv.Value)
                    lstimgob += "-" + GabaritObligatoire[kv.Key] + "\n";
                

            if (lstimgob != "")
            {
                err = "Liste d'images obligatoires : \n";
                err += lstimgob;
                return err;
            }

            if (p_pat.CommentsHisto == null)
                p_pat.CommentsHisto = BasCommon_BL.MgmtCommentairesHisto.GetAllCommentaires(p_pat);

            if (p_pat.commentairesClinique == null)
                p_pat.commentairesClinique = BasCommon_BL.MgmtCommentairesFaitAFaire.GetCommCliniques(p_pat);

            string tempfolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

#if TRACE
            Console.WriteLine(" tempfolder : " + tempfolder);
#endif

            Directory.CreateDirectory(tempfolder);

            foreach (ObjImage obj in p_pat.lstObjFrmBasPhoto)
            {

                string src = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;
                string dest = tempfolder + "\\" + obj.fichier;
#if TRACE
                Console.WriteLine(" ObjImage : " + obj.fichier);
                Console.WriteLine(" src : " + src);
                Console.WriteLine(" dest : " + dest);
#endif

                try
                {
                    if (File.Exists(src))
                        File.Copy(src, dest);
                }
                catch (System.Exception ex)
                {
#if TRACE
                    Console.WriteLine(" ex.Message : " + ex.Message);
#endif
                    err += "\n"+src+" : "+ex.Message;
                }

            }

            string Patfilename = tempfolder + "\\patient.xml";

            StringWriter memoryStream = new StringWriter();

            XmlTextWriter writer = new XmlTextWriter(memoryStream);
            writer.Formatting = Formatting.Indented;

            CustomXMLSerializer.WriteXML(p_pat, writer, "Main");

            File.WriteAllText(Patfilename, memoryStream.ToString());

#if TRACE
            Console.WriteLine(" Patient : " + Patfilename);
#endif

            string Commentfilename = tempfolder + "\\commentaires.txt";
            File.WriteAllText(Commentfilename, commentaire);
#if TRACE
            Console.WriteLine(" File.WriteAllText to " + Commentfilename);
#endif

            string Solutionsfilename = tempfolder + "\\Solutions.txt";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Duree.Length; i++)
                sb.AppendLine("Solution " + i.ToString() + ";" + Duree[i]);
            File.WriteAllText(Solutionsfilename, sb.ToString());
#if TRACE
            Console.WriteLine(" File.WriteAllText to " + Solutionsfilename);
#endif

            string s = System.Configuration.ConfigurationManager.AppSettings["InvisalignLogin"];
            s+=";"+System.Configuration.ConfigurationManager.AppSettings["InvisalignPassword"];

            string InvisalignFile = tempfolder + "\\InvisalignFile.txt";
            File.WriteAllText(InvisalignFile,s );
#if TRACE
            Console.WriteLine(" File.WriteAllText to " + InvisalignFile);
#endif

            using (ZipFile zip = new ZipFile())
            {

                zip.AddDirectory(tempfolder);
                zip.Save(savedfile);
            }

#if TRACE
            Console.WriteLine(" zip to : " + savedfile);
#endif
            try
            {
                Directory.Delete(tempfolder, true);
            }
            catch (System.Exception ex)
            {
                err = ex.Message;
            }
#if TRACE
            Console.WriteLine(" tempfolder deleted ");
#endif
            return err;

        }


        public static basePatient OpenFilePatient(string fichier, ref string tempfolder, ref string comm,ref InvisalignAccount account,ref int[] DureeSolution)
        {

            tempfolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(tempfolder);



            using (ZipFile zip = new ZipFile(fichier))
            {
                zip.ExtractAll(tempfolder);
            }


            try
            {
                string invfilename = tempfolder + "\\InvisalignFile.txt";

                account = new InvisalignAccount();
                string s = File.ReadAllText(invfilename);
                account.login = s.Split(';')[0];
                account.password = s.Split(';')[1];
            }
            catch (Exception) { account = null; }


            try
            {
                string invfilename = tempfolder + "\\Solutions.txt";


                string[] ss = File.ReadAllLines(invfilename);

                DureeSolution = new int[4];

                for (int i = 0; i < ss.Length; i++)
                    DureeSolution[i] = Convert.ToInt32(ss[i].Split(';')[1]);

            }
            catch (Exception) { account = null; }
            

            string commfilename = tempfolder + "\\commentaires.txt";

            comm = File.ReadAllText(commfilename);

            string Patfilename = tempfolder + "\\patient.xml";
            basePatient mdl = new basePatient();

            XmlTextReader rd = null;

            rd = new XmlTextReader(Patfilename);

            try
            {
                CustomXMLSerializer.ReadXML(mdl, rd);
                return mdl;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                rd.Close();
            }

        }


        public static List<ObjImage> getObjectOf(basePatient p_pat)
        {
            JArray json = DAC.getMethodeJsonArray("/objet_by_patient/" + p_pat.Id);
            List<ObjImage> lst = new List<ObjImage>();

            foreach (JObject r in json)
            {
                ObjImage image = Builders.BuildObjImage.BuildJ(r);
                lst.Add(image);
            }
            if(lst.Count> 0)
            AffectAttributsToImage(lst);
            return lst;

        }
        public static List<ObjImage> getObjectOfOLD(basePatient p_pat)
        {
            DataTable dt = DAC.getObjectOf(p_pat);
            DataTable dtAttrs = AffectAttributsToImageByPatients(p_pat.Id);
            List<ObjImage> lst = new List<ObjImage>();

            foreach (DataRow r in dt.Rows)
            {
                ObjImage image = Builders.BuildObjImage.Build(r);
                DataRow[] tmpdt = dtAttrs.Select("PK_OBJET=" + image.Id);
                foreach (DataRow d in tmpdt)
                {
                    image.attributs.Add(Builders.BuildAttribut.Build(d));
                }
              //  AffectAttributsToImage(image);

                lst.Add(image);
            }

            return lst;

        }
        public static string CreatePatientDossier(string rep)
        {
            try
            {
                Uri url = new Uri(rep);
                string absolutePath = url.AbsolutePath;
                string res = BasCommon_DAL.DAC.getMethodeJsonString("/CreatePatientDossier?rep=" + absolutePath);
                /*  if (System.IO.Directory.Exists(newPath) == false)
                      System.IO.Directory.CreateDirectory(newPath);*/
                return absolutePath.Substring(1);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static void insert(string nom, string path, DateTime dtPrise, DateTime dtApersus, int idPatien, Image Apercus, string fullpath)
        {
            DAC.Insert(nom, path, dtPrise, dtApersus, idPatien, Apercus, fullpath);

        }
        private static void PostForm(string postUrl, string contentType, byte[] formData)
        {
            Uri address = new Uri(postUrl);
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            request.Expect = "";
            request.Headers.Add("Authorization", "bearer " + baseMgmtPatient.token);


            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
                requestStream.Dispose();
            }
            HttpWebResponse rep = request.GetResponse() as HttpWebResponse;

            rep.Close();

        }
        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is BasCommon_BL.Invisalign.FileParameter)
                {
                    BasCommon_BL.Invisalign.FileParameter fileToUpload = (BasCommon_BL.Invisalign.FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
        public static void MultipartFormDataPost(string postUrl, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            PostForm(postUrl, contentType, formData);
        }
        public static void saveImage(byte[] fileBytes, string nomImage, string path)
        {

            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("file", new BasCommon_BL.Invisalign.FileParameter(fileBytes, nomImage, "application/octet-stream"));

            MultipartFormDataPost(DAC.PathRest + "/SaveImageNew/image/" + nomImage + "?path=" + path, postParameters);
        }
        public static void saveToBasView(byte[] bytes, int idPatient, string fileName, string fullpath,string path)
        {
            Image img = (Bitmap)(new ImageConverter()).ConvertFrom(bytes);

            int w;
            int h;

            float ratio = (float)img.Width / (float)img.Height;
            w = Convert.ToInt32(200);
            h = Convert.ToInt32(200 / ratio);

            if ((img.Width * ratio) > 200)
            {
                w = Convert.ToInt32(200 * ratio);
                h = Convert.ToInt32(200);
            }

            Image Apercus = img.GetThumbnailImage(w, h, null, System.IntPtr.Zero);

            BasCommon_BL.ImagesMgmt.insert(fileName, path, DateTime.Now, DateTime.Now, idPatient, Apercus, fullpath);
        }
        public static String FindRepertoirePatient(int IdPatient)
        {
            string OldRep = DAC.GetOldRepertoireFromKitView(IdPatient);

            if ((OldRep != null) && (OldRep != ""))
            {
                return OldRep;
            }
            else
            {
                System.Data.DataTable dt = DAC.SelectPatientById(IdPatient);

                string nom = Convert.ToString(dt.Rows[0]["per_nom"]).Trim();
                string prenom = Convert.ToString(dt.Rows[0]["per_prenom"]).Trim();
                DateTime datenaiss = Convert.ToDateTime(dt.Rows[0]["per_datnaiss"]);
                return nom + " " + prenom + " " + datenaiss.ToString("ddMMyyyy");
            }
        }
        public static byte[] ImageToByte(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
