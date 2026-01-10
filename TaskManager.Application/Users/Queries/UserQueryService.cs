using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Auth.Commands.Login;
using TaskManager.Application.Common.Dtos;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Users.Queries
{
    public interface IUserQueryService
    {
        Task<UserDto> GetUser(LoginCommand command, CancellationToken cancellationToken);
    }

    public class UserQueryService : IUserQueryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserQueryService> _logger;

        public UserQueryService(
            AppDbContext context, 
            IMapper mapper,
            ILogger<UserQueryService> logger
            )
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> GetUser(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.User.AsNoTracking()
                    .FirstOrDefaultAsync(x => command.UserName == x.Name, cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("The password isn't correct.");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
