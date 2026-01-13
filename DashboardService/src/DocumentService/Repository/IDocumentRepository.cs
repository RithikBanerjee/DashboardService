using DAL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentService.Repository
{
    public interface IDocumentRepository
    {
        public Task<IEnumerable<Document>> GetDocuments();

        public Task<Document> GetDocument(int id);

        public Task<Document> PostDocument(Document document);

        public Task PutDocument(int id, Document document);

        public Task DeleteDocument(int id);
    }
}
