using System.ComponentModel.DataAnnotations;

namespace CompuTestApi.Models
{
    public class RegisterModel
    {
        [Required, MaxLength(50)]
        public String? FullName { get; set; }
        [Required, MaxLength(50)]
        public string? Email { get; set; }
        [Required, MaxLength(15)]
        public String? PhoneNumber { get; set; }

        [Required, MaxLength(100)]
        public String? Address { get; set; }
        [Required, MaxLength(200)]
        public string? Password { get; set; }
    }
}
