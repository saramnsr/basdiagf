using BasCommon_BL;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BasCommon_BL
{
    public static class SmilersMgmt
    {
        public static string createAndUpload(string json)
        {
            string tokenSmilers = System.Configuration.ConfigurationManager.AppSettings["tokenSmilers"];
            string url = System.Configuration.ConfigurationManager.AppSettings["urlSmilers"];

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("apiKey", tokenSmilers); //+ tokenSmilers

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            int x;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                return result;
            }

        }
        public static void insertSmiler(InfoSmilers smilers)
        {
            DAC.insertSmiler(smilers);
        }
        public static void updatetSmiler(InfoSmilers smilers)
        {
            DAC.updateSmiler(smilers);
        }
        public static void insertSmilerSuivi(InfoSmilers smilers)
        {
            DAC.insertSmilerSuivi(smilers);
        }
        public static void updatetSmilerSuivi(InfoSmilers smilers)
        {
            DAC.updateSmilerSuivi(smilers);
        }
        public static InfoSmilers  getInfoSmilers(int idpatient)
        {
           InfoSmilers info = new InfoSmilers();
           try
           {
               JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet("/InfoSmilers/" + idpatient);
               info = BasCommon_BL.Builders.BuildSmilers.Build(obj);
                    return info;

           }catch(Exception e)
           {
               return null;
           }
        }
        public static List<InfoSmilers> getInfoSmilersSuivi(int idpatient)
        {
            List<InfoSmilers> info = new List<InfoSmilers>();
            try
            {
                JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/InfoSmilersSuivilistbypat/" + idpatient);
               // JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet("/InfoSmilersSuivilistbypat/" + idpatient);
                foreach (JObject dr in json)
                {
                   InfoSmilers sm = BasCommon_BL.Builders.BuildSmilers.Build(dr);
                    info.Add(sm);
                }
                return info;
             
              

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
