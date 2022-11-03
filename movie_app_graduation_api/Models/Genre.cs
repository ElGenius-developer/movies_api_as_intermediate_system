using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace movie_app_graduation_api.Models
{
    [Index("Name", Name = "UC_Genres", IsUnique = true)]
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public DateTime Created_on { get; set; }

        [InverseProperty("Genre")]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
