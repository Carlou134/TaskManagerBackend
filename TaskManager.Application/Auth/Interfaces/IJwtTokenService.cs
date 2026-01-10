using TaskManager.Application.Auth.Dtos;

namespace TaskManager.Application.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        AuthResponseDto GenerateToken(UserDto user);
    }
}
