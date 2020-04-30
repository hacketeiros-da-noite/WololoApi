using Aspose.Words;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WololoApi.Model.ResquestModel;

namespace WololoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : Controller
    {
        [HttpPost(nameof(ConvertBase64ToPdfBase64))]
        public ActionResult<string> ConvertBase64ToPdfBase64([FromBody] ConverterModel converter)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var stream = new MemoryStream(Convert.FromBase64String(converter.Base64));
                var doc = new Document(stream);

                MemoryStream dstStream = new MemoryStream();

                doc.Save(dstStream, SaveFormat.Pdf);

                var base64Pdf = Convert.ToBase64String(dstStream.ToArray());

                return Ok(base64Pdf);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}