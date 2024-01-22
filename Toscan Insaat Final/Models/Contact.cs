using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Adınız standartlara uyğun deyil!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Boş qala bilməz!")]
        [EmailAddress(ErrorMessage = "E-poçt standartlara uyğun deyil!")]
        public string? Email { get; set; }

        public string? Message { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

