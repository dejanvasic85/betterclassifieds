using System;

namespace Paramount.Common.DataTransferObjects.EventService.Messages
{
    public class GetGenreRequest : BaseRequest {
        public override string TransactionName
        {
            get { return AuditTransaction.GetGenre; }
        }
    }
}
