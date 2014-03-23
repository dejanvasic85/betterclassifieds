using System;

namespace Paramount.Betterclassifieds.Business
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class PlaceholderAttribute : Attribute
    {
        public string TokenName { get; private set; }
        
        public PlaceholderAttribute(string tokenName)
        {
            this.TokenName = tokenName;
        }
    }
}