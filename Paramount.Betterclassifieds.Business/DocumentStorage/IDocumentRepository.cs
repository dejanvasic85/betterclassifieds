using System;

namespace Paramount.Betterclassifieds.Business.DocumentStorage
{
    public interface IDocumentRepository
    {
        Document GetDocument(Guid documentId);

        void Save(Document document);
        
        void DeleteDocument(Guid documentId);
    }
}