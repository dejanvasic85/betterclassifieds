using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class MailAttachment
    {
        public string Filename { get; set; }
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
    }
}