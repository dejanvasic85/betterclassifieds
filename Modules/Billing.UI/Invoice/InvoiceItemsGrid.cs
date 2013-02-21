using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.UI.BaseControls;
using Telerik.Web.UI;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public class InvoiceItemsGrid : ParamountCompositeControl
    {
        public RadGrid invoiceItemsGrid;
        decimal total;


        public InvoiceItemsGrid()
        {
            PrepareInvoiceGrid();
        }

        private void PrepareInvoiceGrid()
        {
            this.invoiceItemsGrid = new RadGrid() { ID = "invoiceItemsGrid", AutoGenerateColumns = false, ShowFooter = true};
            this.invoiceItemsGrid.ItemDataBound += ItemDataBound;
            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Title",
                DataField = "Title",
            });

            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Product Type",
                DataField = "ProductType",
            });

            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Summary",
                DataField = "Summary",
            });

            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Price",
                DataField = "Price",
            });
            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Quantity",
                DataField = "Quantity",
            });


            this.invoiceItemsGrid.Columns.Add(new GridBoundColumn()
            {
                HeaderText = "Sub Total",
                DataField = "SubTotal",
            });


        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.invoiceItemsGrid);
        }

        public void LoadData(List<InvoiceItemEntity> invoiceItems)
        {
            this.invoiceItemsGrid.DataSource = invoiceItems;
            this.invoiceItemsGrid.DataBind();
            total = invoiceItems.Sum(item => item.SubTotal);
        }

        private void ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footerItem = e.Item as GridFooterItem;
                footerItem["Quantity"].Text = "total: " + total.ToString();
            }
        } 

    }
}
