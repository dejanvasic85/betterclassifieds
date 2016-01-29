namespace Paramount.Betterclassifieds.Presentation.ViewModels.Location
{
    public class GoogleTimeZone
    {
        public long DstOffset { get; set; }
        public long RawOffset { get; set; }
        public string Status { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
    }
}