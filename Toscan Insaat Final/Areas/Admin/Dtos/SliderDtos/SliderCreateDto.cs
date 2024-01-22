using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.SliderDtos
{
    public class SliderCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public bool Status { get; set;}
        [Required]
        public IFormFile SliderImage { get; set; }
    }
}
