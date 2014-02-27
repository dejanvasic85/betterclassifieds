using System.Linq;
using Paramount.Betterclassifieds.Business.Repository;
using System;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        public Document GetDocument(Guid documentId)
        {
            using (var context = new DocumentContext())
            {
                var document = context.Documents.FirstOrDefault(d => d.DocumentId == documentId);

                return document;
            }
        }
    }
}
