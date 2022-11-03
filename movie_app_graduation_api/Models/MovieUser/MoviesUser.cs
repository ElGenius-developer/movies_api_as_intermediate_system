

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace movie_app_graduation_api.Models
{
    public class MoviesUser : IdentityUser
    {
        [Required, MaxLength(70)]
        public String? FullName { get; set; }
        [Required, MaxLength(100)]
        public String? Address { get; set; }

    }
}
