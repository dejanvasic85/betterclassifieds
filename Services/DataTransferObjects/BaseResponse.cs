namespace Paramount.Common.DataTransferObjects
{
    public class BaseResponse
    {
        public ServiceInformation ServiceInfo { get; set; }

        public BaseResponse ()
        {
            ServiceInfo = new ServiceInformation();
        }
    }
}