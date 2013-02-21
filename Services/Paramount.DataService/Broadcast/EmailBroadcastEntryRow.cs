namespace Paramount.DataService.Broadcast
{
    using System;
    using System.Data;

    public class EmailBroadcastEntryRow
    {
        private readonly DataRow _row;
        public EmailBroadcastEntryRow(DataRow dataRow)
        {
            _row = dataRow;
        }

        public int EmailBroadcastEntryId { get { return (int)_row["EmailBroadcastEntryId"]; } }

        public Guid BroadcastId
        {
            get
            {
                return (Guid)_row["BroadcastId"];
            }
        }

        public string Email
        {
            get
            {
                return (string)_row["Email"];
            }
        }

        public string Body
        {
            get
            {
                return (string)_row["EmailContent"];
            }
        }

        public string Subject
        {
            get
            {
                return (string)_row["Subject"];
            }
        }

        public string Sender
        {
            get
            {
                return (string)_row["Sender"];
            }
        }

        public bool IsBodyHtml
        {
            get
            {
                return (bool)_row["IsBodyHtml"];
            }
        }

        public int Priority
        {
            get
            {
                return (int)_row["Priority"];
            }
        }
    }
}
