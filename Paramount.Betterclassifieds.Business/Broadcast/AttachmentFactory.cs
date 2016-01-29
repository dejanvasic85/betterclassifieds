using System;
using System.Text;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AttachmentFactory
    {
        public byte[] CreateCalendarInvite(string brand, int eventId, string eventName, string eventDescription,
            DateTime eventStartDate, DateTime eventEndDate, string reminderEmail, string location, decimal latitude,
            decimal longitude, string eventUrl, string timezoneId)
        {
            var sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendFormat("PRODID:-//{0}/{0}//NONSGML v1.0//EN{1}", brand, Environment.NewLine);
            sb.AppendLine("BEGIN:VEVENT");

            string startDay = string.Format("{0}{1}",
                GetFormatedDate(eventStartDate), GetFormattedTime(eventStartDate));

            string endDay = string.Format("{0}{1}",
                GetFormatedDate(eventEndDate), GetFormattedTime(eventEndDate));

            sb.AppendFormat("DTSTART;TZID={0}:{1}\n", timezoneId, startDay);
            sb.AppendFormat("DTEND;TZID={0}:{1}\n", timezoneId, endDay);
            //sb.AppendLine("DTSTART;" + startDay);
            //sb.AppendLine("DTEND;" + endDay);
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:" + eventName);
            sb.AppendLine("DESCRIPTION:" + eventDescription);
            sb.AppendFormat("ORGANIZER;CN={0} Reminder:MAILTO:{1}{2}", brand, reminderEmail, Environment.NewLine);
            sb.AppendFormat("GEO:{0};{1}{2}", latitude, longitude, Environment.NewLine);
            sb.AppendLine("LOCATION:" + location);
            sb.AppendFormat("URL:{0}{1}", eventUrl, Environment.NewLine);
            sb.AppendFormat("UID:{0}{1}", eventId, Environment.NewLine);
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString().ToByteArray();
        }

        private string GetFormatedDate(DateTime date)
        {
            return string.Format("{0:00}{1:00}{2:00}", date.Year, date.Month, date.Day);
        }

        private string GetFormattedTime(DateTime dateTime)
        {
            return string.Format("T{0:00}{1:00}{2:00}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
    }
}
