using BookShopWeb.ViewModels;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IBookAuthorService _bookAuthorService;
        public HomeController(IBookService bookService,IAuthorService authorService, IBookAuthorService bookAuthorService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
        }

        public IActionResult Index()
        {
            var bookData = _bookService.GetAll().Data;
            var authorData = _authorService.GetAll().Data;
            var bookAuthorData = _bookAuthorService.GetAll().Data;

            HomeViewModel model = new ()
            {
                Books = bookData,
                Authors = authorData,
                BookAuthors = bookAuthorData
            };
            return View(model);
        }

    }
}
