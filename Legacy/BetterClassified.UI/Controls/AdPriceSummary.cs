using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BetterClassified.UIController.Booking;
using BetterclassifiedsCore;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;
using BetterclassifiedsCore.BusinessEntities;

namespace BetterClassified.UI
{
    public class AdPriceSummary : ParamountCompositeControl
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.CssClass = "adpricesummary-main";

            // Fetch the category session details
            IBookCartContext bookingCart = BookCartController.GetCurrentBookCart();

            // Section - Category
            HtmlGenericControl categorySectionDiv = new HtmlGenericControl("div");
            categorySectionDiv.Attributes["class"] = "adpricesummary-sectionheading";
            categorySectionDiv.InnerText = GetResource(EntityGroup.Betterclassified, ContentItem.AdPriceSummaryControl, "CategoryLabel.Text");

            TableCell mainCategoryCell = new TableCell();
            TableCell subCategoryCell = new TableCell();

            Label mainCategoryLabel = new Label() { Text = bookingCart.MainCategoryName, CssClass = "adpricesummary-chargeitemlabel" };
            mainCategoryCell.Controls.Add(mainCategoryLabel);
            Label subCategorylabel = new Label() { Text = bookingCart.SubCategoryName, CssClass = "adpricesummary-chargeitemlabelWithSpace" };
            subCategoryCell.Controls.Add(subCategorylabel);

            TableRow mainCategoryRow = new TableRow();
            mainCategoryRow.Cells.Add(mainCategoryCell);
            TableRow subCategoryRow = new TableRow();
            subCategoryRow.Cells.Add(subCategoryCell);

            Table categoryTable = new Table() { CssClass = "adpricesummary-items" };
            categoryTable.Rows.Add(mainCategoryRow);
            categoryTable.Rows.Add(subCategoryRow);

            // Section - Publications
            HtmlGenericControl publicationsDiv = new HtmlGenericControl("div");
            publicationsDiv.Attributes["class"] = "adpricesummary-sectionheading";
            publicationsDiv.InnerText = GetResource(EntityGroup.Betterclassified, ContentItem.AdPriceSummaryControl, "PublicationsLabel.Text");

            Table publicationTable = new Table() { CssClass = "adpricesummary-items" };

            decimal totalPublicationsPrice = 0M;
            foreach (var publicationPrice in bookingCart.BookingOrderPrice.PublicationPriceList)
            {
                Label publicationName = new Label() { Text = publicationPrice.PublicationName, CssClass = "adpricesummary-chargeitemlabel" };
                decimal publicationTotalPrice = publicationPrice.CalculatePrice(bookingCart);
                totalPublicationsPrice += publicationTotalPrice;
                Label publicationPriceLabel = new Label() { Text = publicationTotalPrice.ToString("N"), CssClass = "adpricesummary-price" };

                TableCell nameCell = new TableCell();
                nameCell.Controls.Add(publicationName);
                TableCell valueCell = new TableCell();
                valueCell.Controls.Add(publicationPriceLabel);

                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(nameCell);
                tableRow.Cells.Add(valueCell);

                publicationTable.Rows.Add(tableRow);
            }

            // Section - Schedule
            HtmlGenericControl scheduleSectionDiv = new HtmlGenericControl("div");
            scheduleSectionDiv.Attributes["class"] = "adpricesummary-sectionheading";
            scheduleSectionDiv.InnerText = GetResource(EntityGroup.Betterclassified, ContentItem.AdPriceSummaryControl, "ScheduleLabel.Text");

            TableCell editionLabelCell = new TableCell();
            Label editionLabel = new Label()
            {
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.AdPriceSummaryControl, "Editions.Text"),
                CssClass = "adpricesummary-chargeitemlabel"
            };
            editionLabelCell.Controls.Add(editionLabel);

            TableCell editionValueCell = new TableCell();
            int editions = bookingCart.EditionCount == 0 ? 1 : bookingCart.EditionCount;
            Label editionCountLabel = new Label() { Text = editions.ToString(), CssClass = "adpricesummary-price" };
            editionValueCell.Controls.Add(editionCountLabel);

            TableRow scheduleValueRow = new TableRow();
            scheduleValueRow.Cells.Add(editionLabelCell);
            scheduleValueRow.Cells.Add(editionValueCell);

            Table scheduleTable = new Table() { CssClass = "adpricesummary-items" };
            scheduleTable.Rows.Add(scheduleValueRow);

            // Section Sub Total
            HtmlGenericControl subTotalDiv = new HtmlGenericControl("div");
            subTotalDiv.Attributes["class"] = "adpricesummary-total";
            subTotalDiv.InnerText = GetResource(EntityGroup.Betterclassified, ContentItem.AdPriceSummaryControl, "SubTotalLabel.Text");

            Label subTotalLabel = new Label() { Text = totalPublicationsPrice.ToString("N"), CssClass = "adpricesummary-price" };
            subTotalDiv.Controls.Add(subTotalLabel);

            // Add controls to page in order
            this.Controls.Add(categorySectionDiv);
            this.Controls.Add(categoryTable);
            this.Controls.Add(scheduleSectionDiv);
            this.Controls.Add(scheduleTable);
            this.Controls.Add(publicationsDiv);
            this.Controls.Add(publicationTable);
            this.Controls.Add(subTotalDiv);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.jquery-timers.js");
        }
    }
}
