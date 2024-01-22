using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.ProjectDtos
{
    public class ProjectUpdateDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ProjectImageUrl { get; set; }
        public IFormFile? ProjectImage { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool ProjectStatus { get; set; }
    }
}
