using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using System.Web.SessionState;
using Paramount.ApplicationBlock.Configuration;
using Paramount.DSL.UIController;
using Paramount.Utility;
using Paramount.Modules.Logging.UIController;

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
            var groupingId = Guid.NewGuid().ToString();

            try
            {
                var isServerCaching = ConfigSettingReader.DslIsServerCaching;
                var clientCode = ConfigSettingReader.ClientCode;
                bool isRaw = false;
                Stream stream = null;
                Image image = null; // Image to be served
                
                var dslQuery = new DslQueryParam(context.Request.QueryString);

                var documentId = dslQuery.DocumentId;
                decimal? width = dslQuery.Width;
                decimal? height = dslQuery.Height;
                int resolution = dslQuery.Resolution;

                if (isServerCaching)
                {
                    stream = BetterClassified.UIController.ImageCacheController.Get(documentId, width, height, resolution, out isRaw);
                }

                if (stream == null)
                {
                    // We have to fetch the original!
                    // Download the image from Paramount Services
                    
                    // Method - Download File
                    // stream = GetDslImageStream(clientCode, documentId);                   
                    // Method - WebRequest
                    stream =  GetDslImageStream(context, clientCode, documentId, width, height, resolution);

                    isRaw = true;
                }

                // Load the image from the stream
                image = Image.FromStream(stream);

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
                image.Save(context.Response.OutputStream, jpegCodec, encoderParams);

                if (isServerCaching)
                {
                    BetterClassified.UIController.ImageCacheController.Put(image, documentId, width, height, resolution);
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
            }
        }

        /// <summary>
        /// Using DownloadFile method
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static Stream GetDslImageStream(string clientCode, string documentId)
        {
            FileInfo rawFile = BetterClassified.UIController.ImageCacheController.GetRawCacheFile(documentId);
            string downloadPath = rawFile.FullName;

            if (!rawFile.Exists)
            {
                // If raw file has not been downloaded yet, then first download it
                DslController.DownloadFile(new Guid(documentId), clientCode, downloadPath);
            }

            FileStream stream = new FileStream(downloadPath, FileMode.Open);
            return stream;
        }

        /// <summary>
        /// Using WebRequest method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="clientCode"></param>
        /// <param name="documentId"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public static Stream GetDslImageStream(HttpContext context, string clientCode, string documentId, decimal? width, decimal? height, int resolution)
        {
            var encryptedCode = CryptoHelper.Encrypt(clientCode);

            DslQueryParam queryParam = new DslQueryParam(context.Request.QueryString)
            {
                DocumentId = documentId,
                Entity = encryptedCode,
                Height = height,
                Width = width,
                Resolution = resolution
            };

            var handlerUrl = queryParam.GenerateUrl(ConfigSettingReader.DslImageHandler);

            WebRequest request = WebRequest.Create(handlerUrl);
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            return httpResponse.GetResponseStream();
        }
    }
}
