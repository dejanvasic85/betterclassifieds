using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Paramount.ApplicationBlock.Configuration;
using Paramount.DSL.UIController;
using Paramount.Modules.Logging.UIController;
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
            try
            {
                var clientCode = ConfigSettingReader.ClientCode;
                bool isRaw = false;
                Stream stream = null;

                var dslQuery = new DslQueryParam(context.Request.QueryString);

                var documentId = dslQuery.DocumentId;
                decimal? width = dslQuery.Width;
                decimal? height = dslQuery.Height;
                int resolution = dslQuery.Resolution;

                if (ConfigSettingReader.DslIsServerCaching)
                {
                    stream = UIController.ImageCacheController.Get(documentId, width, height, resolution, out isRaw);
                }

                if (stream == null)
                {
                    stream = DslController.DownloadDocumentToStream( new Guid(documentId), clientCode);
                    isRaw = true;
                }

                // Load the image from the stream
                Image image = Image.FromStream(stream);

                if (isRaw && width != null && height != null)
                {
                    // Prepare the image to be right size
                    image = ImageHelper.ResizeFixedSize(image, (int)width, (int)height, resolution);
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
                    UIController.ImageCacheController.Put(image, documentId, width, height, resolution);
                }
                
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }

                image.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionLogController<Exception>.AuditException(ex);
                throw;
            }
        }
    }
}
