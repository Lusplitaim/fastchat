using System.ComponentModel.DataAnnotations;

namespace FastChat.Core.Models
{
    public class SignUser
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
