namespace Paramount.ApplicationBlock.Logging.DataAccess
{
    public struct Proc
    {
        public struct LogInsert
        {
            public const string Name = @"psp_LogInsert";

            public struct Params
            {
                public const string LogId = "LogId";
                public const string AccountId = "AccountId";
                public const string ApplicationName = "Application";
                public const string Category = "Category";
                public const string Data1 = "Data1";
                public const string Data2 = "Data2";
                public const string DateTimeCreated = "DateTimeCreated";
                public const string Domain = "Domain";
                public const string TransactionName = "TransactionName";
                public const string User = "User";
                public const string IPAddress = "IPAddress";
                public const string HostName = "ComputerName";
                public const string SessionId = "SessionId";
            }
        }
    }
}
