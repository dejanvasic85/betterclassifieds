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

        public string SampleCall { get; set; }
    }
}
