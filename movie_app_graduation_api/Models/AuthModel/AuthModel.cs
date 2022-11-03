namespace movie_app_graduation_api.Models
{
    public class AuthModel
    {
        public string? Token { get; set; }
        public string? Message { get; set; }

        public string? Username { get; internal set; }
        public bool IsAuthenticated { get; set; }
        public string? Email { get; internal set; }
        public DateTime ExpiresOn { get; internal set; }
        public List<string?>? Roles { get; internal set; }
    }
}
