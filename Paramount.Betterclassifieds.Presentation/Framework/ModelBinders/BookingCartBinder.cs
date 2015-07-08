using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation.Framework.ModelBinders
{
    public class BookingCartBinder : IModelBinder
    {
        [Dependency]
        public IBookingContext Context { get; set; }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var bookingContext = DependencyResolver.Current.GetService<IBookingContext>();
            return bookingContext.Current();
        }
    }
}