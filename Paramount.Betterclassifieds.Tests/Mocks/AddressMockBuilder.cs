namespace Paramount.Betterclassifieds.Tests
{
    internal partial class AddressMockBuilder
    {
        public AddressMockBuilder Default()
        {
            WithAddressId(1)
                .WithCountry("Australia")
                .WithPostcode("3000")
                .WithState("VIC")
                .WithStreetName("Melbourne Road")
                .WithStreetNumber("Unit 1")
                .WithSuburb("Melbourne CBD");
            return this;
        }
    }
}