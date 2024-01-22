using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Areas.Admin.Dtos.SliderDtos;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ApplicationContext _context;

        public SliderController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> result = await _context.Slides
                                                    .OrderByDescending(x => x.Id)
                                                        .ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SliderCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateDto createDto)
        {

            if (!ModelState.IsValid)
                return View(createDto);

            var extension = Path.GetExtension(createDto.SliderImage.FileName);
            var imageName = Guid.NewGuid() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/", imageName);
            var stream = new FileStream(path, FileMode.Create);
            await createDto.SliderImage.CopyToAsync(stream);

            Slider slider = new()
            {
                Title = createDto.Title,
                SlideImageUrl = imageName,
                CreatedDate = DateTime.Now,
                Status = createDto.Status
            };

            await _context.Slides.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Slider slider = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
                return RedirectToAction(nameof(Index));


            SliderUpdateDto updateDto = new()
            {
                SliderId = slider.Id,
                Title = slider.Title,
                SliderImageUrl = slider.SlideImageUrl,
                SliderStatus = slider.Status,
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SliderUpdateDto updateDto)
        {
            var updatedSlider = await _context.Slides.FirstOrDefaultAsync(x => x.Id == updateDto.SliderId);
            if (updatedSlider == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            updatedSlider.Title = updateDto.Title;
            updatedSlider.Status = updateDto.SliderStatus;
            updatedSlider.UpdatedDate = DateTime.Now;

            if (updateDto.SliderImage != null && updateDto.SliderImage.Length > 0)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/" + updatedSlider.SlideImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/" + updatedSlider.SlideImageUrl));
                }

                var extension = Path.GetExtension(updateDto.SliderImage.FileName);
                var imageName = Guid.NewGuid() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/", imageName);
                var stream = new FileStream(path, FileMode.Create);
                await updateDto.SliderImage.CopyToAsync(stream);

                updatedSlider.SlideImageUrl = imageName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id);

            if (slider == null)
                return RedirectToAction("Error", "Home", new { area = "Admin" });

            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/" + slider.SlideImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders/" + slider.SlideImageUrl));
            }

            _context.Slides.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
