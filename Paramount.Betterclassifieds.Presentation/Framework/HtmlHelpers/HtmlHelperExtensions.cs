using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Framework.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString GenerateRelayQrCode(this HtmlHelper html, string qrValue, int height = 250, int width = 250, int margin = 0)
        {
            var eventBarcodeManager = DependencyResolver.Current.GetService<IEventBarcodeManager>();
            var data = eventBarcodeManager.GenerateBase64StringImageData(qrValue, height, width, margin);

            var img = new TagBuilder("img");
            img.MergeAttribute("alt", "qr");
            img.Attributes.Add("src", $"data:image/gif;base64,{data}");

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}
