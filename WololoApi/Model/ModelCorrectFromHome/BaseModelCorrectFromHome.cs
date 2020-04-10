using System.Collections.Generic;

namespace ConvertingAnyToDoc.Model
{
    public class BaseModelCorrectFromHome
    {
        /// <summary>
        /// Email of sender
        /// </summary>
        public EmailParameters Sender { get; set; }
        
        /// <summary>
        /// Emails of receivers
        /// </summary>
        public List<string> Recipients { get; set; }
        
        /// <summary>
        /// Name of the student of correction
        /// </summary>
        public string Student { get; set; }

        /// <summary>
        /// Name of teacher that made the correction
        /// </summary>
        public string Teacher { get; set; }

        /// <summary>
        /// The grade of the student
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// Observations of text
        /// </summary>
        public List<Paragrafo> TextComents { get; set; }

        /// <summary>
        /// Option that are not good about the Standard Norme Write Domain
        /// Examples: Diatrics aplication erros
        /// </summary>
        public List<string> StandardNormeWriteDomain { get; set; }

        /// <summary>
        /// Option that are not good about the Treatment by topic
        /// Example: vocabulary, title
        /// </summary>
        public List<string> TreatmentByTopic { get; set; }

        /// <summary>
        /// Options that are not good about the Proposed Gender Aplication
        /// Example: linguistic resource domain
        /// </summary>
        public List<string> PoposedGenderAplication { get; set; }

        /// <summary>
        /// Option that are not good about the Text Organization
        /// Example: language domain
        /// </summary>
        public List<string> TextOrganization { get; set; }

        /// <summary>
        /// Option about text to be desconsiderated
        /// Example: theme escaple
        /// </summary>
        public List<string> TextToBeDesconsiderated { get; set; }
    }
}
