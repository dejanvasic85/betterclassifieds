namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business;
    using Business.DocumentStorage;
    using Framework;
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class ImageController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;

        public ImageController(IDocumentRepository documentRepository, IApplicationConfig applicationConfig, IClientConfig clientConfig)
        {
            _documentRepository = documentRepository;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
        }

        [HttpGet]
        [OutputCache(Duration = 60 * 60 * 72, VaryByParam = "*")] // 60 x 60 x 72 should equal 3 days
        public ActionResult Render(Guid documentId, int? height = null, int? width = null)
        {
            // Construct the temporary filepath to where the original image may be cached
            var targetFilePath = Path.Combine(_applicationConfig.ImageCacheDirectory, documentId.ToString().Append(".jpg"));

            // Use the ImageResult from the Simple.ImageResultLibrary to the work
            // And pass in the retrieval for the document
            return ImageResult.FromDocument(() => _documentRepository.GetDocument(documentId), targetFilePath, width ?? 0, height ?? 0);
        }

        [HttpPost]
        public ActionResult UploadCropImage()
        {
            var documentId = Guid.NewGuid();

            // Should be 1 uploaded file
            var uploadedFile = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].As<HttpPostedFileBase>())
                .First(postedFile => postedFile != null && postedFile.ContentLength != 0);

            // Store on the disk for now to see if this will work
            var fileName = string.Format("{0}.jpg", documentId);

            uploadedFile.SaveAs(string.Format("{0}{1}", _applicationConfig.ImageCropDirectory.FullName, fileName));

            return Json(new { documentId = fileName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderCropImage(string documentId)
        {
            return File(string.Format("{0}{1}", _applicationConfig.ImageCropDirectory, documentId), "image/jpeg");
        }

        [HttpPost]
        public ActionResult CropImage(string documentId, int x, int y, int width, int height)
        {
            // Crop the image
            var file = new FileInfo(Path.Combine(_applicationConfig.ImageCropDirectory.FullName, documentId));
            if (!file.Exists)
            {
                return Json(false);
            }

            // Store the cropped img to the document database 
            using (var img = Image.FromFile(file.FullName))
            using (var stream = new MemoryStream())
            {
                img
                    .Crop(x, y, width, height)
                    .Resize(_clientConfig.PrintImagePixelsWidth, _clientConfig.PrintImagePixelsHeight, _clientConfig.PrintImageResolution)
                    .Save(stream, ImageFormat.Jpeg);

                var document = new Document(new Guid(documentId.WithoutFileExtension()), stream.ToArray(), "image/jpeg", documentId, (int)stream.Length);

                _documentRepository.Save(document);
            }

            // Clean the existing document
            try
            {
                file.Delete();
            }
            catch (IOException)
            {
                // Don't worry... there will be a clean up process that runs later
            }

            return Json(documentId.WithoutFileExtension());
        }

        [HttpPost]
        public ActionResult RemoveImage(string documentId)
        {
            // The user decided not to go ahead with the prepared image
            // So destroy it from the document service
            _documentRepository.DeleteDocument(new Guid(documentId.WithoutFileExtension()));

            return Json(true);
        }

        [HttpPost]
        public ActionResult CancelCrop(string documentId)
        {
            // Crop the image
            var file = new FileInfo(Path.Combine(_applicationConfig.ImageCropDirectory.FullName, documentId));
            if (!file.Exists)
            {
                return Json(true);
            }
            // Clean the existing document
            try
            {
                file.Delete();
                return Json(true);
            }
            catch (IOException)
            {
                // Couldn't clean it
                return Json(false);
            }
        }
    }
}