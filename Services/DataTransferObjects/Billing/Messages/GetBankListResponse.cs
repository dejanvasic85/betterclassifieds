using System.Collections.Generic;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetBankListResponse :BaseResponse
    {
        public List<BillingBankEntity> BankList { get; set; }
    }
}