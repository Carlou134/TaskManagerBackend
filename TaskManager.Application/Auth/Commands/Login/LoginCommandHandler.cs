using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Users.Queries;

namespace TaskManager.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserQueryService _userQueryService;

        public LoginCommandHandler(IConfiguration configuration, IUserQueryService userQueryService) 
        {
            _configuration = configuration;
            _userQueryService = userQueryService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryService.GetUser(request, cancellationToken);

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

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireMinutes = expireMinutes
            };
        }
    }
}
