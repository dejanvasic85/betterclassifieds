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


    public class ImageController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IApplicationConfig _applicationConfig;

        public ImageController(IDocumentRepository documentRepository, IApplicationConfig applicationConfig)
        {
            _documentRepository = documentRepository;
            _applicationConfig = applicationConfig;
        }

        [HttpGet]
        [OutputCache(Duration = 60*60*72, VaryByParam = "*")] // 60 x 60 x 72 should equal 3 days
        public ActionResult Render(Guid documentId, int? height = null, int? width = null)
        {
            // Construct the temporary filepath to where the original image may be cached
            var targetFilePath = Path.Combine(_applicationConfig.ImageCacheDirectory, documentId.ToString().Append(".jpg"));

            // Use the ImageResult from the Simple.ImageResultLibrary to the work
            // And pass in the retrieval for the document
            return ImageResult.FromDocument(
                () => _documentRepository.GetDocument(documentId), 
                targetFilePath, width ?? 0, height ?? 0);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            var documentId = Guid.NewGuid();

            // Should be 1 uploaded file
            var uploadedFile = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].CastTo<HttpPostedFileBase>())
                .First(postedFile => postedFile != null && postedFile.ContentLength != 0);

            var imageDocument = new Document(documentId, uploadedFile.InputStream.FromStream(), uploadedFile.ContentType,
               uploadedFile.FileName, uploadedFile.ContentLength, this.User.Identity.Name);
            
            _documentRepository.Save(imageDocument);

            return Json(new { documentId }, JsonRequestBehavior.AllowGet);
        }
    }
}