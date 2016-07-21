using System.Linq;
using System;
using System.Data.Entity;
using System.Text;
using Newtonsoft.Json;
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
            using (var context = _dbContextFactory.CreateDocumentContext())
            {
                var document = context.Documents.FirstOrDefault(d => d.DocumentId == documentId);
                return document;
            }
        }

        public T GetJsonDocument<T>(Guid documentId) where T : class
        {
            // Retrieves document as json object
            var doc = GetDocument(documentId);
            if (doc == null)
                return null;

            var str = Encoding.UTF8.GetString(doc.Data);
            return JsonConvert.DeserializeObject<T>(str);
        }

        public void Create(Document document)
        {
            using (var context = _dbContextFactory.CreateDocumentContext())
            {
                context.Documents.Add(document);
                context.SaveChanges();
            }
        }

        public Document CreateJsonDocument<T>(T obj) where T : class
        {
            return CreateJsonDocument(Guid.NewGuid(), obj);
        }

        public Document CreateJsonDocument<T>(Guid documentId, T obj) where T : class
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var data = Encoding.UTF8.GetBytes(json);

            var doc = new Document(documentId,
                data,
                ContentType.Json,
                $"{documentId}.json",
                data.Length);

            Create(doc);

            return doc;
        }

        public void CreateOrUpdateJsonDocument<T>(Guid id, T obj) where T : class
        {
            var newData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
            var document = GetDocument(id);

            if (document == null)
            {
                CreateJsonDocument(id, obj);
                return;
            }

            document.Data = newData;
            document.UpdatedDate = DateTime.Now;
            UpdateDocument(document);
        }

        public void DeleteDocument(Guid documentId)
        {
            using (var context = _dbContextFactory.CreateDocumentContext())
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
