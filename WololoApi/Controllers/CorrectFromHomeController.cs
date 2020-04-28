﻿using ConvertingAnyToDoc.Model;
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
        private const string title = "Correção do aluno: {0} - {1}";
        private const string body = "Prezados,\r\nSegue em anexo a correção referente ao texto do aluno {0}";
        private readonly IConfiguration configuration;
        #endregion

        public CorrectFromHomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        [HttpGet(nameof(IsAlive))]
        public ActionResult<string> IsAlive()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok("I'm working");
        }

        [HttpPost(nameof(CreateDocument))]
        public ActionResult<string> CreateDocument(BaseModelCorrectFromHome obj)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.ContentType = "application/json";
            converter = new ConvertCorrectFromHome();

            var converterResult = converter.GetBase64Result(obj, Path.GetFullPath(configuration.GetSection("CorrectFromHomeModelPath").Value), File);

            if (string.IsNullOrWhiteSpace(converterResult))
                return BadRequest();    

            return Ok(converterResult);
        }
    }
}
