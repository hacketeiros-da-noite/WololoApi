using System.Collections.Generic;

namespace ConvertingAnyToDoc.Model
{
    public class BaseModelCorrectFromHome
    {
        public EmailParameters Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Student { get; set; }
        public List<Paragrafo> TextComents { get; set; }

        public List<Option> Options { get; set; }
    }
}
