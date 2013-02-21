using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.DataService
{
    internal struct Proc
    {
        public struct GetLineAdBookingByLastEdition
        {
            public const string Name = "psp_Betterclassified_GetLineAdBookingByLastEdition";

            public struct Params
            {
                public const string EditionDate = "@EditionDate";
            }
        }

        public struct GetNextBanner
        {
            public const string Name = "brn_GetNextBanner";

            public struct Params
            {
                public const string Attributes = "Params";
                public const string BannerGroupId = "BannerGroupId";
                public const string ClientCode = "ClientCode";
            }
        }

        public struct UpdateEmailTemplate
        {
            public const string Name = "bst_EmailTemplateUpdate";

            public struct Params
            {
                public const string TemplateName = "Name";
                public const string Description = "Description";
                public const string EmailContent = "EmailContent";
                public const string Subject = "Subject";
                public const string Sender = "Sender";
                public const string ClientCode = "EntityCode";
            }
        }

        public struct CreateEmailTemplate
        {
            public const string Name = "bst_EmailTemplateInsert";

            public struct Params
            {
                public const string TemplateName = "Name";
                public const string Description = "Description";
                public const string EmailContent = "EmailContent";
                public const string Subject = "Subject";
                public const string Sender = "Sender";
                public const string ClientCode = "EntityCode";
            }
        }

        public struct EmailTemplateSearch
        {
            public const string Name = "bst_EmailTemplateSearch";

            public struct Params
            {
                public const string ClientCode = "EntityCode";
            }
        }
        public struct EmailBroadcastEntryProcess
        {
            public const string Name = "bst_EmailBroadcastEntryProcess";

            public struct Params
            {
                public const string EmailBroadcastEntryId = "EmailBroadcastEntryId";
                public const string LastRetryDateTime = "LastRetryDateTime";
                public const string SentDateTime = "SentDateTime";
                public const string RetryNo = "RetryNo";
            }
        }

        public struct EmailTrackerInsert
        {
            public const string Name = "bst_EmailTrackerInsert";

            public struct Params
            {
                public const string EmailBroadcastEntry = "EmailBroadcastEntryId";
                public const string Page = "Page";
                public const string IpAddress = "IpAddress";
                public const string Browser = "Browser";
                public const string DateTime = "DateTime";
                public const string Region = "Region";
                public const string City = "City";
                public const string Country = "Country";
                public const string Postcode = "Postcode";
                public const string Latitude = "Latitude ";
                public const string Longitude = "Longitude";
                public const string TimeZone = "@TimeZone";
            }
        }

        public struct GetUnsentEmailBroadcastEntry
        {
            public const string Name = "bst_GetUnsentEmailBroadcastEntry";
        }

        public struct GetUnsentEmailBroadcastEntryById
        {
            public const string Name = "bst_GetUnsentEmailBroadcastEntryById";
            public struct Params
            {
                public const string BroadcastId = "BroadcastId";
            }
        }

        public struct EmailBroadcastInsert
        {
            public const string Name = "bst_EmailBroadcastInsert";

            public struct Params
            {
                public const string TemplateName = "TemplateName";
                public const string BroadcastId = "BroadcastId";
                public const string EntityCode = "EntityCode";
                public const string ApplicationName = "ApplicationName";
                public const string BroadcastType = "Type";
            }
        }
        public struct EmailTemplateSelectByName
        {
            public const string Name = "bst_EmailTemplateSelectByName";
            public struct Params
            {
                public const string TemplateName = "TemplateName";
            }
        }
        public struct EmailBroadcastEntryInsert
        {
            public const string Name = "bst_EmailBroadcastEntryInsert";

            public struct Params
            {
                public const string Email = "Email";
                public const string EmailContent = "EmailContent";
                public const string LastRetryDateTime = "LastRetryDateTime";
                public const string SentDateTime = "SentDateTime";
                public const string RetryNo = "RetryNo";
                public const string Subject = "Subject";
                public const string Sender = "Sender";
                public const string IsBodyHtml = "IsBodyHtml";
                public const string Priority = "Priority";
                public const string BroadcastId = "BroadcastId";
            }
        }
        public struct ModuleSelectByEntity
        {
            public const string Name = "crm_ModuleSelectByEntity";
            public struct Params
            {
                public const string EntityCode = "EntityCode";
            }
        }

        public struct EntityModuleUpdate
        {
            public const string Name = "crm_EntityModuleUpdate";
            public struct Params
            {
                public const string StartDate = "StartDate";
                public const string EndDate = "EndDate";
                public const string EntityCode = "EntityCode";
                public const string Active = "Active";
                public const string ModuleId = "ModuleId";
                public const string EntityModuleId = "EntityModuleId";
            }
        }

        public struct EntityModuleInsert
        {
            public const string Name = "crm_EntityModuleInsert";
            public struct Params
            {
                public const string StartDate = "StartDate";
                public const string EndDate = "EndDate";
                public const string EntityCode = "EntityCode";
                public const string Active = "Active";
                public const string ModuleId = "ModuleId";
                public const string EntityModuleId = "EntityModuleId";
            }
        }

        public struct EntityUpdate
        {
            public const string Name = "crm_EntityUpdate";
            public struct Params
            {
                public const string EntityName = "EntityName";
                public const string EntityCode = "EntityCode";
                public const string PrimaryContactId = "PrimaryContactId";
                public const string TimeZone = "TimeZone";
                public const string Active = "Active";
            }
        }

        public struct ModuleSelect
        {
            public const string Name = "crm_ModuleSelect";
            public struct Params
            {
                public const string ModuleId = "ModuleId";
            }
        }

        public struct EventCategorySelect
        {
            public const string Name = "psp_EventCategorySelect";
            public struct Params
            {
                public const string EventCategoryId = "EventCategoryId";
            }
        }

        public struct EntityGetNewId
        {
            public const string Name = "crm_EntityGetNewId";
            public struct OutParams
            {
                public const string EntityCode = "EntityCode";
            }
        }

        public struct EntityInsert
        {
            public const string Name = "crm_EntityInsert";
            public struct Params
            {
                public const string EntityName = "EntityName";
                public const string EntityCode = "EntityCode";
                public const string PrimaryContactId = "PrimaryContactId";
                public const string TimeZone = "TimeZone";
                public const string Active = "Active";
            }
        }

        public struct EntitySelect
        {
            public const string Name = "crm_EntitySelect";
            public struct Params
            {
                public const string EntityCode = "EntityCode";
                public const string PageSize = "PageSize";
                public const string PageIndex = "PageIndex";
                public const string TotalPopulationSize = "TotalPopulationSize";
            }
        }

        public struct EventInsert
        {
            public const string Name = "psp_EventInsert";

            public struct Params
            {
                public const string EventId = "EventId";
                public const string EventTitle = "EventTitle";
                public const string Description = "Description";
                public const string ImagePath = "ImagePath";
                public const string CategoryId = "EventCategoryId";
            }
        }

        public struct EventScheduleInsert
        {
            public const string Name = "psp_EventScheduleInsert";

            public struct Params
            {
                public const string Comments = "Comments";
                public const string EventId = "EventId";
                public const string Location = "Location";
                public const string EventScheduleDateTime = "EventScheduleDateTime";
                public const string RegionId = "RegionId";
            }
        }

        public struct EventScheduleSeatingInsert
        {
            public const string Name = "psp_EventScheduleSeatingSelect";

            public struct Params
            {
                public const string AvaliableSeat = "AvaliableSeats";
                public const string Description = "Description";
                public const string EventScheduleId = "EventScheduleId";
                public const string Price = "Price";
                public const string Title = "Titlr";
            }

        }

        public struct CreateStandardEvent
        {
            public const string Name = "psp_CreateStandardEvent";

            public struct Params
            {
                public const string EventId = "EventId";
                public const string EventDescription = "EventDescription";
                public const string EventCategoryId = "EventCategoryId";
                public const string EventTitle = "EventTitle";
                public const string ImagePath = "ImagePath";
                public const string EventScheduleComments = "EventScheduleComments";
                public const string EventScheduleDateTime = "EventScheduleDateTime";
                public const string Location = "Location";
                public const string AvaliableSeat = "AvaliableSeats";
                public const string SeatingDescription = "SeatingDescription";
                //public const string EventScheduleId = "EventScheduleId";
                public const string SeatingPrice = "SeatingPrice";
                public const string SeatingTitle = "SeatingTitle";
            }
        }

        public struct EventCategoryInsert
        {
            public const string Name = "psp_EventCategoryInsert";

            public struct Params
            {
                public const string Title = "CategoryTitle";
                public const string Description = "CategoryDescription";

            }
        }

        public struct AccountInformationInsert
        {
            public const string Name = @"psp_AccountInformationInsert";

            public struct Params
            {
                public const string Domain = "domain";
                public const string AccountId = "AccountId";
                public const string Abn = "Abn";
                public const string AccountName = "AccountName";
                public const string Address = "Address";
                //private const string AllowDeferredPayment = "@AllowDeferredPayment";
                public const string BusinessType = "BusinessType";
                public const string Country = "Country";
                public const string DeferredPaymentEffectiveDate = "DeferredPaymentEffectiveDate";
                public const string DeferredPaymentExpiryDate = "DeferredPaymentExpiryDate";
                public const string MasterUserId = "MasterUserId";
                public const string Postcode = "Postcode";
                public const string State = "State";
            }
        }

        public struct LogInsert
        {
            public const string Name = @"psp_LogInsert";

            public struct Params
            {
                public const string LogId = "LogId";
                public const string AccountId = "AccountId";
                public const string ApplicationName = "Application";
                public const string Category = "Category";
                public const string CrudOperation = "CrudOperation";
                public const string ActionName = "ActionName";
                public const string Data1 = "Data1";
                public const string Data2 = "Data2";
                public const string DateTimeCreated = "DateTimeCreated";
                public const string DateTimeUtcCreated = "DateTimeUtcCreated";
                public const string Domain = "Domain";
                public const string TransactionName = "TransactionName";
                public const string User = "User";
                public const string IPAddress = "IPAddress";
                public const string HostName = "ComputerName";
                public const string SessionId = "SessionId";
                public const string Browser = "Browser";
            }
        }

        public struct RegionSelect
        {
            public const string Name = @"psp_RegionSelect";
            public struct Params
            {

            }
        }

        #region DocumentStorage

        public struct DocumentStorageInsert
        {
            public const string Name = @"dsl_DocumentStorageInsert";

            public struct Params
            {
                public const string DocumentId = "DocumentID";
                public const string ApplicationCode = "ApplicationCode";
                public const string AccountId = "AccountID";
                public const string CategoryCode = "CategoryCode";
                public const string EntityCode = "EntityCode";
                public const string Username = "Username";
                public const string FileData = "FileData";
                public const string FileLength = "FileLength";
                public const string FileType = "FileType";
                public const string FileName = "FileName";
                public const string NumberOfChunks = "NumberOfChunks";
                public const string Reference = "Reference";
                public const string IsPrivate = "IsPrivate";
                public const string StartDate = "StartDate";
                public const string EndDate = "EndDate";
            }
        }

        public struct DocumentStorageClearUpdate
        {
            public const string Name = @"dsl_DocumentStorageClearUpdate";
            public struct Params
            {
                public const string DocumentId = "DocumentID";
            }
        }

        public struct DocumentStorageUpdateChunks
        {
            public const string Name = @"dsl_DocumentStorageUpdateFileData";
            public struct Params
            {
                public const string EntityCode = "EntityCode";
                public const string DocumentId = "DocumentID";
                public const string FileData = "FileDataChunk";
                public const string CategoryCode = "CategoryCode";
                public const string Offset = "Offset";
            }
        }

        public struct DocumentStorageSelect
        {
            public const string Name = @"dsl_DocumentStorageSelect";
            public struct Param
            {
                public const string DocumentId = "DocumentID";
                public const string EntityCode = "EntityCode";
            }
        }

        public struct DocumentStorageSelectFileData
        {
            public const string Name = @"dsl_DocumentStorageSelectFileData";
            public struct Param
            {
                public const string DocumentId = "DocumentID";
            }
        }

        public struct DocumentCategorySelect
        {
            public const string Name = @"dsl_DocumentCategorySelectByCode";
            public struct Param
            {
                public const string CategoryCode = "CategoryCode";
            }
        }

        public struct DocumentStorageDelete
        {
            public const string Name = @"dsl_DocumentStorageDelete";
            public struct Param
            {
                public const string DocumentId = "DocumentID";
            }
        }

        #endregion
    }
}
