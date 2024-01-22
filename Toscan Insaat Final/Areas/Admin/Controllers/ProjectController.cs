using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Areas.Admin.Dtos.ProjectDtos;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly ApplicationContext _context;

        public ProjectController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Project> result = await _context.Projects
                                                    .OrderByDescending(x => x.Id)
                                                        .ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Dtos.ProjectDtos.ProjectCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dtos.ProjectDtos.ProjectCreateDto createDto)
        {

            if (!ModelState.IsValid)
                return View(createDto);

            var extension = Path.GetExtension(createDto.ProjectImage.FileName);
            var imageName = Guid.NewGuid() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/", imageName);
            var stream = new FileStream(path, FileMode.Create);
            await createDto.ProjectImage.CopyToAsync(stream);

            Project project = new()
            {
                Name = createDto.Name,
                Description = createDto.Description,
                ImageUrl = imageName,
                CreatedDate = DateTime.Now,
                Status = createDto.Status,
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Project project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
                return RedirectToAction(nameof(Index));


            ProjectUpdateDto updateDto = new()
            {
                ProjectId = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectImageUrl = project.ImageUrl,
                ProjectStatus = project.Status,
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProjectUpdateDto updateDto)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == updateDto.ProjectId);
            if (updatedProject == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            updatedProject.Name = updateDto.Name;
            updatedProject.Description = updateDto.Description;
            updatedProject.Status = updateDto.ProjectStatus;
            updatedProject.UpdatedDate = DateTime.Now;

            if (updateDto.ProjectImage != null && updateDto.ProjectImage.Length > 0)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + updatedProject.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + updatedProject.ImageUrl));
                }

                var extension = Path.GetExtension(updateDto.ProjectImage.FileName);
                var imageName = Guid.NewGuid() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/", imageName);
                var stream = new FileStream(path, FileMode.Create);
                await updateDto.ProjectImage.CopyToAsync(stream);

                updatedProject.ImageUrl = imageName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Project project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });


            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + project.ImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/projects/" + project.ImageUrl));
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }


}
