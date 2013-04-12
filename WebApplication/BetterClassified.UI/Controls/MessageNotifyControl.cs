namespace BetterClassified.UI
{
    using System.Globalization;
    using System.Web;
    using System.Web.UI.WebControls;
    using BetterclassifiedsCore.Controller;
    using Paramount.Common.UI.BaseControls;

    public class MessageNotifyControl:ParamountCompositeControl
    {
        private readonly HyperLink _link;
        public MessageNotifyControl ()
        {
            _link = new HyperLink { CssClass = "message-notification", NavigateUrl = "~/MemberAccount/Messages.aspx" };
        }

        public int NewMessageCount
        {
            get
            {
                return OnlineAdEnquiryController.GetUnreadMessageCount(HttpContext.Current.User.Identity.Name);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            var count = NewMessageCount;
            if (count > 0)
            {
                _link.Text = string.Format(" - Messages ({0}) - ", count.ToString(CultureInfo.CurrentCulture));
                this.Controls.Add(_link);
            }
        }
        
    }
}