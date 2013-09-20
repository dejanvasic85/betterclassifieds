using System;

namespace BetterClassified.UI.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OnlineAdTypeAttribute : Attribute
    {
        public string OnlineAdName { get; set; }
    }
}