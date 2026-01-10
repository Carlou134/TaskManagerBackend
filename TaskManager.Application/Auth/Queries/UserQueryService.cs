using System.Runtime.CompilerServices;
using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Auth.Queries
{
    public class UserQueryService
    {
        private readonly AppDbContext _context;

        public UserQueryService(AppDbContext context)
        {
            _context = context!;
        }

        //public async Task<UserDto> GetUser(LoginRequestDto request, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var user = await _context.User.FindAsync(request.UserName, cancellationToken).ConfigureAwait(false);

        //        if(user == null)
        //        {
        //            throw new ArgumentNullException("No se encontró el usuario");
        //        }

                
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
