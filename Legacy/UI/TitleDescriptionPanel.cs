namespace Paramount.Common.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;
    using Paramount.Common.UI.BaseControls;

    public class TitleDescriptionPanel : ParamountCompositeControlPanel
    {
        private readonly Label titleLable;
        private readonly Label descriptionLabel;
        private readonly TextBox titleBox;
        private readonly TextBox descriptionBox;


        public TitleDescriptionPanel()
        {
            this.titleLable = new Label { CssClass = "title-lable" };
            this.descriptionLabel = new Label { CssClass = "description-lable" };

            this.titleBox = new TextBox { CssClass = "title-box" };
            this.descriptionBox = new TextBox { CssClass = "description-box" };
        }

        public string Title
        {
            get { return this.titleBox.Text; }
            set { this.titleBox.Text = value; }
        }

        public string Description
        {
            get { return this.descriptionBox.Text; }
            set { this.descriptionBox.Text = value; }
        }

        public string TitleLabelText { get; set; }
        public string DescriptionLabelText { get; set; }

        public int DescriptionRowLength { get; set; }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.DescriptionRowLength != 0)
            {
                this.descriptionBox.Rows = this.DescriptionRowLength;
            }

            this.descriptionLabel.Text = string.IsNullOrEmpty(this.DescriptionLabelText) ? this.GetResource(EntityGroup.Constants, ContentItem.Common, "description-label") : this.DescriptionLabelText;

        }
    }
}
