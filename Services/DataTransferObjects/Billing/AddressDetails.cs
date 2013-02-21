using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public class AddressDetails
    {
        [DisplayName(" Name")]
        public string Name { get; set; }

        [DisplayName(" Address1")]
        public string Address1 { get; set; }

        [DisplayName(" Address2")]
        public string Address2 { get; set; }

        [DisplayName(" Postcode")]
        public string Postcode { get; set; }

        [DisplayName(" State")]
        public string State { get; set; }

        [DisplayName(" Country")]
        public string Country { get; set; }

        public string City { get; set; }
    }
}
