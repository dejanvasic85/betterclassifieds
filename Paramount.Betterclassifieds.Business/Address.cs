
namespace Paramount.Betterclassifieds.Business
{
    public class Address
    {
        public static Address FromCsvString(string addressInString, char delimiter)
        {
            string[] address = addressInString.Split(delimiter);
            return new Address
            {
                AddressLine1 = address[0],
                AddressLine2 = address[1],
                Suburb = address[2],
                State = address[3],
                Postcode = address[4],
                Country = address[5]
            };
        }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", AddressLine1, AddressLine2, Suburb, Postcode);
        }
    }
}
