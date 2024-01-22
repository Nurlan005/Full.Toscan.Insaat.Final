namespace Toscan_Insaat_Final.Models
{
    public class SocialAccount
    {
        public int Id { get; set; }
        public string UrlLink { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
