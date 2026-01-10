using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;

namespace TaskManager.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IJwtTokenService _jwtService;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public AuthService(
            IUserQueryService userQueryService,
            IJwtTokenService jwtService,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _userQueryService = userQueryService!;
            _jwtService = jwtService!;
            _configuration = configuration!;
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _userQueryService.GetUser(request, cancellationToken).ConfigureAwait(false);
            return _jwtService.GenerateToken(user);
        }
    }
}
