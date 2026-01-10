using Microsoft.Extensions.Configuration;
using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;
using TaskManager.Domain;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Api.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context!;
            _configuration = configuration!;
        }

        //public async Task<AuthResponseDto> Login(LoginRequestDto request)
        //{
            
        //}

        //private async Task<AuthResponseDto> GenerateToken(User user)
        //{
        //    var jwt = _configuration.GetSection("Jwt");


        //}
    }
}
