using CompuTestApi.Models;
using CompuTestApi.Models.Data;
using CompuTestApi.Models.Login;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace movie_app_graduation_api.Services.Authenticate
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<MoviesUser> _userManager;
        private readonly Jwt jwt;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthService(UserManager<MoviesUser> userManager, IOptions<Jwt> jwt, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this.jwt = jwt.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel() { Message = "Email is already registered." };
            }

            MoviesUser user = new()
            {
                UserName = model.Email?[..^model.Email[model.Email.IndexOf('@')..].Length],
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                StringBuilder errors = new(value: String.Empty);
                foreach (var item in result.Errors)
                {
                    errors.Append(value: $"{item.Description}");
                    if (item != result.Errors.Last())
                    {
                        errors.Append(value: ',');
                    }
                }
                return new AuthModel { Message = errors.ToString() };
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User.GetDisplayName());
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles =
                new List<string?> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }


        #region login
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }
        #endregion


        private async Task<JwtSecurityToken> CreateJwtToken(MoviesUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UID", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> ModifyPassword(MoviesUser user, String password)
        {

            if (user is null)
            {
                var message = "Email is not found!";

                return new AuthModel() { Message = message };
            }
            /*
                        new JwtSecurityTokenHandler().WriteToken(token)*/
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (String.IsNullOrEmpty(token))
            {
                return new AuthModel() { Message = "Invalid Token" };
            }
            var result = await _userManager.ResetPasswordAsync(user, token
                   , password);
            if (!result.Succeeded)
            {
                StringBuilder errors = new(value: String.Empty);
                foreach (var item in result.Errors)
                {
                    errors.Append(value: $"{item.Description}");
                    if (item != result.Errors.Last())
                    {
                        errors.Append(value: ',');
                    }
                }
                return new AuthModel() { Message = errors.ToString() };
            }
            return new AuthModel() { Message = "Password is Changed successfully" };
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<MoviesUser?> GetUserAsync(String email)
        {
            return await _userManager.FindByEmailAsync(email);

        }

    }
}
