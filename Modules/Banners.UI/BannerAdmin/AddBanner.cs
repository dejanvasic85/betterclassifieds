using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class AddBanner : BannerDetails
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.fromDate.MinDate = DateTime.Now;
            this.toDate.MinDate = DateTime.Now;

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!this.Page.IsPostBack)
            {
                var date = this.Page.Request["Start"].ToString();
                
                this.Start = DateTime.ParseExact(date,"yyyyMMddHHmm", CultureInfo.InvariantCulture);
                this.End = this.Start;
            }
        }
       
    }
}
