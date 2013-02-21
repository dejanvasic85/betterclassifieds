using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Banners.UI.BannerGroup
{
    public class BannerGroupDetails : ParamountCompositeControl
    {
        private readonly TextBox heightBox;

        private readonly FilteredTextBoxExtender heightBoxFilter;
        private readonly CheckBox includeTimer;
        private readonly TextBox titleBox;
        private readonly FilteredTextBoxExtender widthBoxFilter;
        private readonly TextBox widthBox;
        private readonly RequiredFieldValidator titleRequired;

        public BannerGroupDetails()
        {
            titleBox = new TextBox { ID = "titleBox", MaxLength = 30 };
            heightBox = new TextBox { ID = "heightBox", MaxLength = 3 };
            widthBox = new TextBox { ID = "widthBox", MaxLength = 3 };
            includeTimer = new CheckBox { ID = "includeTimer", Text = "Timer" };

            heightBoxFilter = new FilteredTextBoxExtender
                                  {
                                      TargetControlID = heightBox.ID,
                                      FilterMode = FilterModes.ValidChars,
                                      FilterType = FilterTypes.Numbers
                                  };

            widthBoxFilter = new FilteredTextBoxExtender
                                 {
                                     TargetControlID = widthBox.ID,
                                     FilterMode = FilterModes.ValidChars,
                                     FilterType = FilterTypes.Numbers
                                 };

            this.titleRequired = new RequiredFieldValidator() { ID = "titleRequired", ControlToValidate = this.titleBox.ID, ErrorMessage = "You must provide title" };
        }

        public string Title
        {
            get { return titleBox.Text; }
            set { titleBox.Text = value; }
        }

        public int? BannerHeight
        {
            get
            {
                int height;
                if (int.TryParse(heightBox.Text, out height))
                {
                    return height;
                }
                return null;
            }
            set { heightBox.Text = value.ToString(); }
        }


        public int? BannerWidth
        {
            get
            {
                int width;
                if (int.TryParse(widthBox.Text, out width))
                {
                    return width;
                }
                return null;
            }
            set { widthBox.Text = value.ToString(); }
        }

        public bool IncludeTimer
        {
            get { return includeTimer.Checked; }
            set { includeTimer.Checked = value; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Controls.Add(titleBox.DivWrapLabelValue("Title",titleRequired));
            Controls.Add(heightBox.DivWrapLabelValue("Height"));
            Controls.Add(widthBox.DivWrapLabelValue("Width"));

            Controls.Add(includeTimer.DivWrap());

            Controls.Add(heightBoxFilter);
            Controls.Add(widthBoxFilter);
        }

        public UIController.ViewObjects.BannerGroup BannerGroup
        {

            get
            {
                return new UIController.ViewObjects.BannerGroup 
                           {
                               BannerHeight = BannerHeight,
                               BannerWidth = BannerWidth,
                               IncludeTimer = this.IncludeTimer,
                               ClientCode = "1",
                               //todo:
                               Title = this.Title,
                           };
            }
            set
            {
                this.BannerHeight = value.BannerHeight;
                this.BannerWidth = value.BannerWidth;
                this.Title = value.Title; this.IncludeTimer = value.IncludeTimer; 
            }
        }
    }
}