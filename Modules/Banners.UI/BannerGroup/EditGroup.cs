using System;
using System.Web.UI.WebControls;
using Paramount.Banners.UIController;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Banners.UI.BannerGroup
{
    public class EditGroup : ParamountCompositeControl
    {
        private readonly BannerGroupDetails bannerGroup;
        private readonly Button saveButton;
        private readonly Button cancel;
        public event EventHandler Saved;
        public event EventHandler Canceled;

        public void InvokeCanceled(EventArgs e)
        {
            EventHandler handler = Canceled;
            if (handler != null) handler(this, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.Page.IsPostBack)
            {
                this.bannerGroup.BannerGroup = BannerController.GetBannerGroup(SessionManager.GroupId);
            }
        }
        public void InvokeSaved(EventArgs e)
        {
            EventHandler handler = Saved;
            if (handler != null) handler(this, e);
        }

        public EditGroup()
        {
            this.bannerGroup = new BannerGroupDetails() { ID = "bannerGorup" };
            this.saveButton = new Button() { ID = "saveButton", Text = "Save" };
            this.saveButton.Click += SaveBannerClicked;
            this.cancel = new Button() { ID = "cancelButton", Text = "Cancel" };
            this.cancel.Click += CancelBannerClicked;
        }

        private void CancelBannerClicked(object sender, EventArgs e)
        {
            InvokeCanceled(EventArgs.Empty);
        }

        private void SaveBannerClicked(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            if (BannerController.UpdateBannerGorup(this.bannerGroup.BannerGroup))
            {
                InvokeSaved(EventArgs.Empty);             
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.bannerGroup);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancel);
        }
    }
}
