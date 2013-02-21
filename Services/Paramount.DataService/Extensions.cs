using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Common;
using Paramount.Common.DataTransferObjects.LocationService;
using Paramount.DataService.LinqObjects;
namespace Paramount.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Common.DataTransferObjects.Betterclassifieds;
    using Common.DataTransferObjects.DSL;

    public static class Extensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return pageIndex == -1 || pageSize == 0 ? query : query.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static PagedSource<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize, int totalCount)
        {
            return pageIndex == -1 || pageSize == 0 ? new PagedSource<T> { Data = query, TotalCount = totalCount } : new PagedSource<T> { Data = query.Skip(pageIndex * pageSize).Take(pageSize), TotalCount = totalCount };
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        #region DslDocument

        public static DslDocument ToDslDocument(this DataRow row)
        {
            var document = new DslDocument();

            document.DocumentId = (Guid)row["DocumentId"];
            document.ApplicationCode = row["ApplicationCode"].ToString();
            document.EntityCode = row["EntityCode"].ToString();
            document.AccounId = (row["AccountId"].ToString().HasValue() ? (Guid)row["AccountId"] : new Guid?());
            document.DocumentCategory = (DslDocumentCategoryType)row["DocumentCategoryId"];
            document.Username = row["Username"].ToString();
            document.FileType = row["FileType"].ToString();
            document.FileLength = (int)row["FileLength"];
            document.FileName = row["FileName"].ToString();
            document.Reference = row["Reference"].ToString();
            document.IsPrivate = (bool)row["IsPrivate"];
            document.StartDate = (row["StartDate"].ToString().HasValue() ? (DateTime)row["StartDate"] : new DateTime?());
            document.EndDate = (row["EndDate"].ToString().HasValue() ? (DateTime)row["EndDate"] : new DateTime?());
            document.CreatedDate = (row["CreatedDate"].ToString().HasValue() ? (DateTime)row["CreatedDate"] : new DateTime?());
            document.UpdatedDate = (row["UpdatedDate"].ToString().HasValue() ? (DateTime)row["UpdatedDate"] : new DateTime?());

            return document;
        }

        public static DslDocumentCategory ToDslDocumentCategory(this DataRow row)
        {
            var documentCategory = new DslDocumentCategory();
            documentCategory.DocumentCategoryId = (int)row["DocumentCategoryId"];
            documentCategory.Title = row["Title"].ToString();
            documentCategory.Code = (DslDocumentCategoryType)row["Code"];
            documentCategory.ExpiryPurgeDays = (row["ExpiryPurgeDays"].ToString().HasValue() ? (int?)row["ExpiryPurgeDays"] : null);

            // extract the accepted file types
            string[] acceptedFiles = row["AcceptedFileTypes"].ToString().Split(';');
            documentCategory.AcceptedFileTypes = new List<string>();
            foreach (string file in acceptedFiles)
            {
                documentCategory.AcceptedFileTypes.Add(file.ToLower());
            }

            documentCategory.MaximumFileSize = (row["MaximumFileSize"].ToString().HasValue() ? (int?)row["MaximumFileSize"] : null);
            return documentCategory;
        }

        #endregion

        public static BannerGroupEntity Convert(this BannerGroup bannerGroup)
        {
            return new BannerGroupEntity
                       {
                           AcceptedFileType = bannerGroup.AcceptedFileType,
                           Name = bannerGroup.Title,
                           GroupId = bannerGroup.BannerGroupId,
                           Height = bannerGroup.Height,
                           Width = bannerGroup.Width,
                           UseTimer = (bool)bannerGroup.IsTimerEnabled,
                           IsActive = bannerGroup.IsActive,

                       };
        }

        public static BannerGroup Convert(this BannerGroupEntity bannerGroup)
        {
            return new BannerGroup
            {
                AcceptedFileType = bannerGroup.AcceptedFileType,
                Title = bannerGroup.Name,
                BannerGroupId = bannerGroup.GroupId,
                Height = bannerGroup.Height,
                Width = bannerGroup.Width,
                IsTimerEnabled = (bool)bannerGroup.UseTimer,
                IsActive = bannerGroup.IsActive
            };
        }

        public static IEnumerable<BannerEntity> Convert(this IQueryable<Banner> banners)
        {
            return banners.ToList().Select(a => a.Convert());
        }

        public static IEnumerable<BannerGroupEntity> Convert(this IQueryable<BannerGroup> bannerGroups)
        {
            return bannerGroups.ToList().Select(a => a.Convert());
        }

        public static BannerAudit Convert(this BannerAuditEntity audit)
        {
            return new BannerAudit
                       {
                           ActionTypeName = audit.ActionTypeName,
                           ApplicationName = audit.ApplicationName,
                           BannerId = new Guid(audit.BannerId),
                           ClientCode = audit.ClientCode,
                           Gender = audit.Gender,
                           CreatedDateTime = DateTime.Now,
                           IPAddress = audit.IpAddress,
                           Location = audit.Location,
                           PageUrl = audit.Pageurl,
                           PostCode = audit.Postcode,
                           Username = audit.UserId,
                           UserGroup = audit.UserGroup
                       };
        }

        public static BannerEntity Convert(this Banner banner)
        {
            var t = new BannerEntity
                       {
                           BannerId = banner.BannerId,
                           Title = banner.Title,
                           EndDate = banner.EndDateTime,
                           StartDate = banner.StartDateTime,
                           GroupId = banner.BannerGroupId,
                           ImageId = banner.DocumentID,
                           Description = string.Format("{0} - {1}", banner.ClientCode, banner.Title),
                           NavigateUrl = banner.NavigateUrl,
                           RequestCount = banner.RequestCount,
                           IsDeleted = banner.IsDeleted,
                           CreatedBy = banner.CreatedBy,
                           ClickCount = banner.ClickCount,
                           ClientCode = banner.ClientCode
                       };

            foreach (var attribute in
                banner.BannerReferences.Select(item => new CodeDescription { Description = item.BannerReferenceType.Title, Code = item.Value }))
            {
                t.Attributes.Add(attribute);
            }

            return t;

        }

        public static Banner Convert(this BannerEntity banner)
        {
            var t = new Banner
            {
                BannerId = banner.BannerId,
                Title = banner.Title,
                EndDateTime = banner.EndDate,
                StartDateTime = banner.StartDate,
                BannerGroupId = banner.GroupId,
                DocumentID = banner.ImageId,
                NavigateUrl = banner.NavigateUrl,
                RequestCount = banner.RequestCount,
                IsDeleted = banner.IsDeleted,
                CreatedBy = banner.CreatedBy,
                ClickCount = banner.ClickCount,
                ClientCode = banner.ClientCode
            };

            foreach (var b in banner.Attributes.Select(item => new BannerReference
                                                                   {
                                                                       Value = item.Code,
                                                                       BannerReferenceType = new BannerReferenceType { Title = item.Description }
                                                                   }))
            {
                t.BannerReferences.Add(b);
            }

            return t;
        }

        public static Collection<CodeDescription> GetAttributes(this EntitySet<BannerReference> bannerReferences)
        {
            var attributes = new Collection<CodeDescription>();
            foreach (var attribute in
                           bannerReferences.Select(item => new CodeDescription { Description = item.BannerReferenceType.Title, Code = item.Value }))
            {
                attributes.Add(attribute);
            }

            return attributes;
        }

        public static BannerReference GetAttributes(this CodeDescription attribute)
        {

            return new BannerReference
                        {
                            Value = attribute.Code,
                            BannerReferenceType = new BannerReferenceType { Title = attribute.Description }
                        };
        }

        public static CodeDescription GetAttributes(this BannerReference attribute)
        {

            return new CodeDescription
            {
                Code = attribute.Value,
                Description = attribute.BannerReferenceType.Title
            };
        }

        #region Billing

        public static BillingBankEntity Convert(this Billing_Bank bank)
        {
            return new BillingBankEntity()
                       {
                           AllowOverride = bank.AllowOverride,
                           BankId = bank.BankId,
                           BankName = bank.BankName,
                           Description = bank.Description,
                           GatewayUrl = bank.GatewayUrl,
                           GSTRate = bank.GSTRate,
                           ReturnLinkText = bank.ReturnLinkText,
                       };
        }

        public static Billing_Bank Convert(this BillingBankEntity bank)
        {
            return new Billing_Bank()
            {
                AllowOverride = bank.AllowOverride,
                BankId = bank.BankId,
                BankName = bank.BankName,
                Description = bank.Description,
                GatewayUrl = bank.GatewayUrl,
                GSTRate = bank.GSTRate,
                ReturnLinkText = bank.ReturnLinkText,
                VendorCode = bank.VendorCode
            };
        }

        public static BillingSettingsEntity Convert(this BillingSetting setting)
        {
            if ( setting == null) return null;

            return new BillingSettingsEntity()
                       {
                           BankId = setting.BankId,
                           BankName = setting.BankName,
                           ClientCode = setting.ClientCode,
                           CollectAddressDetails = setting.CollectAddressDetails,
                           Description = setting.Description,
                           GatewayUrl = setting.GatewayUrl,
                           GSTRate = setting.GSTRate,
                           InvoiceBannerImageId = setting.InvoiceBannerImageId,
                           ReferencePrefix = setting.ReferencePrefix,
                           RefundPolicyUrl = setting.RefundPolicyUrl,
                           ReturnLinkText = setting.ReturnLinkText,

                           VendorCode = setting.VendorCode,
                           PaypalBusinessEmail = setting.PP_BusinessEmail,
                           PaypalCurrencyCode = setting.PP_CurrencyCode,
                           PaymentAlertEmail = setting.AlertEmail
                       };
        }

        public static BillingSetting Convert(this BillingSettingsEntity setting)
        {
            return new BillingSetting()
            {
                BankId = setting.BankId,
                BankName = setting.BankName,
                ClientCode = setting.ClientCode,
                CollectAddressDetails = setting.CollectAddressDetails,
                Description = setting.Description,
                GatewayUrl = setting.GatewayUrl,
                GSTRate = setting.GSTRate,
                InvoiceBannerImageId = setting.InvoiceBannerImageId,
                PP_BusinessEmail = setting.PaypalBusinessEmail,
                PP_CurrencyCode = setting.PaypalCurrencyCode,
                ReferencePrefix = setting.ReferencePrefix,
                ReturnLinkText = setting.ReturnLinkText,
                AlertEmail = setting.PaymentAlertEmail
                
            };
        }

        public static CurrencyEntity Convert(this Currency currency)
        {
            return new CurrencyEntity()
                       {
                           CurrencyId = currency.CurrencyId,
                           CurrencyName = currency.CurrencyName,
                           CurrencyCode = currency.CurrencyCode
                       };
        }

        public static Currency Convert(this CurrencyEntity currencyEntity)
        {
            return new Currency()
            {
                CurrencyId = currencyEntity.CurrencyId,
                CurrencyName = currencyEntity.CurrencyName,
                CurrencyCode = currencyEntity.CurrencyCode
            };
        }

        public static ShoppingCartEntity Convert(this ShoppingCart cart)
        {
            return new ShoppingCartEntity
            {
                ClientCode = cart.ClientCode,
                DateTimeCreated = cart.DateTimeCreated,
                DateTimeModified = cart.DateTimeModified,
                SessionId = cart.SessionId,
                ShoppingCartId = cart.ShoppingCartId,
                Status = cart.Status,
                UserId = cart.UserId,
                Title = cart.Title,
                TotalAmount = cart.TotalAmount,
                GstIncluded = cart.GstIncluded

            };
        }

        public static ShoppingCartItemEntity Convert(this ShoppingCartItem cartItem)
        {
            return new ShoppingCartItemEntity()
            {
                Price = cartItem.Price,
                ReferenceId = cartItem.ReferenceId    ,
                ProductType = cartItem.ProductType,
                Quantity = cartItem.Quantity,
                ShoppingCartItemId = cartItem.ShoppingCartItemId,
                Summary = cartItem.Summary,
                Title = cartItem.Title,
                ShoppingCartId = cartItem.ShoppingCartId
            };
        }

        public static ShoppingCart Convert(this ShoppingCartEntity cart)
        {
            return new ShoppingCart
            {
                ClientCode = cart.ClientCode,
                DateTimeCreated = cart.DateTimeCreated,
                DateTimeModified = cart.DateTimeModified,
                SessionId = cart.SessionId,
                ShoppingCartId = cart.ShoppingCartId,
                Status = cart.Status,
                UserId = cart.UserId,
                Title = cart.Title,
                TotalAmount = cart.TotalAmount,
                GstIncluded = cart.GstIncluded


            };
        }

        public static Invoice ToInvoice(this ShoppingCartEntity cart)
        {
            return new Invoice()
            {
                ClientCode = cart.ClientCode,
                SessionId = cart.SessionId,
                UserId = cart.UserId,
                Title = cart.Title,
                GstIncluded = cart.GstIncluded,
            };
        }

        public static InvoiceItem ToInvoiceItem(this ShoppingCartItemEntity cartItem, Guid invoiceId)
        {
            return new InvoiceItem()
            {
                Price = cartItem.Price,
                InvoiceId = invoiceId,
                InvoiceItemId = Guid.NewGuid(),
                Quantity = cartItem.Quantity,
                Summary = cartItem.Summary,
                Title = cartItem.Title,
                ReferenceId = cartItem.ReferenceId,
                ProductType = cartItem.ProductType,
            };
        }

        public static ShoppingCartItem Convert(this ShoppingCartItemEntity cartItem)
        {
            return new ShoppingCartItem()
            {
                Price = cartItem.Price,
                ReferenceId =   cartItem.ReferenceId,
                ProductType = cartItem.ProductType,
                Quantity = cartItem.Quantity,
                ShoppingCartItemId = cartItem.ShoppingCartItemId,
                Summary = cartItem.Summary,
                Title = cartItem.Title,
                ShoppingCartId = cartItem.ShoppingCartId
            };
        }
        
        public static InvoiceEntity Convert(this Invoice invoice)
        {
            return new InvoiceEntity
            {
                Title = invoice.Title,
                TotalAmount = invoice.TotalAmount,
                GstIncluded = invoice.GstIncluded,
                ClientCode = invoice.ClientCode,
                DateTimeCreated = invoice.DateTimeCreated,
                DateTimeUpdated = invoice.DateTimeUpdated,
                SessionId = invoice.SessionId,
                InvoiceId = invoice.InvoiceId,
                Status = invoice.Status,
                UserId = invoice.UserId,
                InvoiceNumber = invoice.InvoiceNumber,

              
                BillingAddress = new AddressDetails()
                                     {
                                         Address1 = invoice.BillingAddress1,
                                         Address2 = invoice.BillingAddress2,
                                         Country = invoice.BillingCountry,
                                         Name = invoice.BillingName,
                                         Postcode = invoice.BillingPostcode,
                                         State = invoice.BillingState
                                     },
                DeliveryAddress = new AddressDetails()
                {
                    Address1 = invoice.DeliveryAddress1,
                    Address2 = invoice.DeliveryAddress2,
                    Country = invoice.DeliveryCountry,
                    Name = invoice.DeliveryName,
                    Postcode = invoice.DeliveryPostcode,
                    State = invoice.DeliveryState
                }
            };
        }

        public static InvoiceItemEntity Convert(this InvoiceItem cartItem)
        {
            return new InvoiceItemEntity()
            {
                Price = cartItem.Price,
                InvoiceId = cartItem.InvoiceId,
                InvoiceItemId = cartItem.InvoiceItemId,
                Quantity = cartItem.Quantity,
                Summary = cartItem.Summary,
                Title = cartItem.Title,
                ReferenceId = cartItem.ReferenceId,
                ProductType = cartItem.ProductType
            };
        }

        public static Invoice Convert(this InvoiceEntity invoice)
        {
            return new Invoice
            {
                Title = invoice.Title,
                TotalAmount = invoice.TotalAmount,
                GstIncluded = invoice.GstIncluded,
                ClientCode = invoice.ClientCode,
                DateTimeCreated = invoice.DateTimeCreated,
                DateTimeUpdated = invoice.DateTimeUpdated,
                SessionId = invoice.SessionId,
                InvoiceId = invoice.InvoiceId,
                Status = invoice.Status,
                UserId = invoice.UserId,
            }.FillBillingAddress(invoice.BillingAddress).FillDeliveryAddress(invoice.DeliveryAddress);
        }

        public static Invoice FillBillingAddress(this Invoice invoice, AddressDetails addressDetails)
        {
              invoice.BillingAddress1 = addressDetails.Address1;
              invoice.BillingAddress2 = addressDetails.Address2;
              invoice.BillingCountry = addressDetails.Country;
              invoice.BillingName = addressDetails.Name;
              invoice.BillingPostcode = addressDetails.Postcode;
              invoice.BillingState = addressDetails.State;
              invoice.BillingCity = addressDetails.City;

            return invoice;

        }

        public static Invoice FillDeliveryAddress(this Invoice invoice, AddressDetails addressDetails)
        {
            invoice.DeliveryAddress1 = addressDetails.Address1;
            invoice.DeliveryAddress2 = addressDetails.Address2;
            invoice.DeliveryCountry = addressDetails.Country;
            invoice.DeliveryName = addressDetails.Name;
            invoice.DeliveryPostcode = addressDetails.Postcode;
            invoice.DeliveryState = addressDetails.State;
            invoice.DeliveryCity = addressDetails.City;

            return invoice;

        }

        public static InvoiceItem Convert(this InvoiceItemEntity cartItem)
        {
            return new InvoiceItem()
            {
                Price = cartItem.Price,
                Quantity = cartItem.Quantity,
                InvoiceItemId = cartItem.InvoiceItemId,
                Summary = cartItem.Summary,
                Title = cartItem.Title,
                InvoiceId = cartItem.InvoiceId,
                ReferenceId = cartItem.ReferenceId,
                ProductType = cartItem.ProductType
            };
        }

        #endregion

        #region Betterclassfieds
        public static ExpiredAdEntity Convert(this ExpiredAdRow expiredAdRow)
            {
            return new ExpiredAdEntity()
                       {
                           AdBookingId = expiredAdRow.AdBookingId,
                           AdId = expiredAdRow.AdDesignId,
                           BookingDate = expiredAdRow.BookingDate,
                           BookingEndDate = expiredAdRow.EndDate,
                           BookingReference = expiredAdRow.BookingReference,
                           BookingStartDate = expiredAdRow.StartDate,
                           LastPrintInsertionDate = expiredAdRow.LastEditionDate,
                           MainCategoryId = expiredAdRow.MainCategory,
                           TotalPrice = expiredAdRow.TotalPrice,
                           Username = expiredAdRow.Username
                       };
            }
        #endregion
        #region Common


        public static CountryEntity Convert(this Country country   )
        {
            return new CountryEntity()
                       {
                           Code = country.Code,
                           Description = country.Description,
                           Name = country.Name
                       };
        }


        public static  StateEntity Convert(this State state)
        {
            return new StateEntity()
            {
                Code = state.StateCode,
                Description = state.Description,
                Name = state.Title,
                CountryCode = state.CountryCode
            };
        }

        #endregion
    }
}
