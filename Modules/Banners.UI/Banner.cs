using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Banners.UI.HttpHandlers;
using Paramount.Banners.UIController;
using Paramount.Banners.UIController.ViewObjects;
using Paramount.Common.UI.BaseControls;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using Paramount.DSL.UIController;
using Constants = Paramount.Banners.UIController.Constants;

namespace Paramount.Banners.UI
{
    public class Banner : ParamountCompositeControl
    {
        protected Panel bannerPanel;
        protected Panel imagePanel;
        private Image image;
        private HtmlAnchor bannerUrlLink;
        //protected HtmlInputHidden bannerParamsHidden;
        //protected HtmlInputHidden bannerTypeHidden;

        public Banner()
        {
            bannerPanel = new Panel() { ID = "bannerPanel", CssClass = "banner-panel" };
            imagePanel = new Panel() { ID = "imagebannerPanel", CssClass = "image-panel" };
            //bannerParamsHidden = new HtmlInputHidden() { ID = "bannerParamsHidden" };
            //bannerTypeHidden = new HtmlInputHidden() { ID = "bannerTypeHidden" };
            bannerUrlLink = new HtmlAnchor() { ID = "bannerUrlLink", Target = "_blank"};
            
            image = new Image() {ID = "bannerImage"};
           /// image.Click += ImageClicked;
        }

        private void ImageClicked(object sender, ImageClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(bannerPanel);
            bannerPanel.Controls.Add(imagePanel);
            bannerUrlLink.Controls.Add(image);
            imagePanel.Controls.Add(this.bannerUrlLink);
            //bannerPanel.Controls.Add(bannerParamsHidden);
            //bannerPanel.Controls.Add(bannerTypeHidden);
        }

        public void BindData(BannerParameters parameters)
        {
            this.BannerParams = parameters;
            this.DisplayBanner();
        }

        private void DisplayBanner()
        {
            
            var banner = BannerController.GetNextBanner(this.GroupId, this.BannerParams);
            var bannerGroup = BannerController.GetBannerGroup(this.GroupId.ToString());

            image.ImageUrl = string.Format(CultureInfo.InvariantCulture, "{0}?{1}&" + Constants.BannerHandlerTypeParam + "=" + Constants.BannerHandlerTypeParamRender, BannerHandlerPath, Constants.BannerIdParam + "=" + banner.BannerId + "&" + Constants.BannerHeightParam + "=" + bannerGroup.BannerHeight + "&" + Constants.BannerWidthParam + "=" + bannerGroup.BannerWidth);

            if (!string.IsNullOrEmpty(banner.Url))
            {
                bannerUrlLink.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?{1}&" + Constants.BannerHandlerTypeParam + "=" + Constants.BannerHandlerTypeParamClick, BannerHandlerPath, Constants.BannerIdParam + "=" + banner.BannerId);
            }
        }

        public string BannerHandlerPath { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.BannerParams !=null)
            {
                DisplayBanner();
            }
            //var group = UIController.BannerController.GetBannerGroup(this.GroupId);
            //var bannerType = new BannerType() {IncludeTimer = group.IncludeTimer};

            /*
            var js = new JavaScriptSerializer();
            bannerParamsHidden.Value = js.Serialize(this.BannerParams);
            bannerTypeHidden.Value = js.Serialize(bannerType);

            Page.ClientScript.RegisterClientScriptResource(GetType(),
                     "Paramount.Banners.UI.JavaScript.jquery-1.4.min.js");
            Page.ClientScript.RegisterClientScriptResource(GetType(),
                       "Paramount.Banners.UI.JavaScript.json2.js");
            Page.ClientScript.RegisterClientScriptResource(GetType(),
                     "Paramount.Banners.UI.JavaScript.jquery.timers-1.2.js");
            Page.ClientScript.RegisterClientScriptResource(GetType(),
                       "Paramount.Banners.UI.JavaScript.banner.js");*/

        }

        public BannerParameters BannerParams { get; set; }

        public Guid GroupId { get; set; }

        [Serializable]
        private class BannerType
        {
            public bool IncludeTimer { get; set; }
        }
    }
}

