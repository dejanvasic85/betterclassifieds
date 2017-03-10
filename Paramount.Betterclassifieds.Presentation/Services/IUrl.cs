using System;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IUrl
    {
        IUrl WithAbsoluteUrl();

        string Home();
        string EventUrl(string titleSlug, int id);
        string EventOrganiserInviteUrl(int eventId, string token, string recipient);
    }

    /// <summary>
    /// Wrapper class around the original UrlHelper with lazy loading (cache)
    /// </summary>
    public class UrlService : IUrl
    {
        private readonly UrlHelper _urlHelper;

        public bool IsAbsoluteUrlTurnedOn { get; private set; }

        public UrlService(HttpContextBase httpContext)
        {
            _urlHelper = new UrlHelper(httpContext.Request.RequestContext);
        }

        /// <summary>
        /// When turned set, it will generate full outgoing absolute URL's
        /// </summary>
        /// <returns></returns>
        public IUrl WithAbsoluteUrl()
        {
            IsAbsoluteUrlTurnedOn = true;
            return this;
        }

        public string Home()
        {
            return BuildIt(_urlHelper.Home);
        }
        
        public string EventUrl(string titleSlug, int id)
        {
            return BuildIt(() => _urlHelper.AdUrl(titleSlug, id, CategoryAdType.Event));
        }

        public string EventOrganiserInviteUrl(int eventId, string token, string recipient)
        {
            return BuildIt(() => _urlHelper.EventOrganiserInviteUrl(eventId, token, recipient));
        }

        public UrlBuilder BuildIt(Func<UrlBuilder> urlBuilderFunc)
        {
            var url = urlBuilderFunc();

            if (IsAbsoluteUrlTurnedOn)
            {
                return url.WithFullUrl();
            }

            return url;
        }
    }
}