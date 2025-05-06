using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
