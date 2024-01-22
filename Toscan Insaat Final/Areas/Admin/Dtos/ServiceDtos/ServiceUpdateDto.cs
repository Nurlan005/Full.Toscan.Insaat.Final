using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.ServiceDtos
{
    public class ServiceUpdateDto
    {
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ServiceImageUrl { get; set; }
        public IFormFile? ServiceImage { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool ServiceStatus { get; set; }
    }
}
