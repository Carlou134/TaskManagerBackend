using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Users.Dtos;

namespace TaskManager.Application.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        AuthResponseDto GenerateToken(UserDto user);
    }
}
