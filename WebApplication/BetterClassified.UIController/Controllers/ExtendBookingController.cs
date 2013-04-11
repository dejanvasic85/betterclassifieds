namespace BetterClassified.UIController.Controllers
{
    public class ExtendBookingController : Controller<Views.IExtendBookingView>
    {
        private Repository.IBookingRepository bookingRepository;

        public ExtendBookingController(Views.IExtendBookingView view, Repository.IBookingRepository bookingRepository) : base(view)
        {
            this.bookingRepository = bookingRepository;
        }

        public void Load()
        {
            
        }
    }
}