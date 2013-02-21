namespace Paramount.Services.Host.Core.Dsl
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Web.Services;
    using ApplicationBlock.Logging.EventLogging;
    using Common.DataTransferObjects.DSL.Messages;
    using Utility;
    using Utility.Dsl;
    
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageHandler : IHttpHandler
    {
        private const int StartGuidLength = 8;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var queryParam = new DslQueryParam(context.Request.QueryString);

                var documentId = new Guid(queryParam.DocumentId);
                var cacheDocId = documentId.ToString().Substring(0, StartGuidLength);
                var entityCode = CryptoHelper.Decrypt(HttpUtility.UrlDecode(queryParam.Entity));

                int resolution = queryParam.Resolution;
                decimal? height = queryParam.Height;
                decimal? width = queryParam.Width;

                // Declare the image to be served
                Image image = null;

                // Check if caching is enabled and attempt to fetch from cache first
                if (Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslIsServerCaching)
                {
                    image = GetImageFromServerCache(cacheDocId, entityCode, resolution, height, width);
                }

                // otherwise, fetch from the DB
                if (image == null)
                {
                    // Fetch the image from Db repository via the service method
                    var dslService = new DslService();
                    var request = new GetDslDocumentRequest { DocumentId = documentId, EntityCode = entityCode };
                    var response = dslService.GetDslDocument(request);
                    image = Image.FromStream(response.FileData);

                    // Perform resize if required
                    if ((width != null) && (height != null))
                    {
                        image = ImageHelper.ResizeFixedSize(image, (int)width, (int)height, resolution, true);
                    }
                }

                // Prepare the image after resize
                var qualityParam = new EncoderParameter(Encoder.Quality, 100L);
                var jpegCodec = ImageHelper.GetEncoderInfo("image/jpeg");
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                // Serve the image to the client browser
                image.Save(context.Response.OutputStream, jpegCodec, encoderParams);

                // Save the image to disk cache. If already there, then set last modified date.
                SaveImageToServerCache(image, cacheDocId, entityCode, resolution, height, width);

                // Release all the memory for this image
                image.Dispose();
            }
            catch (ArgumentNullException argumentNullException)
            {
                EventLogManager.Log(argumentNullException);
                context.Response.Write(string.Format("Invalid Arguments. Error: {0}", argumentNullException));
            }
            catch (Exception ex)
            {
                EventLogManager.Log(ex);
                throw ex;
            }
        }

        private static Image GetImageFromServerCache(string documentId, string entityCode, int resolution, decimal? height, decimal? width)
        {
            Image imageCache = null;

            if (Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslImageCacheDirectory != null)
            {
                var fileName = string.Format("{0}_{1}_{2}", entityCode, documentId, resolution);

                if (height != null && width != null)
                {
                    fileName = string.Format("{0}_{1}_{2}", fileName, (int)height, (int)width);
                }
                
                // Set the full name of the filename including the directory
                fileName = Path.Combine(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslImageCacheDirectory.FullName, fileName);

                if (File.Exists(fileName))
                {
                    imageCache = Image.FromFile(Path.Combine(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslImageCacheDirectory.FullName, fileName));
                }
            }

            return imageCache;
        }

        private static void SaveImageToServerCache(Image imageToSave, string documentId, string entityCode, int resolution, decimal? height, decimal? width)
        {
            // Ensure that the Cache Directory exists
            if (Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslIsServerCaching && Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslImageCacheDirectory != null)
            {
                // Construct the filename
                var fileName = string.Format("{0}_{1}_{2}", entityCode, documentId, resolution);

                if (height != null && width != null)
                {
                    fileName = string.Format("{0}_{1}_{2}", fileName, (int)height, (int)width);
                }

                // Combine full directory with name to get full directory + name
                fileName = Path.Combine(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.DslImageCacheDirectory.FullName, fileName);

                if (File.Exists(fileName))
                {
                    // Dispose any current resources to the file
                    imageToSave.Dispose();

                    // Because it's being fetched again, reset the last modified date so that it resets the cache timer
                    File.SetLastWriteTime(fileName, DateTime.Now);

                    return;
                }

                // Save the image to disk now
                imageToSave.Save(fileName);
            }
        }

        private static void OutputCacheResponse(HttpContext context, DateTime lastModified)
        {
            HttpCachePolicy cachePolicy = context.Response.Cache;
            cachePolicy.SetCacheability(HttpCacheability.Public);
            cachePolicy.VaryByParams["p"] = true;
            cachePolicy.SetOmitVaryStar(true);
            cachePolicy.SetExpires(DateTime.Now + TimeSpan.FromDays(365));
            cachePolicy.SetValidUntilExpires(true);
            cachePolicy.SetLastModified(lastModified);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
