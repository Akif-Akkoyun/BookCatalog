using BookCatalog.Data.Entities;
using BookCatalog.Data.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IDataRepository repo) : ControllerBase
    {
        [HttpGet("/all-book-list")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await repo.GetAllAsync<Book>();
            return Ok(books);
        }
        [HttpGet("/get-by/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await repo.GetByIdAsync<Book>(id);
            if (book is null)
                return NotFound();
            return Ok(book);
        }
        [HttpPost("/add-book")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            await repo.AddAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        [HttpPut("/update-book/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest("Güncelleme Yaparken Sorun Oluştu...");
            await repo.UpdateAsync(book);
            return Ok("Güncelleme İşlemi Başarılı...");
        }
        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await repo.DeleteAsync<Book>(id);
            return Ok("Silme İşlemi Başarılı");
        }
    }
}