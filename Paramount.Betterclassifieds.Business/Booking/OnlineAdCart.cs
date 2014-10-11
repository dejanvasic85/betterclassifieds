using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public class OnlineAdCart
    {
        public OnlineAdCart()
        {
            Images = new List<string>();
        }

        public string Heading { get; set; }

        public string Description { get; private set; }

        public string ContactName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal? Price { get; set; }

        public int? LocationId { get; set; }

        public int? LocationAreaId { get; set; }

        public List<string> Images { get; set; }

        public string DescriptionHtml { get; private set; }


        public void SetDescriptionHtml(string html)
        {
            // Set the Description Html
            this.DescriptionHtml = html;

            // We also want to convert the html to normal plaintext to appear normally everywhere else
            this.Description = html.FromHtmlToPlaintext();
        }
    }
}