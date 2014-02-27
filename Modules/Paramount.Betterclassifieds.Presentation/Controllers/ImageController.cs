using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        public ActionResult Id(Guid documentId, int? height = null, int? width = null)
        {
            // Fetch the document from repository
            var document = _documentRepository.GetDocument(documentId);

            if (height.HasValue && width.HasValue)
            {
                using (MemoryStream stream = new MemoryStream(document.Data))
                {
                    var image = Image.FromStream(stream);

                    MemoryStream saveToStream = new MemoryStream();
                    ImageHelper.ResizeFixedSize(image, width.Value, height.Value, 96).Save(saveToStream, ImageFormat.Jpeg);
                    
                    return File(saveToStream, document.ContentType);
                }
            }

            return File(document.Data, document.ContentType);
        }
    }
}