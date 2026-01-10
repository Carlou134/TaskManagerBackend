using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public int ExpireMinutes { get; set; }
    }
}
