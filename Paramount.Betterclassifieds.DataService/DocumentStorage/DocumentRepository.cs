using System.Linq;
using System;
using System.Data.Entity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.DocumentStorage;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public DocumentRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Document GetDocument(Guid documentId)
        {
            using (var context = new DocumentContext())
            {
                var document = context.Documents.FirstOrDefault(d => d.DocumentId == documentId);
                return document;
            }
        }

        public void Create(Document document)
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

        public void UpdateDocument(Document document)
        {
            using (var context = _dbContextFactory.CreateDocumentContext())
            {
                context.Documents.Attach(document);
                context.Entry(document).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
