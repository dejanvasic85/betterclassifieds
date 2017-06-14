using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventAuthorisationException : Exception
    {
        public EventAuthorisationException(string msg) :
            base(msg)
        { }
    }
}