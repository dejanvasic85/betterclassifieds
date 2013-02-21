namespace Paramount.Utility.Broadcast
{
    using System.Collections.Specialized;

    public abstract class Email
    {
        public abstract StringDictionary Fields { get;}
        public abstract string TemplateName { get; }
        public abstract string Subject { get; }

        public bool Send()
        {
            return true;
        }
    }
}