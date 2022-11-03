using System.ComponentModel.DataAnnotations;

namespace CompuTestApi.Models.Login
{
    public class LoginModel
    {
        [Required]
        public String Email { get; set; } = null!;
        [Required]
        public String Password { get; set; } = null!;
    }
}
