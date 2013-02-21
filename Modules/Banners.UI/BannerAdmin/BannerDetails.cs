
using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using Paramount.Banners.UIController;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;
using Paramount.DSL.UI;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class BannerDetails : ParamountCompositeControl
    {
        private readonly DropDownList groupSelector;
        private readonly TextBox titleBox;
        private readonly TextBox urlBox;
        private readonly TextBox alternateTextBox;
        //private readonly TextBox image;
        protected readonly RadDatePicker fromDate;
        protected readonly RadDatePicker toDate;
        private readonly RequiredFieldValidator titleRequired;
        private readonly CustomValidator validDateRange;
        private readonly CustomValidator validBannerTags;
        //private readonly LinkButton insertButton;
        //private readonly LinkButton cancelButton;
        private readonly HiddenField bannerIdHidden;
        private readonly Button addBannerTag;
        private readonly Repeater bannerTags;
        private readonly ImageUploadControl image;
        private readonly RegularExpressionValidator validUrl;

        private readonly Button saveButton;
        private readonly Button cancel;
        public event EventHandler Saved;
        public event EventHandler Canceled;


        public BannerDetails()
        {
            this.groupSelector = new DropDownList() { ID = "groupSelector" };
            this.titleBox = new TextBox() { ID = "titleBox" };
            this.urlBox = new TextBox() { ID = "urlBox" };
            this.validUrl=new RegularExpressionValidator(){ID="validUrl", ErrorMessage = "Url is not valid", ControlToValidate = urlBox.ID, ValidationExpression = Common.UIController.Contants.ValidationExpressions.Url};
            this.alternateTextBox = new TextBox() { ID = "alternateTextBox" };
            this.image = new ImageUploadControl() { ID = "image"};
            this.fromDate = new RadDatePicker() { ID = "fromDate"  };
            this.toDate = new RadDatePicker() { ID = "toDate" };
            this.titleRequired = new RequiredFieldValidator() { ID = "titleRequired", ControlToValidate = this.titleBox.ID, ErrorMessage = "You must provide title" };
            this.validDateRange = new CustomValidator() { ID = "validDateRange" };
            this.addBannerTag = new Button() { ID = "addBannerTag", Text = "Add Tag", CausesValidation = false };
            addBannerTag.Click += AddBannerTagClicked;
            this.bannerTags = new Repeater() { ID = "bannerTags", HeaderTemplate = new BannerTagHeaderTemplate(), ItemTemplate = new BannerTagTemplate() };
            this.validDateRange.ServerValidate += ValidateDateRange;
           // insertButton = new LinkButton() { Text = "Update", CommandName = "Update" };
            //cancelButton = new LinkButton() { Text = "Cancel", CommandName = "Cancel", CausesValidation = false };
            bannerIdHidden = new HiddenField() { ID = "bannerId" };
            validBannerTags = new CustomValidator() { ID = "validBannerTags", ErrorMessage = "Please provide values for all the banner tags. Location and category must be supplied." };
            validBannerTags.ServerValidate += ValidateBannerTags;

            this.saveButton = new Button() { ID = "saveButton", Text = "Save" };
            this.saveButton.Click += SaveBannerClicked;

            this.cancel = new Button() { ID = "cancelButton", Text = "Cancel", CausesValidation =   false};
            this.cancel.Click += CancelBannerClicked;

        }

        private void CancelBannerClicked(object sender, EventArgs e)
        {
            InvokeCanceled(EventArgs.Empty);
        }

        private void SaveBannerClicked(object sender, EventArgs e)
        {
            this.Save();
            this.InvokeSaved(EventArgs.Empty);
        }

        public void InvokeCanceled(EventArgs e)
        {
            EventHandler handler = Canceled;
            if (handler != null) handler(this, e);
        }

        public void InvokeSaved(EventArgs e)
        {
            EventHandler handler = Saved;
            if (handler != null) handler(this, e);
        }

        private void ValidateBannerTags(object source, ServerValidateEventArgs args)
        {
            var tags = new NameValueCollection();
            var hasLocation = false;
            var hasCateogry = false;

            foreach (RepeaterItem item in this.bannerTags.Items)
            {
                var tagName = (TextBox)item.FindControl("tagName");
                var tagvalue = (TextBox)item.FindControl("tagValue");


                if (tagName.Text.ToUpper() == "LOCATION")
                {
                    hasLocation = true;
                }

                if (tagName.Text.ToUpper() == "CATEGORY")
                {
                    hasCateogry = true;
                }

                if ((string.IsNullOrEmpty(tagName.Text)) || (string.IsNullOrEmpty(tagvalue.Text)))
                {
                    args.IsValid = false;
                    return;
                }
            }

            if (!hasLocation || !hasCateogry)
            {
                args.IsValid = false;
            }

        }

        private void AddBannerTagClicked(object sender, EventArgs e)
        {
            SessionManager.BannerTags.Add("New Value" + SessionManager.BannerTags.Count, string.Empty);
            this.PopulateBannerTags();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!this.Page.IsPostBack)
            {
                SessionManager.BannerTags = null;

            }
            //if (!this.Page.IsPostBack)
            //{
                PopulateGroups();
                PopulateBannerTags();
  //          }
//
            Controls.Add(titleBox.DivWrapLabelValue("Title", titleRequired));
            Controls.Add(alternateTextBox.DivWrapLabelValue("Alternate Text"));
            Controls.Add(groupSelector.DivWrapLabelValue("Group"));
            Controls.Add(urlBox.DivWrapLabelValue("URL", validUrl));
            Controls.Add(image.DivWrapLabelValue("Image"));
            Controls.Add(fromDate.DivWrapLabelValue("From Date"));
            Controls.Add(toDate.DivWrapLabelValue("To Date", validDateRange));

            var tagsPanel = new Panel() { CssClass = "banner-tags-panel" };
            Controls.Add(tagsPanel);

            tagsPanel.Controls.Add(addBannerTag);
            tagsPanel.Controls.Add(bannerTags);
            tagsPanel.Controls.Add(validBannerTags);

            var panelButtons = new Panel() { CssClass = "banner-tags-buttons" };
            this.Controls.Add(panelButtons);

            //panelButtons.Controls.Add(insertButton);
            //panelButtons.Controls.Add(cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancel);
            panelButtons.Controls.Add(bannerIdHidden);
        }

        private void ValidateDateRange(object source, ServerValidateEventArgs args)
        {
            if ((!this.fromDate.SelectedDate.HasValue) || (!this.toDate.SelectedDate.HasValue))
            {
                args.IsValid = false;
                validDateRange.ErrorMessage = "Both dates must be supplied";
                return;

            }
            if ((this.BannerId != Guid.Empty && this.BannerId != Guid.Empty) && this.fromDate.SelectedDate < DateTime.Now)
            {
                args.IsValid = false;
                validDateRange.ErrorMessage = "From date must be in today or in future";
                return;
            }
            else if (this.fromDate.SelectedDate > this.toDate.SelectedDate)
            {
                args.IsValid = false;
                validDateRange.ErrorMessage = "From date must be on or before to date";
                return;
            }

        }

        public string Title
        {
            get { return titleBox.Text; }
            set { titleBox.Text = value; }
        }

        public UIController.ViewObjects.Banner Banner
        {
            get
            {
                return new UIController.ViewObjects.Banner()
                           {
                               AlternateText = this.alternateTextBox.Text,
                               ClientCode = ApplicationBlock.Configuration.ConfigSettingReader.ClientCode,
                               End = this.toDate.SelectedDate.Value,
                               Start = this.fromDate.SelectedDate.Value,
                               GroupId = new Guid(this.groupSelector.SelectedValue),
                               ID = this.BannerId,
                               ImageId = this.image.DocumentId,
                               Title = this.titleBox.Text,
                               Url = this.urlBox.Text,
                               BannerId = this.BannerId,
                               BannerTags = SessionManager.BannerTags
                           };
            }
            set
            {
                this.alternateTextBox.Text = value.AlternateText;
                this.fromDate.SelectedDate = value.Start;
                this.toDate.SelectedDate = value.End;
                this.groupSelector.SelectedValue = value.GroupId.ToString();
                this.image.DocumentId = value.ImageId;
                this.titleBox.Text = value.Title;
                this.urlBox.Text = value.Url;
                this.bannerIdHidden.Value = value.BannerId.ToString();
                SessionManager.BannerTags = value.BannerTags;
                this.PopulateBannerTags();
            }
        }

        public Guid BannerId
        {
            get
            {

                return string.IsNullOrEmpty(this.bannerIdHidden.Value) ? Guid.Empty : new Guid(this.bannerIdHidden.Value);

            }
            set
            {
                bannerIdHidden.Value = value.ToString();
                if (value != Guid.Empty)
                {
                    this.Banner = UIController.BannerController.GetBanner(this.BannerId);
                }
            }
        }

        private void PopulateGroups()
        {
            this.groupSelector.DataTextField = "Title";
            this.groupSelector.DataValueField = "GroupId";
            this.groupSelector.DataSource = UIController.BannerController.GetBannerGroups();
            this.groupSelector.DataBind();
        }

        void PopulateBannerTags()
        {
            if (SessionManager.BannerTags == null || SessionManager.BannerTags.Count == 0)
            {
                SessionManager.BannerTags = new NameValueCollection { { "Location", "" }, { "Category", "" } };
            }
            this.bannerTags.DataSource = SessionManager.BannerTags;
            this.bannerTags.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.CssClass = "banner-details";

            //insertButton.CommandArgument = this.bannerIdHidden.ToString();
        }

        public bool Save()
        {
            if (!Page.IsValid)
            {
                return false; 
            }

            GetBannerTags();
            if (image.Upload())
            {
                return this.Page.IsValid && BannerController.SaveBanner(this.Banner);
            }
            return false;
        }

      
        private void GetBannerTags()
        {
            var tags = new NameValueCollection();
            foreach (RepeaterItem item in this.bannerTags.Items)
            {
                var tagName = (TextBox)item.FindControl("tagName");
                var tagvalue = (TextBox)item.FindControl("tagValue");
                var removeTag = (CheckBox)item.FindControl("removeTag");
                if ((!removeTag.Checked) && (!string.IsNullOrEmpty(tagName.Text)) && (!string.IsNullOrEmpty(tagvalue.Text)))
                {
                    tags.Add(tagName.Text, tagvalue.Text);
                }
            }
            SessionManager.BannerTags = tags;
        }

        public DateTime Start
        {
            get
            {

                return this.fromDate.SelectedDate.HasValue ? fromDate.SelectedDate.Value : DateTime.MinValue;
            }
            set
            {
                if (value < this.fromDate.MinDate)
                {
                    value = this.fromDate.MinDate;
                }
                if (value > this.fromDate.MaxDate)
                {
                    value = this.fromDate.MaxDate;
                }
                this.fromDate.SelectedDate = value;
            }
        }

        public DateTime End
        {
            get
            {

                return this.toDate.SelectedDate.HasValue ? toDate.SelectedDate.Value : DateTime.MinValue;
            }
            set
            {
                if (value < this.toDate.MinDate)
                {
                    value = this.toDate.MinDate;
                }
                if (value > this.toDate.MaxDate)
                {
                    value = this.toDate.MaxDate;
                }
                this.toDate.SelectedDate = value;
            }
        }

        
        


    }
}
