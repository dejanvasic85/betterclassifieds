using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects.LoggingService
{
    [DataContract()]
    public enum CrudOperationType
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Create = 1,

        [EnumMember]
        Read = 2,

        [EnumMember]
        Update = 3,

        [EnumMember]
        Delete = 4
    }
}
