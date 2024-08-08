using Microsoft.IdentityModel.Tokens;
using WebAPI.Interface;
using WebAPI.Models;
using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"] ?? string.Empty));
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, appUser.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.GivenName, appUser.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.NameId, appUser.Id)
            };

            var userRoles = _userManager.GetRolesAsync(appUser).Result;

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHadler = new JwtSecurityTokenHandler();

            var token = tokenHadler.CreateToken(tokenDescripor);

            return tokenHadler.WriteToken(token);
        }
    }
}
