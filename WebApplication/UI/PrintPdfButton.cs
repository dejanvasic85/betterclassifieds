using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Paramount.Common.UI.BaseControls;
using Paramount.Utility;

namespace Paramount.Common.UI
{
    public class PrintPdfButton : ParamountCompositeControl
    {
        public Button printPdf;

        public PrintPdfButton()
        {
            this.printPdf = new Button() { ID = "printPdf", CssClass = "print-pdf", Text = "PDF" };
            this.printPdf.Click += PrintPdf;
        }

        public string PdfFileName { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.IsInPdfPrintMode)
            {
                this.Visible = false;
               
            }
        }


        private void PrintPdf(object sender, EventArgs e)
        {
            PrintThisInPdf();
        }

        private void PrintThisInPdf()
        {
            if (!(this.Page is IPdfPrint)) return;
            if (IsInPdfPrintMode) return;
            //var queryString = new CustomQueryString(this.Page.Request.QueryString);
            //queryString.Add("pdf", "true");
            //var url = queryString.CompleteUrl(this.Page.Request.Url);
            //this.Page.Response.Redirect(url);

            var queryString = new CustomQueryString(this.Page.Request.QueryString);
            queryString.Remove("pdf");
           
            //this.Page.Response.Redirect(string.Format("DisplayPdf.aspx?url={0}", generator.Run(string.Format("{0}?pdf=false", pathList[pathList.Length - 1]), this.PdfFileName)));
            this.Page.Response.Redirect(string.Format("DisplayPdf.aspx?pdfUrl={0}", this.Page.Server.UrlEncode(queryString.CompleteUrl(this.Page.Request.Url))));
            //generator.Run(queryString.CompleteUrl(this.Page.Request.Url));
            //generator.Run(this.Page);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.printPdf);
        }
    }
}
