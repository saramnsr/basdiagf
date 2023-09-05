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
using SHDocVw;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace BasCommon_BL
{
    public static class Invisalign
    {
       

      

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

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
        public static Boolean CallIEInvisalign(string nomInvisalignPatient, string PrenomInvisalignPatient, string IdInvisalignPatient)
       {
          NewConnect();
            Newlogin ();

            string login = System.Configuration.ConfigurationManager.AppSettings["InvisalignLogin"];
            string password = System.Configuration.ConfigurationManager.AppSettings["InvisalignPassword"];

            login = login == null ? "pbergegh" : login;
            password = password == null ? "patrice1201" : password;
            if (!CallIEInvisalign(login, password, nomInvisalignPatient, PrenomInvisalignPatient, IdInvisalignPatient))
                return false;
            else
                return true;
        
        }


        public static Boolean CallIEInvisalign(string username, string password, string nomInvisalignPatient, string PrenomInvisalignPatient, string IdInvisalignPatient)
        {
           
            object missing = System.Reflection.Missing.Value;
            InternetExplorer ieExplorer = null;
            ASCIIEncoding Encode = new ASCIIEncoding();

            IWebBrowser2 webDocument = null;
            try
            {



                // Create an instance of IE
               ieExplorer = new InternetExplorerClass();
                webDocument = (IWebBrowser2)ieExplorer;




                string sURL;
                sURL = "https://vip.invisalign.com/";

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(sURL);

                Stream objStream;
                wrGETURL.Proxy = null;
                HttpWebResponse response = wrGETURL.GetResponse() as HttpWebResponse;

                objStream = response.GetResponseStream();
               // response.Close();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";

                string vSessionDatakey = "";
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine.Contains("<input type=\"hidden\" name=\"sessionDataKey\" value="))
                    {

                        vSessionDatakey = sLine.Substring(sLine.IndexOf("'") + 1, sLine.LastIndexOf("'") - sLine.IndexOf("'") - 1);
                        break;
                    }


                }


                response.Close();


              
                string url = " https://auth.aligntech.com/commonauth?doctorname=" + username + "&password="+ password  + "&domainName=ALIGNTECH.COM%2F&chkRemember=true&username=ALIGNTECH.COM%2Fpbergegh&sessionDataKey=" + vSessionDatakey;
            
                webDocument.Navigate(url, ref missing, ref missing, ref missing, ref missing);
                while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;



                url = "https://vip.invisalign.com/v3/auth/patient/patientFile.action?patientId=" + IdInvisalignPatient;
             
                webDocument.Navigate(url, ref missing, ref missing, ref missing, ref missing);
                while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;
                webDocument.Visible = true;
                return true;
            }
            catch (System.Exception ex)
           //catch (System.Exception)
            {
                return false;
            }
            //finally
            //{

            //    if (webDocument != null)
            //        webDocument.Visible = true;
            //}
        }


        //public static void CallIEInvisalign(string username, string password, string nomInvisalignPatient, string PrenomInvisalignPatient, string IdInvisalignPatient)
        //{
        //    Newlogin();
        //    object missing = System.Reflection.Missing.Value;
        //    InternetExplorer ieExplorer = null;
        //    ASCIIEncoding Encode = new ASCIIEncoding();

        //    IWebBrowser2 webDocument = null;
        //    try
        //    {



        //        // Create an instance of IE
        //        ieExplorer = new InternetExplorerClass();
        //        webDocument = (IWebBrowser2)ieExplorer;


        //        string url = "https://vip.invisalign.com/v3/login.action";
        //           byte[] post = Encode.GetBytes("username=" + username + "&password=" + password);
        //           string Headers = "Accept: text/html, application/xhtml+xml, */*";
        //        Headers += "\nAccept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7";
        //        Headers += "\nAccept-Language: fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3";
        //        Headers += "\nAccept-Encoding: gzip, deflate";
        //        Headers += "\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
        //        Headers += "\nContetnType: application/x-www-form-urlencoded";
        //        Headers += "\nContent-Type: application/x-www-form-urlencoded";
        //       // Headers += "\nReferer: https://auth.invisalign.com/oauth/login.action?response_type=code&client_id=6-fxQb6jrC&redirect_uri=https%3A%2F%2Fvip.invisalign.com%2Fv3%2FoauthRedirect.action&scope=sso";





        //        //CookieContainer cookies = new CookieContainer();
        //        //cookies.Add(Anonymouscookie);

        //        //foreach (Cookie c in Sessioncookies)
        //        //{

        //        //    InternetSetCookie(url, c.Name, c.Value);
        //        //}

        //       InternetSetCookie(url, Anonymouscookie.Name, Anonymouscookie.Value);


              
                    
        //        //InternetSetCookie(url, "userParams", "search-");
        //        //InternetSetCookie(url, "domain", "auth.aligntech.com");
        //        //InternetSetCookie(url, "path", "/oauth2");
        //        webDocument.Navigate(url, ref missing, ref missing, post, Headers  );
        //        while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;
               
        //        webDocument.Visible = true;
               
                
                






                
        //        //Headers += "\nAccept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7";
        //        //Headers += "\nAccept-Language: fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3";
        //        //Headers += "\nAccept-Encoding: gzip, deflate";
        //        //Headers += "\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
        //        //Headers += "\nContetnType: application/x-www-form-urlencoded";

                
        //        //        CookieContainer cookies = new CookieContainer();
        //        //cookies.Add(Anonymouscookie);

        //        //foreach (Cookie c in Sessioncookies)
        //        //{
        //        //    //cookies.Add(c);
        //        //    InternetSetCookie(url, c.Name, c.Value);
        //        //}

        //     //  InternetSetCookie(url, Anonymouscookie.Name, Anonymouscookie.Value);


        //        //foreach (CookieContainer c in cookies)
        //        //    InternetSetCookie(url, c.Name, c.Value);


        //       // url = "http://www.yahoo.fr";
        //     ////   webDocument.Navigate(url, ref missing, ref missing, ref missing, Headers);
        //     ////   while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;




        //     ////   Headers += "\nContent-Type: application/x-www-form-urlencoded";
        //     ////   Headers += "\nReferer: https://auth.invisalign.com/oauth/login.action?response_type=code&client_id=6-fxQb6jrC&redirect_uri=https%3A%2F%2Fvip.invisalign.com%2Fv3%2FoauthRedirect.action&scope=sso";
        //     ////   Headers += "\nContent-Type: application/x-www-form-urlencoded";

        //     ////   byte[] post = Encode.GetBytes("username=" + username + "&password=" + password);
        //     ////   //+ "&search=" + nomInvisalignPatient + "+" + PrenomInvisalignPatient);


        //     ////   //url = "https://auth.invisalign.com/oauth/login.action?response_type=code&client_id=6-fxQb6jrC&redirect_uri=https%3A%2F%2Fvip.invisalign.com%2Fv3%2FoauthRedirect.action&scope=sso";


        //     ////   foreach (Cookie c in Sessioncookies)
        //     ////   {
        //     ////       //cookies.Add(c);
        //     ////       InternetSetCookie(url, c.Name, c.Value);
        //     ////   }

        //     //////   InternetSetCookie(url, Anonymouscookie.Name, Anonymouscookie.Value);
        //     ////   webDocument.Navigate(url, ref missing, ref missing, post, Headers);
              
        //     ////   while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;

              

        //     ////   url = "https://vip.invisalign.com/v3/auth/patient/patientFile.action?patientId=" + IdInvisalignPatient;
        //     ////   foreach (Cookie c in Sessioncookies)
        //     ////   {
        //     ////       //cookies.Add(c);
        //     ////       InternetSetCookie(url, c.Name, c.Value);
        //     ////   }
        //     ////   webDocument.Navigate(url, ref missing, ref missing, ref missing, ref missing);
        //     ////   while (webDocument.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) ;

        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //    finally
        //    {

        //        if (webDocument != null)
        //            webDocument.Visible = true;
        //    }
        //}

      private static  CookieContainer cookies = new CookieContainer();
        public static string NewConnect()
        {

            try
            {
                cookies = new CookieContainer();
                Uri address = new Uri("https://vip.invisalign.com/");

                HttpWebRequest wrGETURL;
                wrGETURL = WebRequest.Create(address) as HttpWebRequest;
                wrGETURL.CookieContainer = cookies;

                Stream objStream;
                wrGETURL.Proxy = null;
                HttpWebResponse response = wrGETURL.GetResponse() as HttpWebResponse;

                objStream = response.GetResponseStream();

                // response.Close();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";

                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine.Contains("<input type=\"hidden\" name=\"sessionDataKey\" value="))
                    {

                        _vSessionDatakey = sLine.Substring(sLine.IndexOf("'") + 1, sLine.LastIndexOf("'") - sLine.IndexOf("'") - 1);
                        break;
                    }


                }
                response.Close();

                //string CookieVal = response.Headers["Set-Cookie"];

                //string ookiereturned = response.Headers["Cookie"];




                //string jsessionID = CookieVal.Split(';')[2];
                //string jsessionID2 = jsessionID.Split(',')[1];
                //Cookie cookiereturned = new Cookie();




                ////string[] cookieparams = CookieVal.Split(';');


                //cookiereturned.Name = jsessionID2.Split('=')[0].Trim();
                //cookiereturned.Value = jsessionID2.Split('=')[1];
                //cookiereturned.Domain = "vip.invisalign.com";

                ////   if (cookieparams[1].Split('=')[0].Trim().ToLower() == "path") cookiereturned.Path = cookieparams[1].Split('=')[1];


                //Anonymouscookie = cookiereturned;

                return "";
            }
            catch (System.UriFormatException ex)
            {
                
                return ex.Message;
                // throw new System.Exception("L'adresse Invisalign n'est pas correct !", ex);
            }




            //try
            //{
            //    Uri address = new Uri("https://vip.invisalign.com/v3/login.action");
            //    HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            //    request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["TIMEOUT_REQUEST"]);
            //    request.Method = "GET";


            //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            //    request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            //    request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
            //    request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

            //    // Get response
            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;


            //    response.Close();

            //    string CookieVal = response.Headers["Set-Cookie"];


            //    Cookie cookiereturned = (from s in CookieVal.Split(';')
            //                             where s.Split('=')[0].ToUpper() == " SECURE,JSESSIONID"
            //                             select new Cookie()
            //                             {
            //                                 Name = "JSESSIONID",
            //                                 Value = s.Split('=')[1],
            //                                 Domain = "vip.invisalign.com"
            //                             }).SingleOrDefault();




            //    Anonymouscookie = cookiereturned;





            //    return "";
            //}
            //catch (System.UriFormatException ex)
            //{
            //    return ex.Message;
            //    // throw new System.Exception("L'adresse Invisalign n'est pas correct !", ex);
            //}
        }



        //public static string NewConnect()
        //{
        //    try
        //    {
        //        Uri address = new Uri("https://vip.invisalign.com/v3/login.action");
        //        HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
        //        request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["TIMEOUT_REQUEST"]);
        //        request.Method = "GET";


        //        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        //        request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
        //        request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
        //        request.Headers.Add("Accept-Encoding", "gzip, deflate");
        //        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

        //        // Get response
        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;


        //        response.Close();

        //        string CookieVal = response.Headers["Set-Cookie"];


        //        Cookie cookiereturned = (from s in CookieVal.Split(';')
        //                                 where s.Split('=')[0].ToUpper() == " SECURE,JSESSIONID"
        //                                 select new Cookie()
        //                                 {
        //                                     Name = "JSESSIONID",
        //                                     Value = s.Split('=')[1],
        //                                     Domain = "vip.invisalign.com"
        //                                 }).SingleOrDefault();




        //        Anonymouscookie = cookiereturned;

        //        return "";
        //    }
        //    catch (System.UriFormatException ex)
        //    {
        //        return ex.Message;
        //        // throw new System.Exception("L'adresse Invisalign n'est pas correct !", ex);
        //    }
        //}





        public static string InsertPatient(basePatient pat)
        {

            try
            {

                DateTime DateNaiss = pat.DateNaissance;

                string lastname = pat.Nom;
                string firstname = pat.Prenom;
                string middlenamename = "";
                string gender = pat.Genre==basePatient.Sexe.Feminin ? "FEMALE" : "MALE";

               // string IdAdresse =  "390810";
				string IdAdresse = "389053";
              //  Uri address = new Uri("https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=NEXT&currentStep=INFO&patientId=0&patientInfo.pID=0&patientInfo.patientId=0&showBillingAddresses=false&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&patientInfo.sAddressId=" + IdAdresse);
Uri address = new Uri("https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=NEXT&currentStep=INFO&patientId=0&patientInfo.pid=0&patientInfo.id=&patientInfo.status=REGULAR&patientWorkflow=&patient.workflow=INVISALIGN&showBillingAddresses=true&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&isPostalCode=false&isContactInfoEnabled=false&patientInfo.shippingAddressId=" + IdAdresse + "&patientInfo.billingAddressId=" + IdAdresse + "&patientInfo.soldToAddressId=" + IdAdresse);               
			   HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Headers.Add("Keep-Alive", "115");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";



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

                //foreach (string s in ss)
                //{
                //    if (s.Split('=')[0] == "vPI")
                //        return s.Split('=')[1];
                //}
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

                //string IdAdresse = "390810";
                string IdAdresse = "389053";


                string url2 = " https://vip.invisalign.com/v3/auth/patient/editPatient.action?" +"action=NEXT&currentStep=INFO&patientId=0&patientInfo.pid=0&patientInfo.id=&patientInfo.status=REGULAR&patientWorkflow=&patient.workflow=INVISALIGN&showBillingAddresses=true&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&isPostalCode=false&isContactInfoEnabled=false&patientInfo.shippingAddressId=" + IdAdresse + "&patientInfo.billingAddressId=" + IdAdresse + "&patientInfo.soldToAddressId=" + IdAdresse;


                Uri address = new Uri(url2);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
           
                request.AllowAutoRedirect = true;
                request.CookieContainer = cookies;
                HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;





              //     string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
              //  string contenttype = "application/x-www-form-urlencoded";
              //  string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=0&currentStep=INFO";
              //  string url = "https://vip.invisalign.com/v3/auth/patient/editPatient.action";


              //  StringBuilder data = new StringBuilder();
              //  //action=NEXT&currentStep=NOTES&vPI=2973509&patientInfo.pID=0&patientInfo.vPI=2973509&

              ////  data.Append("action=NEXT&currentStep=INFO&vPI=0&patientInfo.pID=0&patientInfo.vPI=0&showBillingAddresses=false&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&patientInfo.sAddressId=" + IdAdresse);
              //   data.Append("action=NEXT&currentStep=INFO&patientId=0&patientInfo.pid=0&patientInfo.id=&patientInfo.status=REGULAR&patientWorkflow=&patient.workflow=INVISALIGN&showBillingAddresses=true&patientInfo.lastName=" + lastname + "&patientInfo.firstName=" + firstname + "&patientInfo.middleName=" + middlenamename + "&patientInfo.gender=" + gender + "&dobDay=" + DateNaiss.ToString("dd") + "&dobMonth=" + DateNaiss.ToString("MM") + "&dobYear=" + DateNaiss.ToString("yyyy") + "&isPostalCode=false&isContactInfoEnabled=false&patientInfo.shippingAddressId=" + IdAdresse + "&patientInfo.billingAddressId=" + IdAdresse + "&patientInfo.soldToAddressId=" + IdAdresse);
            

              //  string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
              //  // Create a byte array of the data we want to send
              //  byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);

              //  HttpWebResponse response = PostForm(url,useragent,contenttype,byteData,referer);
                

                string[] ss = response2.ResponseUri.Query.Substring( 1).Split('&');


                response2.Close();

                //foreach (string s in ss)
                //{
                //    if (s.Split('=')[0] == "vPI")
                //        return s.Split('=')[1];
                //}
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
            return Newlogin(null);
        }
        private static string _vSessionDatakey = "";
        public static string Newlogin(InvisalignAccount account)
        {

            if (account==null)
            {
                account = new BasCommon_BO.InvisalignAccount();
                account.login = System.Configuration.ConfigurationManager.AppSettings["InvisalignLogin"];
                account.password = System.Configuration.ConfigurationManager.AppSettings["InvisalignPassword"];
            }


            account.login = account.login == null ? "pbergeyr" : account.login;
            account.password = account.password == null ? "patrice1201" : account.password;

            //string sURL;
            //sURL = "https://vip.invisalign.com/";

            //CookieContainer cookies = new CookieContainer();

            //HttpWebRequest wrGETURL;
            //wrGETURL = WebRequest.Create(sURL) as HttpWebRequest;
            //wrGETURL.CookieContainer = cookies;

            //Stream objStream;
            //wrGETURL.Proxy = null;
            //HttpWebResponse response1 = wrGETURL.GetResponse() as HttpWebResponse;

            //objStream = response1.GetResponseStream();
            //// response.Close();

            //StreamReader objReader = new StreamReader(objStream);

            //string sLine = "";

            //string vSessionDatakey = "";
            //int i = 0;

            //while (sLine != null)
            //{
            //    i++;
            //    sLine = objReader.ReadLine();
            //    if (sLine.Contains("<input type=\"hidden\" name=\"sessionDataKey\" value="))
            //    {

            //        _vSessionDatakey = sLine.Substring(sLine.IndexOf("'") + 1, sLine.LastIndexOf("'") - sLine.IndexOf("'") - 1);
            //        break;
            //    }


            //}

            try
            {
                string url = " https://auth.aligntech.com/commonauth?doctorname=" + account.login + "&password=" + account.password + "&domainName=ALIGNTECH.COM%2F&chkRemember=true&username=ALIGNTECH.COM%2Fpbergegh&sessionDataKey=" + _vSessionDatakey;


                Uri address = new Uri(url);
               HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
              //  request.Method = "POST";

              // request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
              //  request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
              //  request.Headers.Add("Accept-Encoding", "gzip, deflate");
              //  request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
              //  request.Headers.Add("Keep-Alive", "115");
              //  request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

              //  request.Referer = "https://auth.aligntech.com/authenticationendpoint/login.do?client_id=pwBaflniIjfavIjewCncE0sj2VIa&commonAuthCallerPath=%2Foauth2%2Fauthorize&forceAuth=false&loginPage=new_doctor_login.jsp&passiveAuth=false&redirect_uri=https%3A%2F%2Fvip.invisalign.com%2Fv3%2FoauthRedirect.action&response_type=code&scope=sso+openid+clincheck&state=5LEJX4&tenantDomain=carbon.super&sessionDataKey=b44714e1-8876-4f77-baed-c327adfb9704&relyingParty=pwBaflniIjfavIjewCncE0sj2VIa&type=oidc&sp=ids_user_ids_PRODUCTION&isSaaSApp=false&authenticators=BasicAuthenticator:LOCAL";


              //CookieContainer cookies = new CookieContainer();
              //cookies.Add(Anonymouscookie);

              //request.CookieContainer = cookies;
              //request.ContentType = "application/x-www-form-urlencoded";

              //StringBuilder data = new StringBuilder();
              //data.Append("doctorname=" + account.login);
              //data.Append("&__checkbox_rememberUsername=" + "true");
              //data.Append("&password=aa" + account.password);
              //data.Append("&search=" + "");
              //data.Append("&sessionDataKey=b44714e1-8876-4f77-baed-c327adfb9704");
              //data.Append("&domainName= " + "ALIGNTECH.COM%2F");
              //data.Append("&username=" + "ALIGNTECH.COM%2Fpbergegh");

              ////   data.Append("&__checkbox_rememberUserName=true");




              //// Create a byte array of the data we want to send
              //byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
              //// Set the content length in the request headers
              //request.ContentLength = byteData.Length;
              //// Write data
              //using (Stream postStream = request.GetRequestStream())
              //{
              //    postStream.Write(byteData, 0, byteData.Length);
              //}
                request.AllowAutoRedirect = false;
                request.CookieContainer = cookies;
               HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                string CookieVal = response.Headers["Set-Cookie"];

                response.Close();

                if (CookieVal == null) return "Impossible de récupérer les cokkies d'authentification"; 

                Sessioncookies.Clear();
                foreach (Cookie coockie in response.Cookies)
                {
                    Cookie Sessioncookie = new Cookie();
                    Sessioncookie.Name = coockie.Name;
                    Sessioncookie.Value = coockie.Value;
                    Sessioncookie.Domain = coockie.Domain;
                    Sessioncookies.Add(Sessioncookie);
                }
                //foreach (string s in CookieVal.Split(','))
                //{


                //    Cookie Sessioncookie = new Cookie();

                //    string[] cookieparams = s.Split(';');


                //    Sessioncookie.Name = cookieparams[0].Split('=')[0];
                //    Sessioncookie.Value = cookieparams[0].Split('=')[1];

                //    Sessioncookie.Domain = "vip.invisalign.com";

                //    //if (cookieparams[1].Split('=')[0].Trim().ToLower() == "path") Sessioncookie.Path = cookieparams[1].Split('=')[1];

                //    Sessioncookies.Add(Sessioncookie);
                //}
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



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=" + IdInvisalign + "&currentStep=NOTES";
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
                data.Append("&patientId=" + IdInvisalign);
                data.Append("&patientInfo.pID=0");                
                data.Append("&patientInfo.id=" + IdInvisalign);
                data.Append("&patientWorkflow=");
                 data.Append("&patient.workflow=INVISALIGN");
                 data.Append("&caseAssessmentAvailableAvailable=");
                data.Append("&patient.flow=PRESCRIPTIVE");
                data.Append("&flow=");


                        data.Append("&patientInfo.chiefConcern['CROWDING']=" + (Encombrement ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['CROWDING']=" + (Encombrement ? "true" : "false"));

                        data.Append("&patientInfo.chiefConcern['SPACING']=" + (Espacement ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['SPACING']=" + (Espacement ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['CLASS_1']=" + (Class2Div1 ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['CLASS_1']=" + (Class2Div1 ? "true" : "false"));

                        data.Append("&patientInfo.chiefConcern['CLASS_2']=" + (Class2Div2 ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['CLASS_2']=" + (Class2Div2 ? "true" : "false"));

                        data.Append("&patientInfo.chiefConcern['CLASS_3']=" + (Class3 ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['CLASS_3']=" + (Class3 ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['OPEN_BITE']=" + (Beance ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['OPEN_BITE']=" + (Beance ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['ANTERIOR_CROSSBITE']=" + (ArticuleCroiseAnt ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['ANTERIOR_CROSSBITE']=" + (ArticuleCroiseAnt ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['POSTERIOR_CROSSBITE']=" + (ArticuleCroisePost ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['POSTERIOR_CROSSBITE']=" + (ArticuleCroisePost ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['DEEP_BITE']=" + (Supraclusion ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['DEEP_BITE']=" + (Supraclusion ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['NARROW_ARCH']=" + (ArcadeEtroite ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['NARROW_ARCH']=" + (ArcadeEtroite ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['FLARED_TEETH']=" + (ProAlveolie ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['FLARED_TEETH']=" + (ProAlveolie ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['OVERJET']=" + (Surplomb ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['OVERJET']=" + (Surplomb ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['UNEVEN_SMILE']=" + (SourireInesthetique ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['UNEVEN_SMILE']=" + (SourireInesthetique ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['MISSHAPEN_TEETH']=" + (AnomalieFormeDentaire ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['MISSHAPEN_TEETH']=" + (AnomalieFormeDentaire ? "true" : "false"));
                        data.Append("&patientInfo.chiefConcern['OTHER']=" + (Autre ? "true" : "false"));
                        data.Append("&__checkbox_patientInfo.chiefConcern['OTHER']=" + (Autre ? "true" : "false"));
                        data.Append("&patientInfo.otherConcern=" + AutreTxt);
                        data.Append("&patientInfo.notes=" + notes);

            

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
             string str =   HttpUtility.UrlPathEncode(data.ToString());
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



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=" + IdInvisalign + "&currentStep=NOTES";
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
                data.Append("&patientId=" + IdInvisalign);
                data.Append("&patientInfo.pID=0");
                data.Append("&patientInfo.patientId=" + IdInvisalign);

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
                //request.Method = "GET";

                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                //request.Headers.Add("Keep-Alive", "115");
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";



                //CookieContainer cookies = new CookieContainer();
                //cookies.Add(Anonymouscookie);

                //foreach (Cookie c in Sessioncookies)
                //    cookies.Add(c);

                request.CookieContainer = cookies;
              //  request.ContentType = "application/x-www-form-urlencoded";
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
            catch (System.Exception e)
            {
                return "";
            }

        }

        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData, string referer)
        {
            return PostForm(postUrl, userAgent, contentType, formData, referer, true);
        }


       private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData, string referer, bool closeResponse)
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
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;
            request.Referer = referer;
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            request.Expect = "";
           // CookieContainer cookies = new CookieContainer();
            request.Timeout = 5000000;
         //   cookies.Add(Anonymouscookie);

            //foreach (Cookie c in Sessioncookies)
            //    cookies.Add(c);

            request.AllowAutoRedirect = true;
            //request.AllowWriteStreamBuffering = false;
            request.CookieContainer = cookies;
            //Stream stream2 = request.GetRequestStream();
            //stream2.WriteTimeout = 50000000;
          
            //stream2.Write(formData, 0, formData.Length);
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
                requestStream.Dispose();
            }

            HttpWebResponse rep = request.GetResponse() as HttpWebResponse;

            if (closeResponse) rep.Close();
            return rep;

            
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
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] profiledata = webClient.DownloadData(file);
                            postParameters.Add("profilePicture", new FileParameter(profiledata, System.IO.Path.GetFileName(file), "application/octet-stream"));

                        }

                    }
                    catch (Exception e)
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
                }

                


                // Create request and receive response
                string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&patientId=" + IdInvisalign;
                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
                string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=" + IdInvisalign + "&currentStep=PROFILE_PICTURE&patientWorkflow=&flow=&caseAssessmentAvailable=false";
                HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, postParameters, referer);

                // Process response
               // StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
               // string fullResponse = responseReader.ReadToEnd();
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

            try
            {
           
            if (string.IsNullOrEmpty(IdInvisalign))
                throw new System.Exception("Patient Invisalign Not Found"); ;


            string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&patientId=" + IdInvisalign;
            string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=" + IdInvisalign + "&currentStep=PHOTOS";
               
            return UploadPhotosInUrl(postURL,referer,
                                        profile,
                                        face,
                                        facesourire,
                                        mand,
                                        max,
                                        intradroit,
                                        intra,
                                        intragauche);


            }
            catch (System.Exception ex)
            {
               return ex.Message ;
            }
        }


        public static string UploadPhotosInUrl(string postURL,
                                        string referent,
                                        string profile,
                                        string face,
                                        string facesourire,
                                        string mand,
                                        string max,
                                        string intradroit,
                                        string intra,
                                        string intragauche)
        {
        

        

            

            try
            {


                Dictionary<string, object> postParameters = new Dictionary<string, object>();

                FileStream fs;
                FileInfo nfo;

                if (!string.IsNullOrEmpty(profile))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] profiledata = webClient.DownloadData(profile);
                            postParameters.Add("eprTf", new FileParameter(profiledata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(profile);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(profile, FileMode.Open, FileAccess.Read);
                            byte[] profiledata = new byte[fs.Length];
                            fs.Read(profiledata, 0, profiledata.Length);
                            fs.Close();

                            postParameters.Add("eprTf", new FileParameter(profiledata, "blob", "image/jpeg"));
                        }
                    }



                  
                }

                if (!string.IsNullOrEmpty(face))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] facedata = webClient.DownloadData(face);
                            postParameters.Add("efrTf", new FileParameter(facedata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {

                        nfo = new FileInfo(face);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(face, FileMode.Open, FileAccess.Read);
                            byte[] facedata = new byte[fs.Length];
                            fs.Read(facedata, 0, facedata.Length);
                            fs.Close();

                            postParameters.Add("efrTf", new FileParameter(facedata, "blob", "image/jpeg"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(facesourire))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] facesouriredata = webClient.DownloadData(facesourire);
                            postParameters.Add("efsTf", new FileParameter(facesouriredata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(facesourire);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(facesourire, FileMode.Open, FileAccess.Read);
                            byte[] facesouriredata = new byte[fs.Length];
                            fs.Read(facesouriredata, 0, facesouriredata.Length);
                            fs.Close();

                            postParameters.Add("efsTf", new FileParameter(facesouriredata, "blob", "image/jpeg"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mand))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] manddata = webClient.DownloadData(mand);
                            postParameters.Add("ioManTf", new FileParameter(manddata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(mand);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(mand, FileMode.Open, FileAccess.Read);
                            byte[] manddata = new byte[fs.Length];
                            fs.Read(manddata, 0, manddata.Length);
                            fs.Close();

                            postParameters.Add("ioManTf", new FileParameter(manddata, "blob", "image/jpeg"));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(max))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] maxdata = webClient.DownloadData(max);
                            postParameters.Add("ioMaxTf", new FileParameter(maxdata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(max);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(max, FileMode.Open, FileAccess.Read);
                            byte[] maxdata = new byte[fs.Length];
                            fs.Read(maxdata, 0, maxdata.Length);
                            fs.Close();

                            postParameters.Add("ioMaxTf", new FileParameter(maxdata, "blob", "image/jpeg"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(intradroit))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] intradroitdata = webClient.DownloadData(intradroit);
                            postParameters.Add("ibrTf", new FileParameter(intradroitdata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(intradroit);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(intradroit, FileMode.Open, FileAccess.Read);
                            byte[] intradroitdata = new byte[fs.Length];
                            fs.Read(intradroitdata, 0, intradroitdata.Length);
                            fs.Close();

                            postParameters.Add("ibrTf", new FileParameter(intradroitdata, "blob", "image/jpeg"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(intra))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] intradata = webClient.DownloadData(intra);
                            postParameters.Add("iaTf", new FileParameter(intradata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(intra);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(intra, FileMode.Open, FileAccess.Read);
                            byte[] intradata = new byte[fs.Length];
                            fs.Read(intradata, 0, intradata.Length);
                            fs.Close();

                            postParameters.Add("iaTf", new FileParameter(intradata, "blob", "image/jpeg"));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(intragauche))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] intragauchedata = webClient.DownloadData(intragauche);
                            postParameters.Add("iblTf", new FileParameter(intragauchedata, "blob", "image/jpeg"));

                        }

                    }
                    catch (Exception e)
                    {
                        nfo = new FileInfo(intragauche);
                        if (nfo.Exists)
                        {
                            fs = new FileStream(intragauche, FileMode.Open, FileAccess.Read);
                            byte[] intragauchedata = new byte[fs.Length];
                            fs.Read(intragauchedata, 0, intragauchedata.Length);
                            fs.Close();

                            postParameters.Add("iblTf", new FileParameter(intragauchedata, "blob", "image/jpeg"));
                        }
                    }
                }


                // Create request and receive response
                //string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
                string err = "";
                foreach (var param in postParameters)
                {
                    Dictionary<string, object> post = new Dictionary<string, object>();
                    post.Add(param.Key, param.Value);
                    HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, post, referent);
                    webResponse.Close();

                    err = webResponse.StatusCode == HttpStatusCode.OK ? "" : "Reponse serveur : " + webResponse.StatusCode.ToString();
                }
                return err;

                // Process response
                //StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                //string fullResponse = responseReader.ReadToEnd();
              


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
            
                string postURL = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?action=UPLOAD&patientId=" + IdInvisalign;
            string referer = "https://vip.invisalign.com/v3/auth/patient/editPatient.action?patientId=" + IdInvisalign + "&currentStep=PHOTOS";
            return UploadRadiosInUrl(postURL, referer, fullmouth, panoramic);

            }

            public static string UploadRadiosInUrl(string postURL,
                string referer,
                                     string fullmouth,
                                     string panoramic)
        {

          

            try
            {


                Dictionary<string, object> postParameters = new Dictionary<string, object>();



                FileInfo nfo;
                FileStream fs;

                if (!string.IsNullOrEmpty(fullmouth))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] fullmouthdata = webClient.DownloadData(fullmouth);
                            postParameters.Add("panoramicxTf", new FileParameter(fullmouthdata, System.IO.Path.GetFileName(fullmouth), "application/octet-stream"));

                        }

                    }
                    catch (Exception e)
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

                }

                if (!string.IsNullOrEmpty(panoramic))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] panoramicdata = webClient.DownloadData(panoramic);
                            postParameters.Add("fullmouthxTf", new FileParameter(panoramicdata, Path.GetFileName(panoramic), "application/octet-stream"));

                        }

                    }
                    catch (Exception e)
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
                }



                // Create request and receive response
                string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36";
               HttpWebResponse webResponse = MultipartFormDataPost(postURL, userAgent, postParameters, referer);

                // Process response
               // StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                //string fullResponse = responseReader.ReadToEnd();
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





        public static HttpWebResponse PrescriptionSelectProduct(string IdInvisalign, InvisalignPrescriptionFullObj.InvisalignType typePrescription,InvisalignPrescriptionFullObj.productType productType,InvisalignPrescriptionFullObj.PatientType patientType)
        {


            if (string.IsNullOrEmpty(IdInvisalign))
                return null;



            try
            {

                string tpe = "COMPREHENSIVE_MAUI_INT_2_0";
                string tpeProduct = "INVISALIGN_CLEAR_ALIGNER";
                string tpePatient = "ADULT";
                switch (typePrescription)
                {
                    case InvisalignPrescriptionFullObj.InvisalignType.Compréhensive:
                        if (patientType == InvisalignPrescriptionFullObj.PatientType.Child)
                            tpe = "FIRST_COMPREHENSIVE_INT_2_0";
                        else
                        tpe = "COMPREHENSIVE_MAUI_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.I7: tpe = "EXPRESS_MAUI_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Teen: tpe = "TEEN_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Lite: tpe = "LITE_MAUI_INT_2_0"; break;
                    case InvisalignPrescriptionFullObj.InvisalignType.Vivera: tpe = "RETAINER_MULTIPACK_INT_2_0"; break;
                        
                }
                   switch (productType)
                {
                    case InvisalignPrescriptionFullObj.productType.VIVERA_RETAINERS: tpeProduct = "VIVERA_RETAINERS"; break;
                    case InvisalignPrescriptionFullObj.productType.INVISALIGN_CLEAR_ALIGNER: tpeProduct = "INVISALIGN_CLEAR_ALIGNER"; break;
                    case InvisalignPrescriptionFullObj.productType.PHASE_1_ALIGNER: tpeProduct = "PHASE_1_ALIGNER"; break;

                        
                }
                   switch (patientType)
                   {
                       case InvisalignPrescriptionFullObj.PatientType.Teen: tpePatient = "TEEN"; break;
                       case InvisalignPrescriptionFullObj.PatientType.Adulte: tpePatient = "ADULT"; break;
                       case InvisalignPrescriptionFullObj.PatientType.Child: tpePatient = "CHILD"; break;
                   }
                   string postUrl = "https://vip.invisalign.com/v3/auth/rx/mauiProduct.action?patientId=" + IdInvisalign + "&formType=" + tpe + "&treatmentWorkflow="+tpePatient+"&treatmentCategory=" + tpeProduct + "&productSelectionCategoryType=NONE&formId=0&orderId=0&action=START";
                //string postUrl = "  https://vip.invisalign.com/v3/auth/rx/mauiProduct.action?patientId=" + IdInvisalign + 
                //    "&formType" + tpe + "&treatmentWorkflow=TEEN&treatmentCategory=VIVERA_RETAINERS&productSelectionCategoryType=NONE&formId=0&orderId=0&action=START";
                   string refer = "https://vip.invisalign.com/v3/auth/rx/mauiProduct.action?patientId="+IdInvisalign+"&productSelectionCategoryType=NONE&valid=true&formId=0&change=true";
                Uri address = new Uri(postUrl);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                //request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                request.Timeout = 500000;

                //CookieContainer cookies = new CookieContainer();
                //cookies.Add(Anonymouscookie);

                //foreach (Cookie c in Sessioncookies)
                //    cookies.Add(c);
                request.CookieContainer = cookies;
                request.AllowAutoRedirect = true;
              
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                response.Close();

                return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);
                return null;
            }
            catch (System.Exception e)
            {
                throw new System.Exception("ex 2 : https://vip.invisalign.com/v3/login.action", e);

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
            Etape77Full(invi, formId);
            Etape6Full(invi, formId);
            Etape7Full(invi, formId);
            Etape8Full(invi, formId);
            Etape9Full(invi, formId);
            Etape10Full(invi, formId);
            Etape11Full(invi, formId);
            Etape12Full(invi, formId);
        }



        public static void Etape1Full(InvisalignPrescriptionFullObj invi, string formId)
        {
                      
            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
                response.Close();
                
               // return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                //return null;
            }
        }
        
        public static void Etape2Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
                response.Close();

               // return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }

        public static void Etape3Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
                response.Close();

                //return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
                //return null;
            }
        }

        public static void Etape4Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
                response.Close();

               // return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }

        public static void Etape5Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
                response.Close();

               // return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }


        public static void Etape77Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                /*action=NEXT&formId=18302147&
                 * step=6&
                 * pref=
                 * &__radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.none=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.none_true_false&
                 * __radio__biteRampsUpper=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions_true&
                 * __radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions_true_false&
                 * __radio__biteRampsUpperOptions=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors_true&
                 * __radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors_true_false&
                 * xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.centralIncisors=true&
                 * __checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.centralIncisors=true&
                 * xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.lateralIncisors=true&
                 * __checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.lateralIncisors=true&
                 * __radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.canines=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.canines_true_false
                 * */


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=6");
                data.Append("&pref=");

                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.none=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.none_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors_true_false");
                data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.canines=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.canines_true_false");


                switch (invi.Etape7.biteRampsUpperOpt)
                {
                    case BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.None:
                        data.Append("&__radio__biteRampsUpper=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.none_true");
                        break;
                    case BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.biteRampsUpperOptions:
                        data.Append("&__radio__biteRampsUpper=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.biteRampsUpperOptions_true");
                        break;
                }

                switch (invi.Etape7.biteRampsUpperOptOn)
                {
                    case BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.incisors:
                        data.Append("&__radio__biteRampsUpperOptions=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.incisors_true");
                        break;
                    case BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.canines:
                        data.Append("&__radio__biteRampsUpperOptions=xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.canines_true");
                        break;
                }

                if (invi.Etape7.centralIncisors)
                {
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.centralIncisors=true");
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.centralIncisors=true");
                }
                if (invi.Etape7.lateralIncisors)
                {
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.lateralIncisors=true");
                    data.Append("&__checkbox_xmlFormObj.prescriptionQuestions.primaryProduct.biteRampsUpper.biteRampsUpperOptions.incisors.lateralIncisors=true");
                }
                    


                switch (invi.Etape8.options)
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


                if (invi.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsLeft_true");
                if (invi.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsRight_true");

                if (invi.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsLeft_true");
                if (invi.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsRight_true");

                if (invi.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.maintain_true");
                if (invi.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.correct_true");


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);
                response.Close();

             //   return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }
        
        public static void Etape6Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=7");
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

                if (invi.Etape8.improveMidlineWithIPRUpperArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.upperArch=true");
                if (invi.Etape8.improveMidlineWithIPRLowerArch)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.lowerArch=true");


                switch (invi.Etape8.options)
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


                if (invi.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsLeft_true");
                if (invi.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.upper=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.upperArch.patientsRight_true");

                if (invi.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Left)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsLeft_true");
                if (invi.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Right)
                    data.Append("&__radio__midline.lower=xmlFormObj.prescriptionQuestions.primaryProduct.midline.improveMidlineWithIPR.orientation.lowerArch.patientsRight_true");

                if (invi.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.maintain_true");
                if (invi.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct)
                    data.Append("&__radio__posteriorCrossbite=xmlFormObj.prescriptionQuestions.primaryProduct.posteriorCrossBite.correct_true");
                

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);

                response.Close();
              //  return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }

        public static void Etape7Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=8");
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

                for (int i = 0; i < invi.Etape10.SpaceArray.Length; i++)
                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.spaceArray%5B" + i.ToString() + "%5D.amount.stringValue=" + invi.Etape10.SpaceArray[i].ToString("0.0"));

                if (invi.Etape10.SpaceCloseAll)
                    data.Append("&__radio__spaces_close=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.closeAllSpaces_true");
                else
                    data.Append("&__radio__spaces_close=xmlFormObj.prescriptionQuestions.primaryProduct.archLengthDiscrepancy.leaveSpaces.leaveSpaces_true");


                switch (invi.Etape10.upperExpansion)
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

                switch (invi.Etape10.upperVestibuloVersion)
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

                switch (invi.Etape10.upperRIPAnterieur)
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

                switch (invi.Etape10.upperRIPPosterieurDroit)
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

                switch (invi.Etape10.upperRIPPosterieurGauche)
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


                switch (invi.Etape10.lowerExpansion)
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

                switch (invi.Etape10.lowerVestibuloVersion)
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

                switch (invi.Etape10.lowerRIPAnterieur)
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

                switch (invi.Etape10.lowerRIPPosterieurDroit)
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

                switch (invi.Etape10.lowerRIPPosterieurGauche)
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
                response.Close();

              //  return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }
        
        public static void Etape8Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=9");
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

                if (!invi.Etape10.NeedExtraction)
                    data.Append("&__radio__extraction=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.none_true");
                else
                    data.Append("&__radio__extraction=xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.theseTeeth_true");


                for (int i = 0; i < invi.Etape10.Extraction.Length; i++)
                    if (invi.Etape10.Extraction[i])
                        data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.extraction.theseTeeth.toothArray%5B"+i.ToString()+"%5D.stringValue=true");
               


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);

                response.Close();
              //  return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }

        public static void Etape11Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {


                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=11");
                data.Append("&pref=");


                data.Append("&__radio__dentalRecords=dentalRecords.impression_true&__radio_dentalRecords.impression=dentalRecords.impression_true_false&__radio_dentalRecords.intraOralScan=dentalRecords.intraOralScan_true_false&scanId=&scannerVendor=&__checkbox_dentalRecords.noIntraOralScanAvailable=true");



                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer,false);



                String urlUpload = "";
                String responseString = "";
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.ASCII);
                    responseString = reader.ReadToEnd();


                    string strRegex = @"(_uploadUrl(\s)*:(\s)*"")(.*)("")";  
                    System.Text.RegularExpressions.Regex myRegex = new Regex(strRegex);
                    Match m = myRegex.Match(responseString);
                    if (m.Success)
                    {
                        urlUpload = m.Groups[m.Groups.Count - 2].Value;
                     //   v3/auth/rx/imageUpload.action?orderId=15194711"
                      //  /v3/auth/rx/imageUpload.action?orderId=15193875
                       
                        Invisalign.UploadPhotosInUrl("https://vip.invisalign.com"+urlUpload,
                           referer,
                        patient.Img_Ext_Profile,
                        patient.Img_Ext_Face,
                        patient.Img_Ext_Face_Sourire,
                        patient.Img_Int_Man,
                        patient.Img_Int_Max,
                        patient.Img_Int_Droit,
                        patient.Img_Int_Face,
                        patient.Img_Int_Gauche
                         );

                    }
                }

                response.Close();

                data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=12");
                data.Append("&pref=");


                data.Append("&submitTypeRadio=online&uploadTypeRadio=&imageOps%5B0%5D=&imageOps%5B1%5D=&imageOps%5B2%5D=&imageOps%5B3%5D=&imageOps%5B4%5D=&imageOps%5B5%5D=&imageOps%5B6%5D=&imageOps%5B7%5D=&imageOps%5B8%5D=");



                encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                response = PostForm(url, useragent, contenttype, byteData, referer, false);
                response.Close();
               // return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }



        public static void Etape12Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";


                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=12");
                data.Append("&pref=");


                data.Append("&valid=true&discountPage=false&discountTermsConditions=false&noneStaffDiscount=true");


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer, false);



                String urlUpload = "";
                String responseString = "";
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.ASCII);
                    responseString = reader.ReadToEnd();


                    string strRegex = @"(_uploadUrl(\s)*:(\s)*"")(.*)("")";
                    System.Text.RegularExpressions.Regex myRegex = new Regex(strRegex);
                    Match m = myRegex.Match(responseString);
                    if (m.Success)
                    {
                        urlUpload = m.Groups[m.Groups.Count - 2].Value;

                        Invisalign.UploadRadiosInUrl("https://vip.invisalign.com" + urlUpload,
                           referer,
                        patient.Img_Rad_Pano,
                        ""
                         );

                    }
                }

                response.Close();

                data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=13");
                data.Append("&pref=");


                data.Append("&submitTypeRadio=online&imageOps%5B9%5D=&imageOps%5B10%5D=");
                


                encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                response = PostForm(url, useragent, contenttype, byteData, referer, false);
                response.Close();
             //   return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }


        

        //action=NEXT&formId=23266872&step=11&pref=&__radio__dentalRecords=dentalRecords.impression_true&__radio_dentalRecords.impression=dentalRecords.impression_true_false&__radio_dentalRecords.intraOralScan=dentalRecords.intraOralScan_true_false&scanId=&scannerVendor=&__checkbox_dentalRecords.noIntraOralScanAvailable=true
        public static void Etape10Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=11");
                data.Append("&pref=");


                data.Append("&__radio__dentalRecords=dentalRecords.impression_true&__radio_dentalRecords.impression=dentalRecords.impression_true_false&__radio_dentalRecords.intraOralScan=dentalRecords.intraOralScan_true_false&scanId=&scannerVendor=&__checkbox_dentalRecords.noIntraOralScanAvailable=true");
                


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);

                response.Close();
              //  return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
               // return null;
            }
        }

        public static void Etape9Full(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/full.action?formId=" + formId + "&step=2&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/full.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=10");
                data.Append("&pref=");


                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.specialInstructions.getInstructionArray(0).stringValue="+invi.Etape11_SpecialInstruction.Replace("\n","%0D%0A"));
                //qsdqsdqsd%0D%0Aqsdqsd%0D%0Aqsdqsqd
                


                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);
                response.Close();

              //  return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }


        public static void EtapeTeenFull(InvisalignPrescriptionFullObj invi, string formId)
        {

            try
            {



                string useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string contenttype = "application/x-www-form-urlencoded";
                string referer = "https://vip.invisalign.com/v3/auth/rx/i20/teen.action?formId=" + formId + "&step=9&pref=&valid=true";
                string url = " https://vip.invisalign.com/v3/auth/rx/i20/teen.action";




                StringBuilder data = new StringBuilder();


                data.Append("action=NEXT");
                data.Append("&formId=" + formId);
                data.Append("&step=11");
                data.Append("&pref=");

                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B3%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[3]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B5%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[5]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B10%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[10]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B12%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[12]]));

                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B28%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[28]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B26%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[26]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B21%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[21]]));
                data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.spacesForCanineAnd2NdBi.toothArray%5B19%5D.space=" + (InvisalignPrescriptionFullObj.TeenQuestions.spacecan[(int)invi.teen.spacesForCanineAnd2NdBi[19]]));
                data.Append("&teenSecondMolarTabsGroup=on");

                if (invi.teen.IsteenSecondMolar)
                {
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B0%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B0%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B1%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B1%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B14%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B14%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B15%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B15%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B31%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B31%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B30%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B30%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B17%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B17%5D.stringValue_true_false");
                    data.Append("&__radio_xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B16%5D.stringValue=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray%5B16%5D.stringValue_true_false");


                    if (invi.teen.teenSecondMolarTabs[17])
                        data.Append("&__radio__teenSecondMolarTabs_1716=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[17].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[16])
                        data.Append("&__radio__teenSecondMolarTabs_1716=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[16].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[30])
                        data.Append("&__radio__teenSecondMolarTabs_3130=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[30].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[31])
                        data.Append("&__radio__teenSecondMolarTabs_3130=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[31].stringValue_true");

                    if (invi.teen.teenSecondMolarTabs[0])
                        data.Append("&__radio__teenSecondMolarTabs_01=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[0].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[1])
                        data.Append("&__radio__teenSecondMolarTabs_01=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[1].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[14])
                        data.Append("&__radio__teenSecondMolarTabs_1415=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[14].stringValue_true");
                    if (invi.teen.teenSecondMolarTabs[15])
                        data.Append("&__radio__teenSecondMolarTabs_1415=xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.toothArray[15].stringValue_true");


                    data.Append("&xmlFormObj.prescriptionQuestions.primaryProduct.teenSecondMolarTabs.startStage=" + invi.teen.teenSecondMolarTabsStartStage.ToString());

                }

           

                string encoded = data.ToString();// HttpUtility.UrlEncode(data.ToString());
                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(encoded);


                HttpWebResponse response = PostForm(url, useragent, contenttype, byteData, referer);

                response.Close();
             //   return response;
            }
            catch (System.UriFormatException ex)
            {
                throw new System.Exception("L'adresse Invisalign n'est pas correct : https://vip.invisalign.com/v3/login.action", ex);

            }
            catch (System.Exception ex)
            {
              //  return null;
            }
        }


        public static void ConvertThePrescription(int num, InvisalignPrescriptionFullObj PrescriptionFull)
        {
            if (num == 11)
                PrescriptionFull.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.both;

            if (num == 12)
            {
                PrescriptionFull.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.upperOnly;
                PrescriptionFull.Etape1.upperOnlyDiagnosticSetup = true;
            }

            if (num == 13)
            {
                PrescriptionFull.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.lowerOnly;
                PrescriptionFull.Etape1.lowerOnlyDiagnosticSetup = true;
            }

            if (num == 21)
                PrescriptionFull.Etape2.DoNotMoveAnyTeeth = true;
            if (num == 22)
                PrescriptionFull.Etape2.DoNotMoveAnyTeeth = false;

            if (num == 31)
                PrescriptionFull.Etape3.TeethPermittedForAttachements = true;
            if (num == 32)
                PrescriptionFull.Etape3.TeethPermittedForAttachements = false;


            if (num == 411)
                PrescriptionFull.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;
            if (num == 412)
                PrescriptionFull.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;
            if (num == 421)
                PrescriptionFull.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;
            if (num == 422)
                PrescriptionFull.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;
            if (num == 431)
                PrescriptionFull.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;
            if (num == 432)
                PrescriptionFull.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;
            if (num == 441)
                PrescriptionFull.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;
            if (num == 442)
                PrescriptionFull.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;


            if (num == 45)
            {
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
                PrescriptionFull.Etape4.PosteriorIPR = true;

            }

            if (num == 461)
            {
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
                PrescriptionFull.Etape4.classIIOrIIICorrectionSimulation = true;
                PrescriptionFull.Etape4.classIIOrIIICorrectionSimulationPrecisionCut = InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes;

            }

            if (num == 462)
            {
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
                PrescriptionFull.Etape4.classIIOrIIICorrectionSimulation = true;
                PrescriptionFull.Etape4.classIIOrIIICorrectionSimulationPrecisionCut = InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;

            }

            if (num == 471)
            {
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
                PrescriptionFull.Etape4.distalization = true;
                PrescriptionFull.Etape4.distalizationPrecisionCut = InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes;

            }

            if (num == 472)
            {
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
                PrescriptionFull.Etape4.distalization = true;
                PrescriptionFull.Etape4.distalizationPrecisionCut = InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;

            }

            if (num == 48)
                PrescriptionFull.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.Surgical;

            if (num == 51)
                PrescriptionFull.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.showResultantOverjetAfterAlignment;
            if (num == 52)
                PrescriptionFull.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.maintainInitialOverjet;
            if (num == 53)
                PrescriptionFull.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.improveResultingOverjetWithIPR;


            if (num == 61)
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainResultant;

            if (num == 62)
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainInitial;

            if (num == 631)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
                PrescriptionFull.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorOnly;
                PrescriptionFull.Etape6.extrudeAnteriorOnlyUpperArch = true;
            }

            if (num == 632)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
                PrescriptionFull.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorOnly;
                PrescriptionFull.Etape6.extrudeAnteriorOnlyLowerArch = true;
            }

            if (num == 633)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
                PrescriptionFull.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorTeethAndIntrudePosterior;
                PrescriptionFull.Etape6.extrudeAnteriorTeethAndIntrudePosteriorUpperArch = true;
            }

            if (num == 634)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
                PrescriptionFull.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorTeethAndIntrudePosterior;
                PrescriptionFull.Etape6.extrudeAnteriorTeethAndIntrudePosteriorLowerArch = true;
            }


            if (num == 641)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctDeepBite;
                PrescriptionFull.Etape6.correctDeepBiteUpperArch = true;
            }

            if (num == 6412)
            {
                PrescriptionFull.Etape7.biteRampsUpperOpt = BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.biteRampsUpperOptions;                
            }

            if (num == 64123)
            {
                PrescriptionFull.Etape7.biteRampsUpperOptOn = BasCommon_BO.InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.canines;
            }

            if (num == 64121)
            {
                PrescriptionFull.Etape7.centralIncisors = true;
            }

            if (num == 64122)
            {
                PrescriptionFull.Etape7.lateralIncisors = true;
            }

            if (num == 642)
            {
                PrescriptionFull.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctDeepBite;
                PrescriptionFull.Etape6.correctDeepBiteLowerArch = true;

            }


            if (num == 71)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.showResultantMidlineAfterAlignment;

            }

            if (num == 72)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.maintainInitialMidline;

            }

            if (num == 7311)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;
                PrescriptionFull.Etape8.improveMidlineWithIPRUpperArch = true;
                PrescriptionFull.Etape8.improveMidlineWithIPRUpperArchside = InvisalignPrescriptionFullObj.midline.side.Right;

            }

            if (num == 7312)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;
                PrescriptionFull.Etape8.improveMidlineWithIPRUpperArch = true;
                PrescriptionFull.Etape8.improveMidlineWithIPRUpperArchside = InvisalignPrescriptionFullObj.midline.side.Left;

            }

            if (num == 7321)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;
                PrescriptionFull.Etape8.improveMidlineWithIPRLowerArch = true;
                PrescriptionFull.Etape8.improveMidlineWithIPRLowerArchside = InvisalignPrescriptionFullObj.midline.side.Right;

            }

            if (num == 7322)
            {
                PrescriptionFull.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;
                PrescriptionFull.Etape8.improveMidlineWithIPRLowerArch = true;
                PrescriptionFull.Etape8.improveMidlineWithIPRLowerArchside = InvisalignPrescriptionFullObj.midline.side.Left;

            }

            if (num == 81)
                PrescriptionFull.Etape9.options = InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain;

            if (num == 82)
                PrescriptionFull.Etape9.options = InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct;


            if (num == 911)
                PrescriptionFull.Etape10.SpaceCloseAll = false;

            if (num == 912)
                PrescriptionFull.Etape10.SpaceCloseAll = true;

            if (num == 921)
                PrescriptionFull.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 922)
                PrescriptionFull.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 923)
                PrescriptionFull.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 931)
                PrescriptionFull.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 932)
                PrescriptionFull.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 933)
                PrescriptionFull.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 941)
                PrescriptionFull.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 942)
                PrescriptionFull.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 943)
                PrescriptionFull.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 951)
                PrescriptionFull.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 952)
                PrescriptionFull.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 953)
                PrescriptionFull.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 961)
                PrescriptionFull.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 962)
                PrescriptionFull.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 963)
                PrescriptionFull.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;




            if (num == 971)
                PrescriptionFull.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 972)
                PrescriptionFull.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 973)
                PrescriptionFull.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 981)
                PrescriptionFull.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 982)
                PrescriptionFull.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 983)
                PrescriptionFull.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 991)
                PrescriptionFull.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 992)
                PrescriptionFull.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 993)
                PrescriptionFull.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            if (num == 9911)
                PrescriptionFull.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 9912)
                PrescriptionFull.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 9913)
                PrescriptionFull.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (num == 9921)
                PrescriptionFull.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (num == 9922)
                PrescriptionFull.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (num == 9923)
                PrescriptionFull.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


        }
      
    }

}
