namespace Paramount.Banners.UI.HttpHandlers
{
    using DSL.UI.HttpHanders;
    using ApplicationBlock.Configuration;
    using UIController;
    using UIController.ViewObjects;

    public class ImageHandler : DocumentHandler
    {
        public ImageHandler()
        {
            ProcessRequestBegin += ImageHandler_ProcessRequestBegin;
        }

        static void ImageHandler_ProcessRequestBegin(object sender, HttpHandlerEventArgs e)
        {
            if (e.Context == null) return;
            var bannerId = e.Context.Request.QueryString[Constants.BannerId];
            var audit = new BannerAudit
                            {
                                ActionTypeName = e.Context.Request.RequestType,
                                ApplicationName = ConfigSettingReader.ApplicationName,
                                ClientCode = ConfigSettingReader.ClientCode,
                                BannerId = bannerId,
                                IpAddress = e.Context.Request.UserHostAddress,
                                UserId = e.Context.User.Identity.Name,
                                Location = e.Context.Request.UserHostName,
                                Pageurl = e.Context.Request.Url.ToString(),
                                UserGroup = e.Context.Request.Browser.Browser
                            };
            BannerController.LogBannerAudit(audit, e.GroupingId);
        }

    }
}