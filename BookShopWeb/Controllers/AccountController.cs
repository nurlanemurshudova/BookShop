using Business.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usersResult = _userService.GetAll();
            var user = usersResult.Data
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.Message = "Email və ya şifrə yanlışdır";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (user.Role == "Admin")
                return RedirectToAction("Index", "Home", new { area = "Dashboard" });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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

            var user = _userService.GetAll().Data
                .FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
            {
                ViewBag.Message = "Bu email qeydiyyatdan keçib";
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
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Xəta baş verdi";
            return View(model);
        }

    }
}
