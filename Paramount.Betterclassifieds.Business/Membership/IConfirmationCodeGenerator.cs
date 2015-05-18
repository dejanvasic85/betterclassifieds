using System;

namespace Paramount.Betterclassifieds.Business
{
    public interface IConfirmationCodeGenerator
    {
        ConfirmationCodeResult GenerateCode();
    }

    public class ConfirmationCodeResult
    {
        public ConfirmationCodeResult(string confirmationCode, DateTime expiry, DateTime expiryUtc)
        {
            ConfirmationCode = confirmationCode;
            Expiry = expiry;
            ExpiryUtc = expiryUtc;
        }

        public string ConfirmationCode { get; private set; }
        public DateTime Expiry { get; private set; }
        public DateTime ExpiryUtc { get; private set; }
    }
}