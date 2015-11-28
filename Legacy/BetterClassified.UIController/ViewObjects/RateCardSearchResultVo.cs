using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController.ViewObjects
{
    [Serializable]
    public class RateCardSearchResultVo
    {
        public int RateCardId { get; set; }
        public string RateCardName { get; set; }
        public decimal MinimumCharge { get; set; }
        public decimal MaximumCharge { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public int PublicationCount { get; set; }
    }
}
