using System;
using System.Globalization;

namespace Paramount.Betterclassifieds
{
    /// <summary>
    /// Encapsulates the date handling to help with globalization
    /// </summary>
    public interface IDateService
    {
        DateTime Today { get; }
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime NowToNextHour { get; }
        string Timestamp { get; }
        DateTime ConvertFromString(string dateString);
        DateTime ConvertFromString(string dateString, string hourMinuteString);
        string ConvertToString(DateTime? date);
        string ConvertToString(DateTime? date, string format);
        string ConvertToStringTime(DateTime? date, string format = "HH:mm");
        bool IsFutureDate(DateTime dateTime);
    }

    /// <summary>
    /// Returns the current time based on the server (not the user)
    /// </summary>
    public class ServerDateService : IDateService
    {
        private const string DATE_FORMAT = "dd/MM/yyyy"; 
        

        public DateTime Today => DateTime.Today;

        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime NowToNextHour
        {
            get
            {
                var currentServerTime = DateTime.Now.AddHours(1);
                return new DateTime(currentServerTime.Year, currentServerTime.Month, currentServerTime.Day,
                    currentServerTime.Hour, minute: 0, second: 0);
            }
        }

        public string Timestamp => (DateTime.Now - GetEpochTime()).TotalSeconds.ToString();

        public DateTime GetEpochTime()
        {
            return new DateTime(1970, 1, 1);
        }

        public DateTime ConvertFromString(string dateString)
        {
            return DateTime.ParseExact(dateString, DATE_FORMAT, new DateTimeFormatInfo());
        }

        public DateTime ConvertFromString(string dateString, string hourMinuteString)
        {
            var date = ConvertFromString(dateString);
            var time = hourMinuteString.Split(':');
            var hours = int.Parse(time[0]);
            var minutes = 0;
            var seconds = 0;

            if (time.Length > 1)
            {
                minutes = int.Parse(time[1]);
            }

            if (time.Length > 2)
            {
                seconds = int.Parse(time[2]);
            }

            return new DateTime(date.Year, date.Month, date.Day, hours, minutes, seconds);
        }

        public string ConvertToString(DateTime? date)
        {
            return ConvertToString(date, DATE_FORMAT);
        }

        public string ConvertToString(DateTime? date, string format)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }

            return date.Value.ToString(format);
        }

        public string ConvertToStringTime(DateTime? date, string format = "HH:mm")
        {
            return ConvertToString(date, format);
        }

        public bool IsFutureDate(DateTime dateTime)
        {
            return DateTime.Now < dateTime;
        }
    }
}
