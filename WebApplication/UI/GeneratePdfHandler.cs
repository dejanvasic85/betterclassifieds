using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using Paramount.Utility;

namespace Paramount.Common.UI
{
    public class GeneratePdfHandler : IHttpHandler, IRequiresSessionState  
    {
        const string PDFPath = "~/pdf";

        #region Implementation of IHttpHandler

        public void ProcessRequest(HttpContext context)
        {
            DownloadFile(context);
            GeneratePdf(context);
        }

        private static void DownloadFile(HttpContext context)
        {
            var file = context.Request.Params["File"];

            if (string.IsNullOrEmpty(file))
            {
                return;
            }

            file = PDFPath + "/" + file;

          

            if (file == null) return;

            var bRet = false;
            var  iTimeout = 0;
            while (bRet == false)
            {
                bRet = CheckIfFileExist(file,context);
                Thread.Sleep(1000);
                iTimeout++;
                if (iTimeout == 10)
                    break;
            }

            if (bRet)
            {
                file = context.Server.MapPath(file);

                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.ContentType = "Application/pdf";
                try
                {
                    context.Response.WriteFile(file);
                    context.Response.Flush();
                    context.Response.Close();
                }
                catch
                {
                    context.Response.ClearContent();
                }

            }
            else
            {
                if (context.Request.Params["Msg"] != null)
                {
                    context.Response.Write(context.Request.Params["Msg"]);
                }
            }
        }

        private static  void GeneratePdf(HttpContext context)
        {
            string sRawUrl = context.Request.Params["pdfUrl"];
            if (sRawUrl == null) return;

            sRawUrl = context.Request.Url.MakeRelativeUri(new Uri(sRawUrl)).ToString();

            
            var pGen = new PDFGenerator(context.Server.MapPath(PDFPath));
            //var  sFile = pGen.RunWeb(context.Request.Params["Url"]);
            var sFile = pGen.Run(sRawUrl,string.Empty);

            context.Response.Redirect("DisplayPDF.aspx?File=" + sFile);
        }

        private static bool CheckIfFileExist(string sFile, HttpContext context)
        {
            //var sRoot = context.Server.MapPath("/");
            //var  sDrive = "" + sRoot[0];

            //return File.Exists(context.Server.MapPath(PDFPath + context.Request.Params["File"].ToString())) != false;
            return File.Exists(context.Server.MapPath(sFile)) != false;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion
    }
}
