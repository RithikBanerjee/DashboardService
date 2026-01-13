using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using DocumentService.Repository;
using DAL.Data;

namespace DocumentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "DocumentAPI")] // Require JWT authentication for all actions
    public class DocumentController : ControllerBase
    {
        private readonly DocumentRepository _repo;

        public DocumentController(DocumentRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Document
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _repo.GetDocuments();
            return Ok(documents);
        }

        // GET: api/Document/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _repo.GetDocument(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        // POST: api/Document
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            var newDocument = await _repo.PostDocument(document);
            return CreatedAtAction(nameof(GetDocument), new { id = newDocument.Id }, newDocument);
        }

        // PUT: api/Document/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }
            try
            {
                await _repo.PutDocument(id, document);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE: api/Document/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            _repo.DeleteDocument(id);
            return NoContent();
        }
    }
}
