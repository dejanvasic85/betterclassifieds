using System.Web;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IRobotVerifier
    {
        bool IsValid(string siteSectionSecret, string googleCaptchaResult);
        bool IsValid(string siteSectionSecret, HttpRequestBase httpRequest);
    }
}