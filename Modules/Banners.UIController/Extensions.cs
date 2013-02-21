using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Banners.UIController.ViewObjects;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.Common.DataTransferObjects.Banner.Messages;

namespace Paramount.Banners.UIController
{
   public static class Extensions
    {
        public static List<BannerGroup> Convert(this GetBannerGroupResponse response)
        {
            var bannerGroups = new List<BannerGroup>();
            foreach (var group in response.Groups)
            {
                bannerGroups.Add(Convert(group));
            }
            return bannerGroups;
        }

        public static BannerGroup Convert(this BannerGroupEntity group)
        {
            return new BannerGroup
            {
                BannerHeight = group.Height,
                BannerWidth = group.Width,
                ClientCode = ConfigSettingReader.ClientCode,
                GroupId = group.GroupId,
                IncludeTimer = group.UseTimer,
                Title = group.Name
            };
        }

        public static BannerGroupEntity Convert(this BannerGroup bannerGroup)
        {
            return new BannerGroupEntity
            {
                Name = bannerGroup.Title,
                Height = bannerGroup.BannerHeight,
                Width = bannerGroup.BannerWidth,
                UseTimer = bannerGroup.IncludeTimer,
                IsActive = true
            };
        }

        public static List<Banner> Convert(this GetBannersResponse response)
        {
            var banners = new List<Banner>();
            foreach (BannerEntity bannerEntity in response.BannerCollection)
            {
                var banner = bannerEntity.Convert();
                banners.Add(banner);
            }

            return banners;
        }

        public static Banner Convert(this BannerEntity bannerEntity)
        {
            var banner = new Banner
            {
                BannerId = bannerEntity.BannerId,
                ClientCode = ConfigSettingReader.ClientCode,
                Start = bannerEntity.StartDate,
                End = bannerEntity.EndDate,
                ImageId = bannerEntity.ImageId.ToString(),
                Title = bannerEntity.Title,
                GroupId = bannerEntity.GroupId,
                Group = bannerEntity.GroupName,
                BannerTags = new NameValueCollection(),
                Url = bannerEntity.NavigateUrl
            };
            foreach (var attribute in bannerEntity.Attributes)
            {
                banner.BannerTags.Add(attribute.Code, attribute.Description);
            }
            return banner;
        }

        public static BannerEntity Convert(this Banner banner)
        {
            return new BannerEntity
            {
                BannerId = banner.BannerId,
                ClientCode = banner.ClientCode,
                Description = banner.AlternateText,
                EndDate = banner.End,
                StartDate = banner.Start,
                ImageId = new Guid(banner.ImageId),
                Title = banner.Title,
                NavigateUrl = banner.Url,
                CreatedBy = HttpContext.Current.User.Identity.Name,
                GroupId = banner.GroupId,
                IsDeleted = false,
                Attributes = Convert(banner.BannerTags)
            };
        }

        public static BannerModifyEntity ConvertModify(this Banner banner)
        {
            var bannerModifyEntity = new BannerModifyEntity
            {
                BannerId = banner.BannerId,
                ImageId = new Guid(banner.ImageId),
                Title = banner.Title,
                NavigateUrl = banner.Url,
                GroupId = banner.GroupId,
                Attributes = Convert(banner.BannerTags)
            };

            return bannerModifyEntity;
        }

        public static BannerRebookDetailEntity ConvertRebook(this Banner banner)
        {
            var bannerEntity = new BannerRebookDetailEntity
            {
                BannerId = banner.BannerId,
                ImageId = new Guid(banner.ImageId),
                Title = banner.Title,
                NavigateUrl = banner.Url,
                GroupId = banner.GroupId,
                Attributes = Convert(banner.BannerTags),
                StartDateTime = banner.Start,
                EndDateTime = banner.End
            };

            return bannerEntity;
        }


        public static Collection<Common.DataTransferObjects.CodeDescription> Convert(NameValueCollection bannerTags)
        {
            var attributes = new Collection<Common.DataTransferObjects.CodeDescription>();
            if (bannerTags == null)
            {
                return attributes;
            }
            
            foreach (var bannerTag in bannerTags.AllKeys)
            {
                attributes.Add(new Common.DataTransferObjects.CodeDescription { Code = bannerTag, Description = bannerTags[bannerTag] });
            }
            return attributes;
        }

        public static Collection<Common.DataTransferObjects.CodeDescription> Convert(this BannerParameters bannerParams)
        {
            var parameters = new Collection<Common.DataTransferObjects.CodeDescription>();
            foreach (var param in bannerParams.GetAllParams())
            {
                parameters.Add(new Common.DataTransferObjects.CodeDescription { Code = param.Code, Description = param.Description });
            }
            return parameters;
        }

        public static void SetBaseRequest(this BaseRequest request)
        {
            request.SetBaseRequest(Guid.NewGuid().ToString());
        }

       public static void SetBaseRequest(this BaseRequest request, string group)
        {
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;
            request.AuditData = new AuditData()
            {
                BrowserType = HttpContext.Current.Request.UserAgent + HttpContext.Current.Request.Browser,
                ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                GroupingId = group,
                HostName = HttpContext.Current.Request.UserHostName,
                SessionId = HttpContext.Current.Session.SessionID,
                Username = HttpContext.Current.User.Identity.Name
            };
        }

       public static BannerAuditEntity Convert(this BannerAudit audit)
       {
           return new BannerAuditEntity()
           {
               ActionTypeName = audit.ActionTypeName,
               ApplicationName = audit.ApplicationName,
               BannerId = audit.BannerId,
               ClientCode = audit.ClientCode,
               Gender = audit.Gender,
               IpAddress = audit.IpAddress,
               Location = audit.Location,
               Pageurl = audit.Pageurl,
               Postcode = audit.Postcode,
               UserGroup = audit.UserGroup,
               UserId = audit.UserId
           };
       }

    }
}
