using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Just an example of how to send an email with a calendar.")]
    public class CalendarEmailSender : ITask
    {
        public void HandleArgs(TaskArguments args)
        {

        }

        public void Run()
        {
            var ics = new Appointment().CreateIcs("Test", "9 Sophia Street, Sunshine West, VIC 3020", DateTime.Now.AddHours(8), DateTime.Now.AddHours(9));

            MemoryStream ms = new MemoryStream();
            UTF8Encoding enc = new UTF8Encoding();
            byte[] arrBytData = enc.GetBytes(ics);
            ms.Write(arrBytData, 0, arrBytData.Length);
            ms.Position = 0;

            // Be sure to give the name a .ics extension here, otherwise it will not work.
            Attachment attachment = new Attachment(ms, "Appointment.ics");

            var client = new SmtpClient();
            client.Send(new MailMessage(
                "test@email.com",
                "dejanvasic24@gmail.com",
                "Testing the invite",
                "We are just testing the invite here")
            {
                Attachments = { attachment }
            });
        }

        public bool Singleton { get; }
    }

    public class Appointment
    {
        private string GetFormatedDate(DateTime date)
        {
            return string.Format("{0:00}{1:00}{2:00}", date.Year, date.Month, date.Day);
        }

        private string GetFormattedTime(DateTime dateTime)
        {
            return string.Format("T{0:00}{1:00}{2:00}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public string CreateIcs(string subject, string location, DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//Testing/Testing//NONSGML v1.0//EN");
            sb.AppendLine("BEGIN:VEVENT");

            string startDay = string.Format("VALUE=DATE:{0}{1}",
                GetFormatedDate(startDate), GetFormattedTime(startDate));

            string endDay = string.Format("VALUE=DATE:{0}{1}",
                GetFormatedDate(endDate), GetFormattedTime(endDate));

            sb.AppendLine("DTSTART;" + startDay);
            sb.AppendLine("DTEND;" + endDay);
            sb.AppendLine("SUMMARY:" + subject);
            sb.AppendLine("LOCATION:" + location);
            sb.AppendLine("GEO:" + location);
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();
        }
    }
}
