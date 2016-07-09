using System;

namespace Paramount.Betterclassifieds.Business.DocumentStorage
{
    public interface IDocumentRepository
    {
        Document GetDocument(Guid documentId);

        void Create(Document document);
        
        void DeleteDocument(Guid documentId);

        void UpdateDocument(Document document);
    }
}