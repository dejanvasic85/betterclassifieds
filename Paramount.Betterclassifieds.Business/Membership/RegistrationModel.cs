using System;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Business
{
    public class RegistrationModel
    {
        public int RegistrationId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }

        public string Username { get; private set; }
        public string Token { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public DateTime? ExpirationDateUtc { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }
        public DateTime? LastModifiedDateUtc { get; private set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? ConfirmationDateUtc { get; set; }

        public RegistrationModel GenerateToken()
        {
            this.Token = CryptoHelper.GenerateToken();
            this.ExpirationDate = DateTime.Now.AddMinutes(10);
            this.ExpirationDateUtc = DateTime.UtcNow.AddMinutes(10);
            this.LastModifiedDate = DateTime.Now;
            this.LastModifiedDateUtc = DateTime.UtcNow;
            return this;
        }

        public RegistrationModel GenerateUniqueUsername(Func<string, bool> verifier)
        {
            // We always generate a username based on the email address
            // Generally, this should be unique, but you never know what is after @ :)
            Guard.NotNullOrEmpty(Email);

            var count = 1;

            while (true)
            {
                var firstPart = Email.Substring(0, Email.IndexOf("@"));
                Username = count == 1 ? firstPart : string.Format("{0}{1}", firstPart, count);

                if (verifier(Username) == false)
                    break;

                count++;
            }

            return this;
        }
    }
}