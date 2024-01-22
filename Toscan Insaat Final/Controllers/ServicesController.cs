using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Dtos.ServiceDtos;

namespace Toscan_Insaat_Final.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationContext _context;

        public ServicesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ServiceDto> data = await _context.Services
                                .Where(x => x.Status)
                                .OrderByDescending(x => x.CreatedDate)
                                .Select(x => new ServiceDto()
                                {
                                    Name = x.Name,
                                    Description = x.Description,
                                    ImageUrl = x.ImageUrl,
                                }).ToListAsync();

            return View(data);
        }
    }
}
