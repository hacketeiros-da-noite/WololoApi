using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace ConvertingAnyToDoc.Model.Email
{
    public interface IEmailRequest
    {
        /// <summary>
        /// Set the necessary properties to send the email
        /// </summary>
        /// <param name="host">The default value is smtp.gmail.com</param>
        void SetProperties(EmailParameters sender, List<string> receivers, string subject, string body, string host = "smtp.gmail.com");

        /// <summary>
        /// Set the necessary properties to a archive
        /// </summary>
        void SetArchiveParams(Stream archive, string archiveName, ContentType contentType);

        /// <summary>
        /// Send the email to properties set in <see cref="SetProperties(EmailParameters, List{string}, string, string, string)"/> and <see cref="SetArchiveParams(Stream, string, ContentType)"/>
        /// </summary>
        /// <returns>If the email was send correctly will return true</returns>
        bool TrySendEmail();
    }
}
