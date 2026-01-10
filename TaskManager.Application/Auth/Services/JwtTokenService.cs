using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;
using TaskManager.Application.Users.Dtos;

namespace TaskManager.Application.Auth.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponseDto GenerateToken(UserDto user)
        {
            var jwt = _configuration.GetSection("Jwt");
            int expireMinutes = int.Parse(jwt["ExpireMinutes"]!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!)
                );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
                );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireMinutes = expireMinutes
            };
        }
    }
}
