namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketFieldViewModel
    {
        public string FieldName { get; set; }
        public bool IsRequired { get; set; }
        public string FieldValue { get; set; }
        public int EventTicketId { get; set; }
    }
}