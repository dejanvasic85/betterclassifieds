using System.Web;
using System.Web.UI.WebControls;

namespace BetterClassified.UI
{
    using UIController.Views;
    using UIController.Controllers;

    public class ExtendBookingForm : BaseCompositeControl<ExtendBookingController, IExtendBookingView>, IExtendBookingView
    {
        private readonly Label label;

        public ExtendBookingForm()
        {
            label = new Label { Text = "Welcome to the new form! You can edit AdBooking " + this.AdBookingId.ToString() + " here...S"};
        }

        protected override void CreateChildControls()
        {
            this.Controls.Add(label);
        }

        public int AdBookingId
        {
            get { return HttpContext.Current.ReadQueryString<int>("AdBookingId"); }
        }
    }
}
