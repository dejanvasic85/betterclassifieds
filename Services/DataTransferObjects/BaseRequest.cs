using System;
using System.Runtime.Serialization;
using Paramount.ApplicationBlock.Configuration;

namespace Paramount.Common.DataTransferObjects
{
    public static class Extensions
    {
        public static BaseRequest LoadContext(this BaseRequest request)
        {
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.Domain = ConfigSettingReader.Domain;
            return request;
        }
    }

    [Serializable, DataContract]
    public abstract class BaseRequest
    {
        public string RequestTransactionName { get { return TransactionName + ".Request"; } }
        protected BaseRequest ()
        {
            AuditData = new AuditData();
            this.ApplicationName = ConfigSettingReader.ApplicationName;
            this.ClientCode = ConfigSettingReader.ClientCode;
            this.Domain = ConfigSettingReader.Domain;
        }
        
        [DataMember]
        public AuditData AuditData { get; set; }

        [DataMember]
        public string Domain { get; set; }

        [DataMember]
        public string ClientCode { get; set; }

        [DataMember]
        public bool Initialise { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Domain))
            {
                throw new Exception("Invalid context");
            }
            Initialise = true;
            return true;
        }

        public abstract string TransactionName { get; }

    }
}
