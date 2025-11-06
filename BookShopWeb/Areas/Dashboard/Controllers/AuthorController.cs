using Business.Abstract;
using Business.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookShopWeb.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AuthorController : Controller
    {
        //private readonly IAuthorService _authorService;
        //public AuthorController(IAuthorService authorService)
        //{
        //    _authorService = authorService;
        //}
        AuthorManager _authorService = new();
        BookManager _bookService = new();
        public IActionResult Index()
        {
            //var data = _authorService.GetAll().Data;
            var data = _authorService.GetAll().Data;
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Books"] = _bookService.GetAll().Data;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author, List<int> SelectedBookIds)
        {
            var result = _authorService.AddAuthorWithBooks(author, SelectedBookIds);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                ViewData["Books"] = _bookService.GetAll().Data;
                return View(author);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            //var data = _authorService.GetById(id).Data;
            //return View(data);
            var classResult = _authorService.GetById(id).Data; ;
            var selectedBookIds = new List<int>();
            if (classResult.BookAuthors != null)
            {
                selectedBookIds = classResult.BookAuthors.Select(ba => ba.BookId).ToList();
            }
            ViewData["Books"] = _bookService.GetAll().Data;
            ViewData["SelectedBookIds"] = selectedBookIds;

            return View(classResult);


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
