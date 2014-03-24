using System;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Used for properties on Broadcast types e.g. AccountConfirmation class for FirstName property
    /// </summary>
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