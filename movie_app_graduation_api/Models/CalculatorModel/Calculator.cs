namespace movie_app_graduation_api.Models
{
    public partial class Calculator
    {
        [Key]
        public int Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public string OperationName { get; set; } = null!;
    }
}
