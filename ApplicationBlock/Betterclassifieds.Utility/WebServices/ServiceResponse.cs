namespace Paramount.Betterclassified.Utilities.WebServices
{
    using System;

    [Serializable]
    public class ServiceResponse
    {
        public object Result
        { 
            get;
            set;
        }

        public ServiceResponse ()
        {
            Result = new object();
        }

        public int  ErrorCode
        {
            get; set;
        }
    }
}