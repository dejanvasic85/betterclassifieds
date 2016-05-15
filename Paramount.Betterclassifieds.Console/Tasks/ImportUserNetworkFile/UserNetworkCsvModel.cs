namespace Paramount.Betterclassifieds.Console.Tasks
{
    class UserNetworkCsvModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string FullName => $"{FirstName} {Surname}";

        public bool MeetsRequirements()
        {
            return FirstName.HasValue() &&
                   Surname.HasValue() &&
                   Email.HasValue();
        }
    }
}