using System;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Business
{
    public class RegistrationModel
    {
        public int? RegistrationId { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; private set; }
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
        public byte[] Version { get; set; }
        public string HowYouFoundUs { get; set; }
        public string Phone { get; set; }
        public int? ConfirmationAttempts { get; private set; }

        public RegistrationModel GenerateToken()
        {
            this.Token = CryptoHelper.GenerateToken();
            this.ExpirationDate = DateTime.Now.AddMinutes(10);
            this.ExpirationDateUtc = DateTime.UtcNow.AddMinutes(10);
            this.LastModifiedDate = DateTime.Now;
            this.LastModifiedDateUtc = DateTime.UtcNow;
            return this;
        }

        public RegistrationModel SetPasswordFromPlaintext(string plaintextPassword)
        {
            EncryptedPassword = CryptoHelper.Encrypt(plaintextPassword);
            return this;
        }

        public string DecryptPassword()
        {
            if (EncryptedPassword.IsNullOrEmpty())
                return string.Empty;

            return CryptoHelper.Decrypt(EncryptedPassword);
        }

        public RegistrationModel GenerateUniqueUsername(Func<string, bool> verifier)
        {
            // We always generate a username based on the email address
            // Generally, this should be unique, but you never know what is after @ :)
            
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

        public bool HasConfirmedAlready()
        {
            return ConfirmationDate.HasValue && ConfirmationDateUtc.HasValue;
        }

        public bool Confirm(string compareToken)
        {
            if (this.Token.Equals(compareToken))
            {
                this.ConfirmationDate = DateTime.Now;
                this.ConfirmationDateUtc = DateTime.UtcNow;
                this.ConfirmationAttempts = null;
                return true;
            }
            
            this.ConfirmationDate = null;
            this.ConfirmationDateUtc = null;
            this.ConfirmationAttempts = this.ConfirmationAttempts.GetValueOrDefault() + 1;
            return false;
        }

        public void SetConfirmationCode(ConfirmationCodeResult generatedCode)
        {
            this.Token = generatedCode.ConfirmationCode;
            this.ExpirationDate = generatedCode.Expiry;
            this.ExpirationDateUtc = generatedCode.ExpiryUtc;
        }
    }
}