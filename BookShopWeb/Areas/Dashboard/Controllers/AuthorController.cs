using Business.Abstract;
using Business.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        public AuthorController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        //AuthorManager _authorService = new();
        //BookManager _bookService = new();
        public IActionResult Index()
        {
            var data = _authorService.GetAll().Data;
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Books"] = _bookService.GetAll().Data;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            var result = _authorService.Add(author);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(author);


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _authorService.GetById(id).Data;
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            var result = _authorService.Update(author);
            if (result.IsSuccess)
                return RedirectToAction("Index");
            return View(author);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _authorService.Delete(id);
            if (result.IsSuccess)
                return RedirectToAction("Index");
            return View(result);
        }
    }
}
