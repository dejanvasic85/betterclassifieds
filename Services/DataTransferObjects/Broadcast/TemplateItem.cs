namespace Paramount.Common.DataTransferObjects.Broadcast
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TemplateItem
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}