using Paramount.Betterclassifieds.Business.Repository;
using System;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IDocumentRepository _documentRepository;

        public ImageController(IDocumentRepository documentRepository) 
        {
            _documentRepository = documentRepository;
        }

        [HttpGet]
        public ActionResult Id(Guid documentId, int? height = null, int? width = null)
        {
            // Fetch the document from repository
            var image = _documentRepository.GetDocument(documentId);

            return File(image.Data, image.ContentType);
        }
    }
}