using System.ComponentModel.DataAnnotations.Schema;

namespace movie_app_graduation_api.Models
{
    [Index("GenreId", Name = "IX_Movies_GenreId")]
    public partial class Movie
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
        [StringLength(2500)]
        public string Storeline { get; set; } = null!;
        public string Poster { get; set; } = null!;
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        [InverseProperty("Movies")]
        public virtual Genre Genre { get; set; } = null!;
    }
}
