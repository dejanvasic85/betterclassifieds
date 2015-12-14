namespace Paramount.Betterclassifieds.Tests
{
    internal partial class ApplicationUserMockBuilder
    {
        public ApplicationUserMockBuilder Default()
        {
            WithUsername("foo@bar.com");
            WithEmail("foo@bar.com");
            WithFirstName("Foo");
            WithLastName("Bar");
            WithAddressLine1("1 Fake Road");
            WithAddressLine2("Suburb");
            WithCity("City");
            WithPostcode("3000");
            WithState("State");
            WithPhone("0433 333 333");
            return this;
        }
    }
}