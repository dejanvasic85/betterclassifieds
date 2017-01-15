using ZXing;
using ZXing.Common;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business;
    using Business.DocumentStorage;
    using Framework;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ImageController : ApplicationController
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

            _documentRepository.Create(imageDocument);

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
            var fileName = $"{Guid.NewGuid()}.jpg";

            uploadedFile.SaveAs($"{_applicationConfig.ImageCropDirectory.FullName}{fileName}");

            return Json(new { documentId = fileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadEventFloorplanUrl()
        {
            var uploadedFile = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].As<HttpPostedFileBase>())
                .FirstOrDefault(postedFile => postedFile != null && postedFile.ContentLength != 0);

            if (uploadedFile == null)
                throw new ArgumentNullException("There seems to be no uploaded file");

            // We accept pdf and images for floor plans
            // and then handle it in javascript (see eventView.ui)
            var acceptedImages = new List<string> { ContentType.Pdf }.Union(_applicationConfig.AcceptedImageFileTypes);
            if (!acceptedImages.Any(accepted => accepted.Equals(uploadedFile.ContentType, StringComparison.OrdinalIgnoreCase)))
            {
                return Json(new { errorMsg = "Not an accepted file type." });
            }

            if (uploadedFile.ContentLength > _applicationConfig.MaxImageUploadBytes)
            {
                return Json(new { errorMsg = "The file exceeds the maximum file size." });
            }

            var floorplanDocument = new Document(Guid.NewGuid(), uploadedFile.InputStream.FromStream(), uploadedFile.ContentType,
                uploadedFile.FileName, uploadedFile.ContentLength, this.User.Identity.Name);

            _documentRepository.Create(floorplanDocument);

            return Json(new { floorplanDocument.DocumentId, floorplanDocument.FileName });
        }

        public ActionResult RenderCropImage(string fileName)
        {
            return File(string.Format("{0}{1}", _applicationConfig.ImageCropDirectory, fileName), ContentType.Jpeg);
        }

        [HttpPost]
        public ActionResult CropImage(string fileName, int x, int y, int width, int height, bool resizeForPrint)
        {
            // Crop the image
            var file = new FileInfo(Path.Combine(_applicationConfig.ImageCropDirectory.FullName, fileName));
            if (!file.Exists)
            {
                return Json(false);
            }

            var documentId = new Guid(fileName.WithoutFileExtension());

            // Store the cropped img to the document database 
            using (var img = Image.FromFile(file.FullName))
            using (var stream = new MemoryStream())
            {
                var cropped = img.Crop(x, y, width, height);
                if (resizeForPrint)
                {
                    cropped = cropped.Resize(_clientConfig.PrintImagePixelsWidth, _clientConfig.PrintImagePixelsHeight, _clientConfig.PrintImageResolution);
                }
                cropped.Save(stream, ImageFormat.Jpeg);
                var document = new Document(documentId, stream.ToArray(), "image/jpeg", fileName, (int)stream.Length);
                _documentRepository.Create(document);
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

        public ActionResult Barcode()
        {
            var data = DateTime.Now;
            var documentId = Guid.NewGuid();

            // Use the third party library here
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Height = 250, Width = 250, Margin = 0}
            };

            using (var bitmap = barcodeWriter.Write(data.ToString()))
            {
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Gif);
                    var barcodeByteArray = stream.ToArray();
                    _documentRepository.Create(new Document(documentId,
                        barcodeByteArray,
                        ContentType.Gif,
                        documentId+".gif",
                        barcodeByteArray.Length));
                }
            }


            return View(documentId);
        }
    }
}