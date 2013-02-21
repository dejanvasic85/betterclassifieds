namespace Paramount.Common.DataTransferObjects.Banner
{
    public class BannerAuditEntity
    {
        public string ActionTypeName { get; set; }
        public string BannerId { get; set; }
        public string IpAddress { get; set; }
        public string ClientCode { get; set; }
        public string ApplicationName { get; set; }
        public string Pageurl { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string UserGroup { get; set; }
        public string Gender { get; set; }
        public string Postcode { get; set; }
    }
}