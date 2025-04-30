using BookCatalog.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BookCatalog.Mvc.ViewComponents
{
    public class BookListViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BookListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("all-book-list");
            if (response.IsSuccessStatusCode)
            {
                var books = await response.Content.ReadFromJsonAsync<IEnumerable<BookViewModel>>();
                Log.Information("Kitap listesi görüntülendi.");
                return View(books);
            }
            return View(Enumerable.Empty<BookViewModel>());
        }
    }
}