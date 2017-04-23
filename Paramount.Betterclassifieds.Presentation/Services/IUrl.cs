using System;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IUrl
    {
        IUrl WithAbsoluteUrl();

        string Home();
        string EventUrl(string titleSlug, int id);
        string EventOrganiserInviteUrl(int eventId, string token, string recipient);
        string Login();
        string AdUrl(string heading, int id, string categoryAdType);
        string AdUrl(AdBookingModel ad);
        string Image(string documentId, int height = 100, int width = 100);
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

        public string Login()
        {
            return BuildIt(() => _urlHelper.Login());
        }

        public string AdUrl(string heading, int id, string categoryAdType)
        {
            return BuildIt(() => _urlHelper.AdUrl(Slug.Create(true, heading), id, categoryAdType));
        }

        public string AdUrl(AdBookingModel ad)
        {
            Guard.NotNull(ad);
            Guard.NotNull(ad.OnlineAd);

            return AdUrl(ad.OnlineAd.Heading, ad.AdBookingId, ad.CategoryAdType);
        }

        public string Image(string documentId, int height = 100, int width = 200)
        {
            return BuildIt(() => _urlHelper.Image(documentId, height, width));
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