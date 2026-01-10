using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Auth.Queries
{
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
            _context = context!;
            _mapper = mapper!;
            _logger = logger!;
        }

        public async Task<UserDto> GetUser(LoginRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.User.AsNoTracking()
                    .FirstOrDefaultAsync(x => request.UserName == x.Name, cancellationToken)
                    .ConfigureAwait(false);

                if (user == null)
                {
                    throw new ArgumentNullException("Usuario no encontrado");
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    throw new UnauthorizedAccessException("La contraseña no es correcta.");
                }

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al autenticar usuario: {ex.Message}", request);
                throw;
            }
        }
    }
}
