using Aspose.Words;
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
using WololoApi.Model.ResquestModel;

namespace ConvertingAnyToDoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrectFromHomeController : Controller
    {
        #region Properties
        private IConverter converter;
        private const string title = "Correção do aluno: {0} - {1}";
        private const string body = "Prezados,\r\nSegue em anexo a correção referente ao texto do aluno {0}";
        private readonly IConfiguration configuration;
        #endregion

        public CorrectFromHomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
        }

        [HttpPost(nameof(CreateDocument))]
        public ActionResult<ConverterModel> CreateDocument(BaseModelCorrectFromHome obj)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.ContentType = "application/json";
            converter = new ConvertCorrectFromHome();

            var converterResult = converter.GetBase64Result(obj, Path.GetFullPath(configuration.GetSection("CorrectFromHomeModelPath").Value), File);

            if (string.IsNullOrWhiteSpace(converterResult))
                return BadRequest();    

            return new ConverterModel { Base64 = converterResult };
        }

        [HttpGet(nameof(IsAlive))]
        public ActionResult<string> IsAlive()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok("I'm working");
        }

        [HttpPost(nameof(ConvertBase64ToPdfBase64))]
        public ActionResult<ConverterModel> ConvertBase64ToPdfBase64([FromBody] ConverterModel converter)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            var mstreamDocument = new MemoryStream();
            var dstStream = new MemoryStream();
            try
            {
                var byteDocument = Convert.FromBase64String(converter.Base64);
                mstreamDocument.Write(byteDocument);
                
                var doc = new Document(mstreamDocument);

                doc.Save(dstStream, SaveFormat.Pdf);
                
                var base64Pdf = Convert.ToBase64String(dstStream.ToArray());

                return new ConverterModel { Base64 = base64Pdf };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                mstreamDocument.Dispose();
                mstreamDocument.Close();

                dstStream.Dispose();
                dstStream.Close();
            }
        }
    }
}
