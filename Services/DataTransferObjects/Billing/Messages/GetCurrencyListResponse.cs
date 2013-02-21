using System;
using System.Collections.Generic;
using Paramount.Common.DataTransferObjects.Common;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetCurrencyListResponse:BaseResponse 
    {
        public List<CurrencyEntity> CurrencyList { get; set; }
    }
}