using TaskManager.Application.Auth.Dtos;

namespace TaskManager.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken);
    }
}
