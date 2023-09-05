using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.IO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MailMgmt
    {
        public static string token()
        {
            return DAC.token;
        }
        private static string _pathRest;
        public static string pathRest
        {
            get
            {
               return DAC.PathRest;

            }
            set
            {
                _pathRest = "_" + value;
            }
        }
        //public static string token
        //{
        //    get
        //    {

        //        return System.Configuration.ConfigurationManager.AppSettings["token" + prefix];

        //    }
        //}
        private static string _prefix;
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                   DAC.GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        public static void SendMailAsync(System.Net.Mail.MailMessage message)
        {
            Action<System.Net.Mail.MailMessage> act = new Action<System.Net.Mail.MailMessage>(SendMail);

            act.BeginInvoke(message,null,null);
        }

        public static void SendMail(System.Net.Mail.MailMessage message)
        {

            if (message.From == null)
            {
                string From = ConfigurationManager.AppSettings["MailFrom"];
                string AliasFrom = ConfigurationManager.AppSettings["MailFromAlias"];
                message.From = new MailAddress(From, AliasFrom);
            }


            string MailToTEST = ConfigurationManager.AppSettings["MailToTEST"];
            string CCI = ConfigurationManager.AppSettings["MailCCI"];
            string accuse = ConfigurationManager.AppSettings["AccuseReception"];


            string Smtp = ConfigurationManager.AppSettings["MailSMTP"];
            string SmtpPort = ConfigurationManager.AppSettings["MailSMTPPort"];
            string User = ConfigurationManager.AppSettings["MailSMTPUser"];
            string Password = ConfigurationManager.AppSettings["MailSMTPPassword"];

            int Prt;
            if (!int.TryParse(SmtpPort, out Prt))
                Prt = 25;

            if ((message.Subject == "") || (message.Body == "")) return;

            //Attachment att = new Attachment(FileToAttach);

          
         

           


            if ((MailToTEST != "") && (MailToTEST != null))
            {
                List<MailAddress> ToAddrs = message.To.Select(x=>x).ToList();
                message.To.Clear();
                message.To.Add(MailToTEST);
                message.Subject = "[TEST] " + message.Subject;

                string Body = message.Body;
                message.Body = "";

                foreach (MailAddress add in ToAddrs)
                    message.Body += "[send to " + add.DisplayName + "(" + add.Address + ")] \n";

                message.Body += "\n\n";
                message.Body += Body;
                if (CCI != "")
                    message.Bcc.Add(CCI);
            }



            SmtpClient client = new SmtpClient(Smtp, Prt);

            if ((User != null) && (Password != null))
            {
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(User, Password);
                client.UseDefaultCredentials = false;
                client.Credentials = SMTPUserInfo;
            }
            if (accuse == "") accuse = message.From.Address;

            message.Headers.Add("Disposition-Notification-To", accuse);
            message.Headers.Add("Return-Receipt-To", accuse);
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;


            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Exception Subex = ex;
                string s = ex.Message;
                while (Subex.InnerException != null)
                {
                    Subex = Subex.InnerException;
                    s = s + "\n" + Subex.Message;
                }

                throw new System.Exception(s);
            }
            finally
            {

            }


        }



        public static void SendMail(string fromAddr, string alias, List<MailAddress> ToAddrs, string Subject, string Body, List<Attachment> files, bool isHtml)
        {

            string From = "";
            string AliasFrom = "";
            if (fromAddr == "")
            {
                From = ConfigurationManager.AppSettings["MailFrom"];
                if (alias == "")
                    AliasFrom = ConfigurationManager.AppSettings["MailFromAlias"];
                else
                    AliasFrom = alias;
            }
            else
            {
                From = fromAddr;
                AliasFrom = alias;
            }


            string MailToTEST = ConfigurationManager.AppSettings["MailToTEST"];
            string CCI = ConfigurationManager.AppSettings["MailCCI"];
            string accuse = ConfigurationManager.AppSettings["AccuseReception"];


            string Smtp = ConfigurationManager.AppSettings["MailSMTP"];
            string SmtpPort = ConfigurationManager.AppSettings["MailSMTPPort"];
            string User = ConfigurationManager.AppSettings["MailSMTPUser"];
            string Password = ConfigurationManager.AppSettings["MailSMTPPassword"];

            int Prt;
            if (!int.TryParse(SmtpPort, out Prt))
                Prt = 25;

            if ((Subject == "") || (Body == "")) return;

            //Attachment att = new Attachment(FileToAttach);

            MailAddress FromAddr = new MailAddress(From, AliasFrom);

            MailMessage msg = new MailMessage();
            msg.From = FromAddr;

            if (files != null)
                foreach (Attachment a in files)
                    msg.Attachments.Add(a);

            msg.IsBodyHtml = isHtml;

            if ((MailToTEST != "") && (MailToTEST != null))
            {
                msg.To.Add(MailToTEST);
                msg.Subject = "[TEST] " + Subject;

                foreach (MailAddress add in ToAddrs)
                    msg.Body += "[send to " + add.DisplayName + "(" + add.Address + ")] \n";

                msg.Body += "\n\n";
                msg.Body += Body;
                if (CCI != "")
                    msg.Bcc.Add(CCI);
            }
            else
            {

                foreach (MailAddress add in ToAddrs)
                    msg.CC.Add(add);
                msg.Subject = Subject;
                msg.Body = Body;
                if (CCI != "")
                    msg.Bcc.Add(CCI);

            }



            SmtpClient client = new SmtpClient(Smtp, Prt);

            if ((User != null) && (Password != null))
            {
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(User, Password);
                client.UseDefaultCredentials = false;
                client.Credentials = SMTPUserInfo;
            }
            if (accuse == "") accuse = From;

            msg.Headers.Add("Disposition-Notification-To", accuse);
            msg.Headers.Add("Return-Receipt-To", accuse);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;


            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            try
            {
                client.Send(msg);
            }
            catch (SmtpException ex)
            {
                Exception Subex = ex;
                string s = ex.Message;
                while (Subex.InnerException != null)
                {
                    Subex = Subex.InnerException;
                    s = s + "\n" + Subex.Message;
                }

                throw new System.Exception(s);
            }
            finally
            {

            }


        }


        public static void SendMail(MailAddress ToAddr, string Subject, string Body, Attachment files, bool isHtml)
        {

            string From = ConfigurationManager.AppSettings["MailFrom"];
            string AliasFrom = ConfigurationManager.AppSettings["MailFromAlias"];
            List<MailAddress> lstToAddrs = new List<MailAddress>();
            List<Attachment> lstfiles = new List<Attachment>();
            if (files != null) lstfiles.Add(files);
            lstToAddrs.Add(ToAddr);

            SendMail(From, AliasFrom, lstToAddrs, Subject, Body, lstfiles, isHtml);

        }


        public static void SendMail(MailAddress ToAddr, string Subject, string Body, Attachment files)
        {



            string From = ConfigurationManager.AppSettings["MailFrom"];
            string AliasFrom = ConfigurationManager.AppSettings["MailFromAlias"];
            string MailToTEST = ConfigurationManager.AppSettings["MailToTEST"];
            string CCI = ConfigurationManager.AppSettings["MailCCI"];
            string accuse = ConfigurationManager.AppSettings["AccuseReception"];


            string Smtp = ConfigurationManager.AppSettings["MailSMTP"];
            string SmtpPort = ConfigurationManager.AppSettings["MailSMTPPort"];
            string User = ConfigurationManager.AppSettings["MailSMTPUser"];
            string Password = ConfigurationManager.AppSettings["MailSMTPPassword"];

            int Prt;
            if (!int.TryParse(SmtpPort, out Prt))
                Prt = 25;

            if ((Subject == "") || (Body == "")) return;

            //Attachment att = new Attachment(FileToAttach);

            MailAddress FromAddr = new MailAddress(From, AliasFrom);

            MailMessage msg = new MailMessage();
            msg.From = FromAddr;

            if (files!=null) 
                msg.Attachments.Add(files);

            if ((MailToTEST != "") && (MailToTEST != null))
            {
                msg.To.Add(MailToTEST);
                msg.Subject = "[TEST] " + Subject;
                msg.Body = "[send to " + ToAddr + "] \n\n" + Body;
                if (CCI != "")
                    msg.Bcc.Add(CCI);
            }
            else
            {
                msg.To.Add(ToAddr);
                msg.Subject = Subject;
                msg.Body = Body;
                if (CCI != "")
                    msg.Bcc.Add(CCI);

            }



            SmtpClient client = new SmtpClient(Smtp, Prt);

            if ((User != null) && (Password != null))
            {
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(User, Password);
                client.UseDefaultCredentials = false;
                client.Credentials = SMTPUserInfo;
            }
            if (accuse == "") accuse = From;

            msg.Headers.Add("Disposition-Notification-To", accuse);
            msg.Headers.Add("Return-Receipt-To", accuse);
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;


            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            try
            {
                client.Send(msg);
            }
            catch (SmtpException ex)
            {
                Exception Subex = ex;
                string s = ex.Message;
                while (Subex.InnerException != null)
                {
                    Subex = Subex.InnerException;
                    s = s + "\n" + Subex.Message;
                }

                throw new System.Exception(s);
            }
            finally
            {

            }


        }


        private static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //
        }
        public static void sendMailGoogleAvis(string postUrl, String mail)
        {

            
            Uri address = new Uri(postUrl+mail);
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;


            var data = Encoding.Default.GetBytes(mail);
            // Set up the request properties.
            request.Method = "POST";

            //request.ContentType = ("application/json");
            //request.CookieContainer = new CookieContainer();
            request.ContentLength = data.Length;
            //
            //request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            //request.Expect = "";
            request.Headers.Add("Authorization", "bearer " + token());
            Stream webpageStream = request.GetRequestStream();
            webpageStream.Flush();
            webpageStream.Close();
            //HttpWebResponse rep = request.GetResponse() as HttpWebResponse;

           // rep.Close();
           
        }

        public static string contentType { get; set; }
    }
}
