using System;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.DocumentStorage;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ActionResult File(string id)
        {
            Guid documentId;
            if (!Guid.TryParse(id, out documentId))
                return RedirectToAction("NotFound", "Error");

            var document = _documentRepository.GetDocument(documentId);
            if(document == null)
                return RedirectToAction("NotFound", "Error");

            return File(document.Data, document.ContentType);
        }
    }
}