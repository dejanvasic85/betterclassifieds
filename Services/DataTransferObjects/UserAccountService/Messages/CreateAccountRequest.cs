namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System;

    [Serializable]
    public class CreateAccountRequest : CreateUserRequest
    {
        public string AccountName { get; set; }

        public string ABN { get; set; }

        public string Address { get; set; }

        public int? Postcode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int? BusinessType { get; set; }
    }
}
