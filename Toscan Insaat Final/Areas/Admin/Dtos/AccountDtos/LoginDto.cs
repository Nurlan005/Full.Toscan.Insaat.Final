using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Areas.Admin.Dtos.AccountDtos
{
    public class LoginCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
