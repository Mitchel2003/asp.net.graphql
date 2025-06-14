using System.ComponentModel.DataAnnotations;

namespace AppLogin.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
