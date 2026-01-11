using MediatR;
using System.ComponentModel.DataAnnotations;
using TaskManager.Domain;

namespace TaskManager.Application.Tasks.Commands.CreateTask
{
    public class TaskCreateCommand : IRequest<TaskCreateMessage>
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }

    public record TaskCreateMessage
    {
        public string Message { get; set; } = string.Empty;
    }
}
