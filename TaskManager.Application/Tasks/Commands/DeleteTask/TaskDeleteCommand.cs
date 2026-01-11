using MediatR;

namespace TaskManager.Application.Tasks.Commands.DeleteTask
{
    public class TaskDeleteCommand : IRequest<TaskDeleteMessage>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    public record TaskDeleteMessage
    {
        public string Message { get; set; } = string.Empty;
    }
}
