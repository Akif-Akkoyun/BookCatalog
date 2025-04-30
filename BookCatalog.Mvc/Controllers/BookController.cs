using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Mvc.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
