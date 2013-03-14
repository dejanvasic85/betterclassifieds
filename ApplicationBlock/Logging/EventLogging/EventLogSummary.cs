using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paramount.ApplicationBlock.Logging
{
    public class EventLogSummary
    {
        private readonly DataRow dataRow;

        public EventLogSummary(DataRow dataRow)
        {
            this.dataRow = dataRow;
        }
        public int Occurrances
        {
            get { return dataRow.Field<int>("Occurrances"); }
        }

        public string ApplicationName
        {
            get { return dataRow.Field<string>("Application"); }
        }

        public string ErrorMessage
        {
            get { return dataRow.Field<string>("ErrorMessage"); }
        }
    }
}
