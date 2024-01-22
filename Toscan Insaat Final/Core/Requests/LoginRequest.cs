using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Toscan_Insaat_Final.Core.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
