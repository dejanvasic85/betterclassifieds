using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Caching;
using System.Web.UI;
using Paramount.Betterclassifieds.Business.Repository;
using System;
using System.Web.Mvc;
using Paramount.Utility;

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
        [OutputCache(Duration = 120, VaryByParam = "documentId;height;width")]
        public ActionResult Render(Guid documentId, int? height = null, int? width = null)
        {
            // Fetch the document from repository
            var document = _documentRepository.GetDocument(documentId);

            if (height.HasValue && width.HasValue)
            {
                var resizedData = ImageHelper.ResizeFixedSize(document.Data, width.Value, height.Value);
                return File(resizedData, document.ContentType);
            }

            return File(document.Data, document.ContentType);
        }
    }
}