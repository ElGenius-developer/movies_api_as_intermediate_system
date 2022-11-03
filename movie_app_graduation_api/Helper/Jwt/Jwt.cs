namespace movie_app_graduation_api.Helper
{
    public class Jwt
    {
        public String? Key { get; set; }
        public String? Issuer { get; set; }
        public String? Audience { get; set; }
        public int DurationInDays { get; set; }


    }
}
