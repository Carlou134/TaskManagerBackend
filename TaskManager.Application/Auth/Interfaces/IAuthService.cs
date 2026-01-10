using TaskManager.Application.Auth.Dtos;
using TaskManager.Domain;

namespace TaskManager.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginRequestDto request);
        Task<AuthResponseDto> GenerateToken(User user);
    }
}
