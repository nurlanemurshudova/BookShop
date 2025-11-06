using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BookController : Controller
    {
        AuthorManager _authorService = new();
        BookManager _bookService = new();
        public IActionResult Index()
        {
            return View();
        }
    }
}
