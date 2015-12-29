using System;
using System.Text;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AttachmentFactory
    {
        public byte[] CreateCalendarInvite(string brand, int eventId, string eventName, string eventDescription, 
            DateTime eventStartDate, DateTime eventEndDate, string supportEmail, string location, decimal latitude, decimal longitude)
        {
            var calendarData = CalendarTemplate
                .Replace("{{Brand}}", brand)
                .Replace("{{EventName}}", eventName)
                .Replace("{{EventDescription}}", eventDescription)
                .Replace("{{EventStartDate}}", eventStartDate.ToString())
                .Replace("{{EventEndDate}}", eventEndDate.ToString())
                .Replace("{{SupportEmail}}", supportEmail)
                .Replace("{{Location}}", location)
                .Replace("{{Latitude}}", latitude.ToString())
                .Replace("{{Longitude}}", longitude.ToString())
                .Replace("{{CreatedDateTime}}", DateTime.Now.ToString())
                .Replace("{{EventId}}", eventId.ToString())
                ;
            return calendarData.ToByteArray();
        }

        private const string CalendarTemplate = @"BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//{{Brand}}//{{Brand}} Events v1.0//EN
CALSCALE:GREGORIAN
METHOD:PUBLISH
X-WR-CALNAME:Events - {{EventName}}
X-MS-OLK-FORCEINSPECTOROPEN:TRUE
BEGIN:VTIMEZONE
TZID:Australia/Melbourne
X-LIC-LOCATION:Australia/Melbourne
BEGIN:STANDARD
TZOFFSETFROM:+1100
TZOFFSETTO:+1000
TZNAME:AEST
DTSTART:19700405T030000
RRULE:FREQ=YEARLY;BYMONTH=4;BYDAY=1SU
END:STANDARD
BEGIN:DAYLIGHT
TZOFFSETFROM:+1000
TZOFFSETTO:+1100
TZNAME:AEDT
DTSTART:19701004T020000
RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=1SU
END:DAYLIGHT 
END:VTIMEZONE
BEGIN:VEVENT
DTSTAMP:20160130T035620Z
DTSTART;TZID=Australia/Melbourne:20160112T183000
DTEND;TZID=Australia/Melbourne:20160112T213000
STATUS:CONFIRMED
SUMMARY:{{EventName}}
DESCRIPTION:{{EventDescription}}
ORGANIZER;CN={{Brand}} Reminder:MAILTO:{{SupportEmail}}
CLASS:PUBLIC
CREATED:{{CreatedDateTime}}
GEO:{{Latitude}};{{Logitude}}
LOCATION:{{Location}}
URL:{{EventUrl}}
SEQUENCE:2
LAST-MODIFIED:{{CreatedDateTime}}
UID:{{EventId}}
END:VEVENT
END:VCALENDAR";
    }
}
