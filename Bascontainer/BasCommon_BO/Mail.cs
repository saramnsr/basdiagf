using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Net.Configuration;

namespace BasCommon_BO
{
    public class Mail
    {

        private string _Username = "";
        [PropertyCanBeSerialized]
        public string Username
        {
            get
            {


                return _Username;
            }
            set
            {
                _Username = value;
            }
        }

        private string _Host;
        [PropertyCanBeSerialized]
        public string Host
        {
            get
            {


                return _Host;
            }
            set
            {
                _Host = value;
            }
        }

        private string _Password;
        [PropertyCanBeSerialized]
        public string Password
        {
            get
            {


                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        public string Title { get; set; }
        public string Text { get; set; }
        public string From { get; set; }
        public bool RequireAutentication { get; set; }
        public bool DeleteFilesAfterSend { get; set; }

        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> AttachmentFiles { get; set; }

        #region appi declarations

        internal enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 1,
            MOVEFILE_COPY_ALLOWED = 2,
            MOVEFILE_DELAY_UNTIL_REBOOT = 4,
            MOVEFILE_WRITE_THROUGH = 8
        }

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool MoveFileEx(string lpExistingFileName,
                                      string lpNewFileName,
                                      MoveFileFlags dwFlags);

        #endregion

        public Mail()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
           
            AttachmentFiles = new List<string>();
           
        }

        public void Send()
        {
            var client = new SmtpClient
            {
                Host = Host,
                EnableSsl = false,
            };

            if (RequireAutentication)
            {
                var credentials = new NetworkCredential(Username,
                                                        Password);
                client.Credentials = credentials;
            }

            var message = new MailMessage
            {
                Sender = new MailAddress(From, From),
                From = new MailAddress(From, From)
            };

            AddDestinataryToList(To, message.To);
            AddDestinataryToList(Cc, message.CC);
            AddDestinataryToList(Bcc, message.Bcc);

            message.Subject = Title;
            message.Body = Text;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.High;

            var attachments = AttachmentFiles.Select(file => new Attachment(file));
            foreach (var attachment in attachments)
                message.Attachments.Add(attachment);

            client.Send(message);

            if (DeleteFilesAfterSend)
                AttachmentFiles.ForEach(DeleteFile);
        }

        private void AddDestinataryToList(IEnumerable<string> from,
           ICollection<MailAddress> mailAddressCollection)
        {
            foreach (var destinatary in from)
                mailAddressCollection.Add(new MailAddress(destinatary, destinatary));
        }

        private void DeleteFile(string filepath)
        {
            // this should delete the file in the next reboot, not now.
            MoveFileEx(filepath, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
        }
    }
}
