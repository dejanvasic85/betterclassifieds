namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetBankListRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GetBankList"; }
        }

        #endregion
    }
}