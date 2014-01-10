namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Data;

    public class EmailBroadcastRow
    {
        private readonly DataRow _row;
        public EmailBroadcastRow(DataRow dataRow)
        {
            _row = dataRow;
        }
        public Guid? BroadcastId
        {
            get
            {
                return _row["BroadcastId"] as Guid?;
            }
        }

        public int EmailBroadcastId
        {
            get
            {
                return (int)_row["EmailBroadcastId"];
            }
        }
    }
}
