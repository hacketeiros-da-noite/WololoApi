using GemBox.Document;
using System;
using System.IO;
using System.Linq;

namespace ConvertingAnyToDoc.Model.Converter
{
    public class ConvertCorrectFromHome : JsonToWordConverter
    {
        /// <summary>
        /// Get the converted object to Docx
        /// </summary>
        public override DocumentModel Process<T>(T model) where T : class
        {
            GenericElementValidate(model);

            string path = Path.Combine(pathDoc);

            DocumentModel document = DocumentModel.Load(path);

            var date = DateTime.Now.ToShortDateString();
            var modelCorrectFromHome = model as BaseModelCorrectFromHome;

            var data = new
            {
                Aluno = modelCorrectFromHome.Student,
                Date = date,
                Text = modelCorrectFromHome.TextComents.Aggregate((x, y) => new Paragrafo { Comment = $"{x.Comment}\r\n\r\n{y.Comment}" }).Comment,
                Option = modelCorrectFromHome.Options.Select(x => $"{x.Question}: {x.Answer}").Aggregate((x, y) => $"{x}\r\n\r\n{y}")
            };

            document.MailMerge.Execute(data);

            return document;
        }

        /// <summary>
        /// Return the name of archive base of object element
        /// </summary>
        public override string GetNameOfArchive<T>(T model) where T : class
        {
            GenericElementValidate(model);

            return (model as BaseModelCorrectFromHome).Student.ToLowerInvariant();
        }

        /// <summary>
        /// Validate if the T is the expected type
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public override void GenericElementValidate<T>(T obj)
        {
            if (obj.GetType() != typeof(BaseModelCorrectFromHome))
                throw new ArgumentException();
        }
    }
}
