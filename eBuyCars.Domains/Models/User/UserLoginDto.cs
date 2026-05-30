using System.ComponentModel.DataAnnotations;

namespace eBuyCars.Domain.Models.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email or username is required")]
        public string CredentialType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
