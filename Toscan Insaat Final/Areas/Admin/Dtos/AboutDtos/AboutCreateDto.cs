using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.AboutDtos
{
    public class AboutCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public IFormFile ABackgroundImageUrl { get; set; }
        [Required]
        public IFormFile ACircleImageUrl { get; set; }

    }
}
