using GemBox.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace ConvertingAnyToDoc.Model
{
    public class EmailRequest : Email.IEmailRequest
    {
        #region Properties
        private SmtpClient smtpClient;
        private EmailParameters sender;
        private List<string> receivers;
        private string host;
        private string subject;
        private string body;
        private string archiveName;
        private Stream archive;
        private ContentType contentType;
        #endregion

        public EmailRequest()
        {
            smtpClient = new SmtpClient()
            {
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
        }

        /// <summary>
        /// Set the necessary properties to send the email
        /// </summary>
        /// <param name="host">The default value is smtp.gmail.com</param>
        public void SetProperties(EmailParameters sender, List<string> receivers, string subject, string body, string host = "smtp.gmail.com")
        {
            this.sender = sender;
            this.receivers = receivers;
            this.host = host;
            this.subject = subject;
            this.body = body;
        }

        /// <summary>
        /// Set the necessary properties to a archive
        /// </summary>
        public void SetArchiveParams(Stream archive, string archiveName, ContentType contentType)
        {
            this.archiveName = archiveName;
            this.archive = archive;
            this.contentType = contentType;
        }

        /// <summary>
        /// Send the email to properties set in <see cref="SetProperties(EmailParameters, List{string}, string, string, string)"/> and <see cref="SetArchiveParams(Stream, string, ContentType)"/>
        /// </summary>
        /// <returns>If the email was send correctly will return true</returns>
        public bool TrySendEmail()
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(sender.User);
            receivers.ForEach(x => mailMessage.To.Add(new MailAddress(x)));

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = false;

            if(archive != null)
            {
                var attachment = new Attachment(archive, archiveName, contentType.ToString());

                mailMessage.Attachments.Add(attachment);
            }

            smtpClient.Host = this.host;
            smtpClient.Credentials = new NetworkCredential(sender.User, sender.Password);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
