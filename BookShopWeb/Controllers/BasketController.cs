using Business.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims; 

namespace BookShopWeb.Controllers
{
    [Authorize(Roles ="User")]
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var basketResult = _basketService.GetByUserId(userId);
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

        [HttpPost]
        public IActionResult UpdateQuantity(int id, string actionType)
        {
            var data = _basketItemService.GetById(id).Data;
            if (data == null)
            {
                ViewBag.Message = "Məlumat tapılmadı.";
            }
            if (actionType == "increase")
                data.Quantity++;
            else if (actionType == "decrease" && data.Quantity > 1)
                data.Quantity--;

            _basketItemService.Update(data);

            return RedirectToAction("Index");
        }
    }
}