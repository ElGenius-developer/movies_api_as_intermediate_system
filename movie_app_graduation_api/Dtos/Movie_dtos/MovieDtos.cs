namespace MoviesApi.Dtos
{
    public class MovieDtos
    {

        [MaxLength(250)]
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string? Storeline { get; set; }
        public string Poster { get; set; } = null!;
        public byte GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
