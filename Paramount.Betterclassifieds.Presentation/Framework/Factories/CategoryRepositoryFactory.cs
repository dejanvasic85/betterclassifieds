using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    public class CategoryRepositoryFactory : ICategoryAdRepositoryFactory
    {
        private readonly IUnityContainer _unityContainer;

        public CategoryRepositoryFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// Uses the named registrations for each repository within UnityConfig
        /// </summary>
        public ICategoryAdRepository<ICategoryAd> Create(IBookingCart bookingCart)
        {
            if (bookingCart.ViewName.IsNullOrEmpty())
            {
                return null;
            }

            return _unityContainer.Resolve<ICategoryAdRepository<ICategoryAd>>(bookingCart.ViewName);
        }
    }
}