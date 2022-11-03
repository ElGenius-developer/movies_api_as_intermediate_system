using System.Diagnostics.CodeAnalysis;

namespace MoviesApi.Dtos
{
    public class GenreDtos
    {
        [MaxLength(100)]
        [Required]
        [NotNull]
        public string Name { set; get; } = null!;
    }
}
