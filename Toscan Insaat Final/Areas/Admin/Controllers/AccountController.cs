using Microsoft.AspNetCore.Mvc;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationContext _applicationContext;

        public AccountController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            string DbEmail = _applicationContext.Users.First().Email;
            string Password = _applicationContext.Users.First().Password;
            if (ModelState.IsValid)
            {
                if (user.Email == DbEmail && user.Password == Password)
                {
                    return RedirectToAction("Index", "Slider", new { area = "admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz giriş denemesi. Lütfen e-posta ve şifrenizi kontrol edin.");
                }
            }

            return View(user);

        }
    }

}



