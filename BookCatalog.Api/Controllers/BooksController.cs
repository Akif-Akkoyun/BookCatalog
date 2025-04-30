using BookCatalog.Api.Dtos;
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
            if (books is null || !books.Any())
                return NotFound("Hiç kitap bulunamadı.");
            var bookListDto = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                PageCount = b.PageCount
            }).ToList();
            return Ok(bookListDto);
        }
        [HttpGet("/get-by/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await repo.GetByIdAsync<Book>(id);
            if (book is null)
                return NotFound();
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PageCount = book.PageCount
            };
            return Ok(bookDto);
        }
        [HttpPost("/add-book")]
        public async Task<IActionResult> AddBook([FromBody] BookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookDto = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
                PageCount = dto.PageCount
            };
            await repo.AddAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }
        [HttpPut("/update-book/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var book = await repo.GetByIdAsync<Book>(id);
            if (book is null)
                return NotFound("Belirtilen Id'de kitap bulunamadı.");
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Genre = dto.Genre;
            book.PageCount = dto.PageCount;
            await repo.UpdateAsync(book);
            return Ok("Güncelleme İşlemi Başarılı");
        }
        [HttpDelete("/delete-book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await repo.GetByIdAsync<Book>(id);
            if (book is null)
                return NotFound("Belirtilen Id'de kitap bulunamadı.");
            await repo.DeleteAsync<Book>(id);
            return Ok("Silme İşlemi Başarılı");
        }
    }
}