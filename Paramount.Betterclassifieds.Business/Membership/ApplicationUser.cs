using System.Text;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public PaymentType PreferredPaymentMethod { get; set; }
        public string PayPalEmail { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBsbNumber { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string FullAddress
        {
            get
            {
                var sb = new StringBuilder(AddressLine1);
                if (AddressLine2.HasValue())
                {
                    sb.AppendFormat(", {0}", AddressLine2);
                }
                sb.AppendFormat(", {0}, {1}", State, Postcode);

                return sb.ToString();
            }
        }

        public bool? RequiresEventOrganiserConfirmation { get; set; }

        public virtual bool AuthenticateUser(IAuthManager authManager, string password, bool persistAuthCookie = true)
        {
            if (!authManager.ValidatePassword(Username, password))
                return false;

            authManager.Login(Username, persistAuthCookie);

            return true;
        }
    }
}