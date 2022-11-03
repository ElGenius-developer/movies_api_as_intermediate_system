using CompuTestApi.Models.Login;
using movie_app_graduation_api.Models;
using movie_app_graduation_api.Services.Authenticate;

namespace CalculatorClient
{
    public class HandelAuth
    {
        readonly AuthService AuthService;

        public HandelAuth(AuthService authService)
        {
            AuthService = authService;
        }

        public async Task<AuthModel> TestAuth()
        {
            var user = await AuthService.LoginAsync(new LoginModel()
            { Email = "ahmed.abdalmola555@gmail.com", Password = "Joker1997@" });

            return user;
        }
    }
}
