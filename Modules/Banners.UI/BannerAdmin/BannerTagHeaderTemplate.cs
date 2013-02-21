using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class BannerTagHeaderTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            var bannerTagLabel = new Label() { ID = "bannerTagLabel", Text="Tag" };
            var bannerTagValueLabel = new Label() { ID = "bannerTagValueLabel", Text="Value" };
            var removeTagLabel = new Label() { ID = "removeTagLabel", Text="Remove" };

            var header = new Panel(){CssClass = "banner-tags-header"};
            header.Controls.Add(bannerTagLabel);
            header.Controls.Add(bannerTagValueLabel);
            header.Controls.Add(removeTagLabel);

            container.Controls.Add(header);
        }
    }
}