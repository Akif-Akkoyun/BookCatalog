using BookCatalog.Data;
using BookCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BookCatalog.Controllers
{
    public class BookController(ApplicationDbContext dbContext) : Controller
    {
        [HttpGet("/book-by/{id}/detail")]
        public async Task<IActionResult> GetBookByIdDetail(int id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book is null)
            {
                Log.Warning("Kitap bulunamadı. Id: {BookId}", id);
                return NotFound();
            }

            Log.Information("Kitap detayları getirildi: {@Book}", book);
            return View(book);
        }
        [HttpGet("/create-book")]
        public IActionResult CreateBook()
        {
            return View();
        }
        [HttpPost("/create-book")]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();
                Log.Information("Yeni kitap oluşturuldu: {@Book}", book);
                return RedirectToAction("Index", "Home");
            }
            return View(book);
        }
        [HttpGet("edit-book/{id}")]
        public async Task<IActionResult> EditBook(int id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book is null)
            {
                Log.Warning("Düzenlenecek kitap bulunamadı. Id: {BookId}", id);
                return NotFound();
            }
            Log.Information("Kitap düzenleme sayfası açıldı: {@Book}", book);
            return View(book);
        }
        [HttpPost("edit-book/{id}")]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                dbContext.Books.Update(book);
                await dbContext.SaveChangesAsync();
                Log.Information("Kitap güncellendi: {@Book}", book);
                return RedirectToAction("Index", "Home");
            }
            return View(book);
        }
        [HttpGet("delete-book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book is null)
            {
                Log.Warning("Silinecek kitap bulunamadı. Id: {BookId}", id);
                return NotFound();
            }
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            Log.Information("Kitap silindi: {@Book}", book);
            return RedirectToAction("Index", "Home");
        }
    }
}