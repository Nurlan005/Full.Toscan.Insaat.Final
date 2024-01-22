using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Dtos.AboutDtos;
using Toscan_Insaat_Final.Dtos.ServiceDtos;
using Toscan_Insaat_Final.Dtos.SliderDto;
using Toscan_Insaat_Final.ViewModels;

namespace Toscan_Insaat_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var slider = await _context.Slides
                                     .Where(x => x.Status)
                                     .Select(x => new SliderDto()
                                     {
                                         SlideImageUrl = x.SlideImageUrl,
                                         Title = x.Title,
                                         Status = x.Status

                                     }).ToListAsync();

            var services = await _context.Services
                                    .Where(x => x.Status)
                                    .OrderByDescending(x => x.CreatedDate)
                                    .Take(6)
                                    .Select(x => new ServiceDto()
                                    {
                                        Name = x.Name,
                                        Description = x.Description,
                                        ImageUrl = x.ImageUrl
                                    }).ToListAsync();

            var about = await _context.Abouts
                                    .Where(x => x.Status)
                                    .Select(x => new AboutDto()
                                    {
                                        Title = x.Title,
                                        Description = x.Description,
                                        BackgroundImageUrl = x.BackgroundImageUrl,
                                        CircleImageUrl = x.CircleImageUrl
                                    }).FirstOrDefaultAsync();


            HomeIndexVM vm = new()
            {
                Services = services,
                About = about,
                Slider=slider
            };


            return View(vm);
        }
        public IActionResult Error() { return View(); } 
    }
}
