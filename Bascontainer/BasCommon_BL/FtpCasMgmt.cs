using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.IO;
using System.Xml.Serialization;

namespace BasCommon_BL
{


    [Serializable()]
    [System.Xml.Serialization.XmlRoot("FtpCasCollection")]
    public class FtpCasCollection
    {
        [XmlArray("FtpAllCas")]
        [XmlArrayItem("FtpOneCas", typeof(FtpCas))]
        public FtpCas[] Cas { get; set; }
    }


    public static class FtpCasMgmt
    {




        const string DEFINITIONFILE = "definition.xml";

        private static List<FtpCas> _cas = null;
        public static List<FtpCas> cas
        {
            get
            {
                if (_cas == null)
                    GetAllCas();
                return _cas;
            }
            set { _cas = value; }
        }

        public static List<FtpCas> casNonVus
        {
            get
            {
                return cas.Where(c => c.DateLastVisu == null).Select(c => c).ToList();
            }
        }

        public static int NbcasNonVus
        {
            get
            {
                if (cas == null) return 0;
                return cas.Where(c => c.DateLastVisu == null).Count();
            }
        }

        static object k = new object();
        public static void GetAllCas()
        {
            lock (k)
            {
                string ftphost = (string)System.Configuration.ConfigurationManager.AppSettings["ftphost"];
                string ftpuser = (string)System.Configuration.ConfigurationManager.AppSettings["ftpuser"];
                string ftppwd = (string)System.Configuration.ConfigurationManager.AppSettings["ftppwd"];

                BasCommon_BL.ftp ftp = new ftp(ftphost, ftpuser, ftppwd);

                string defile = Path.GetTempFileName();

                ftp.download(DEFINITIONFILE, defile);

                if (File.Exists(defile))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(FtpCasCollection));

                        StreamReader reader = new StreamReader(defile);
                        FtpCasCollection collec = (FtpCasCollection)serializer.Deserialize(reader);
                        _cas = collec.Cas.ToList();
                        reader.Close();
                    }
                    catch (System.Exception e) { _cas = new List<FtpCas>(); }
                }
            }

        }

        public static void SaveAllCas()
        {

            string ftphost = (string)System.Configuration.ConfigurationManager.AppSettings["ftphost"];
            string ftpuser = (string)System.Configuration.ConfigurationManager.AppSettings["ftpuser"];
            string ftppwd = (string)System.Configuration.ConfigurationManager.AppSettings["ftppwd"];



            FtpCasCollection collec = new FtpCasCollection();
            collec.Cas = cas.ToArray();

            string tempfile = Path.GetTempFileName();

            FileStream fs = new FileStream(tempfile, FileMode.Create, FileAccess.Write);

            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(FtpCasCollection));
            x.Serialize(fs, collec);

            fs.Flush();
            fs.Close();

            BasCommon_BL.ftp ftp = new ftp(ftphost, ftpuser, ftppwd);
            ftp.upload(DEFINITIONFILE, tempfile);

        }

        public static void DeleteCasFile(FtpCas cas)
        {

            string ftphost = (string)System.Configuration.ConfigurationManager.AppSettings["ftphost"];
            string ftpuser = (string)System.Configuration.ConfigurationManager.AppSettings["ftpuser"];
            string ftppwd = (string)System.Configuration.ConfigurationManager.AppSettings["ftppwd"];


            BasCommon_BL.ftp ftp = new ftp(ftphost, ftpuser, ftppwd);
            ftp.delete(cas.Fichier);

        }

        public static void UpdateCas(FtpCas element)
        {
            GetAllCas();

            foreach (FtpCas c in cas)
                if (c.IdPatient == element.IdPatient && c.Site == element.Site)
                {
                    c.DateLastVisu = element.DateLastVisu;
                    c.IsNew = element.IsNew;

                }

            SaveAllCas();
        }

        public static void DeleteCas(FtpCas element)
        {
            GetAllCas();

            for (int i = cas.Count - 1; i >= 0; i--)
            {
                FtpCas c = cas[i];
                if (c.IdPatient == element.IdPatient && c.Site == element.Site)
                {
                    cas.Remove(cas[i]);
                    break;

                }
            }

            SaveAllCas();
            DeleteCasFile(element);
        }

    }
}
