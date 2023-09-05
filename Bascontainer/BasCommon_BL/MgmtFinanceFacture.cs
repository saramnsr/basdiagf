using BasCommon_BL;
using BasCommon_BL.Builders;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BasCommon_BL
{
    public static class MgmtFinanceFacture
    {
        public static void InsertFacture(FinanceFacture facture)
        {

            DAC.AddFacture(facture);

        }
        public static int maxId()
        {

            return DAC.getNextId();

        }
        public static void deleteFacture(FinanceFacture facture)
        {

            DAC.deleteFacture(facture);

        }
        //public static List<FinanceGraphStat> getFacturessByYear(int year)
        //{
        //    JArray json = DAC.getMethodeJsonArray("/FactureByYear/" + year);

        //    List<FinanceGraphStat> lst = new List<FinanceGraphStat>();

        //    foreach (JObject dr in json)
        //    {
        //        FinanceGraphStat d = Builders.BuildFactureByYear(dr);
        //        lst.Add(d);

        //    }

        //    return lst;
        //}
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

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

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
        public static string _globalFactureFolderPath = ConfigurationSettings.AppSettings["PHOTO_FOLDER_PATH" + DAC.prefix];

        public static void saveImage(byte[] fileBytes, string nomImage )
        {
            Uri url = new Uri(_globalFactureFolderPath);
            string absolutePath = url.AbsolutePath;
            absolutePath = absolutePath.Substring(1, absolutePath.Length - 1);
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("file", new FileParameter(fileBytes, nomImage, "application/octet-stream"));

            MultipartFormDataPost(DAC.PathRest + "/SaveImageNew/image/" + nomImage + "?path=" + absolutePath, postParameters);
        }
        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
        public static List<FinanceFacture> GetAllFacture(DateTime dte1, DateTime dte2)
        {
            JArray json = DAC.getMethodeJsonArray("/AllFacture/" + dte1.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dte2.ToString("yyyy-MM-dd HH:mm:ss"));

            List<FinanceFacture> lst = new List<FinanceFacture>();

            foreach (JObject dr in json)
            {
                FinanceFacture d = BuildFacture.BuildFinance(dr);
                lst.Add(d);

            }

            return lst;
        }
    }
}
