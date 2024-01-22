namespace Toscan_Insaat_Final.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string? SlideImageUrl { get; set; }
        public string? Title { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
