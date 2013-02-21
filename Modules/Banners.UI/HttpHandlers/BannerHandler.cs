using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using Paramount.ApplicationBlock.Configuration;
using Paramount.ApplicationBlock.Logging.EventLogging;
using Paramount.Banners.UIController;
using Paramount.Banners.UIController.ViewObjects;
using Paramount.Common.DataTransferObjects;
using Paramount.DSL.UI.HttpHanders;
using Paramount.DSL.UIController;
using Constants = Paramount.Banners.UIController.Constants;

namespace Paramount.Banners.UI.HttpHandlers
{
    public class BannerHandler : IHttpHandler, IRequiresSessionState
    {
        //public BannerHandler()
        //{
        //    ProcessRequestBegin += BannerHandler_ProcessRequestBegin;
        //}

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var groupingId = Guid.NewGuid().ToString();

                if (context == null) return;
                var bannerId =new Guid( context.Request.QueryString[Constants.BannerIdParam]);

                var banner = BannerController.GetBanner(bannerId);

                var type = context.Request.QueryString[Constants.BannerHandlerTypeParam];

                AuditLog(context, bannerId, groupingId);

                if (type.ToUpper() == Constants.BannerHandlerTypeParamClick.ToUpper())
                {
                    context.Response.Redirect(banner.Url);
                }
                else if (type.ToUpper() == Constants.BannerHandlerTypeParamRender.ToUpper())
                {
                    RenderBannerImage(context, banner);
                }
            }
            catch(Exception ex)
            {
                EventLogManager.Log(ex);
            }
        }

        private static decimal? GetIntValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            decimal intValue;
            if (decimal.TryParse(value, out intValue))
            {
                return intValue;
            }
            return null;
        }

        private static void RenderBannerImage(HttpContext context,  UIController.ViewObjects.Banner banner)
        {

            var bannerHeight = GetIntValue(context.Request.QueryString[Constants.BannerHeightParam]);
            var bannerWidth = GetIntValue(context.Request.QueryString[Constants.BannerWidthParam]);
            var param = new DslQueryParam(HttpContext.Current.Request.QueryString)
                            {
                                Entity = ConfigSettingReader.ClientCodeEncrypted,
                                Height = bannerHeight,
                                Width = bannerWidth,
                                Resolution = 100,
                                DocumentId = banner.ImageId.ToString()
                            };

            string imageHandlerUrl = param.GenerateUrl(ConfigSettingReader.DslImageHandler);
            context.Response.Redirect(imageHandlerUrl);
        }

        private static void AuditLog(HttpContext context, Guid bannerId, string groupingId)
        {
            var audit= new BannerAudit
                       {
                           ActionTypeName = context.Request.QueryString[Constants.BannerHandlerTypeParam],
                           ApplicationName = ConfigSettingReader.ApplicationName,
                           ClientCode = ConfigSettingReader.ClientCode,
                           BannerId = bannerId.ToString(),
                           IpAddress = context.Request.UserHostAddress,
                           //todo: pass user name
                           //UserId = context.User.Identity.Name,
                           Location = context.Request.UserHostName,
                           Pageurl = context.Request.Url.ToString(),
                           UserGroup = context.Request.Browser.Browser
                       };

            BannerController.LogBannerAudit(audit, groupingId);
        }


        public bool IsReusable
        {
            get { return false; }
        }
    }
}