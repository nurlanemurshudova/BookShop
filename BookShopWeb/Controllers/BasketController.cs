using Business.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
        public BasketController(IBasketService basketService, IBasketItemService basketItemService)
        {
            _basketService = basketService;
            _basketItemService = basketItemService;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Basketi userId ilə çəkirik
            var basketResult = _basketService.GetByUserId(userId.Value);
            var basket = basketResult.Data;

            if (basket == null || basket.Items == null || !basket.Items.Any())
            {
                ViewBag.Message = "Səbətiniz boşdur.";
                return View(new List<BasketItem>());
            }

            return View(basket.Items.Where(item => item.Deleted == 0).ToList());
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _basketItemService.Delete(id);
            if (result.IsSuccess)
                return RedirectToAction("Index");
            return View(result);
        }

    }
}
