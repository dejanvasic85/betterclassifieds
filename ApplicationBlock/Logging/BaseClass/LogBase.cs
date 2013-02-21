namespace Paramount.ApplicationBlock.Logging.BaseClass
{
    using Enum;
    using System;
    using Interface;
    public abstract class LogBase:ILog
    {
        /// <summary>
        /// Data field 
        /// </summary>
        public string Data
        { 
            get; set;
        }

        /// <summary>
        /// Application the log is coming from
        /// </summary>
        public string Application
        {
            get; set;
        }

        /// <summary>
        /// Company Or the domain/context of the log 
        /// </summary>
        public string Domain
        {
            get; set;
        }

        /// <summary>
        /// User Performing the action
        /// </summary>
        public string User
        {
            get; set;
        }

        /// <summary>
        /// Date and time the action was performed
        /// </summary>
        public DateTime DateTimeCreated
        {
            get; set;
        }

        /// <summary>
        /// The account Id/company Id the action is being perfomed on
        /// </summary>
        public string AccountId
        {
            get; set;
        }

        /// <summary>
        /// the category e.g. Event Log | Audit Log | Activity Log
        /// </summary>
        public abstract CategoryTypes Category { get; }

        public string TransactionName
        {
            get; set;
        }

        public string IPAddress { get; set; }

        public string SessionId
        {
            get; set;
        }

        public string HostName
        {
            get; set;
        }

        public  string SecondaryData { get; set; }
    }
}
