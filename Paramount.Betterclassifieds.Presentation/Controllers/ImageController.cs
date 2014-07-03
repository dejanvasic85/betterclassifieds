using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.Presentation.Framework;
using System;
using System.IO;
using System.Web.Caching;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
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
    }
}