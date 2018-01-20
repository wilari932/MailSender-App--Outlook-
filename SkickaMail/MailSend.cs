using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.ComponentModel.DataAnnotations;
namespace SkickaMail
{
    class MailSend
    {
        private SmtpClient Smtp = new SmtpClient("smtp-mail.outlook.com");
        private MailMessage Message { get; set; }
        private List<string> EmailsToSpam = new List<string>();
        private string _emailAcount;
        private string _password;
        private string _subject;

        public string Subject { get => _subject; set => _subject = value; }
        public string Password { get => _password; set => _password = value; }
        public string EmailAcount { get => _emailAcount; set => _emailAcount = value; }
        public List<Attachment> AddAttachment = new List<Attachment>();

       public MailSend()
        {
            CreateSmtpConnection();
        }

        public void SendMailFromFile(string MessagePath, string EmailsPath)
        {
            
            GetEmailsFromFile(EmailsPath);
            CreateEmailSender(MessagePath);

        }
        public bool TestConnection()
        {
            try
            {
                CreateSmtpConnection();
                SendTestMail();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SendTestMail()
        {
           MailMessage TestMessage = new MailMessage(_emailAcount, _emailAcount,"Connection Success!", "Connection Success!");
            Smtp.Send(TestMessage);
        }
        private void CreateSmtpConnection()
        {
            Smtp.Port = 587;
            Smtp.UseDefaultCredentials = false;
            Smtp.EnableSsl = true;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.Credentials = new System.Net.NetworkCredential(_emailAcount, _password);
            Smtp.Timeout = 10000;
        }
        private void CreateEmailSender(string MessagePath)
        {
            
            foreach (string i in EmailsToSpam)
            {
              
                    if (MailIsValid(i)) {
                        Message = new MailMessage(_emailAcount, i, _subject, GetMessageFromFile(MessagePath));
                        Message.BodyEncoding = UTF8Encoding.UTF8;
                        ReadAttachments();  
                        Smtp.Send(Message);
                    
                    
                    }
                    else
                    {
                        Console.WriteLine("Could not send to this Email, your data is problably wrong" + i);
                    }
                }
              

            
        }
        public bool MailIsValid(string Email)
        {
            var Checkmail = new EmailAddressAttribute();
            return Checkmail.IsValid(Email);
        }
        private void ReadAttachments()
        {
            if (AddAttachment.Count > 0)
            {
                foreach (Attachment i in AddAttachment)
                    Message.Attachments.Add(i);
            }
        }
        private string GetMessageFromFile (string MessageFilePath)
        {
            try
            {
                string GetMessage = File.ReadAllText(MessageFilePath);
                return GetMessage;
            }
            catch
            {
                throw new ArgumentException("We colud not use or find this filepath");
            }
        }
        

        private void GetEmailsFromFile(string FilePath)
        {
            try
            {
                string[] Getmails = File.ReadAllLines(@FilePath);
                foreach (string i in Getmails)
                {
                    EmailsToSpam.Add(i);
                }

            }
            catch
            {
                throw new ArgumentException("We colud not use or find this filepath");
            }
        }
    }


}
