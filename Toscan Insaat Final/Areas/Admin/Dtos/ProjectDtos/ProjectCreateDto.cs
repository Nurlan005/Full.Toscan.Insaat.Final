using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.ProjectDtos
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public IFormFile ProjectImage { get; set; }
    }
}
