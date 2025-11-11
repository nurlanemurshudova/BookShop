using Business.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usersResult = _userService.GetAll();

            var user = usersResult.Data
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.Message = "Email və ya şifrə yanlışdır!";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            var basket = user.Basket;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = _userService.GetAll().Data
                .FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                ViewBag.Message = "Bu email artıq qeydiyyatdan keçib!";
                return View(model);
            }

            User newUser = new()
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            var result = _userService.Add(newUser);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Qeydiyyat uğurla tamamlandı!";
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Qeydiyyat zamanı xəta baş verdi!";
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
