
using System;
using System.Text;

namespace Paramount.Betterclassifieds.Business
{
    public class Address
    {
        public static Address FromCsvString(string addressInString, char delimiter)
        {
            string[] address = addressInString.Split(delimiter);
            if(address.Length != 6)
                throw new ArgumentException("The specific text address does not have 6 parts", nameof(addressInString));

            return new Address
            {
                StreetNumber = address[0],
                StreetName = address[1],
                Suburb = address[2],
                State = address[3],
                Postcode = address[4],
                Country = address[5]
            };
        }
        public long? AddressId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", StreetNumber, StreetName);
            
            if (Suburb.HasValue())
            {
                sb.AppendFormat(", {0}", Suburb);
            }

            sb.AppendFormat(" {0} {1}", State, Postcode);

            return sb.ToString();
        }
    }
}
