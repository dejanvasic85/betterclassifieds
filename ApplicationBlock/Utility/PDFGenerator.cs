using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Paramount.Utility
{
    public class PDFGenerator : System.Web.UI.Page
    {
        private string sLandScape;
        private string waterMark;
        private readonly string drive;
        private readonly string directory;
        public PDFGenerator(string sDirectory)
        {
            sFontSize = "";
            sLandScape = "--landscape";
            waterMark = "";
            var sRoot = Server.MapPath("/");
            drive = "" + sRoot[0];
            directory = sDirectory;
        }

        private string sFontSize;
        public string FontSize
        {
            set { sFontSize = "--fontsize " + value; }
            get { return (sFontSize); }
        }

        public void SetLandScape(bool bRet)
        {
            if (bRet == false)
                sLandScape = "";
        }

        public void SetWaterMark(bool bRet)
        {
            if (bRet == false)
                waterMark = "";

        }

        public string RunWeb(string sUrl)
        {
            var sFileName = GetNewName();

            var pProcess = new System.Diagnostics.Process
                               {
                                   StartInfo =
                                       {
                                           FileName = directory + "\\ghtmldoc.exe",
                                           Arguments =
                                               "--webpage --quiet " + sFontSize + " --bodyfont Arial " + sLandScape +
                                               " -t pdf14 -f " + sFileName + ".pdf " + sUrl,
                                           WorkingDirectory = directory
                                       }
                               };

            pProcess.Start();

            return (sFileName + ".pdf");
        }

        public string Run(string sRawUrl, string newFileName)
        {
            var sUrlVirtual = sRawUrl; //HttpContext.Current.Request.Url.MakeRelativeUri(new Uri(sRawUrl)).ToString();
            var sw = new StringWriter();
            Server.Execute(sUrlVirtual, sw);

            return CreatePdfFromHtml(sw,newFileName);
        }

        public string Run(IHttpHandler httpHandler, string newFileName)
        {
            var sw = new StringWriter();

            Server.Execute(httpHandler, sw, false);

            return CreatePdfFromHtml(sw,newFileName);
        }

        private string CreatePdfFromHtml(StringWriter sw, string newFileName)
        {
            var sFileName = GetNewName();
            if (string.IsNullOrEmpty(newFileName))
            {
                newFileName = sFileName;
            }

            var sPage = directory; // Server.MapPath(directory); //+"/" + sFileName + ".html";

            var sWriter = File.CreateText(string.Format("{0}/{1}.html", sPage, sFileName));
            sWriter.WriteLine(sw.ToString());
            sWriter.Close();

            var pProcess = new System.Diagnostics.Process
                               {
                                   StartInfo =
                                       {
                                           FileName = directory + "\\ghtmldoc.exe",
                                           Arguments =
                                               "--webpage --quiet " + sFontSize + waterMark + " --bodyfont Arial " +
                                               sLandScape + " -t pdf14 -f " + newFileName + ".pdf " + sFileName + ".html",
                                           WorkingDirectory = directory
                                       }
                               };

            pProcess.Start();

         
            return (newFileName + ".pdf");
        }


        private static string GetNewName()
        {
            var sName = Guid.NewGuid();
            return (sName.ToString());
        }
    }
}
