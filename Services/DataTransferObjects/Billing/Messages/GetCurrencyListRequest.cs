namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetCurrencyListRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GetCurrencyList"; }
        }

        #endregion
    }
}