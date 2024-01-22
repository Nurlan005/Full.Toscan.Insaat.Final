namespace Toscan_Insaat_Final.Models
{
    public class About
    {
        internal readonly IFormFile ABackgroundImageUrl;
        internal IFormFile ACircleImageUrl;

        public int Id { get; set; }
        public string? BackgroundImageUrl { get; set; }
        public string? CircleImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
