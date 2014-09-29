using System.Linq;
using System;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    using Business;
    using Business.Repository;

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

        public void Save(Document document)
        {
            using (var context = new DocumentContext())
            {
                context.Documents.Add(document);
                context.SaveChanges();
            }
        }

        public void DeleteDocument(Guid documentId)
        {
            using (var context = new DocumentContext())
            {
                var toRemove = context.Documents.FirstOrDefault(d => d.DocumentId == documentId);
                context.Documents.Remove(toRemove);
                context.SaveChanges();
            }
        }
    }
}
