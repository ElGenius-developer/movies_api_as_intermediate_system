using System.ComponentModel.DataAnnotations;

namespace movie_app_graduation_api.Models
{
    public class AddRoleModel
    {
        [Required, MaxLength(70)]
        public string? UserId { get; internal set; }
        [Required, MaxLength(20)]
        public string? Role { get; internal set; }
    }
}
