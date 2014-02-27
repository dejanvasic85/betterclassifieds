using Paramount.Betterclassifieds.Business.Repository;
using System;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        public Document GetDocument(Guid documentId)
        {
            return new Document();
        }
    }
}
