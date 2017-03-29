using System;
using System.Text;

namespace Paramount.Betterclassifieds.Presentation.Services.Mail
{
    public class MailAttachment
    {
        public string Filename { get; private set; }
        public byte[] FileContents { get; private set; }
        public string ContentType { get; private set; }

        public static MailAttachment New(string filename, byte[] fileContents, string contentType = Business.ContentType.Pdf)
        {
            Guard.NotNullOrEmpty(filename);
            Guard.NotNull(fileContents);

            return new MailAttachment
            {
                Filename = filename,
                FileContents = fileContents,
                ContentType = contentType
            };
        }

        /// <summary>
        /// Creates a mailAttachment as the iCal that will add details to GMail.
        /// </summary>
        public static MailAttachment NewCalendarAttachment(string brand, int id, string title, string description,
            DateTime startDate, DateTime endDate, string reminderEmail, string location, decimal latitude,
            decimal longitude, string eventUrl, string timezoneId)
        {
            var sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendFormat("PRODID:-//{0}/{0}//NONSGML v1.0//EN{1}", brand, Environment.NewLine);
            sb.AppendLine("BEGIN:VEVENT");

            string startDay = $"{GetFormatedDate(startDate)}{GetFormattedTime(startDate)}";

            string endDay = $"{GetFormatedDate(endDate)}{GetFormattedTime(endDate)}";

            sb.AppendFormat("DTSTART;TZID={0}:{1}\n", timezoneId, startDay);
            sb.AppendFormat("DTEND;TZID={0}:{1}\n", timezoneId, endDay);
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:" + title);
            sb.AppendLine("DESCRIPTION:" + description);
            sb.AppendFormat("ORGANIZER;CN={0} Reminder:MAILTO:{1}{2}", brand, reminderEmail, Environment.NewLine);
            sb.AppendFormat("GEO:{0};{1}{2}", latitude, longitude, Environment.NewLine);
            sb.AppendLine("LOCATION:" + location);
            sb.AppendFormat("URL:{0}{1}", eventUrl, Environment.NewLine);
            sb.AppendFormat("UID:{0}{1}", id, Environment.NewLine);
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return new MailAttachment
            {
                Filename = "EventInvite.ics",
                FileContents = sb.ToString().ToByteArray(),
                ContentType = Business.ContentType.Calendar
            };
        }

        private static string GetFormatedDate(DateTime date)
        {
            return $"{date.Year:00}{date.Month:00}{date.Day:00}";
        }

        private static string GetFormattedTime(DateTime dateTime)
        {
            return $"T{dateTime.Hour:00}{dateTime.Minute:00}{dateTime.Second:00}";
        }
    }
}