using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Toscan_Insaat_Final.Areas.Admin.Dtos.ServiceDtos;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationContext _context;

        public ServiceController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> result = await _context.Services
                                                    .OrderByDescending(x => x.Id)
                                                        .ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ServiceCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateDto createDto)
        {

            if (!ModelState.IsValid)
                return View(createDto);

            var extension = Path.GetExtension(createDto.ServiceImage.FileName);
            var imageName = Guid.NewGuid() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/", imageName);
            var stream = new FileStream(path, FileMode.Create);
            await createDto.ServiceImage.CopyToAsync(stream);

            Service service = new()
            {
                Name = createDto.Name,
                Description = createDto.Description,
                ImageUrl = imageName,
                CreatedDate = DateTime.Now,
                Status = createDto.Status
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
                return RedirectToAction(nameof(Index));


            ServiceUpdateDto updateDto = new()
            {
                ServiceId = service.Id,
                Name = service.Name,
                Description = service.Description,
                ServiceImageUrl = service.ImageUrl,
                ServiceStatus = service.Status,
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateDto updateDto)
        {
            var updatedService = await _context.Services.FirstOrDefaultAsync(x => x.Id == updateDto.ServiceId);
            if (updatedService == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            updatedService.Name = updateDto.Name;
            updatedService.Description = updateDto.Description;
            updatedService.Status = updateDto.ServiceStatus;
            updatedService.UpdatedDate = DateTime.Now;

            if (updateDto.ServiceImage != null && updateDto.ServiceImage.Length > 0)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/" + updatedService.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/" + updatedService.ImageUrl));
                }

                var extension = Path.GetExtension(updateDto.ServiceImage.FileName);
                var imageName = Guid.NewGuid() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/", imageName);
                var stream = new FileStream(path, FileMode.Create);
                await updateDto.ServiceImage.CopyToAsync(stream);

                updatedService.ImageUrl = imageName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/" + service.ImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services/" + service.ImageUrl));
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
