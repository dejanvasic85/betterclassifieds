using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Paramount;
using Paramount.ApplicationBlock.Configuration;
using Paramount.DSL.UIController;
using Paramount.Utility;

namespace BetterClassified.UI.Handlers
{
    public class DocumentHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var clientCode = ConfigSettingReader.ClientCode;
            bool isRaw = false;
            Stream stream = null;

            var dslQuery = new DslQueryParam(context.Request.QueryString);

            Guid documentId;
            if (!Guid.TryParse(dslQuery.DocumentId, out documentId))
            {
                return;
            }

            int resolution = dslQuery.Resolution;
            
            if (ConfigSettingReader.DslIsServerCaching)
            {
                stream = UIController.ImageCacheController.Get(documentId.ToString(), dslQuery.Width, dslQuery.Height, dslQuery.Resolution, out isRaw);
            }

            if (stream == null)
            {
                // This is throwing whole bunch of errors if publication or other stuff don't have an image...
                // Silly
                stream = DslController.DownloadDocumentToStream(documentId, clientCode);
                isRaw = true;
            }

            // Load the image from the stream
            Image image = Image.FromStream(stream);

            if (isRaw && dslQuery.Width != null && dslQuery.Height != null)
            {
                // Prepare the image to be right size
                image = ImageHelper.Resize(image, (int)dslQuery.Width, (int)dslQuery.Height, dslQuery.Resolution);
            }

            // Prepare the image for better quality
            var qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            var jpegCodec = ImageHelper.GetEncoderInfo("image/jpeg");
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            // Serve the image to the client browser
            context.Response.ContentType = "image/jpeg";

            image.Save(context.Response.OutputStream, jpegCodec, encoderParams);

            if (ConfigSettingReader.DslIsServerCaching)
            {
                UIController.ImageCacheController.Put(image, documentId.ToString(), dslQuery.Width, dslQuery.Height, dslQuery.Resolution);
            }

            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }

            image.Dispose();
        }
    }
}
