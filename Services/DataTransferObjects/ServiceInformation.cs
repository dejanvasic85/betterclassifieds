using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects
{
    [DataContract]
    public class ServiceInformation
    {
        [DataMember ]
        public string ApplicationName { get; set; }

        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }
    }
}