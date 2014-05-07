﻿using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Betterclassifieds.DataService.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public class DataContextFactory
    {
        private const string SectionName = "paramount/services";

        public static ClassifiedsDataContext CreateClassifiedContext()
        {
            var connection = ConfigReader.GetConnectionString(SectionName, "BetterclassifiedsConnection");
            return new ClassifiedsDataContext(connection);
        }

        public static ClassifiedsSearchEntitiesDataContext CreateClassifiedSearchContext()
        {
            var connection = ConfigReader.GetConnectionString(SectionName, "BetterclassifiedsConnection");
            return new ClassifiedsSearchEntitiesDataContext(connection);
        }

        public static UserMembershipDataContext CreateMembershipContext()
        {
            var connection = ConfigReader.GetConnectionString(SectionName, "AppUserConnection");
            return new UserMembershipDataContext(connection);
        }
    }
}
