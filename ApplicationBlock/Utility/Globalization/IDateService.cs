using System;

namespace Paramount.Utility
{
    /// <summary>
    /// Encapsulates the date handling to help with globalization
    /// </summary>
    public interface IDateService
    {
        DateTime Today { get; }
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }

    /// <summary>
    /// Returns the current time based on the server (not the user)
    /// </summary>
    public class ServerDateService : IDateService
    {
        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
