using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    public class CategoryAdFactory : ICategoryAdFactory
    {
        private readonly IUnityContainer _unityContainer;

        public CategoryAdFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// Uses the named registrations for each repository within UnityConfig
        /// </summary>
        public ICategoryAdRepository<ICategoryAd> CreateRepository(IBookingCart bookingCart)
        {
            if (bookingCart.CategoryAdType.IsNullOrEmpty())
            {
                return null;
            }

            return _unityContainer.Resolve<ICategoryAdRepository<ICategoryAd>>(bookingCart.CategoryAdType);
        }
        
        public ICategoryAdAuthoriser CreateAuthoriser(string name)
        {
            return _unityContainer.Resolve<ICategoryAdAuthoriser>(name);
        }

        public IEnumerable<ICategoryAdAuthoriser> CreateAuthorisers()
        {
            return _unityContainer.ResolveAll<ICategoryAdAuthoriser>();
        }

        public ICategoryAdUrlService CreateUrlService(string categoryAdType)
        {
            return _unityContainer.Resolve<ICategoryAdUrlService>(categoryAdType);
        }
    }
}