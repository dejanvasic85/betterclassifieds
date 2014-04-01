namespace Paramount.Betterclassifieds.Console
{
    using System;

    public class HelpAttribute : Attribute
    {
        public HelpAttribute()
        { }

        public HelpAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
