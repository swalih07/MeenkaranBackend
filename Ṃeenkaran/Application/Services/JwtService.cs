using Ṃeenkaran.Domain.Entities.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Ṃeenkaran.Application.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        // USER token
        public string GenerateToken(User user)
        {
            return GenerateToken(user.Id, user.Name, user.Email, user.Role);
        }

        // GUIDE token
        public string GenerateToken(Guide guide)
        {
            return GenerateToken(guide.Id, guide.Name, guide.Email, "Guide");
        }

        // Common token generator
        public string GenerateToken(int id, string name, string email, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtKey = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("Jwt:Key is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // access token short expiry
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Refresh token generator
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}