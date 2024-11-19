using FinalProject.Models;
using FinalProject.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Helpers;

using Microsoft.AspNetCore.Http;

namespace FinalProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Job = model.Job
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new AuthModel { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

            
            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else
            {
                return new AuthModel { Message = $"Role {model.Role} does not exist!" };
            }

            var jwtToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                Roles = new List<string> { model.Role }
            };
        }

        public async Task<AuthModel> LoginAsync(LoginModel model , HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { Message = "Invalid email or password!" };

            var jwtToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            httpContext.Session.SetString("JWToken", new JwtSecurityTokenHandler().WriteToken(jwtToken));
            httpContext.Session.SetString("UserRole", roles.FirstOrDefault());
            httpContext.Session.SetString("UserId", user.Id);

          

            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                Roles = roles.ToList(), 
                UserId = user.Id


            };
        }

        

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials);
        }
    }
}
