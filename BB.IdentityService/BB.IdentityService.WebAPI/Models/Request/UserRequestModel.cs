using System.ComponentModel.DataAnnotations;

namespace BB.IdentityService.WebAPI.Models.Request
{
    public class UserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
