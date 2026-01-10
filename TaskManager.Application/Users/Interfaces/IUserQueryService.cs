using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Users.Dtos;

namespace TaskManager.Application.Users.Interfaces
{
    public interface IUserQueryService
    {
        Task<UserDto> GetUser(LoginRequestDto request, CancellationToken cancellationToken);
    }
}
