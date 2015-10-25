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
        public ActionResult UploadOnlineImage()
        {
            var files = Request.Files.Cast<string>()
               .Select(file => Request.Files[file].As<HttpPostedFileBase>())
               .Where(postedFile => postedFile != null && postedFile.ContentLength != 0)
               .ToList();

            // There should only be 1 uploaded file so just check the size ...
            var uploadedFile = files.Single();
            if (uploadedFile.ContentLength > _applicationConfig.MaxImageUploadBytes)
            {
                return Json(new { errorMsg = "The file exceeds the maximum file size." });
            }

            if (!_applicationConfig.AcceptedImageFileTypes.Any(type => type.Equals(uploadedFile.ContentType)))
            {
                return Json(new { errorMsg = "Not an accepted file type." });
            }

            var documentId = Guid.NewGuid();

            var imageDocument = new Document(documentId, uploadedFile.InputStream.FromStream(), uploadedFile.ContentType,
                uploadedFile.FileName, uploadedFile.ContentLength, this.User.Identity.Name);

            _documentRepository.Save(imageDocument);

            return Json(new { documentId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadCropImage()
        {
            // Should be 1 uploaded file
            var uploadedFile = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].As<HttpPostedFileBase>())
                .First(postedFile => postedFile != null && postedFile.ContentLength != 0);

            // Store on the disk for now to see if this will work
            var fileName = string.Format("{0}.jpg", Guid.NewGuid());
            
            uploadedFile.SaveAs(string.Format("{0}{1}", _applicationConfig.ImageCropDirectory.FullName, fileName));

            return Json(new { documentId = fileName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderCropImage(string fileName)
        {
            return File(string.Format("{0}{1}", _applicationConfig.ImageCropDirectory, fileName), "image/jpeg");
        }

        [HttpPost]
        public ActionResult CropImage(string fileName, int x, int y, int width, int height)
        {
            // Crop the image
            var file = new FileInfo(Path.Combine(_applicationConfig.ImageCropDirectory.FullName, fileName));
            if (!file.Exists)
            {
                return Json(false);
            }

            var documentId = new Guid( fileName.WithoutFileExtension() );

            // Store the cropped img to the document database 
            using (var img = Image.FromFile(file.FullName))
            using (var stream = new MemoryStream())
            {
                img
                    .Crop(x, y, width, height)
                    .Resize(_clientConfig.PrintImagePixelsWidth, _clientConfig.PrintImagePixelsHeight, _clientConfig.PrintImageResolution)
                    .Save(stream, ImageFormat.Jpeg);

                var document = new Document(documentId, stream.ToArray(), "image/jpeg", fileName, (int)stream.Length);

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

            return Json(documentId);
        }
        
        [HttpPost]
        public ActionResult CancelCrop(string fileName)
        {
            // Crop the image
            var file = new FileInfo(Path.Combine(_applicationConfig.ImageCropDirectory.FullName, fileName));
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

        [HttpGet]
        public ActionResult Barcode(string data)
        {
            var generator = new Spire.Barcode.BarCodeGenerator(new Spire.Barcode.BarcodeSettings
            {
                Data = data,
                BarHeight = 10,
                ImageHeight = 12,
                Type = Spire.Barcode.BarCodeType.Codabar,
                ShowText = false,
                BorderWidth = 20,
                ImageWidth = 30
            });
            
            var img = generator.GenerateImage();
            var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return File(stream.ToArray(), ContentType.Png);
        }
    }
}