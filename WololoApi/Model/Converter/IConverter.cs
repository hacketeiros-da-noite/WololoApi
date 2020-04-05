using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace ConvertingAnyToDoc.Model
{
    public interface IConverter
    {
        /// <summary>
        /// Convert the informed object to Docx File
        /// </summary>
        /// <param name="getFile">It's good to use <see cref="File"/></param>
        (FileResult file, MemoryStream memory) GetFileResult<T>(T obj, string pathDocModel, Func<byte[], string, string, FileResult> getFile) where T : class;

        /// <summary>
        /// Validate if the T is the expected type
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        void GenericElementValidate<T>(T obj) where T : class;
    }
}
