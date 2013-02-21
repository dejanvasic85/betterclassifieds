using System;
using System.Web.UI.WebControls;
using Paramount.Banners.UI.BannerGroup;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class EditBanner : BannerDetails
    {

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (this.Page.IsPostBack) return;
            this.BannerId = new Guid(this.Page.Request["AppointmentId"]);
        }


    }
}
