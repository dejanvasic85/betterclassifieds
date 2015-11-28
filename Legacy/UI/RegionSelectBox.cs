namespace Paramount.Common.UI
{
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BaseControls;
    using Telerik.Web.UI;
    using WebContent;

    public class RegionSelectBox : ParamountCompositeControl
    {
        private readonly RadComboBox _regionList;
        private readonly ParamountLabel _regionLabel;
        private readonly bool _showLabel;

        public int SelectedRegionId { get; set; }

        public RegionSelectBox(bool showLabel)
        {
            _showLabel = showLabel;
            _regionLabel = new ParamountLabel
            {
                CssClass = "label",
                Text = GetResource(EntityGroup.Events, ContentItem.Location, "region.text")
            };

            _regionList = new RadComboBox
            {
                ID = "regionBox",
                WebServiceSettings = { Method = "GetRegionList", Path = "~/UIService/AjaxWebService.asmx" },
                EnableItemCaching = true,
                Width = Unit.Percentage(100),
                EnableLoadOnDemand = true,
                DataTextField = "Description",
                DataValueField = "Code",
                Skin = "Vista",
            };
            _regionList.SelectedIndexChanged += RegionListSelectedIndexChanged;
        }

        void RegionListSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SelectedRegionId = Convert.ToInt32(_regionList.SelectedValue);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            CssClass = "region-list";
            if (SelectedRegionId != 0)
            {
                _regionList.SelectedValue = SelectedRegionId.ToString(CultureInfo.CurrentCulture);
            }
        }

        protected override void CreateChildControls()
        {
            if (_showLabel)
            {
                Controls.Add(_regionLabel);
            }
            Controls.Add(_regionList);
        }

        internal WebServiceSettings GetServiceSetting()
        {
            return new WebServiceSettings(new StateBag(true)) { Method = "GetRegionList", Path = "~/UIService/AjaxWebService.asmx" };
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
    }
}
