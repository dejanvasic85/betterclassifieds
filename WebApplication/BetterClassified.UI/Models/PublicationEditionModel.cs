using System.Collections.Generic;

namespace BetterClassified.UI.Models
{
    public class PublicationEditionModel
    {
        public int PublicationId { get; set; }
        public string PublicationName { get; set; }
        public List<EditionModel> Editions { get; set; }

        public PublicationEditionModel()
        {
            this.Editions = new List<EditionModel>();
        }
    }
}
