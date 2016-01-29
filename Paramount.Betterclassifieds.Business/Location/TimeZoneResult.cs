namespace Paramount.Betterclassifieds.Business.Location
{
    public class TimeZoneResult
    {
        public long DstOffset { get; set; }
        public long RawOffset { get; set; }
        public string Status { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }

        public bool IsOk()
        {
            if (Status.IsNullOrEmpty())
                return false;

            return Status.ToLower().Equals("ok");
        }
    }
}