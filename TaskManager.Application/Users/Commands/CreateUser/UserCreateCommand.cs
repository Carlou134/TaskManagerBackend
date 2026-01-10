
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Users.Commands.CreateUser
{
    public class UserCreateCommand : IRequest<UserCreateResponse>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }

    public record UserCreateResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}
