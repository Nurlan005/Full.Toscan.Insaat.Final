using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Dtos.ProjectDtos;

namespace Toscan_Insaat_Final.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationContext _context;

        public ProjectController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProjectDto> data = await _context.Projects
                                .Where(x => x.Status)
                                .OrderByDescending(x => x.CreatedDate)
                                .Select(x => new ProjectDto()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Description = x.Description,
                                    ImageUrl = x.ImageUrl
                                })
                               .ToListAsync();
            // hardadi ?
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _context.Projects.Where(x => x.Id == id).Select(x=>new ProjectDetailDto
            {
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            }).FirstOrDefaultAsync();

            if (data == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(data);
        }

    }
}
