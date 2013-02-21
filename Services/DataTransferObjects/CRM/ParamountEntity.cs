namespace Paramount.Common.DataTransferObjects.CRM
{
    public class ParamountEntity
    {
        public string EntityCode { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
        public int PrimaryContactId { get; set; }
        public int? TimeZone { get; set; }
    }
}
