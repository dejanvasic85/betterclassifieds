namespace Paramount.Betterclassifieds.Business.Events
{
    /// <summary>
    /// Contains details about extra details that are required for each ticket 
    /// </summary>
    public class EventTicketField
    {
        public int EventTicketFieldId { get; set; }
        public int? EventId { get; set; }
        public string FieldName { get; set; }
        public bool IsRequired { get; set; }
    }
}