using ConvertingAnyToDoc.Model;
using ConvertingAnyToDoc.Model.Converter;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace ConvertingAnyToDoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrectFromHomeController : Controller
    {
        #region Properties
        private IConverter converter;
        private Model.Email.IEmailRequest emailRequest;
        private const string body = "Correção do aluno: {0} - {1}";
        private const string title = "Prezados,\r\nSegue em anexo a correção referente ao texto do aluno {0}";
        private readonly IConfiguration configuration;
        #endregion

        public CorrectFromHomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        [HttpGet]
        public ActionResult<string> IsAlive()
        {
            return "I'm working";
        }

        [HttpPost]
        public (HttpStatusCode statusCode, FileResult result) CreateDocument(BaseModelCorrectFromHome obj)
        {
            converter = new ConvertCorrectFromHome();

            var converterResult = converter.GetFileResult(obj, configuration.GetSection("CorrectFromHomeModelPath").Value, File);

            if (SendEmail(obj.Sender, obj.Recipients, converterResult.memory, obj.Student))
                return (HttpStatusCode.OK, converterResult.file);

            return (HttpStatusCode.BadRequest, converterResult.file);
        }

        private bool SendEmail(EmailParameters sender, List<string> receivers, MemoryStream doc, string aluno)
        {
            var contentType = new ContentType(SaveOptions.DocxDefault.ContentType);
            
            emailRequest = new EmailRequest();
            
            emailRequest.SetProperties(sender, receivers, string.Format(title, aluno), string.Format(body, aluno, DateTime.Now));
            emailRequest.SetArchiveParams(doc, $"{aluno}.docx", contentType);

            return emailRequest.TrySendEmail();
        }
    }
}
