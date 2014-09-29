using System;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IDocumentRepository
    {
        Document GetDocument(Guid documentId);

        void Save(Document document);
        
        void DeleteDocument(Guid documentId);
    }
}