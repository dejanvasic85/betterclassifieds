using System;
using System.IO;
using System.Web;

namespace BetterClassified.UI.Dsl
{
    public class WebImageMakerImageHelper
    {
        string workingDirectory;
        string keySuffix;

        public WebImageMakerImageHelper(string workingDirectory, string keySuffix)
        {
            this.workingDirectory = workingDirectory;
            this.keySuffix = keySuffix;
        }

        public void serveImage()
        {
            HttpRequest req = HttpContext.Current.Request;
            HttpResponse res = HttpContext.Current.Response;

            String mode = req.QueryString["mode" + keySuffix]; //   _" + WebImageMaker.WebImageMakerID];
            String imageName = req.QueryString["img" + keySuffix];

            if (imageName.Contains("\\") || imageName.Contains("/"))
            {
                // somebody is up to no good
                res.End();
                return;
            }

            if (imageName.EndsWith(".gif"))
            {
                res.ContentType = "image/gif";
            }
            else if (imageName.EndsWith(".jpg"))
            {
                res.ContentType = "image/jpeg";
            }
            else if (imageName.EndsWith(".png"))
            {
                res.ContentType = "image/png";
            }

            String filepath = "";
            switch (mode)
            {
                case "canvas":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.CanvasImageDirName);
                    break;
                case "web":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.WebImageDirName);
                    break;
                case "thumbnail":
                    filepath = Path.Combine(workingDirectory, WebImageMaker.ThumbnailImageDirName);
                    break;
            }

            filepath = Path.Combine(filepath, imageName);

            res.WriteFile(filepath);
            res.End();
        }
    }
}
