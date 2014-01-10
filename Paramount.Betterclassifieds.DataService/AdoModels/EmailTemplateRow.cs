namespace Paramount.Betterclassifieds.DataService
{
    using System.Data;

    public class EmailTemplateRow
    {
        private readonly DataRow _row;
        public EmailTemplateRow(DataRow dataRow)
        {
            _row = dataRow;
        }

        public string TemplateName
        {
            get
            {
                return (string)_row["Name"];
            }
        }

        public string Description
        {
            get
            {
                return (string)_row["Description"];
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

        public string EmailContent
        {
            get
            {
                return (string)_row["EmailContent"];
            }
        }

        public string EntityCode
        {
            get { return (string)_row["EntityCode"]; }
        }

        public int EmailTemplateId { get { return (int)_row["EmailTemplateId"]; } }

    }
}
