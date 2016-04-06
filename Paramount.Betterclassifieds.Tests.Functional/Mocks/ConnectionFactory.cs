﻿using System.Data;
using System.Data.SqlClient;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal class ConnectionFactory
    {
        private readonly IConfig _config;

        public ConnectionFactory(IConfig config)
        {
            _config = config;
        }

        public IDbConnection CreateClassifieds()
        {
            return new SqlConnection(_config.ClassifiedsDbConnection);
        }

        public IDbConnection CreateBroadcast()
        {
            return new SqlConnection(_config.BroadcastDbConnection);
        }

        public IDbConnection CreateMembership()
        {
            return new SqlConnection(_config.AppUserDbConnection);
        }
    }
}
