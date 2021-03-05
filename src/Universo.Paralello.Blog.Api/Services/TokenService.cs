using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Enums;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Parallel.Universe.Blog.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(User usuario);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public string GenerateToken(User user)
        {
            var tokenConfigurations = _configuration.GetSection("TokenConfiguration");
            var tokenKey = tokenConfigurations.GetSection("Key").Value;
            var tokenTimeExpiration = double.Parse(tokenConfigurations.GetSection("TokentimeExpiration").Value);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Account.Email),
                    new Claim(ClaimTypes.Role, Roles.Admin.ToString()),
                }),
                Expires = DateTime.Now.AddHours(tokenTimeExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
