using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
