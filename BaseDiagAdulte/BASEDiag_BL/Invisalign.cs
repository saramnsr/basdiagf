using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web;
using BasCommon_BO;
using BasCommon_BL;
using System.Globalization;
using BASEDiag_BO;

namespace BASEDiag_BL
{
    public static class Invisalign
    {

        private static basePatient _patient;
        public static basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }


        public static Cookie Anonymouscookie = null;
        public static List<Cookie> Sessioncookies = new List<Cookie>();


        public static string NewConnect()
        {
            try
            {
                Uri address = new Uri("https://vip.invisalign.com/v3/login.action");
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["TIMEOUT_REQUEST"]);
                request.Method = "GET";


                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";

                // Get response
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                string CookieVal = response.Headers["Set-Cookie"];

                string jsessionID = response.ResponseUri.LocalPath.Split(';')[1];

                Cookie cookiereturned = new Cookie();

                //string[] cookieparams = CookieVal.Split(';');


                cookiereturned.Name = jsessionID.Split('=')[0];
                cookiereturned.Value = jsessionID.Split('=')[1];
                cookiereturned.Domain = "vip.invisalign.com";

                //   if (cookieparams[1].Split('=')[0].Trim().ToLower() == "path") cookiereturned.Path = cookieparams[1].Split('=')[1];


                Anonymouscookie = cookiereturned;

                return "";
            }
            catch (System.UriFormatException ex)
            {
                return ex.Message;
               // throw new System.Exception("L'adresse Invisalign n'est pas correct !", ex);
            }
        }




        // https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=NEXT&currentStep=INFO&vPI=0&patientInfo.pID=0&patientInfo.vPI=0&showBillingAddresses=false&patientInfo.lastName=Test&patientInfo.firstName=Test&patientInfo.middleName=T&patientInfo.gender=MALE&dobDay=09&dobMonth=09&dobYear=1980&patientInfo.sAddressId=390810 

        public static string InsertPatient(basePatient pat)
        {

            try
            {

                DateTime DateNaiss = pat.DateNaissance;

                string lastname = pat.Nom;
                string firstname = pat.Prenom;
                string middlenamename = "";
                string gender = pat.Genre==basePatient.Sexe.Feminin ? "FEMALE" : "MALE";

             //   string IdAdresse =  "390810";
				string IdAdresse = "389053";
             //   Uri address = new Uri("https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=NEXT&currentStep=INFO&vPI=0&patientInfo.pID=0&patientInfo.vPI=0&showBillingAddresses=false&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&patientInfo.sAddressId=" + IdAdresse);
				Uri address = new Uri("https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=NEXT&currentStep=INFO&patientId=0&patientInfo.pid=0&patientInfo.id=&patientInfo.status=REGULAR&patientWorkflow=&patient.workflow=INVISALIGN&showBillingAddresses=true&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&isPostalCode=false&isContactInfoEnabled=false&patientInfo.shippingAddressId=" + IdAdresse + "&patientInfo.billingAddressId=" + IdAdresse + "&patientInfo.soldToAddressId=" + IdAdresse);               
			   HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Keep-Alive", "115");
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";



                CookieContainer cookies = new CookieContainer();
                cookies.Add(Anonymouscookie);

                foreach (Cookie c in Sessioncookies)
                    cookies.Add(c);

                request.CookieContainer = cookies;
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
               
                

                string[] ss = response.Headers["Location"].Substring(response.Headers["Location"].IndexOf('?') + 1).Split('&');


                response.Close();

                // foreach (string s in ss)
                // {
                    // if (s.Split('=')[0] == "vPI")
                        // return s.Split('=')[1];
                // }

				foreach (string s in ss)
                {
                    if (s.Split('=')[0] == "patientId")
                        return s.Split('=')[1];
                }
                return "";
            }
            catch (System.UriFormatException ex)
            {
                //throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                return ex.Message;
            }
            catch (System.Exception)
            {
                return "";
            }

        }

        public static string InsertPatientPost(basePatient pat)
        {

            try
            {

                DateTime DateNaiss = pat.DateNaissance;

                string lastname = pat.Nom;
                string firstname = pat.Prenom;
                string middlenamename = "";
                string gender = pat.Genre == basePatient.Sexe.Feminin ? "FEMALE" : "MALE";

             //   string IdAdresse = "390810";
				string IdAdresse = "389053";
                


                   string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=0&currentStep=INFO";
                string url = "https://vip.invisalign.com/v3/auth/patient/editPatient.action";


                StringBuilder data = new StringBuilder();
                //action=NEXT&currentStep=NOTES&vPI=2973509&patientInfo.pID=0&patientInfo.vPI=2973509&

               // data.Append("action=NEXT&currentStep=INFO&vPI=0&patientInfo.pID=0&patientInfo.vPI=0&showBillingAddresses=false&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&patientInfo.sAddressId=" + IdAdresse);
                data.Append("action=NEXT&currentStep=INFO&patientId=0&patientInfo.pid=0&patientInfo.id=&patientInfo.status=REGULAR&patientWorkflow=&patient.workflow=INVISALIGN&showBillingAddresses=true&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&isPostalCode=false&isContactInfoEnabled=false&patientInfo.shippingAddressId=" + IdAdresse + "&patientInfo.billingAddressId=" + IdAdresse + "&patientInfo.soldToAddressId=" + IdAdresse);

            

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);

                HttpWebResponse response = PostForm(url,useragent,contenttype,byteData,referer);
                

                string[] ss = response.ResponseUri.Query.Substring( 1).Split('&');


                response.Close();

                // foreach (string s in ss)
                // {
                    // if (s.Split('=')[0] == "vPI")
                        // return s.Split('=')[1];
                // }
				foreach (string s in ss)
                {
                    if (s.Split('=')[0] == "patientId")
                        return s.Split('=')[1];
                }

                return "";
            }
            catch (System.UriFormatException ex)
            {
                //throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                return ex.Message;
            }
            catch (System.Exception)
            {
                return "";
            }

        }




        public static string Newlogin()
        {


            string login = System.Configuration.ConfigurationManager.AppSettings["InvisalignLogin"];
            string password = System.Configuration.ConfigurationManager.AppSettings["InvisalignPassword"];

            login = login == null ? "pbergeyr" : login;
            password = password == null ? "patrice82" : password;

            try
            {
                Uri address = new Uri("https://vip.invisalign.com/v3/login.action");
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "POST";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Keep-Alive", "115");
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";

                request.Referer = "https://vip.invisalign.com/v3/login.action";


                CookieContainer cookies = new CookieContainer();
                cookies.Add(Anonymouscookie);

                request.CookieContainer = cookies;
                request.ContentType = "application/x-www-form-urlencoded";

                StringBuilder data = new StringBuilder();
                data.Append("username=" + login);
                data.Append("&password=" + password);
                data.Append("&search=" + "");
                data.Append("&__checkbox_rememberUserName=true");


                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
                // Set the content length in the request headers
                request.ContentLength = byteData.Length;
                // Write data
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }
                request.AllowAutoRedirect = false;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                string CookieVal = response.Headers["Set-Cookie"];

                if (CookieVal == null) return "Impossible de récupérer les cokkies d'authentification"; ;

                Sessioncookies.Clear();
                foreach (string s in CookieVal.Split(','))
                {


                    Cookie Sessioncookie = new Cookie();

                    string[] cookieparams = s.Split(';');


                    Sessioncookie.Name = cookieparams[0].Split('=')[0];
                    Sessioncookie.Value = cookieparams[0].Split('=')[1];
                    Sessioncookie.Domain = "vip.invisalign.com";

                    if (cookieparams[1].Split('=')[0].Trim().ToLower() == "path") Sessioncookie.Path = cookieparams[1].Split('=')[1];

                    Sessioncookies.Add(Sessioncookie);
                }
                return "";
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }



        }


        public static string UpdateConditionsCliniquePOST(string IdInvisalign,
            bool Encombrement,
                bool Espacement,
                bool Class2Div1,
                bool Class2Div2,
                bool Class3,
                bool Beance,
                bool Supraclusion,
                bool ArticuleCroiseAnt,
                bool ArticuleCroisePost,
                bool ArcadeEtroite,
                bool ProAlveolie,
                bool Surplomb,
                bool SourireInesthetique,
                bool AnomalieFormeDentaire,
                bool Autre,
                string AutreTxt,
                string notes)
        {

            if (string.IsNullOrEmpty(IdInvisalign))
                return "Patient Invisalign Not Found";


            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=" + IdInvisalign + "&currentStep=NOTES";
                string url = "https://vip.invisalign.com/v3/auth/patient/editPatient.action";

                
                //Uri address = new Uri(url);
                //HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                //request.Method = "POST";

                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                //request.Headers.Add("Keep-Alive", "115");
                //request.UserAgent = useragent;

                //request.Referer = referer;


                //CookieContainer cookies = new CookieContainer();
                //cookies.Add(Anonymouscookie);

                //foreach (Cookie c in Sessioncookies)
                //    cookies.Add(c);


                //request.CookieContainer = cookies;
                //request.ContentType = contenttype;

                StringBuilder data = new StringBuilder();
                //action=NEXT&currentStep=NOTES&vPI=2973509&patientInfo.pID=0&patientInfo.vPI=2973509&

                data.Append("action=NEXT");
                data.Append("&currentStep=NOTES");
                data.Append("&vPI=" + IdInvisalign);
                data.Append("&patientInfo.pID=0");                
                data.Append("&patientInfo.vPI=" + IdInvisalign);

                data.Append("&patientInfo.chiefConcern['CROWDING']=" + (Encombrement ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['SPACING']=" + (Espacement ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_1']=" + (Class2Div1 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_2']=" + (Class2Div2 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_3']=" + (Class3 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OPEN_BITE']=" + (Beance ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['DEEP_BITE']=" + (Supraclusion ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['ANTERIOR_CROSSBITE']=" + (ArticuleCroiseAnt ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['POSTERIOR_CROSSBITE']=" + (ArticuleCroisePost ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['NARROW_ARCH']=" + (ArcadeEtroite ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['FLARED_TEETH']=" + (ProAlveolie ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OVERJET']=" + (Surplomb ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['UNEVEN_SMILE']=" + (SourireInesthetique ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['MISSHAPEN_TEETH']=" + (AnomalieFormeDentaire ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OTHER']=" + (Autre ? "true" : "false"));
                data.Append("&patientInfo.otherConcern=" + AutreTxt);
                data.Append("&patientInfo.notes=" + notes);

            

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);

              
                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                  return response.StatusCode == HttpStatusCode.OK ? "" : "reponse serveur : " + response.StatusCode.ToString();
            }
            catch (System.UriFormatException ex)
            {
                    throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }



        public static string UpdateConditionsCliniqueGET(string IdInvisalign,
            bool Encombrement,
                bool Espacement,
                bool Class2Div1,
                bool Class2Div2,
                bool Class3,
                bool Beance,
                bool Supraclusion,
                bool ArticuleCroiseAnt,
                bool ArticuleCroisePost,
                bool ArcadeEtroite,
                bool ProAlveolie,
                bool Surplomb,
                bool SourireInesthetique,
                bool AnomalieFormeDentaire,
                bool Autre,
                string AutreTxt,
                string notes)
        {

            if (string.IsNullOrEmpty(IdInvisalign))
                return "Patient Invisalign Not Found";


            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=" + IdInvisalign + "&currentStep=NOTES";
                string url = "https://vip.invisalign.com/v3/auth/patient/editPatient.action";


                //Uri address = new Uri(url);
                //HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                //request.Method = "POST";

                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                //request.Headers.Add("Keep-Alive", "115");
                //request.UserAgent = useragent;

                //request.Referer = referer;


                //CookieContainer cookies = new CookieContainer();
                //cookies.Add(Anonymouscookie);

                //foreach (Cookie c in Sessioncookies)
                //    cookies.Add(c);


                //request.CookieContainer = cookies;
                //request.ContentType = contenttype;

                StringBuilder data = new StringBuilder();
                //action=NEXT&currentStep=NOTES&vPI=2973509&patientInfo.pID=0&patientInfo.vPI=2973509&

                data.Append("action=NEXT");
                data.Append("&currentStep=NOTES");
                data.Append("&vPI=" + IdInvisalign);
                data.Append("&patientInfo.pID=0");
                data.Append("&patientInfo.vPI=" + IdInvisalign);

                data.Append("&patientInfo.chiefConcern['CROWDING']=" + (Encombrement ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['SPACING']=" + (Espacement ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_1']=" + (Class2Div1 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_2']=" + (Class2Div2 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['CLASS_3']=" + (Class3 ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OPEN_BITE']=" + (Beance ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['DEEP_BITE']=" + (Supraclusion ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['ANTERIOR_CROSSBITE']=" + (ArticuleCroiseAnt ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['POSTERIOR_CROSSBITE']=" + (ArticuleCroisePost ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['NARROW_ARCH']=" + (ArcadeEtroite ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['FLARED_TEETH']=" + (ProAlveolie ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OVERJET']=" + (Surplomb ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['UNEVEN_SMILE']=" + (SourireInesthetique ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['MISSHAPEN_TEETH']=" + (AnomalieFormeDentaire ? "true" : "false"));
                data.Append("&patientInfo.chiefConcern['OTHER']=" + (Autre ? "true" : "false"));
                data.Append("&patientInfo.otherConcern=" + AutreTxt);
                data.Append("&patientInfo.notes=" + notes);



                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);

                return UpdateCliniqueByGet(url + "?" + data.ToString(), useragent, contenttype, referer);

            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
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


        public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters, string referer)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData, referer);
        }




        private static string UpdateCliniqueByGet(string postUrl, string userAgent, string contentType, string referer)
        {

            try
            {



                Uri address = new Uri(postUrl);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Keep-Alive", "115");
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";



                CookieContainer cookies = new CookieContainer();
                cookies.Add(Anonymouscookie);

                foreach (Cookie c in Sessioncookies)
                    cookies.Add(c);

                request.CookieContainer = cookies;
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                response.Close();

                return response.StatusCode == HttpStatusCode.OK ? "" : response.StatusCode.ToString();
            }
            catch (System.UriFormatException ex)
            {
                //throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                return ex.Message;
            }
            catch (System.Exception)
            {
                return "";
            }

        }


        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData, string referer)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;
            request.Referer = referer;
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            request.Expect = "";

            CookieContainer cookies = new CookieContainer();
            cookies.Add(Anonymouscookie);

            foreach (Cookie c in Sessioncookies)
                cookies.Add(c);

            request.CookieContainer = cookies;

            
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }


        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
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


        public static string UploadProfilPicture(string IdInvisalign, string file)
        {

            if (string.IsNullOrEmpty(IdInvisalign))
                throw new System.Exception("Patient Invisalign Not Found"); ;


            try
            {


                Dictionary<string, object> postParameters = new Dictionary<string, object>();

                FileStream fs;
                FileInfo nfo;

                if (!string.IsNullOrEmpty(file))
                {
                    nfo = new FileInfo(file);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                        byte[] profiledata = new byte[fs.Length];
                        fs.Read(profiledata, 0, profiledata.Length);
                        fs.Close();

                        postParameters.Add("profilePicture", new FileParameter(profiledata, nfo.Name, "application/octet-stream"));
                    }
                }

                


                // Create request and receive response
                string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&vPI=" + IdInvisalign;
                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=" + IdInvisalign + "&currentStep=PHOTOS";
                HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, postParameters, referer);

                // Process response
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();
                return webResponse.StatusCode == HttpStatusCode.OK ? "" : "Reponse serveur : " + webResponse.StatusCode.ToString();


            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

      

        public static string UploadPhotos(string IdInvisalign,
                                            string profile,
                                            string face,
                                            string facesourire,
                                            string mand,
                                            string max,
                                            string intradroit,
                                            string intra,
                                            string intragauche)
        {

            if (string.IsNullOrEmpty(IdInvisalign))
                throw new System.Exception("Patient Invisalign Not Found"); ;


            try
            {


                Dictionary<string, object> postParameters = new Dictionary<string, object>();

                FileStream fs;
                FileInfo nfo;

                if (!string.IsNullOrEmpty(profile))
                {
                    nfo = new FileInfo(profile);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(profile, FileMode.Open, FileAccess.Read);
                        byte[] profiledata = new byte[fs.Length];
                        fs.Read(profiledata, 0, profiledata.Length);
                        fs.Close();

                        postParameters.Add("eprTf", new FileParameter(profiledata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(face))
                {
                    nfo = new FileInfo(face);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(face, FileMode.Open, FileAccess.Read);
                        byte[] facedata = new byte[fs.Length];
                        fs.Read(facedata, 0, facedata.Length);
                        fs.Close();

                        postParameters.Add("efrTf", new FileParameter(facedata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(facesourire))
                {
                    nfo = new FileInfo(facesourire);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(facesourire, FileMode.Open, FileAccess.Read);
                        byte[] facesouriredata = new byte[fs.Length];
                        fs.Read(facesouriredata, 0, facesouriredata.Length);
                        fs.Close();

                        postParameters.Add("efsTf", new FileParameter(facesouriredata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(mand))
                {
                    nfo = new FileInfo(mand);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(mand, FileMode.Open, FileAccess.Read);
                        byte[] manddata = new byte[fs.Length];
                        fs.Read(manddata, 0, manddata.Length);
                        fs.Close();

                        postParameters.Add("ioManTf", new FileParameter(manddata, nfo.Name, "application/octet-stream"));
                    }
                }
                if (!string.IsNullOrEmpty(max))
                {
                    nfo = new FileInfo(max);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(max, FileMode.Open, FileAccess.Read);
                        byte[] maxdata = new byte[fs.Length];
                        fs.Read(maxdata, 0, maxdata.Length);
                        fs.Close();

                        postParameters.Add("ioMaxTf", new FileParameter(maxdata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(intradroit))
                {
                    nfo = new FileInfo(intradroit);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(intradroit, FileMode.Open, FileAccess.Read);
                        byte[] intradroitdata = new byte[fs.Length];
                        fs.Read(intradroitdata, 0, intradroitdata.Length);
                        fs.Close();

                        postParameters.Add("ibrTf", new FileParameter(intradroitdata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(intra))
                {
                    nfo = new FileInfo(intra);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(intra, FileMode.Open, FileAccess.Read);
                        byte[] intradata = new byte[fs.Length];
                        fs.Read(intradata, 0, intradata.Length);
                        fs.Close();

                        postParameters.Add("iaTf", new FileParameter(intradata, nfo.Name, "application/octet-stream"));
                    }
                }

                if (!string.IsNullOrEmpty(intragauche))
                {
                    nfo = new FileInfo(intragauche);
                    if (nfo.Exists)
                    {
                        fs = new FileStream(intragauche, FileMode.Open, FileAccess.Read);
                        byte[] intragauchedata = new byte[fs.Length];
                        fs.Read(intragauchedata, 0, intragauchedata.Length);
                        fs.Close();

                        postParameters.Add("iblTf", new FileParameter(intragauchedata, nfo.Name, "application/octet-stream"));
                    }
                }


                // Create request and receive response
                string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&vPI=" + IdInvisalign;
                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=" + IdInvisalign + "&currentStep=PHOTOS";
                HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, postParameters, referer);

                // Process response
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();
                return webResponse.StatusCode == HttpStatusCode.OK ? "" : "Reponse serveur : " + webResponse.StatusCode.ToString();


            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }


        public static string UploadRadios(string IdInvisalign,
                                     string fullmouth,
                                     string panoramic)
        {

            if (string.IsNullOrEmpty(IdInvisalign))
                throw new System.Exception("Patient Invisalign Not Found"); ;


            try
            {


                Dictionary<string, object> postParameters = new Dictionary<string, object>();



                FileInfo nfo;
                FileStream fs;

                if (!string.IsNullOrEmpty(fullmouth))
                {
                    nfo = new FileInfo(fullmouth);
                    if (nfo.Exists)
                    {

                        fs = new FileStream(fullmouth, FileMode.Open, FileAccess.Read);
                        byte[] fullmouthdata = new byte[fs.Length];
                        fs.Read(fullmouthdata, 0, fullmouthdata.Length);
                        fs.Close();

                        postParameters.Add("panoramicxTf", new FileParameter(fullmouthdata, nfo.Name, "application/octet-stream"));
                    }

                }

                if (!string.IsNullOrEmpty(panoramic))
                {
                    nfo = new FileInfo(panoramic);
                    if (nfo.Exists)
                    {

                        fs = new FileStream(panoramic, FileMode.Open, FileAccess.Read);
                        byte[] panoramicdata = new byte[fs.Length];
                        fs.Read(panoramicdata, 0, panoramicdata.Length);
                        fs.Close();


                        postParameters.Add("fullmouthxTf", new FileParameter(panoramicdata, nfo.Name, "application/octet-stream"));
                    }
                }



                // Create request and receive response
                string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&vPI=" + IdInvisalign;
                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?vPI=" + IdInvisalign + "&currentStep=PHOTOS";
                HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, postParameters, referer);

                // Process response
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();
                return webResponse.StatusCode == HttpStatusCode.OK ? "" : "Reponse serveur : " + webResponse.StatusCode.ToString();


            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }



        public static int DownloadFile(String remoteFilename,
                               String localFilename)
        {

            //pssword : ledled71
            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                // Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                request.Credentials = new NetworkCredential("pbergeyr", "ledled71");

                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Close the response and streams objects here
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;
        }


        private static bool InProgress = false;

     


        private static PatientInvisalign pat = new PatientInvisalign();

      
        public static bool CookiesExpired()
        {
            bool isAllexpired = false;
            foreach (Cookie c in Sessioncookies)
                if (c.Expired)
                    isAllexpired = true;

            return isAllexpired;
        }


        public static String RemoveAccent(String stArg)
        {
            if (stArg == null) return null;
            try
            {
                string stFormD = stArg.ToString().Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();

                for (int ich = 0; ich < stFormD.Length; ich++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(stFormD[ich]);
                    }
                }

                return (sb.ToString());
            }
            catch (System.Exception) { return stArg; }
        }





        public static HttpWebResponse PrescriptionSelectProduct(string IdInvisalign, InvisalignPrescriptionFullObj.InvisalignType typePrescription)
        {


            if (string.IsNullOrEmpty(IdInvisalign))
                return null;



            try
            {

                string tpe = "FULL_INT_2_0";
                switch (typePrescription)
                {
                    case InvisalignPrescriptionFullObj.InvisalignType.Full: tpe = "FULL_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.I7: tpe = "I_SEVEN_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Teen: tpe = "TEEN_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Lite: tpe = "LITE_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Vivera: tpe = "RETAINER_MULTIPACK_INT_2_0"; break;
                        
                }

                string postUrl = "https://vip.invisalign.com/v3/auth/rx/selectProduct.action?action=START&vPI="+IdInvisalign+"&vOI=&formId=0&change=false&studyId=&formType=" + tpe;

                Uri address = new Uri(postUrl);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Keep-Alive", "115");
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";



                CookieContainer cookies = new CookieContainer();
                cookies.Add(Anonymouscookie);

                foreach (Cookie c in Sessioncookies)
                    cookies.Add(c);

                request.CookieContainer = cookies;
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                response.Close();

                return response;
            }
            catch (System.UriFormatException ex)
            {
                //throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
                        

        }





        public static void PrescriptionFull(HttpWebResponse response,InvisalignPrescriptionFullObj invi)
        {
            string[] queries = response.ResponseUri.Query.Substring(1, response.ResponseUri.Query.Length-1).Split(',');
            Dictionary<string, string> dicoquery = new Dictionary<string, string>();
            foreach (string s in queries)
            {
                string[] ss = s.Split('=');
                dicoquery.Add(ss[0],ss[1]);
            }
            string formId = dicoquery["formId"];


            Etape1Full(invi, formId);
            Etape2Full(invi, formId);
            Etape3Full(invi, formId);
            Etape4Full(invi, formId);
            Etape5Full(invi, formId);
            Etape6Full(invi, formId);
            Etape7Full(invi, formId);
            Etape8Full(invi, formId);
            Etape9Full(invi, formId);
        }



        public static WebResponse Etape1Full(InvisalignPrescriptionFullObj invi, string formId)
        {
                      
            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId;
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";


               

                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=1");
                data.Append("&pref=");

                if (invi.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.both)
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.treatArches=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.both.both_true");
                if (invi.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.upperOnly)
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.treatArches=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.upperArch.upperArch_true");
                if (invi.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.lowerOnly)
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.treatArches=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.lowerArch.lowerArch_true");

                if (invi.Etape1.upperOnlyDiagnosticSetup)
                {
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.upperArch.diagnosticSetup=true");
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.upperArch.diagnosticSetup=true");
                }


                if (invi.Etape1.upperOnlyDiagnosticSetup)
                {
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.lowerArch.diagnosticSetup=true");
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.lowerArch.diagnosticSetup=true");
                }


                if (invi.Etape1.FullArcade)
                {
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated=xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.full_true");
                }
                else
                {
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated=xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.anterior_true");

                }
                
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.both.both=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.both.both_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.upperArch.upperArch=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.upperArch.upperArch_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.lowerArch.lowerArch=xmlFormObj.prescriptionQuestions.primaryProduct.treatedArches.lowerArch.lowerArch_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.full=xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.full_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.anterior=xmlFormObj.prescriptionQuestions.primaryProduct.teethToBeTreated.anterior_true_false");

                


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        public static WebResponse Etape2Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId +"&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=2");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.none=xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.theseTeeth.theseTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.theseTeeth.theseTeeth_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.anyTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.anyTeeth_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.doNotPutTheseTeeth.doNotPutTheseTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.doNotPutTheseTeeth.doNotPutTheseTeeth_true_false");

                if (invi.Etape2.DoNotMoveAnyTeeth)
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.none_true");
                else
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.theseTeeth.theseTeeth_true");

                if (invi.Etape3.TeethPermittedForAttachements)
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments=xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.anyTeeth_true");
                else
                    data.Append("&__radio__xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments=xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.doNotPutTheseTeeth.doNotPutTheseTeeth_true");


                for (int i=0;i<invi.Etape2.DoNotMoveTheseTeeth.Length;i++)
                {
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.theseTeeth.toothArray%5B" + i.ToString() + "%5D.stringValue=true");
                        if (invi.Etape2.DoNotMoveTheseTeeth[i])
                    {
                        data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.doNotMoveTeeth.theseTeeth.toothArray%5B" + i.ToString() + "%5D.stringValue=true");
                    }
                
                }

                for (int i = 0; i < invi.Etape3.TeethPermittedForAttachement.Length; i++)
                {
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.doNotPutTheseTeeth.toothArray%5B" + i.ToString() + "%5D.stringValue=true");
                    if (invi.Etape3.TeethPermittedForAttachement[i])
                    {
                        data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.teethPermittedForAttachments.doNotPutTheseTeeth.toothArray%5B" + i.ToString() + "%5D.stringValue=true");
                    }

                }

                
                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static WebResponse Etape3Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=3");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.maintain=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.maintain_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.maintain=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.maintain_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.improveToClass1CanineOnly=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.improveToClass1CanineOnly_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.improveToClass1CanineOnly=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.improveToClass1CanineOnly_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.partialClass1UpTo4Mm=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.partialClass1UpTo4Mm_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.partialClass1UpTo4Mm=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.partialClass1UpTo4Mm_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.completeClass1CanineAndMolar=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.completeClass1CanineAndMolar_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.completeClass1CanineAndMolar=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.completeClass1CanineAndMolar_true_false");

                switch (invi.Etape4.aPRelationRight)
                {
                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain:
                        data.Append("&__radio__aPRelation.right=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.maintain_true");
                        break;
                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly:
                        data.Append("&__radio__aPRelation.right=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.improveToClass1CanineOnly_true");
                        break;

                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly:
                        data.Append("&__radio__aPRelation.right=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.partialClass1UpTo4Mm_true");
                        break;

                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar:
                        data.Append("&__radio__aPRelation.right=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.right.completeClass1CanineAndMolar_true");
                        break;
                }

                switch (invi.Etape4.aPRelationLeft)
                {
                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain:
                        data.Append("&__radio__aPRelation.left=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.maintain_true");
                        break;
                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly:
                        data.Append("&__radio__aPRelation.left=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.improveToClass1CanineOnly_true");
                        break;

                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly:
                        data.Append("&__radio__aPRelation.left=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.partialClass1UpTo4Mm_true");
                        break;

                    case InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar:
                        data.Append("&__radio__aPRelation.left=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipType.left.completeClass1CanineAndMolar_true");
                        break;
                }

                if (invi.Etape4.options == InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement)
                    data.Append("&__radio__APRelation_how=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.toothMovementOptions_true");
                if (invi.Etape4.options == InvisalignPrescriptionFullObj.aPRelation.Options.Surgical)
                    data.Append("&__radio__APRelation_how=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.orthognathicSurgicalSetup_true");


                if (invi.Etape4.PosteriorIPR)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.posteriorIPR=true");
                if (invi.Etape4.classIIOrIIICorrectionSimulation)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.classIIOrIIICorrectionSimulation=true");
                if (invi.Etape4.distalization)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.distalization=true");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.yes=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.yes_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.no=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.no_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.yes=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.yes_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.no=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.no_true_false");

                if (invi.Etape4.classIIOrIIICorrectionSimulationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes)
                    data.Append("&__radio__classIIOrIIICorrectionSimulation_alignerCuts=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.yes_true");
                if (invi.Etape4.classIIOrIIICorrectionSimulationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No)
                    data.Append("&__radio__classIIOrIIICorrectionSimulation_alignerCuts=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.classIIOrIIICorrectionSimulation.precisionCuts.no_true");

                if (invi.Etape4.distalizationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes)
                    data.Append("&__radio__distalization_alignerCuts=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.yes_true");
                if (invi.Etape4.distalizationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No)
                    data.Append("&__radio__distalization_alignerCuts=xmlFormObj.prescriptionQuestions.primaryProduct.aPRelation.aPRelationshipOptions.toothMovementOptions.distalization.precisionCuts.no_true");
                

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static WebResponse Etape4Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=4");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.overJet.showResultantOverjetAfterAlignment=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.showResultantOverjetAfterAlignment_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.overJet.maintainInitialOverjet=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.maintainInitialOverjet_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.overJet.improveResultingOverjetWithIPR=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.improveResultingOverjetWithIPR_true_false");


                switch (invi.Etape5.options)
                {
                    case InvisalignPrescriptionFullObj.overJet.overJetOptions.showResultantOverjetAfterAlignment:
                        data.Append("&__radio__overjet=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.showResultantOverjetAfterAlignment_true");
                        break;
                    case InvisalignPrescriptionFullObj.overJet.overJetOptions.maintainInitialOverjet:
                        data.Append("&__radio__overjet=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.maintainInitialOverjet_true");
                        break;
                    case InvisalignPrescriptionFullObj.overJet.overJetOptions.improveResultingOverjetWithIPR:
                        data.Append("&__radio__overjet=xmlFormObj.prescriptionQuestions.primaryProduct.overJet.improveResultingOverjetWithIPR_true");
                        break;
                }

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static WebResponse Etape5Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=5");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainResultantOverbiteAfterAlignment=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainResultantOverbiteAfterAlignment_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainInitialOverbite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainInitialOverbite_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.correctOpenBiteOptions=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.correctOpenBiteOptions_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctDeepBite.correctDeepBite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctDeepBite.correctDeepBite_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorOnly.extrudeAnteriorOnly=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorOnly.extrudeAnteriorOnly_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorTeethAndIntrudePosterior.extrudeAnteriorTeethAndIntrudePosterior=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorTeethAndIntrudePosterior.extrudeAnteriorTeethAndIntrudePosterior_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.other=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.other_true_false");

                switch (invi.Etape6.options)
                {
                    case InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainResultant:
                        data.Append("&__radio__overbite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainResultantOverbiteAfterAlignment_true");
                        break;
                    case InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainInitial:
                        data.Append("&__radio__overbite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.maintainInitialOverbite_true");
                        break;
                    case InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite:
                        data.Append("&__radio__overbite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.correctOpenBiteOptions_true");
                        break;
                    case InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctDeepBite:
                        data.Append("&__radio__overbite=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctDeepBite.correctDeepBite_true");
                        break;
                }

                switch (invi.Etape6.OpenbiteOption)
                {
                    case InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorOnly:
                        data.Append("&__radio__correctOpenBiteOptions=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorOnly.extrudeAnteriorOnly_true");
                        break;
                    case InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorTeethAndIntrudePosterior:
                        data.Append("&__radio__correctOpenBiteOptions=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorTeethAndIntrudePosterior.extrudeAnteriorTeethAndIntrudePosterior_true");
                        break;
                    case InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.other:
                        data.Append("&__radio__correctOpenBiteOptions=xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.other_true");
                        break;
                }


                if (invi.Etape6.extrudeAnteriorOnlyUpperArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorOnly.upperArch=true");
                if (invi.Etape6.extrudeAnteriorOnlyLowerArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorOnly.lowerArch=true");                
                if (invi.Etape6.extrudeAnteriorTeethAndIntrudePosteriorUpperArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorTeethAndIntrudePosterior.upperArch=true");
                if (invi.Etape6.extrudeAnteriorTeethAndIntrudePosteriorLowerArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctOpenBiteOptions.extrudeAnteriorTeethAndIntrudePosterior.lowerArch=true");
                if (invi.Etape6.correctDeepBiteUpperArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctDeepBite.upperArch=true");
                if (invi.Etape6.correctDeepBiteLowerArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.OverBite.correctDeepBite.lowerArch=true");

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        public static WebResponse Etape6Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=6");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.showResultantMidlineAfterAlignment=xmlFormObj.prescriptionQuestions.primaryProduct.midline.showResultantMidlineAfterAlignment_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.maintainInitialMidline=xmlFormObj.prescriptionQuestions.primaryProduct.midline.maintainInitialMidline_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.improveMidlineWithIPR=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.improveMidlineWithIPR_true_false");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsRight=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsRight_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsLeft=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsLeft_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsRight=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsRight_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsLeft=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsLeft_true_false");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.maintain=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.maintain_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.correct=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.correct_true_false");

                if (invi.Etape7.improveMidlineWithIPRUpperArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.upperArch=true");
                if (invi.Etape7.improveMidlineWithIPRLowerArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.lowerArch=true");


                switch (invi.Etape7.options)
                {
                    case InvisalignPrescriptionFullObj.midline.midlineOptions.showResultantMidlineAfterAlignment:
                        data.Append("&__radio__midline=xmlFormObj.prescriptionQuestions.primaryProduct.midline.showResultantMidlineAfterAlignment_true");
                        break;
                    case InvisalignPrescriptionFullObj.midline.midlineOptions.maintainInitialMidline:
                        data.Append("&__radio__midline=xmlFormObj.prescriptionQuestions.primaryProduct.midline.maintainInitialMidline_true");
                        break;
                    case InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR:
                        data.Append("&__radio__midline=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.improveMidlineWithIPR_true");
                        break;

                }


                if (invi.Etape7.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsLeft_true");
                if (invi.Etape7.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsRight_true");

                if (invi.Etape7.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsLeft_true");
                if (invi.Etape7.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsRight_true");

                if (invi.Etape8.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.maintain_true");
                if (invi.Etape8.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.correct_true");
                

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static WebResponse Etape7Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=7");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.closeAllSpaces=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.closeAllSpaces_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.leaveSpaces=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.leaveSpaces_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.primarily=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.primarily_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.ifNeeded=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.ifNeeded_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.none=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.none_true_false");

                for (int i = 0; i < invi.Etape9.SpaceArray.Length; i++)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.spaceArray%5B" + i.ToString() + "%5D.amount.stringValue=" + invi.Etape9.SpaceArray[i].ToString("0.0"));

                if (invi.Etape9.SpaceCloseAll)
                    data.Append("&__radio__spaces_close=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.closeAllSpaces_true");
                else
                    data.Append("&__radio__spaces_close=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.leaveSpaces_true");


                switch (invi.Etape9.upperExpansion)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__upperExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__upperExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__upperExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.expand.none_true");
                        break;
                }

                switch (invi.Etape9.upperVestibuloVersion)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__upperProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__upperProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__upperProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.procline.none_true");
                        break;
                }

                switch (invi.Etape9.upperRIPAnterieur)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__upperAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__upperAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__upperAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.anterior.none_true");
                        break;
                }

                switch (invi.Etape9.upperRIPPosterieurDroit)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__upperPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__upperPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__upperPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorRight.none_true");
                        break;
                }

                switch (invi.Etape9.upperRIPPosterieurGauche)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__upperPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__upperPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__upperPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.upperArch.iPR.posteriorLeft.none_true");
                        break;
                }

                /////////////////////////////////////////////


                switch (invi.Etape9.lowerExpansion)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__lowerExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__lowerExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__lowerExpand=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.expand.none_true");
                        break;
                }

                switch (invi.Etape9.lowerVestibuloVersion)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__lowerProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__lowerProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__lowerProcline=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.procline.none_true");
                        break;
                }

                switch (invi.Etape9.lowerRIPAnterieur)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__lowerAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__lowerAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__lowerAnterior=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.anterior.none_true");
                        break;
                }

                switch (invi.Etape9.lowerRIPPosterieurDroit)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__lowerPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__lowerPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__lowerPosteriorRight=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorRight.none_true");
                        break;
                }

                switch (invi.Etape9.lowerRIPPosterieurGauche)
                {
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily:
                        data.Append("&__radio__lowerPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.primarily_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded:
                        data.Append("&__radio__lowerPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.ifNeeded_true");
                        break;
                    case InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None:
                        data.Append("&__radio__lowerPosteriorLeft=xmlFormObj.prescriptionQuestions.primaryProduct.crowding.lowerArch.iPR.posteriorLeft.none_true");
                        break;
                }


                

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        public static WebResponse Etape8Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=8");
                data.Append("&pref=");

                
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.none=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.theseTeeth=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.theseTeeth_true_false");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B0%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B1%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B2%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B3%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B4%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B5%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B6%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B7%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B8%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B9%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B10%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B11%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B12%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B13%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B14%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B15%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B31%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B30%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B29%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B28%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B27%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B26%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B25%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B24%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B23%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B22%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B21%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B20%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B19%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B18%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B17%5D.stringValue=true");
                data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B16%5D.stringValue=true");

                if (!invi.Etape9.NeedExtraction)
                    data.Append("&__radio__extraction=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.none_true");
                else
                    data.Append("&__radio__extraction=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.theseTeeth_true");


                for (int i = 0; i < invi.Etape9.Extraction.Length; i++)
                    if (invi.Etape9.Extraction[i])
                        data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B"+i.ToString()+"%5D.stringValue=true");
               


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        public static WebResponse Etape9Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=9");
                data.Append("&pref=");


                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.specialInstructions.getInstructionArray(0).stringValue="+invi.Etape10_SpecialInstruction.Replace("\n","%0D%0A"));
                //qsdqsdqsd%0D%0Aqsdqsd%0D%0Aqsdqsqd
                


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);


                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

      
    }

}
