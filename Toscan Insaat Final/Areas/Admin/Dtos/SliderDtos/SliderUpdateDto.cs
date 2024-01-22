using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.SliderDtos
{
    public class SliderUpdateDto
    {
        [Required]
        public int SliderId { get; set; }
        [Required]
        public string Title { get; set; }
        public string SliderImageUrl { get; set; }
        [Required]
        public IFormFile? SliderImage { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool SliderStatus { get; set; }
    }
}
