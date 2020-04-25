using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace ConvertingAnyToDoc.Model
{
    public abstract class JsonToWordConverter : IConverter
    {
        public static MemoryStream stream;
        public static string pathDoc;

        /// <summary>
        /// Convert the informed object to Docx File
        /// </summary>
        /// <param name="getFile">It's good to use <see cref="File"/></param>
        public string GetBase64Result<T>(T obj, string pathDocModel, Func<byte[], string, string, FileResult> getFile) where T : class
        {
            pathDoc = pathDocModel;
            var option = SaveOptions.DocxDefault;

            var response = GetBytes(Process(obj), option);

            return Convert.ToBase64String(response);
        }

        /// <summary>
        /// Get the byte array of the converted file and save the MemoryStream in <see cref="stream"/>
        /// </summary>
        private static byte[] GetBytes(DocumentModel document, SaveOptions options)
        {
            stream = new MemoryStream();

            document.Save(stream, options);

            return stream.ToArray();
        }

        /// <summary>
        /// Validate if the T is the expected type
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public abstract void GenericElementValidate<T>(T obj) where T : class;

        /// <summary>
        /// Return the name of archive base of object element
        /// </summary>
        public abstract string GetNameOfArchive<T>(T model) where T : class;

        /// <summary>
        /// Get the converted object to Docx
        /// </summary>
        public abstract DocumentModel Process<T>(T model) where T : class;
    }
}
