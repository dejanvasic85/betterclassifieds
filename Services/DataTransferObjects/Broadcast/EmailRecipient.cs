namespace Paramount.Common.DataTransferObjects.Broadcast
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class EmailRecipient
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public TemplateItemCollection TemplateValue { get; set; }
        public EmailRecipient()
        {
            TemplateValue = new TemplateItemCollection();
        }
    }
}