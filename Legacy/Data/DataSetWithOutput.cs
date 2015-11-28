namespace Paramount.ApplicationBlock.Data
{
    using System.Collections.Generic;
    using System.Data;

    public class DataSetWithOutput
    {
        public DataSetWithOutput(DataSet dataSet, Dictionary<string, object> output)
        {
            DataSet = dataSet;
            Output = output;
        }

        public Dictionary<string, object> Output
        {
            get;
            private set;
        }

        public DataSet DataSet
        {
            get;
            private set;
        }
    }
}
