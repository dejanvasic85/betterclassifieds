using System.Data;

namespace Paramount.Betterclassifieds.DataService
{
    public class BroadcastActivityRow
    {
        private readonly DataRow row;

        public BroadcastActivityRow(DataRow row)
        {
            this.row = row;
        }

        public int NumberOfEmailSent
        {
            get { return row.Field<int>("NumberOfEmailsSent"); }
        }
    }
}