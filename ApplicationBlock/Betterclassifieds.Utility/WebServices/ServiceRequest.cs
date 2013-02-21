namespace Paramount.Betterclassified.Utilities.WebServices
{
    using System.Collections.Generic;
    using System;
    [Serializable]
    public class ServiceRequest:ServiceCallBase 
    {
        public ServiceRequest ()
        {
            Parameters = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Parameters
        {
            get; set;
        }

        public string ClientHostName
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string IPAddress
        { 
            get;
            set;
        }
    }
}