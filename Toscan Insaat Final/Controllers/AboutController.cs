using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Dtos.AboutDtos;

namespace Toscan_Insaat_Final.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationContext _context;

        public AboutController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _context.Abouts
                                .Where(x => x.Status)
                                .Select(x => new AboutDto()
                                {
                                    Title = x.Title,
                                    Description = x.Description,
                                    BackgroundImageUrl = x.BackgroundImageUrl,
                                    CircleImageUrl = x.CircleImageUrl
                                }).FirstOrDefaultAsync();

            return View(data);
        }
    }
}
