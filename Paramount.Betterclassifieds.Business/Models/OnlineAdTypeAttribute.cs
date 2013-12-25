using System;

namespace BetterClassified.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OnlineAdTypeAttribute : Attribute
    {
        public string OnlineAdName { get; set; }
    }
}