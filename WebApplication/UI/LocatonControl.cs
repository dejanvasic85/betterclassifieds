using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.UI
{
    using System.Web.UI.WebControls;
    using System;
    using BaseControls;
    using WebContent;

    public class LocationControl : ParamountCompositeControlPanel
    {
        private readonly TextBox address2Box;
        private readonly TextBox address1Box;
        private readonly TextBox cityBox;
        private readonly TextBox regionBox;
        private readonly TextBox postcodeBox;
        private readonly TextBox countryBox;

        private readonly Label address2Label;
        private readonly Label address1Label;
        private readonly Label cityLabel;
        private readonly Label regionLabel;
        private readonly Label postcodeLabel;
        private readonly Label countryLabel;

        public LocationControl()
        {
            this.address2Box = new TextBox { Width = Unit.Percentage(100) };
            this.address1Box = new TextBox { Width = Unit.Percentage(100) };
            this.cityBox = new TextBox { Width = Unit.Percentage(100) };
            this.regionBox = new TextBox { Width = Unit.Percentage(100) };
            this.postcodeBox = new TextBox { Width = Unit.Percentage(100) };
            this.countryBox = new TextBox { Width = Unit.Percentage(100) };
            this.address1Label = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "address1.text") };
            this.address2Label = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "address2.text") };
            this.cityLabel = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "city.text") };
            this.postcodeLabel = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "postcode.text") };
            this.regionLabel = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "region.text") };
            this.countryLabel = new ParamountLabel { CssClass = "location-label", Text = GetResource(EntityGroup.Events, ContentItem.Location, "country.text") };
        }

        public string Address1
        {
            get { return this.address1Box.Text; }
            set { this.address1Box.Text = value; }
        }

        public string Address2
        {
            get { return this.address2Box.Text; }
            set { this.address2Box.Text = value; }
        }


        public string City
        {
            get { return this.cityBox.Text; }
            set { this.countryBox.Text = value; }
        }

        public string Region
        {
            get { return this.regionBox.Text; }
            set { this.regionBox.Text = value; }
        }

        public string Postcode
        {
            get { return this.postcodeBox.Text; }
            set { this.postcodeBox.Text = value; }
        }

        public string Country
        {
            get { return this.countryBox.Text; }
            set { this.countryBox.Text = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.address1Box.Text = this.Address1;
            //this.address2Box.Text = this.Address2;
            //this.cityBox.Text = this.City;
            //this.regionBox.Text = this.Region;
            //this.postcodeBox.Text = this.Postcode;
            //this.countryBox.Text = this.Country;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //add address1
            var address1Panel = GetSpanControl("layout-full");
            address1Panel.Controls.Add(this.address1Box);
            address1Panel.Controls.Add(this.address1Label);
            this.Controls.Add(address1Panel);

            //add address2
            var address2Panel = GetSpanControl("layout-full");
            address2Panel.Controls.Add(this.address2Box);
            address2Panel.Controls.Add(this.address2Label);
            this.Controls.Add(address2Panel);

            //add city
            var cityPanel = GetSpanControl("layout-left");
            cityPanel.Controls.Add(this.cityBox);
            cityPanel.Controls.Add(this.cityLabel);
            this.Controls.Add(cityPanel);

            //add region
            var regionPanel = GetSpanControl("layout-right");
            regionPanel.Controls.Add(this.regionBox);
            regionPanel.Controls.Add(this.regionLabel);
            this.Controls.Add(regionPanel);

            //add postcode
            var postcodePanel = GetSpanControl("layout-left");
            postcodePanel.Controls.Add(this.postcodeBox);
            postcodePanel.Controls.Add(this.postcodeLabel);
            this.Controls.Add(postcodePanel);

            //add country
            var countryPanel = GetSpanControl("layout-right");
            countryPanel.Controls.Add(this.countryBox);
            countryPanel.Controls.Add(this.countryLabel);
            this.Controls.Add(countryPanel);


        }

        private static Label GetSpanControl(string cssClass)
        {
            var panel = new Label { CssClass = cssClass };
            return panel;
        }
    }
}
