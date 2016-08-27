namespace Paramount.Betterclassifieds.Business.Payment
{
    public class CreditCardResponse
    {
        private CreditCardResponse()
        { }

        private CreditCardResponse(string transactionId)
        {
            if (transactionId.IsNullOrEmpty())
                return;

            TransactionId = transactionId;
            ResponseType = ResponseType.Success;
        }

        public static CreditCardResponse Success(string transactionId)
        {
            return new CreditCardResponse(transactionId);
        }

        public static CreditCardResponse Failed(ResponseType failureType)
        {
            return new CreditCardResponse
            {
                ResponseType = failureType
            };
        }

        public string TransactionId { get; private set; }
        public ResponseType ResponseType { get; private set; }
    }
}