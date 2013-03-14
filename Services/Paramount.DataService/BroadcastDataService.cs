using System.Linq;

namespace Paramount.DataService
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using ApplicationBlock.Data;
    using Broadcast;

    public static class BroadcastDataService
    {
        private const string ConfigSection = @"paramount/services";
        private const string ConfigKey = "";

        public static void UpdateEmailTemplate(string clientCode, string name, string description, string subject,
                string sender, string emailContent)
        {
            var df = new DatabaseProxy(Proc.UpdateEmailTemplate.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.Description, description, StringType.VarChar);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.EmailContent, emailContent, StringType.VarChar);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.ClientCode, clientCode, StringType.VarChar);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.Sender, sender, StringType.VarChar);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.Subject, subject, StringType.VarChar);
            df.AddParameter(Proc.UpdateEmailTemplate.Params.TemplateName, name, StringType.VarChar);
            df.ExecuteQuery();
        }

        public static void InsertEmailTemplate(string clientCode, string name, string description, string subject,
        string sender, string emailContent)
        {
            var df = new DatabaseProxy(Proc.CreateEmailTemplate.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.CreateEmailTemplate.Params.Description, description, StringType.VarChar);
            df.AddParameter(Proc.CreateEmailTemplate.Params.EmailContent, emailContent, StringType.VarChar);
            df.AddParameter(Proc.CreateEmailTemplate.Params.ClientCode, clientCode, StringType.VarChar);
            df.AddParameter(Proc.CreateEmailTemplate.Params.Sender, sender, StringType.VarChar);
            df.AddParameter(Proc.CreateEmailTemplate.Params.Subject, subject, StringType.VarChar);
            df.AddParameter(Proc.CreateEmailTemplate.Params.TemplateName, name, StringType.VarChar);
            df.ExecuteQuery();

        }

        public static DataTable SearchEmailTemplateByEntity(string clientCode)
        {
            var df = new DatabaseProxy(Proc.EmailTemplateSearch.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.EmailTemplateSearch.Params.ClientCode, clientCode, StringType.VarChar);

            return df.ExecuteQuery().Tables[0];
        }

        public static DataTable EmailBroadcastInsert(Guid broadcastId, string templateName, string entityCode, string applicationName, string broadcastType)
        {
            var df = new DatabaseProxy(Proc.EmailBroadcastInsert.Name, ConfigSection, ConfigKey);

            df.AddParameter(Proc.EmailBroadcastInsert.Params.BroadcastId, broadcastId);
            df.AddParameter(Proc.EmailBroadcastInsert.Params.EntityCode, entityCode, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastInsert.Params.TemplateName, templateName, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastInsert.Params.BroadcastType, broadcastType, StringType.VarChar);
            if (applicationName.HasValue())
            {
                df.AddParameter(Proc.EmailBroadcastInsert.Params.ApplicationName, applicationName, StringType.VarChar);
            }

            return df.ExecuteQuery().Tables[0];
        }

        public static void CreateEmailTrack(
            string emailBroadcastEntry,
            string page,
            string ipaddress,
            string browser,
            string country,
            string region,
            string city,
            string postcode,
            string latitude,
            string longitude,
            string timeZone
            )
        {
            var df = new DatabaseProxy(Proc.EmailTrackerInsert.Name, ConfigSection, ConfigKey);

            df.AddParameter(Proc.EmailTrackerInsert.Params.Browser, browser, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.City, city, StringType.VarChar);
            //df.AddParameter(Proc.EmailTrackerInsert.Params.DateTime, dateTime);
            df.AddParameter(Proc.EmailTrackerInsert.Params.EmailBroadcastEntry, emailBroadcastEntry, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.IpAddress, ipaddress, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Latitude, latitude, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Longitude, longitude, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Page, page, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Postcode, postcode, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Region, region, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.TimeZone, timeZone, StringType.VarChar);
            df.AddParameter(Proc.EmailTrackerInsert.Params.Country, country, StringType.VarChar);

            df.ExecuteQuery();
        }

        public static void EmailBroadcastEntryProcess(int emailBroadcastEntryId, int retry, DateTime? sentDateTime)
        {
            var df = new DatabaseProxy(Proc.EmailBroadcastEntryProcess.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.EmailBroadcastEntryProcess.Params.EmailBroadcastEntryId, emailBroadcastEntryId);
            df.AddParameter(Proc.EmailBroadcastEntryProcess.Params.RetryNo, retry);
            if (sentDateTime.HasValue)
            {
                df.AddParameter(Proc.EmailBroadcastEntryProcess.Params.SentDateTime, sentDateTime.Value);
            }
            df.ExecuteQuery();
        }
        public static DataTable EmailBroadcastEntryInsert(Guid broadcastId,
                                                          string email,
                                                          string emailContent,
                                                          string subject,
                                                          string sender,
                                                          bool isHtml,
                                                          int? priority)
        {
            var df = new DatabaseProxy(Proc.EmailBroadcastEntryInsert.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.BroadcastId, broadcastId);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.Email, email, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.EmailContent, emailContent, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.Subject, subject, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.Sender, sender, StringType.VarChar);
            df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.IsBodyHtml, isHtml);
            if (priority.HasValue)
            {
                df.AddParameter(Proc.EmailBroadcastEntryInsert.Params.Priority, priority.Value);
            }

            return df.ExecuteQuery().Tables[0];
        }

        public static EmailTemplateRow GetEmailTemplateSelectByName(string templateName)
        {
            var df = new DatabaseProxy(Proc.EmailTemplateSelectByName.Name, ConfigSection, ConfigKey);
            df.AddParameter(Proc.EmailTemplateSelectByName.Params.TemplateName, templateName, StringType.VarChar);

            return new EmailTemplateRow(df.ExecuteQuery().Tables[0].Rows[0]);
        }

        public static DataTable GetUnsentEmailBroadcastEntry()
        {
            var df = new DatabaseProxy(Proc.GetUnsentEmailBroadcastEntry.Name, ConfigSection, ConfigKey);
            return df.ExecuteQuery().Tables[0];
        }

        public static DataTable GetUnsentEmailBroadcastEntry(Guid? broadcastId)
        {
            var df = new DatabaseProxy(Proc.GetUnsentEmailBroadcastEntryById.Name, ConfigSection, ConfigKey);
            if (broadcastId.HasValue)
            {
                df.AddParameter(Proc.GetUnsentEmailBroadcastEntryById.Params.BroadcastId, broadcastId.Value);
            }

            return df.ExecuteQuery().Tables[0];
        }

        public static IEnumerable<BroadcastActivityRow> GetBroadcastActivities(DateTime date)
        {
            var df = new DatabaseProxy("bst_Broadcast_Activity", ConfigSection, ConfigKey);
            df.AddParameter("@ReportDate", date);
            return from DataRow row in df.ExecuteQuery().Tables[0].Rows 
                   select new BroadcastActivityRow(row) ;
        }
    }
}
