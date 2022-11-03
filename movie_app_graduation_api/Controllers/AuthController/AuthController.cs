using CompuTestApi.Models;
using CompuTestApi.Models.Login;
using Microsoft.AspNetCore.Mvc;
using movie_app_graduation_api.Models;
using movie_app_graduation_api.Services.Authenticate;

namespace movie_app_graduation_api.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthModel result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            return Ok(result);
        }
        [HttpPost("add_role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpPost("Reset_password")]
        public async Task<IActionResult> ResetPasswordAsync(LoginModel loginCred)
        {

            if (!ModelState.IsValid || loginCred.Email is null)
                return BadRequest(ModelState);

            try
            {


                var user = await _authService.GetUserAsync(loginCred.Email);
                if (user is null)
                    return BadRequest("Email is not registered");



                var result = await _authService.ModifyPassword(user, loginCred.Password);
                if (result == null)
                    return BadRequest("some errors occurred");

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }


        }
    }
}
