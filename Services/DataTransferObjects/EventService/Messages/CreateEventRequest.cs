namespace Paramount.Common.DataTransferObjects.EventService.Messages
{
    using System;
    using System.Collections.ObjectModel;

   
    public class CreateEventRequest : BaseRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string HostName { get; set; }

        public string RegionCode { get; set; }

        public string LocationName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string IsRepeat { get; set; }



        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public override string TransactionName
        {
            get { return AuditTransaction.CreateEvent; }
        }
    }
}
