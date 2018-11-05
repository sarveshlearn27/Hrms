using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core
{
    public class EmailNotification : INotification
    {
        /// <summary>
        /// Send Email Notification
        /// </summary>
        /// <param name="destination"></param>
        public void sendNotification(string destination)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("sarvesh.sawant@wonderbiz.in");
            mail.To.Add("trupti.nagekar@wonderbiz.in");
            mail.Subject = "Auto Generated Attendance Report";
            mail.Body = "Auto Generated Attendance Report";

            System.Net.Mail.Attachment attachment;
            var emailAttachmentPath = ConfigurationManager.AppSettings["GeneratedReport"];
            attachment = new System.Net.Mail.Attachment(emailAttachmentPath);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("sarvesh.sawant@wonderbiz.in", "sarveshs!123#");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
