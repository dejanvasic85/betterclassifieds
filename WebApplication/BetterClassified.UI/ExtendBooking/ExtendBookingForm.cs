using System.Web;
using System.Web.UI.WebControls;

namespace BetterClassified.UI
{
    using UIController.Views;
    using UIController.Controllers;

    /// <summary>
    /// Form control used for extending an existing booking by allowing user to specify additional insertion number
    /// </summary>
    public class ExtendBookingForm : BaseCompositeControl<ExtendBookingController, IExtendBookingView>, IExtendBookingView
    {
        private readonly Label label;
        private readonly Button btnSubmit;
        private readonly DropDownList ddlInsertions;

        public ExtendBookingForm()
        {
            label = new Label { Text = "Welcome to the new form! You can edit AdBooking " + this.AdBookingId.ToString() + " here...S"};
        }

        protected override void CreateChildControls()
        {
            this.Controller.Load();

            this.Controls.Add(label);
        }

        public int AdBookingId
        {
            get { return HttpContext.Current.ReadQueryString<int>("AdBookingId"); }
        }
    }
}
