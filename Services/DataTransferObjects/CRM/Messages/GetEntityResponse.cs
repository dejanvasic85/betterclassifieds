namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class GetEntityResponse
    {
        public ParamountEntity Entity { get; set; }
        public GetEntityResponse()
        {
            Entity = new ParamountEntity();
        }
    }
}
