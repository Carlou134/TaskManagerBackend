using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Users.Commands.CreateUser
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserCreateResponse>
    {
        private readonly AppDbContext _context;

        public UserCreateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserCreateResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            if (await _context.User.AnyAsync(x => x.Name == request.Name || x.Email == request.Email, cancellationToken))
            {
                throw new InvalidOperationException("The name or email is already registered");
            }

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreateDate = DateTime.UtcNow,
                RoleId = 1
            };

            _context.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            return new UserCreateResponse
            {
                Message = "User successfully registered!"
            };
        }
    }
}
