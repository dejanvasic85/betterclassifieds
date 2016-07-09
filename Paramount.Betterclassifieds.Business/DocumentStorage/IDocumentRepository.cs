using System;

namespace Paramount.Betterclassifieds.Business.DocumentStorage
{
    public interface IDocumentRepository
    {
        Document GetDocument(Guid documentId);
        T GetJsonDocument<T>(Guid documentId) where T : class;
        void Create(Document document);
        Document CreateJsonDocument<T>(T obj) where T : class;
        Document CreateJsonDocument<T>(Guid documentId, T obj) where T : class;
        void UpdateDocument(Document document);
        void CreateOrUpdateJsonDocument<T>(Guid id, T obj) where T : class;
        void DeleteDocument(Guid documentId);
    }
}