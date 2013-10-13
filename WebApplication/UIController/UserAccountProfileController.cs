//namespace Paramount.Common.UIController
//{
//    using System;
//    using ApplicationBlock.Configuration;
//    using ApplicationBlock.Logging.AuditLogging;
//    using ApplicationBlock.Logging.Constants;
//    using Common.DataTransferObjects.UserAccountWebService.Messages;
//    using Services.Proxy;
//    using Utility;

//    public static class UserAccountProfileController
//    {
//        public static PagedSource GetNewsletterUsers(string usernameMatch, bool includeAllUsers, int pageIndex, int pageSize)
//        {
//            var request = new GetNewsletterUsersRequest { UsernameMatch = usernameMatch, IncludeAll = includeAllUsers, PageIndex = pageIndex, PageSize = pageSize };

//            var grouping = Guid.NewGuid().ToString();
//            AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(request), SecondaryData = grouping, TransactionName = TransactionNames.GetNewsletterUsersRequest });

//            var response = WebServiceHostManager.AccountWebServiceHost.GetNewsletterUsers(request);
           
//            AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(response), SecondaryData = grouping, TransactionName = TransactionNames.GetNewsletterUsersResponse });
//            return new PagedSource { Datasource = Converter.FromContractCollection(response.Profiles), TotalPageSize = response.TotalCount };
//        }

//        public static void UnSubscribeUser(string userName)
//        {
//            var groupingId = Guid.NewGuid().ToString();

//            var request = new UnSubscribeUserRequest
//            {
//                UserName = userName,
//                ClientCode = ConfigSettingReader.ClientCode,
//                ApplicationName = ConfigSettingReader.ApplicationName
//            };
//            AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(request), SecondaryData = groupingId, TransactionName = TransactionNames.UnSubscribeRequest });
//            WebServiceHostManager.AccountWebServiceHost.UnsubscribUser(request);
//        }
//    }
//}
