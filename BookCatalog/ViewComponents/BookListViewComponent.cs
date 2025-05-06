using BookCatalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.ViewComponenets
{
    public class BookListViewComponent(ApplicationDbContext dbContext) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }
    }
}
