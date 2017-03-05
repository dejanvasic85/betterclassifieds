using System;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IUrl
    {
        UrlBuilder Home();
        UrlBuilder EventUrl(string titleSlug, int id);
        UrlBuilder EventOrganiserInviteUrl(int eventId, string token, string recipient);
    }

    /// <summary>
    /// Wrapper class around the original UrlHelper with lazy loading (cache)
    /// </summary>
    public class UrlService : IUrl
    {
        private readonly UrlHelper _urlHelper;

        public UrlService(HttpContextBase httpContext)
        {
            _urlHelper = new UrlHelper(httpContext.Request.RequestContext);
        }

        public UrlBuilder Home() => new Lazy<UrlBuilder>(() => _urlHelper.Home()).Value;

        public UrlBuilder EventUrl(string titleSlug, int id)
        {
            return _urlHelper.AdUrl(titleSlug, id, CategoryAdType.Event);
        }

        public UrlBuilder EventOrganiserInviteUrl(int eventId, string token, string recipient)
        {
            return _urlHelper.EventOrganiserInviteUrl(eventId, token, recipient);
        }
    }
}