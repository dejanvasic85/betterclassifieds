using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects.LoggingService
{
    [DataContract()][Flags]
    public enum CategoryType
    {
        None = 0,

        [EnumMember]
        AuditLog = 1,

        [EnumMember]
        EventLog = 2,

        [EnumMember]
        ActivityLog = 3
    }
}
