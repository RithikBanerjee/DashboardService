using DAL;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentService.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        // Implementation of data access methods for Document entity
        private readonly DashboardDbContext _context;

        public DocumentRepository(DashboardDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> GetDocument(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task<Document> PostDocument(Document document)
        {
            document.CreatedAt = DateTime.UtcNow;
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task PutDocument(int id, Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_context.Documents.Any(e => e.Id == id))
                {
                    throw new Exception("Not found");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
