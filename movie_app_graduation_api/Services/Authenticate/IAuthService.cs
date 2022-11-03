using CompuTestApi.Models;
using CompuTestApi.Models.Login;
using movie_app_graduation_api.Models;

namespace movie_app_graduation_api.Services.Authenticate
{
    public interface IAuthService
    {
        //Add Main Functions of Auth ==> Register , Login , AddRole

        public Task<AuthModel> RegisterAsync(RegisterModel model);
        public Task<AuthModel> LoginAsync(LoginModel model);
        public Task<String> AddRoleAsync(AddRoleModel model);
        public Task<MoviesUser?> GetUserAsync(String email);

        public Task<AuthModel> ModifyPassword(MoviesUser user, String password);

    }
}
