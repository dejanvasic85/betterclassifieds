using System.Web.Services;
using BetterClassified.UIController;

namespace BetterClassified.UI
{
    [WebService(Namespace = "http://Betterclassified.UI.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public partial class AjaxWebService
    {
        [WebMethod(true)]
        public string GetNextBanner(string parameters)
        {
            return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public int GetLineAdWordCount(string adText)
        {
            return LineAdHelper.GetWordCount(adText);
        }
   }
}