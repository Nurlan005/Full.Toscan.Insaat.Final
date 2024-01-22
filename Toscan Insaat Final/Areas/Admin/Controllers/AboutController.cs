using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Toscan_Insaat_Final.Areas.Admin.Dtos.AboutDtos;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly ApplicationContext _context;

        public AboutController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<About> result = await _context.Abouts
                                                    .OrderByDescending(x => x.Id)
                                                        .ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AboutCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateDto createDto)
        {

            if (!ModelState.IsValid)
                return View(createDto);

            var extension = Path.GetExtension(createDto.ABackgroundImageUrl.FileName);
            var imageName = Guid.NewGuid() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/", imageName);
            var stream = new FileStream(path, FileMode.Create);
            await createDto.ABackgroundImageUrl.CopyToAsync(stream);

            var extension2 = Path.GetExtension(createDto.ACircleImageUrl.FileName);
            var imageName2 = Guid.NewGuid() + extension2;
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/", imageName2);
            var stream2 = new FileStream(path2, FileMode.Create);
            await createDto.ACircleImageUrl.CopyToAsync(stream2);

            About about = new()
            {
                Title = createDto.Name,
                BackgroundImageUrl = imageName,
                CircleImageUrl = imageName2,
                CreatedDate = DateTime.Now,
                Status = createDto.Status
            };

            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            About about = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (about == null)
                return RedirectToAction(nameof(Index));


            AboutUpdateDto updateDto = new()
            {
                AboutId = about.Id,
                Name = about.Title,
                ABackgroundImageUrl = about.ABackgroundImageUrl,
                ACircleImageUrl = about.ACircleImageUrl,
                AboutStatus = about.Status,
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AboutUpdateDto updateDto)
        {
            var updatedAbout = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == updateDto.AboutId);
            if (updatedAbout == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            updatedAbout.Title = updateDto.Name;
            updatedAbout.Status = updateDto.AboutStatus;
            updatedAbout.UpdatedDate = DateTime.Now;

            if (updateDto.ABackgroundImageUrl != null && updateDto.ABackgroundImageUrl.Length > 0)
            {

                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + updatedAbout.BackgroundImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + updatedAbout.BackgroundImageUrl));
                }

                var extension = Path.GetExtension(updateDto.ABackgroundImageUrl.FileName);
                var imageName = Guid.NewGuid() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/", imageName);
                var stream = new FileStream(path, FileMode.Create);
                await updateDto.ABackgroundImageUrl.CopyToAsync(stream);

                updatedAbout.BackgroundImageUrl = imageName;
            }

            if (updateDto.ACircleImageUrl != null && updateDto.ACircleImageUrl.Length > 0)
            {

                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + updatedAbout.ACircleImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + updatedAbout.ACircleImageUrl));
                }

                var extension2 = Path.GetExtension(updateDto.ACircleImageUrl.FileName);
                var imageName2 = Guid.NewGuid() + extension2;
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/", imageName2);
                var stream2 = new FileStream(path2, FileMode.Create);
                await updateDto.ACircleImageUrl.CopyToAsync(stream2); ;

                updatedAbout.BackgroundImageUrl = imageName2;
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            About about = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == id);

            if (about == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });


            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + about.BackgroundImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/abouts/" + about.BackgroundImageUrl));
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
    }
}
