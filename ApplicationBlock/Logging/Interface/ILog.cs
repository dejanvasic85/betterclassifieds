using System;
using Paramount.ApplicationBlock.Logging.Enum;

namespace Paramount.ApplicationBlock.Logging.Interface
{
    public interface ILog
    {
        /// <summary>
        /// Data field 
        /// </summary>
        string Data { get; set; }
        /// <summary>
        /// Application the log is coming from
        /// </summary>
        string Application { get; set; }
        /// <summary>
        /// Company Or the domain/context of the log 
        /// </summary>
        string Domain { get; set; }
        /// <summary>
        /// User Performing the action
        /// </summary>
        string User { get; set; }
        /// <summary>
        /// Date and time the action was performed
        /// </summary>
        DateTime DateTimeCreated{ get; set; }
        /// <summary>
        /// The account Id/company Id the action is being perfomed on
        /// </summary>
        string AccountId { get; set; }
        /// <summary>
        /// the category e.g. Event Log | Audit Log | Activity Log
        /// </summary>
        CategoryTypes Category { get; }
        /// <summary>
        /// Transaction Name
        /// </summary>
        string TransactionName { get; set; }

        string IPAddress { get; set; }
        string SessionId { get; set; }
        string HostName { get; set; }
    }
}