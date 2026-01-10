using TaskManager.Application.Auth.Dtos;

namespace TaskManager.Application.Auth.Interfaces
{
    public interface IUserQueryService
    {
        Task<UserDto> GetUser(LoginRequestDto request, CancellationToken cancellationToken);
    }
}
