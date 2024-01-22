using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.ServiceDtos
{
    public class ServiceCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool Status { get; set;}
        [Required]
        public IFormFile ServiceImage { get; set; }
    }
}
