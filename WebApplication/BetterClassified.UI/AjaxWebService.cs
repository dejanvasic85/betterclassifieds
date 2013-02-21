using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using BetterclassifiedsCore;
using BetterClassified.UIController;
using System.IO;
using System.Web.UI;
using BetterClassified.UIController.Booking;

namespace BetterClassified.UI
{
    [System.Web.Services.WebService(Namespace = "http://Betterclassified.UI.Services")]
    [System.Web.Services.WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService()]
    public partial class AjaxWebService : System.Web.Services.WebService
    {
        [WebMethod(true)]
        public string GetNextBanner(string parameters)
        {
            //'Dim js As New JavaScriptSerializer()
            //        'Dim bannerParams As UI.BannerParameters = js.Deserialize(Of UI.BannerParameters)(params)
            //        'Return bannerParams.Category + (New Random()).Next().ToString()
            return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public int GetLineAdWordCount(string adText)
        {
            return LineAdHelper.GetWordCount(adText);
        }
   }
}