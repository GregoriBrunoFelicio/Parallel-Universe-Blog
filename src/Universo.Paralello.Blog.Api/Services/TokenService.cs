using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(User usuario);
    }

    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public string GenerateToken(User usuario)
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
                    new Claim("Name", usuario.Name), 
                    new Claim("Email", usuario.Account.Email), 
                    new Claim(ClaimTypes.Role, "Admin"), 
                }),
                Expires = DateTime.Now.AddHours(tokenTimeExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
