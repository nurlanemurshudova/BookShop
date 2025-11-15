using Business.Abstract;
using Business.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _env;
        public BookController(IAuthorService authorService, IBookService bookService, IWebHostEnvironment env)
        {
            _authorService = authorService;
            _bookService = bookService;
            _env = env;
        }

        //AuthorManager _authorService = new();
        //BookManager _bookService = new();
        public IActionResult Index()
        {
            var data = _bookService.GetAll().Data;
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Authors"] = _authorService.GetAll().Data;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book, List<int> SelectedBookIds,IFormFile photoUrl)
        {
            var result = _bookService.Add(book, SelectedBookIds,photoUrl,_env.WebRootPath);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                ViewData["Authors"] = _authorService.GetAll().Data;
                return View(book);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var bookResult = _bookService.GetById(id).Data;
            var selectedBookIds = new List<int>();
            if (bookResult.BookAuthors != null)
            {
                selectedBookIds = bookResult.BookAuthors.Select(ba => ba.AuthorId).ToList();
            }
            ViewData["Authors"] = _authorService.GetAll().Data;
            ViewData["SelectedBookIds"] = selectedBookIds;

            return View(bookResult);
        }


        [HttpPost]
        public IActionResult Edit(Book book, List<int> selectedAuthorIds, IFormFile photoUrl)
        {
            var result = _bookService.Update(book, selectedAuthorIds, photoUrl, _env.WebRootPath);

            if (result.IsSuccess)
                return RedirectToAction("Index");

            ViewData["Authors"] = _authorService.GetAll().Data;
            ViewData["SelectedBookIds"] = selectedAuthorIds;

            return View(book);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);
            if (result.IsSuccess)
                return RedirectToAction("Index");
            return View(result);
        }
    }
}
