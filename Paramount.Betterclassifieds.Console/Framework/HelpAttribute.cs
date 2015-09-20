namespace Paramount.Betterclassifieds.Console
{
    using System;

    public class HelpAttribute : Attribute
    {
        public HelpAttribute()
        { }

        public HelpAttribute(string sampleCall)
        {
            this.SampleCall = sampleCall;
        }

        public HelpAttribute(string sampleCall, string description)
            : this(sampleCall)
        {
            this.Description = description;
        }

        public string SampleCall { get; set; }
        public string Description { get; set; }
    }
}
