using System.IO;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Paramount.Betterclassifieds.Business;
using System;

namespace Paramount.Betterclassifieds.Presentation.Framework
{
    public class ImageMvcResizer : Simple.ImageResizer.MvcExtensions.ImageResult
    {
        public ImageMvcResizer(string filePath, int width = 0, int height = 0)
            : base(filePath, width, height)
        {
            
        }

        public static Simple.ImageResizer.MvcExtensions.ImageResult FromDocument(Func<Document> fetcher, string targetFilePath, int width = 0, int height = 0)
        {
            if (File.Exists(targetFilePath))
            {
                // The original file has already been cached so just get that one
                return new Simple.ImageResizer.MvcExtensions.ImageResult(targetFilePath, width, height);
            }

            // Fetch it from the Db
            var document = fetcher();

            if (document == null)
                return null;

            File.WriteAllBytes(targetFilePath, document.Data);
            return new Simple.ImageResizer.MvcExtensions.ImageResult(targetFilePath, width, height);
        }

    }
}