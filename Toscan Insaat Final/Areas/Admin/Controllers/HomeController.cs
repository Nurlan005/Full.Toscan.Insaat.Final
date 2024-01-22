using Microsoft.AspNetCore.Mvc;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error() => View();
    }
}
