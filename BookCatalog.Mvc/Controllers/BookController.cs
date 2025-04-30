using BookCatalog.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BookCatalog.Mvc.Controllers
{
    public class BookController(IHttpClientFactory httpClientFactory) : Controller
    {
        private HttpClient Client => httpClientFactory.CreateClient("api");
        [HttpGet("create")]
        public IActionResult CreateBook()
        {
            return View();
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromForm] BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Geçersiz model verisi gönderildi.");
                return View(bookViewModel);
            }

            var dto = new BookViewModel
            {
                Title = bookViewModel.Title,
                Author = bookViewModel.Author,
                Genre = bookViewModel.Genre,
                PageCount = bookViewModel.PageCount
            };

            var response = await Client.PostAsJsonAsync("add-book", dto);
            if (response.IsSuccessStatusCode)
            {
                Log.Information("Yeni kitap eklendi: {@Book}", dto);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Log.Error("Kitap ekleme sırasında hata oluştu: {@Book}", dto);
                ModelState.AddModelError(string.Empty, "Kitap Eklenirken Hata Oluştu");
                return View(bookViewModel);
            }
        }
        [HttpGet]
        public IActionResult EditBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditBook([FromRoute] int id, [FromForm] BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Geçersiz model verisi güncelleme sırasında gönderildi.");
                return View(bookViewModel);
            }

            var dto = new BookViewModel
            {
                Title = bookViewModel.Title,
                Author = bookViewModel.Author,
                Genre = bookViewModel.Genre,
                PageCount = bookViewModel.PageCount
            };

            var response = await Client.PutAsJsonAsync($"update-book/{id}", dto);
            if (response.IsSuccessStatusCode)
            {
                Log.Information("Kitap güncellendi (ID: {Id}): {@Book}", id, dto);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Log.Error("Kitap güncellenirken hata oluştu (ID: {Id}): {@Book}", id, dto);
                ModelState.AddModelError(string.Empty, "Kitap Güncellenirken Hata Oluştu");
                return View(bookViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var response = await Client.DeleteAsync($"delete-book/{id}");
            if (response.IsSuccessStatusCode)
            {
                Log.Information("Kitap silindi (ID: {Id})", id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Log.Error("Kitap silinirken hata oluştu (ID: {Id})", id);
                ModelState.AddModelError(string.Empty, "Kitap Silinirken Hata Oluştu");
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBookByIdDetail([FromRoute] int id)
        {
            if (id <= 0)
            {
                Log.Warning("Geçersiz kitap ID gönderildi: {Id}", id);
                return BadRequest("Id is not valid");
            }

            var response = await Client.GetAsync($"get-by/{id}");
            if (response.IsSuccessStatusCode)
            {
                var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
                Log.Information("Kitap detayları getirildi: {@Book}", book);
                return View(book);
            }
            else
            {
                Log.Error("Kitap detayları alınamadı (ID: {Id})", id);
                ModelState.AddModelError(string.Empty, "Kitap Detayları Alınırken Hata Oluştu");
                return View();
            }
        }
    }
}