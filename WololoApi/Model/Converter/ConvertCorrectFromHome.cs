using GemBox.Document;
using System;
using System.Collections.Generic;
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
                Aluno = modelCorrectFromHome?.Student,
                Professor = modelCorrectFromHome?.Teacher,
                Nota = modelCorrectFromHome?.Grade,
                NormaPadraoEscrita = TryGetValueByList(modelCorrectFromHome.StandardNormeWriteDomain),
                TratamentoDadoTema = TryGetValueByList(modelCorrectFromHome?.TreatmentByTopic),
                EmpregoGeneroProposto = TryGetValueByList(modelCorrectFromHome?.PoposedGenderAplication),
                OrganizacaoTextual = TryGetValueByList(modelCorrectFromHome?.TextOrganization),
                Desconsideradas = TryGetValueByList(modelCorrectFromHome?.TextToBeDesconsiderated),
                Comentarios = TryGetValueByList(modelCorrectFromHome?.TextComents)
            };

            document.MailMerge.Execute(data);

            return document;
        }

        private static string TryGetValueByList(List<string>? listValues)
        {
            try
            {
                return listValues?.Aggregate((x, y) => $"{x}\r\n{y}");
            }
            catch
            {
                return null;
            }
            
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
