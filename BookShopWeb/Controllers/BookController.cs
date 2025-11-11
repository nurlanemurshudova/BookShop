using BookShopWeb.ViewModels;
using Business.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IBasketItemService _basketItemService;
        private readonly IBasketService _basketService;
        public BookController(IBookService bookService, IAuthorService authorService, IBookAuthorService bookAuthorService, IBasketItemService basketItemService, IBasketService basketService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
            _basketItemService = basketItemService;
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            var bookData = _bookService.GetAll().Data;
            var authorData = _authorService.GetAll().Data;
            var bookAuthorData = _bookAuthorService.GetAll().Data;

            BookViewModel model = new()
            {
                Books = bookData,
                Authors = authorData,
                BookAuthors = bookAuthorData
            };
            return View(model);
        }



        [HttpPost]
        public IActionResult AddToBasket(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var basketResult = _basketService.GetByUserId(userId.Value);
            var basket = basketResult?.Data;

            if (basket == null)
            {
                basket = new Basket { UserId = userId.Value };
                _basketService.Add(basket);
            }

            var activeItem = basket.Items.FirstOrDefault(x => x.BookId == bookId && x.Deleted == 0);

            if (activeItem != null)
            {
                activeItem.Quantity++;
                activeItem.LastUpdatedDate = DateTime.Now;
                _basketItemService.Update(activeItem);
            }
            else
            {

                var deletedItem = basket.Items.FirstOrDefault(x => x.BookId == bookId && x.Deleted != 0);

                if (deletedItem != null)
                {
                    deletedItem.Deleted = 0; 
                    deletedItem.Quantity = 1; 
                    deletedItem.LastUpdatedDate = DateTime.Now;
                    _basketItemService.Update(deletedItem);
                }
                else
                {
                    var book = _bookService.GetById(bookId).Data;
                    if (book == null)
                        return Json(new { success = false, message = "Book not found" });

                    decimal discountFactor = 1 - ((decimal)book.DiscountRate / 100);
                    decimal discountedPrice = book.Price * discountFactor;

                    BasketItem newItem = new()
                    {
                        BasketId = basket.Id,
                        BookId = bookId,
                        Quantity = 1,
                        PriceAtTimeOfAddition = discountedPrice
                    };
                    _basketItemService.Add(newItem);
                }
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
