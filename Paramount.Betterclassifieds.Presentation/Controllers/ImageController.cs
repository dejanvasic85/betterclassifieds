using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Utility;
using System;
using System.Web.Caching;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ImageController : Controller
    {
        private readonly IDocumentRepository _documentRepository;

        public ImageController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpGet]
        [OutputCache(Duration = 120, VaryByParam = "documentId;height;width")]
        public ActionResult Render(Guid documentId, int? height = null, int? width = null)
        {
            // Fetch the document from repository
            var document = _documentRepository.GetDocument(documentId);

            if (document == null)
                return new EmptyResult();

            // Only resize the image if both values are provided
            if (height.HasValue && width.HasValue)
            {
                // Use the helper to do the work...
                var resizedData = ImageHelper.ResizeFixedSize(document.Data, width.Value, height.Value);
                return File(resizedData, document.ContentType);
            }

            return File(document.Data, document.ContentType);
        }
    }
}